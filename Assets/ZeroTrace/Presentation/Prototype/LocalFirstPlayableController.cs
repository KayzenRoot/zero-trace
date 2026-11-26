using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using ZeroTrace.Domain;

namespace ZeroTrace.Presentation
{
    /// <summary>Local-only playable entry point for the technical prototype.</summary>
    public sealed class LocalFirstPlayableController : MonoBehaviour
    {
        private static readonly Color Obsidian = new Color32(11, 13, 16, 255);
        private static readonly Color Carbon = new Color32(26, 30, 36, 255);
        private static readonly Color Frost = new Color32(230, 237, 246, 255);
        private static readonly Color Lime = new Color32(183, 255, 46, 255);
        private static readonly Color Cyan = new Color32(93, 235, 255, 255);
        private static readonly Color Muted = new Color32(161, 174, 191, 255);

        private StartupController startup;
        private UIDocument document;
        private VisualElement startupRoot;
        private VisualElement overlayRoot;
        private VisualElement roomHud;
        private VisualElement desktopRoot;
        private VisualElement appContent;
        private Label roomPrompt;
        private Label roomState;
        private Label desktopClock;
        private Label exerciseResult;
        private Label selectedFileLabel;
        private LocalPrototypeFlow flow;
        private LocalPrototypeRoom room;
        private Coroutine startupLinkRoutine;
        private Coroutine bootRoutine;
        private ILocalPrototypeProgressStore injectedProgressStore;
        private string localeCode = "en-US";
        private string selectedFileId;
        private bool prototypeRunning;

        public bool IsPrototypeRunning => prototypeRunning;
        public bool IsDesktopVisible => desktopRoot != null;
        public bool IsLookingAtComputer => IsComputerInView();
        public LocalComputerState ComputerState => flow == null ? LocalComputerState.Off : flow.ComputerState;
        public LocalPrototypeApp ActiveApp => flow == null ? LocalPrototypeApp.None : flow.ActiveApp;
        public bool ExerciseComplete => flow != null && flow.ExerciseComplete;
        public int ExerciseAttemptCount => flow == null ? 0 : flow.AttemptCount;
        public string CurrentLocaleCode => localeCode;
        public string LastExerciseResultText => exerciseResult == null ? string.Empty : exerciseResult.text;
        public bool HasFirstPersonPlayer => room != null && room.Camera != null && room.Motor != null;
        public bool PlayerControlsEnabled => room != null && room.Motor != null && room.Motor.ControlsEnabled;

        private void OnEnable()
        {
            startupLinkRoutine = StartCoroutine(BindStartupWhenReady());
        }

        private IEnumerator BindStartupWhenReady()
        {
            var timeout = Time.realtimeSinceStartup + 12f;
            while (startup == null && Time.realtimeSinceStartup < timeout)
            {
                startup = FindFirstObjectByType<StartupController>();
                if (startup == null || !startup.IsReady)
                {
                    yield return null;
                    continue;
                }

                break;
            }

            if (startup != null)
            {
                startup.PrototypeRequested -= HandlePrototypeRequested;
                startup.PrototypeRequested += HandlePrototypeRequested;
                document = startup.Document;
                localeCode = string.IsNullOrEmpty(startup.CurrentLocaleCode) ? "en-US" : startup.CurrentLocaleCode;
            }

            startupLinkRoutine = null;
        }

        public void SetProgressStoreForSession(ILocalPrototypeProgressStore store)
        {
            if (prototypeRunning)
            {
                return;
            }

            injectedProgressStore = store;
        }

        public bool StartPrototype()
        {
            if (prototypeRunning)
            {
                return false;
            }

            if (startup == null)
            {
                startup = FindFirstObjectByType<StartupController>();
            }

            document = startup != null ? startup.Document : FindFirstObjectByType<UIDocument>();
            if (document == null || document.rootVisualElement == null)
            {
                return false;
            }

            startupRoot = document.rootVisualElement.Q<VisualElement>("startup-root");
            if (startupRoot == null)
            {
                return false;
            }

            var camera = Camera.main != null ? Camera.main : FindFirstObjectByType<Camera>();
            room = LocalPrototypeRoomFactory.Create(camera, out _);
            if (room == null)
            {
                return false;
            }

            flow = new LocalPrototypeFlow(injectedProgressStore ?? new LocalPrototypeProgressStore());
            localeCode = startup != null && !string.IsNullOrEmpty(startup.CurrentLocaleCode)
                ? startup.CurrentLocaleCode
                : localeCode;
            startupRoot.style.display = DisplayStyle.None;
            CreateOverlay();
            prototypeRunning = true;
            room.Motor.SetControlsEnabled(true);
            RenderRoomStatus();
            return true;
        }

