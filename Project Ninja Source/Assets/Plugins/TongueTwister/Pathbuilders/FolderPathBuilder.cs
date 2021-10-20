using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace TongueTwister.Pathbuilders
{
    /// <summary>
    /// A type of module which builds a string folder path. Please note, this class may go away in future versions and
    /// become updated similar to how the editor importer tools work.
    /// </summary>
    [Serializable]
    public abstract class FolderPathBuilder : ScriptableObject
    {
        /// <summary>
        /// Draws any editor UI options.
        /// </summary>
        /// <param name="onChangeCallback">A callback used by the <c>TongueTwisterWindow</c> to denote to the Unity
        /// Editor whenever something has changed. The first parameter is the UnityEngine.Object which has changed,
        /// the second parameter is an explanation of the change.</param>
        public abstract void DrawEditorUi(Action<Object, string> onChangeCallback);

        /// <summary>
        /// Creates the path that the additional localizations folder should exist at.
        /// </summary>
        /// <returns>A string folder path.</returns>
        public abstract string BuildPath();

        /// <summary>
        /// Allows for this object to mark itself as dirty if used within the unity editor context.
        /// </summary>
        protected virtual void MarkSelfAsDirty()
        {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
#endif
        }
    }
}