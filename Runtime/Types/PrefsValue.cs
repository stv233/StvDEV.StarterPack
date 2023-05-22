using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace StvDEV.Types
{
    /// <summary>
    /// Class for the stored value.
    /// </summary>
    /// <typeparam name="T">Type of value</typeparam>
    [MovedFrom("StvDEV.StarterPack")]
    public class PrefsValue<T>
    {
        private readonly string _prefsName;
        private readonly T _defaultValue;

        /// <summary>
        /// Value.
        /// </summary>
        public T Value
        {
            get
            {
                return GetValue();
            }
            set
            {
                SetValue(value);
            }
        }

        /// <summary>
        /// Class for the stored value.
        /// </summary>
        /// <param name="prefsName">Name of stored value</param>
        /// <param name="defaultValue">Default value</param>
        public PrefsValue(string prefsName, T defaultValue)
        {
            _prefsName = prefsName;
            _defaultValue = defaultValue;
        }

        /// <summary>
        /// Set value.
        /// </summary>
        /// <param name="value">Value</param>
        private void SetValue(T value)
        {
            switch (value)
            {
                case int i:
                    PlayerPrefs.SetInt(_prefsName, i);
                    break;
                case float f:
                    PlayerPrefs.SetFloat(_prefsName, f);
                    break;
                case string s:
                    PlayerPrefs.SetString(_prefsName, s);
                    break;
                case bool b:
                    PlayerPrefs.SetString(_prefsName, b.ToString());
                    break;
                default:
                    PlayerPrefs.SetString(_prefsName, JsonConvert.SerializeObject(value));
                    break;
            }
        }

        /// <summary>
        /// Get value.
        /// </summary>
        /// <returns>Value</returns>
        private T GetValue()
        {
            if (typeof(T) == typeof(int))
            {
                return (T)(object)PlayerPrefs.GetInt(_prefsName, (int)(object)_defaultValue);
            }
            else if (typeof(T) == typeof(float))
            {
                return (T)(object)PlayerPrefs.GetFloat(_prefsName, (float)(object)_defaultValue);
            }
            else if (typeof(T) == typeof(string))
            {
                return (T)(object)PlayerPrefs.GetString(_prefsName, (string)(object)_defaultValue);
            }
            else if (typeof(T) == typeof(bool))
            {
                return (T)(object)bool.Parse(PlayerPrefs.GetString(_prefsName, _defaultValue.ToString()));
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(PlayerPrefs.GetString(_prefsName,JsonConvert.SerializeObject(_defaultValue)));
            }
        }
    }
}
