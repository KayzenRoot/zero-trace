using System;
using System.IO;
using NUnit.Framework;
using UnityEngine;
using ZeroTrace.Domain;
using ZeroTrace.Presentation;

namespace ZeroTrace.Presentation.Tests.EditMode
{
    public sealed class LocalPrototypeProgressStoreTests
    {
        private string testRoot;
        private string progressPath;

        [SetUp]
        public void CreateIsolatedProgressDirectory()
        {
            testRoot = Path.Combine(Application.temporaryCachePath, "ZT-V01-WO-001-" + Guid.NewGuid().ToString("N"));
            progressPath = Path.Combine(testRoot, "local-prototype-progress.json");
        }

        [TearDown]
        public void RemoveOnlyIsolatedTestData()
        {
            if (!string.IsNullOrEmpty(testRoot) && Directory.Exists(testRoot))
            {
                Directory.Delete(testRoot, true);
            }
        }

        [Test]
        public void FirstSaveAndReplacementKeepARecoverablePreviousSave()
        {
            var store = new LocalPrototypeProgressStore(progressPath);
            store.Save(new LocalPrototypeProgress { attemptCount = 1 });
            store.Save(new LocalPrototypeProgress { exerciseComplete = true, attemptCount = 2 });

            var current = store.Load();
            Assert.That(current.exerciseComplete, Is.True);
            Assert.That(current.attemptCount, Is.EqualTo(2));
            Assert.That(File.Exists(progressPath + ".bak"), Is.True);

            var previous = new LocalPrototypeProgressStore(progressPath + ".bak").Load();
            Assert.That(previous.exerciseComplete, Is.False);
            Assert.That(previous.attemptCount, Is.EqualTo(1));
        }

        [Test]
        public void ReadsExistingLegacySaveWithEitherFieldOrder()
        {
            Directory.CreateDirectory(testRoot);
            File.WriteAllText(progressPath, "{\"attemptCount\":3,\"exerciseComplete\":true}");

            var loaded = new LocalPrototypeProgressStore(progressPath).Load();

            Assert.That(loaded.exerciseComplete, Is.True);
            Assert.That(loaded.attemptCount, Is.EqualTo(3));
        }

        [Test]
        public void CorruptPrimaryRecoversPreviousValidBackup()
        {
            Directory.CreateDirectory(testRoot);
            File.WriteAllText(progressPath, "{\"exerciseComplete\":true,");
            File.WriteAllText(progressPath + ".bak", "{\"exerciseComplete\":false,\"attemptCount\":4}");

            var loaded = new LocalPrototypeProgressStore(progressPath).Load();

            Assert.That(loaded.exerciseComplete, Is.False);
            Assert.That(loaded.attemptCount, Is.EqualTo(4));
            Assert.That(File.ReadAllText(progressPath), Is.EqualTo("{\"exerciseComplete\":true,"));
        }

        [Test]
        public void InterruptedValidTemporaryWriteCanBeRecoveredWhenNoCommitExists()
        {
            Directory.CreateDirectory(testRoot);
            File.WriteAllText(progressPath + ".tmp.interrupted", "{\"exerciseComplete\":false,\"attemptCount\":5}");

            var loaded = new LocalPrototypeProgressStore(progressPath).Load();

            Assert.That(loaded.exerciseComplete, Is.False);
            Assert.That(loaded.attemptCount, Is.EqualTo(5));
            Assert.That(File.Exists(progressPath + ".tmp.interrupted"), Is.True);
        }

        [Test]
        public void InvalidOrTruncatedPayloadsReturnSafeDefaultWithoutDeletingEvidence()
        {
            Directory.CreateDirectory(testRoot);
            var invalidPayloads = new[]
            {
                "{\"exerciseComplete\":true,",
                "{\"exerciseComplete\":false}",
                "{\"exerciseComplete\":false,\"attemptCount\":-1}",
                "{\"exerciseComplete\":true,\"attemptCount\":0}",
                "{\"exerciseComplete\":false,\"attemptCount\":2147483648}",
                "{\"exerciseComplete\":false,\"attemptCount\":1,\"unexpected\":true}"
            };

            foreach (var payload in invalidPayloads)
            {
                File.WriteAllText(progressPath, payload);
                var loaded = new LocalPrototypeProgressStore(progressPath).Load();
                Assert.That(loaded.exerciseComplete, Is.False, payload);
                Assert.That(loaded.attemptCount, Is.Zero, payload);
                Assert.That(File.ReadAllText(progressPath), Is.EqualTo(payload), payload);
            }
        }

        [Test]
        public void FailedAtomicReplacementLeavesPreviousCommittedSaveReadable()
        {
            var store = new LocalPrototypeProgressStore(progressPath);
            store.Save(new LocalPrototypeProgress { attemptCount = 2 });
            Directory.CreateDirectory(progressPath + ".bak");

            Assert.Throws<LocalPrototypePersistenceException>(() =>
                store.Save(new LocalPrototypeProgress { exerciseComplete = true, attemptCount = 3 }));

            var loaded = store.Load();
            Assert.That(loaded.exerciseComplete, Is.False);
            Assert.That(loaded.attemptCount, Is.EqualTo(2));
        }

        [Test]
        public void RejectsInvalidCandidateWithoutChangingExistingSave()
        {
            var store = new LocalPrototypeProgressStore(progressPath);
            store.Save(new LocalPrototypeProgress { attemptCount = 2 });

            Assert.Throws<LocalPrototypePersistenceException>(() =>
                store.Save(new LocalPrototypeProgress { exerciseComplete = true, attemptCount = -1 }));

            Assert.That(store.Load().attemptCount, Is.EqualTo(2));
        }
    }
}
