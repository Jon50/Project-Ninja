using System;
using System.Collections.Generic;
using System.Linq;
using TongueTwister.Models;
using TongueTwister.StaticLabels;
using TongueTwister.Utilities;
using UnityEngine;

namespace TongueTwister.Editor.Trees.ModelEditorTree
{
	/// <summary>
	/// The model which holds all tree data for the TongueTwister editor.
	/// </summary>
    public class ModelEditorTree : TongueTwisterModel
    {
        #region Private

        /// <summary>
        /// The data for this tree model in the form of a list of models.
        /// </summary>
        private List<TongueTwisterModel> _data;

        /// <summary>
        /// The current max ID value used to create new models.
        /// </summary>
        private int _maxId; 

        #endregion
        
        #region Properties
        
        /// <summary>
        /// The starting node of the tree model. It's the furthest ancestor in the list and is not displayed in the
        /// editor.
        /// </summary>
        public TongueTwisterModel Root { get; set; }

        /// <summary>
        /// Event occurs whenever the model has changed.
        /// </summary>
        public event Action OnModelChanged;

        /// <summary>
        /// The data for this tree model.
        /// </summary>
        public List<TongueTwisterModel> Data => _data;

        #endregion
        
        #region Constructors

        /// <summary>
        /// Initializes this tree model with the given data.
        /// </summary>
        /// <param name="data"></param>
        private void Initialize(List<TongueTwisterModel> data)
        {
	        if (_data != data)
	        {
		        _data?.Clear();
		        _data = data ?? new List<TongueTwisterModel>();
	        }

	        Root = ModelUtility.ListToTree(_data) ?? new TongueTwisterModel("root", -1, -1, ModelType.Group);
            _maxId = _data.Count > 0 ? _data.Max(e => e.Id) : 0;
            OnModelChanged?.Invoke();
        }

        /// <summary>
        /// Constructs a TreeModel from a list of data.
        /// </summary>
        /// <param name="data"></param>
        public ModelEditorTree(List<TongueTwisterModel> data)
        {
            SetDataFromList(data); 
        }
        
        #endregion

        #region Utilities

        /// <summary>
        /// Finds a model with the given ID.
        /// </summary>
        /// <param name="id">ID of the model to search for.</param>
        /// <returns>Either a model with the given ID or null if one isn't found.</returns>
        public TongueTwisterModel Find(int id) => _data.FirstOrDefault(model => model.Id == id);

        /// <summary>
        /// Finds a model with the given name.
        /// </summary>
        /// <param name="name">Name of the model to search for.</param>
        /// <returns>Either a model with the given name or null if one isn't found.</returns>
        public TongueTwisterModel Find(string name) => _data.FirstOrDefault(model => model.Name == name);
        
        /// <summary>
        /// Re-initializes this tree model with the given data.
        /// </summary>
        /// <param name="data">The data to re-initialize this tree model with.</param>
        public void SetDataFromList(List<TongueTwisterModel> data) => Initialize(data);

        /// <summary>
        /// Gets the next available (unused) ID for a model.
        /// </summary>
        /// <returns>Next available (unused) ID.</returns>
        public int GetNextId() => ++_maxId;

        /// <summary>
        /// Gets all ancestor model ID's for the given ID.
        /// </summary>
        /// <param name="id">ID of the model to get ancestors for.</param>
        /// <returns>List of ancestor models.</returns>
        public IList<int> GetAncestors(int id)
        {
            var parents = new List<int>();
            
            var Model = Find(id);
            
            if (Model == null) return parents;
            
            while (Model.Parent != null)
            {
                parents.Add(Model.Parent.Id);
                Model = Model.Parent;
            }
            
            return parents;
        }
        
        /// <summary>
        /// Gets descendants of the given model ID that have children.
        /// </summary>
        /// <param name="id">ID of the model to get descendants for.</param>
        /// <returns>List of model ID's that are descendants which have children.</returns>
        public IList<int> GetDescendantsThatHaveChildren (int id)
		{
			var searchFromThis = Find(id);
			if (searchFromThis != null)
			{
				return GetParentsBelowStackBased(searchFromThis);
			}
			return new List<int>();
		}

