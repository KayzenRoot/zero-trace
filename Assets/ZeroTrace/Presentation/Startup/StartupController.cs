using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.UIElements;

namespace ZeroTrace.Presentation
{
    public sealed class ResourceLocalesProvider : ILocalesProvider
    {
        private readonly List<Locale> locales;
        public List<Locale> Locales => locales;

        public ResourceLocalesProvider(IEnumerable<Locale> source)
        {
            locales = source.Where(locale => locale != null).ToList();
        }

        public Locale GetLocale(LocaleIdentifier id) =>
            locales.FirstOrDefault(locale => string.Equals(locale.Identifier.Code, id.Code, StringComparison.OrdinalIgnoreCase));

        public void AddLocale(Locale locale)
        {
            if (locale != null && GetLocale(locale.Identifier) == null)
            {
                locales.Add(locale);
            }
        }

        public bool RemoveLocale(Locale locale) => locales.Remove(locale);
    }

    public sealed class StartupController : MonoBehaviour
    {
        private const string EnglishCode = "en-US";
        private const string PortugueseCode = "pt-BR";
        private const string TitleKey = "foundation.title";
        private const string StatusKey = "foundation.status";
        private const string UnavailableKey = "foundation.status.unavailable";
        private const string LanguageKey = "foundation.language.label";
        private const string EnglishLabelKey = "foundation.language.en";
        private const string PortugueseLabelKey = "foundation.language.pt";

        private static readonly string[] RequiredRuntimeKeys =
        {
            TitleKey,
            StatusKey,
            UnavailableKey,
            LanguageKey,
            EnglishLabelKey,
            PortugueseLabelKey
        };

        [SerializeField] private UIDocument document;
        private Locale englishLocale;
        private Locale portugueseLocale;
        private StringTable englishTable;
        private StringTable portugueseTable;
        private Label titleLabel;
        private Label statusLabel;
        private Label languageLabel;
        private Button englishButton;
        private Button portugueseButton;
        private Button prototypeLaunchButton;
        private Label prototypeNoticeLabel;
        private Coroutine initializationRoutine;
        private bool subscribed;
        private bool handlersRegistered;
        private bool viewBound;
        private bool ready;
        private bool initializedSuccessfully;
        private string currentLocaleCode;

        public bool IsReady => ready;
        public UIDocument Document => document;
        public string CurrentLocaleCode => currentLocaleCode;
        public string StatusText => statusLabel == null ? string.Empty : statusLabel.text;
        public event Action PrototypeRequested;

        private void OnEnable()
        {
            ready = false;
            initializationRoutine = StartCoroutine(InitializeOnNextFrame());
        }

        private IEnumerator InitializeOnNextFrame()
        {
            // UIDocument creates its visual tree from OnEnable. Wait one frame so
            // component ordering cannot make the startup view appear unavailable.
            yield return null;

            if (!isActiveAndEnabled)
            {
                initializationRoutine = null;
                yield break;
            }

            if (document == null)
            {
                document = GetComponent<UIDocument>();
            }

            if (!BindView())
            {
                ShowUnavailable(GetSelectedLocaleCode(), "Startup interface is unavailable.");
                initializationRoutine = null;
                yield break;
            }

            englishLocale = Resources.Load<Locale>("Localization/Locales/Locale-en-US");
            portugueseLocale = Resources.Load<Locale>("Localization/Locales/Locale-pt-BR");
            englishTable = Resources.Load<StringTable>("Localization/Tables/Startup-en-US");
            portugueseTable = Resources.Load<StringTable>("Localization/Tables/Startup-pt-BR");

            if (!HasRequiredAssets())
            {
                ShowUnavailable(GetSelectedLocaleCode(), null);
                initializationRoutine = null;
                yield break;
            }

            var settings = LocalizationSettings.Instance;
            if (settings == null)
            {
                ShowUnavailable(GetSelectedLocaleCode(), null);
                initializationRoutine = null;
                yield break;
            }

            settings.SetAvailableLocales(new ResourceLocalesProvider(new[] { englishLocale, portugueseLocale }));

            // The first successful startup is always English. Re-enabling a
            // previously initialized controller retains the selected supported locale.
            var selectedLocale = initializedSuccessfully
                ? FindSupportedLocale(LocalizationSettings.SelectedLocale?.Identifier.Code) ??
                  FindSupportedLocale(currentLocaleCode) ?? englishLocale
                : englishLocale;

            LocalizationSettings.SelectedLocale = selectedLocale;
            if (!TryRenderLocale(selectedLocale))
            {
                ShowUnavailable(selectedLocale.Identifier.Code, null);
                initializationRoutine = null;
                yield break;
            }

            ready = true;
            initializedSuccessfully = true;
            RegisterButtonHandlers();
            SubscribeToLocaleChanges();
            initializationRoutine = null;
        }

