
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace StvDEV.Components.UI.Fields
{
    /// <summary>
    /// Key-Value pair string field.
    /// </summary>
    [AddComponentMenu("StvDEV/UI/Fields/KeyValue Field")]
    public class KeyValueField : Field<string>
    {
        [Header("Key")]
        [SerializeField] private TMP_InputField _keyInput;
        [SerializeField] private TMP_Text _keyPlaceholder;

        [Header("Value")]
        [SerializeField] private TMP_InputField _valueInput;
        [SerializeField] private TMP_Text _valuePlaceholder;

        [Header("Events")]
        [SerializeField] private UnityEvent<string> _keyChanged;

        /// <summary>
        /// Gets or sets field key.
        /// </summary>
        public string Key
        {
            get => _keyInput.text;
            set => _keyInput.text = value;
        }

        /// <summary>
        /// Gets or sets key placeholder.
        /// </summary>
        public string KeyPlaceholder
        {
            get => _keyPlaceholder.text;
            set => _keyPlaceholder.text = value;
        }

        /// <summary>
        /// Gets or sets key content type.
        /// </summary>
        public TMP_InputField.ContentType KeyContentType
        {
            get => _keyInput.contentType;
            set => _keyInput.contentType = value;
        }

        public override string Value 
        {
            get => _valueInput.text; 
            set => _valueInput.text = value;
        }

        /// <summary>
        /// Gets or sets value placeholder.
        /// </summary>
        public string ValuePlaceholder
        {
            get => _valuePlaceholder.text;
            set => _valuePlaceholder.text = value;
        }

        /// <summary>
        /// Gets or sets value content type.
        /// </summary>
        public TMP_InputField.ContentType ValueContentType
        {
            get => _valueInput.contentType;
            set => _valueInput.contentType = value;
        }

        public override bool ReadOnly
        {
            get => _keyInput.readOnly;
            set
            {
                _keyInput.readOnly = value;
                _valueInput.readOnly = value;
            }
        }

        public override bool IsFocused => _keyInput.isFocused || _valueInput.isFocused;

        /// <summary>
        /// On field key changed.
        /// </summary>
        public UnityEvent<string> KeyChanged => _keyChanged;

        private void Awake()
        {
            _keyInput.onValueChanged.AddListener(value => KeyChanged?.Invoke(value));
            _valueInput.onValueChanged.AddListener(value => ValueChanged?.Invoke(value));
        }

    }
}
