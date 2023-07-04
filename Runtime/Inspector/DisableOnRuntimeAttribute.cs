using UnityEngine;
using System;

namespace StvDEV.Inspector
{
    /// <summary>
    /// Disables the display of a field in the inspector by when application running.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property |
        AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
    public class DisableOnRuntimeAttribute : PropertyAttribute { }
}