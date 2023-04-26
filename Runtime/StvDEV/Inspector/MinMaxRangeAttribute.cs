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
        private float? max;
        private float min;

        /// <summary>
        /// Maximum slider value.
        /// </summary>
        public float? Maximum => max;

        /// <summary>
        /// Minimum slider value.
        /// </summary>
        public float Minimum => min;

        /// <summary>
        /// Draws a two-sided slider in inspector.
        /// </summary>
        public MinMaxRangeAttribute() { }

        /// <summary>
        /// Draws a two-sided slider in ins
        /// </summary>
        /// <param name="max">Maximum value</param>
        /// <param name="min">Minimum value</param>
        public MinMaxRangeAttribute(float max, float min = 0)
        {
            this.max = max;
            this.min = min;
        }
    }
}
