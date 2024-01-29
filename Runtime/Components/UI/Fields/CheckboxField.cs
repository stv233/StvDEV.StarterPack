using UnityEngine;
using UnityEngine.UI;

namespace StvDEV.Components.UI.Fields
{
    /// <summary>
    /// Field with checkbox.
    /// </summary>
    [AddComponentMenu("StvDEV/UI/Fields/Chackbox Field")]
    [HelpURL("https://docs.stvdev.pro/StvDEV/Components/UI/Fields/CheckboxField/index.html")]
    public class CheckboxField : Field<bool>
    {
        [Header("Checkbox")]
        [Tooltip("Field toggle component.")]
        [SerializeField] private Toggle _toggle;

        public override bool Value
        {
            get => _toggle.isOn;
            set => _toggle.isOn = value;
        }

        public override bool ReadOnly
        {
            get => _toggle.interactable;
            set => _toggle.interactable = value;
        }

        public override bool IsFocused => false;

        private void Awake()
        {
            _toggle.onValueChanged.AddListener(value =>
            {
                ValueChanged?.Invoke(value);
            });
        }

    }
}
