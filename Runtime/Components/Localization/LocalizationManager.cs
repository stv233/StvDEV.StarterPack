using StvDEV.Patterns;
using StvDEV.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace StvDEV.Components.Localization
{
    /// <summary>
    /// Data storage for a localized component.
    /// </summary>
    public interface ILocalizationData
    {
        /// <summary>
        /// Language identifier.
        /// </summary>
        public string Language { get; }
    }

    /// <summary>
    /// Localization management component.
    /// </summary>
    [AddComponentMenu("StvDEV/Localization/Localization Manager")]
    [HelpURL("https://docs.stvdev.pro/StvDEV/Components/Localization/LocalizationManager/index.html")]
    public class LocalizationManager : MonoBehaviourSingleton<LocalizationManager>
    {
        private static string _language = "ru-RU";
        private static UnityEvent<string> _languageChanged = new UnityEvent<string>();

        /// <summary>
        /// Gets or sets the current language.
        /// </summary>
        public static string Language
        {
            get => _language;
            set
            {
                _language = value;
                _languageChanged?.Invoke(value);
            }
        }

        /// <summary>
        /// On dynamic language changed.
        /// </summary>
        public static UnityEvent<string> LanguageChanged => _languageChanged;

    }
}
