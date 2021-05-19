using System;
using System.Collections.Generic;
using System.Linq;
using TongueTwister.Extensions;
using TongueTwister.Models;
using UnityEngine;

namespace TongueTwister.Utilities
{
	/// <summary>
	/// Provides functionality and additional operations in regards to models, lists, and trees.
	/// </summary>
    public static class ModelUtility
	{
		/// <summary>
		/// Converts a model tree starting at the given root object into a localization dictionary.
		/// </summary>
		/// <param name="root">The root of the tree (first model).</param>
		/// <param name="configuredLocales">The configured locales.</param>
		/// <returns>A localization dictionary.</returns>
		public static LocalizationDictionary TreeToLocalizationDictionary(TongueTwisterModel root, Locale[] configuredLocales)
		{
			var result = new LocalizationDictionary();

			if (root == null)
			{
				Debug.LogError("Cannot create a localization dictionary from a null root object. This can occur when no content has been configured.");
				return new LocalizationDictionary();
			}

			var displayKeys = root.GetAllChildrenOfType(true, TongueTwisterModel.ModelType.DisplayKey);

			foreach (var displayKey in displayKeys)
			{
				try
				{
					var displayKeyFullDotName = displayKey.GetFullDotName();

					var allLocalizationChildren = displayKey?.Children;

					if (allLocalizationChildren == null || allLocalizationChildren.Count == 0)
					{
						Debug.Log($"Display key \"{displayKey.Name}\" has no configured localizations, skipping.");
						continue;
					}

					var localizations = allLocalizationChildren
						.Select(child => child.ToLocalization(configuredLocales))
						.ToList();

					if (localizations.Count == 0)
					{
						Debug.Log($"Display key \"{displayKey.Name}\" has no valid localizations, skipping.");
						continue;
					}

					result[displayKeyFullDotName] = new DisplayKeyLocalizationSet(displayKeyFullDotName, localizations);;
				}
				catch (Exception exception)
				{
					Debug.LogError($"Failed to add display key \"{displayKey.Name}\", reason: {exception.Message}");
				}
			}
			
			return result;
		}
		
		/// <summary>
		/// Converts a model tree into a list.
		/// </summary>
		/// <param name="root">The root model node of the tree.</param>
		/// <param name="result">The pointer result list.</param>
		/// <typeparam name="T">Type expected of the root node, where T is a <c>Model</c>.</typeparam>
		/// <exception cref="NullReferenceException"></exception>
		public static void TreeToList<T>(T root, IList<T> result) where T : TongueTwisterModel
		{
			if (result == null)
				throw new NullReferenceException("The input 'IList<T> result' list is null");
			result.Clear();

			Stack<T> stack = new Stack<T>();
			stack.Push(root);

			while (stack.Count > 0)
			{
				T current = stack.Pop();
				result.Add(current);

				if (current.Children != null && current.Children.Count > 0)
				{
					for (int i = current.Children.Count - 1; i >= 0; i--)
					{
						stack.Push((T)current.Children[i]);
					}
				}
			}
		}

		/// <summary>
		/// Converts a list of unstructured models into a structured tree. The first element of the list is used as the
		/// root, and as such must have a depth value of -1. Every other item must have a depth of >= 0.
		/// </summary>
		/// <param name="list">The list of unstructured nodes.</param>
		/// <typeparam name="T">The model type expected where T is a <c>Model</c>.</typeparam>
		/// <returns></returns>
		public static T ListToTree<T>(IList<T> list) where T : TongueTwisterModel
		{
			if (list.Count == 0) return null; 
			
			ValidateDepthValues (list);

			foreach (var element in list)
			{
				element.Parent = null;
				element.Children = null;
			}

			for (int parentIndex = 0; parentIndex < list.Count; parentIndex++)
			{
				var parent = list[parentIndex];
				bool alreadyHasValidChildren = parent.Children != null;
				
				if (alreadyHasValidChildren)
					continue;

				int parentDepth = parent.Depth;
				int childCount = 0;

				for (int i = parentIndex + 1; i < list.Count; i++)
				{
					if (list[i].Depth == parentDepth + 1)
						childCount++;
					if (list[i].Depth <= parentDepth)
						break;
				}

				List<TongueTwisterModel> childList = null;
				
				if (childCount != 0)
				{
					childList = new List<TongueTwisterModel>(childCount); // Allocate once
					childCount = 0;
					for (int i = parentIndex + 1; i < list.Count; i++)
					{
						if (list[i].Depth == parentDepth + 1)
						{
							list[i].Parent = parent;
							childList.Add(list[i]);
							childCount++;
						}

						if (list[i].Depth <= parentDepth)
							break;
					}
				}

				parent.Children = childList;
			}

			return list[0];
		}

