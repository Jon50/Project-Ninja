using System.Collections.Generic;
using System.Linq;
using TongueTwister.Editor.Utilities;
using TongueTwister.Fields;
using TongueTwister.Models;
using UnityEditor;
using UnityEngine;

namespace TongueTwister.Editor
{
    /// <summary>
    /// This display key field property drawer does its best to provide an assumed set of options given there's a
    /// LocalizationManager selected in the TongueTwister window. Warnings are provided on the field if this is not the
    /// case or if there's an issue with the selection. Apologies for the large amount of in-line comments.
    /// </summary>
    [CustomPropertyDrawer(typeof(ActiveSelectionDisplayKeyField))]
    public class ActiveSelectionDisplayKeyFieldPropertyDrawer : PropertyDrawer
    {
        private const string MODEL_ID_PROPERTY_NAME = "_modelId", VALUE_PROPERTY_NAME = "_stringBackup";
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var modelIdProperty = property.FindPropertyRelative(MODEL_ID_PROPERTY_NAME);
            var stringBackupProperty = property.FindPropertyRelative(VALUE_PROPERTY_NAME);
            var guiEnabled = GUI.enabled;
            var currentValueIndex = 0;
            var localizationManagerAvailable = TongueTwisterWindow.CurrentWindow?.HasLocalizationManager == true;
            var chosenOptionNotIndexed = false;
            
            var displayKeys = localizationManagerAvailable 
                ? TongueTwisterWindow.AllDisplayKeys 
                : new List<TongueTwisterModel>();

            if (!localizationManagerAvailable)
            {
                label.image = TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.Warning);
                label.tooltip = "No LocalizationManager selected in the TongueTwister window. This field pulls all of its values from the currently selected LocalizationManager. Open TongueTwister to assign a LocalizationManager.";
                GUI.enabled = false;
            }
            else if (modelIdProperty.intValue == -1)
            {
                // check if the field needs to be defaulted. If there are no display keys, this can't happen and the
                // values will remain -1/"". 
                
                if (displayKeys.Count > 0)
                {
                    var firstDisplayKey = displayKeys.First();
                    modelIdProperty.intValue = firstDisplayKey.Id;
                    stringBackupProperty.stringValue = firstDisplayKey.GetFullDotName();
                    property.serializedObject.ApplyModifiedProperties();
                }
                else
                {
                    modelIdProperty.intValue = -1;
                    stringBackupProperty.stringValue = "";
                    property.serializedObject.ApplyModifiedProperties();
                }
            }
            else
            {
                // check to see if the model ID might've changed or if the current chosen value represents a display 
                // key that doesn't exist. If so, that's a problem because there's a potential de-sync between 
                // this display key field and a legit LocalizationManager. 
                
                var indexOfChosenValue = IndexOf(displayKeys, modelIdProperty.intValue);
                if (indexOfChosenValue == -1)
                {
                    // there's no display key with the given ID in the currently selected LocalizationManager
                    
                    indexOfChosenValue = IndexOf(displayKeys, stringBackupProperty.stringValue);
                    if (indexOfChosenValue == -1)
                    {
                        // doing an "IndexOf" search by the previously recorded value property's string value, there
                        // is no display key that might have the same FullDotName. In which case, this is an "orphan" 
                        // display key case.
                        
                        label.image = EditorGUIUtility.IconContent("console.warnicon").image;
                        label.tooltip =
                            "The chosen display key value is not listed amongst the options of the currently selected LocalizationManager in the TongueTwister window. Either add it or select a new display key.";
                        chosenOptionNotIndexed = true;
                    }
                    else
                    {
                        // doing an "IndexOf" search by the previously recorded value property's string value, there is
                        // another display key that just so happens to have the same exact FullDotName. Either the
                        // original display key's model ID changed or the original display key was deleted and a new one
                        // was created and given the same name (either by coincidence or purposefully).
                        
                        modelIdProperty.intValue = displayKeys[indexOfChosenValue].Id;
                        stringBackupProperty.stringValue = displayKeys[indexOfChosenValue].GetFullDotName();
                        property.serializedObject.ApplyModifiedProperties();
                        currentValueIndex = indexOfChosenValue;
                    }
                }
                else
                {
                    currentValueIndex = indexOfChosenValue;
                }
            }
            
            var stringOptions = localizationManagerAvailable && displayKeys.Count > 0
                ? CreateStringOptions(displayKeys, chosenOptionNotIndexed ? stringBackupProperty.stringValue : null)
                : new List<string>() { stringBackupProperty.stringValue ?? "No Display Keys" };

            var guiContentOptions = CreateGuiOptions(stringOptions);

            GUI.enabled = displayKeys.Count > 0;
            
            var chosenValue = EditorGUI.Popup(position, label, currentValueIndex, guiContentOptions);
            if (localizationManagerAvailable && chosenValue != currentValueIndex)
            {
                // modify the chosen value in case of there existing an orphaned display key which is selected
                
                var modifiedChosenValue = 
                    Mathf.Clamp(
                        chosenOptionNotIndexed ? chosenValue - 1 : chosenValue,
                        0,
                        stringOptions.Count);
                modelIdProperty.intValue = displayKeys[modifiedChosenValue].Id;
                stringBackupProperty.stringValue = displayKeys[modifiedChosenValue].GetFullDotName();
                property.serializedObject.ApplyModifiedProperties();
            }
            
            GUI.enabled = guiEnabled;
        }

        private int IndexOf(List<TongueTwisterModel> models, int id)
        {
            for (int i = 0; i < models.Count; i++)
            {
                if (models[i].Id == id) return i;
            }

            return -1;
        }

        private int IndexOf(List<TongueTwisterModel> models, string name)
        {
            for (int i = 0; i < models.Count; i++)
            {
                if (models[i].GetFullDotName() == name) return i;
            }

            return -1;
        }

        private List<string> CreateStringOptions(List<TongueTwisterModel> displayKeys, string zeroValue)
        {
            var result = new List<string>();

            if (!string.IsNullOrWhiteSpace(zeroValue))
            {
                result.Add(zeroValue);
            }

            result.AddRange(displayKeys.Select(dk => dk.GetFullDotName()));

            return result;
        }

        private GUIContent[] CreateGuiOptions(List<string> options)
        {
            return options
                .Select(displayKey => new GUIContent(displayKey.Replace(".", "/")))
                .ToArray();
        }
    }
}