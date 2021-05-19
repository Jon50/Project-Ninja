using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TongueTwister.Validation;
using UnityEngine;
using UnityEngine.Serialization;

namespace TongueTwister.Models
{
    /// <summary>
    /// Represents a common ground object between groups, display keys, and localizations which is easily serialized and
    /// deserialized. These objects make up the standard data sets of Tongue Twister and have combined properties of
    /// all the aforementioned elements. This is because Unity's JSON serialization does not take kindly to derived
    /// types.
    /// </summary>
    [Serializable]
    public class TongueTwisterModel
    {
        #region Constants

        private const string FORMATTED_NAME_REGEX = @"[^0-9a-zA-Z:,]+";

        #endregion
        
        #region General Serialized Data 
        
        /// <summary>
        /// The type of this model.
        /// </summary>
        [FormerlySerializedAs("ModelType")] [SerializeField] 
        public ModelType Type;
        
        /// <summary>
        /// The name of this model.
        /// </summary>
        [SerializeField] public string Name;
        
        /// <summary>
        /// Editor metadata regarding the ID of this model.
        /// </summary>
        [SerializeField] public int Id;
        
        /// <summary>
        /// Editor metadata regarding the depth of this model in the tree view.
        /// </summary>
        [SerializeField] public int Depth;

        /// <summary>
        /// Editor metadata regarding any notes about this model.
        /// </summary>
        [SerializeField] public string Notes;
        
        #endregion

        #region Nonserialized Data
        
        /// <summary>
        /// The parent model of this model.
        /// </summary>
        [NonSerialized] public TongueTwisterModel Parent;
        
        /// <summary>
        /// The children models of this model.
        /// </summary>
        [NonSerialized] public List<TongueTwisterModel> Children;
        
        #endregion
        
        #region Error Properties

        /// <summary>
        /// List of model rule violations.
        /// </summary>
        public List<RuleViolation> ModelRuleViolations { get; set; } = new List<RuleViolation>();

