using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace StvDEV.Inspector
{
    /// <summary>
    /// Draws a two-sided slider for the fields marked <see cref="MinMaxRangeAttribute"/> attribute.
    /// </summary>
    [CustomPropertyDrawer(typeof(MinMaxRangeAttribute))]
    public class MinMaxRangePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect ctrlRect = EditorGUI.PrefixLabel(position, label);
            Rect[] rect = SplitRectIn3(ctrlRect, 36, 5);
            MinMaxRangeAttribute attributeObject = (MinMaxRangeAttribute)attribute;
            SerializedPropertyType type = property.propertyType;
            if (type == SerializedPropertyType.Vector2)
            {
                Vector2 vector = property.vector2Value;
                float min = vector.x;
                float to = vector.y;
                min = EditorGUI.FloatField(rect[0], min);
                to = EditorGUI.FloatField(rect[2], to);
                EditorGUI.MinMaxSlider(rect[1], ref min, ref to, attributeObject.Minimum, attributeObject.Maximum ?? to);
                vector = new Vector2(min < to ? min : to, to);
                property.vector2Value = vector;
            }
            else if (type == SerializedPropertyType.Vector2Int)
            {
                Vector2Int vector = property.vector2IntValue;
                float min = vector.x;
                float to = vector.y;
                min = EditorGUI.IntField(rect[0], (int)min);
                to = EditorGUI.IntField(rect[2], (int)to);
                EditorGUI.MinMaxSlider(rect[1], ref min, ref to, attributeObject.Minimum, attributeObject.Maximum ?? to);
                vector = new Vector2Int(Mathf.RoundToInt(min < to ? min : to), Mathf.RoundToInt(to));
                property.vector2IntValue = vector;
            }
            else
            {
                EditorGUI.HelpBox(ctrlRect, "Incorrect type for the MinMaxRange attribute.", MessageType.Error);
            }
        }

        /// <summary>
        /// Splits Rect into three parts.
        /// </summary>
        /// <param name="rect">Rect</param>
        /// <param name="bordersSize">Part border size</param>
        /// <param name="space">Space between rects</param>
        /// <returns>Rects array</returns>
        public static Rect[] SplitRectIn3(Rect rect, int bordersSize, int space = 0)
        {
            Rect[] rects = SplitRect(rect, 3);
            int pad = (int)rects[0].width - bordersSize;
            int ps = pad + space;
            rects[0].width = rects[2].width -= ps;
            rects[1].width += pad * 2;
            rects[1].x -= pad;
            rects[2].x += ps;
            return rects;
        }

        /// <summary>
        /// Splits Rect into parts.
        /// </summary>
        /// <param name="rect">Rect</param>
        /// <param name="partsCount">Parts count</param>
        /// <returns>Rects array</returns>
        public static Rect[] SplitRect(Rect rect, int partsCount)
        {
            Rect[] rects = new Rect[partsCount];
            for (var i = 0; i < partsCount; ++i)
            {
                rects[i] = new Rect(rect.x + rect.width / partsCount * i, rect.y, rect.width / partsCount, rect.height);
            }
            return rects;
        }
    }
}
