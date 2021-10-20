using TongueTwister.Models;

namespace TongueTwister.Editor.Trees.ModelEditorTree
{
    /// <summary>
    /// A custom implementation of the TreeViewItem developed by Unity. This TreeViewItem has a model reference.
    /// Documentation support for this class is not entirely completed or "good" in any regards as it should not be
    /// edited or worried about by the consumer.
    /// </summary>
    public class ModelEditorTreeViewItem : UnityEditor.IMGUI.Controls.TreeViewItem
    {
        /// <summary>
        /// The model data that this element is tied to.
        /// </summary>
        public TongueTwisterModel Data { get; set; }

        /// <summary>
        /// Creates a TVIE object with the given pre-defined details. This is the only constructor.
        /// </summary>
        /// <param name="id">ID of the object.</param>
        /// <param name="depth">The depth at which the object will appear in the tree view.</param>
        /// <param name="name">The object's name.</param>
        /// <param name="data">The model data for this object.</param>
        public ModelEditorTreeViewItem(int id, int depth, string name, TongueTwisterModel data) : base(id, depth, name)
        {
            Data = data; 
        }
    }
}