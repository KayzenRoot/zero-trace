using System;
using System.Collections;
using System.IO;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UIElements;
using ZeroTrace.Domain;
using ZeroTrace.Presentation;

namespace ZeroTrace.Presentation.Tests.PlayMode
{
    public sealed class LocalFirstPlayablePlayModeTests
    {
        private sealed class MemoryProgressStore : ILocalPrototypeProgressStore
        {
            private LocalPrototypeProgress value = new LocalPrototypeProgress();
            public LocalPrototypeProgress Load() => value.Copy();
            public void Save(LocalPrototypeProgress progress) => value = progress.Copy();
        }

        private StartupController startup;
        private LocalFirstPlayableController controller;
        private Locale originalLocale;
        private Scene previousActiveScene;
        private bool loadedPlayableSceneHere;

        [UnitySetUp]
        public IEnumerator WaitForPrototypeSceneAndStartup()
        {
            originalLocale = LocalizationSettings.SelectedLocale;
            previousActiveScene = SceneManager.GetActiveScene();
            var playableScene = SceneManager.GetSceneByPath("Assets/Scenes/SampleScene.unity");
            if (!playableScene.IsValid() || !playableScene.isLoaded)
            {
                var load = SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Additive);
                Assert.That(load, Is.Not.Null, "The registered startup scene could not be loaded for PlayMode tests.");
                yield return load;
                loadedPlayableSceneHere = true;
                playableScene = SceneManager.GetSceneByPath("Assets/Scenes/SampleScene.unity");
            }

            Assert.That(playableScene.IsValid() && playableScene.isLoaded, Is.True);
            SceneManager.SetActiveScene(playableScene);
            var deadline = Time.realtimeSinceStartup + 8f;
            while (Time.realtimeSinceStartup < deadline)
            {
                startup = UnityEngine.Object.FindFirstObjectByType<StartupController>();
                controller = UnityEngine.Object.FindFirstObjectByType<LocalFirstPlayableController>();
                if (startup != null && startup.IsReady && controller != null && startup.Document != null &&
                    startup.Document.rootVisualElement.Q<Button>("prototype-launch") != null)
                {
                    yield break;
                }

                yield return null;
            }

            Assert.Fail("The registered startup scene or its local prototype entry point was not ready.");
        }

        [UnityTearDown]
        public IEnumerator RestorePrototypeState()
        {
            if (controller != null)
            {
                controller.ExitPrototype();
            }

            var english = Resources.Load<Locale>("Localization/Locales/Locale-en-US");
            var restore = originalLocale != null &&
                         (originalLocale.Identifier.Code == "en-US" || originalLocale.Identifier.Code == "pt-BR")
                ? originalLocale
                : english;
            if (restore != null)
            {
                LocalizationSettings.SelectedLocale = restore;
            }

            if (previousActiveScene.IsValid() && previousActiveScene.isLoaded)
            {
                SceneManager.SetActiveScene(previousActiveScene);
            }

            if (loadedPlayableSceneHere)
            {
                var playableScene = SceneManager.GetSceneByPath("Assets/Scenes/SampleScene.unity");
                if (playableScene.IsValid() && playableScene.isLoaded)
                {
                    yield return SceneManager.UnloadSceneAsync(playableScene);
                }

                loadedPlayableSceneHere = false;
            }

            yield return null;
        }

        [UnityTest]
        public IEnumerator LocalEntryMovesInFirstPersonAndComputerCanBootShutdownAndRestart()
        {
            controller.SetProgressStoreForSession(new MemoryProgressStore());
            var launch = startup.Document.rootVisualElement.Q<Button>("prototype-launch");
            Assert.That(launch.text, Is.EqualTo("Start local prototype"));
            Assert.That(startup.Document.rootVisualElement.Q<Label>("prototype-notice").text,
                Does.Contain("NOT RELEASE CONTENT"));

            Assert.That(controller.StartPrototype(), Is.True);
            Assert.That(controller.IsPrototypeRunning, Is.True);
            Assert.That(controller.HasFirstPersonPlayer, Is.True);
            Assert.That(controller.PlayerControlsEnabled, Is.True);
            Assert.That(controller.IsLookingAtComputer, Is.True);
            Assert.That(controller.InteractWithComputer(), Is.True);
            yield return WaitForComputerReady();
            Assert.That(controller.ComputerState, Is.EqualTo(LocalComputerState.Ready));
            Assert.That(controller.IsDesktopVisible, Is.True);
            Assert.That(controller.PlayerControlsEnabled, Is.False);

            Assert.That(controller.ShutdownComputer(), Is.True);
            Assert.That(controller.ComputerState, Is.EqualTo(LocalComputerState.Shutdown));
            Assert.That(controller.PlayerControlsEnabled, Is.True);
            Assert.That(controller.BeginComputerBootForInteraction(), Is.True);
            yield return WaitForComputerReady();
            Assert.That(controller.ComputerState, Is.EqualTo(LocalComputerState.Ready));
        }