		/// <summary>
		/// Performs error checking on a list of nodes. Will throw exceptions if something is found to be incorrect.
		/// </summary>
		/// <param name="list">The list of nodes.</param>
		/// <typeparam name="T">The type of models within the list where T is a <c>Model</c>.</typeparam>
		/// <exception cref="ArgumentException">The thrown exception regarding the incorrect list.</exception>
		public static void ValidateDepthValues<T>(IList<T> list) where T : TongueTwisterModel
		{
			if (list.Count == 0)
				throw new ArgumentException("list should have items, count is 0, check before calling ValidateDepthValues", "list");

			if (list[0].Depth != -1)
				throw new ArgumentException("list item at index 0 should have a depth of -1 (since this should be the hidden root of the tree). Depth is: " + list[0].Depth, "list");

			for (int i = 0; i < list.Count - 1; i++)
			{
				int depth = list[i].Depth;
				int nextDepth = list[i + 1].Depth;
				if (nextDepth > depth && nextDepth - depth > 1)
					throw new ArgumentException(string.Format("Invalid depth info in input list. Depth cannot increase more than 1 per row. Index {0} has depth {1} while index {2} has depth {3}", i, depth, i + 1, nextDepth));
			}

			for (int i = 1; i < list.Count; ++i)
				if (list[i].Depth < 0)
					throw new ArgumentException("Invalid depth value for item at index " + i + ". Only the first item (the root) should have depth below 0.");

			if (list.Count > 1 && list[1].Depth != 0)
				throw new ArgumentException("Input list item at index 1 is assumed to have a depth of 0", "list");
		}
		
		/// <summary>
		/// Updates the depth values of children elements given a root. Especially useful after re-parenting elements.
		/// </summary>
		/// <param name="root">The root model object.</param>
		/// <typeparam name="T">The model type where T is a <c>Model</c>.</typeparam>
		/// <exception cref="ArgumentNullException">The thrown exception when root is null.</exception>
		public static void UpdateDepthValues<T>(T root) where T : TongueTwisterModel
		{
			if (root == null)
				throw new ArgumentNullException("root", "The root is null");

			if (root.Children == null || root.Children.Count == 0)
				return;

			Stack<TongueTwisterModel> stack = new Stack<TongueTwisterModel>();
			stack.Push(root);
			while (stack.Count > 0)
			{
				TongueTwisterModel current = stack.Pop();
				if (current.Children != null)
				{
					foreach (var child in current.Children)
					{
						child.Depth = current.Depth + 1;
						stack.Push(child);
					}
				}
			}
		}

		/// <summary>
		/// Checks to see if a model is a child of any of the given elements.
		/// </summary>
		/// <param name="child">The model to check for child status.</param>
		/// <param name="elements">The elements where a child may be a child of.</param>
		/// <typeparam name="T">The model type where T is a <c>Model</c>.</typeparam>
		/// <returns></returns>
		static bool IsChildOf<T>(T child, IList<T> elements) where T : TongueTwisterModel
		{
			while (child != null)
			{
				child = (T)child.Parent;
				if (elements.Contains(child))
					return true;
			}
			return false;
		}

		/// <summary>
		/// Finds all ancestors within the given list of elements that would have multiple parents (not allowed).
		/// </summary>
		/// <param name="elements">The list of elements to check for.</param>
		/// <typeparam name="T">The model type where T is a <c>Model</c>.</typeparam>
		/// <returns>List of elements with shared ancestry.</returns>
		public static IList<T> FindCommonAncestorsWithinList<T>(IList<T> elements) where T : TongueTwisterModel
		{
			if (elements.Count == 1)
				return new List<T>(elements);

			List<T> result = new List<T>(elements);
			result.RemoveAll(g => IsChildOf(g, elements));
			return result;
		}
	}
}