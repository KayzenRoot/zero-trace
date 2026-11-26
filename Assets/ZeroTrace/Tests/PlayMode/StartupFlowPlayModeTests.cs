using System.Collections;
using System.IO;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UIElements;
using ZeroTrace.Presentation;

namespace ZeroTrace.Presentation.Tests.PlayMode
{
    public sealed class StartupFlowPlayModeTests
    {
        [UnityTest]
        public IEnumerator StartupSceneLocalizesAndAcceptsInputFocusAndExpansion()
        {
            var outputDirectory = Path.Combine(Application.dataPath, "../Temp/IM02/CD-01");
            Directory.CreateDirectory(outputDirectory);
            var originalWidth = Screen.width;
            var originalHeight = Screen.height;
            var originalLocale = LocalizationSettings.SelectedLocale;
            StartupController controller = null;
            PanelSettings panel = null;
            var originalPanelScale = 1f;
            var englishLocale = Resources.Load<Locale>("Localization/Locales/Locale-en-US");
            var portugueseLocale = Resources.Load<Locale>("Localization/Locales/Locale-pt-BR");
            SceneManager.sceneLoaded += SelectPortugueseBeforeStartup;

            try
            {
                LocalizationSettings.SelectedLocale = englishLocale;
                Screen.SetResolution(1920, 1080, false);
                yield return SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Single);
                SceneManager.sceneLoaded -= SelectPortugueseBeforeStartup;

                Assert.That(Screen.width, Is.EqualTo(1920));
                Assert.That(Screen.height, Is.EqualTo(1080));

                var deadline = Time.realtimeSinceStartup + 30f;
                while ((controller == null || !controller.IsReady) && Time.realtimeSinceStartup < deadline)
                {
                    controller = UnityEngine.Object.FindFirstObjectByType<StartupController>();
                    yield return null;
                }

                Assert.That(controller, Is.Not.Null);
                Assert.That(controller.IsReady, Is.True, "Localization timed out.");
                Assert.That(controller.CurrentLocaleCode, Is.EqualTo("en-US"), "First startup must be English even when locale changes before initialization.");
                yield return null;

                var root = controller.Document.rootVisualElement;
                var title = root.Q<Label>("product-title");
                var status = root.Q<Label>("status-label");
                var language = root.Q<Label>("language-label");
                var englishButton = root.Q<Button>("language-en");
                var portugueseButton = root.Q<Button>("language-pt");

                Assert.That(status.text, Is.EqualTo("The local foundation is ready to continue."));
                Assert.That(language.text, Is.EqualTo("Language: English (US)"));
                Assert.That(englishButton.ClassListContains("locale-button--selected"), Is.True);
                Assert.That(portugueseButton.ClassListContains("locale-button--selected"), Is.False);
                Assert.That(englishButton.focusable && portugueseButton.focusable, Is.True);
                Assert.That(englishButton.resolvedStyle.borderBottomWidth, Is.EqualTo(4f));
                Assert.That(englishButton.resolvedStyle.borderBottomColor.r, Is.EqualTo(167f / 255f).Within(0.02f));
                yield return new WaitForEndOfFrame();
                ScreenCapture.CaptureScreenshot("Temp/IM02/CD-01/startup-en-US-1920x1080.png");

                portugueseButton.Focus();
                yield return WaitForLayoutFrames();
                Assert.That(root.focusController.focusedElement, Is.SameAs(portugueseButton));
                Assert.That(portugueseButton.ClassListContains("locale-button--focused"), Is.True, "FocusIn did not apply the visible focus state.");
                Assert.That(portugueseButton.resolvedStyle.borderTopColor.r, Is.GreaterThan(0.95f), "Keyboard focus needs a white outline distinct from selection.");

                englishButton.Focus();
                yield return null;
                Assert.That(root.focusController.focusedElement, Is.SameAs(englishButton));
                Assert.That(englishButton.ClassListContains("locale-button--focused"), Is.True);

                SendSyntheticKey(englishButton, KeyCode.Tab, shift: false);
                yield return null;
                Assert.That(root.focusController.focusedElement, Is.SameAs(portugueseButton), "Tab focus order failed.");

                SendSyntheticKey(portugueseButton, KeyCode.Tab, shift: true);
                yield return null;
                Assert.That(root.focusController.focusedElement, Is.SameAs(englishButton), "Shift+Tab reverse focus order failed.");

                SendSyntheticKey(englishButton, KeyCode.Tab, shift: false);
                yield return null;
                Assert.That(root.focusController.focusedElement, Is.SameAs(portugueseButton), "Tab focus order failed after reverse traversal.");

                SendSyntheticKey(portugueseButton, KeyCode.Return, shift: false);
                yield return WaitForReadyLocale(controller, "pt-BR", 5f);
                Assert.That(controller.CurrentLocaleCode, Is.EqualTo("pt-BR"));
                Assert.That(status.text, Is.EqualTo("A fundação local está pronta para continuar."));
                Assert.That(language.text, Is.EqualTo("Idioma: Português (Brasil)"));
                Assert.That(portugueseButton.ClassListContains("locale-button--selected"), Is.True);
                Assert.That(englishButton.ClassListContains("locale-button--selected"), Is.False);
                yield return new WaitForEndOfFrame();
                ScreenCapture.CaptureScreenshot("Temp/IM02/CD-01/startup-pt-BR-1920x1080.png");

                SendSyntheticClick(root.panel.visualTree, englishButton.worldBound.center);
                yield return WaitForReadyLocale(controller, "en-US", 5f);
                Assert.That(controller.CurrentLocaleCode, Is.EqualTo("en-US"), "Synthetic pointer event did not activate English.");
                Assert.That(language.text, Is.EqualTo("Language: English (US)"));

                controller.enabled = false;
                Assert.That(controller.IsReady, Is.False, "Disabled controller must not remain ready.");
                LocalizationSettings.SelectedLocale = portugueseLocale;
                yield return null;
                Assert.That(controller.CurrentLocaleCode, Is.EqualTo("en-US"), "Disabled controller received locale changes.");

                controller.enabled = true;
                yield return WaitForReadyLocale(controller, "pt-BR", 5f);
                Assert.That(controller.CurrentLocaleCode, Is.EqualTo("pt-BR"), "Re-enabled controller did not render the current supported locale.");
                Assert.That(language.text, Is.EqualTo("Idioma: Português (Brasil)"));

                controller.enabled = false;
                Assert.That(controller.IsReady, Is.False);
                LocalizationSettings.SelectedLocale = englishLocale;
                yield return null;
                Assert.That(controller.CurrentLocaleCode, Is.EqualTo("pt-BR"), "Second disabled controller observed a global locale change.");

                controller.enabled = true;
                yield return WaitForReadyLocale(controller, "en-US", 5f);
                Assert.That(controller.CurrentLocaleCode, Is.EqualTo("en-US"));
                Assert.That(language.text, Is.EqualTo("Language: English (US)"));

                panel = controller.Document.panelSettings;
                originalPanelScale = panel.scale;
                var portugueseTable = Resources.Load<StringTable>("Localization/Tables/Startup-pt-BR");
                var expandedStatus = portugueseTable.GetEntry("foundation.status.expansion").LocalizedValue;

                panel.scale = 1f;
                status.text = expandedStatus;
                yield return WaitForLayoutFrames();
                AssertElementsFit(root, title, status, language, englishButton, portugueseButton);
                AssertNoOverlap(title, status, language, englishButton, portugueseButton);
                Assert.That(root.worldBound.width, Is.EqualTo(1920f).Within(2f));
                Assert.That(root.worldBound.height, Is.EqualTo(1080f).Within(2f));
                Debug.LogFormat("IM02_LAYOUT scale=1 screen={0}x{1} logical={2:F0}x{3:F0} expansionChars={4}",
                    Screen.width, Screen.height, root.worldBound.width, root.worldBound.height, expandedStatus.Length);
                yield return new WaitForEndOfFrame();
                ScreenCapture.CaptureScreenshot("Temp/IM02/CD-01/startup-en-US-expanded-1920x1080.png");

                panel.scale = 1.5f;
                yield return WaitForLayoutFrames();
                AssertElementsFit(root, title, status, language, englishButton, portugueseButton);
                AssertNoOverlap(title, status, language, englishButton, portugueseButton);
                Assert.That(root.worldBound.width, Is.EqualTo(1280f).Within(2f));
                Assert.That(root.worldBound.height, Is.EqualTo(720f).Within(2f));
                Debug.LogFormat("IM02_LAYOUT scale=1.5 screen={0}x{1} logical={2:F0}x{3:F0} expansionChars={4}",
                    Screen.width, Screen.height, root.worldBound.width, root.worldBound.height, expandedStatus.Length);
                yield return new WaitForEndOfFrame();
                ScreenCapture.CaptureScreenshot("Temp/IM02/CD-01/startup-en-US-expanded-150-percent.png");
            }
            finally
            {
                SceneManager.sceneLoaded -= SelectPortugueseBeforeStartup;
                if (panel != null)
                {
                    panel.scale = originalPanelScale;
                }

                var restoreLocale = originalLocale != null &&
                                    (originalLocale.Identifier.Code == "en-US" || originalLocale.Identifier.Code == "pt-BR")
                    ? originalLocale
                    : englishLocale;
                if (restoreLocale != null)
                {
                    LocalizationSettings.SelectedLocale = restoreLocale;
                }

                Screen.SetResolution(originalWidth, originalHeight, false);
            }
        }

