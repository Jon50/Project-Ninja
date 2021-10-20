using UnityEditor;
using UnityEngine;

namespace TongueTwister.Editor.Tools
{
    /// <summary>
    /// An editor-only tool module. The system will scan the assembly for all classes which may implement or inherit
    /// from this class and treat them as an instantiable editor tool.
    /// </summary>
    public abstract class TongueTwisterTool
    {
        /// <summary>
        /// The category path which is used to help organize this tool in the tree view. Categories are separated by
        /// slash marks ("/") to denote a new folder.
        /// </summary>
        public abstract string Category { get; }

        /// <summary>
        /// The actual title of this tool which appears in the editor once selected. 
        /// </summary>
        public abstract string Title { get;  }
        
        /// <summary>
        /// The description of the tool.
        /// </summary>
        public abstract string Description { get; }
        
        /// <summary>
        /// The version of the tool.
        /// </summary>
        public abstract string Version { get; }

        /// <summary>
        /// The resources path used for loading this tool's icon.
        /// </summary>
        public virtual string ResourceIconPath => EditorGUIUtility.isProSkin ? "icons/toolsIcon" : "icons/toolsIconDark";

        private Texture _resourceIcon;

        /// <summary>
        /// The texture icon representing this tool.
        /// </summary>
        public Texture ResourceIcon
        {
            get
            {
                if (!_resourceIcon)
                {
                    _resourceIcon = Resources.Load<Texture>(ResourceIconPath);
                }

                return _resourceIcon;
            }
        }
        
        /// <summary>
        /// Draws editor UI.
        /// </summary>
        public abstract void DrawEditorUi();
    }
}