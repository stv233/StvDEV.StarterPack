using StvDEV.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace StvDEV.Components.Localization
{
    [RequireComponent(typeof(TMP_Text)), AddComponentMenu("StvDEV/Localization/Localized Text")]
    public class LocalizedText : MonoBehaviour
    {
        /// <summary>
        /// Text localization variant.
        /// </summary>
        [Serializable]
        private struct Localization
        {
            [Header("Language")]
            [Tooltip("Language identifier.")]
            [SerializeField] private string _language;

            [Header("Content")]
            [Tooltip("Localized text.")]
            [SerializeField] private string _text;

            /// <summary>
            /// Language identifier.
            /// </summary>
            public string Language => _language;
        
            /// <summary>
            /// Localized text.
            /// </summary>
            public string Text => _text;
        }

        private static PrefsValue<string> _selectedLanguage = new PrefsValue<string>("StvDEV/Localization/Language", "RU-ru");
    
        /// <summary>
        /// Current language.
        /// </summary>
        public static string Language
        {
            get => _selectedLanguage.Value;
            set => _selectedLanguage.Value = value;
        }

        [Header("Localization")]
        [Tooltip("Localized text variants.")]
        private List<Localization> _localizations;

        private void Start()
        {
            TMP_Text text = GetComponent<TMP_Text>();

            Dictionary<string, string> localizations = _localizations.ToDictionary(x => x.Language, x => x.Text);

            if (localizations.ContainsKey(Language))
            {
                text.SetText(localizations[Language]);
            }
        }
    }
}
