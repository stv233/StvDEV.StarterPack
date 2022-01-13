using System.Collections.Generic;
using UnityEngine.Events;

namespace StvDEV.StarterPack
{
    /// <summary>
    /// Abstract global event.
    /// </summary>
    public class GlobalEvent<T> : Singleton<T> where T : class
    {
#if UNITY_2020_1_OR_NEWER
        protected UnityEvent<List<object>> unityArgumentsEvent = new UnityEvent<List<object>>();
#endif
        protected UnityEvent unityEvent = new UnityEvent();

#if UNITY_2020_1_OR_NEWER
        /// <summary>
        /// Subscribe to an event with arguments.
        /// </summary>
        /// <param name="action">Event action</param>
        public virtual void Subscribe(UnityAction<List<object>> action)
        {
            unityArgumentsEvent.AddListener(action);
        }
#endif

        /// <summary>
        /// Subscribe to an event without arguments.
        /// </summary>
        /// <param name="action">Event action</param>
        public virtual void Subscribe(UnityAction action)
        {
            unityEvent.AddListener(action);
        }

#if UNITY_2020_1_OR_NEWER
        /// <summary>
        /// Unsubscribe from an event with arguments.
        /// </summary>
        /// <param name="action">Event action</param>
        public virtual void Unsubscribe(UnityAction<List<object>> action)
        {
            unityArgumentsEvent.RemoveListener(action);
        }
#endif

        /// <summary>
        /// Unsubscribe from the event without arguments.
        /// </summary>
        /// <param name="action">Event action</param>
        public virtual void Unsubscribe(UnityAction action)
        {
            unityEvent.RemoveListener(action);
        }

#if UNITY_2020_1_OR_NEWER
        /// <summary>
        /// Call an event with arguments.
        /// </summary>
        /// <param name="arguments">Arguments</param>
        public virtual void Invoke(List<object> arguments)
        {
            unityArgumentsEvent?.Invoke(arguments);
            Invoke();
        }
#endif

        /// <summary>
        /// Call an event without arguments.
        /// </summary>
        public virtual void Invoke()
        {
            unityEvent?.Invoke();
        }

        /// <summary>
        /// Unsubscribe everyone from the event.
        /// </summary>
        public virtual void Dispose()
        {
#if UNITY_2020_1_OR_NEWER
            unityArgumentsEvent.RemoveAllListeners();
#endif
            unityEvent.RemoveAllListeners();
        }
    }
}
