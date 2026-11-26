using System;

namespace ZeroTrace.Domain
{
    public enum LocalComputerState
    {
        Off,
        Booting,
        Ready,
        Shutdown
    }

    public enum LocalPrototypeApp
    {
        None,
        Mail,
        FileManager,
        Academy
    }

    public enum ExerciseSubmissionResult
    {
        Unavailable,
        InvalidSelection,
        AttemptLimitReached,
        PersistenceFailed,
        Incorrect,
        Correct,
        AlreadyComplete
    }

    public sealed class LocalPrototypePersistenceException : Exception
    {
        public LocalPrototypePersistenceException(string message, Exception innerException = null)
            : base(message, innerException)
        {
        }
    }

    [Serializable]
    public sealed class LocalPrototypeProgress
    {
        public bool exerciseComplete;
        public int attemptCount;

        public LocalPrototypeProgress Copy() => new LocalPrototypeProgress
        {
            exerciseComplete = exerciseComplete,
            attemptCount = Math.Max(0, attemptCount)
        };
    }

    public interface ILocalPrototypeProgressStore
    {
        LocalPrototypeProgress Load();
        void Save(LocalPrototypeProgress progress);
    }

    /// <summary>Deterministic, Unity-independent flow for the local technical prototype.</summary>
    public sealed class LocalPrototypeFlow
    {
        public const string RequiredSyntheticFileId = "recovery_manifest";

        private readonly ILocalPrototypeProgressStore progressStore;
        private LocalPrototypeProgress progress;

        public LocalComputerState ComputerState { get; private set; } = LocalComputerState.Off;
        public LocalPrototypeApp ActiveApp { get; private set; } = LocalPrototypeApp.None;
        public bool ExerciseComplete => progress.exerciseComplete;
        public int AttemptCount => progress.attemptCount;
        public string LastExerciseFileId { get; private set; }
        public bool LastPersistenceFailed { get; private set; }

        public LocalPrototypeFlow(ILocalPrototypeProgressStore progressStore)
        {
            this.progressStore = progressStore ?? throw new ArgumentNullException(nameof(progressStore));
            progress = progressStore.Load()?.Copy() ?? new LocalPrototypeProgress();
        }

        public LocalPrototypeProgress ReadProgress() => progress.Copy();

        public bool BeginBoot()
        {
            if (ComputerState != LocalComputerState.Off && ComputerState != LocalComputerState.Shutdown)
            {
                return false;
            }

            ComputerState = LocalComputerState.Booting;
            ActiveApp = LocalPrototypeApp.None;
            return true;
        }

        public bool CompleteBoot()
        {
            if (ComputerState != LocalComputerState.Booting)
            {
                return false;
            }

            ComputerState = LocalComputerState.Ready;
            return true;
        }

        public bool PowerOff()
        {
            if (ComputerState != LocalComputerState.Booting && ComputerState != LocalComputerState.Ready)
            {
                return false;
            }

            ComputerState = LocalComputerState.Shutdown;
            ActiveApp = LocalPrototypeApp.None;
            return true;
        }

        public bool OpenApp(LocalPrototypeApp app)
        {
            if (ComputerState != LocalComputerState.Ready || app == LocalPrototypeApp.None ||
                !Enum.IsDefined(typeof(LocalPrototypeApp), app))
            {
                return false;
            }

            ActiveApp = app;
            return true;
        }

        public bool CloseApp()
        {
            if (ComputerState != LocalComputerState.Ready || ActiveApp == LocalPrototypeApp.None)
            {
                return false;
            }

            ActiveApp = LocalPrototypeApp.None;
            return true;
        }

        public ExerciseSubmissionResult SubmitExerciseFile(string syntheticFileId)
        {
            if (ComputerState != LocalComputerState.Ready || ActiveApp != LocalPrototypeApp.FileManager)
            {
                return ExerciseSubmissionResult.Unavailable;
            }

            if (!IsKnownSyntheticFile(syntheticFileId))
            {
                return ExerciseSubmissionResult.InvalidSelection;
            }

            if (progress.exerciseComplete)
            {
                return ExerciseSubmissionResult.AlreadyComplete;
            }

            if (progress.attemptCount == int.MaxValue)
            {
                return ExerciseSubmissionResult.AttemptLimitReached;
            }

            var candidate = progress.Copy();
            candidate.attemptCount++;
            var correct = string.Equals(syntheticFileId, RequiredSyntheticFileId, StringComparison.Ordinal);
            if (correct)
            {
                candidate.exerciseComplete = true;
            }

            if (!TrySave(candidate))
            {
                return ExerciseSubmissionResult.PersistenceFailed;
            }

            progress = candidate;
            LastExerciseFileId = syntheticFileId;
            return correct ? ExerciseSubmissionResult.Correct : ExerciseSubmissionResult.Incorrect;
        }

        public bool RestartExercise()
        {
            if (ComputerState != LocalComputerState.Ready)
            {
                return false;
            }

            var candidate = new LocalPrototypeProgress();
            if (!TrySave(candidate))
            {
                return false;
            }

            progress = candidate;
            LastExerciseFileId = null;
            ActiveApp = LocalPrototypeApp.FileManager;
            return true;
        }

        private bool TrySave(LocalPrototypeProgress candidate)
        {
            try
            {
                progressStore.Save(candidate.Copy());
                LastPersistenceFailed = false;
                return true;
            }
            catch (LocalPrototypePersistenceException)
            {
                LastPersistenceFailed = true;
                return false;
            }
        }

        private static bool IsKnownSyntheticFile(string syntheticFileId)
        {
            return string.Equals(syntheticFileId, "briefing", StringComparison.Ordinal) ||
                   string.Equals(syntheticFileId, RequiredSyntheticFileId, StringComparison.Ordinal) ||
                   string.Equals(syntheticFileId, "shift_note", StringComparison.Ordinal);
        }
    }
}
