using System;
using UnityEngine;

namespace StvDEV.Inspector
{
    /// <summary>
    /// Draws the int value as a drop-down list with captions.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
       AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
    public class EnumAttribute : PropertyAttribute
    {
        private string[] _captions = new string[0];

        /// <summary>
        /// Captions.
        /// </summary>
        public string[] Captions => _captions;

        /// <summary>
        /// Draws the int value as a drop-down list with captions.
        /// </summary>
        /// <param name="captions">Captions</param>
        public EnumAttribute(params string[] captions)
        {
            _captions = captions;
        }
    }
}
