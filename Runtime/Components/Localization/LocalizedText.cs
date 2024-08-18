using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace StvDEV.Components.Localization
{
    /// <summary>
    /// Text localization variant.
    /// </summary>
    [Serializable]
    public struct TextLocalizationData : ILocalizationData
    {
        [Header("Language")]
        [Tooltip("Language identifier.")]
        [SerializeField] private string _language;

        [Header("Content")]
        [Multiline(3), Tooltip("Localized text.")]
        [SerializeField] private string _text;

        /// <summary>
        /// Text localization variant.
        /// </summary>
        /// <param name="language">Localization language</param>
        /// <param name="text">Localized text</param>
        public TextLocalizationData(string language, string text)
        {
            _language = language;
            _text = text;
        }

        /// <summary>
        /// Language identifier.
        /// </summary>
        public string Language => _language;

        /// <summary>
        /// Localized text.
        /// </summary>
        public string Text => _text;
    }

    /// <summary>
    /// Component to localize text.
    /// </summary>
    [RequireComponent(typeof(TMP_Text))]
    [AddComponentMenu("StvDEV/Localization/Localized Text")]
    [HelpURL("https://docs.stvdev.pro/StvDEV/Components/Localization/LocalizedText/index.html")]
    public class LocalizedText : LocalizedComponent
    {

        [Header("Localization")]
        [Tooltip("Localized text variants.")]
        [SerializeField] private List<TextLocalizationData> _localizations;

        private void Start()
        {
            LocalizationManager.LanguageChanged.AddListener(SetLanguage);
            SetLanguage(LocalizationManager.Language);
        }

        private void OnDisable()
        {
            LocalizationManager.LanguageChanged.RemoveListener(SetLanguage);
        }

        /// <summary>
        /// Set localize text by language.
        /// </summary>
        /// <param name="language">Language</param>
        public override void SetLanguage(string language)
        {
            TMP_Text text = GetComponent<TMP_Text>();

            Dictionary<string, string> localizations = _localizations.ToDictionary(x => x.Language, x => x.Text);

            if (localizations.ContainsKey(language))
            {
                text.SetText(localizations[language]);
            }
        }
    }
}