        [UnityTest]
        public IEnumerator MissingStartupDocumentRemainsUnavailableAcrossDisableAndLocaleChanges()
        {
            var originalLocale = LocalizationSettings.SelectedLocale;
            var englishLocale = Resources.Load<Locale>("Localization/Locales/Locale-en-US");
            var portugueseLocale = Resources.Load<Locale>("Localization/Locales/Locale-pt-BR");
            var holder = new GameObject("StartupControllerWithoutDocument");
            holder.SetActive(false);
            var controller = holder.AddComponent<StartupController>();

            try
            {
                holder.SetActive(true);
                controller.enabled = false;
                LocalizationSettings.SelectedLocale = portugueseLocale;
                yield return null;
                Assert.That(controller.IsReady, Is.False, "Disabling during initialization must cancel pending work.");

                LogAssert.Expect(LogType.Error, "[StartupController] Startup interface is unavailable.");
                controller.enabled = true;
                yield return null;
                Assert.That(controller.IsReady, Is.False, "Missing UI must never be reported ready.");
                Assert.That(controller.CurrentLocaleCode, Is.Null);

                LocalizationSettings.SelectedLocale = englishLocale;
                yield return null;
                Assert.That(controller.IsReady, Is.False, "A locale event cannot recover a failed startup without its view.");
                Assert.That(controller.CurrentLocaleCode, Is.Null);
            }
            finally
            {
                if (originalLocale != null)
                {
                    LocalizationSettings.SelectedLocale = originalLocale;
                }

                UnityEngine.Object.Destroy(holder);
            }
        }

