using System;
using TongueTwister.Models;
using TongueTwister.Validation;
using UnityEditor;
using UnityEngine;

namespace TongueTwister.Editor.Utilities
{
    /// <summary>
    /// Provides accessibility and management of the TongueTwister icons. For editor use only.
    /// </summary>
    public static class TongueTwisterIconUtility
    {
        // Dev Notes / FWIW:
        // the word "dark" at the end of texture names refers to the coloring of the image, not that it belongs to
        // a dark mode style. 
        
        private static Texture 
            _addGroupIcon,
            _addGroupIconDark,
            _addDisplayKeyIcon,
            _addDisplayKeyIconDark,
            _addLocalizationIcon,
            _addLocalizationIconDark,
            _localizationIcon,
            _localizationIconDark,
            _errorIcon,
            _infoIcon,
            _displayKeyIcon,
            _displayKeyIconDark,
            _warningIcon,
            _trashIcon,
            _trashIconDark,
            _saveIcon,
            _toolbarAddIcon,
            _duplicateIcon,
            _duplicateIconDark,
            _editIcon,
            _deselectIcon,
            _deselectIconDark,
            _goUpIcon,
            _goUpIconDark,
            _groupIcon,
            _groupIconDark,
            _reRunErrorIcon,
            _reRunErrorIconDark,
            _backIcon,
            _backIconDark,
            _forwardIcon,
            _forwardIconDark,
            _homeIcon,
            _homeIconDark,
            _gearIcon,
            _gearIconDark,
            _localesIcon,
            _localesIconDark,
            _editorIcon,
            _editorIconDark,
            _toolsIcon,
            _toolsIconDark,
            _importIcon,
            _importIconDark,
            _logoIcon,
            _logoNoBackground,
            _addLocale,
            _addLocaleDark,
            _d,
            _dDark,
            _defaultLocale,
            _defaultLocaleDark,
            _localeIconColumn,
            _localeIconColumnDark,
            _localeStatusColumn,
            _localeStatusColumnDark;
        
        private static Texture addGroupIcon 
        { 
            get 
            {
                if (_addGroupIcon == null) 
                {
                    _addGroupIcon = Resources.Load<Texture>("icons/addGroupIcon");
                }
                
                return _addGroupIcon;
            }
        }
        private static Texture addGroupIconDark 
        { 
            get 
            {
                if (_addGroupIconDark == null) 
                {
                    _addGroupIconDark = Resources.Load<Texture>("icons/addGroupIconDark");
                }
                
                return _addGroupIconDark;
            }
        }
        private static Texture addDisplayKeyIcon 
        { 
            get 
            {
                if (_addDisplayKeyIcon == null) 
                {
                    _addDisplayKeyIcon = Resources.Load<Texture>("icons/addDisplayKeyIcon");
                }
                
                return _addDisplayKeyIcon;
            }
        }
        private static Texture addDisplayKeyIconDark 
        { 
            get 
            {
                if (_addDisplayKeyIconDark == null) 
                {
                    _addDisplayKeyIconDark = Resources.Load<Texture>("icons/addDisplayKeyIconDark");
                }
                
                return _addDisplayKeyIconDark;
            }
        }
        private static Texture addLocalizationIcon 
        { 
            get 
            {
                if (_addLocalizationIcon == null) 
                {
                    _addLocalizationIcon = Resources.Load<Texture>("icons/addLocalizationIcon");
                }
                
                return _addLocalizationIcon;
            }
        }
        private static Texture addLocalizationIconDark 
        { 
            get 
            {
                if (_addLocalizationIconDark == null) 
                {
                    _addLocalizationIconDark = Resources.Load<Texture>("icons/addLocalizationIconDark");
                }
                
                return _addLocalizationIconDark;
            }
        }
        private static Texture localizationIcon 
        { 
            get 
            {
                if (_localizationIcon == null) 
                {
                    _localizationIcon = Resources.Load<Texture>("icons/localizationIcon");
                }
                
                return _localizationIcon;
            }
        }
        private static Texture localizationIconDark 
        { 
            get 
            {
                if (_localizationIconDark == null) 
                {
                    _localizationIconDark = Resources.Load<Texture>("icons/localizationIconDark");
                }
                
                return _localizationIconDark;
            }
        }
        private static Texture errorIcon 
        { 
            get 
            {
                if (_errorIcon == null) 
                {
                    _errorIcon = EditorGUIUtility.IconContent("console.erroricon").image;
                }
                
                return _errorIcon;
            }
        }