        private bool BindView()
        {
            if (document == null || document.rootVisualElement == null)
            {
                return false;
            }

            var root = document.rootVisualElement;
            titleLabel = root.Q<Label>("product-title");
            statusLabel = root.Q<Label>("status-label");
            languageLabel = root.Q<Label>("language-label");
            englishButton = root.Q<Button>("language-en");
            portugueseButton = root.Q<Button>("language-pt");
            prototypeLaunchButton = root.Q<Button>("prototype-launch");
            prototypeNoticeLabel = root.Q<Label>("prototype-notice");
            viewBound = true;

            if (!HasRequiredControls())
            {
                return false;
            }

            englishButton.focusable = true;
            portugueseButton.focusable = true;
            englishButton.tabIndex = 0;
            portugueseButton.tabIndex = 0;
            if (prototypeLaunchButton != null)
            {
                prototypeLaunchButton.focusable = true;
                prototypeLaunchButton.tabIndex = 2;
            }
            return true;
        }

        private bool HasRequiredControls() =>
            titleLabel != null && statusLabel != null && languageLabel != null &&
            englishButton != null && portugueseButton != null;

        private bool HasRequiredAssets() =>
            englishLocale != null && portugueseLocale != null && englishTable != null && portugueseTable != null &&
            string.Equals(englishLocale.Identifier.Code, EnglishCode, StringComparison.Ordinal) &&
            string.Equals(portugueseLocale.Identifier.Code, PortugueseCode, StringComparison.Ordinal) &&
            HasRequiredEntries(englishTable) && HasRequiredEntries(portugueseTable);

        private static bool HasRequiredEntries(StringTable table) =>
            table != null && RequiredRuntimeKeys.All(key =>
            {
                var entry = table.GetEntry(key);
                return entry != null && !string.IsNullOrWhiteSpace(entry.LocalizedValue);
            });

        private void RegisterButtonHandlers()
        {
            if (!viewBound || handlersRegistered || !HasRequiredControls())
            {
                return;
            }

            englishButton.clicked += SelectEnglish;
            portugueseButton.clicked += SelectPortuguese;
            if (prototypeLaunchButton != null)
            {
                prototypeLaunchButton.clicked += HandlePrototypeRequested;
            }
            englishButton.RegisterCallback<KeyDownEvent>(HandleEnglishKeyDown);
            englishButton.RegisterCallback<FocusInEvent>(HandleEnglishFocusIn);
            englishButton.RegisterCallback<FocusOutEvent>(HandleEnglishFocusOut);
            portugueseButton.RegisterCallback<KeyDownEvent>(HandlePortugueseKeyDown);
            portugueseButton.RegisterCallback<FocusInEvent>(HandlePortugueseFocusIn);
            portugueseButton.RegisterCallback<FocusOutEvent>(HandlePortugueseFocusOut);
            handlersRegistered = true;
        }

        private void UnregisterButtonHandlers()
        {
            if (!handlersRegistered)
            {
                return;
            }

            if (englishButton != null)
            {
                englishButton.clicked -= SelectEnglish;
                englishButton.UnregisterCallback<KeyDownEvent>(HandleEnglishKeyDown);
                englishButton.UnregisterCallback<FocusInEvent>(HandleEnglishFocusIn);
                englishButton.UnregisterCallback<FocusOutEvent>(HandleEnglishFocusOut);
            }

            if (portugueseButton != null)
            {
                portugueseButton.clicked -= SelectPortuguese;
                portugueseButton.UnregisterCallback<KeyDownEvent>(HandlePortugueseKeyDown);
                portugueseButton.UnregisterCallback<FocusInEvent>(HandlePortugueseFocusIn);
                portugueseButton.UnregisterCallback<FocusOutEvent>(HandlePortugueseFocusOut);
            }

            if (prototypeLaunchButton != null)
            {
                prototypeLaunchButton.clicked -= HandlePrototypeRequested;
            }

            handlersRegistered = false;
        }

