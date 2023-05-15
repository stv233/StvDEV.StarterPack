using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace StvDEV.Inspector
{
    /// <summary>
    /// Draws a two-sided slider for the fields marked <see cref="AnnotatedBoolAttribute"/> attribute.
    /// </summary>
    [CustomPropertyDrawer(typeof(AnnotatedBoolAttribute))]
    public class AnnotatedBoolPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect ctrlRect = EditorGUI.PrefixLabel(position, label);
            AnnotatedBoolAttribute attributeObject = (AnnotatedBoolAttribute)attribute;
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