        private static Texture infoIcon
        {
            get
            {
                if (_infoIcon == null)
                {
                    _infoIcon = EditorGUIUtility.IconContent("console.infoicon").image;
                }

                return _infoIcon;
            }
        }
        private static Texture displayKeyIcon 
        { 
            get 
            {
                if (_displayKeyIcon == null) 
                {
                    _displayKeyIcon = Resources.Load<Texture>("icons/displayKeyIcon");
                }
                
                return _displayKeyIcon;
            }
        }
        private static Texture displayKeyIconDark 
        { 
            get 
            {
                if (_displayKeyIconDark == null) 
                {
                    _displayKeyIconDark = Resources.Load<Texture>("icons/displayKeyIconDark");
                }
                
                return _displayKeyIconDark;
            }
        }
        private static Texture warningIcon 
        { 
            get 
            {
                if (_warningIcon == null) 
                {
                    _warningIcon = EditorGUIUtility.IconContent("console.warnicon").image;;
                }
                
                return _warningIcon;
            }
        }
        private static Texture trashIcon 
        { 
            get 
            {
                if (_trashIcon == null) 
                {
                    _trashIcon = Resources.Load<Texture>("icons/trashIcon");
                }
                
                return _trashIcon;
            }
        }
        private static Texture trashIconDark 
        { 
            get 
            {
                if (_trashIconDark == null) 
                {
                    _trashIconDark = Resources.Load<Texture>("icons/trashIconDark");
                }
                
                return _trashIconDark;
            }
        }
        private static Texture saveIcon 
        { 
            get 
            {
                if (_saveIcon == null) 
                {
                    _saveIcon = EditorGUIUtility.IconContent("SaveActive").image;
                }
                
                return _saveIcon;
            }
        }
        private static Texture toolbarAddIcon 
        { 
            get 
            {
                if (_toolbarAddIcon == null) 
                {
                    _toolbarAddIcon = EditorGUIUtility.IconContent("Toolbar Plus").image;
                }
                
                return _toolbarAddIcon;
            }
        }
        private static Texture duplicateIcon 
        { 
            get 
            {
                if (_duplicateIcon == null) 
                {
                    _duplicateIcon = Resources.Load<Texture>("icons/duplicateIcon");
                }
                
                return _duplicateIcon;
            }
        }
        private static Texture duplicateIconDark 
        { 
            get 
            {
                if (_duplicateIconDark == null) 
                {
                    _duplicateIconDark = Resources.Load<Texture>("icons/duplicateIconDark");
                }
                
                return _duplicateIconDark;
            }
        }
        private static Texture editIcon 
        { 
            get 
            {
                if (_editIcon == null) 
                {
                    _editIcon = EditorGUIUtility.IconContent("editicon.sml").image;
                }
                
                return _editIcon;
            }
        }
        private static Texture deselectIcon 
        { 
            get 
            {
                if (_deselectIcon == null) 
                {
                    _deselectIcon = Resources.Load<Texture>("icons/deselectIcon");
                }
                
                return _deselectIcon;
            }
        }
        private static Texture deselectIconDark 
        { 
            get 
            {
                if (_deselectIconDark == null) 
                {
                    _deselectIconDark = Resources.Load<Texture>("icons/deselectIconDark");
                }
                
                return _deselectIconDark;
            }
        }
        private static Texture goUpIcon 
        { 
            get 
            {
                if (_goUpIcon == null) 
                {
                    _goUpIcon = Resources.Load<Texture>("icons/goUpIcon");
                }
                
                return _goUpIcon;
            }
        }
        private static Texture goUpIconDark 
        { 
            get 
            {
                if (_goUpIconDark == null) 
                {
                    _goUpIconDark = Resources.Load<Texture>("icons/goUpIconDark");
                }
                
                return _goUpIconDark;
            }
        }
        private static Texture groupIcon 
        { 
            get 
            {
                if (_groupIcon == null) 
                {
                    _groupIcon = Resources.Load<Texture>("icons/groupIcon");
                }
                
                return _groupIcon;
            }
        }
        private static Texture groupIconDark 
        { 
            get 
            {
                if (_groupIconDark == null) 
                {
                    _groupIconDark = Resources.Load<Texture>("icons/groupIconDark");
                }
                
                return _groupIconDark;
            }
        }
        private static Texture reRunErrorIcon 
        { 
            get 
            {
                if (_reRunErrorIcon == null) 
                {
                    _reRunErrorIcon = Resources.Load<Texture>("icons/reRunErrorIcon");
                }
                
                return _reRunErrorIcon;
            }
        }
        private static Texture reRunErrorIconDark 
        { 
            get 
            {
                if (_reRunErrorIconDark == null) 
                {
                    _reRunErrorIconDark = Resources.Load<Texture>("icons/reRunErrorIconDark");
                }
                
                return _reRunErrorIconDark;
            }
        }
        private static Texture backIcon 
        { 
            get 
            {
                if (_backIcon == null) 
                {
                    _backIcon = Resources.Load<Texture>("icons/backIcon");
                }
                
                return _backIcon;
            }
        }
        private static Texture backIconDark 
        { 
            get 
            {
                if (_backIconDark == null) 
                {
                    _backIconDark = Resources.Load<Texture>("icons/backIconDark");
                }
                
                return _backIconDark;
            }
        }
        private static Texture forwardIcon 
        { 
            get 
            {
                if (_forwardIcon == null) 
                {
                    _forwardIcon = Resources.Load<Texture>("icons/forwardIcon");
                }
                
                return _forwardIcon;
            }
        }
        private static Texture forwardIconDark 
        { 
            get 
            {
                if (_forwardIconDark == null) 
                {
                    _forwardIconDark = Resources.Load<Texture>("icons/forwardIconDark");
                }
                
                return _forwardIconDark;
            }
        }
        private static Texture homeIcon 
        { 
            get 
            {
                if (_homeIcon == null) 
                {
                    _homeIcon = Resources.Load<Texture>("icons/homeIcon");
                }
                
                return _homeIcon;
            }
        }
        private static Texture homeIconDark 
        { 
            get 
            {
                if (_homeIconDark == null) 
                {
                    _homeIconDark = Resources.Load<Texture>("icons/homeIconDark");
                }
                
                return _homeIconDark;
            }
        }
        private static Texture gearIcon 
        { 
            get 
            {
                if (_gearIcon == null) 
                {
                    _gearIcon = Resources.Load<Texture>("icons/gearIcon");
                }
                
                return _gearIcon;
            }
        }
        private static Texture gearIconDark 
        { 
            get 
            {
                if (_gearIconDark == null) 
                {
                    _gearIconDark = Resources.Load<Texture>("icons/gearIconDark");
                }
                
                return _gearIconDark;
            }
        }
        private static Texture localesIcon 
        { 
            get 
            {
                if (_localesIcon == null) 
                {
                    _localesIcon = Resources.Load<Texture>("icons/localesIcon");
                }
                
                return _localesIcon;
            }
        }
        private static Texture localesIconDark 
        { 
            get 
            {
                if (_localesIconDark == null) 
                {
                    _localesIconDark = Resources.Load<Texture>("icons/localesIconDark");
                }
                
                return _localesIconDark;
            }
        }
        private static Texture editorIcon 
        { 
            get 
            {
                if (_editorIcon == null) 
                {
                    _editorIcon = Resources.Load<Texture>("icons/editorIcon");
                }
                
                return _editorIcon;
            }
        }
        private static Texture editorIconDark 
        { 
            get 
            {
                if (_editorIconDark == null) 
                {
                    _editorIconDark = Resources.Load<Texture>("icons/editorIconDark");
                }
                
                return _editorIconDark;
            }
        }
        private static Texture toolsIcon 
        { 
            get 
            {
                if (_toolsIcon == null) 
                {
                    _toolsIcon = Resources.Load<Texture>("icons/toolsIcon");
                }
                
                return _toolsIcon;
            }
        }
        private static Texture toolsIconDark 
        { 
            get 
            {
                if (_toolsIconDark == null) 
                {
                    _toolsIconDark = Resources.Load<Texture>("icons/toolsIconDark");
                }
                
                return _toolsIconDark;
            }
        }
        private static Texture importIcon 
        { 
            get 
            {
                if (_importIcon == null) 
                {
                    _importIcon = Resources.Load<Texture>("icons/importIcon");
                }
                
                return _importIcon;
            }
        }
        private static Texture importIconDark 
        { 
            get 
            {
                if (_importIconDark == null) 
                {
                    _importIconDark = Resources.Load<Texture>("icons/importIconDark");
                }
                
                return _importIconDark;
            }
        }
        private static Texture logoIcon 
        { 
            get 
            {
                if (_logoIcon == null) 
                {
                    _logoIcon = Resources.Load<Texture>("icons/logoIcon");
                }
                
                return _logoIcon;
            }
        }
        private static Texture logoNoBackground 
        { 
            get 
            {
                if (_logoNoBackground == null) 
                {
                    _logoNoBackground = Resources.Load<Texture>("icons/logoNoBackground");
                }
                
                return _logoNoBackground;
            }
        }

