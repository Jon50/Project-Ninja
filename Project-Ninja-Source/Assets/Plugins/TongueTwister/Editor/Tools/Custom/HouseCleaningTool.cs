using UnityEngine;

namespace TongueTwister.Editor.Tools.Custom
{
    /// <summary>
    /// Functions and utilities to help maintain a clean project.
    /// </summary>
    public class HouseCleaningTool : TongueTwisterTool
    {
        public override string Category => "Tools";

        public override string Title => "House Cleaning Tools";

        public override string Description => "Functions and utilities to help maintain a clean project.";

        public override string Version => "1.2";
        
        public override void DrawEditorUi()
        {
            var tongueTwisterWindow = TongueTwister.Editor.TongueTwisterWindow.CurrentWindow;
            
            if (GUILayout.Button("Remove Empty Localizations"))
            {
                tongueTwisterWindow.RemoveEmptyLocalizations();
                UnityEditor.EditorUtility.DisplayDialog("Remove Empty Localizations", "Removed all empty localizations.", "Close");
            }
            if (GUILayout.Button("Remove Empty Display Keys"))
            {
                tongueTwisterWindow.RemoveEmptyDisplayKeys();
                UnityEditor.EditorUtility.DisplayDialog("Remove Empty Display Keys", "Removed all empty display keys.", "Close");
            }
            if (GUILayout.Button("Remove Empty Groups"))
            {
                tongueTwisterWindow.RemoveEmptyGroups();
                UnityEditor.EditorUtility.DisplayDialog("Remove Empty Groups", "Removed all empty groups.", "Close");
            }
        }
    }
}