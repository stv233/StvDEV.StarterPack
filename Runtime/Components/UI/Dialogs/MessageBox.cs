using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StvDEV.Components.UI.Dialogs
{
    /// <summary>
    /// Gloabal message box.
    /// </summary>
    [AddComponentMenu("StvDEV/UI/Dialogs/Message Box")]
    [HelpURL("https://docs.stvdev.pro/StvDEV/Components/UI/Dialogs/MessageBox/index.html")]
    public class MessageBox : Dialog<MessageBox>
    {
        /// <summary>
        /// Buttons for message box.
        /// </summary>
        [Flags]
        public enum MessageBoxButtons
        {
            /// <summary>
            /// Ok button.
            /// </summary>
            Ok = 1,
            /// <summary>
            /// Yes button.
            /// </summary>
            Yes = 2,
            /// <summary>
            /// No button.
            /// </summary>
            No = 4,
            /// <summary>
            /// Cancel button.
            /// </summary>
            Cancel = 8
        }

        [Header("Text")]
        [Tooltip("Dialog text component.")]
        [SerializeField] private TMP_Text _text;
        
        [Header("Buttons")]
        [Header("Ok")]
        [Tooltip("Dialog OK button.")]
        [SerializeField] private Button _ok;

        [Header("Yes/No")]
        [Tooltip("Dialog YES button.")]
        [SerializeField] private Button _yes;
        [Tooltip("Dialog NO button.")]
        [SerializeField] private Button _no;

        [Header("Cancel")]
        [Tooltip("Dialog CANCEL button.")]
        [SerializeField] private Button _cancel;

        /// <summary>
        /// Current message.
        /// </summary>
        public string Message => _text.text;

        protected override void AwakeSingleton()
        {
            _ok.onClick.AddListener(() =>
            {
                Hide(DialogResult.Ok);
            });
            _yes.onClick.AddListener(() =>
            {
                Hide(DialogResult.Yes);
            });
            _no.onClick.AddListener(() =>
            {
                Hide(DialogResult.No);
            });
            _cancel.onClick.AddListener(() =>
            {
                Hide(DialogResult.Cancel);
            });
            Close();
            base.AwakeSingleton();
        }

        private void ShowButtons(MessageBoxButtons buttons)
        {
            _ok.gameObject.SetActive(buttons.HasFlag(MessageBoxButtons.Ok));
            _yes.gameObject.SetActive(buttons.HasFlag(MessageBoxButtons.Yes));
            _no.gameObject.SetActive(buttons.HasFlag(MessageBoxButtons.No));
            _cancel.gameObject.SetActive(buttons.HasFlag(MessageBoxButtons.Cancel));
        }

        /// <summary>
        /// Show message box with spesifed message.
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="buttons">Buttons</param>
        public static void Show(string message, MessageBoxButtons buttons = MessageBoxButtons.Ok)
        {
            ShowDialog(message, buttons, null);
        }

        /// <summary>
        /// Show message box with callback after closed.
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="buttons">Buttons</param>
        /// <param name="callback">Dialog close callback</param>
        public static void ShowDialog(string message, MessageBoxButtons buttons = MessageBoxButtons.Ok, Action<DialogResult> callback = null)
        {
            Instance.ShowButtons(buttons);
            Instance._text.text = message;
            ShowDialog(callback);
        }

        /// <summary>
        /// Hide dialog.
        /// </summary>
        /// <param name="dialogResult">Result from which the dialog was closed</param>
        public new static void Close(DialogResult dialogResult = DialogResult.No)
        {
            Instance.Hide(dialogResult);
        }

    }
}