        private static Texture addLocale
        {
            get
            {
                if (_addLocale == null)
                {
                    _addLocale = Resources.Load<Texture>("icons/addLocale");
                }

                return _addLocale;
            }
        }
        
        private static Texture addLocaleDark
        {
            get
            {
                if (_addLocaleDark == null)
                {
                    _addLocaleDark = Resources.Load<Texture>("icons/addLocaleDark");
                }

                return _addLocaleDark;
            }
        }
        
        private static Texture d
        {
            get
            {
                if (_d == null)
                {
                    _d = Resources.Load<Texture>("icons/d");
                }

                return _d;
            }
        }
        
        private static Texture dDark
        {
            get
            {
                if (_dDark == null)
                {
                    _dDark = Resources.Load<Texture>("icons/dDark");
                }

                return _dDark;
            }
        }
        
        private static Texture defaultLocale
        {
            get
            {
                if (_defaultLocale == null)
                {
                    _defaultLocale = Resources.Load<Texture>("icons/defaultLocaleIcon");
                }

                return _defaultLocale;
            }
        }
        
        private static Texture defaultLocaleDark
        {
            get
            {
                if (_defaultLocaleDark == null)
                {
                    _defaultLocaleDark = Resources.Load<Texture>("icons/defaultLocaleIconDark");
                }

                return _defaultLocaleDark;
            }
        }
        
