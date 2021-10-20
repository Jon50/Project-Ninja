using System;
using System.Collections.Generic;
using System.Linq;
using TongueTwister.Editor.Tools;
using TongueTwister.Editor.Trees.LocaleEditorTree;
using TongueTwister.Editor.Utilities;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace TongueTwister.Editor.Trees.ToolTree
{
    public class ToolTreeView : TreeView
    {
        #region Constants & Static Readonly

        private const float CONST_ROW_HEIGHT = 20.0f;

        private const float CONST_ROW_EXTRA_SPACE_BEFORE_ICON_AND_LABEL = 20.0f;

        private const float CONST_CODE_COLUMN_CONTENT_WIDTH = 60;

        private const float CONST_GENERAL_COLUMN_CONTENT_INDENT = 5;

        private const float CONST_ICON_COLUMN_CONTENT_WIDTH = 30;

        #endregion
        
        #region Enumerations

        private enum Column
        {
            Name
        }

        #endregion

        #region Private

        private ToolTree _tree;

        private readonly List<TreeViewItem> _rows = new List<TreeViewItem>();

        #endregion

        #region Properties

        public ToolTree tree => _tree;

        #endregion

        #region Constructors & Initialization

        public ToolTreeView(
            TreeViewState state, 
            MultiColumnHeader multiColumnHeader,
            ToolTree tree,
            bool showAlternatingRowBackgrounds = false) 
            : base(state, multiColumnHeader)
        {
            Initialize(tree, multiColumnHeader, showAlternatingRowBackgrounds);
        }
        
        private void Initialize(
            ToolTree tree,
            MultiColumnHeader multiColumnHeader = null,
            bool showAlternatingRowBackgrounds = false)
        {
            _tree = tree;

            if (multiColumnHeader != null)
            {
                rowHeight = CONST_ROW_HEIGHT;
                columnIndexForTreeFoldouts = 0;
                this.showAlternatingRowBackgrounds = showAlternatingRowBackgrounds;
                this.multiColumnHeader = multiColumnHeader;
                showBorder = true;
                extraSpaceBeforeIconAndLabel = CONST_ROW_EXTRA_SPACE_BEFORE_ICON_AND_LABEL;
            }

            Reload();
        }

        #endregion

        #region Utilities

         public static MultiColumnHeaderState CreateDefaultMultiColumnHeaderState()
        {
            var columns = new[] 
            {
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent(
                        "Tools",
                        ""), 
                    contextMenuText = "Info",
                    headerTextAlignment = TextAlignment.Left,
                    sortedAscending = true,
                    sortingArrowAlignment = TextAlignment.Right,
                    width = 1000, 
                    minWidth = 100,
                    maxWidth = 1000,
                    autoResize = false,
                    allowToggleVisibility = true,
                    canSort = false,
                }, 
            };

            var state =  new MultiColumnHeaderState(columns);
            return state;
        }

         public TongueTwisterTool GetTool(int rowId)
         {
             for (int i = 0; i < _rows.Count; i++)
             {
                 if (_rows[i].id == rowId)
                 {
                     return (_rows[i] as ToolTreeViewItem)?.Tool;
                 }
             }

             return null;
         }

        #endregion

        #region Protected & Private Utilities

        protected override TreeViewItem BuildRoot() => _tree.Root;

        protected override IList<TreeViewItem> BuildRows(TreeViewItem root)
        {
            _rows.Clear();

            var hasSearchString = !string.IsNullOrWhiteSpace(searchString);
            var allRows = CreateTreeStructureFromTreeData();
            var hiddenRows = new List<TreeViewItem>();

            foreach (var row in allRows)
            {
                if (hasSearchString)
                {
                    if (row is ToolTreeViewItem toolTreeViewItem && toolTreeViewItem.Tool == null)
                    {
                        continue;
                    }
                    
                    if (row.displayName.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        _rows.Add(row);
                    }
                }
                else
                {
                    if (row.parent.id != -1 && // root 
                        row.parent != null &&
                        (!IsExpanded(row.parent.id) || hiddenRows.Contains(row.parent)))
                    {
                        hiddenRows.Add(row);
                        continue;
                    }

                    _rows.Add(row);
                }
            }
            
            SetupParentsAndChildrenFromDepths(tree.Root, _rows);

            if (hasSearchString)
            {
                _rows.Sort((x, y) => EditorUtility.NaturalCompare(x.displayName, y.displayName));
            }

            return _rows;
        }

        private IList<TreeViewItem> CreateTreeStructureFromTreeData()
        {
            var result = new List<TreeViewItem>();
            var allTools = tree.Tools;
            var nextId = 0;

            if (tree.Root.children == null)
            {
                tree.Root.children = new List<TreeViewItem>();
            }

            // (1) create all items
            
            foreach (var tool in allTools)
            {
                if (!string.IsNullOrWhiteSpace(tool.Category))
                {
                    CreateCategoryChain(tool.Category.Split('/'), result, ref nextId);
                }
                
                var toolItem = new ToolTreeViewItem(nextId++, 0, tool);

                result.Add(toolItem);
            }
            
            // (2) create parents / depths

            foreach (var treeViewItem in result)
            {
                var item = (ToolTreeViewItem) treeViewItem;
                var categoryNames = item.FullName.Split('/');

                if (categoryNames.Length == 0 || item.FullName == item.displayName)
                {
                    item.parent = tree.Root;
                    item.depth = 0;

                    tree.Root.children.Add(item);
                }
                else
                {
                    var parent = result.FirstOrDefault(possibleParent =>
                        (possibleParent as ToolTreeViewItem).FullName ==
                        string.Join("/", categoryNames, 0, categoryNames.Length - 1));

                    if (parent != null)
                    {
                        if (parent.children == null)
                        {
                            parent.children = new List<TreeViewItem>();
                        }

                        parent.children.Add(item);
                        item.parent = parent;
                        item.depth = parent.depth + 1;
                    }
                    else
                    {
                        Debug.LogError($"Failed to find parent for item {item.FullName}");
                    }
                }
            }
            
            // (3) order by category so that they appear correctly

            result = result.OrderBy(item => (item as ToolTreeViewItem)?.FullName).ToList(); 

            return result;
        }

        private void CreateCategoryChain(string[] categories, IList<TreeViewItem> list, ref int nextId)
        {
            for (int i = 0; i < categories.Length; i++)
            {
                var fullCategoryName = string.Join("/", categories, 0, i + 1);
                var existingItem = list.FirstOrDefault(listItem => (listItem as ToolTreeViewItem)?.FullName == fullCategoryName);

                if (existingItem != null)
                {
                    continue;
                }

                var item = new ToolTreeViewItem(
                    nextId++,
                    0,
                    fullCategoryName,
                    categories[i]);

                list.Add(item);
            }
        }

        private void AddContainerBasedParents(string fullPathName, IList<TreeViewItem> list, ref int nextId)
        {
            var split = fullPathName.Split('/');

            for (int i = 0; i < split.Length; i++)
            {
                var splitName = string.Join("/", split, 0, i + 1);
                
                if (list.All(item => item.displayName != splitName))
                {
                    var item = new ToolTreeViewItem(nextId++, i, splitName, null);

                    if (i > 0)
                    {
                        // there should be a parent for this item if this is a split item beyond index 0
                        var parentName = string.Join("/", split, 0, i);
                        var parent = Get(parentName, list);
                        item.parent = parent;

                        if (parent.children == null)
                        {
                            parent.children = new List<TreeViewItem>();
                        }
                        
                        parent.children.Add(item);
                    }
                    
                    item.displayName = split[i];
                    item.icon = TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.Group) as Texture2D;
                    list.Add(item);
                }
            }
        }

        private TreeViewItem Get(string fullPathName, IList<TreeViewItem> list)
        {
            foreach (var item in list)
            {
                if (item is ToolTreeViewItem toolTreeViewItem && toolTreeViewItem.FullName == fullPathName)
                {
                    return item;
                }
            }

            return null;
        }

        protected override void RowGUI(RowGUIArgs args)
        {
            var item = args.item as ToolTreeViewItem;

            for (int i = 0; i < args.GetNumVisibleColumns(); i++)
            {
                CellGUI(args.GetCellRect(i), item, (Column) args.GetColumn(i), ref args);
            }
        }

        private void CellGUI(Rect cellRect, ToolTreeViewItem item, Column column, ref RowGUIArgs args)
        {
            CenterRectUsingSingleLineHeight(ref cellRect);

            try
            {
                var labelRect = cellRect;
                labelRect.x += GetContentIndent(item);

                var label = item.Tool?.Title ?? item.displayName;

                var guiContent = new GUIContent
                {
                    text = label,
                    image = item.Tool == null
                        ? TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.Group)
                        : item.Tool.ResourceIcon,
                    tooltip = item.Tool?.Description ?? label
                };
                
                GUI.Label(labelRect, guiContent);
            }
            catch (Exception ex)
            {
                if (ex is ExitGUIException exitGUIException)
                {
                    throw exitGUIException;
                }
                else
                {
                    Debug.LogError($"Failed to draw row, reason: {ex.Message}");
                }
            }
        }

        #endregion
    }
}