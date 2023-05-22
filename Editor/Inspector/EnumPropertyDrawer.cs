using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace StvDEV.Inspector
{
    /// <summary>
    /// Draws the int value as a drop-down list with captions for the fields marked <see cref="EnumAttribute"/> attribute.
    /// </summary>
    [CustomPropertyDrawer(typeof(EnumAttribute))]
    public class EnumPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect ctrlRect = EditorGUI.PrefixLabel(position, label);
            EnumAttribute attributeObject = (EnumAttribute)attribute;
            if (property.propertyType == SerializedPropertyType.Integer)
            {
                property.intValue = EditorGUI.Popup(ctrlRect, property.intValue, attributeObject.Captions.Select(x => new GUIContent(x)).ToArray());
            }
            else
            {
                EditorGUI.HelpBox(ctrlRect, "Incorrect type for the AnnotatedBool attribute.", MessageType.Error);
            }
        }
    }
}
