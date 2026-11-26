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
        Incorrect,
        Correct,
        AlreadyComplete
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

            if (progress.exerciseComplete)
            {
                return ExerciseSubmissionResult.AlreadyComplete;
            }

            progress.attemptCount++;
            LastExerciseFileId = syntheticFileId;
            var correct = string.Equals(syntheticFileId, RequiredSyntheticFileId, StringComparison.Ordinal);
            if (correct)
            {
                progress.exerciseComplete = true;
            }

            progressStore.Save(progress.Copy());
            return correct ? ExerciseSubmissionResult.Correct : ExerciseSubmissionResult.Incorrect;
        }

        public bool RestartExercise()
        {
            if (ComputerState != LocalComputerState.Ready)
            {
                return false;
            }

            progress = new LocalPrototypeProgress();
            progressStore.Save(progress.Copy());
            LastExerciseFileId = null;
            ActiveApp = LocalPrototypeApp.FileManager;
            return true;
        }
    }
}
