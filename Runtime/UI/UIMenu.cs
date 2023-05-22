using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Scripting.APIUpdating;

namespace StvDEV.UI
{
    /// <summary>
    /// UI Menu basic component.
    /// </summary>
    [MovedFrom(true, "StvDEV.StarterPack", "StvDEV.StarterPack", "GUIMenu")]
    public class UIMenu : MonoBehaviour 
    {
        [Header("Events")]
        [SerializeField] private UnityEvent _opened;
        [SerializeField] private UnityEvent _closed;

        /// <summary>
        /// Called when the menu is opened.
        /// </summary>
        public UnityEvent Opened => _opened;

        /// <summary>
        /// Called when the menu is closed.
        /// </summary>
        public UnityEvent Closed => _closed;

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
            OnOpen();
            _opened?.Invoke();
        }

        /// <summary>
        /// Close menu.
        /// </summary>
        public void Close()
        {
            gameObject.SetActive(false);
            OnClose();
            _closed?.Invoke();
        }

        /// <summary>
        /// Defines the actions that occur when the window is opened.
        /// </summary>
        protected virtual void OnOpen() { }

        /// <summary>
        /// Defines the actions that occur when the window is closed.
        /// </summary>
        protected virtual void OnClose() { }
    }
}
