using UnityEngine;
using UnityEngine.UI;

namespace StvDEV.Components.UI.Fields
{
    /// <summary>
    /// Field with checkbox.
    /// </summary>
    [AddComponentMenu("StvDEV/UI/Fields/Chackbox Field")]
    public class CheckboxField : Field<bool>
    {
        [Header("Checkbox")]
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

        private void Awake()
        {
            _toggle.onValueChanged.AddListener(value =>
            {
                ValueChanged?.Invoke(value);
            });
        }

    }
}
