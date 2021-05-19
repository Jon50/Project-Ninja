using System.Collections.Generic;
using TongueTwister.Editor.Tools;

namespace TongueTwister.Editor.Trees.ToolTree
{
    public class ToolTree
    {
        #region Private
        
        private List<TongueTwisterTool> _tools;

        private ToolTreeViewItem _root;
        
        #endregion

        #region Properties

        public List<TongueTwisterTool> Tools => _tools;

        public ToolTreeViewItem Root => _root ?? (_root = new ToolTreeViewItem(-1, -1, "root", null));

        #endregion
        
        #region Constructors

        private void Initialize(List<TongueTwisterTool> tools)
        {
            if (_tools != tools)
            {
                _tools?.Clear();
                _tools = tools ?? new List<TongueTwisterTool>();
            }
        }

        public ToolTree(List<TongueTwisterTool> tools) => Initialize(tools);

        #endregion

        #region Utilities

        public void SetDataFromList(List<TongueTwisterTool> tools) => Initialize(tools);

        #endregion
    }
}