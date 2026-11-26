using System;
using System.IO;
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
            if (!File.Exists(filePath))
            {
                return new LocalPrototypeProgress();
            }

            try
            {
                var stored = JsonUtility.FromJson<StoredProgress>(File.ReadAllText(filePath));
                return stored == null
                    ? new LocalPrototypeProgress()
                    : new LocalPrototypeProgress
                    {
                        exerciseComplete = stored.exerciseComplete,
                        attemptCount = Math.Max(0, stored.attemptCount)
                    };
            }
            catch (IOException)
            {
                return new LocalPrototypeProgress();
            }
            catch (UnauthorizedAccessException)
            {
                return new LocalPrototypeProgress();
            }
            catch (ArgumentException)
            {
                return new LocalPrototypeProgress();
            }
        }

        public void Save(LocalPrototypeProgress progress)
        {
            if (progress == null)
            {
                throw new ArgumentNullException(nameof(progress));
            }

            var directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var stored = new StoredProgress
            {
                exerciseComplete = progress.exerciseComplete,
                attemptCount = Math.Max(0, progress.attemptCount)
            };
            File.WriteAllText(filePath, JsonUtility.ToJson(stored));
        }
    }
}
