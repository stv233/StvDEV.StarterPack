using System;
using UnityEngine;

namespace StvDEV.StarterPack
{
    /// <summary>
    /// GUI Menu.
    /// </summary>
    public class GUIMenu : MonoBehaviour 
    {
        /// <summary>
        /// Called when the menu is opened.
        /// </summary>
        public event Action OnOpen;

        /// <summary>
        /// Called when the menu is closed.
        /// </summary>
        public event Action OnClose;

        /// <summary>
        /// Returns whether this menu is active.
        /// </summary>
        public bool IsActive => gameObject.activeInHierarchy;

        /// <summary>
        /// Open menu.
        /// </summary>
        public void Open()
        {
            gameObject.SetActive(true);
            OnOpened();
            OnOpen?.Invoke();
        }

        /// <summary>
        /// Close menu.
        /// </summary>
        public void Close()
        {
            gameObject.SetActive(false);
            OnClosed();
            OnClose?.Invoke();
        }

        /// <summary>
        /// Defines the actions that occur when the window is opened.
        /// </summary>
        protected virtual void OnOpened() { }

        /// <summary>
        /// Defines the actions that occur when the window is closed.
        /// </summary>
        protected virtual void OnClosed() { }
    }
}