        private static void SelectPortugueseBeforeStartup(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "SampleScene")
            {
                LocalizationSettings.SelectedLocale = Resources.Load<Locale>("Localization/Locales/Locale-pt-BR");
            }
        }

        private static IEnumerator WaitForReadyLocale(StartupController controller, string localeCode, float timeoutSeconds)
        {
            var deadline = Time.realtimeSinceStartup + timeoutSeconds;
            while ((!controller.IsReady || controller.CurrentLocaleCode != localeCode) && Time.realtimeSinceStartup < deadline)
            {
                yield return null;
            }
        }

        private static IEnumerator WaitForLayoutFrames()
        {
            for (var frame = 0; frame < 3; frame++)
            {
                yield return null;
            }
        }

        private static void SendSyntheticKey(VisualElement target, KeyCode keyCode, bool shift)
        {
            var osEvent = new Event
            {
                type = EventType.KeyDown,
                keyCode = keyCode,
                modifiers = shift ? EventModifiers.Shift : EventModifiers.None
            };

            using (var keyDown = KeyDownEvent.GetPooled(osEvent))
            {
                target.SendEvent(keyDown);
            }
        }

        private static void SendSyntheticClick(VisualElement panel, Vector2 position)
        {
            var pointerDownEvent = new Event
            {
                type = EventType.MouseDown,
                button = 0,
                clickCount = 1,
                mousePosition = position
            };
            using (var pointerDown = PointerDownEvent.GetPooled(pointerDownEvent))
            {
                panel.SendEvent(pointerDown);
            }

            var pointerUpEvent = new Event
            {
                type = EventType.MouseUp,
                button = 0,
                clickCount = 1,
                mousePosition = position
            };
            using (var pointerUp = PointerUpEvent.GetPooled(pointerUpEvent))
            {
                panel.SendEvent(pointerUp);
            }
        }

        private static void AssertElementsFit(VisualElement root, params VisualElement[] children)
        {
            Assert.That(root.worldBound.width, Is.GreaterThan(0f));
            var rootBounds = root.worldBound;

            foreach (var child in children)
            {
                var childBounds = child.worldBound;
                Assert.That(childBounds.xMin, Is.GreaterThanOrEqualTo(rootBounds.xMin - 1f));
                Assert.That(childBounds.yMin, Is.GreaterThanOrEqualTo(rootBounds.yMin - 1f));
                Assert.That(childBounds.xMax, Is.LessThanOrEqualTo(rootBounds.xMax + 1f));
                Assert.That(childBounds.yMax, Is.LessThanOrEqualTo(rootBounds.yMax + 1f));
            }
        }

        private static void AssertNoOverlap(params VisualElement[] elements)
        {
            for (var left = 0; left < elements.Length; left++)
            {
                for (var right = left + 1; right < elements.Length; right++)
                {
                    Assert.That(elements[left].worldBound.Overlaps(elements[right].worldBound), Is.False,
                        $"Layout elements {elements[left].name} and {elements[right].name} overlap.");
                }
            }
        }
    }
}
