using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TongueTwister.Editor.Utilities;
using TongueTwister.Extensions;
using TongueTwister.Models;
using TongueTwister.StaticLabels;
using TongueTwister.Validation;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace TongueTwister.Editor.Trees.ModelEditorTree
{
	/// <summary>
	/// Custom implementation of the UnityEditor tree view control. Documentation support for this class is not
	/// entirely completed or "good" in any regards as it should not be edited or worried about by the consumer.
	/// </summary>
	public class ModelEditorTreeView : TreeView
	{
		#region Constants & Static Readonly

		/// <summary>
		/// The height of each row in the tree view.
		/// </summary>
		private const float CONST_ROW_HEIGHT = 20.0f;

		/// <summary>
		/// The width of icons appearing in the first column of the tree view.
		/// </summary>
		private const float CONST_ICON_WIDTH = 20.0f;

		/// <summary>
		/// The padding or "extra space" that exists to the left of an icon and label in a tree view row.
		/// </summary>
		private const float CONST_ROW_EXTRA_SPACE_BEFORE_ICON_AND_LABEL = 20.0f;

		private const string CONST_ERROR_WARNING_TOOLTIP_ITEM = "• {0}";

		/// <summary>
		/// The sortable options for the tree view columns.
		/// </summary>
		private static readonly SortOption[] SortOptions =
		{
			SortOption.Status,
			SortOption.Label
		};

	    #endregion

	    #region Enumerations

	    /// <summary>
	    /// Column enumeration describes each column type in the tree view.
	    /// </summary>
	    private enum Column
	    {
		    Status,
		    Label
	    }

	    /// <summary>
	    /// Sort option enumeration describes each sort option type for rows of the tree view.
	    /// </summary>
	    private enum SortOption
	    {
		    Status,
		    Label
	    }

	    #endregion
	    
        #region Private

        /// <summary>
        /// The tree model which contains all tree data.
        /// </summary>
        private ModelEditorTree _tree;

        /// <summary>
        /// The rows of the tree view items themselves.
        /// </summary>
        private readonly List<TreeViewItem> _rows = new List<TreeViewItem>();

        #endregion
        
        #region Properties
        
        public ModelEditorTree tree => _tree;

        /// <summary>
        /// Event that occurs whenever something in the tree changes.
        /// </summary>
        public event Action OnTreeChanged; 
        
        /// <summary>
        /// Event that occurs when a list of tree view items are being dragged then suddenly dropped somewhere.
        /// </summary>
        public event Action<IList<TreeViewItem>>  OnDroppedDraggedItems;

        #endregion
        
        #region Constructors

        /// <summary>
        /// Used for multi column tree view GUI.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="multiColumnHeader"></param>
        /// <param name="model"></param>
        /// <param name="showAlternatingRowBackgrounds"></param>
        public ModelEditorTreeView(
	        TreeViewState state, 
	        MultiColumnHeader multiColumnHeader, 
	        ModelEditorTree model, 
	        bool showAlternatingRowBackgrounds = true) 
            : base(state, multiColumnHeader)
        {
            Initialize(model, multiColumnHeader, showAlternatingRowBackgrounds);
        }

        /// <summary>
        /// Initializes the data.
        /// </summary>
        /// <param name="model">The root model.</param>
        /// <param name="multiColumnHeader">The multi column header to use.</param>
        /// <param name="showAlternatingRowBackgrounds">Whether or not to show alternating row background colors.</param>
        private void Initialize(
	        ModelEditorTree model, 
	        MultiColumnHeader multiColumnHeader = null, 
	        bool showAlternatingRowBackgrounds = true)
        {
	        _tree = model;

	        RunRootErrorCheck(
		        TongueTwisterWindow.CurrentWindow?.HasLocalizationManager == true
			        ? TongueTwisterWindow.CurrentWindow.EnabledValidationRules
			        : ValidationUtility.GetAllValidationRules());

	        if (multiColumnHeader != null)
            {
	            rowHeight = CONST_ROW_HEIGHT;
	            columnIndexForTreeFoldouts = 1; // the "label" column
	            this.showAlternatingRowBackgrounds = showAlternatingRowBackgrounds;
	            showBorder = true;
	            extraSpaceBeforeIconAndLabel = CONST_ROW_EXTRA_SPACE_BEFORE_ICON_AND_LABEL;
	            multiColumnHeader.sortingChanged += OnSortingChanged;
            }

            _tree.OnModelChanged += OnChanged;
            
            EnableDeselectOnUnhandledMouseClick();
            Reload();
        }
        
        #endregion

        #region Protected & Private Utilities

        /// <summary>
        /// Unity hides a valuable field in the <see cref="TreeView"/> class which requires Reflection to enable. Doing
        /// so will allow the TreeView to "deselect" the current selection whenever the user clicks empty space.
        /// However, any changes to delayed text fields in this scenario may be lost if the user didn't commit them.
        /// </summary>
        private void EnableDeselectOnUnhandledMouseClick()
        {
	        try
	        {
		        typeof(TreeView).GetRuntimeProperties()
			        .First(prop => prop.Name == "deselectOnUnhandledMouseDown")
			        .SetValue(this, true);
	        }
	        catch { /* do nothing where this may not be supported in some versions of Unity */ }
        }

        /// <summary>
        /// Creates a tree view item out of the root of the tree model.
        /// </summary>
        /// <returns>The root of the tree model.</returns>
        protected override TreeViewItem BuildRoot()
            => new ModelEditorTreeViewItem(_tree.Root.Id, -1, _tree.Root.Name, _tree.Root);

        /// <summary>
        /// Creates a list of rows out of a tree view item that represents the root of the data.
        /// </summary>
        /// <param name="root">The root node of the tree view model.</param>
        /// <returns>Structured list of rows created from the root of the data.</returns>
        protected override IList<TreeViewItem> BuildRows(TreeViewItem root)
        {
            if (_tree.Root == null)
            {
                Debug.LogError ("Root is null. Did you call SetData()?");
            }

            _rows.Clear ();
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                Search (_tree.Root, searchString, _rows);
            }
            else
            {
                if (_tree?.Root?.HasChildren == true)
                    RecursivelyAddChildren(_tree.Root, 0, _rows);
            }

            // We still need to setup the child parent information for the rows since this 
            // information is used by the TreeView internal logic (navigation, dragging etc)
            SetupParentsAndChildrenFromDepths (root, _rows);

            SortIfNeeded(root, _rows);
            
            return _rows;
        }
        
        /// <summary>
        /// Recursively adds new children to a parent with the given depth. This is useful for adding new items in
        /// bulk to a model (such as copy/paste, duplication, and drag/drop actions).
        /// </summary>
        /// <param name="parent">The parent model node that will receive the new content.</param>
        /// <param name="depth">The new depth of the children</param>
        /// <param name="newRows">The new rows to add to the parent.</param>
        private void RecursivelyAddChildren (TongueTwisterModel parent, int depth, IList<TreeViewItem> newRows)
        {
            foreach (TongueTwisterModel child in parent.Children)
            {
                var item = new ModelEditorTreeViewItem(child.Id, depth, child.Name, child);
                newRows.Add(item);

                if (child.HasChildren)
                {
                    if (IsExpanded(child.Id))
                    {
                        RecursivelyAddChildren (child, depth + 1, newRows);
                    }
                    else
                    {
                        item.children = CreateChildListForCollapsedParent();
                    }
                }
            }
        }

		/// <summary>
		/// Searches the tree model with the given filter. This is default Unity provided function.
		/// </summary>
		/// <param name="searchFromThis">The model to start searching from (including its children).</param>
		/// <param name="search">The string value of the search content desired.</param>
		/// <param name="result">The pointer results of the search.</param>
		/// <exception cref="ArgumentException">Thrown when the <c>search</c> parameter is invalid.</exception>
        private void Search(TongueTwisterModel searchFromThis, string search, List<TreeViewItem> result)
        {
            if (string.IsNullOrEmpty(search))
                throw new ArgumentException("Invalid search: cannot be null or empty", "search");

            const int kItemDepth = 0; // tree is flattened when searching

            Stack<TongueTwisterModel> stack = new Stack<TongueTwisterModel>();
            foreach (var element in searchFromThis.Children)
                stack.Push(element);
            
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                // Matches search?
                if (current.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    result.Add(new ModelEditorTreeViewItem(current.Id, kItemDepth, current.Name, current));
                }

                if (current.Children != null && current.Children.Count > 0)
                {
                    foreach (var element in current.Children)
                    {
                        stack.Push(element);
                    }
                }
            }
            SortSearchResult(result);
        }
        
		/// <summary>
		/// Uses the <c>EditorUtility</c>'s "NaturalCompare" algorithm to sort search results. This is a default Unity
		/// provided function.
		/// </summary>
		/// <param name="rows">The search results.</param>
        protected virtual void SortSearchResult (List<TreeViewItem> rows)
        {
            rows.Sort ((x,y) => EditorUtility.NaturalCompare (x.displayName, y.displayName)); // sort by displayName by default, can be overriden for multicolumn solutions
        }
        
		/// <summary>
		/// Gets all ancestors of the given model ID.
		/// </summary>
		/// <param name="id">Model ID to get ancestors of.</param>
		/// <returns>List of ancestor ("children") IDs.</returns>
        protected override IList<int> GetAncestors (int id)
	        => _tree.GetAncestors(id);
        
		/// <summary>
		/// Gets all descendants of the given model ID which have children.
		/// </summary>
		/// <param name="id">Model ID.</param>
		/// <returns>List of all ID's of ancestors/descendants ("children") that also have children.</returns>
        protected override IList<int> GetDescendantsThatHaveChildren (int id)
	        => _tree.GetDescendantsThatHaveChildren(id);
        
        /// <summary>
        /// "Sorts if needed" a list of rows for the given root. This is a default Unity provided function.
        /// </summary>
        /// <param name="root">Root of all the given rows.</param>
        /// <param name="rows">Rows that belong to the given root.</param>
        void SortIfNeeded (TreeViewItem root, IList<TreeViewItem> rows)
        {
	        if (rows.Count <= 1)
		        return;
			
	        if (multiColumnHeader.sortedColumnIndex == -1)
	        {
		        return; // No column to sort for (just use the order the data are in)
	        }
			
	        // Sort the roots of the existing tree items
	        SortByMultipleColumns ();
	        TreeToList(root, rows);
	        Repaint();
        }

        private string BuildModelWarningErrorTooltip(ModelEditorTreeViewItem viewItem)
        {
	        var result = new List<string>();

	        if (viewItem.Data.HasErrors)
	        {
		        foreach (var error in viewItem.Data.ModelRuleViolations)
		        {
			        if (error.RuleViolationSeverity != RuleViolationSeverityType.Error) continue;
			        
			        result.Add(
				        string.Format(
					        CONST_ERROR_WARNING_TOOLTIP_ITEM,
					        error.Message));
		        }
	        }

	        if (viewItem.Data.HasWarnings)
	        {
		        foreach (var warning in viewItem.Data.ModelRuleViolations)
		        {
			        if (warning.RuleViolationSeverity != RuleViolationSeverityType.Warning) continue;

			        result.Add(
				        string.Format(
					        CONST_ERROR_WARNING_TOOLTIP_ITEM,
					        warning.Message));
		        }
	        }
	        
	        return string.Join("\n\n", result);
        }

        /// <summary>
        /// Ensures no localization is ever a parent. Rules are performed elsewhere to ensure the correct model types
        /// are enforced.
        /// </summary>
        /// <param name="item">The expecting parent item.</param>
        /// <returns>Whether or not the item can be a parent.</returns>
        protected override bool CanBeParent(TreeViewItem item)
        {
	        if (item is ModelEditorTreeViewItem treeViewItemExtended)
	        {
		        if (treeViewItemExtended.Data.Type == TongueTwisterModel.ModelType.Localization) return false;
	        }
	        
	        return base.CanBeParent(item);
        }

        protected override void ContextClickedItem(int id)
        {
	        base.ContextClickedItem(id);

	        var item = FindItem(id, rootItem) as ModelEditorTreeViewItem;
	        if (item == null)
	        {
		        return;
	        }
	        
	        var contextMenu = new GenericMenu();

	        if (item.Data.Type == TongueTwisterModel.ModelType.Group)
	        {
		        contextMenu.AddItem(new GUIContent("Add Group"), false, OnContextMenuSelection, "0");
		        contextMenu.AddItem(new GUIContent("Add Display Key"), false, OnContextMenuSelection, "1");
	        }

	        if (item.Data.Type == TongueTwisterModel.ModelType.DisplayKey)
	        {
		        contextMenu.AddItem(new GUIContent("Add Localization"), false, OnContextMenuSelection, "2");
	        }

	        contextMenu.AddSeparator(string.Empty);

	        contextMenu.AddItem(new GUIContent("Duplicate"), false, OnContextMenuSelection, "3");
	        contextMenu.AddItem(new GUIContent("Copy"), false, OnContextMenuSelection, "4");
	        contextMenu.AddItem(new GUIContent("Paste"), false, OnContextMenuSelection, "5");
	        contextMenu.AddItem(new GUIContent("Remove"), false, OnContextMenuSelection, "6");
	        
	        contextMenu.AddSeparator(string.Empty);
	        
	        contextMenu.ShowAsContext();
        }

        #endregion

        #region Public Utilities

        /// <summary>
        /// Forces the tree model to be marked as changed which will invoke the "OnTreeChanged" event.
        /// </summary>
        public void MarkTreeModelAsChanged() => tree.MarkAsChanged();

        /// <summary>
        /// Sets the internal selection of the tree view to the given model ID.
        /// </summary>
        /// <param name="id">Model to set the selection to.</param>
        public void SetSelection(int id)
        {
	        GUI.FocusControl(null);
	        Repaint();
	        
	        if (id >= 0)
	        {
		        base.SetSelection(new List<int>() {id});
	        }
	        else
	        {
		        base.SetSelection(new List<int>());
	        }
        }

        /// <summary>
        /// Clears the internal selection of the tree view.
        /// </summary>
        public void Deselect() => SetSelection(-1);

        /// <summary>
        /// Returns the current selection, choosing whether or not to filter valid IDs.
        /// </summary>
        /// <param name="filterInvalidIds">Whether or not to filter valid IDs</param>
        /// <returns>The selection of IDs.</returns>
        public IList<int> GetSelection(bool filterInvalidIds = true)
        {
	        var selection = base.GetSelection();

	        if (filterInvalidIds)
	        {
		        var rows = FindRows(selection);
		        return selection.Where(id => rows.Select(item => item.id).Contains(id)).ToList();
	        }

	        return selection;
        }

        /// <summary>
        /// Calling the base function <c>HasSelection()</c> will return true even if <see cref="GetSelection"/> returns
        /// 0 items. This is because <see cref="GetSelection"/> only handles visibly selected items. This function will
        /// check to see if <see cref="GetSelection"/> has any items, which would mean that there are visibly selected
        /// items amongst the tree view. Otherwise, the current selection is hidden by closed tree view items.
        /// </summary>
        /// <returns>True if any selected items are currently visible.</returns>
        public bool HasVisibleSelection() => GetSelection().Count > 0;
        
        /// <summary>
        /// Starts an error check from the root node model using TongueTwisterWindow settings if available.
        /// </summary>
        public void RunRootErrorCheck(List<ValidationRule> validationRules)
        {
	        if (_tree == null || !TongueTwisterWindow.CurrentWindow?.LocalizationManagerEditor) return;

	        _tree.Root.RunRuleCheck(validationRules);
        }

        #endregion

        #region GUI

        /// <summary>
        /// Draw row GUI given arguments.
        /// </summary>
        /// <param name="args">The row GUI arguments</param>
        protected override void RowGUI (RowGUIArgs args)
        {
	        var item = args.item as ModelEditorTreeViewItem;

	        for (int i = 0; i < args.GetNumVisibleColumns (); ++i)
			{
				CellGUI(args.GetCellRect(i), item, (Column)args.GetColumn(i), ref args);
			}
		}

        /// <summary>
        /// Draw cell GUI.
        /// </summary>
        /// <param name="cellRect">The cell's rect.</param>
        /// <param name="viewItem">The tree view item to fill the cell with.</param>
        /// <param name="column">The current column the cell will be drawn in.</param>
        /// <param name="args">The row GUI arguments.</param>
		void CellGUI (Rect cellRect, ModelEditorTreeViewItem viewItem, Column column, ref RowGUIArgs args)
		{
			CenterRectUsingSingleLineHeight(ref cellRect);

			try
			{
				switch (column)
				{
					case Column.Status:
						if (viewItem.Data.HasErrors)
						{
							GUI.Label(
								cellRect,
								new GUIContent(
									TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.Error),
									BuildModelWarningErrorTooltip(viewItem)));
						}
						else if (viewItem.Data.HasWarnings)
						{
							GUI.Label(
								cellRect,
								new GUIContent(
									TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.Warning),
									BuildModelWarningErrorTooltip(viewItem)));
						}
						break;
					
					case Column.Label:
						var iconRect = cellRect;
						iconRect.x += GetContentIndent(viewItem);
						iconRect.width = CONST_ICON_WIDTH;

						Texture2D icon = null;
						string displayName = "";

						switch (viewItem.Data.Type)
						{
							case TongueTwisterModel.ModelType.Localization:
								var locale = TongueTwisterWindow.CurrentWindow?.LocalizationManager.GetLocaleById(viewItem.Data.LocaleId);
								
								icon = locale != null && locale.Icon
									? (Texture2D) locale.Icon
									: TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.Localization) as Texture2D;
								
								displayName = locale != null 
									? locale?.Metadata?.DisplayName
									: EditorLabels.LocalizationManager.EditorModes.LocaleEditing.LocaleNotSelectedForLocalization;
								
								break;
							
							default:
								icon = TongueTwisterIconUtility.GetIcon(viewItem.Data.Type) as Texture2D;
								displayName = viewItem.Data.Name;
								break;
						}
						
						GUI.Label(iconRect, icon); 
						args.rowRect = cellRect;
						args.label = displayName;

						base.RowGUI(args);
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

        #region Sorting & Ordering

        /// <summary>
        /// Functionality to sort columns when multiple columns have been selected.
        /// </summary>
        void SortByMultipleColumns ()
        {
	        var sortedColumns = multiColumnHeader.state.sortedColumns;

	        if (sortedColumns.Length == 0)
		        return;

	        var myTypes = rootItem.children.Cast<ModelEditorTreeViewItem>();
	        var orderedQuery = InitialOrder (myTypes, sortedColumns);
	        for (int i=1; i<sortedColumns.Length; i++)
	        {
		        SortOption sortOption = SortOptions[sortedColumns[i]];
		        bool ascending = multiColumnHeader.IsSortedAscending(sortedColumns[i]);

		        switch (sortOption)
		        {
			        case SortOption.Status:
				        orderedQuery = orderedQuery.ThenBy((item) =>
				        {
					        if (item.Data.HasErrors) return 2;
					        if (item.Data.HasWarnings) return 1;
					        return 0;
				        }, ascending);
				        break;
			        case SortOption.Label:
				        orderedQuery = orderedQuery.ThenBy(item => item.Data.Name, ascending);
				        break;
		        }
	        }

	        rootItem.children = orderedQuery.Cast<TreeViewItem> ().ToList ();
        }
        
        /// <summary>
        /// Creates the initial ordering of items.
        /// </summary>
        /// <param name="itemElements">An <see cref="IEnumerable{T}"/> of <see cref="ModelEditorTreeViewItem"/></param>
        /// <param name="history">A history of ID's.</param>
        /// <returns>An ordered list.</returns>
        IOrderedEnumerable<ModelEditorTreeViewItem> InitialOrder(IEnumerable<ModelEditorTreeViewItem> itemElements, int[] history)
        {
	        SortOption sortOption = SortOptions[history[0]];
	        bool ascending = multiColumnHeader.IsSortedAscending(history[0]);
	        switch (sortOption)
	        {
		        case SortOption.Status:
			        return itemElements.Order((item) =>
			        {
				        if (item.Data.HasErrors) return 2;
				        if (item.Data.HasWarnings) return 1;
				        return 0;
			        }, ascending);
		        case SortOption.Label:
			        return itemElements.Order(item => item.Data.Name, ascending);
	        }

	        return itemElements.Order(item => item.Data.Name, ascending);
        }


        #endregion

        #region Static Utilities

        /// <summary>
        /// Converts a tree view (based on a root <see cref="TreeViewItem"/>) into an <see cref="IList{T}"/> of
        /// <see cref="TreeViewItem"/> objects.
        /// </summary>
        /// <param name="root">The initial <see cref="TreeViewItem"/>.</param>
        /// <param name="result">The output <see cref="IList{T}"/></param>
        /// <exception cref="NullReferenceException">Thrown when the root object or result list is null.</exception>
        public static void TreeToList (TreeViewItem root, IList<TreeViewItem> result)
        {
        	if (root == null)
        		throw new NullReferenceException("root");
        	if (result == null)
        		throw new NullReferenceException("result");

        	result.Clear();
    
        	if (root.children == null)
        		return;

        	Stack<TreeViewItem> stack = new Stack<TreeViewItem>();
        	for (int i = root.children.Count - 1; i >= 0; i--)
        		stack.Push(root.children[i]);

        	while (stack.Count > 0)
        	{
        		TreeViewItem current = stack.Pop();
        		result.Add(current);

        		if (current.hasChildren && current.children[0] != null)
        		{
        			for (int i = current.children.Count - 1; i >= 0; i--)
        			{
        				stack.Push(current.children[i]);
        			}
        		}
        	}
        }
        
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
					headerContent = new GUIContent(EditorGUIUtility.FindTexture("Collab.Warning"), "Filter by status"),
					contextMenuText = "Status",
					headerTextAlignment = TextAlignment.Center,
					sortedAscending = true,
					sortingArrowAlignment = TextAlignment.Right,
					width = 30, 
					minWidth = 30,
					maxWidth = 60,
					autoResize = false,
					allowToggleVisibility = true
				}, 
				new MultiColumnHeaderState.Column
				{
					headerContent = new GUIContent("Item"),
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

        #endregion

        #region Events

        /// <summary>
        /// Monitors for selection changed event. When nothing was selected/changed, a GUI exit will occur.
        /// </summary>
        /// <param name="selectedIds"></param>
        protected override void SelectionChanged(IList<int> selectedIds)
        {
	        if (selectedIds == null || selectedIds.Count == 0)
	        {
		        GUIUtility.ExitGUI();
	        }
        }

        /// <summary>
        /// Called whenever the model is changed. This invokes <see cref="TreeView.Reload"/>,
        /// <see cref="OnTreeChanged"/>, and the tree model's root's
        /// <see cref="RunRootErrorCheck"/> to look for issues.
        /// </summary>
        private void OnChanged()
        {
	        if (TongueTwisterWindow.CurrentWindow?.HasLocalizationManager ==  true)
	        {
		        if (!TongueTwisterWindow.CurrentWindow.ManualErrorChecks)
		        {
			        RunRootErrorCheck(TongueTwisterWindow.CurrentWindow.EnabledValidationRules);
		        }
	        }
	        else
	        {
		        RunRootErrorCheck(ValidationUtility.GetAllValidationRules());
	        }

	        OnTreeChanged?.Invoke();
            
            OnCustomModelChanged();
            
            Reload();
        }

        /// <summary>
        ///  Called before <c>Reload</c> and after <see cref="OnTreeChanged"/> in <see cref="OnChanged"/>. 
        /// </summary>
        protected virtual void OnCustomModelChanged()
        {
	        
        }

        /// <summary>
        /// Called whenever sorting is changed given the multi column header.
        /// </summary>
        /// <param name="multiColumnHeader">The multi column header where sorting changed.</param>
        private void OnSortingChanged(MultiColumnHeader multiColumnHeader)
        {
	        SortIfNeeded (rootItem, GetRows());
        }

        /// <summary>
        /// Called when a selection from the TreeView's context menu has been made.
        /// </summary>
        /// <param name="obj">The contextual OBJ.</param>
        private void OnContextMenuSelection(object obj)
        {
	        switch (obj as string)
	        {
		        case "0": // add new group
			        TongueTwisterWindow.CurrentWindow.AddNewModelToSelectionOrRoot(TongueTwisterModel.ModelType.Group);
			        break;
		        case "1": // add new display key
			        TongueTwisterWindow.CurrentWindow.AddNewModelToSelectionOrRoot(TongueTwisterModel.ModelType.DisplayKey);
			        break;
		        case "2": // add new localization
			        TongueTwisterWindow.CurrentWindow.AddNewModelToSelectionOrRoot(TongueTwisterModel.ModelType.Localization);
			        break;
		        case "3": // duplicate
			        TongueTwisterWindow.CurrentWindow.DuplicateModelEditorSelection();
			        break;
		        case "4": // copy selection
			        TongueTwisterWindow.CurrentWindow.CopyModelEditorSelection();
			        break;
		        case "5": // paste selection
			        TongueTwisterWindow.CurrentWindow.PasteModelEditorSelection();
			        break;
		        case "6": // delete selection
			        TongueTwisterWindow.CurrentWindow.DeleteModelEditorSelection();
			        break;
		        default:
			        OnCustomContextMenuSelection(obj);
			        break;
	        }
        }
        
        protected virtual void OnCustomContextMenuSelection(object obj) { }

        #endregion

        #region Dragging

        const string k_GenericDragID = "GenericDragColumnDragging";

		protected override bool CanStartDrag (CanStartDragArgs args)
		{
			return true;
		}

		protected override void SetupDragAndDrop(SetupDragAndDropArgs args)
		{
			if (hasSearch)
				return;

			DragAndDrop.PrepareStartDrag();
			var draggedRows = GetRows().Where(item => args.draggedItemIDs.Contains(item.id)).ToList();
			DragAndDrop.SetGenericData(k_GenericDragID, draggedRows);
			DragAndDrop.objectReferences = new UnityEngine.Object[] { };
			string title = draggedRows.Count == 1 ? draggedRows[0].displayName : "< Multiple >";
			DragAndDrop.StartDrag (title);
		}

		protected override DragAndDropVisualMode HandleDragAndDrop (DragAndDropArgs args)
		{
			var result = DragAndDropVisualMode.None;
			var draggedRows = DragAndDrop.GetGenericData(k_GenericDragID) as List<TreeViewItem>;
			if (draggedRows == null)
				return DragAndDropVisualMode.None;

			switch (args.dragAndDropPosition)
			{
				case DragAndDropPosition.UponItem:
				case DragAndDropPosition.BetweenItems:
					bool validDrag = ValidDrag(args.parentItem, draggedRows);
					if (args.performDrop && validDrag)
					{
						TongueTwisterModel parentData = ((ModelEditorTreeViewItem)args.parentItem).Data;
						OnDropDraggedElementsAtIndex(draggedRows, parentData, args.insertAtIndex == -1 ? 0 : args.insertAtIndex);
					}
					result = validDrag ? DragAndDropVisualMode.Move : DragAndDropVisualMode.None;
					break;
				case DragAndDropPosition.OutsideItems:
					if (args.performDrop)
						OnDropDraggedElementsAtIndex(draggedRows, _tree.Root, _tree.Root.Children.Count);

					result = DragAndDropVisualMode.Move;
					break;
				default:
					Debug.LogError("Unhandled enum " + args.dragAndDropPosition);
					break;
			}

			OnCustomHandleDragAndDrop(args);
			
			return result;
		}

		protected virtual void OnCustomHandleDragAndDrop(DragAndDropArgs args)
		{
			
		}

		/// <summary>
		/// Called externally from any code wishing to alert the tree view that the "on-drop-drag" of elements has
		/// occurred at the given index with the given parent model.
		/// </summary>
		/// <param name="draggedRows">The rows that were dragged from their original position into a new one.</param>
		/// <param name="parent">The destination parent of where the rows should be dragged to.</param>
		/// <param name="insertIndex">The index of where rows should be inserted.</param>
		public virtual void OnDropDraggedElementsAtIndex (List<TreeViewItem> draggedRows, TongueTwisterModel parent, int insertIndex)
		{
			// sort out items that cannot be dragged to other items using these rules:
			// 1. Display keys can only be dragged into groups
			// 2. Groups can only be dragged into other groups
			// 3. Localizations can only be dragged into display keys

			switch (parent.Type)
			{
				case TongueTwisterModel.ModelType.Group:
					draggedRows = draggedRows
						.Where(row => ((ModelEditorTreeViewItem) row).Data.Type != TongueTwisterModel.ModelType.Localization)
						.ToList();
					break;
				
				case TongueTwisterModel.ModelType.DisplayKey:
					draggedRows = draggedRows
						.Where(row => ((ModelEditorTreeViewItem) row).Data.Type == TongueTwisterModel.ModelType.Localization)
						.ToList();
					break;
				
				case TongueTwisterModel.ModelType.Localization:
					Debug.LogError("Cannot drag items onto localizations.");
					return; 
			}
			
			if (OnDroppedDraggedItems != null)
				OnDroppedDraggedItems (draggedRows);

			var draggedElements = new List<TongueTwisterModel> ();
			foreach (var x in draggedRows)
				draggedElements.Add (((ModelEditorTreeViewItem) x).Data);
		
			var selectedIDs = draggedElements.Select (x => x.Id).ToArray();
			_tree.MoveElements (parent, insertIndex, draggedElements);
			SetSelection(selectedIDs, TreeViewSelectionOptions.RevealAndFrame);
		}

		bool ValidDrag(TreeViewItem parent, List<TreeViewItem> draggedItems)
		{
			TreeViewItem currentParent = parent;
			while (currentParent != null)
			{
				if (draggedItems.Contains(currentParent))
					return false;
				currentParent = currentParent.parent;
			}
			return true;
		}

        #endregion
	}
}