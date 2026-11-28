using System;
using System.Collections;
using System.IO;
using System.Reflection;
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
            public bool FailNextSave;
            public int SaveCount;
            public LocalPrototypeProgress Load() => value.Copy();
            public void Save(LocalPrototypeProgress progress)
            {
                SaveCount++;
                if (FailNextSave)
                {
                    FailNextSave = false;
                    throw new LocalPrototypePersistenceException("Synthetic save failure.");
                }

                value = progress.Copy();
            }
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

        [UnityTest]
        public IEnumerator RoomWallsBlockEscapeAndRecoveryReturnsSafelyBeforeInteraction()
        {
            controller.SetProgressStoreForSession(new MemoryProgressStore());
            Assert.That(controller.StartPrototype(), Is.True);

            var roomRoot = GameObject.Find("ZERO TRACE Local Prototype Room");
            Assert.That(roomRoot, Is.Not.Null);
            var frontWall = roomRoot.transform.Find("Front Wall (Negative Z)");
            Assert.That(frontWall, Is.Not.Null);
            Assert.That(frontWall.GetComponent<BoxCollider>(), Is.Not.Null);

            var motor = UnityEngine.Object.FindFirstObjectByType<LocalPrototypePlayerMotor>();
            var characterController = motor.GetComponent<CharacterController>();
            motor.SetControlsEnabled(false);

            Assert.That(MoveAndReadPosition(motor.transform, characterController,
                new Vector3(2.2f, 0f, -2.4f), Vector3.forward * 0.25f).z, Is.GreaterThan(-2.4f));
            Assert.That(MoveAndReadPosition(motor.transform, characterController,
                new Vector3(-2.55f, 0f, -3.2f), Vector3.back * 2f).z, Is.GreaterThan(-3.8f),
                "The decorative entrance must remain backed by the solid front wall.");
            Assert.That(MoveAndReadPosition(motor.transform, characterController,
                new Vector3(3.2f, 0f, -2.4f), Vector3.right * 2f).x, Is.LessThan(3.8f));
            Assert.That(MoveAndReadPosition(motor.transform, characterController,
                new Vector3(3.2f, 0f, 2f), Vector3.right * 2f).x, Is.LessThan(3.8f),
                "The decorative window must remain backed by the solid right wall.");
            Assert.That(MoveAndReadPosition(motor.transform, characterController,
                new Vector3(-3.2f, 0f, -2.4f), Vector3.left * 2f).x, Is.GreaterThan(-3.8f));
            Assert.That(MoveAndReadPosition(motor.transform, characterController,
                new Vector3(3.2f, 0f, 3.2f), Vector3.forward * 2f).z, Is.LessThan(3.8f));
            Assert.That(MoveAndReadPosition(motor.transform, characterController,
                new Vector3(3.2f, 0f, -3.2f), Vector3.back * 2f).z, Is.GreaterThan(-3.8f));

            motor.SetControlsEnabled(true);
            SetPlayerPosition(motor.transform, characterController, new Vector3(0f, -20f, -2.4f));
            typeof(LocalPrototypePlayerMotor).GetField("verticalSpeed", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(motor, -42f);

            Assert.That(motor.RecoverIfOutsideRoom(), Is.True);
            Assert.That(motor.transform.position, Is.EqualTo(new Vector3(0f, 0f, -2.4f)));
            Assert.That(typeof(LocalPrototypePlayerMotor).GetField("verticalSpeed", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(motor), Is.EqualTo(0f));
            Assert.That(motor.RecoveredThisFrame, Is.True);
            Assert.That(controller.InteractWithComputer(), Is.False);
            Assert.That(controller.ComputerState, Is.EqualTo(LocalComputerState.Off));

            yield return null;
            Assert.That(controller.IsLookingAtComputer, Is.True);
            Assert.That(controller.InteractWithComputer(), Is.True);
            yield return WaitForComputerReady();
            Assert.That(controller.ComputerState, Is.EqualTo(LocalComputerState.Ready));
        }

        [UnityTest]
        public IEnumerator StarterApartmentCompositionKeepsSingleMonitorAndComputerTarget()
        {
            controller.SetProgressStoreForSession(new MemoryProgressStore());
            Assert.That(controller.StartPrototype(), Is.True);
            yield return null;

            var roomRoot = GameObject.Find("ZERO TRACE Local Prototype Room");
            Assert.That(roomRoot, Is.Not.Null);
            AssertNamedRoomObject(roomRoot.transform, "Apartment Entrance Door Panel");
            AssertNamedRoomObject(roomRoot.transform, "Apartment Window Pane");
            AssertNamedRoomObject(roomRoot.transform, "Apartment Sofa Seat");
            AssertNamedRoomObject(roomRoot.transform, "Apartment Storage Cabinet");

            var door = roomRoot.transform.Find("Apartment Entrance Door Panel").gameObject;
            var pane = roomRoot.transform.Find("Apartment Window Pane").gameObject;
            Assert.That(door.GetComponent<Collider>().enabled, Is.False,
                "The entrance is set dressing; the wall behind it provides the boundary.");
            Assert.That(pane.GetComponent<Collider>().enabled, Is.False,
                "The window is set dressing; the wall behind it provides the boundary.");

            var monitorCount = 0;
            var targetCount = 0;
            foreach (var transform in roomRoot.GetComponentsInChildren<Transform>(true))
            {
                if (transform.name == "Recovery Computer Monitor")
                {
                    monitorCount++;
                    var collider = transform.GetComponent<Collider>();
                    if (collider != null && collider.enabled)
                    {
                        targetCount++;
                    }
                }

                Assert.That(transform == roomRoot.transform || transform.IsChildOf(roomRoot.transform), Is.True,
                    "Generated room objects must remain under the prototype root.");
            }

            Assert.That(monitorCount, Is.EqualTo(1));
            Assert.That(targetCount, Is.EqualTo(1));
            Assert.That(controller.IsLookingAtComputer, Is.True,
                "The apartment dressing must not intercept the approved spawn-to-computer ray.");
            Assert.That(controller.InteractWithComputer(), Is.True);
            yield return WaitForComputerReady();
            Assert.That(controller.ComputerState, Is.EqualTo(LocalComputerState.Ready));
        }

        [UnityTest]
        public IEnumerator StarterApartmentExitRestoresCameraAndRemovesGeneratedRoot()
        {
            controller.SetProgressStoreForSession(new MemoryProgressStore());
            var camera = Camera.main;
            Assert.That(camera, Is.Not.Null);
            var originalParent = camera.transform.parent;
            var originalPosition = camera.transform.localPosition;
            var originalRotation = camera.transform.localRotation;
            var originalScale = camera.transform.localScale;
            var originalEnabled = camera.enabled;

            Assert.That(controller.StartPrototype(), Is.True);
            Assert.That(camera.transform.parent, Is.Not.EqualTo(originalParent));
            Assert.That(GameObject.Find("ZERO TRACE Local Prototype Room"), Is.Not.Null);

            controller.ExitPrototype();
            yield return null;

            Assert.That(GameObject.Find("ZERO TRACE Local Prototype Room"), Is.Null);
            Assert.That(camera.transform.parent, Is.EqualTo(originalParent));
            Assert.That(camera.transform.localPosition, Is.EqualTo(originalPosition));
            Assert.That(camera.transform.localRotation, Is.EqualTo(originalRotation));
            Assert.That(camera.transform.localScale, Is.EqualTo(originalScale));
            Assert.That(camera.enabled, Is.EqualTo(originalEnabled));
        }

        private static void AssertNamedRoomObject(Transform root, string objectName)
        {
            var child = root.Find(objectName);
            Assert.That(child, Is.Not.Null, objectName + " should be present in the generated apartment.");
            Assert.That(child.IsChildOf(root), Is.True);
        }

        [UnityTest]
        public IEnumerator FileSelectionVisualsStaySynchronizedAcrossAppLifecycleAndLocale()
        {
            controller.SetProgressStoreForSession(new MemoryProgressStore());
            Assert.That(controller.StartPrototype(), Is.True);
            Assert.That(controller.BeginComputerBootForInteraction(), Is.True);
            yield return WaitForComputerReady();
            Assert.That(controller.OpenApp("files"), Is.True);

            var content = startup.Document.rootVisualElement.Q<VisualElement>("null-os-app-content");
            var briefing = content.Q<Button>("prototype-file-briefing");
            var recovery = content.Q<Button>("prototype-file-recovery_manifest");
            var shift = content.Q<Button>("prototype-file-shift_note");
            controller.SelectSyntheticFile("shift_note");
            Assert.That(shift.ClassListContains("prototype-file-selected"), Is.True);
            Assert.That(shift.text, Does.StartWith("✓  "));

            controller.SelectSyntheticFile("briefing");
            Assert.That(shift.ClassListContains("prototype-file-selected"), Is.False);
            Assert.That(shift.text, Does.Not.StartWith("✓  "));
            Assert.That(briefing.ClassListContains("prototype-file-selected"), Is.True);
            Assert.That(content.Q<Label>("prototype-selected-file").text, Does.Contain("briefing.txt"));

            Assert.That(controller.CloseActiveApp(), Is.True);
            Assert.That(controller.OpenApp("files"), Is.True);
            content = startup.Document.rootVisualElement.Q<VisualElement>("null-os-app-content");
            Assert.That(content.Q<Label>("prototype-selected-file").text, Is.EqualTo("No file selected."));
            Assert.That(content.Q<Button>("prototype-file-briefing").ClassListContains("prototype-file-selected"), Is.False);

            controller.SelectSyntheticFile(LocalPrototypeFlow.RequiredSyntheticFileId);
            recovery = content.Q<Button>("prototype-file-recovery_manifest");
            Assert.That(recovery.ClassListContains("prototype-file-selected"), Is.True);
            var portuguese = Resources.Load<Locale>("Localization/Locales/Locale-pt-BR");
            LocalizationSettings.SelectedLocale = portuguese;
            yield return WaitForLocale("pt-BR");

            content = startup.Document.rootVisualElement.Q<VisualElement>("null-os-app-content");
            recovery = content.Q<Button>("prototype-file-recovery_manifest");
            Assert.That(recovery.ClassListContains("prototype-file-selected"), Is.True);
            Assert.That(recovery.text, Does.StartWith("✓  "));
            Assert.That(content.Q<Label>("prototype-selected-file").text, Does.Contain("recovery_manifest.txt"));
            Assert.That(content.Q<Label>("prototype-selected-file").text, Does.StartWith("Selecionado:"));

            Assert.That(controller.RestartExercise(), Is.True);
            content = startup.Document.rootVisualElement.Q<VisualElement>("null-os-app-content");
            Assert.That(content.Q<Label>("prototype-selected-file").text, Is.EqualTo("Nenhum arquivo selecionado."));
            Assert.That(content.Q<Button>("prototype-file-recovery_manifest").ClassListContains("prototype-file-selected"), Is.False);
            Assert.That(controller.SubmitExerciseSelection(), Is.EqualTo(ExerciseSubmissionResult.InvalidSelection));
            Assert.That(controller.ExerciseAttemptCount, Is.Zero);
        }

        [UnityTest]
        public IEnumerator SaveFailureIsLocalizedAndRetryCommitsProgress()
        {
            var store = new MemoryProgressStore();
            controller.SetProgressStoreForSession(store);
            Assert.That(controller.StartPrototype(), Is.True);
            Assert.That(controller.BeginComputerBootForInteraction(), Is.True);
            yield return WaitForComputerReady();
            Assert.That(controller.OpenApp("files"), Is.True);
            controller.SelectSyntheticFile(LocalPrototypeFlow.RequiredSyntheticFileId);

            store.FailNextSave = true;
            Assert.That(controller.SubmitExerciseSelection(), Is.EqualTo(ExerciseSubmissionResult.PersistenceFailed));
            Assert.That(controller.LastExerciseResultText, Does.Contain("Progress was not saved"));
            Assert.That(controller.ExerciseComplete, Is.False);
            Assert.That(controller.ExerciseAttemptCount, Is.Zero);

            store.FailNextSave = true;
            var portuguese = Resources.Load<Locale>("Localization/Locales/Locale-pt-BR");
            LocalizationSettings.SelectedLocale = portuguese;
            yield return WaitForLocale("pt-BR");
            Assert.That(controller.SubmitExerciseSelection(), Is.EqualTo(ExerciseSubmissionResult.PersistenceFailed));
            Assert.That(controller.LastExerciseResultText, Does.Contain("O progresso não foi salvo"));
            Assert.That(controller.ExerciseAttemptCount, Is.Zero);

            Assert.That(controller.SubmitExerciseSelection(), Is.EqualTo(ExerciseSubmissionResult.Correct));
            Assert.That(controller.ExerciseComplete, Is.True);
            Assert.That(controller.ExerciseAttemptCount, Is.EqualTo(1));
        }

        [UnityTest]
        public IEnumerator TerminalUsesKeyboardHistoryFocusLocaleAndCloseLifecycle()
        {
            var store = new MemoryProgressStore();
            controller.SetProgressStoreForSession(store);
            Assert.That(controller.StartPrototype(), Is.True);
            Assert.That(controller.OpenApp("terminal"), Is.False,
                "Terminal must not open before the fictional computer reaches Ready.");
            Assert.That(controller.BeginComputerBootForInteraction(), Is.True);
            yield return WaitForComputerReady();
            Assert.That(controller.PlayerControlsEnabled, Is.False);
            Assert.That(controller.OpenApp("terminal"), Is.True);
            yield return null;

            var root = startup.Document.rootVisualElement;
            var input = root.Q<TextField>("terminal-input");
            var output = root.Q<ScrollView>("terminal-output");
            Assert.That(input, Is.Not.Null);
            Assert.That(output, Is.Not.Null);
            Assert.That(input.maxLength, Is.EqualTo(256));
            Assert.That(input.focusable, Is.True);
            Assert.That(root.Q<Button>("terminal-run").focusable, Is.True);
            Assert.That(root.Q<Button>("terminal-clear-output").focusable, Is.True);
            Assert.That(root.Q<Button>("prototype-app-close").focusable, Is.True);

            input.Focus();
            Assert.That(input.focusController.focusedElement, Is.SameAs(input));
            var keyDownObserved = false;
            var rootKeyDownObserved = false;
            input.RegisterCallback<KeyDownEvent>(_ => keyDownObserved = true);
            root.RegisterCallback<KeyDownEvent>(_ => rootKeyDownObserved = true);
            input.SetValueWithoutNotify("help");
            Assert.That(input.value, Is.EqualTo("help"));
            SendKey(input, KeyCode.Return);
            yield return null;
            Assert.That(keyDownObserved || rootKeyDownObserved, Is.True,
                "The synthesized key event must be delivered through the UI Toolkit panel.");
            Assert.That(output.contentContainer.Q<Label>("terminal-output-line-0").text,
                Does.Contain("files.read <file-id|filename.txt>"));
            Assert.That(output.contentContainer.Q<Label>("terminal-output-line-0").resolvedStyle.fontSize,
                Is.GreaterThanOrEqualTo(16f), "Terminal body text must remain readable at the specified minimum size.");

            input.SetValueWithoutNotify("files.list");
            SendKey(input, KeyCode.Return);
            yield return null;
            Assert.That(output.contentContainer.Q<Label>("terminal-output-line-1").text,
                Does.Contain("recovery_manifest.txt"));

            input.SetValueWithoutNotify("draft-command");
            SendKey(input, KeyCode.UpArrow);
            Assert.That(input.value, Is.EqualTo("files.list"));
            SendKey(input, KeyCode.UpArrow);
            Assert.That(input.value, Is.EqualTo("help"));
            SendKey(input, KeyCode.DownArrow);
            Assert.That(input.value, Is.EqualTo("files.list"));
            SendKey(input, KeyCode.DownArrow);
            Assert.That(input.value, Is.EqualTo("draft-command"));

            SendKey(input, KeyCode.Tab);
            yield return null;
            var runButton = root.Q<Button>("terminal-run");
            var clearButton = root.Q<Button>("terminal-clear-output");
            var closeButton = root.Q<Button>("prototype-app-close");
            Assert.That(input.focusController.focusedElement, Is.SameAs(runButton),
                "Tab must move focus from input to Run.");
            SendKey(runButton, KeyCode.Tab);
            Assert.That(input.focusController.focusedElement, Is.SameAs(clearButton));
            SendKey(clearButton, KeyCode.Tab);
            Assert.That(input.focusController.focusedElement, Is.SameAs(closeButton));
            SendKey(closeButton, KeyCode.Tab);
            Assert.That(input.focusController.focusedElement, Is.SameAs(input),
                "The terminal focus ring must wrap to the input.");

            LocalizationSettings.SelectedLocale = Resources.Load<Locale>("Localization/Locales/Locale-pt-BR");
            yield return WaitForLocale("pt-BR");
            yield return null;
            root = startup.Document.rootVisualElement;
            input = root.Q<TextField>("terminal-input");
            output = root.Q<ScrollView>("terminal-output");
            Assert.That(input.value, Is.EqualTo("draft-command"),
                "Locale rebuild must preserve the in-progress terminal input.");
            Assert.That(output.contentContainer.Q<Label>("terminal-output-line-1").text,
                Does.Contain("Arquivos sintéticos"));
            Assert.That(controller.ActiveApp, Is.EqualTo(LocalPrototypeApp.Terminal));

            Assert.That(controller.CloseActiveApp(), Is.True);
            Assert.That(controller.OpenApp("terminal"), Is.True);
            yield return null;
            root = startup.Document.rootVisualElement;
            input = root.Q<TextField>("terminal-input");
            output = root.Q<ScrollView>("terminal-output");
            Assert.That(output.contentContainer.Q<Label>("terminal-output-line-1").text,
                Does.Contain("Arquivos sintéticos"), "Closing Terminal must keep its session transcript.");
            input.SetValueWithoutNotify(string.Empty);
            SendKey(input, KeyCode.UpArrow);
            Assert.That(input.value, Is.EqualTo("files.list"), "Closing Terminal must keep command history.");
            Assert.That(store.SaveCount, Is.Zero, "Read-only Terminal commands must not write progress.");
        }

        [UnityTest]
        public IEnumerator TerminalClearAndShutdownAreReadOnlyAndResetSession()
        {
            var store = new MemoryProgressStore();
            controller.SetProgressStoreForSession(store);
            Assert.That(controller.StartPrototype(), Is.True);
            Assert.That(controller.BeginComputerBootForInteraction(), Is.True);
            yield return WaitForComputerReady();
            Assert.That(controller.OpenApp("terminal"), Is.True);
            yield return null;

            var root = startup.Document.rootVisualElement;
            var input = root.Q<TextField>("terminal-input");
            var output = root.Q<ScrollView>("terminal-output");
            for (var index = 0; index < 12; index++)
            {
                input.SetValueWithoutNotify("help");
                SendKey(input, KeyCode.Return);
                yield return null;
            }

            Assert.That(output.contentContainer.childCount, Is.EqualTo(12));
            Assert.That(output.verticalScroller.highValue, Is.GreaterThan(output.verticalScroller.lowValue),
                "Terminal output must be scrollable when it exceeds the visible viewport.");

            input.SetValueWithoutNotify("clear");
            SendKey(input, KeyCode.Return);
            yield return null;
            Assert.That(output.contentContainer.childCount, Is.Zero);
            SendKey(input, KeyCode.UpArrow);
            Assert.That(input.value, Is.EqualTo("clear"), "clear must not erase command history.");
            Assert.That(store.SaveCount, Is.Zero);

            Assert.That(controller.ShutdownComputer(), Is.True);
            Assert.That(controller.ActiveApp, Is.EqualTo(LocalPrototypeApp.None));
            Assert.That(controller.BeginComputerBootForInteraction(), Is.True);
            yield return WaitForComputerReady();
            Assert.That(controller.OpenApp("terminal"), Is.True);
            yield return null;
            root = startup.Document.rootVisualElement;
            input = root.Q<TextField>("terminal-input");
            output = root.Q<ScrollView>("terminal-output");
            SendKey(input, KeyCode.UpArrow);
            Assert.That(input.value, Is.Empty, "Shutdown and reboot must reset session history.");
            Assert.That(output.contentContainer.childCount, Is.Zero,
                "Shutdown and reboot must reset terminal output.");
            Assert.That(store.SaveCount, Is.Zero);
        }

        [UnityTest]
        public IEnumerator TerminalProducesActualPlayModeEvidenceInBothLocales()
        {
            controller.SetProgressStoreForSession(new MemoryProgressStore());
            Assert.That(controller.StartPrototype(), Is.True);
            Assert.That(controller.BeginComputerBootForInteraction(), Is.True);
            yield return WaitForComputerReady();
            Assert.That(controller.OpenApp("terminal"), Is.True);
            yield return null;
            yield return CaptureTerminalScreenshot("terminal-open-en-US.png");

            var root = startup.Document.rootVisualElement;
            var input = root.Q<TextField>("terminal-input");
            input.SetValueWithoutNotify("files.read recovery_manifest.txt");
            SendKey(input, KeyCode.Return);
            yield return null;
            Assert.That(root.Q<ScrollView>("terminal-output").contentContainer
                .Q<Label>("terminal-output-line-0").text,
                Does.Contain("Recovery index: locate this synthetic file"));
            yield return CaptureTerminalScreenshot("terminal-read-en-US.png");

            input = startup.Document.rootVisualElement.Q<TextField>("terminal-input");
            input.SetValueWithoutNotify("os.shutdown");
            SendKey(input, KeyCode.Return);
            yield return null;
            Assert.That(startup.Document.rootVisualElement.Q<ScrollView>("terminal-output").contentContainer
                .Q<Label>("terminal-output-line-1").text, Does.Contain("ERROR:"));
            yield return CaptureTerminalScreenshot("terminal-invalid-command-en-US.png");

            LocalizationSettings.SelectedLocale = Resources.Load<Locale>("Localization/Locales/Locale-pt-BR");
            yield return WaitForLocale("pt-BR");
            root = startup.Document.rootVisualElement;
            input = root.Q<TextField>("terminal-input");
            input.SetValueWithoutNotify("files.read briefing");
            SendKey(input, KeyCode.Return);
            yield return null;
            Assert.That(root.Q<ScrollView>("terminal-output").contentContainer
                .Q<Label>("terminal-output-line-2").text,
                Does.Contain("Nota de treinamento: esta sessão fictícia"));
            yield return CaptureTerminalScreenshot("terminal-pt-BR.png");
        }

        private static Vector3 MoveAndReadPosition(Transform player, CharacterController characterController,
            Vector3 start, Vector3 movement)
        {
            SetPlayerPosition(player, characterController, start);
            characterController.Move(movement);
            return player.position;
        }

        private static void SetPlayerPosition(Transform player, CharacterController characterController, Vector3 position)
        {
            characterController.enabled = false;
            player.position = position;
            characterController.enabled = true;
            Physics.SyncTransforms();
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

        private static void SendKey(VisualElement target, KeyCode keyCode)
        {
            target.Focus();
            using (var keyEvent = KeyDownEvent.GetPooled('\0', keyCode, EventModifiers.None))
            {
                keyEvent.target = target;
                target.panel.visualTree.SendEvent(keyEvent);
            }
        }

        private IEnumerator CaptureTerminalScreenshot(string fileName)
        {
            var directory = Path.GetFullPath(Path.Combine(Application.dataPath, "..", "docs", "evidence",
                "ZT-V01-WO-003", "screenshots"));
            Directory.CreateDirectory(directory);
            var path = Path.Combine(directory, fileName);
            yield return new WaitForEndOfFrame();
            ScreenCapture.CaptureScreenshot(path, 1);

            var deadline = Time.realtimeSinceStartup + 10f;
            while ((!File.Exists(path) || new FileInfo(path).Length == 0) && Time.realtimeSinceStartup < deadline)
            {
                yield return null;
            }

            Assert.That(File.Exists(path), Is.True, "The actual PlayMode screenshot was not written: " + fileName);
            Assert.That(new FileInfo(path).Length, Is.GreaterThan(0));
        }
    }
}
