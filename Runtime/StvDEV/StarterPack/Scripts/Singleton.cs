using System.Reflection;
using System;

namespace StvDEV.StarterPack
{
    /// <summary>
    /// Singleton.
    /// </summary>
    /// <typeparam name="T">Type of Singleton</typeparam>
    public class Singleton<T> where T : class
    {
        private static T instance;

        protected Singleton() { }
        
        /// <summary>
        /// Instance.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = CreateInstance();
                }

                return instance;
            }
        }

        /// <summary>
        /// Create instance of type.
        /// </summary>
        /// <returns>New instance of type</returns>
        private static T CreateInstance()
        {
            ConstructorInfo cInfo = typeof(T).GetConstructor(
                BindingFlags.Instance | BindingFlags.NonPublic,
                null,
                new Type[0],
                new ParameterModifier[0]);

           if (cInfo == null)
            {
                return (T)Activator.CreateInstance(typeof(T));
            }

            return (T)cInfo.Invoke(null);
        }
    }
}
