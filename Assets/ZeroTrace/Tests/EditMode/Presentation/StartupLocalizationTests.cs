using System;
using System.Linq;
using NUnit.Framework;
using UnityEditor;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;
using ZeroTrace.Presentation;

namespace ZeroTrace.Presentation.Tests.EditMode
{
    public sealed class StartupLocalizationTests
    {
        private const string LocalePath = "Assets/ZeroTrace/Presentation/Resources/Localization/Locales/";
        private const string TablePath = "Assets/ZeroTrace/Presentation/Resources/Localization/Tables/";

        private static readonly string[] RequiredKeys =
        {
            "foundation.title",
            "foundation.status",
            "foundation.language.label",
            "foundation.language.en",
            "foundation.language.pt",
            "foundation.status.expansion"
        };

        [Test]
        public void LocaleAssetsUseEnUsAndPtBr()
        {
            var english = AssetDatabase.LoadAssetAtPath<Locale>(LocalePath + "Locale-en-US.asset");
            var portuguese = AssetDatabase.LoadAssetAtPath<Locale>(LocalePath + "Locale-pt-BR.asset");

            Assert.That(english, Is.Not.Null);
            Assert.That(portuguese, Is.Not.Null);
            Assert.That(english.Identifier.Code, Is.EqualTo("en-US"));
            Assert.That(portuguese.Identifier.Code, Is.EqualTo("pt-BR"));
        }

        [Test]
        public void StringTablesHaveMatchingNonblankKeysAndExpansion()
        {
            var shared = AssetDatabase.LoadAssetAtPath<SharedTableData>(TablePath + "Startup-Shared.asset");
            var english = AssetDatabase.LoadAssetAtPath<StringTable>(TablePath + "Startup-en-US.asset");
            var portuguese = AssetDatabase.LoadAssetAtPath<StringTable>(TablePath + "Startup-pt-BR.asset");

            Assert.That(shared, Is.Not.Null);
            Assert.That(english, Is.Not.Null);
            Assert.That(portuguese, Is.Not.Null);
            CollectionAssert.AreEquivalent(RequiredKeys, shared.Entries.Select(entry => entry.Key).ToArray());

            foreach (var key in RequiredKeys)
            {
                Assert.That(english.GetEntry(key), Is.Not.Null, $"Missing en-US key: {key}");
                Assert.That(portuguese.GetEntry(key), Is.Not.Null, $"Missing pt-BR key: {key}");
                Assert.That(english.GetEntry(key).LocalizedValue, Is.Not.Null.And.Not.Empty, $"Blank en-US value: {key}");
                Assert.That(portuguese.GetEntry(key).LocalizedValue, Is.Not.Null.And.Not.Empty, $"Blank pt-BR value: {key}");
            }

            var englishExpansion = english.GetEntry("foundation.status.expansion").LocalizedValue;
            var portugueseExpansion = portuguese.GetEntry("foundation.status.expansion").LocalizedValue;
            Assert.That(portugueseExpansion.Length, Is.GreaterThanOrEqualTo((int)Math.Ceiling(englishExpansion.Length * 1.35d)));
        }

        [Test]
        public void MissingLocalizationEntryUsesSafeFallback()
        {
            var english = AssetDatabase.LoadAssetAtPath<StringTable>(TablePath + "Startup-en-US.asset");

            Assert.That(StartupController.ResolveEntry(null, "foundation.status", "Safe fallback"), Is.EqualTo("Safe fallback"));
            Assert.That(StartupController.ResolveEntry(english, "foundation.missing", "Safe fallback"), Is.EqualTo("Safe fallback"));
        }
    }
}
