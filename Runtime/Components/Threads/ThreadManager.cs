using StvDEV.Patterns;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace StvDEV.Components.Threads
{
    /// <summary>
    /// Component for control & communicate with Unity main thread.
    /// </summary>
    public class ThreadManager : MonoBehaviourSingleton<ThreadManager>
    {
        private ConcurrentQueue<Action> _actions = new ConcurrentQueue<Action>();
        private Thread _mainThread;

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
        public static void ToMainThread(Action action)
        {
            Instance._actions.Enqueue(action);
        }

        /// <summary>
        /// Returns whether the current execution thread is the main unity execution thread.
        /// </summary>
        /// <returns>True - if the current thread is the main Unity thread, otherwise false</returns>
        public static bool IsMainThread()
        {
            return Instance._mainThread.Equals(System.Threading.Thread.CurrentThread);
        }
    }
}
