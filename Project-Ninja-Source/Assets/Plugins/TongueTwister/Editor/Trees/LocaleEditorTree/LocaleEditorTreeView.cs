using System;
using System.Collections.Generic;
using TongueTwister.Editor.Utilities;
using TongueTwister.Utilities;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace TongueTwister.Editor.Trees.LocaleEditorTree
{
    public class LocaleEditorTreeView : TreeView
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
            Status,
            Icon,
            Code,
            Name
        }

        #endregion

        #region Private

        /// <summary>
        /// The tree model which contains all tree data.
        /// </summary>
        private LocaleEditorTree _tree;

        /// <summary>
        /// The rows of the tree view items themselves.
        /// </summary>
        private readonly List<TreeViewItem> _rows = new List<TreeViewItem>();

        private GUIStyle _centeredStyle;

        #endregion

        #region Properties

        public LocaleEditorTree tree => _tree;

        private GUIStyle centeredStyle
        {
            get
            {
                if (_centeredStyle == null)
                {
                    _centeredStyle = new GUIStyle()
                    {
                        alignment = TextAnchor.MiddleCenter
                    };
                }

                return _centeredStyle;
            }
        }
        
        #endregion
        
        #region Constructors & Initialization
        
        public LocaleEditorTreeView(
            TreeViewState state, 
            MultiColumnHeader multiColumnHeader,
            LocaleEditorTree tree,
            bool showAlternatingRowBackgrounds = false) 
            : base(state, multiColumnHeader)
        {
            Initialize(tree, multiColumnHeader, showAlternatingRowBackgrounds);
        }

        private void Initialize(
            LocaleEditorTree tree,
            MultiColumnHeader multiColumnHeader = null,
            bool showAlternatingRowBackgrounds = false)
        {
            _tree = tree;

            if (multiColumnHeader != null)
            {
                rowHeight = CONST_ROW_HEIGHT;
                columnIndexForTreeFoldouts = 1; // label/name column
                this.showAlternatingRowBackgrounds = showAlternatingRowBackgrounds;
                showBorder = true;
                extraSpaceBeforeIconAndLabel = CONST_ROW_EXTRA_SPACE_BEFORE_ICON_AND_LABEL;
                multiColumnHeader.sortingChanged += OnSortingChanged;
            }

            _tree.OnModelChanged += OnModelChanged;

            Reload();
        }

        #endregion

        #region Protected & Private Utilities

        protected override TreeViewItem BuildRoot() => _tree.Root;

        protected override IList<TreeViewItem> BuildRows(TreeViewItem root)
        {
            _rows.Clear();

            var hasSearchString = !string.IsNullOrWhiteSpace(searchString);

            foreach (var data in tree.Data)
            {
                if (hasSearchString)
                {
                    if (data.Metadata.DisplayName.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        data.Metadata.NativeName.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        data.Metadata.CustomCode.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        _rows.Add(new LocaleEditorTreeViewItem(
                            _tree.Data.IndexOf(data), 
                            0, 
                            data.Metadata.DisplayName, 
                            data));
                    }
                }
                else
                {
                    _rows.Add(new LocaleEditorTreeViewItem(
                        tree.Data.IndexOf(data),
                        0,
                        data.Metadata.DisplayName,
                        data
                    ));
                }
            }

            if (hasSearchString)
            {
                _rows.Sort((x, y) => EditorUtility.NaturalCompare(x.displayName, y.displayName));
            }

            return _rows;
        }

        protected override void RowGUI(RowGUIArgs args)
        {
            var item = args.item as LocaleEditorTreeViewItem;

            for (int i = 0; i < args.GetNumVisibleColumns(); i++)
            {
                CellGUI(args.GetCellRect(i), item, (Column) args.GetColumn(i), ref args);
            }
        }

        private void CellGUI(Rect cellRect, LocaleEditorTreeViewItem item, Column column, ref RowGUIArgs args)
        {
            CenterRectUsingSingleLineHeight(ref cellRect);

            try
            {
                switch (column)
                {
                    case Column.Status:
                        if (TongueTwisterWindow.CurrentWindow?.LocalizationManagerEditor?.DefaultLocaleId == item.Data.Id)
                        {
                            GUI.Label(
                                cellRect,
                                new GUIContent(
                                    TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.DefaultSystemLocale),
                                    "This is the default locale."),
                                centeredStyle);
                        }
                        break;
                    
                    case Column.Icon:
                        var iconRect = cellRect;
                        iconRect.width = 30;
                        if (item.Data.Icon)
                        {
                            GUI.Label(iconRect, item.Data.Icon, centeredStyle);
                        }
                        else
                        {
                            GUI.Label(iconRect, TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.DefaultLocale), centeredStyle);
                        }
                        break;
                    
                    case Column.Code:
                        var codeRect = cellRect;
                        codeRect.x += CONST_GENERAL_COLUMN_CONTENT_INDENT;
                        codeRect.width = CONST_CODE_COLUMN_CONTENT_WIDTH;
                        var codeLabel = "N/A";
                        if (item.Data.Metadata.ISOCountryCode != ISO3166Alpha2.NONE)
                        {
                            codeLabel = item.Data.Metadata.ISOLanguageCode != ISO639Alpha2.NONE 
                                ? $"{item.Data.Metadata.ISOCountryCode}-{item.Data.Metadata.ISOLanguageCode}" 
                                : $"{item.Data.Metadata.ISOCountryCode}";
                        }
                        GUI.Label(codeRect, codeLabel);
                        break;
                    
                    case Column.Name:
                        var nameRect = cellRect;
                        nameRect.x += CONST_GENERAL_COLUMN_CONTENT_INDENT;
                        GUI.Label(nameRect, item?.Data?.Metadata?.DisplayName ?? "Unnamed Locale");
                        break;
                }
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

        #region Utilities

        /// <summary>
        /// Creates the default "multi column header" state for all columns.
        /// </summary>
        /// <returns>The default header state.</returns>
        public static MultiColumnHeaderState CreateDefaultMultiColumnHeaderState()
        {
            var columns = new[] 
            {
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent(
                        TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.LocaleStatusColumn), 
                        "General status of the locale."), 
                    contextMenuText = "Info",
                    headerTextAlignment = TextAlignment.Center,
                    sortedAscending = true,
                    sortingArrowAlignment = TextAlignment.Right,
                    width = CONST_ICON_COLUMN_CONTENT_WIDTH, 
                    minWidth = CONST_ICON_COLUMN_CONTENT_WIDTH,
                    maxWidth = CONST_ICON_COLUMN_CONTENT_WIDTH,
                    autoResize = false,
                    allowToggleVisibility = true
                }, 
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent(
                        TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.LocaleIconColumn), 
                        "Icon representing the locale."), 
                    contextMenuText = "Icon",
                    headerTextAlignment = TextAlignment.Center,
                    sortedAscending = true,
                    sortingArrowAlignment = TextAlignment.Right,
                    width = CONST_ICON_COLUMN_CONTENT_WIDTH, 
                    minWidth = CONST_ICON_COLUMN_CONTENT_WIDTH,
                    maxWidth = CONST_ICON_COLUMN_CONTENT_WIDTH,
                    autoResize = false,
                    allowToggleVisibility = true
                }, 
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("Code", "The locale's localization code."), //new GUIContent(EditorGUIUtility.FindTexture("Collab.Warning"), "Filter by status"),
                    contextMenuText = "Code",
                    headerTextAlignment = TextAlignment.Center,
                    sortedAscending = true,
                    sortingArrowAlignment = TextAlignment.Right,
                    width = CONST_CODE_COLUMN_CONTENT_WIDTH, 
                    minWidth = CONST_CODE_COLUMN_CONTENT_WIDTH,
                    maxWidth = CONST_CODE_COLUMN_CONTENT_WIDTH * 2.0f,
                    autoResize = false,
                    allowToggleVisibility = true
                }, 
                new MultiColumnHeaderState.Column
                {
                    headerContent = new GUIContent("Display Name"),
                    headerTextAlignment = TextAlignment.Left,
                    sortedAscending = true,
                    sortingArrowAlignment = TextAlignment.Center,
                    width = 9001, 
                    minWidth = 60,
                    autoResize = true,
                    allowToggleVisibility = false
                },
            };

            var state =  new MultiColumnHeaderState(columns);
            return state;
        }

        /// <summary>
        /// Deselects the current selection of locales.
        /// </summary>
        public void Deselect()
        {
            SetSelection(new List<int>());
        }

        #endregion

        #region Events

        private void OnModelChanged()
        {
            
        }
        
        private void OnSortingChanged(MultiColumnHeader multiColumnHeader)
        {
            // SortIfNeeded (rootItem, GetRows());
        }

        #endregion
    }
}