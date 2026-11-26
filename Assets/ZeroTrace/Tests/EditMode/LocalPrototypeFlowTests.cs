using NUnit.Framework;
using ZeroTrace.Domain;

namespace ZeroTrace.Domain.Tests
{
    public sealed class LocalPrototypeFlowTests
    {
        private sealed class MemoryProgressStore : ILocalPrototypeProgressStore
        {
            public LocalPrototypeProgress Value = new LocalPrototypeProgress();
            public int SaveCount;
            public bool FailNextSave;
            public LocalPrototypeProgress Load() => Value.Copy();
            public void Save(LocalPrototypeProgress progress)
            {
                SaveCount++;
                if (FailNextSave)
                {
                    FailNextSave = false;
                    throw new LocalPrototypePersistenceException("Synthetic save failure.");
                }

                Value = progress.Copy();
            }
        }

        [Test]
        public void ComputerRequiresBootBeforeReadyAndCanRestartAfterShutdown()
        {
            var flow = new LocalPrototypeFlow(new MemoryProgressStore());

            Assert.That(flow.ComputerState, Is.EqualTo(LocalComputerState.Off));
            Assert.That(flow.CompleteBoot(), Is.False);
            Assert.That(flow.BeginBoot(), Is.True);
            Assert.That(flow.ComputerState, Is.EqualTo(LocalComputerState.Booting));
            Assert.That(flow.BeginBoot(), Is.False);
            Assert.That(flow.CompleteBoot(), Is.True);
            Assert.That(flow.PowerOff(), Is.True);
            Assert.That(flow.ComputerState, Is.EqualTo(LocalComputerState.Shutdown));
            Assert.That(flow.BeginBoot(), Is.True);
            Assert.That(flow.CompleteBoot(), Is.True);
        }

        [Test]
        public void ApplicationsRequireReadyComputerAndCloseDeterministically()
        {
            var flow = new LocalPrototypeFlow(new MemoryProgressStore());

            Assert.That(flow.OpenApp(LocalPrototypeApp.Mail), Is.False);
            flow.BeginBoot();
            flow.CompleteBoot();
            Assert.That(flow.OpenApp(LocalPrototypeApp.None), Is.False);
            Assert.That(flow.OpenApp(LocalPrototypeApp.Mail), Is.True);
            Assert.That(flow.ActiveApp, Is.EqualTo(LocalPrototypeApp.Mail));
            Assert.That(flow.CloseApp(), Is.True);
            Assert.That(flow.CloseApp(), Is.False);
        }

        [Test]
        public void ExerciseReportsFailureThenSuccessAndPersistsAttempts()
        {
            var store = new MemoryProgressStore();
            var flow = ReadyFileManager(store);

            Assert.That(flow.SubmitExerciseFile("shift_note"), Is.EqualTo(ExerciseSubmissionResult.Incorrect));
            Assert.That(flow.ExerciseComplete, Is.False);
            Assert.That(flow.AttemptCount, Is.EqualTo(1));
            Assert.That(flow.SubmitExerciseFile(LocalPrototypeFlow.RequiredSyntheticFileId), Is.EqualTo(ExerciseSubmissionResult.Correct));
            Assert.That(flow.ExerciseComplete, Is.True);
            Assert.That(flow.AttemptCount, Is.EqualTo(2));
            Assert.That(flow.SubmitExerciseFile(LocalPrototypeFlow.RequiredSyntheticFileId), Is.EqualTo(ExerciseSubmissionResult.AlreadyComplete));

            var reloaded = new LocalPrototypeFlow(store);
            Assert.That(reloaded.ExerciseComplete, Is.True);
            Assert.That(reloaded.AttemptCount, Is.EqualTo(2));
        }

        [Test]
        public void ExerciseIsUnavailableOutsideFileManager()
        {
            var flow = new LocalPrototypeFlow(new MemoryProgressStore());
            flow.BeginBoot();
            flow.CompleteBoot();
            flow.OpenApp(LocalPrototypeApp.Academy);

            Assert.That(flow.SubmitExerciseFile(LocalPrototypeFlow.RequiredSyntheticFileId), Is.EqualTo(ExerciseSubmissionResult.Unavailable));
            Assert.That(flow.AttemptCount, Is.Zero);
        }

