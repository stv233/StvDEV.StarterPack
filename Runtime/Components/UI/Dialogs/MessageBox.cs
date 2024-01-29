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
        public enum MessageBoxButtons
        {
            Ok,
            YesNo,
            YesNoCancel,
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

        protected override void AwakeSingletone()
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
            base.AwakeSingletone();
        }

        private void ShowButtons(MessageBoxButtons buttons)
        {
            HideAllButtons();
            switch (buttons)
            {
                case MessageBoxButtons.YesNo:
                    _yes.gameObject.SetActive(true);
                    _no.gameObject.SetActive(true);
                    break;
                case MessageBoxButtons.YesNoCancel:
                    _yes.gameObject.SetActive(true);
                    _no.gameObject.SetActive(true);
                    _cancel.gameObject.SetActive(true);
                    break;
                default:
                    _ok.gameObject.SetActive(true);
                    break;
            }
        }

        private void HideAllButtons()
        {
            _ok.gameObject.SetActive(false);
            _yes.gameObject.SetActive(false);
            _no.gameObject.SetActive(false);
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
