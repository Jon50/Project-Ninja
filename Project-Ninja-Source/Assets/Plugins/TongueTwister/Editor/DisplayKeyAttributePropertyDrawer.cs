using System;
using System.Collections.Generic;
using System.Linq;
using TongueTwister.Fields;
using UnityEditor;
using UnityEngine;

namespace TongueTwister.Editor
{
    /// <summary>
    /// Custom property drawer implementation for <see cref="DisplayKeyAttribute"/>.
    /// </summary>
    [CustomPropertyDrawer(typeof(DisplayKeyAttribute))]
    public class DisplayKeyAttributePropertyDrawer : PropertyDrawer
    {
        /// <summary>
        /// Draws GUI.
        /// </summary>
        /// <param name="position">The existing GUI Rect position.</param>
        /// <param name="property">The serialized property.</param>
        /// <param name="label">The label.</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var displayKeyAttribute = attribute as DisplayKeyAttribute;
            var guiEnabled = GUI.enabled;

            if (displayKeyAttribute?.Type == null)
            {
                var labelStyle = new GUIStyle() { richText = true };
                GUI.Label(position, "<color=#FF0000FF>Cannot Render Display Key - Type is Null</color>", labelStyle);
                return;
            }

            var options = new List<string>();
            BuildOptions(displayKeyAttribute.Type, ref options);
            var noOptions = options.Count == 1; // empty string will be the only option 
            
            var currentChosenItemIndex = options.IndexOf(property.stringValue.Replace(".", "/"));
            if (currentChosenItemIndex == -1)
            {
                currentChosenItemIndex = 0;
            }

            if (guiEnabled && noOptions)
            {
                GUI.enabled = false;
            }
            
            var newChosenIndex = EditorGUI.Popup(position, label, currentChosenItemIndex, options.Select(option => new GUIContent(option)).ToArray());
            var chosenItem = options[newChosenIndex].Replace("/", ".");

            if (!noOptions && chosenItem != property.stringValue)
            {
                property.stringValue = chosenItem;
                property.serializedObject.ApplyModifiedProperties();
            }

            GUI.enabled = guiEnabled;
        }

        /// <summary>
        /// Builds a list of string options for the editor's drop down given a type.
        /// </summary>
        /// <param name="type">A C# type that contains a list of static classes and display key constants.</param>
        /// <param name="options">A referenced list of string options to output to.</param>
        private void BuildOptions(Type type, ref List<string> options)
        {
            if (options == null || options.Count == 0)
            {
                options = new List<string>();
                options.Add("");
            }
            
            var fields = type.GetFields();
            foreach (var field in fields)
            {
                var value = field.GetRawConstantValue() as string;
                value = value.Replace(".", "/");
                options.Add(value);
            }

            var nestTypes = type.GetNestedTypes();
            foreach (var nestedType in nestTypes)
            {
                BuildOptions(nestedType, ref options);
            }
        }
    }
}