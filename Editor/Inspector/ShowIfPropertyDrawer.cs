using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace StvDEV.Inspector
{
    /// <summary>
    /// Hides or disables the display of fields marked with the <see cref="ShowIfAttribute"/> attribute.
    /// </summary>
    [CustomPropertyDrawer(typeof(ShowIfAttribute))]
    public class ShowIfPropertyDrawer : PropertyDrawer
    {

        /// <summary>
        /// Draw gui using UIToolkit.
        /// </summary>
        /// <param name="property">Property</param>
        /// <returns>Root element</returns>
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement container = new VisualElement();
            PropertyField field = new PropertyField(property);

            container.Add(field);

            container.TrackSerializedObjectValue(property.serializedObject, _ =>
            {
                UpdateVisibility();
            });

            UpdateVisibility();

            void UpdateVisibility()
            {
                ShowIfAttribute hideIf = (ShowIfAttribute)attribute;
                bool enabled = GetConditionalHideAttributeResult(hideIf, property);

                field.SetEnabled(enabled);

                if (!hideIf.HideInInspector || enabled)
                {
                    field.style.display = DisplayStyle.Flex;
                }
                else
                {
                    field.style.display = DisplayStyle.None;
                }
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
            ShowIfAttribute hideIf = (ShowIfAttribute)attribute;
            bool enabled = GetConditionalHideAttributeResult(hideIf, property);

            bool wasEnabled = GUI.enabled;
            GUI.enabled = enabled;
            if (!hideIf.HideInInspector || enabled)
            {
                EditorGUI.PropertyField(position, property, label, true);
            }

            GUI.enabled = wasEnabled;
        }

        /// <summary>
        /// Calculate the height of property so that when the property needs to be hidden the following properties that are being drawn don�t overlap.
        /// </summary>
        /// <param name="property">Property</param>
        /// <param name="label">Property label</param>
        /// <returns>Property height</returns>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            ShowIfAttribute hideIf = (ShowIfAttribute)attribute;
            bool enabled = GetConditionalHideAttributeResult(hideIf, property);

            if (!hideIf.HideInInspector || enabled)
            {
                return EditorGUI.GetPropertyHeight(property, label);
            }
            else
            {
                return -EditorGUIUtility.standardVerticalSpacing;
            }
        }

        /// <summary>
        /// Checks the value of the target field.
        /// </summary>
        /// <param name="showIf">Attribute</param>
        /// <param name="property">Property</param>
        /// <returns>Field value</returns>
        private bool GetConditionalHideAttributeResult(ShowIfAttribute showIf, SerializedProperty property)
        {
            bool enabled;

            if (showIf.ByValue)
            {
                enabled = showIf.Value;
            }
            else if (showIf.ByFunc)
            {
                enabled = showIf.Func != null ? showIf.Func.Invoke() : true;
            }
            else
            {
                string fieldName = showIf.ConditionalSourceField;
                bool inverse = showIf.Inverse;
                string propertyPath = property.propertyPath;
                string conditionPath = propertyPath.Replace(property.name, fieldName);
                SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(conditionPath);

                if (sourcePropertyValue != null)
                {
                    switch (sourcePropertyValue.propertyType)
                    {
                        case SerializedPropertyType.Boolean:
                            enabled = sourcePropertyValue.boolValue;
                            break;
                        case SerializedPropertyType.Integer:
                            enabled = sourcePropertyValue.intValue != 0;
                            break;
                        case SerializedPropertyType.Float:
                            enabled = sourcePropertyValue.floatValue != 0;
                            break;
                        case SerializedPropertyType.ObjectReference:
                            enabled = sourcePropertyValue.objectReferenceValue;
                            break;
                        default:
                            throw new NotSupportedException($"ShowIf attribute does not support the type of this field type: {sourcePropertyValue.propertyType}");
                    }

                    if (inverse)
                    {
                        enabled = !enabled;
                    }
                }
                else
                {
                    throw new ArgumentException("Attempting to use a ShowIf but no matching field found in object: " + showIf.ConditionalSourceField);
                }
            }


            return enabled;
        }
    }
}