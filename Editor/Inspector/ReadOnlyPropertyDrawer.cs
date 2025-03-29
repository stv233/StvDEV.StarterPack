using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace StvDEV.Inspector
{
    /// <summary>
    /// Disables the display of fields marked with the <see cref="ReadOnlyAttribute"/> attribute.
    /// </summary>
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyPropertyDrawer : PropertyDrawer
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

            field.SetEnabled(false);

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
            bool wasEnabled = GUI.enabled;
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
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
            return EditorGUI.GetPropertyHeight(property, label);
        }

    }
}