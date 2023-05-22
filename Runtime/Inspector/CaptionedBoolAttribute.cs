using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StvDEV.Inspector
{
    /// <summary>
    /// Draws the bool value as a drop-down list with a signed selection.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
       AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
    public class CaptionedBoolAttribute : PropertyAttribute
    {
        private string _true;
        private string _false;

        /// <summary>
        /// True annotation.
        /// </summary>
        public string True => _true;

        /// <summary>
        /// False annotation.
        /// </summary>
        public string False => _false;

        /// <summary>
        /// Draws the bool value as a drop-down list with a signed selection.
        /// </summary>
        /// <param name="trueAnnotation">True annotation</param>
        /// <param name="falseAnnotation">False annotation</param>
        public CaptionedBoolAttribute(string trueAnnotation = "Enabled", string falseAnnotation = "Disabled")
        {
            _true = trueAnnotation;
            _false = falseAnnotation;
        }
    }
}