        private void SubscribeToLocaleChanges()
        {
            if (!subscribed)
            {
                LocalizationSettings.SelectedLocaleChanged += HandleLocaleChanged;
                subscribed = true;
            }
        }

        private void UnsubscribeFromLocaleChanges()
        {
            if (subscribed)
            {
                LocalizationSettings.SelectedLocaleChanged -= HandleLocaleChanged;
                subscribed = false;
            }
        }

        private void HandleEnglishFocusIn(FocusInEvent evt) => englishButton.AddToClassList("locale-button--focused");
        private void HandleEnglishFocusOut(FocusOutEvent evt) => englishButton.RemoveFromClassList("locale-button--focused");
        private void HandlePortugueseFocusIn(FocusInEvent evt) => portugueseButton.AddToClassList("locale-button--focused");
        private void HandlePortugueseFocusOut(FocusOutEvent evt) => portugueseButton.RemoveFromClassList("locale-button--focused");
        private void SelectEnglish() => SelectLocale(englishLocale);
        private void SelectPortuguese() => SelectLocale(portugueseLocale);

        private void HandlePrototypeRequested()
        {
            if (ready)
            {
                PrototypeRequested?.Invoke();
            }
        }

        private void HandleEnglishKeyDown(KeyDownEvent evt)
        {
            if (evt.keyCode == KeyCode.Tab && !evt.shiftKey)
            {
                portugueseButton.Focus();
                evt.PreventDefault();
                evt.StopPropagation();
                return;
            }

            if (IsActivationKey(evt.keyCode))
            {
                SelectEnglish();
                evt.StopPropagation();
            }
        }

        private void HandlePortugueseKeyDown(KeyDownEvent evt)
        {
            if (evt.keyCode == KeyCode.Tab && !evt.shiftKey && prototypeLaunchButton != null &&
                prototypeLaunchButton.enabledInHierarchy)
            {
                prototypeLaunchButton.Focus();
                evt.PreventDefault();
                evt.StopPropagation();
                return;
            }

            if (evt.keyCode == KeyCode.Tab && evt.shiftKey)
            {
                englishButton.Focus();
                evt.PreventDefault();
                evt.StopPropagation();
                return;
            }

            if (IsActivationKey(evt.keyCode))
            {
                SelectPortuguese();
                evt.StopPropagation();
            }
        }

        private static bool IsActivationKey(KeyCode keyCode) =>
            keyCode == KeyCode.Return || keyCode == KeyCode.KeypadEnter || keyCode == KeyCode.Space;

        private void SelectLocale(Locale locale)
        {
            if (ready && locale != null)
            {
                LocalizationSettings.SelectedLocale = locale;
            }
        }

        private void HandleLocaleChanged(Locale locale)
        {
            if (!ready || !isActiveAndEnabled || !viewBound || locale == null)
            {
                return;
            }

            var code = locale.Identifier.Code;
            if (code != EnglishCode && code != PortugueseCode)
            {
                return;
            }

            if (string.Equals(code, currentLocaleCode, StringComparison.Ordinal))
            {
                return;
            }

            if (!TryRenderLocale(locale))
            {
                ShowUnavailable(code, null);
            }
        }

