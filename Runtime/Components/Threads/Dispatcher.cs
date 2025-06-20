using StvDEV.Patterns;
using System;
using System.Collections.Concurrent;
using System.Threading;
using UnityEngine;

namespace StvDEV.Components.Threads
{
    /// <summary>
    /// Component for control & communicate with Unity main thread.
    /// </summary>
    [AddComponentMenu("StvDEV/Threads/Dispatcher")]
    [HelpURL("https://docs.stvdev.pro/StvDEV/Components/Threads/Dispatcher/index.html")]
    [DefaultExecutionOrder(-16000)]
    public class Dispatcher : MonoBehaviourSingleton<Dispatcher>
    {
        private ConcurrentQueue<Action> _actions = new ConcurrentQueue<Action>();
        private Thread _mainThread;

        /// <summary>
        /// Returns whether the current execution thread is the main unity execution thread.
        /// </summary>
        /// <returns>True - if the current thread is the main Unity thread, otherwise False</returns>
        public static bool InvokeRequired => Instance._mainThread.Equals(Thread.CurrentThread);

        protected override void AwakeSingletone()
        {
            _mainThread = Thread.CurrentThread;
            base.AwakeSingletone();
        }

        void Update()
        {
            while (_actions.Count > 0)
            {
                if (_actions.TryDequeue(out Action action))
                {
                    action.Invoke();
                }
            }
        }

        /// <summary>
        /// Send action to main unity thread.
        /// </summary>
        /// <param name="action">Action</param>
        public static void Invoke(Action action)
        {
            Instance._actions.Enqueue(action);
        }
    }
}
