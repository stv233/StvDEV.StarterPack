using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace StvDEV.Inspector
{
    /// <summary>
    /// Draws the bool value as a drop-down list for the fields marked <see cref="CaptionedBoolAttribute"/> attribute.
    /// </summary>
    [CustomPropertyDrawer(typeof(CaptionedBoolAttribute))]
    public class CaptionedBoolPropertyDrawer : PropertyDrawer
    {
        /// <summary>
        /// Draw gui using UIToolkit.
        /// </summary>
        /// <param name="property">Property</param>
        /// <returns>Root element</returns>
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement container = new VisualElement();

            if (property.propertyType == SerializedPropertyType.Boolean)
            {
                CaptionedBoolAttribute attributeObject = (CaptionedBoolAttribute)attribute;

                PopupField<string> field = new PopupField<string>(property.displayName, new List<string> { attributeObject.False, attributeObject.True}, property.boolValue ? 1 : 0);
                field.value = property.boolValue ? attributeObject.True : attributeObject.False;
                field.AddToClassList("unity-base-field__aligned");

                field.RegisterCallback<ChangeEvent<string>>(x =>
                {
                    property.boolValue = x.newValue == attributeObject.True;
                    property.serializedObject.ApplyModifiedProperties();
                });

                container.Add(field);
            }
            else
            {
                HelpBox box = new HelpBox("Incorrect type for the CaptionedBool attribute.", HelpBoxMessageType.Error);
                container.Add(box);
            }
            return container;
        }

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
                EditorGUI.HelpBox(ctrlRect, "Incorrect type for the CaptionedBool attribute.", MessageType.Error);
            }
        }
    }
}
