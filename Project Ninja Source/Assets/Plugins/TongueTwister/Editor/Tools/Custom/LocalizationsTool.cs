using System;
using TongueTwister.Models;
using UnityEditor;
using UnityEngine;

namespace TongueTwister.Editor.Tools.Custom
{
    public class LocalizationsTool : TongueTwisterTool
    {
        public override string Category => "Tools";

        public override string Title => "Localization Management";

        public override string Description => "Tools for managing localizations within the project.";
        
        public override string Version => "BETA 0.1";
        
        public override void DrawEditorUi()
        {
            GUILayout.Label("This tool ensures every display key has a localization for every configured locale. This is useful for quickly creating large quantities of localizations.", EditorStyles.wordWrappedLabel);
            GUILayout.Space(15);
            
            if (GUILayout.Button(
                new GUIContent(
                    "Create Missing Localizations", 
                    "Adds an empty localization to each display key that doesn't support one of the configured locales.")))
            {
                CreateMissingLocalizations();
            }
        }

        private void CreateMissingLocalizations()
        {
            var configuredLocales = TongueTwisterWindow.CurrentWindow.LocalizationManagerEditor.ConfiguredLocales;

            if (configuredLocales == null || configuredLocales.Count == 0)
            {
                EditorUtility.DisplayDialog("Operation Failed", "No configured locales.", "OK");
                return;
            }
            
            var displayKeys = TongueTwisterWindow.AllDisplayKeys;

            if (displayKeys == null || displayKeys.Count == 0)
            {
                EditorUtility.DisplayDialog("Operation Failed", "No configured display keys.", "OK");
                return;
            }
            
            var progressBarTitle = "Adding Localizations...";
            var totalLocalizations = 0;

            TongueTwisterWindow.CurrentWindow.RecordLocalizationManagerEditorChange("Added Empty Localizations");

            for (int i = 0; i < displayKeys.Count; i++)
            {
                EditorUtility.DisplayProgressBar(progressBarTitle, displayKeys[i].GetFullDotName(), (float) i / displayKeys.Count);

                try
                {
                    foreach (var locale in configuredLocales)
                    {
                        var hasLocale = false;

                        if (displayKeys[i].HasChildren)
                        {
                            foreach (var localization in displayKeys[i].Children)
                            {
                                if (localization.LocaleId == locale.Id)
                                {
                                    hasLocale = true;
                                    break;
                                }
                            }
                        }

                        if (!hasLocale)
                        {
                            var model = TongueTwisterWindow.CurrentWindow.AddNewModel(
                                TongueTwisterModel.ModelType.Localization,
                                displayKeys[i],
                                false);

                            model.LocaleId = locale.Id;
                            model.Name = locale.Metadata.DisplayName;

                            totalLocalizations++;
                        }
                    }
                }
                catch (Exception exception)
                {
                    Debug.LogError($"Failed to create localization for display key (\"{displayKeys[i].GetFullDotName()}), reason: {exception.Message}");
                }
            }
            
            EditorUtility.ClearProgressBar();
            EditorUtility.DisplayDialog("Done", $"Added {totalLocalizations} localization(s).", "Close");
            
            TongueTwisterWindow.CurrentWindow.Repaint();
            TongueTwisterWindow.CurrentWindow.SetDirty();
        }
    }
}