        [Test]
        public void RestartClearsOnlyPrototypeExerciseProgress()
        {
            var store = new MemoryProgressStore();
            var flow = ReadyFileManager(store);
            flow.SubmitExerciseFile(LocalPrototypeFlow.RequiredSyntheticFileId);

            Assert.That(flow.RestartExercise(), Is.True);
            Assert.That(flow.ActiveApp, Is.EqualTo(LocalPrototypeApp.FileManager));
            Assert.That(flow.ExerciseComplete, Is.False);
            Assert.That(flow.AttemptCount, Is.Zero);
            Assert.That(new LocalPrototypeFlow(store).AttemptCount, Is.Zero);
        }

        [Test]
        public void InvalidSyntheticFileIdsDoNotChangeOrPersistProgress()
        {
            var store = new MemoryProgressStore();
            var flow = ReadyFileManager(store);

            Assert.That(flow.SubmitExerciseFile(null), Is.EqualTo(ExerciseSubmissionResult.InvalidSelection));
            Assert.That(flow.SubmitExerciseFile(string.Empty), Is.EqualTo(ExerciseSubmissionResult.InvalidSelection));
            Assert.That(flow.SubmitExerciseFile("unknown_file"), Is.EqualTo(ExerciseSubmissionResult.InvalidSelection));

            Assert.That(flow.AttemptCount, Is.Zero);
            Assert.That(flow.ExerciseComplete, Is.False);
            Assert.That(flow.LastExerciseFileId, Is.Null);
            Assert.That(store.SaveCount, Is.Zero);
        }

        [Test]
        public void FailedSubmissionSavePreservesConfirmedStateAndCanRetry()
        {
            var store = new MemoryProgressStore
            {
                Value = new LocalPrototypeProgress { attemptCount = 2 },
                FailNextSave = true
            };
            var flow = ReadyFileManager(store);

            Assert.That(flow.SubmitExerciseFile(LocalPrototypeFlow.RequiredSyntheticFileId),
                Is.EqualTo(ExerciseSubmissionResult.PersistenceFailed));
            Assert.That(flow.ExerciseComplete, Is.False);
            Assert.That(flow.AttemptCount, Is.EqualTo(2));
            Assert.That(flow.LastExerciseFileId, Is.Null);
            Assert.That(flow.LastPersistenceFailed, Is.True);
            Assert.That(store.Value.attemptCount, Is.EqualTo(2));

            Assert.That(flow.SubmitExerciseFile(LocalPrototypeFlow.RequiredSyntheticFileId),
                Is.EqualTo(ExerciseSubmissionResult.Correct));
            Assert.That(flow.ExerciseComplete, Is.True);
            Assert.That(flow.AttemptCount, Is.EqualTo(3));
            Assert.That(flow.LastPersistenceFailed, Is.False);
        }

        [Test]
        public void FailedRestartPreservesCompletedProgressAndCanRetry()
        {
            var store = new MemoryProgressStore
            {
                Value = new LocalPrototypeProgress { exerciseComplete = true, attemptCount = 3 }
            };
            var flow = ReadyFileManager(store);
            store.FailNextSave = true;

            Assert.That(flow.RestartExercise(), Is.False);
            Assert.That(flow.ExerciseComplete, Is.True);
            Assert.That(flow.AttemptCount, Is.EqualTo(3));
            Assert.That(flow.ActiveApp, Is.EqualTo(LocalPrototypeApp.FileManager));
            Assert.That(store.Value.exerciseComplete, Is.True);
            Assert.That(store.Value.attemptCount, Is.EqualTo(3));
            Assert.That(flow.LastPersistenceFailed, Is.True);

            Assert.That(flow.RestartExercise(), Is.True);
            Assert.That(flow.ExerciseComplete, Is.False);
            Assert.That(flow.AttemptCount, Is.Zero);
            Assert.That(flow.LastPersistenceFailed, Is.False);
        }

        [Test]
        public void AttemptCounterDoesNotOverflow()
        {
            var store = new MemoryProgressStore
            {
                Value = new LocalPrototypeProgress { attemptCount = int.MaxValue }
            };
            var flow = ReadyFileManager(store);
            var saveCount = store.SaveCount;

            Assert.That(flow.SubmitExerciseFile("briefing"), Is.EqualTo(ExerciseSubmissionResult.AttemptLimitReached));
            Assert.That(flow.AttemptCount, Is.EqualTo(int.MaxValue));
            Assert.That(flow.ExerciseComplete, Is.False);
            Assert.That(store.SaveCount, Is.EqualTo(saveCount));
        }

        private static LocalPrototypeFlow ReadyFileManager(ILocalPrototypeProgressStore store)
        {
            var flow = new LocalPrototypeFlow(store);
            flow.BeginBoot();
            flow.CompleteBoot();
            flow.OpenApp(LocalPrototypeApp.FileManager);
            return flow;
        }
    }
}
