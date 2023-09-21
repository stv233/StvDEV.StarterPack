using StvDEV.Patterns;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StvDEV.Components.UI.Dialogs
{
    /// <summary>
    /// Dialog results.
    /// </summary>
    public enum DialogResult
    {
        None,
        Ok,
        Yes,
        No,
        Cancel,
    }

    /// <summary>
    /// Dialog basic class.
    /// </summary>
    /// <typeparam name="T">Dialog singleton.</typeparam>
    public abstract class Dialog<T> : MonoBehaviourSingleton<T> where T : Dialog<T>
    {
        private Action<DialogResult> _callback;

        protected override void AwakeSingletone()
        {
            Hide(DialogResult.None);
            base.AwakeSingletone();
        }

        /// <summary>
        /// Show dialog.
        /// </summary>
        /// <param name="callback">Dialog callback</param>
        protected void Show(Action<DialogResult> callback)
        {
            _callback = callback;
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Hide dialog.
        /// </summary>
        /// <param name="result">Dialog result</param>
        protected void Hide(DialogResult result)
        {
            Action<DialogResult> callback = _callback;
            _callback = null;
            gameObject.SetActive(false);
            callback?.Invoke(result);
        }

        /// <summary>
        /// Open dialog window.
        /// </summary>
        /// <param name="callback">Dialog callback</param>
        public static void ShowDialog(Action<DialogResult> callback)
        {
            Instance.Show(callback);
        }

        /// <summary>
        /// Close dialog window.
        /// </summary>
        /// <param name="result">Dialog result</param>
        public static void Close(DialogResult result)
        {
            Instance.Hide(result);
        }

    }
}
