using System;
using System.Globalization;
using System.IO;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using ZeroTrace.Domain;

namespace ZeroTrace.Presentation
{
    /// <summary>Stores only synthetic tutorial completion and attempt count on this device.</summary>
    public sealed class LocalPrototypeProgressStore : ILocalPrototypeProgressStore
    {
        [Serializable]
        private sealed class StoredProgress
        {
            public bool exerciseComplete;
            public int attemptCount;
        }

        private static readonly Regex PayloadPattern = new Regex(
            @"\A\s*\{\s*(?:""exerciseComplete""\s*:\s*(true|false)\s*,\s*""attemptCount""\s*:\s*(-?(?:0|[1-9][0-9]*))|""attemptCount""\s*:\s*(-?(?:0|[1-9][0-9]*))\s*,\s*""exerciseComplete""\s*:\s*(true|false))\s*\}\s*\z",
            RegexOptions.CultureInvariant);

        private readonly string filePath;

        public LocalPrototypeProgressStore()
            : this(Path.Combine(Application.persistentDataPath, "ZeroTrace", "local-prototype-progress.json"))
        {
        }

        public LocalPrototypeProgressStore(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("A local progress path is required.", nameof(filePath));
            }

            var resolvedPath = Path.GetFullPath(filePath);
            if (!IsWithinRoot(resolvedPath, Application.persistentDataPath) &&
                !IsWithinRoot(resolvedPath, Application.temporaryCachePath))
            {
                throw new ArgumentException("Prototype progress must stay inside Unity-owned local data or temporary storage.", nameof(filePath));
            }

            this.filePath = resolvedPath;
        }

        private static bool IsWithinRoot(string path, string root)
        {
            if (string.IsNullOrWhiteSpace(root))
            {
                return false;
            }

            var resolvedRoot = Path.GetFullPath(root).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
            var prefix = resolvedRoot + Path.DirectorySeparatorChar;
            return path.StartsWith(prefix, StringComparison.OrdinalIgnoreCase);
        }

        public LocalPrototypeProgress Load()
        {
            var commitPath = CommitFilePath;
            if (TryReadProgress(commitPath, out var progress) ||
                TryReadProgress(filePath, out progress) ||
                TryReadProgress(commitPath + ".bak", out progress) ||
                TryReadProgress(filePath + ".bak", out progress))
            {
                return progress;
            }

            return new LocalPrototypeProgress();
        }

        public void Save(LocalPrototypeProgress progress)
        {
            if (progress == null)
            {
                throw new ArgumentNullException(nameof(progress));
            }

            if (progress.attemptCount < 0 || (progress.exerciseComplete && progress.attemptCount == 0))
            {
                throw new LocalPrototypePersistenceException("The local progress values are invalid.");
            }

            var stored = new StoredProgress
            {
                exerciseComplete = progress.exerciseComplete,
                attemptCount = progress.attemptCount
            };
            var payload = JsonUtility.ToJson(stored);
            if (!TryParseProgress(payload, out _))
            {
                throw new LocalPrototypePersistenceException("The local progress could not be validated.");
            }

            var directory = Path.GetDirectoryName(filePath);
            var temporaryPath = filePath + ".tmp." + Guid.NewGuid().ToString("N");
            var committed = false;
            try
            {
                Directory.CreateDirectory(directory);
                EnsurePrimaryMatchesCommittedJournal();

                var bytes = new UTF8Encoding(false).GetBytes(payload);
                using (var stream = new FileStream(temporaryPath, FileMode.CreateNew, FileAccess.Write, FileShare.None))
                {
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Flush();
                }

                if (!TryReadProgress(temporaryPath, out _))
                {
                    throw new LocalPrototypePersistenceException("The temporary progress could not be validated.");
                }

                PromoteTemporaryFileToJournal(temporaryPath);
                committed = true;
                temporaryPath = null;
            }
            catch (LocalPrototypePersistenceException)
            {
                throw;
            }
            catch (IOException exception)
            {
                committed = IsCommittedCandidate(temporaryPath, payload);
                if (!committed)
                {
                    throw new LocalPrototypePersistenceException("The local progress could not be saved safely.", exception);
                }
            }
            catch (UnauthorizedAccessException exception)
            {
                committed = IsCommittedCandidate(temporaryPath, payload);
                if (!committed)
                {
                    throw new LocalPrototypePersistenceException("The local progress could not be saved safely.", exception);
                }
            }
            catch (ArgumentException exception)
            {
                committed = IsCommittedCandidate(temporaryPath, payload);
                if (!committed)
                {
                    throw new LocalPrototypePersistenceException("The local progress could not be saved safely.", exception);
                }
            }
            catch (NotSupportedException exception)
            {
                committed = IsCommittedCandidate(temporaryPath, payload);
                if (!committed)
                {
                    throw new LocalPrototypePersistenceException("The local progress could not be saved safely.", exception);
                }
            }
            catch (SecurityException exception)
            {
                committed = IsCommittedCandidate(temporaryPath, payload);
                if (!committed)
                {
                    throw new LocalPrototypePersistenceException("The local progress could not be saved safely.", exception);
                }
            }
            finally
            {
                DeleteOwnedTemporaryFile(temporaryPath);
            }

            if (committed)
            {
                // The journal rename/replacement is the commit point. This mirror is best-effort;
                // Load reads the journal first, so mirror failure cannot turn a committed save
                // into a reported failure whose state later appears on restart.
                TryMirrorCommittedJournal();
            }
        }

        private string CommitFilePath => filePath + ".commit";

