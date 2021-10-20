using System;
using System.IO;
using TongueTwister.StaticLabels;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TongueTwister.Pathbuilders
{
    /// <summary>
    /// Builds an "additional localization folder path" based on <c>Application.persistentDataPath</c> and some provided string.
    /// </summary>
    [CreateAssetMenu(menuName = "TongueTwister/Folder Path Builders/Application Persistent Folder Path Builder")]
    public class ApplicationPersistentFolderPathBuilder : FolderPathBuilder
    {
        [HideInInspector] [SerializeField] private string _baseFolderName = "";
        
        public override void DrawEditorUi(Action<Object, string> onChangeCallback)
        {
#if UNITY_EDITOR
            GUILayout.Label(EditorLabels.LocalizationManager.EditorModes.Settings
                .LocalizationSettings.AdditionalLocalizationsFolder.ApplicationPersistentDataPathInstructions, 
                UnityEditor.EditorStyles.wordWrappedLabel);
            
            var baseFolderName = UnityEditor.EditorGUILayout.TextField(new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.Settings.LocalizationSettings.AdditionalLocalizationsFolder.Text,
                    EditorLabels.LocalizationManager.EditorModes.Settings.LocalizationSettings.AdditionalLocalizationsFolder.ApplicationPersistentDataPathTooltip),
                _baseFolderName);

            if (baseFolderName != _baseFolderName)
            {
                _baseFolderName = baseFolderName;
                
                onChangeCallback(
                    this,
                    EditorLabels.LocalizationManager.EditorChanges.ChangedAdditionalLocalizationsFolderPath);
                
                MarkSelfAsDirty();
            }
            
            if (!Directory.Exists(InternalBuildPath()))
            {
                GUILayout.Label(
                    $"<color=#FF0000FF>{EditorLabels.LocalizationManager.EditorModes.Settings.LocalizationSettings.AdditionalLocalizationsFolder.PathDoesNotExist}</color>",
                    new GUIStyle() {richText = true, wordWrap = true});
            }

            GUI.enabled = false;
            UnityEditor.EditorGUILayout.TextField(
                EditorLabels.TongueTwisterWindow.Common.Preview, _baseFolderName == null ? "" : InternalBuildPath());
            GUI.enabled = true;
#endif
        }

        private string InternalBuildPath()
        {
            return Path.Combine(
                Application.persistentDataPath.Replace("/", "\\"), 
                _baseFolderName?.Replace("/", "\\") ?? "");
        }
        
        public override string BuildPath()
        {
            try
            {
                if (_baseFolderName == null)
                {
                    throw new Exception("Base folder name not set.");
                }
                
                return InternalBuildPath();
            }
            catch (Exception exception)
            {
                Debug.LogError($"Failed to build path, reason: {exception.Message}");
                return "";
            }
        }
    }
}