		IList<int> GetParentsBelowStackBased(TongueTwisterModel searchFromThis)
		{
			Stack<TongueTwisterModel> stack = new Stack<TongueTwisterModel>();
			stack.Push(searchFromThis);

			var parentsBelow = new List<int>();
			while (stack.Count > 0)
			{
				TongueTwisterModel current = stack.Pop();
				if (current.HasChildren)
				{
					parentsBelow.Add(current.Id);
					foreach (var T in current.Children)
					{
						stack.Push(T);
					}
				}
			}

			return parentsBelow;
		}
		
		/// <summary>
		/// Removes an element from the data with the given element ID.
		/// </summary>
		/// <param name="elementId">ID of the element to remove.</param>
		public void RemoveElement (int elementId) => RemoveElements(new List<int>() { elementId });
		
		/// <summary>
		/// Removes an element from the data.
		/// </summary>
		/// <param name="element">The element to remove.</param>
		public void RemoveElement (TongueTwisterModel element) => RemoveElements(new List<TongueTwisterModel>() { element });

		/// <summary>
		/// Removes all elements with the given IDs from the data.
		/// </summary>
		/// <param name="elementIDs">The IDs of the elements to remove.</param>
		public void RemoveElements (IList<int> elementIDs)
		{
			IList<TongueTwisterModel> elements = _data.Where (element => elementIDs.Contains (element.Id)).ToArray ();
			RemoveElements (elements);
		}

		/// <summary>
		/// Removes all given elements from the data.
		/// </summary>
		/// <param name="elements">Elements to remove from the data.</param>
		/// <exception cref="ArgumentException">Occurs when one of the elements is the root data.</exception>
		public void RemoveElements (IList<TongueTwisterModel> elements)
		{
			foreach (var element in elements)
				if (element == Root)
					throw new ArgumentException("It is not allowed to remove the root element");
		
			var commonAncestors = ModelUtility.FindCommonAncestorsWithinList (elements);

			foreach (var element in commonAncestors)
			{
				element.Parent.Children.Remove (element);
				element.Parent = null;
			}

			ModelUtility.TreeToList(Root, _data);

			MarkAsChanged();
		}

		/// <summary>
		/// Removes empty localizations.
		/// </summary>
		public void CleanLocalizations()
		{
			var cleanedLocalizations = 0; 
			
			for (int i = _data.Count - 1; i >= 0; i--)
			{
				var data = _data[i];

				if (data.Type != ModelType.Localization) continue;

				if (string.IsNullOrEmpty(data.Text))
				{
					RemoveElement(data);
					cleanedLocalizations++;
				}
			}
			
			Debug.Log($"Removed {cleanedLocalizations} empty localizations.");
		}

		/// <summary>
		/// Removes empty display keys.
		/// </summary>
		public void CleanDisplayKeys()
		{
			var cleanedDisplayKeys = 0; 
			
			for (int i = _data.Count - 1; i >= 0; i--)
			{
				var data = _data[i];

				if (data.Type != ModelType.DisplayKey) continue;

				if (!data.HasChildren)
				{
					RemoveElement(data);
					cleanedDisplayKeys++;
				}
			}
			
			Debug.Log($"Removed {cleanedDisplayKeys} empty display keys.");
		}
		
		/// <summary>
		/// Removes empty groups.
		/// </summary>
		public void CleanGroups()
		{
			var cleanedGroups = 0; 
			
			for (int i = _data.Count - 1; i >= 0; i--)
			{
				var data = _data[i];

				if (data.Type != ModelType.Group) continue;

				if (!data.HasChildren)
				{
					RemoveElement(data);
					cleanedGroups++;
				}
			}
			
			Debug.Log($"Removed {cleanedGroups} empty groups.");
		}

