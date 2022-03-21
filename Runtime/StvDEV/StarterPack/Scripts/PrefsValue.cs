using UnityEngine;

namespace StvDEV.StarterPack
{
    /// <summary>
    /// Class for the stored value.
    /// </summary>
    /// <typeparam name="T">Type of value</typeparam>
    public class PrefsValue<T>
    {
        private readonly string prefsName;
        private readonly T defaultValue;

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
            this.prefsName = prefsName;
            this.defaultValue = defaultValue;
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
                    PlayerPrefs.SetInt(prefsName, i);
                    break;
                case float f:
                    PlayerPrefs.SetFloat(prefsName, f);
                    break;
                case string s:
                    PlayerPrefs.SetString(prefsName, s);
                    break;
                default:
                    PlayerPrefs.SetString(prefsName, JsonUtility.ToJson(value));
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
                return (T)(object)PlayerPrefs.GetInt(prefsName, (int)(object)defaultValue);
            }
            else if (typeof(T) == typeof(float))
            {
                return (T)(object)PlayerPrefs.GetFloat(prefsName, (float)(object)defaultValue);
            }
            else if (typeof(T) == typeof(string))
            {
                return (T)(object)PlayerPrefs.GetString(prefsName, (string)(object)defaultValue);
            }
            else
            {
                return JsonUtility.FromJson<T>(PlayerPrefs.GetString(prefsName,JsonUtility.ToJson(defaultValue)));
            }
        }
    }
}
