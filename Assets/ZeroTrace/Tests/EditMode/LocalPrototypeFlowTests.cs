using NUnit.Framework;
using ZeroTrace.Domain;

namespace ZeroTrace.Domain.Tests
{
    public sealed class LocalPrototypeFlowTests
    {
        private sealed class MemoryProgressStore : ILocalPrototypeProgressStore
        {
            public LocalPrototypeProgress Value = new LocalPrototypeProgress();
            public LocalPrototypeProgress Load() => Value.Copy();
            public void Save(LocalPrototypeProgress progress) => Value = progress.Copy();
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
