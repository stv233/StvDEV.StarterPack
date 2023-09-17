using UnityEngine;
using System;

namespace StvDEV.Inspector
{
    /// <summary>
    /// Disables the a field in the inspector.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
        AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
    public class ReadOnlyAttribute : PropertyAttribute { }
}