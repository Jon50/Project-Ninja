using UnityEditor;
using UnityEngine;

namespace TongueTwister.Examples.Editor
{
    /// <summary>
    /// Provides context menu support for adding an TMP-LTB to the scene.
    /// </summary>
    public static class TMPLTBContextMenus
    {
        [MenuItem("GameObject/Tongue Twister/TMP LocalizableTextBehaviour", false, 48)]
        private static void CreateTMPLTB()
        {
            _ = new GameObject("LocalizableText", typeof(TMPLocalizableTextBehaviour));
        }
    }
}