        public bool InteractWithComputer()
        {
            if (!prototypeRunning || flow == null || !IsComputerInView() ||
                (flow.ComputerState != LocalComputerState.Off && flow.ComputerState != LocalComputerState.Shutdown))
            {
                return false;
            }

            if (!flow.BeginBoot())
            {
                return false;
            }

            RenderRoomStatus();
            bootRoutine = StartCoroutine(BootComputer());
            return true;
        }

        public bool BeginComputerBootForInteraction()
        {
            if (!prototypeRunning || flow == null ||
                (flow.ComputerState != LocalComputerState.Off && flow.ComputerState != LocalComputerState.Shutdown))
            {
                return false;
            }

            if (!flow.BeginBoot())
            {
                return false;
            }

            RenderRoomStatus();
            bootRoutine = StartCoroutine(BootComputer());
            return true;
        }

        private IEnumerator BootComputer()
        {
            yield return new WaitForSecondsRealtime(1.1f);
            if (prototypeRunning && flow != null && flow.CompleteBoot())
            {
                room.Motor.SetControlsEnabled(false);
                roomHud.style.display = DisplayStyle.None;
                flow.OpenApp(LocalPrototypeApp.Mail);
                BuildDesktop();
            }

            bootRoutine = null;
        }

        public bool OpenApp(string appId)
        {
            if (!prototypeRunning || flow == null)
            {
                return false;
            }

            var app = appId == "mail" ? LocalPrototypeApp.Mail
                : appId == "files" ? LocalPrototypeApp.FileManager
                : appId == "academy" ? LocalPrototypeApp.Academy
                : LocalPrototypeApp.None;
            if (!flow.OpenApp(app))
            {
                return false;
            }

            selectedFileId = null;
            exerciseResult = null;
            RenderApp();
            return true;
        }

        public bool CloseActiveApp()
        {
            if (flow == null || !flow.CloseApp())
            {
                return false;
            }

            exerciseResult = null;
            selectedFileId = null;
            RenderApp();
            return true;
        }

        public void SelectSyntheticFile(string fileId)
        {
            if (flow == null || flow.ActiveApp != LocalPrototypeApp.FileManager)
            {
                return;
            }

            selectedFileId = fileId;
            if (selectedFileLabel != null)
            {
                selectedFileLabel.text = string.IsNullOrEmpty(fileId)
                    ? Text("notSelected")
                    : LocalPrototypeText.Format("selected", localeCode, fileId + ".txt");
            }
        }

        public ExerciseSubmissionResult SubmitExerciseSelection()
        {
            if (flow == null)
            {
                return ExerciseSubmissionResult.Unavailable;
            }

            var result = flow.SubmitExerciseFile(selectedFileId);
            if (exerciseResult != null)
            {
                exerciseResult.text = result == ExerciseSubmissionResult.Correct ? Text("correct")
                    : result == ExerciseSubmissionResult.AlreadyComplete ? Text("alreadyComplete")
                    : result == ExerciseSubmissionResult.Incorrect ? Text("incorrect")
                    : Text("notSelected");
            }

            RefreshExerciseStatus();
            return result;
        }

        public bool RestartExercise()
        {
            if (flow == null || !flow.RestartExercise())
            {
                return false;
            }

            selectedFileId = null;
            RenderApp();
            return true;
        }

        public bool ShutdownComputer()
        {
            if (!prototypeRunning || flow == null || !flow.PowerOff())
            {
                return false;
            }

            if (bootRoutine != null)
            {
                StopCoroutine(bootRoutine);
                bootRoutine = null;
            }

            RemoveDesktop();
            room.Motor.SetControlsEnabled(true);
            RenderRoomStatus();
            return true;
        }

