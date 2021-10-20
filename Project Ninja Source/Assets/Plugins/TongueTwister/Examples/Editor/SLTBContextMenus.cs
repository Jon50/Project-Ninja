using UnityEditor;
using UnityEngine;

namespace TongueTwister.Examples.Editor
{
    /// <summary>
    /// Provides context menu support for adding an SLTB to the scene.
    /// </summary>
    public static class SLTBContextMenus
    {
        [MenuItem("GameObject/Tongue Twister/Standard LocalizableTextBehaviour", false, 48)]
        private static void CreateSLTB()
        {
            _ = new GameObject("LocalizableText", typeof(StandardLocalizableTextBehaviour));
        }
    }
}