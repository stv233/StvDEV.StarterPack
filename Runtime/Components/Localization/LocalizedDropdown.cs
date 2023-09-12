using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace StvDEV.Components.Localization
{
    /// <summary>
    /// Component to localize dropdown.
    /// </summary>
    [RequireComponent(typeof(TMP_Dropdown)), AddComponentMenu("StvDEV/Localization/Localized Dropdown")]
    public class LocalizedDropdown : LocalizedComponent
    {
        /// <summary>
        /// Dropdown localization variant.
        /// </summary>
        [Serializable]
        private struct Localization : ILocalizationData
        {
            [Header("Language")]
            [Tooltip("Language identifier.")]
            [SerializeField] private string _language;

            [Header("Content")]
            [Tooltip("Localized options.")]
            [SerializeField] private List<string> _options;

            /// <summary>
            /// Language identifier.
            /// </summary>
            public string Language => _language;

            /// <summary>
            /// Localized options.
            /// </summary>
            public List<string> Options => _options;
        }

        [Header("Localization")]
        [Tooltip("Localized text variants.")]
        [SerializeField] private List<Localization> _localizations;

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
        /// Update localized text by language.
        /// </summary>
        /// <param name="language">Language</param>
        public override void SetLanguage(string language)
        {
            TMP_Dropdown dropdown = GetComponent<TMP_Dropdown>();

            Dictionary<string, List<string>> localizations = _localizations.ToDictionary(x => x.Language, x => x.Options);

            if (localizations.ContainsKey(language))
            {
                List<string> options = localizations[language];
                for (var i = 0; i < dropdown.options.Count; i++)
                {
                    if (i < options.Count)
                    {
                        dropdown.options[i].text = options[i];
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }
    }
}
