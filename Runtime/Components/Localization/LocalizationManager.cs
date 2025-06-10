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
        private static string s_language = "en-US";
        private static UnityEvent<string> s_languageChanged = new UnityEvent<string>();

        /// <summary>
        /// Gets or sets the current language.
        /// </summary>
        public static string Language
        {
            get => s_language;
            set
            {
                s_language = value;
                s_languageChanged?.Invoke(value);
            }
        }

        /// <summary>
        /// On dynamic language changed.
        /// </summary>
        public static UnityEvent<string> LanguageChanged => s_languageChanged;

    }
}
