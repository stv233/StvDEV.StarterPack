using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace StvDEV.Inspector
{
    /// <summary>
    /// Draws the bool value as a drop-down list for the fields marked <see cref="CaptionedBoolAttribute"/> attribute.
    /// </summary>
    [CustomPropertyDrawer(typeof(CaptionedBoolAttribute))]
    public class CaptionedBoolPropertyDrawer : PropertyDrawer
    {
        /// <summary>
        /// Ons the gui using the specified position
        /// </summary>
        /// <param name="position">The position</param>
        /// <param name="property">The property</param>
        /// <param name="label">The label</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect ctrlRect = EditorGUI.PrefixLabel(position, label);
            CaptionedBoolAttribute attributeObject = (CaptionedBoolAttribute)attribute;
            if (property.propertyType == SerializedPropertyType.Boolean)
            {
                property.boolValue = EditorGUI.Popup(ctrlRect, property.boolValue ? 1 : 0, 
                    new GUIContent[] { new GUIContent(attributeObject.False), new GUIContent(attributeObject.True) }) == 1;
            }
            else
            {
                EditorGUI.HelpBox(ctrlRect, "Incorrect type for the AnnotatedBool attribute.", MessageType.Error);
            }
        }
    }
}
