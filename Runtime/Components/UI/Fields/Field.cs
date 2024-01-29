using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace StvDEV.Components.UI.Fields
{
    /// <summary>
    /// Basic field component.
    /// </summary>
    /// <typeparam name="T">Field value type</typeparam>
    public abstract class Field<T> : MonoBehaviour
    {
        [Header("Label")]
        [Tooltip("Field label text.")]
        [SerializeField] protected TMP_Text _label;

        [Header("Tip")]
        [Tooltip("Field tip text.")]
        [SerializeField] private TMP_Text _tip;

        [Header("Events")]
        [Tooltip("On field value changed.")]
        [SerializeField] private UnityEvent<T> _valueChanged;

        /// <summary>
        /// Gets or sets field label.
        /// </summary>
        public virtual string Label
        {
            get => _label.text;
            set => _label.text = value;
        }

        /// <summary>
        /// Gets or sets field value.
        /// </summary>
        public abstract T Value { get; set; }

        /// <summary>
        /// Gets or sets readonly field parameter.
        /// </summary>
        public abstract bool ReadOnly { get; set; }

        /// <summary>
        /// Gets that field is focused now.
        /// </summary>
        public abstract bool IsFocused { get; }

        /// <summary>
        /// On field value changed.
        /// </summary>
        public UnityEvent<T> ValueChanged => _valueChanged;

        /// <summary>
        /// Show tip for field.
        /// </summary>
        /// <param name="tip">Tip text</param>
        public virtual void ShowTip(string tip)
        {
            _tip.text = tip;
        }

        /// <summary>
        /// Hide field tip.
        /// </summary>
        public virtual void HideTip()
        {
            ShowTip(string.Empty);
        }
    }
}