        public void ExitPrototype()
        {
            if (!prototypeRunning)
            {
                return;
            }

            if (bootRoutine != null)
            {
                StopCoroutine(bootRoutine);
                bootRoutine = null;
            }

            if (flow != null && (flow.ComputerState == LocalComputerState.Ready || flow.ComputerState == LocalComputerState.Booting))
            {
                flow.PowerOff();
            }

            RemoveDesktop();
            if (overlayRoot != null)
            {
                overlayRoot.RemoveFromHierarchy();
            }

            LocalPrototypeRoomFactory.Restore(room);
            room = null;
            flow = null;
            startupRoot.style.display = DisplayStyle.Flex;
            startupRoot = null;
            overlayRoot = null;
            roomHud = null;
            roomPrompt = null;
            roomState = null;
            prototypeRunning = false;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
        }

        private void CreateOverlay()
        {
            overlayRoot = new VisualElement { name = "local-prototype-overlay", pickingMode = PickingMode.Ignore };
            overlayRoot.style.position = Position.Absolute;
            overlayRoot.style.left = 0;
            overlayRoot.style.right = 0;
            overlayRoot.style.top = 0;
            overlayRoot.style.bottom = 0;
            document.rootVisualElement.Add(overlayRoot);

            roomHud = new VisualElement { name = "local-room-hud", pickingMode = PickingMode.Ignore };
            roomHud.style.position = Position.Absolute;
            roomHud.style.left = 0;
            roomHud.style.right = 0;
            roomHud.style.top = 0;
            roomHud.style.bottom = 0;
            roomHud.style.justifyContent = Justify.SpaceBetween;
            roomHud.style.paddingLeft = 28;
            roomHud.style.paddingRight = 28;
            roomHud.style.paddingTop = 22;
            roomHud.style.paddingBottom = 20;
            overlayRoot.Add(roomHud);

            var top = new VisualElement { pickingMode = PickingMode.Ignore };
            top.style.flexDirection = FlexDirection.Row;
            top.style.justifyContent = Justify.SpaceBetween;
            roomState = StyledLabel("", 14, Cyan, true);
            roomState.name = "prototype-computer-state";
            var badge = StyledLabel(Text("roomBadge"), 12, Frost, true);
            top.Add(roomState);
            top.Add(badge);
            roomHud.Add(top);

            var center = new VisualElement { pickingMode = PickingMode.Ignore };
            center.style.alignItems = Align.Center;
            center.style.justifyContent = Justify.Center;
            center.style.flexGrow = 1;
            var crosshair = StyledLabel("+", 22, Lime, true);
            crosshair.style.unityTextAlign = TextAnchor.MiddleCenter;
            center.Add(crosshair);
            roomPrompt = StyledLabel("", 16, Frost, false);
            roomPrompt.name = "prototype-computer-prompt";
            roomPrompt.style.marginTop = 12;
            roomPrompt.style.unityTextAlign = TextAnchor.MiddleCenter;
            center.Add(roomPrompt);
            roomHud.Add(center);

            var bottom = new VisualElement { pickingMode = PickingMode.Ignore };
            bottom.style.backgroundColor = (Color)new Color32(11, 13, 16, 225);
            bottom.style.paddingLeft = 14;
            bottom.style.paddingRight = 14;
            bottom.style.paddingTop = 10;
            bottom.style.paddingBottom = 10;
            var controls = StyledLabel(Text("focusHint"), 13, Frost, false);
            bottom.Add(controls);
            roomHud.Add(bottom);
            roomHud.pickingMode = PickingMode.Position;
        }

