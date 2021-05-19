namespace TongueTwister.Editor.Trees.LocaleEditorTree
{
    public class LocaleEditorTreeViewItem : UnityEditor.IMGUI.Controls.TreeViewItem
    {
        public Locale Data { get; set; }

        public LocaleEditorTreeViewItem(int id, int depth, string name, Locale data) : base(id, depth, name)
        {
            Data = data;
        }
    }
}