        private void EnsurePrimaryMatchesCommittedJournal()
        {
            if (!TryReadProgress(CommitFilePath, out var committed) ||
                TryReadProgress(filePath, out var primary) && HasSameValues(primary, committed))
            {
                return;
            }

            MirrorCommittedJournal();
            if (!TryReadProgress(filePath, out primary) || !HasSameValues(primary, committed))
            {
                throw new LocalPrototypePersistenceException(
                    "The last committed progress could not be safely mirrored before the next save.");
            }
        }

        private void TryMirrorCommittedJournal()
        {
            try
            {
                MirrorCommittedJournal();
            }
            catch (LocalPrototypePersistenceException)
            {
                // The committed journal remains authoritative and readable.
            }
            catch (IOException)
            {
                // The committed journal remains authoritative and readable.
            }
            catch (UnauthorizedAccessException)
            {
                // The committed journal remains authoritative and readable.
            }
            catch (ArgumentException)
            {
                // The committed journal remains authoritative and readable.
            }
            catch (NotSupportedException)
            {
                // The committed journal remains authoritative and readable.
            }
            catch (SecurityException)
            {
                // The committed journal remains authoritative and readable.
            }
        }

        private void MirrorCommittedJournal()
        {
            var journalPath = CommitFilePath;
            if (!TryReadProgress(journalPath, out _))
            {
                return;
            }

            var temporaryPath = filePath + ".mirror.tmp." + Guid.NewGuid().ToString("N");
            try
            {
                File.Copy(journalPath, temporaryPath, false);
                if (!TryReadProgress(temporaryPath, out _))
                {
                    throw new LocalPrototypePersistenceException("The mirrored progress could not be validated.");
                }

                PromoteTemporaryFileToPrimary(temporaryPath);
                temporaryPath = null;
            }
            finally
            {
                DeleteOwnedTemporaryFile(temporaryPath);
            }
        }

        private void PromoteTemporaryFileToJournal(string temporaryPath)
        {
            var journalPath = CommitFilePath;
            if (!File.Exists(journalPath))
            {
                File.Move(temporaryPath, journalPath);
                return;
            }

            if (TryReadProgress(journalPath, out _))
            {
                File.Replace(temporaryPath, journalPath, journalPath + ".bak");
                return;
            }

            if (TryReadProgress(journalPath + ".bak", out _))
            {
                File.Replace(temporaryPath, journalPath, null);
                return;
            }

            File.Replace(temporaryPath, journalPath, FindUnusedRecoveryPath(journalPath));
        }

        private void PromoteTemporaryFileToPrimary(string temporaryPath)
        {
            if (!File.Exists(filePath))
            {
                File.Move(temporaryPath, filePath);
                return;
            }

            if (TryReadProgress(filePath, out _))
            {
                File.Replace(temporaryPath, filePath, filePath + ".bak");
                return;
            }

            if (TryReadProgress(filePath + ".bak", out _))
            {
                File.Replace(temporaryPath, filePath, null);
                return;
            }

            File.Replace(temporaryPath, filePath, FindUnusedRecoveryPath(filePath));
        }

        private bool IsCommittedCandidate(string temporaryPath, string payload)
        {
            if (!string.IsNullOrEmpty(temporaryPath) && File.Exists(temporaryPath))
            {
                return false;
            }

            return TryReadProgress(CommitFilePath, out var committed) &&
                   TryParseProgress(payload, out var candidate) &&
                   HasSameValues(committed, candidate);
        }

        private static bool HasSameValues(LocalPrototypeProgress left, LocalPrototypeProgress right)
        {
            return left != null && right != null &&
                   left.exerciseComplete == right.exerciseComplete &&
                   left.attemptCount == right.attemptCount;
        }

        private static void DeleteOwnedTemporaryFile(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch (IOException)
            {
                // Load ignores uncommitted .tmp files; cleanup is best-effort and operation-scoped.
            }
            catch (UnauthorizedAccessException)
            {
                // Load ignores uncommitted .tmp files; cleanup is best-effort and operation-scoped.
            }
            catch (SecurityException)
            {
                // Load ignores uncommitted .tmp files; cleanup is best-effort and operation-scoped.
            }
        }

        private static string FindUnusedRecoveryPath(string path)
        {
            var candidate = path + ".recovery";
            var suffix = 0;
            while (File.Exists(candidate) || Directory.Exists(candidate))
            {
                suffix++;
                candidate = path + ".recovery." + suffix.ToString(CultureInfo.InvariantCulture);
            }

            return candidate;
        }

        private static bool TryReadProgress(string path, out LocalPrototypeProgress progress)
        {
            progress = null;
            try
            {
                if (!File.Exists(path))
                {
                    return false;
                }

                return TryParseProgress(File.ReadAllText(path), out progress);
            }
            catch (IOException)
            {
                return false;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (SecurityException)
            {
                return false;
            }
        }

        private static bool TryParseProgress(string payload, out LocalPrototypeProgress progress)
        {
            progress = null;
            if (string.IsNullOrWhiteSpace(payload))
            {
                return false;
            }

            var match = PayloadPattern.Match(payload);
            if (!match.Success)
            {
                return false;
            }

            var completeText = match.Groups[1].Success ? match.Groups[1].Value : match.Groups[4].Value;
            var countText = match.Groups[2].Success ? match.Groups[2].Value : match.Groups[3].Value;
            if (!int.TryParse(countText, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out var attemptCount) ||
                attemptCount < 0)
            {
                return false;
            }

            var exerciseComplete = string.Equals(completeText, "true", StringComparison.Ordinal);
            if (exerciseComplete && attemptCount == 0)
            {
                return false;
            }

            progress = new LocalPrototypeProgress
            {
                exerciseComplete = exerciseComplete,
                attemptCount = attemptCount
            };
            return true;
        }
    }
}
