using System;
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
            Screen.SetResolution(1920, 1080, false);
            yield return SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Single);

            Assert.That(Screen.width, Is.EqualTo(1920));
            Assert.That(Screen.height, Is.EqualTo(1080));

            var deadline = Time.realtimeSinceStartup + 30f;
            StartupController controller = null;
            while ((controller == null || !controller.IsReady) && Time.realtimeSinceStartup < deadline)
            {
                controller = UnityEngine.Object.FindFirstObjectByType<StartupController>();
                yield return null;
            }

            Assert.That(controller, Is.Not.Null);
            Assert.That(controller.IsReady, Is.True, "Localization timed out.");
            Assert.That(controller.CurrentLocaleCode, Is.EqualTo("en-US"));

            var root = controller.Document.rootVisualElement;
            var title = root.Q<Label>("product-title");
            var status = root.Q<Label>("status-label");
            var language = root.Q<Label>("language-label");
            var englishButton = root.Q<Button>("language-en");
            var portugueseButton = root.Q<Button>("language-pt");

            Assert.That(status.text, Is.EqualTo("The local foundation is ready to continue."));
            Assert.That(language.text, Is.EqualTo("Language"));
            Assert.That(englishButton.focusable && portugueseButton.focusable, Is.True);
            ScreenCapture.CaptureScreenshot("Temp/IM02/startup-en-US-1920x1080.png");
            yield return new WaitForEndOfFrame();

            portugueseButton.Focus();
            yield return null;
            Assert.That(root.focusController.focusedElement, Is.SameAs(portugueseButton));
            englishButton.Focus();
            yield return null;
            Assert.That(root.focusController.focusedElement, Is.SameAs(englishButton));
            Assert.That(englishButton.ClassListContains("locale-button--focused"), Is.True, "FocusIn did not apply the visible focus state.");

            SendKey(englishButton, KeyCode.Tab, shift: false);
            yield return null;
            Assert.That(root.focusController.focusedElement, Is.SameAs(portugueseButton), "Tab focus order failed.");

            SendKey(portugueseButton, KeyCode.Tab, shift: true);
            yield return null;
            Assert.That(root.focusController.focusedElement, Is.SameAs(englishButton), "Shift+Tab reverse focus order failed.");

            SendKey(englishButton, KeyCode.Tab, shift: false);
            yield return null;
            Assert.That(root.focusController.focusedElement, Is.SameAs(portugueseButton), "Tab focus order failed after reverse traversal.");

            SendKey(portugueseButton, KeyCode.Return, shift: false);
            yield return WaitForLocale(controller, "pt-BR", 5f);
            Assert.That(controller.CurrentLocaleCode, Is.EqualTo("pt-BR"));
            Assert.That(status.text, Is.EqualTo("A fundação local está pronta para continuar."));
            Assert.That(language.text, Is.EqualTo("Idioma"));

            SendClick(root.panel.visualTree, englishButton.worldBound.center);
            yield return WaitForLocale(controller, "en-US", 5f);
            Assert.That(controller.CurrentLocaleCode, Is.EqualTo("en-US"), "Mouse activation failed.");

            var localeChangeCount = 0;
            Action<Locale> localeChanged = _ => localeChangeCount++;
            LocalizationSettings.SelectedLocaleChanged += localeChanged;

            controller.enabled = false;
            LocalizationSettings.SelectedLocale = Resources.Load<Locale>("Localization/Locales/Locale-pt-BR");
            yield return null;
            Assert.That(controller.CurrentLocaleCode, Is.EqualTo("en-US"), "Disabled controller received locale changes.");

            controller.enabled = true;
            yield return null;
            Assert.That(controller.CurrentLocaleCode, Is.EqualTo("pt-BR"));
            Assert.That(localeChangeCount, Is.EqualTo(1), "Expected one locale event after re-enabling the controller.");
            LocalizationSettings.SelectedLocaleChanged -= localeChanged;

            var panel = controller.Document.panelSettings;
            var originalScale = panel.scale;
            var portugueseTable = Resources.Load<StringTable>("Localization/Tables/Startup-pt-BR");
            var expandedStatus = portugueseTable.GetEntry("foundation.status.expansion").LocalizedValue;
            Directory.CreateDirectory(Path.Combine(Application.dataPath, "../Temp/IM02"));

            panel.scale = 1f;
            status.text = expandedStatus;
            for (var frame = 0; frame < 3; frame++)
            {
                yield return null;
            }

            AssertElementsFit(root, title, status, language, englishButton, portugueseButton);
            AssertNoOverlap(title, status, language, englishButton, portugueseButton);
            Assert.That(root.worldBound.width, Is.EqualTo(1920f).Within(2f));
            Assert.That(root.worldBound.height, Is.EqualTo(1080f).Within(2f));
            Debug.LogFormat("IM02_LAYOUT scale=1 screen={0}x{1} logical={2:F0}x{3:F0} expansionChars={4}",
                Screen.width, Screen.height, root.worldBound.width, root.worldBound.height, expandedStatus.Length);
            ScreenCapture.CaptureScreenshot("Temp/IM02/startup-pt-BR-1920x1080.png");
            yield return new WaitForEndOfFrame();

            panel.scale = 1.5f;
            for (var frame = 0; frame < 3; frame++)
            {
                yield return null;
            }

            AssertElementsFit(root, title, status, language, englishButton, portugueseButton);
            AssertNoOverlap(title, status, language, englishButton, portugueseButton);
            Assert.That(root.worldBound.width, Is.EqualTo(1280f).Within(2f));
            Assert.That(root.worldBound.height, Is.EqualTo(720f).Within(2f));
            Debug.LogFormat("IM02_LAYOUT scale=1.5 screen={0}x{1} logical={2:F0}x{3:F0} expansionChars={4}",
                Screen.width, Screen.height, root.worldBound.width, root.worldBound.height, expandedStatus.Length);
            ScreenCapture.CaptureScreenshot("Temp/IM02/startup-pt-BR-150-percent.png");
            yield return new WaitForEndOfFrame();
            panel.scale = originalScale;
        }

        private static IEnumerator WaitForLocale(StartupController controller, string localeCode, float timeoutSeconds)
        {
            var deadline = Time.realtimeSinceStartup + timeoutSeconds;
            while (controller.CurrentLocaleCode != localeCode && Time.realtimeSinceStartup < deadline)
            {
                yield return null;
            }
        }

        private static void SendKey(VisualElement target, KeyCode keyCode, bool shift)
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

        private static void SendClick(VisualElement panel, Vector2 position)
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
