using StvDEV.Patterns;
using StvDEV.Types;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace StvDEV.Components.Localization
{
    [AddComponentMenu("StvDEV/Localization/Localization Manager")]
    public class LocalizationManager : MonoBehaviourSingleton<LocalizationManager>
    {
        private static PrefsValue<string> _savedLanguage = new PrefsValue<string>("StvDEV/Localization/Language", "ru-RU");
        private static bool _forceLanguage = false;
        private static string _forcedLanguage = "ru-RU";
        private static UnityEvent<string> _languageChanged = new UnityEvent<string>();

        /// <summary>
        /// Dynamically returns the current language used, depending on the settings.
        /// </summary>
        public static string Language
        {
            get
            {
                if (_forceLanguage)
                {
                    return _forcedLanguage;
                }
                else
                {
                    return _savedLanguage.Value;
                }

            }
        }

        /// <summary>
        /// On dynamic language changed.
        /// </summary>
        public static UnityEvent<string> LanguageChanged => _languageChanged;

        /// <summary>
        /// Sets and returns the language saved in the settings.
        /// </summary>
        public string SavedLanguage
        {
            get => _savedLanguage.Value;
            set
            {
                _savedLanguage.Value = value;
                if (!UseForcedLanguage)
                {
                    _languageChanged?.Invoke(value);
                }
            }
        }

        /// <summary>
        /// Use the explicitly specified language instead of the one saved in the settings.
        /// </summary>
        public bool UseForcedLanguage
        {
            get => _forceLanguage;
            set
            {
                _forceLanguage = value;
                if (value)
                {
                    _languageChanged?.Invoke(_forcedLanguage);
                }
                else
                {
                    _languageChanged?.Invoke(_savedLanguage.Value);
                }
            }
        }

        /// <summary>
        /// Sets the explicitly specified language to be used in the current session instead of the language saved in the settings.
        /// </summary>
        public string ForcedLanguage
        {
            get => _forcedLanguage;
            set
            {
                _forceLanguage = true;
                _forcedLanguage = value;
                _languageChanged?.Invoke(value);
            }
        }
    }
}
