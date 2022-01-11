using UnityEngine;

namespace StvDEV.StarterPack
{
    public class Settings : ScriptableObject
    {
        public object GetSettingByName(string name)
        {
            return GetType().GetField(name).GetValue(this);
        }

        public void SetSettingByName(string name, object value)
        {
            GetType().GetField(name).SetValue(this,value);
        }
    }
}