        /// <summary>
        /// Whether or not this model has errors. This value changes whenever an error check is ran. 
        /// </summary>
        public bool HasErrors
        {
            get
            {
                if (ModelRuleViolations == null || ModelRuleViolations.Count == 0)
                {
                    return false;
                }
                
                for (int i = 0; i < ModelRuleViolations.Count; i++)
                {
                    if (ModelRuleViolations[i].RuleViolationSeverity == RuleViolationSeverityType.Error)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Whether or not this model has warnings. This value changes whenever an error check is ran.
        /// </summary>
        public bool HasWarnings
        {
            get
            {
                if (ModelRuleViolations == null || ModelRuleViolations.Count == 0)
                {
                    return false;
                }
                
                for (int i = 0; i < ModelRuleViolations.Count; i++)
                {
                    if (ModelRuleViolations[i].RuleViolationSeverity == RuleViolationSeverityType.Warning)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Whether or not this model has children.
        /// </summary>
        public bool HasChildren => Children != null && Children.Count > 0;

        /// <summary>
        /// Whether or not this model has children that are groups.
        /// </summary>
        public bool HasGroupChildren => 
            HasChildren && Children.Any(child => child.Type == ModelType.Group);

        /// <summary>
        /// Whether or not this model has children which are display keys.
        /// </summary>
        public bool HasDisplayKeyChildren =>
            HasChildren && Children.Any(child => child.Type == ModelType.DisplayKey);
        
        /// <summary>
        /// Whether or not this model has children that are localizations.
        /// </summary>
        public bool HasLocalizationChildren =>
            HasChildren && Children.Any(child => child.Type == ModelType.Localization);

        /// <summary>
        /// The formatted name of the model with no spaces or special characters.
        /// </summary>
        public string FormattedName => 
            Regex.Replace(
                Name.Replace(" ", ""),
                FORMATTED_NAME_REGEX,
                "");

        #endregion

        #region Group Related Serialized Data

        /// <summary>
        /// Editor meta data regarding whether or not the child groups of this model should be expanded.
        /// </summary>
        [SerializeField] public bool ChildGroupsExpanded = true;
        
        /// <summary>
        /// Editor meta data regarding whether or not child display keys of this model should be expanded.
        /// </summary>
        [SerializeField] public bool DisplayKeysExpanded = true;

        /// <summary>
        /// Editor meta data that can be used to store display key filter data.
        /// </summary>
        [NonSerialized] 
        public string FilterDisplayKeys = "";
        
        /// <summary>
        /// Editor meta data that can be used to store child group filter data.
        /// </summary>
        [NonSerialized] 
        public string FilterChildGroups = "";

        #endregion

        #region Display Key Related Serialized Data

        /// <summary>
        /// Editor meta data regarding whether or not child localizations of this model should be expanded.
        /// </summary>
        [SerializeField] public bool LocalizationsExpanded = true;

        /// <summary>
        /// Editor meta that can be used to store localization filter data.
        /// </summary>
        [NonSerialized] 
        public string FilterLocalizations = "";

        #endregion

        #region Localization Related Serialized Data

        [SerializeField]
        public string LocaleId;
        
        /// <summary>
        /// The text of this model's localization.
        /// </summary>
        [SerializeField] 
        public string Text;

        /// <summary>
        /// The audio clip tied to a localization.
        /// </summary>
        [SerializeField]
        public AudioClip AudioClip;
        
        /// <summary>
        /// A texture object 
        /// </summary>
        [SerializeField] 
        public Texture Texture;

        /// <summary>
        /// A generic Unity Engine object to store localized content not supported in localizations by default.
        /// </summary>
        [SerializeField] 
        public UnityEngine.Object UnityObject;

        /// <summary>
        /// A collection of additional Unity Objects which may not fit into the category of Texture, AudioClip, or
        /// generic UnityEngine Object.
        /// </summary>
        [SerializeField] 
        public LocalizedContentCollection AdditionalLocalizedContent = new LocalizedContentCollection();
        
        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new model.
        /// </summary>
        public TongueTwisterModel()
        {
            
        }

        /// <summary>
        /// Creates a new model with the given data.
        /// </summary>
        /// <param name="name">The name of the new model.</param>
        /// <param name="id">The ID of the new model.</param>
        /// <param name="depth">The depth that this model should appear in within the Editor tree.</param>
        /// <param name="modelType">The type of model.</param>
        public TongueTwisterModel(string name, int id, int depth, ModelType modelType)
        {
            Name = name;
            Id = id;
            Depth = depth;
            Type = modelType;
        }

        /// <summary>
        /// Creates an exact copy of a model. If no function is provided to create a new ID with, the model will have
        /// the same exact ID.
        /// </summary>
        /// <param name="copy">The model to copy.</param>
        /// <param name="createNewId">The function provided for generating a new ID in this model.</param>
        public TongueTwisterModel(TongueTwisterModel copy, Func<int> createNewId = null)
        {
            Copy(copy, createNewId);
        }

        /// <summary>
        /// Duplicates a model to the given parent.
        /// </summary>
        /// <param name="duplicate">The model to duplicate.</param>
        /// <param name="to">The model to parent the new model to.</param>
        /// <param name="createNewId">The function provided for generating a new ID in this model.</param>
        public TongueTwisterModel(TongueTwisterModel duplicate, TongueTwisterModel to, Func<int> createNewId = null)
        {
            Copy(duplicate, createNewId);
            Parent = to;
        }

        /// <summary>
        /// Copies the property values of one model to this model.
        /// </summary>
        /// <param name="copy">The model to copy from.</param>
        /// <param name="createNewId">A nullable function that would retrieve the next possible ID for the model.</param>
        private void Copy(TongueTwisterModel copy, Func<int> createNewId)
        {
            Type = copy.Type;
            Name = string.IsNullOrEmpty(copy.Name) ? "" : string.Copy(copy.Name);
            Id = createNewId?.Invoke() ?? copy.Id;
            Depth = copy.Depth;
            Notes = string.IsNullOrEmpty(copy.Notes) ? "" : string.Copy(copy.Notes);
            Parent = copy.Parent;
            ChildGroupsExpanded = copy.ChildGroupsExpanded;
            DisplayKeysExpanded = copy.DisplayKeysExpanded;
            LocalizationsExpanded = copy.LocalizationsExpanded;
            LocaleId = copy.LocaleId;
            AudioClip = copy.AudioClip;
            AdditionalLocalizedContent = new LocalizedContentCollection(copy.AdditionalLocalizedContent);
            Text = string.IsNullOrEmpty(copy.Text) ? "" : string.Copy(copy.Text);
            Texture = copy.Texture;
            UnityObject = copy.UnityObject;

            Children = new List<TongueTwisterModel>();

            if (copy.HasChildren)
            {
                foreach (var child in copy.Children)
                {
                    Children.Add(new TongueTwisterModel(child, this, createNewId));
                }
            }
            else
            {
                Children = new List<TongueTwisterModel>();
            }
        }

        #endregion

        #region Accessors

        /// <summary>
        /// Gets an array of models representing children of the given model type.
        /// </summary>
        /// <param name="ofType">The type of model to get children of.</param>
        /// <returns>Array of models of the given model type.</returns>
        public TongueTwisterModel[] GetChildrenOfType(ModelType ofType)
            => HasChildren
                ? Children.Where(child => child.Type == ofType).ToArray()
                : new TongueTwisterModel[0];

        /// <summary>
        /// Gets the fully combined string of all ancestral and children formatted names, separated by a period (dot).
        /// </summary>
        /// <returns>The full dot name.</returns>
        public string GetFullDotName()
        {
            var parent = Parent;
            var result = FormattedName;

            while (parent != null && parent.Id != -1)
            {
                result = $"{parent.FormattedName}.{result}";
                parent = parent.Parent;
            }

            return result;
        }

        /// <summary>
        /// Recursively retrieves all children of the given type, extending down through its children's children and
        /// so on.
        /// </summary>
        /// <param name="includeChildren">Recursively check children as well.</param>
        /// <param name="modelType">The type of models to retrieve.</param>
        /// <returns></returns>
        public List<TongueTwisterModel> GetAllChildrenOfType(bool includeChildren, ModelType modelType)
        {
            var result = new List<TongueTwisterModel>();

            if (HasChildrenOfType(modelType))
            {
                result.AddRange(GetChildrenOfType(modelType));
            }

            if (includeChildren && HasChildren)
            {
                foreach (var child in Children)
                    result.AddRange(child.GetAllChildrenOfType(true, modelType)); 
            }

            return result;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Checks whether or not this model has children of the given model type.
        /// </summary>
        /// <param name="modelType">The model type to check against.</param>
        /// <returns>Whether or not this model has children of the given model type.</returns>
        public bool HasChildrenOfType(ModelType modelType)
        {
            switch (modelType)
            {
                case ModelType.Group: return HasGroupChildren;
                case ModelType.DisplayKey: return HasDisplayKeyChildren;
                case ModelType.Localization: return HasLocalizationChildren;
                default: return false;
            }
        }

        #endregion

        #region Supporting Types

        public enum ModelType
        {
            Group = 0, 
            DisplayKey = 1, 
            Localization = 2
        }

        #endregion
    }
}