        private void BuildDesktop()
        {
            if (desktopRoot != null)
            {
                desktopRoot.RemoveFromHierarchy();
            }

            desktopRoot = new VisualElement { name = "null-os-desktop" };
            desktopRoot.style.position = Position.Absolute;
            desktopRoot.style.left = 0;
            desktopRoot.style.right = 0;
            desktopRoot.style.top = 0;
            desktopRoot.style.bottom = 0;
            desktopRoot.style.backgroundColor = Obsidian;
            desktopRoot.style.paddingLeft = 24;
            desktopRoot.style.paddingRight = 24;
            desktopRoot.style.paddingTop = 18;
            desktopRoot.style.paddingBottom = 18;
            desktopRoot.style.flexDirection = FlexDirection.Column;
            desktopRoot.pickingMode = PickingMode.Position;
            overlayRoot.Add(desktopRoot);

            var top = new VisualElement();
            top.style.flexDirection = FlexDirection.Row;
            top.style.alignItems = Align.Center;
            top.style.justifyContent = Justify.SpaceBetween;
            top.style.paddingLeft = 16;
            top.style.paddingRight = 16;
            top.style.paddingTop = 12;
            top.style.paddingBottom = 12;
            top.style.backgroundColor = Carbon;
            top.style.borderBottomWidth = 2;
            top.style.borderBottomColor = Cyan;
            var title = StyledLabel(Text("desktopTitle"), 18, Frost, true);
            title.name = "prototype-desktop-title";
            desktopClock = StyledLabel(ClockText(), 14, Cyan, true);
            top.Add(title);
            top.Add(desktopClock);
            desktopRoot.Add(top);

            var status = StyledLabel(Text("prototypeBadge"), 12, Lime, true);
            status.name = "prototype-status-badge";
            status.style.marginTop = 8;
            status.style.marginBottom = 8;
            desktopRoot.Add(status);

            var navigation = new VisualElement();
            navigation.style.flexDirection = FlexDirection.Row;
            navigation.style.flexWrap = Wrap.Wrap;
            navigation.style.marginBottom = 12;
            AddNavigationButton(navigation, "mail", LocalPrototypeApp.Mail);
            AddNavigationButton(navigation, "files", LocalPrototypeApp.FileManager);
            AddNavigationButton(navigation, "academy", LocalPrototypeApp.Academy);
            var power = CreateButton(Text("power"), () => ShutdownComputer(), false);
            power.style.marginLeft = 12;
            navigation.Add(power);
            desktopRoot.Add(navigation);

            appContent = new VisualElement { name = "null-os-app-content" };
            appContent.style.flexGrow = 1;
            appContent.style.minHeight = 0;
            appContent.style.backgroundColor = Carbon;
            appContent.style.paddingLeft = 20;
            appContent.style.paddingRight = 20;
            appContent.style.paddingTop = 18;
            appContent.style.paddingBottom = 18;
            desktopRoot.Add(appContent);
            RenderApp();
        }

        private void AddNavigationButton(VisualElement navigation, string textKey, LocalPrototypeApp app)
        {
            var id = app == LocalPrototypeApp.Mail ? "mail" : app == LocalPrototypeApp.FileManager ? "files" : "academy";
            var button = CreateButton(Text(textKey), () => OpenApp(id), false);
            navigation.Add(button);
        }

