using TMPro;
using UnityEngine;

namespace StvDEV.Components.UI.Fields
{
    /// <summary>
    /// Text field.
    /// </summary>
    [AddComponentMenu("StvDEV/UI/Fields/Text Field")]
    public class TextField : Field<string>
    {
        [Header("Links")]
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private TMP_Text _placeHolder;

        public override string Value
        {
            get { return _inputField.text; }
            set { _inputField.text = value; }
        }

        /// <summary>
        /// Gets or sets field placeholder.
        /// </summary>
        public string Placeholder
        {
            get { return _placeHolder.text; }
            set { _placeHolder.text = value; } 
        }

        /// <summary>
        /// Gets  or sets field content type.
        /// </summary>
        public TMP_InputField.ContentType ContentType
        {
            get => _inputField.contentType;
            set => _inputField.contentType = value;
        }

        public override bool ReadOnly 
        { 
            get => _inputField.readOnly; 
            set => _inputField.readOnly = value; 
        }

        private void Awake()
        {
            _inputField.onValueChanged.AddListener(value =>
            {
                ValueChanged?.Invoke(value);
            });
        }

    }
}
