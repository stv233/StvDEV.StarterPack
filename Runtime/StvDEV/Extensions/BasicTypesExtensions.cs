﻿using UnityEngine;

namespace StvDEV.Extensions
{
    /// <summary>
    /// Represents extensions for basic C# types.
    /// </summary>
    public static class BasicTypesExtensions
    {
        /// <summary>
        /// Returns a string enclosed in tags of the specified color.
        /// </summary>
        /// <param name="string">String</param>
        /// <param name="color">Color</param>
        /// <returns>String with color tags</returns>
        public static string Colorize(this string @string, Color color)
        {
            return $"{color.ToTag()}{@string}</color>";
        }

        /// <summary>
        /// Indicates whether the specified string is empty.
        /// </summary>
        /// <param name="string">String</param>
        /// <returns>True if the string is empty, otherwise false</returns>
        public static bool IsEmpty(this string @string)
        {
            return string.IsNullOrEmpty(@string);
        }
    }
}