        private void RenderApp()
        {
            if (appContent == null || flow == null)
            {
                return;
            }

            appContent.Clear();
            exerciseResult = null;
            selectedFileLabel = null;

            var appTitle = flow.ActiveApp == LocalPrototypeApp.Mail ? Text("mailTitle")
                : flow.ActiveApp == LocalPrototypeApp.FileManager ? Text("filesTitle")
                : flow.ActiveApp == LocalPrototypeApp.Academy ? Text("academyTitle")
                : Text("desktopTitle");
            var heading = StyledLabel(appTitle, 22, Frost, true);
            heading.name = "prototype-app-title";
            heading.style.marginBottom = 12;
            appContent.Add(heading);

            if (flow.ActiveApp == LocalPrototypeApp.Mail)
            {
                appContent.Add(StyledLabel(Text("mailFrom"), 14, Cyan, true));
                var body = StyledLabel(Text("mailBody"), 16, Frost, false);
                body.style.whiteSpace = WhiteSpace.Normal;
                body.style.maxWidth = 980;
                body.style.marginTop = 12;
                body.style.marginBottom = 18;
                appContent.Add(body);
                appContent.Add(CreateButton(Text("files"), () => OpenApp("files"), false));
            }
            else if (flow.ActiveApp == LocalPrototypeApp.FileManager)
            {
                var prompt = StyledLabel(Text("exercisePrompt"), 15, Frost, false);
                prompt.name = "prototype-exercise-prompt";
                prompt.style.whiteSpace = WhiteSpace.Normal;
                prompt.style.marginBottom = 8;
                appContent.Add(prompt);
                appContent.Add(StyledLabel(Text("selectFile"), 13, Muted, false));
                AddFileButton("briefing", "fileBriefing");
                AddFileButton(LocalPrototypeFlow.RequiredSyntheticFileId, "fileRecovery");
                AddFileButton("shift_note", "fileShift");
                selectedFileLabel = StyledLabel(string.IsNullOrEmpty(selectedFileId)
                    ? Text("notSelected")
                    : LocalPrototypeText.Format("selected", localeCode, selectedFileId + ".txt"), 13, Cyan, true);
                selectedFileLabel.name = "prototype-selected-file";
                selectedFileLabel.style.marginTop = 8;
                appContent.Add(selectedFileLabel);
                var actions = new VisualElement();
                actions.style.flexDirection = FlexDirection.Row;
                actions.style.flexWrap = Wrap.Wrap;
                actions.style.marginTop = 12;
                actions.Add(CreateButton(Text("submit"), () => SubmitExerciseSelection(), true));
                actions.Add(CreateButton(Text("restart"), () => RestartExercise(), false));
                appContent.Add(actions);
                exerciseResult = StyledLabel(string.Empty, 15, Frost, true);
                exerciseResult.name = "prototype-exercise-result";
                exerciseResult.style.whiteSpace = WhiteSpace.Normal;
                exerciseResult.style.marginTop = 12;
                appContent.Add(exerciseResult);
                RefreshExerciseStatus();
            }
            else if (flow.ActiveApp == LocalPrototypeApp.Academy)
            {
                var body = StyledLabel(Text("academyBody"), 16, Frost, false);
                body.style.whiteSpace = WhiteSpace.Normal;
                body.style.maxWidth = 980;
                body.style.marginBottom = 18;
                appContent.Add(body);
                appContent.Add(CreateButton(Text("files"), () => OpenApp("files"), false));
            }
            else
            {
                appContent.Add(StyledLabel(Text("notice"), 15, Cyan, true));
            }

            var footer = new VisualElement();
            footer.style.flexGrow = 1;
            footer.style.justifyContent = Justify.FlexEnd;
            footer.style.alignItems = Align.FlexEnd;
            var close = CreateButton(Text("close"), () => CloseActiveApp(), false);
            footer.Add(close);
            appContent.Add(footer);
        }

        private void AddFileButton(string id, string textKey)
        {
            var button = CreateButton(Text(textKey), () => SelectSyntheticFile(id), false);
            button.style.alignSelf = Align.FlexStart;
            button.style.minWidth = 280;
            if (selectedFileId == id)
            {
                button.text = "✓  " + Text(textKey);
            }

            appContent.Add(button);
        }

        private void RefreshExerciseStatus()
        {
            if (desktopClock != null)
            {
                desktopClock.text = ClockText();
            }

            if (exerciseResult != null && string.IsNullOrEmpty(exerciseResult.text))
            {
                exerciseResult.text = flow != null && flow.ExerciseComplete ? Text("correct") : string.Empty;
            }
        }

        private void RemoveDesktop()
        {
            if (desktopRoot != null)
            {
                desktopRoot.RemoveFromHierarchy();
                desktopRoot = null;
                appContent = null;
                desktopClock = null;
                exerciseResult = null;
                selectedFileLabel = null;
            }
        }

        private void RenderRoomStatus()
        {
            if (roomState != null && flow != null)
            {
                roomState.text = flow.ComputerState == LocalComputerState.Off ? Text("computerOff")
                    : flow.ComputerState == LocalComputerState.Booting ? Text("computerBooting")
                    : flow.ComputerState == LocalComputerState.Ready ? Text("computerReady")
                    : Text("computerShutdown");
            }

            if (roomPrompt != null && flow != null)
            {
                roomPrompt.text = flow.ComputerState == LocalComputerState.Booting ? Text("roomHintBooting") : string.Empty;
            }
        }

