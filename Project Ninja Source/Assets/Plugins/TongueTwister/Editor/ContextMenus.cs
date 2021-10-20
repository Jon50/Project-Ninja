using UnityEditor;
using UnityEngine;

namespace TongueTwister.Editor
{
    /// <summary>
    /// Contains menu items for various parts of the Unity Editor.
    /// </summary>
    public static class ContextMenus
    {
        [MenuItem("GameObject/Tongue Twister/Localization Manager", false, 48)]
        private static void CreateLocalizationManager()
        {
            _ = new GameObject("LocalizationManager", typeof(LocalizationManager));
        }
    }
}