        private static Texture localeIconColumn
        {
            get
            {
                if (_localeIconColumn == null)
                {
                    _localeIconColumn = Resources.Load<Texture>("icons/localeIconColumn");
                }

                return _localeIconColumn;
            }
        }
        
        private static Texture localeIconColumnDark
        {
            get
            {
                if (_localeIconColumnDark == null)
                {
                    _localeIconColumnDark = Resources.Load<Texture>("icons/localeIconColumnDark");
                }

                return _localeIconColumnDark;
            }
        }
        
        private static Texture localeStatusColumn
        {
            get
            {
                if (_localeStatusColumn == null)
                {
                    _localeStatusColumn = Resources.Load<Texture>("icons/localeStatusColumn");
                }

                return _localeStatusColumn;
            }
        }
        
        private static Texture localeStatusColumnDark
        {
            get
            {
                if (_localeStatusColumnDark == null)
                {
                    _localeStatusColumnDark = Resources.Load<Texture>("icons/localeStatusColumnDark");
                }

                return _localeStatusColumnDark;
            }
        }
        
        /// <summary>
        /// Gets the correct light/dark mode icon for the given context.
        /// </summary>
        /// <param name="iconType">The icon type to get.</param>
        /// <returns>The texture for the icon</returns>
        public static Texture GetIcon(IconType iconType)
        {
            var darkMode = EditorGUIUtility.isProSkin;
            
            switch (iconType)
            {
                default:
                    return null;
                case IconType.AddGroup:
                    return darkMode ? addGroupIcon : addGroupIconDark;
                case IconType.AddGeneric:
                    return toolbarAddIcon;
                case IconType.AddLocalization:
                    return darkMode ? addLocalizationIcon : addLocalizationIconDark;
                case IconType.AddDisplayKey:
                    return darkMode ? addDisplayKeyIcon : addDisplayKeyIconDark;
                case IconType.Edit:
                    return editIcon;
                case IconType.Error:
                    return errorIcon;
                case IconType.Info:
                    return infoIcon;
                case IconType.DeselectAll:
                    return darkMode ? deselectIcon : deselectIconDark;
                case IconType.DisplayKey:
                    return darkMode ? displayKeyIcon : displayKeyIconDark;
                case IconType.Duplicate:
                    return darkMode ? duplicateIcon : duplicateIconDark;
                case IconType.Localization:
                    return darkMode ? localizationIcon : localizationIconDark;
                case IconType.GoUp:
                    return darkMode ? goUpIcon : goUpIconDark;
                case IconType.Group:
                    return darkMode ? groupIcon : groupIconDark;
                case IconType.Save:
                    return saveIcon;
                case IconType.Trash:
                    return darkMode ? trashIcon : trashIconDark;
                case IconType.Warning:
                    return warningIcon;
                case IconType.ReRunErrorCheck:
                    return darkMode ? reRunErrorIcon : reRunErrorIconDark;
                case IconType.Backwards:
                    return darkMode ? backIcon : backIconDark;
                case IconType.Forwards:
                    return darkMode ? forwardIcon : forwardIconDark;
                case IconType.Locales:
                    return darkMode ? localesIcon : localesIconDark;
                case IconType.Editor:
                    return darkMode ? editorIcon : editorIconDark;
                case IconType.Tools:
                    return darkMode ? toolsIcon : toolsIconDark;
                case IconType.Import:
                    return darkMode ? importIcon : importIconDark;
                case IconType.Logo:
                    return logoIcon;
                case IconType.LogoNoBackground:
                    return logoNoBackground;
                case IconType.Home:
                    return darkMode ? homeIcon : homeIconDark;
                case IconType.Settings:
                    return darkMode ? gearIcon : gearIconDark;
                case IconType.AddLocale:
                    return darkMode ? addLocale : addLocaleDark;
                case IconType.DefaultSystemLocale:
                    return darkMode ? d : dDark;
                case IconType.DefaultLocale:
                    return darkMode ? defaultLocale : defaultLocaleDark;
                case IconType.LocaleIconColumn:
                    return darkMode ? localeIconColumn : localeIconColumnDark;
                case IconType.LocaleStatusColumn:
                    return darkMode ? localeStatusColumn : localeStatusColumnDark;
            }
        }

