using TongueTwister.StaticLabels;
using UnityEngine;

namespace TongueTwister.Pathbuilders
{
    /// <summary>
    /// Allows for a simple folder path to be used.
    /// </summary>
    [CreateAssetMenu(menuName = "TongueTwister/Folder Path Builders/Simple Folder Path Builder")]
    public class SimpleFolderPathBuilder : FolderPathBuilder
    {
        [HideInInspector]
        [SerializeField] 
        private string _folderName = "C:\\localizations";
        
        public override void DrawEditorUi(System.Action<Object, string> onChangeCallback)
        {
#if UNITY_EDITOR
            var folderName = UnityEditor.EditorGUILayout.TextField(new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.Settings.LocalizationSettings.AdditionalLocalizationsFolder.Text,
                    EditorLabels.LocalizationManager.EditorModes.Settings.LocalizationSettings.AdditionalLocalizationsFolder.SimplePathTooltip),
                _folderName);

            if (folderName != _folderName)
            {
                _folderName = folderName;
                
                onChangeCallback(
                    this,
                    EditorLabels.LocalizationManager.EditorChanges.ChangedAdditionalLocalizationsFolderPath);

                MarkSelfAsDirty();
            }

            UnityEditor.EditorGUILayout.LabelField(BuildPath());
#endif
        }

        public override string BuildPath()
        {
            return _folderName;
        }
    }
}