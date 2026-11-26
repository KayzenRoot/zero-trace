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
        private const string LanguageKey = "foundation.language.label";
        private const string EnglishLabelKey = "foundation.language.en";
        private const string PortugueseLabelKey = "foundation.language.pt";

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
        private bool subscribed;
        private bool handlersRegistered;
        private bool viewBound;
        private bool ready;
        private string currentLocaleCode;

        public bool IsReady => ready;
        public UIDocument Document => document;
        public string CurrentLocaleCode => currentLocaleCode;
        public string StatusText => statusLabel == null ? string.Empty : statusLabel.text;

        private void OnEnable()
        {
            SubscribeToLocaleChanges();
            RegisterButtonHandlers();
            if (ready)
            {
                var activeLocale = LocalizationSettings.SelectedLocale;
                if (activeLocale != null)
                {
                    RenderLocale(activeLocale);
                }
            }
        }

        private IEnumerator Start()
        {
            if (document == null)
            {
                document = GetComponent<UIDocument>();
            }

            if (!BindView())
            {
                yield break;
            }

            englishLocale = Resources.Load<Locale>("Localization/Locales/Locale-en-US");
            portugueseLocale = Resources.Load<Locale>("Localization/Locales/Locale-pt-BR");
            englishTable = Resources.Load<StringTable>("Localization/Tables/Startup-en-US");
            portugueseTable = Resources.Load<StringTable>("Localization/Tables/Startup-pt-BR");

            if (!HasRequiredAssets())
            {
                ShowUnavailable("Local language data is unavailable.");
                yield break;
            }

            LocalizationSettings.Instance.SetAvailableLocales(new ResourceLocalesProvider(new[] { englishLocale, portugueseLocale }));
            SubscribeToLocaleChanges();
            LocalizationSettings.SelectedLocale = englishLocale;

            var deadline = Time.realtimeSinceStartup + 15f;
            while (!ready && Time.realtimeSinceStartup < deadline)
            {
                yield return null;
            }

            if (!ready)
            {
                ShowUnavailable("Local language service could not start.");
            }
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

            if (titleLabel == null || statusLabel == null || languageLabel == null || englishButton == null || portugueseButton == null)
            {
                return false;
            }

            englishButton.focusable = true;
            portugueseButton.focusable = true;
            englishButton.tabIndex = 0;
            portugueseButton.tabIndex = 0;
            viewBound = true;
            RegisterButtonHandlers();
            statusLabel.text = "Starting local language data...";
            return true;
        }

        private bool HasRequiredAssets() =>
            englishLocale != null && portugueseLocale != null && englishTable != null && portugueseTable != null &&
            string.Equals(englishLocale.Identifier.Code, EnglishCode, StringComparison.Ordinal) &&
            string.Equals(portugueseLocale.Identifier.Code, PortugueseCode, StringComparison.Ordinal);

        private void RegisterButtonHandlers()
        {
            if (!viewBound || handlersRegistered)
            {
                return;
            }

            englishButton.clicked += SelectEnglish;
            portugueseButton.clicked += SelectPortuguese;
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

            englishButton.clicked -= SelectEnglish;
            portugueseButton.clicked -= SelectPortuguese;
            englishButton.UnregisterCallback<KeyDownEvent>(HandleEnglishKeyDown);
            englishButton.UnregisterCallback<FocusInEvent>(HandleEnglishFocusIn);
            englishButton.UnregisterCallback<FocusOutEvent>(HandleEnglishFocusOut);
            portugueseButton.UnregisterCallback<KeyDownEvent>(HandlePortugueseKeyDown);
            portugueseButton.UnregisterCallback<FocusInEvent>(HandlePortugueseFocusIn);
            portugueseButton.UnregisterCallback<FocusOutEvent>(HandlePortugueseFocusOut);
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
            if (!isActiveAndEnabled || locale == null)
            {
                return;
            }

            var code = locale.Identifier.Code;
            if (code != EnglishCode && code != PortugueseCode)
            {
                return;
            }

            ready = true;
            RenderLocale(locale);
        }

        private void RenderLocale(Locale locale)
        {
            var code = locale.Identifier.Code;
            var table = code == PortugueseCode ? portugueseTable : englishTable;
            currentLocaleCode = code;
            titleLabel.text = ResolveEntry(table, TitleKey, "ZERO TRACE");
            statusLabel.text = ResolveEntry(table, StatusKey, "Local foundation ready");
            languageLabel.text = ResolveEntry(table, LanguageKey, "Language");
            englishButton.text = ResolveEntry(table, EnglishLabelKey, "English (US)");
            portugueseButton.text = ResolveEntry(table, PortugueseLabelKey, "Português (Brasil)");
            englishButton.EnableInClassList("locale-button--selected", code == EnglishCode);
            portugueseButton.EnableInClassList("locale-button--selected", code == PortugueseCode);
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

        private void ShowUnavailable(string message)
        {
            ready = false;
            statusLabel.text = message;
            englishButton.SetEnabled(false);
            portugueseButton.SetEnabled(false);
        }

        private void OnDisable()
        {
            UnsubscribeFromLocaleChanges();
            UnregisterButtonHandlers();
        }

        private void OnDestroy()
        {
            UnsubscribeFromLocaleChanges();
            UnregisterButtonHandlers();
        }
    }
}
