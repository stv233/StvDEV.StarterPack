using StvDEV.Patterns;
using StvDEV.Types;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StvDEV.Components.Localization
{
    [AddComponentMenu("StvDEV/Localization/Localization Manager")]
    public class LocalizationManager : MonoBehaviourSingleton<LocalizationManager>
    {
        private static PrefsValue<string> _savedLanguage = new PrefsValue<string>("StvDEV/Localization/Language", "ru-RU");
        private static bool _forceLanguage = false;
        private static string _forcedLanguage = "ru-RU";

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
        /// Sets and returns the language saved in the settings.
        /// </summary>
        public string SavedLanguage
        {
            get => _savedLanguage.Value;
            set => _savedLanguage.Value = value;
        }

        /// <summary>
        /// Use the explicitly specified language instead of the one saved in the settings.
        /// </summary>
        public bool UseForcedLanguage
        {
            get => _forceLanguage;
            set => _forceLanguage = value;
        }

        /// <summary>
        /// Sets the explicitly specified language to be used in the current session instead of the language saved in the settings.
        /// </summary>
        public string ForcedLanguage
        {
            set
            {
                UseForcedLanguage = true;
                _forcedLanguage = value;
            }
            get
            {
                return _forcedLanguage;
            }
        }
    }
}
