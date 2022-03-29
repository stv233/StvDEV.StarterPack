using System.Reflection;
using UnityEngine;

namespace StvDEV.StarterPack
{
    /// <summary>
    /// SO for storing global settings.
    /// </summary>
    public class Settings : ScriptableObject
    {
        /// <summary>
        /// Gets the value of the setting by its name.
        /// </summary>
        /// <param name="name">Setting name</param>
        /// <returns>Setting value</returns>
        public object GetSettingByName(string name)
        {
            return GetType().GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).GetValue(this);
        }

        /// <summary>
        /// Sets the setting value by name
        /// </summary>
        /// <param name="name">Setting name</param>
        /// <param name="value">Setting value</param>
        public void SetSettingByName(string name, object value)
        {
            GetType().GetField(name).SetValue(this, value);
        }
    }
}