        /// <summary>
        /// Gets the correct icon for the given rule violation severity type.
        /// </summary>
        /// <param name="ruleViolationSeverityType">The violation severity type to retrieve an icon for.</param>
        /// <returns>The texture for the icon</returns>
        public static Texture GetIcon(RuleViolationSeverityType ruleViolationSeverityType)
        {
            return GetIcon(GetIconTypeFromRuleViolationSeverityType(ruleViolationSeverityType));
        }

        /// <summary>
        /// Gets the correct icon for the given model type.
        /// </summary>
        /// <param name="modelType">The model type used to retrieve an icon for.</param>
        /// <returns>An icon related to the given mode type.</returns>
        public static Texture GetIcon(TongueTwisterModel.ModelType modelType)
        {
            return GetIcon(GetIconTypeFromModelType(modelType));
        }

        /// <summary>
        /// Gets an icon type based on the given model type.
        /// </summary>
        /// <param name="modelType">The model type.</param>
        /// <returns>An icon matching the model type.</returns>
        /// <exception cref="Exception">Thrown when the given model type is not supported by this function.</exception>
        public static IconType GetIconTypeFromModelType(TongueTwisterModel.ModelType modelType)
        {
            switch (modelType)
            {
                case TongueTwisterModel.ModelType.Group:
                    return IconType.Group;
                case TongueTwisterModel.ModelType.DisplayKey:
                    return IconType.DisplayKey;
                case TongueTwisterModel.ModelType.Localization:
                    return IconType.Localization;
            }

            throw new Exception($"Model type not supported: {modelType}");
        }

        /// <summary>
        /// Gets an icon type based on a rule violation severity type.
        /// </summary>
        /// <param name="ruleViolationSeverityType"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static IconType GetIconTypeFromRuleViolationSeverityType(
            RuleViolationSeverityType ruleViolationSeverityType)
        {
            switch (ruleViolationSeverityType)
            {
                case RuleViolationSeverityType.Error:
                    return IconType.Error;
                case RuleViolationSeverityType.Warning:
                    return IconType.Warning;
            }

            throw new Exception($"Rule violation severity type not supported: {ruleViolationSeverityType}");
        }
        
        /// <summary>
        /// An icon type value that is paired with a single icon texture.
        /// </summary>
        public enum IconType
        {
            None,
            AddGeneric,
            AddGroup,
            AddDisplayKey,
            AddLocalization,
            DeselectAll,
            ReRunErrorCheck,
            DisplayKey,
            Duplicate,
            Edit,
            Error,
            Info,
            GoUp,
            Group,
            Localization,
            Save,
            Trash,
            Warning,
            Backwards,
            Forwards,
            Locales,
            Home,
            Editor,
            Tools,
            Import,
            Settings,
            Logo,
            LogoNoBackground,
            AddLocale,
            DefaultSystemLocale,
            DefaultLocale,
            LocaleIconColumn,
            LocaleStatusColumn,
        }
    }
}