        private bool TryRenderLocale(Locale locale)
        {
            if (!viewBound || !HasRequiredControls() || locale == null)
            {
                return false;
            }

            var code = locale.Identifier.Code;
            var table = code == PortugueseCode ? portugueseTable : code == EnglishCode ? englishTable : null;
            if (table == null || !HasRequiredEntries(table))
            {
                return false;
            }

            var englishLabel = ResolveEntry(table, EnglishLabelKey, "English (US)");
            var portugueseLabel = ResolveEntry(table, PortugueseLabelKey, "Português (Brasil)");
            titleLabel.text = ResolveEntry(table, TitleKey, "ZERO TRACE");
            statusLabel.text = ResolveEntry(table, StatusKey, "Local foundation ready");
            languageLabel.text = ResolveEntry(table, LanguageKey, code == PortugueseCode ? "Idioma" : "Language") +
                                 ": " + (code == PortugueseCode ? portugueseLabel : englishLabel);
            englishButton.text = englishLabel;
            portugueseButton.text = portugueseLabel;
            englishButton.SetEnabled(true);
            portugueseButton.SetEnabled(true);
            englishButton.EnableInClassList("locale-button--selected", code == EnglishCode);
            portugueseButton.EnableInClassList("locale-button--selected", code == PortugueseCode);
            if (prototypeLaunchButton != null)
            {
                prototypeLaunchButton.text = LocalPrototypeText.Get("launch", code);
                prototypeLaunchButton.SetEnabled(true);
            }

            if (prototypeNoticeLabel != null)
            {
                prototypeNoticeLabel.text = LocalPrototypeText.Get("notice", code);
            }
            currentLocaleCode = code;
            return true;
        }

        public static string ResolveEntry(StringTable table, string key, string fallback)
        {
            if (table == null || string.IsNullOrWhiteSpace(key))
            {
                return fallback;
            }

            var entry = table.GetEntry(key);
            return entry == null || string.IsNullOrWhiteSpace(entry.LocalizedValue) ? fallback : entry.LocalizedValue;
        }

        private static Locale FindSupportedLocale(string code)
        {
            if (string.Equals(code, EnglishCode, StringComparison.Ordinal))
            {
                return Resources.Load<Locale>("Localization/Locales/Locale-en-US");
            }

            return string.Equals(code, PortugueseCode, StringComparison.Ordinal)
                ? Resources.Load<Locale>("Localization/Locales/Locale-pt-BR")
                : null;
        }

        private static string GetSelectedLocaleCode() => LocalizationSettings.SelectedLocale?.Identifier.Code ?? EnglishCode;

        private void ShowUnavailable(string localeCode, string diagnostic)
        {
            UnsubscribeFromLocaleChanges();
            UnregisterButtonHandlers();
            ready = false;
            currentLocaleCode = null;

            if (englishButton != null)
            {
                englishButton.SetEnabled(false);
                englishButton.RemoveFromClassList("locale-button--selected");
            }

            if (portugueseButton != null)
            {
                portugueseButton.SetEnabled(false);
                portugueseButton.RemoveFromClassList("locale-button--selected");
            }

            if (prototypeLaunchButton != null)
            {
                prototypeLaunchButton.SetEnabled(false);
            }

            if (statusLabel != null)
            {
                var table = localeCode == PortugueseCode ? portugueseTable : englishTable;
                var fallback = localeCode == PortugueseCode
                    ? "Os dados locais de inicialização estão indisponíveis."
                    : "Local startup data is unavailable.";
                statusLabel.text = ResolveEntry(table, UnavailableKey, fallback);
            }
            else if (!string.IsNullOrEmpty(diagnostic))
            {
                Debug.LogError("[StartupController] " + diagnostic);
            }
        }

        private void OnDisable()
        {
            if (initializationRoutine != null)
            {
                StopCoroutine(initializationRoutine);
                initializationRoutine = null;
            }

            ready = false;
            UnsubscribeFromLocaleChanges();
            UnregisterButtonHandlers();
            viewBound = false;
            ClearViewReferences();
        }

        private void OnDestroy()
        {
            if (initializationRoutine != null)
            {
                StopCoroutine(initializationRoutine);
                initializationRoutine = null;
            }

            UnsubscribeFromLocaleChanges();
            UnregisterButtonHandlers();
        }

        private void ClearViewReferences()
        {
            titleLabel = null;
            statusLabel = null;
            languageLabel = null;
            englishButton = null;
            portugueseButton = null;
            prototypeLaunchButton = null;
            prototypeNoticeLabel = null;
        }
    }
}
