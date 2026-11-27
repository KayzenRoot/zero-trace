using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ZeroTrace.Domain.Tests
{
    public sealed class LocalPrototypeTerminalTests
    {
        private sealed class MemoryProgressStore : ILocalPrototypeProgressStore
        {
            public LocalPrototypeProgress Value = new LocalPrototypeProgress { attemptCount = 2 };
            public int SaveCount;

            public LocalPrototypeProgress Load() => Value.Copy();

            public void Save(LocalPrototypeProgress progress)
            {
                SaveCount++;
                Value = progress.Copy();
            }
        }

        private static TerminalStateSnapshot ReadyTerminal =>
            new TerminalStateSnapshot(LocalComputerState.Ready, LocalPrototypeApp.Terminal, false, 2);

        [Test]
        public void CatalogHasOnlyExistingFilesAndIsImmutable()
        {
            Assert.That(SyntheticFileCatalog.All.Count, Is.EqualTo(3));
            Assert.That(SyntheticFileCatalog.All[0].Id, Is.EqualTo("briefing"));
            Assert.That(SyntheticFileCatalog.All[1].Id, Is.EqualTo("recovery_manifest"));
            Assert.That(SyntheticFileCatalog.All[2].Id, Is.EqualTo("shift_note"));
            Assert.That(SyntheticFileCatalog.All[0].FileName, Is.EqualTo("briefing.txt"));
            Assert.That(SyntheticFileCatalog.All[1].FileName, Is.EqualTo("recovery_manifest.txt"));
            Assert.That(SyntheticFileCatalog.All[2].FileName, Is.EqualTo("shift_note.txt"));
            Assert.That(SyntheticFileCatalog.TryGetById("recovery_manifest", out var byId), Is.True);
            Assert.That(SyntheticFileCatalog.TryGetByIdOrFileName("recovery_manifest.txt", out var byName), Is.True);
            Assert.That(byName, Is.SameAs(byId));
            Assert.That(SyntheticFileCatalog.TryGetByIdOrFileName("Recovery_Manifest.txt", out _), Is.False);

            var list = SyntheticFileCatalog.All as IList<SyntheticFile>;
            Assert.That(list, Is.Not.Null);
            Assert.Throws<NotSupportedException>(() => list[0] = list[1]);
        }

        [Test]
        public void TerminalEnumIsAppendedAndExistingExerciseUsesTheSharedCatalog()
        {
            Assert.That((int)LocalPrototypeApp.None, Is.EqualTo(0));
            Assert.That((int)LocalPrototypeApp.Mail, Is.EqualTo(1));
            Assert.That((int)LocalPrototypeApp.FileManager, Is.EqualTo(2));
            Assert.That((int)LocalPrototypeApp.Academy, Is.EqualTo(3));
            Assert.That((int)LocalPrototypeApp.Terminal, Is.EqualTo(4));
            Assert.That(LocalPrototypeFlow.RequiredSyntheticFileId,
                Is.EqualTo(SyntheticFileCatalog.RecoveryManifestId));

            var store = new MemoryProgressStore();
            var flow = new LocalPrototypeFlow(store);
            flow.BeginBoot();
            flow.CompleteBoot();
            Assert.That(flow.OpenApp(LocalPrototypeApp.Terminal), Is.True);
            Assert.That(flow.SubmitExerciseFile("recovery_manifest.txt"), Is.EqualTo(ExerciseSubmissionResult.Unavailable));
            Assert.That(store.SaveCount, Is.Zero);
        }

        [TestCase("help", TerminalResultCode.Help)]
        [TestCase("clear", TerminalResultCode.Clear)]
        [TestCase("world.status", TerminalResultCode.WorldStatus)]
        [TestCase("files.list", TerminalResultCode.FilesList)]
        [TestCase("files.read recovery_manifest", TerminalResultCode.FileRead)]
        [TestCase("files.read recovery_manifest.txt", TerminalResultCode.FileRead)]
        [TestCase("case.list", TerminalResultCode.CaseList)]
        [TestCase("academy.status", TerminalResultCode.AcademyStatus)]
        public void DispatcherSupportsOnlyTheSevenAllowlistedCommands(string input, TerminalResultCode expected)
        {
            var result = new TerminalCommandDispatcher().Execute(input, ReadyTerminal);

            Assert.That(result.Code, Is.EqualTo(expected));
            Assert.That(result.IsError, Is.False);
        }

        [TestCase("help extra")]
        [TestCase("clear extra")]
        [TestCase("world.status extra")]
        [TestCase("files.list extra")]
        [TestCase("files.read")]
        [TestCase("files.read briefing.txt extra")]
        [TestCase("case.list extra")]
        [TestCase("academy.status extra")]
        public void KnownCommandsRejectWrongArity(string input)
        {
            Assert.That(new TerminalCommandDispatcher().Execute(input, ReadyTerminal).Code,
                Is.EqualTo(TerminalResultCode.WrongArity));
        }

        [TestCase("unknown.command", TerminalResultCode.UnknownCommand)]
        [TestCase("files.read missing.txt", TerminalResultCode.SyntheticFileNotFound)]
        [TestCase("files.read ../briefing.txt", TerminalResultCode.InvalidCharacters)]
        [TestCase("files.read C:\\private.txt", TerminalResultCode.InvalidCharacters)]
        [TestCase("files.read briefing.txt;shutdown", TerminalResultCode.InvalidCharacters)]
        [TestCase("world.status\t", TerminalResultCode.InvalidCharacters)]
        [TestCase("world.status\n", TerminalResultCode.InvalidCharacters)]
        [TestCase("files.read $HOME", TerminalResultCode.InvalidCharacters)]
        [TestCase("HELP", TerminalResultCode.UnknownCommand)]
        public void UnknownPathsControlsAndScriptLikeInputAreRejected(string input, TerminalResultCode expected)
        {
            Assert.That(new TerminalCommandDispatcher().Execute(input, ReadyTerminal).Code, Is.EqualTo(expected));
        }

        [Test]
        public void EmptyAndOversizedInputAreRejectedWithinBoundedParsing()
        {
            var dispatcher = new TerminalCommandDispatcher();
            Assert.That(dispatcher.Execute(null, ReadyTerminal).Code, Is.EqualTo(TerminalResultCode.EmptyInput));
            Assert.That(dispatcher.Execute("   ", ReadyTerminal).Code, Is.EqualTo(TerminalResultCode.EmptyInput));
            Assert.That(dispatcher.Execute("\u0001", ReadyTerminal).Code, Is.EqualTo(TerminalResultCode.InvalidCharacters));
            Assert.That(dispatcher.Execute(new string('a', 257), ReadyTerminal).Code,
                Is.EqualTo(TerminalResultCode.InputTooLong));
            Assert.That(dispatcher.Execute("help" + new string(' ', 252), ReadyTerminal).Code,
                Is.EqualTo(TerminalResultCode.Help));
        }

        [Test]
        public void ArgumentCountIsCappedBeforeCommandSpecificArity()
        {
            var dispatcher = new TerminalCommandDispatcher();
            Assert.That(dispatcher.Execute("help a b c d", ReadyTerminal).Code,
                Is.EqualTo(TerminalResultCode.WrongArity));
            Assert.That(dispatcher.Execute("help a b c d e", ReadyTerminal).Code,
                Is.EqualTo(TerminalResultCode.TooManyArguments));
        }

        [Test]
        public void TerminalRequiresReadyStateAndActiveTerminalApp()
        {
            var dispatcher = new TerminalCommandDispatcher();
            Assert.That(dispatcher.Execute("help",
                new TerminalStateSnapshot(LocalComputerState.Off, LocalPrototypeApp.Terminal, false, 0)).Code,
                Is.EqualTo(TerminalResultCode.NotReady));
            Assert.That(dispatcher.Execute("help",
                new TerminalStateSnapshot(LocalComputerState.Ready, LocalPrototypeApp.FileManager, false, 0)).Code,
                Is.EqualTo(TerminalResultCode.TerminalInactive));
        }

        [Test]
        public void IdenticalSnapshotAndCommandProduceIdenticalTypedResults()
        {
            var dispatcher = new TerminalCommandDispatcher();
            var first = dispatcher.Execute("academy.status", ReadyTerminal);
            var second = dispatcher.Execute("academy.status", ReadyTerminal);

            Assert.That(second.Code, Is.EqualTo(first.Code));
            Assert.That(second.Snapshot.ComputerState, Is.EqualTo(first.Snapshot.ComputerState));
            Assert.That(second.Snapshot.ActiveApp, Is.EqualTo(first.Snapshot.ActiveApp));
            Assert.That(second.Snapshot.ExerciseComplete, Is.EqualTo(first.Snapshot.ExerciseComplete));
            Assert.That(second.Snapshot.AttemptCount, Is.EqualTo(first.Snapshot.AttemptCount));
        }

        [Test]
        public void ReadOnlyCommandsDoNotMutateProgressOrInvokePersistence()
        {
            var store = new MemoryProgressStore();
            var flow = new LocalPrototypeFlow(store);
            flow.BeginBoot();
            flow.CompleteBoot();
            flow.OpenApp(LocalPrototypeApp.Terminal);
            var snapshot = new TerminalStateSnapshot(flow.ComputerState, flow.ActiveApp,
                flow.ExerciseComplete, flow.AttemptCount);
            var dispatcher = new TerminalCommandDispatcher();

            foreach (var command in new[]
            {
                "help", "world.status", "files.list", "files.read briefing",
                "case.list", "academy.status"
            })
            {
                Assert.That(dispatcher.Execute(command, snapshot).IsError, Is.False, command);
            }

            Assert.That(store.SaveCount, Is.Zero);
            Assert.That(flow.ReadProgress().exerciseComplete, Is.False);
            Assert.That(flow.ReadProgress().attemptCount, Is.EqualTo(2));
            Assert.That(flow.LastExerciseFileId, Is.Null);
        }

        [Test]
        public void SessionCapsHistoryAndClearRemovesOnlyVisibleOutput()
        {
            var session = new LocalPrototypeTerminalSession();
            var result = new TerminalCommandDispatcher().Execute("help", ReadyTerminal);
            for (var index = 0; index < 52; index++)
            {
                session.RecordHistory("command-" + index.ToString("D2"));
            }

            Assert.That(session.History.Count, Is.EqualTo(50));
            Assert.That(session.History[0], Is.EqualTo("command-02"));
            Assert.That(session.History[49], Is.EqualTo("command-51"));
            Assert.That(session.AppendOutput(new TerminalTranscriptEntry("help", true, result), 18), Is.True);
            session.ClearOutput();
            Assert.That(session.Output.Count, Is.Zero);
            Assert.That(session.VisibleOutputCharacters, Is.Zero);
            Assert.That(session.History.Count, Is.EqualTo(50));
        }

        [Test]
        public void SessionEnforcesPerCommandAndTotalVisibleOutputQuotas()
        {
            var session = new LocalPrototypeTerminalSession();
            var result = new TerminalCommandDispatcher().Execute("help", ReadyTerminal);
            var tooLong = new TerminalTranscriptEntry(string.Empty, false, result);
            Assert.That(session.AppendOutput(tooLong,
                LocalPrototypeTerminalSession.MaximumCommandOutputCharacters + 1), Is.False);
            Assert.That(session.Output.Count, Is.Zero);

            for (var index = 0; index < 9; index++)
            {
                Assert.That(session.AppendOutput(new TerminalTranscriptEntry("help", true, result),
                    LocalPrototypeTerminalSession.MaximumCommandOutputCharacters), Is.True);
            }

            Assert.That(session.Output.Count, Is.EqualTo(8));
            Assert.That(session.VisibleOutputCharacters,
                Is.EqualTo(LocalPrototypeTerminalSession.MaximumVisibleOutputCharacters));
        }

        [Test]
        public void LocaleRebalanceTrimsOldestOutputAndResetClearsTheSession()
        {
            var session = new LocalPrototypeTerminalSession();
            var result = new TerminalCommandDispatcher().Execute("help", ReadyTerminal);
            for (var index = 0; index < 9; index++)
            {
                session.AppendOutput(new TerminalTranscriptEntry("help", true, result), 10);
            }

            Assert.That(session.RecalculateOutputLengths(new int[] { 4096, 4096, 4096, 4096, 4096, 4096, 4096, 4096, 4096 }),
                Is.True);
            Assert.That(session.Output.Count, Is.EqualTo(8));
            Assert.That(session.VisibleOutputCharacters, Is.EqualTo(LocalPrototypeTerminalSession.MaximumVisibleOutputCharacters));

            session.Reset();
            Assert.That(session.History.Count, Is.Zero);
            Assert.That(session.Output.Count, Is.Zero);
            Assert.That(session.VisibleOutputCharacters, Is.Zero);
        }

        [Test]
        public void InvalidLocaleRebalancePreservesExistingOutputAccounting()
        {
            var session = new LocalPrototypeTerminalSession();
            var result = new TerminalCommandDispatcher().Execute("help", ReadyTerminal);
            Assert.That(session.AppendOutput(new TerminalTranscriptEntry("help", true, result), 18), Is.True);

            Assert.That(session.RecalculateOutputLengths(new[]
            {
                LocalPrototypeTerminalSession.MaximumCommandOutputCharacters + 1
            }), Is.False);
            Assert.That(session.Output.Count, Is.EqualTo(1));
            Assert.That(session.VisibleOutputCharacters, Is.EqualTo(18));
        }
    }
}
