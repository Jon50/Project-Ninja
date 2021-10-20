using TongueTwister.Editor.Tools;

namespace TongueTwister.Editor.Trees.ToolTree
{
    public class ToolTreeViewItem : UnityEditor.IMGUI.Controls.TreeViewItem
    {
        /// <summary>
        /// The full name of the tool tree view item including its parent categories.
        /// </summary>
        public string FullName { get; set; }
        
        public TongueTwisterTool Tool { get; set; }

        /// <summary>
        /// Creates a new item based on a tool.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="depth"></param>
        /// <param name="tool"></param>
        public ToolTreeViewItem(int id, int depth, TongueTwisterTool tool) : base(id, depth, tool.Title)
        {
            Tool = tool;
            FullName = $"{tool.Category}/{tool.Title}";
        }

        /// <summary>
        /// Creates a new item based on a category (no tool).
        /// </summary>
        /// <param name="id"></param>
        /// <param name="depth"></param>
        /// <param name="fullName"></param>
        /// <param name="displayName"></param>
        public ToolTreeViewItem(int id, int depth, string fullName, string displayName) : base(id, depth, displayName)
        {
            Tool = null;
            FullName = fullName;
        }
    }
}