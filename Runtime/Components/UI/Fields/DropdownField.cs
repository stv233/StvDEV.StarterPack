using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace StvDEV.Components.UI.Fields
{
    /// <summary>
    /// String field with dropdown.
    /// </summary>
    [AddComponentMenu("StvDEV/UI/Fields/Dropdown Field")]
    [HelpURL("https://docs.stvdev.pro/StvDEV/Components/UI/Fields/DropdownField/index.html")]
    public class DropdownField : Field<string>
    {
        [Header("Dropdown")]
        [Tooltip("Field dropdown component.")]
        [SerializeField] protected TMP_Dropdown _dropdown;

        /// <summary>
        /// Gets or sets field value as dropdown option text.
        /// </summary>
        public override string Value 
        {
            get
            {
                return _dropdown.options[_dropdown.value].text;
            }
            set
            {
                TMP_Dropdown.OptionData itemToSet = _dropdown.options.Where(x => x.text == value).FirstOrDefault();
                if (itemToSet != null)
                {
                    _dropdown.value = _dropdown.options.IndexOf(itemToSet);
                }
                else
                {
                    AddOption(value);
                    _dropdown.value = _dropdown.options.Count - 1;
                }
                _dropdown.RefreshShownValue();
            }
        }

        /// <summary>
        /// Gets or sets dropdown selected option index.
        /// </summary>
        public int Selected
        {
            get => _dropdown.value;
            set => _dropdown.value = value;
        }

        /// <summary>
        /// Dropdown field options list.
        /// </summary>
        public virtual IReadOnlyList<string> Options => _dropdown.options.Select(x => x.text).ToList();

        public override bool IsReadOnly
        {
            get => _dropdown.interactable;
            set => _dropdown.interactable = value;
        }

        public override bool IsFocused => _dropdown.IsExpanded;

        protected void Awake()
        {
            _dropdown.onValueChanged.AddListener(value =>
            {
                ValueChanged?.Invoke(Value);
            });

        }

        /// <summary>
        /// Add option to dropdown field.
        /// </summary>
        /// <param name="option">String option</param>
        public virtual void AddOption(string option)
        {
            _dropdown.options.Add(new TMP_Dropdown.OptionData(option));
            _dropdown.RefreshShownValue();
        }

        /// <summary>
        /// Add options range to dropdown field.
        /// </summary>
        /// <param name="options"></param>
        public virtual void AddOptions(IEnumerable<string> options)
        {
            _dropdown.options.AddRange(options.Select(x => new TMP_Dropdown.OptionData(x)));
            _dropdown.RefreshShownValue();
        }

        /// <summary>
        /// Remove option from dropdown field.
        /// </summary>
        /// <param name="option">String option</param>
        public virtual void RemoveOption(string option)
        {
            TMP_Dropdown.OptionData optionToRemove = _dropdown.options.Where(x => x.text == option).FirstOrDefault();
            if (optionToRemove != null)
            {
                _dropdown.options.Remove(optionToRemove);
            }
            _dropdown.RefreshShownValue();
        }

        /// <summary>
        /// Clear all dropdown field options.
        /// </summary>
        public void Clear()
        {
            _dropdown.options.Clear();
            _dropdown.RefreshShownValue();
        }
    }
}