        private bool IsComputerInView()
        {
            if (room == null || room.Camera == null || room.ComputerCollider == null)
            {
                return false;
            }

            return Physics.Raycast(room.Camera.transform.position, room.Camera.transform.forward,
                out var hit, 3.6f) && hit.collider == room.ComputerCollider;
        }

        private void Update()
        {
            if (!prototypeRunning)
            {
                return;
            }

            var activeLocale = startup != null && !string.IsNullOrEmpty(startup.CurrentLocaleCode)
                ? startup.CurrentLocaleCode
                : localeCode;
            if (activeLocale != localeCode)
            {
                localeCode = activeLocale;
                if (desktopRoot != null)
                {
                    BuildDesktop();
                }
                else
                {
                    RenderRoomStatus();
                    if (roomPrompt != null)
                    {
                        roomPrompt.text = Text("roomHintOff");
                    }
                }
            }

            if (desktopRoot != null)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    ShutdownComputer();
                    return;
                }

                if (desktopClock != null)
                {
                    desktopClock.text = ClockText();
                }

                return;
            }

            if (room == null || room.Motor == null || !room.Motor.ControlsEnabled)
            {
                return;
            }

            if (flow != null && flow.ComputerState == LocalComputerState.Booting)
            {
                return;
            }

            var looking = IsComputerInView();
            if (roomPrompt != null && flow != null)
            {
                roomPrompt.text = !looking ? string.Empty
                    : flow.ComputerState == LocalComputerState.Shutdown ? Text("roomHintShutdown")
                    : flow.ComputerState == LocalComputerState.Off ? Text("roomHintOff")
                    : string.Empty;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ExitPrototype();
                return;
            }

            if (looking && Input.GetKeyDown(KeyCode.E))
            {
                InteractWithComputer();
            }
        }

        private void OnDisable()
        {
            if (startupLinkRoutine != null)
            {
                StopCoroutine(startupLinkRoutine);
                startupLinkRoutine = null;
            }

            if (startup != null)
            {
                startup.PrototypeRequested -= HandlePrototypeRequested;
            }

            ExitPrototype();
        }

        private void OnDestroy()
        {
            if (startup != null)
            {
                startup.PrototypeRequested -= HandlePrototypeRequested;
            }
        }

        private void HandlePrototypeRequested() => StartPrototype();

        private string Text(string key) => LocalPrototypeText.Get(key, localeCode);
        private string ClockText() => LocalPrototypeText.Format("clock", localeCode,
            Mathf.FloorToInt((Time.time * 3f) % 60f).ToString("00"));

        private static Label StyledLabel(string text, int fontSize, Color color, bool bold)
        {
            var label = new Label(text);
            label.style.color = color;
            label.style.fontSize = fontSize;
            label.style.unityFontStyleAndWeight = bold ? FontStyle.Bold : FontStyle.Normal;
            label.style.whiteSpace = WhiteSpace.Normal;
            label.pickingMode = PickingMode.Ignore;
            return label;
        }

        private static Button CreateButton(string text, System.Action onClick, bool primary)
        {
            var button = new Button(onClick) { text = text };
            button.focusable = true;
            button.style.minHeight = 42;
            button.style.marginRight = 8;
            button.style.marginBottom = 8;
            button.style.paddingLeft = 14;
            button.style.paddingRight = 14;
            button.style.paddingTop = 8;
            button.style.paddingBottom = 8;
            button.style.color = primary ? Obsidian : Frost;
            button.style.backgroundColor = primary ? Lime : (Color)new Color32(36, 43, 51, 255);
            button.style.borderTopWidth = 1;
            button.style.borderRightWidth = 1;
            button.style.borderBottomWidth = 1;
            button.style.borderLeftWidth = 1;
            button.style.borderTopColor = primary ? Lime : (Color)new Color32(88, 101, 116, 255);
            button.style.borderRightColor = primary ? Lime : (Color)new Color32(88, 101, 116, 255);
            button.style.borderBottomColor = primary ? Lime : (Color)new Color32(88, 101, 116, 255);
            button.style.borderLeftColor = primary ? Lime : (Color)new Color32(88, 101, 116, 255);
            button.style.fontSize = 14;
            button.style.unityFontStyleAndWeight = FontStyle.Bold;
            return button;
        }
    }
}