        [UnityTest]
        public IEnumerator SyntheticAppsExerciseAndLocalProgressSaveLoadWork()
        {
            var progressPath = Path.Combine(Application.temporaryCachePath,
                "ZT-V01-WO-001-" + Guid.NewGuid().ToString("N") + ".json");
            var store = new LocalPrototypeProgressStore(progressPath);
            controller.SetProgressStoreForSession(store);

            Assert.That(controller.StartPrototype(), Is.True);
            Assert.That(controller.BeginComputerBootForInteraction(), Is.True);
            yield return WaitForComputerReady();
            Assert.That(controller.OpenApp("mail"), Is.True);
            Assert.That(controller.ActiveApp, Is.EqualTo(LocalPrototypeApp.Mail));
            Assert.That(controller.CloseActiveApp(), Is.True);
            Assert.That(controller.OpenApp("academy"), Is.True);
            Assert.That(controller.OpenApp("files"), Is.True);
            Assert.That(startup.Document.rootVisualElement.Q<VisualElement>("null-os-app-content")
                .Q<Label>("prototype-exercise-prompt").text, Does.Contain("select the recovery index"));

            controller.SelectSyntheticFile("shift_note");
            Assert.That(controller.SubmitExerciseSelection(), Is.EqualTo(ExerciseSubmissionResult.Incorrect));
            Assert.That(controller.LastExerciseResultText, Does.Contain("Not quite"));
            Assert.That(controller.ExerciseComplete, Is.False);

            controller.SelectSyntheticFile(LocalPrototypeFlow.RequiredSyntheticFileId);
            Assert.That(controller.SubmitExerciseSelection(), Is.EqualTo(ExerciseSubmissionResult.Correct));
            Assert.That(controller.LastExerciseResultText, Does.Contain("Correct"));
            Assert.That(controller.ExerciseComplete, Is.True);
            Assert.That(controller.ExerciseAttemptCount, Is.EqualTo(2));
            Assert.That(File.Exists(progressPath), Is.True);
            var loaded = store.Load();
            Assert.That(loaded.exerciseComplete, Is.True);
            Assert.That(loaded.attemptCount, Is.EqualTo(2));

            Assert.That(controller.RestartExercise(), Is.True);
            Assert.That(controller.ExerciseComplete, Is.False);
            Assert.That(controller.ExerciseAttemptCount, Is.Zero);
            Assert.That(new LocalPrototypeProgressStore(progressPath).Load().attemptCount, Is.Zero);
        }

        [UnityTest]
        public IEnumerator RuntimeCopyTracksEnglishAndBrazilianPortuguese()
        {
            controller.SetProgressStoreForSession(new MemoryProgressStore());
            var portuguese = Resources.Load<Locale>("Localization/Locales/Locale-pt-BR");
            var english = Resources.Load<Locale>("Localization/Locales/Locale-en-US");
            Assert.That(portuguese, Is.Not.Null);
            Assert.That(english, Is.Not.Null);

            LocalizationSettings.SelectedLocale = portuguese;
            yield return WaitForLocale("pt-BR");
            var launch = startup.Document.rootVisualElement.Q<Button>("prototype-launch");
            Assert.That(launch.text, Is.EqualTo("Iniciar protótipo local"));
            Assert.That(controller.StartPrototype(), Is.True);
            Assert.That(controller.BeginComputerBootForInteraction(), Is.True);
            yield return WaitForComputerReady();
            Assert.That(controller.OpenApp("files"), Is.True);
            var content = startup.Document.rootVisualElement.Q<VisualElement>("null-os-app-content");
            Assert.That(content.Q<Label>("prototype-exercise-prompt").text, Does.Contain("selecione o índice de recuperação"));
            controller.SelectSyntheticFile("shift_note");
            Assert.That(controller.SubmitExerciseSelection(), Is.EqualTo(ExerciseSubmissionResult.Incorrect));
            Assert.That(controller.LastExerciseResultText, Does.Contain("Ainda não"));

            LocalizationSettings.SelectedLocale = english;
            yield return WaitForLocale("en-US");
            content = startup.Document.rootVisualElement.Q<VisualElement>("null-os-app-content");
            Assert.That(content.Q<Label>("prototype-exercise-prompt").text, Does.Contain("select the recovery index"));
            Assert.That(LocalPrototypeText.Get("correct", "pt-BR"), Does.Contain("Correto"));
            Assert.That(LocalPrototypeText.Get("correct", "en-US"), Does.Contain("Correct"));
        }

        private IEnumerator WaitForComputerReady()
        {
            var deadline = Time.realtimeSinceStartup + 5f;
            while (controller.ComputerState != LocalComputerState.Ready && Time.realtimeSinceStartup < deadline)
            {
                yield return null;
            }
        }

        private IEnumerator WaitForLocale(string code)
        {
            var deadline = Time.realtimeSinceStartup + 3f;
            while ((startup.CurrentLocaleCode != code ||
                    (controller.IsPrototypeRunning && controller.CurrentLocaleCode != code)) &&
                   Time.realtimeSinceStartup < deadline)
            {
                yield return null;
            }

            Assert.That(startup.CurrentLocaleCode, Is.EqualTo(code));
        }
    }
}
