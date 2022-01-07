using System.Collections.Generic;
using UnityEngine.Events;

namespace StvDEV.StarterPack
{
    /// <summary>
    /// Abstract global event.
    /// </summary>
    public class GlobalEvent : Singleton<GlobalEvent>
    {
        protected UnityEvent<List<object>> unityArgumentsEvent = new UnityEvent<List<object>>();
        protected UnityEvent unityEvent = new UnityEvent();

        /// <summary>
        /// Subscribe to an event with arguments.
        /// </summary>
        /// <param name="action">Event action</param>
        public virtual void Subscribe(UnityAction<List<object>> action)
        {
            unityArgumentsEvent.AddListener(action);
        }

        /// <summary>
        /// Subscribe to an event without arguments.
        /// </summary>
        /// <param name="action">Event action</param>
        public virtual void Subscribe(UnityAction action)
        {
            unityEvent.AddListener(action);
        }

        /// <summary>
        /// Unsubscribe from an event with arguments.
        /// </summary>
        /// <param name="action">Event action</param>
        public virtual void Unsubscribe(UnityAction<List<object>> action)
        {
            unityArgumentsEvent.RemoveListener(action);
        }

        /// <summary>
        /// Unsubscribe from the event without arguments.
        /// </summary>
        /// <param name="action">Event action</param>
        public virtual void Unsubscribe(UnityAction action)
        {
            unityEvent.RemoveListener(action);
        }

        /// <summary>
        /// Call an event with arguments.
        /// </summary>
        /// <param name="arguments">Arguments</param>
        public virtual void Invoke(List<object> arguments)
        {
            unityArgumentsEvent?.Invoke(arguments);
            Invoke();
        }

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
            unityArgumentsEvent.RemoveAllListeners();
            unityEvent.RemoveAllListeners();
        }
    }
}
