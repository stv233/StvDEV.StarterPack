using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StvDEV.Components.UI.Buttons
{
    /// <summary>
    /// Button with customizable text.
    /// </summary>
    [AddComponentMenu("StvDEV/UI/Buttons/Text Button")]
    [HelpURL("https://docs.stvdev.pro/StvDEV/Components/UI/Buttons/TextButton/index.html")]
    public class TextButton : Button
    {
        [Header("Settings")]
        [Tooltip("Button text.")]
        [SerializeField] private TMP_Text _text;

        protected override void Awake()
        {
            InitializeText();
        }

        /// <summary>
        /// Button text.
        /// </summary>
        public string Text
        {
            get => _text ? _text.text : string.Empty;
            set
            {
                if (!_text)
                {
                    InitializeText();
                }
                _text.text = value;
            }
        }
        private void InitializeText()
        {
            if (!_text)
            {
                _text = GetComponentInChildren<TMP_Text>();
                if (_text == null)
                {
                    if (!TryGetComponent(out _text))
                    {
                        _text = gameObject.AddComponent<TMP_Text>();
                    }
                }
            }
        }
    }
}
