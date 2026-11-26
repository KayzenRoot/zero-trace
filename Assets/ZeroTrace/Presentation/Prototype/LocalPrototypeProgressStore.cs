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
            if (TryReadProgress(filePath, out var progress) ||
                TryReadProgress(filePath + ".bak", out progress))
            {
                return progress;
            }

            var directory = Path.GetDirectoryName(filePath);
            var pattern = Path.GetFileName(filePath) + ".tmp.*";
            string[] candidates;
            try
            {
                candidates = Directory.Exists(directory) ? Directory.GetFiles(directory, pattern) : Array.Empty<string>();
            }
            catch (IOException)
            {
                return new LocalPrototypeProgress();
            }
            catch (UnauthorizedAccessException)
            {
                return new LocalPrototypeProgress();
            }
            catch (SecurityException)
            {
                return new LocalPrototypeProgress();
            }

            Array.Sort(candidates, (left, right) =>
            {
                var timeOrder = File.GetLastWriteTimeUtc(right).CompareTo(File.GetLastWriteTimeUtc(left));
                return timeOrder != 0 ? timeOrder : string.CompareOrdinal(right, left);
            });

            foreach (var candidate in candidates)
            {
                if (TryReadProgress(candidate, out progress))
                {
                    return progress;
                }
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
            try
            {
                Directory.CreateDirectory(directory);
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

                PromoteTemporaryFile(temporaryPath);
                if (!TryReadProgress(filePath, out _))
                {
                    throw new LocalPrototypePersistenceException("The saved progress could not be verified.");
                }
            }
            catch (LocalPrototypePersistenceException)
            {
                throw;
            }
            catch (IOException exception)
            {
                throw new LocalPrototypePersistenceException("The local progress could not be saved safely.", exception);
            }
            catch (UnauthorizedAccessException exception)
            {
                throw new LocalPrototypePersistenceException("The local progress could not be saved safely.", exception);
            }
            catch (ArgumentException exception)
            {
                throw new LocalPrototypePersistenceException("The local progress could not be saved safely.", exception);
            }
            catch (NotSupportedException exception)
            {
                throw new LocalPrototypePersistenceException("The local progress could not be saved safely.", exception);
            }
            catch (SecurityException exception)
            {
                throw new LocalPrototypePersistenceException("The local progress could not be saved safely.", exception);
            }
        }

        private void PromoteTemporaryFile(string temporaryPath)
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

            File.Replace(temporaryPath, filePath, FindUnusedRecoveryPath());
        }

        private string FindUnusedRecoveryPath()
        {
            var candidate = filePath + ".recovery";
            var suffix = 0;
            while (File.Exists(candidate) || Directory.Exists(candidate))
            {
                suffix++;
                candidate = filePath + ".recovery." + suffix.ToString(CultureInfo.InvariantCulture);
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
