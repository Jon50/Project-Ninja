using System.Collections.Generic;
using System.Reflection;
using TongueTwister.Models;
using UnityEditor;
using UnityEngine;
using TongueTwister.Pathbuilders;
using TongueTwister.StaticLabels;

namespace TongueTwister.Editor
{
    /// <summary>
    /// Custom editor for the LocalizationManager. Most fields utilize the Unity serialized property system, others
    /// use reflection. For more information about the fields and properties of this class, it's recommended to see
    /// the documentation for <see cref="LocalizationManager"/> instead.
    /// </summary>
    [CustomEditor(typeof(LocalizationManager))]
    public class LocalizationManagerEditor : UnityEditor.Editor
    {
        public TongueTwisterModelCollection ModelCollection
        {
            get
            {
                var fieldInfo = typeof(LocalizationManager).GetField("_modelCollection",
                    BindingFlags.Instance | BindingFlags.NonPublic);
                return fieldInfo?.GetValue(target as LocalizationManager) as TongueTwisterModelCollection ?? new TongueTwisterModelCollection();
            }
            set
            {
                serializedObject.Update();
                var fieldInfo = typeof(LocalizationManager).GetField("_modelCollection",
                    BindingFlags.Instance | BindingFlags.NonPublic);
                fieldInfo?.SetValue(target as LocalizationManager, value);
                EditorUtility.SetDirty(target);
                serializedObject.ApplyModifiedProperties();
            }
        }

        public string DefaultLocaleId
        {
            get
            {
                var value = _defaultLocaleId.stringValue;

                if (string.IsNullOrEmpty(value))
                {
                    // ensure that if the string is empty, it's defaulted to null instead.
                    value = null;
                }

                return value;
            }
            set
            {
                _defaultLocaleId.stringValue = value; 
                serializedObject.ApplyModifiedProperties();
            }
        }
        
        private SerializedProperty _defaultLocaleId => serializedObject.FindProperty("_defaultLocaleId");

        public string LastUsedLocalePrefName
        {
            get => _lastUsedLocalePrefName.stringValue;
            set
            {
                _lastUsedLocalePrefName.stringValue = value; 
                serializedObject.ApplyModifiedProperties();
            }
        }
        
        private SerializedProperty _lastUsedLocalePrefName =>
            serializedObject.FindProperty("_lastUsedLocalePlayerPrefKey");
        
        public bool SaveLastUsedLocaleToPlayerPrefs
        {
            get => _saveLastUsedLocaleToPlayerPrefs.boolValue;
            set
            {
                _saveLastUsedLocaleToPlayerPrefs.boolValue = value;
                serializedObject.ApplyModifiedProperties();
            }
        }

        private SerializedProperty _saveLastUsedLocaleToPlayerPrefs =>
            serializedObject.FindProperty("_saveLastUsedLocaleToPlayerPrefs");
        
        public bool LoadAdditionalLocalizationFiles
        {
            get => _loadAdditionalLocalizationFiles.boolValue;
            set
            {
                _loadAdditionalLocalizationFiles.boolValue = value;
                serializedObject.ApplyModifiedProperties();
            }
        }

        private SerializedProperty _loadAdditionalLocalizationFiles =>
            serializedObject.FindProperty("_loadAdditionalLocalizationFiles");

        public FolderPathBuilder AdditionaLocalizationsFolderPathBuilder
        {
            get => (FolderPathBuilder) _additionalLocalizationsFolderPathBuilder?.objectReferenceValue;
            set
            {
                _additionalLocalizationsFolderPathBuilder.objectReferenceValue = value;
                serializedObject.ApplyModifiedProperties();
            }
        }

        private SerializedProperty _additionalLocalizationsFolderPathBuilder =>
            serializedObject.FindProperty("_additionalLocalizationsFolderPathBuilder");

        public List<Locale> ConfiguredLocales
        {
            get => ConfiguredLocalesCollection?.Locales ?? new List<Locale>();
            set => ConfiguredLocalesCollection = new LocaleCollection(value);
        }

        public LocaleCollection ConfiguredLocalesCollection
        {
            get
            {
                var fieldInfo = typeof(LocalizationManager)
                        .GetField(
                            "_configuredLocalesCollection", 
                            BindingFlags.Instance | BindingFlags.NonPublic);
                return fieldInfo?.GetValue(target as LocalizationManager) as LocaleCollection;
            }
            set
            {
                serializedObject.Update();
                var fieldInfo = typeof(LocalizationManager).GetField("_configuredLocalesCollection",
                    BindingFlags.Instance | BindingFlags.NonPublic);
                fieldInfo?.SetValue(target as LocalizationManager, value);
                EditorUtility.SetDirty(target);
                serializedObject.ApplyModifiedProperties();
            }
        }
        
        private SerializedProperty _expandDebugSettings =>
            serializedObject.FindProperty("_expandDebugSettings");

        private SerializedProperty _enableDebugWindow => 
            serializedObject.FindProperty("_enableInGameDebugWindow");

        private SerializedProperty _debugWindowId =>
            serializedObject.FindProperty("_inGameDebugWindowId");

        public override void OnInspectorGUI()
        {
            DrawDebugEditorContent();
            DrawTongueTwisterWindowContent();
        }

        protected virtual void DrawDebugEditorContent()
        {
            var changeDetected = false;
            
            var toggled = EditorGUILayout.ToggleLeft(
                new GUIContent(
                    EditorLabels.LocalizationManagerEditor.Sections.Debug.Text,
                    EditorLabels.LocalizationManagerEditor.Sections.Debug.Tooltip),
                _expandDebugSettings.boolValue);

            if (toggled != _expandDebugSettings.boolValue)
            {
                _expandDebugSettings.boolValue = toggled;
                changeDetected = true;
            }

            if (toggled)
            {
                EditorGUI.indentLevel++;
                var enableDebugWindow =
                    EditorGUILayout.ToggleLeft(
                        new GUIContent(
                            EditorLabels.LocalizationManagerEditor.Sections.Debug.EnableDebugWindow.Label,
                            EditorLabels.LocalizationManagerEditor.Sections.Debug.EnableDebugWindow.Tooltip),
                        _enableDebugWindow.boolValue
                    );

                if (enableDebugWindow != _enableDebugWindow.boolValue)
                {
                    _enableDebugWindow.boolValue = enableDebugWindow;
                    changeDetected = true;
                }

                GUI.enabled = enableDebugWindow;
                
                var debugWindowId =
                    EditorGUILayout.IntField(
                        new GUIContent(
                            EditorLabels.LocalizationManagerEditor.Sections.Debug.DebugWindowId.Label,
                            EditorLabels.LocalizationManagerEditor.Sections.Debug.DebugWindowId.Tooltip),
                        _debugWindowId.intValue
                    );

                if (debugWindowId != _debugWindowId.intValue)
                {
                    _debugWindowId.intValue = debugWindowId;
                    changeDetected = true;
                }

                GUI.enabled = true;
                EditorGUI.indentLevel--;
            }

            if (changeDetected)
            {
                serializedObject.ApplyModifiedProperties();
            }
        }

        private void DrawTongueTwisterWindowContent()
        {
            if (GUILayout.Button(
                new GUIContent(
                    EditorLabels.LocalizationManagerEditor.Buttons.Open.Text,
                    EditorLabels.LocalizationManagerEditor.Buttons.Open.Tooltip), 
                GUILayout.Height(50)))
            {
                TongueTwisterWindow.Open(this);
            }
        }
    }
}