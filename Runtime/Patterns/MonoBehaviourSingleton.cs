using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace StvDEV.Patterns
{
    /// <summary>
    /// Singleton for MonoBehaviour.
    /// </summary>
    /// <typeparam name="T">MonoBehaviour</typeparam>
    [MovedFrom("StvDEV.StarterPack")]
    public class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviourSingleton<T>
    {
        private static T _instance;

        /// <summary>
        /// Instance.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = FindObjectOfType<T>(true);
                    if (!_instance)
                    {
                        Debug.LogError($"[{nameof(MonoBehaviourSingleton<T>)}] Singleton of type {typeof(T)} not contains in scene");
                        return null;
                    }

                    _instance.AwakeSingleton();
                }

                return _instance;
            }
        }

        /// <summary>
        /// Has instance.
        /// </summary>
        public static bool InstanceIsNotNull => _instance;

        /// <summary>
        /// Awakes this instance.
        /// </summary>
        protected void Awake()
        {
            if (!_instance)
            {
                _instance = GetComponent<T>();
                AwakeSingleton();
            }
            else if (_instance != this)
            {
                Debug.LogError($"[{nameof(MonoBehaviourSingleton<T>)}] Dublicated singleton instance {nameof(T)}", this);
            }
        }

        /// <summary>
        /// On Awake.
        /// </summary>
        protected virtual void AwakeSingleton() { }

    }
}