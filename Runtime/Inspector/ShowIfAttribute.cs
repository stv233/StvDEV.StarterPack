using UnityEngine;
using System;

namespace StvDEV.Inspector
{
    /// <summary>
    /// Hides or disables the display of a field in the inspector by the value of a field of type bool.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
        AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
    public class ShowIfAttribute : PropertyAttribute
    {
        /// <summary>
        /// Types of interaction on the target field.
        /// </summary>
        public enum HideType
        {
            /// <summary>
            /// Hide field.
            /// </summary>
            Hide,

            /// <summary>
            /// Disable field.
            /// </summary>
            Disable
        }

        private string _conditionalSourceField = "";
        private bool _hideInInspector = false;
        private bool _byBool = false;
        private bool _inverse = false;
        private bool _value = false; 

        /// <summary>
        /// Gets the value of the conditional source field.
        /// </summary>
        public string ConditionalSourceField => _conditionalSourceField;

        /// <summary>
        /// Gets the value of the hide in inspector.
        /// </summary>
        public bool HideInInspector => _hideInInspector;

        /// <summary>
        /// Gets the value of the by value.
        /// </summary>
        public bool ByValue => _byBool;

        /// <summary>
        /// Inverse value in check.
        /// </summary>
        public bool Inverse => _inverse;

        /// <summary>
        /// Gets the value of the value.
        /// </summary>
        public bool Value => _value;  

        /// <summary>
        /// Hides the display of a field in the inspector by the value of a field of type bool.
        /// </summary>
        /// <param name="conditionalSourceField">Name of the field containing the condition</param>
        public ShowIfAttribute(string conditionalSourceField) : this(conditionalSourceField, false, HideType.Hide) { }

        /// <summary>
        /// Hides the display of a field in the inspector by the value of a field of type bool.
        /// </summary>
        /// <param name="conditionalSourceField">Name of the field containing the condition</param>
        /// <param name="inverseFieldValue">Inverse field value on check</param>
        public ShowIfAttribute(string conditionalSourceField, bool inverseFieldValue) : this(conditionalSourceField, inverseFieldValue, HideType.Hide) { }

        /// <summary>
        /// Hides the display of a field in the inspector by the value of a field of type bool.
        /// </summary>
        /// <param name="conditionalSourceField">Name of the field containing the condition</param>
        /// <param name="hideType">Hide type</param>
        public ShowIfAttribute(string conditionalSourceField, HideType hideType) : this(conditionalSourceField, false, hideType) { }

        /// <summary>
        /// Hides or disables the display of a field in the inspector by the value of a field of type bool.
        /// </summary>
        /// <param name="conditionalSourceField">Name of the field containing the condition</param>
        /// <param name="inverseFieldValue">Inverse field value on check</param>
        /// <param name="hideType">Hide type</param>
        public ShowIfAttribute(string conditionalSourceField, bool inverseFieldValue, HideType hideType)
        {
            _conditionalSourceField = conditionalSourceField;
            _inverse = inverseFieldValue;
            _hideInInspector = hideType == HideType.Hide;
        }

        /// <summary>
        /// Hides or disables the display of a field in the inspector by predicate.
        /// </summary>
        /// <param name="predicate">Predicate</param>
        public ShowIfAttribute(bool value) : this(value, HideType.Hide) { }

        /// <summary>
        /// Hides or disables the display of a field in the inspector by value.
        /// </summary>
        /// <param name="value">Predicate</param>
        /// <param name="hideType">Hide Type</param>
        public ShowIfAttribute(bool value, HideType hideType)
        {
            _hideInInspector = hideType == HideType.Hide;
            _value = value;
            _byBool = true;
        }
    }
}