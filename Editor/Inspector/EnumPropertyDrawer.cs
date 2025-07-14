using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace StvDEV.Inspector
{
    /// <summary>
    /// Draws the int value as a drop-down list with captions for the fields marked <see cref="EnumAttribute"/> attribute.
    /// </summary>
    [CustomPropertyDrawer(typeof(EnumAttribute))]
    public class EnumPropertyDrawer : PropertyDrawer
    {
        /// <summary>
        /// Draw gui using UIToolkit.
        /// </summary>
        /// <param name="property">Property</param>
        /// <returns>Root element</returns>
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement container = new();

            EnumAttribute attributeObject = (EnumAttribute)attribute;

            if (property.propertyType == SerializedPropertyType.Integer)
            {
                PopupField<string> field = new(property.displayName, attributeObject.Captions.ToList(), 0);
                field.AddToClassList("unity-base-field__aligned");
                field.RegisterCallback<ChangeEvent<string>>(x =>
                {
                    property.intValue = Array.IndexOf(attributeObject.Captions, x.newValue);
                    property.serializedObject.ApplyModifiedProperties();
                });

                if (property.intValue >= 0 && property.intValue < attributeObject.Captions.Length)
                {
                    field.SetValueWithoutNotify(attributeObject.Captions[property.intValue]);
                }
                container.Add(field);
            }
            else
            {
                HelpBox box = new("Incorrect type for the Enum attribute.", HelpBoxMessageType.Error);
                container.Add(box);
            }
            return container;
        }

        /// <summary>
        /// Ons the gui using the specified position.
        /// </summary>
        /// <param name="position">The position</param>
        /// <param name="property">The property</param>
        /// <param name="label">The label</param>
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
                EditorGUI.HelpBox(ctrlRect, "Incorrect type for the Enum attribute.", MessageType.Error);
            }
        }
    }
}
