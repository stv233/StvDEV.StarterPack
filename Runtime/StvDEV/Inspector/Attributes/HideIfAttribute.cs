using UnityEngine;
using System;

namespace StvDEV.Inspector.Attributes
{
    /// <summary>
    /// Hides or disables the display of a field in the inspector by the value of a field of type bool.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
        AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
    public class HideIfAttribute : PropertyAttribute
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

        private string conditionalSourceField = "";
        private bool hideInInspector = false;

        public string ConditionalSourceField => conditionalSourceField;
        public bool HideInInspector => hideInInspector;

        /// <summary>
        /// Hides the display of a field in the inspector by the value of a field of type bool.
        /// </summary>
        /// <param name="conditionalSourceField">Name of the field containing the condition</param>
        public HideIfAttribute(string conditionalSourceField) : this(conditionalSourceField, HideType.Hide) { }

        /// <summary>
        /// Hides or disables the display of a field in the inspector by the value of a field of type bool.
        /// </summary>
        /// <param name="conditionalSourceField">Name of the field containing the condition</param>
        /// <param name="hideType">Hide type</param>
        public HideIfAttribute(string conditionalSourceField, HideType hideType)
        {
            this.conditionalSourceField = conditionalSourceField;
            this.hideInInspector = hideType == HideType.Hide;
        }
    }
}