		/// <summary>
		/// Ensures an add-operation is possible.
		/// </summary>
		/// <param name="element">The element to be added.</param>
		/// <param name="parent">The parent that the element will be added to.</param>
		/// <returns>True if the add-operation is possible.</returns>
		private bool ValidateAddElement(TongueTwisterModel element, TongueTwisterModel parent)
		{
			if (element == null)
			{
				Debug.LogError("The element you're trying to add is null.");
				return false;
			}

			if (parent == null)
			{
				Debug.LogError("Parent is null.");
				return false;
			}

			if (parent.Type == ModelType.Localization)
			{
				Debug.LogError("Localizations cannot parent objects.");
				return false;
			}

			if (parent.Type == ModelType.DisplayKey && element.Type != ModelType.Localization)
			{
				Debug.LogError("Display keys can only contain localizations.");
				return false;
			}

			if (parent.Type == ModelType.Group && element.Type == ModelType.Localization)
			{
				Debug.LogError("Localizations can only be parented to display keys.");
				return false;
			}

			return true;
		}

		/// <summary>
		/// Adds an element to the given parent with the optional insert position.
		/// </summary>
		/// <param name="element">The element to add</param>
		/// <param name="parent">The parent that will receive this element</param>
		/// <param name="insertPosition">The optional insert position.</param>
		/// <exception cref="ArgumentNullException">Occurs when the element or parent is null.</exception>
		public void AddElement (TongueTwisterModel element, TongueTwisterModel parent, int insertPosition = -1)
		{
			if (!ValidateAddElement(element, parent)) return;
		
			if (parent.Children == null)
				parent.Children = new List<TongueTwisterModel> ();

			if (insertPosition >= 0) parent.Children.Insert(insertPosition, element);
			else parent.Children.Add(element);
			
			element.Parent = parent;

			ModelUtility.UpdateDepthValues(parent);
			ModelUtility.TreeToList(Root, _data);

			MarkAsChanged ();
		}

		/// <summary>
		/// Adds a new element of the given type to the parent.
		/// </summary>
		/// <param name="parent">The parent to receive a new element</param>
		/// <param name="modelType">The type of model to add</param>
		/// <returns>The newly created element.</returns>
		public TongueTwisterModel AddElement(TongueTwisterModel parent, ModelType modelType)
		{
			var elementName = "";

			switch (modelType)
			{
				case ModelType.Group:
					elementName = "New Group";
					break; 
				case ModelType.DisplayKey:
					elementName = "New Display Key";
					break; 
				case ModelType.Localization:
					elementName = "New Localization";
					break;
			}

			var element = new TongueTwisterModel(
				elementName,
				GetNextId(),
				parent.Depth + 1,
				modelType);

			AddElement(element, parent);

			return element;
		}

		/// <summary>
		/// Adds a new display key based on the given full display key name. Then adds localizations for the given
		/// localizations.
		/// </summary>
		/// <param name="fullDisplayKeyName">The full display key name.</param>
		/// <param name="displayKeyLocalizationSet">The set of localizations to add.</param>
		public void AddElement(string fullDisplayKeyName, DisplayKeyLocalizationSet displayKeyLocalizationSet)
		{
			var splitNames = fullDisplayKeyName.Split('.');
			var parentalModelChain = new List<TongueTwisterModel>();
			for (int i = 0; i < splitNames.Length; i++)
			{
				var splitName = splitNames[i];
				var model = Find(splitName);
				if (model == null)
				{
					var lastParent = parentalModelChain.LastOrDefault() ?? Root;
					var newModel = new TongueTwisterModel()
					{
						Name = splitName,
						Type = i == splitNames.Length - 1 ? ModelType.DisplayKey : ModelType.Group,
						Parent = lastParent,
						Id = GetNextId(),
						Depth = lastParent.Depth + 1
					};
					AddElement(newModel, lastParent);
					parentalModelChain.Add(newModel);
				}
				else
				{
					parentalModelChain.Add(model);
				}
			}

			var lastModel = parentalModelChain.LastOrDefault();
			
			if (lastModel == null)
			{
				throw new Exception("Could not add model chain.");
			}

			var localizations = displayKeyLocalizationSet.GetAllLocalizations();
			foreach (var localization in localizations)
			{
				var newModel = new TongueTwisterModel()
				{
					Name = 
						localization.Locale?.Metadata?.DisplayName ??
						EditorLabels.LocalizationManager.EditorModes.LocaleEditing.LocaleNotSelectedForLocalization,
					LocaleId = localization.Locale?.Id,
					AudioClip = localization.AudioClip,
					Text = localization.Text,
					Id = GetNextId(),
					Parent = lastModel,
					Depth = lastModel.Depth + 1,
					Type = ModelType.Localization
				};
				AddElement(newModel, lastModel);
			}
		}

