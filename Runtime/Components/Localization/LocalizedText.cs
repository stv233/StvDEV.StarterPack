using StvDEV.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace StvDEV.Components.Localization
{
    /// <summary>
    /// Component to localize text.
    /// </summary>
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
            [Multiline(3), Tooltip("Localized text.")]
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

        [Header("Localization")]
        [Tooltip("Localized text variants.")]
        [SerializeField] private List<Localization> _localizations;

        private void Start()
        {
            TMP_Text text = GetComponent<TMP_Text>();

            Dictionary<string, string> localizations = _localizations.ToDictionary(x => x.Language, x => x.Text);

            if (localizations.ContainsKey(LocalizationManager.Language))
            {
                text.SetText(localizations[LocalizationManager.Language]);
            }
        }
    }
}
