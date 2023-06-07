using UnityEngine;

namespace StvDEV.Extensions
{
    /// <summary>
    /// Represents extensions for colors.
    /// </summary>
    public static class ColorsExtensions
    {
        /// <summary>
        /// Returns the color as a hexadecimal string in the format "RRGGBBAA"
        /// </summary>
        /// <param name="color">Color</param>
        /// <returns>Hexadecimal string in the format "RRGGBBAA"</returns>
        public static string ToHTMLString(this Color color)
        {
            return ColorUtility.ToHtmlStringRGBA(color);
        }

        /// <summary>
        /// Returns the color as a hexadecimal string in the format "#RRGGBBAA"
        /// </summary>
        /// <param name="color">Color</param>
        /// <returns>Hexadecimal string in the format "#RRGGBBAA"</returns>
        public static string ToHTML(this Color color)
        {
            return $"#{color.ToHTMLString()}";
        }

        /// <summary>
        /// Returns a string with an opening RTF color tag.
        /// </summary>
        /// <param name="color">Color</param>
        /// <returns>Opening RTF color tag</returns>
        public static string ToTag(this Color color)
        {
            return $"<color={color.ToHTML()}>";
        }

    }
}
