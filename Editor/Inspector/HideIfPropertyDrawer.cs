using UnityEngine;
using UnityEditor;

namespace StvDEV.Inspector
{
    /// <summary>
    /// Hides or disables the display of fields marked with the <see cref="HideIfAttribute"/> attribute.
    /// </summary>
    [CustomPropertyDrawer(typeof(HideIfAttribute))]
    public class HideIFPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            HideIfAttribute hideIf = (HideIfAttribute)attribute;
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
        /// Calculate the height of property so that when the property needs to be hidden the following properties that are being drawn don’t overlap.
        /// </summary>
        /// <param name="property">Property</param>
        /// <param name="label">Property label</param>
        /// <returns>Property height</returns>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            HideIfAttribute hideIf = (HideIfAttribute)attribute;
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
        /// <param name="hideIf">Attribute</param>
        /// <param name="property">Property</param>
        /// <returns>Field value</returns>
        private bool GetConditionalHideAttributeResult(HideIfAttribute hideIf, SerializedProperty property)
        {
            bool enabled = true;
            string propertyPath = property.propertyPath;
            string conditionPath = propertyPath.Replace(property.name, hideIf.ConditionalSourceField); 
            SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(conditionPath);

            if (sourcePropertyValue != null)
            {
                enabled = sourcePropertyValue.boolValue;
            }
            else
            {
                Debug.LogWarning("Attempting to use a ConditionalHideAttribute but no matching SourcePropertyValue found in object: " + hideIf.ConditionalSourceField);
            }

            return enabled;
        }
    }
}