		/// <summary>
		/// Adds a new group model element to the root.
		/// </summary>
		public void AddNewRootElement(ModelType modelType)
		{
			if (modelType == ModelType.Localization)
			{
				// cannot add localizations at the root.
				return;
			}
			
			AddElement(Root, modelType); 
		}

		/// <summary>
		/// Duplicates each item within its own parent.
		/// </summary>
		/// <param name="items">The items to duplicate.</param>
		public void Duplicate(IList<int> items)
		{
			foreach (var index in items)
			{
				var item = Find(index);
				AddElement(new TongueTwisterModel(item, GetNextId), item.Parent);
			}
			
			if (items.Count > 0) MarkAsChanged();
		}

		/// <summary>
		/// Duplicates each item to the destination parent ID.
		/// </summary>
		/// <param name="items">The items to duplicate.</param>
		/// <param name="destinationParentId">The ID of the parent element where the items will be duplicated to.</param>
		public void DuplicateTo(IList<int> items, int destinationParentId)
		{
			var parent = Find(destinationParentId);
			foreach (var index in items)
			{
				var item = Find(index);
				AddElement(new TongueTwisterModel(item, parent, GetNextId), parent);
			}
			
			if (items.Count > 0) MarkAsChanged();
		}

		/// <summary>
		/// Duplicates each item once to each parent.
		/// </summary>
		/// <param name="items">Items that will be duplicated to each parent.</param>
		/// <param name="destinationParentIds">The destination parent ids that will receive a new copy of
		/// each item.</param>
		public void DuplicateTo(IList<int> items, IList<int> destinationParentIds)
		{
			foreach (var destinationParentId in destinationParentIds)
				DuplicateTo(items, destinationParentId);
			
			if (items.Count > 0) MarkAsChanged();
		}

		/// <summary>
		/// Moves elements to a parent with the given insertion index.
		/// </summary>
		/// <param name="parentElement">The parent that will receive the elements.</param>
		/// <param name="insertionIndex">The positional index where the elements will be inserted amongst the existing
		/// children of the parent's elements.</param>
		/// <param name="elements">The elements to move.</param>
		/// <exception cref="ArgumentException">Occurs when insertion index is invalid (such as -1).</exception>
		public void MoveElements(TongueTwisterModel parentElement, int insertionIndex, List<TongueTwisterModel> elements)
		{
			if (insertionIndex < 0)
				throw new ArgumentException("Invalid input: insertionIndex is -1, client needs to decide what index elements should be reparented at");

			if (parentElement == null)
				return;

			if (insertionIndex > 0)
				insertionIndex -= parentElement.Children.GetRange(0, insertionIndex).Count(elements.Contains);

			foreach (var draggedItem in elements)
			{
				draggedItem.Parent.Children.Remove(draggedItem);
				draggedItem.Parent = parentElement;				
			} 

			if (parentElement.Children == null)
				parentElement.Children = new List<TongueTwisterModel>();

			parentElement.Children.InsertRange(insertionIndex, elements);

			ModelUtility.UpdateDepthValues (Root);
			ModelUtility.TreeToList (Root, _data);

			MarkAsChanged ();
		}

		/// <summary>
		/// Invokes the <c>OnModelChanged</c> function.
		/// </summary>
		public void MarkAsChanged ()
		{
			OnModelChanged?.Invoke();
		}
        
        #endregion
    }
}