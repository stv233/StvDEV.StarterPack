using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StvDEV.Inspector
{
    /// <summary>
    /// Draws a two-sided slider in inspector.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
       AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
    public class MinMaxRangeAttribute : PropertyAttribute
    {
        private float? _maximum;
        private float _minimum;

        /// <summary>
        /// Maximum slider value.
        /// </summary>
        public float? Maximum => _maximum;

        /// <summary>
        /// Minimum slider value.
        /// </summary>
        public float Minimum => _minimum;

        /// <summary>
        /// Draws a two-sided slider in inspector.
        /// </summary>
        public MinMaxRangeAttribute() { }

        /// <summary>
        /// Draws a two-sided slider in ins
        /// </summary>
        /// <param name="maximum">Maximum value</param>
        /// <param name="minimum">Minimum value</param>
        public MinMaxRangeAttribute(float maximum, float minimum = 0)
        {
            this._maximum = maximum;
            this._minimum = minimum;
        }
    }
}
