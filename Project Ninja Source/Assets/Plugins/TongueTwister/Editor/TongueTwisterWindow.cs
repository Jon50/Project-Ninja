using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TongueTwister.Editor.Announcements;
using TongueTwister.Editor.Tools;
using TongueTwister.Editor.Trees.LocaleEditorTree;
using TongueTwister.Editor.Trees.ModelEditorTree;
using TongueTwister.Editor.Trees.ToolTree;
using TongueTwister.Editor.Utilities;
using TongueTwister.Models;
using TongueTwister.Extensions;
using TongueTwister.Pathbuilders;
using TongueTwister.StaticLabels;
using TongueTwister.Utilities;
using TongueTwister.Validation;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace TongueTwister.Editor
{
    /// <summary>
    /// The editor window for TongueTwister. Most functionality is unavailable until a <see cref="LocalizationManager"/>
    /// has been provided which can be operated on. Additionally, most functionality is private to this class but
    /// some can be accessed and used for external means such as import tools and editor implementations. Apologies
    /// for the large amount of in-line comments, this is a very complex implementation.
    /// </summary>
    public class TongueTwisterWindow : EditorWindow
    {
        #region Editor Pref Keys

        private const string EDITOR_PREF_KEY_LAST_EDITOR_STATE = 
            "TT:EditorState";

        private const string EDITOR_PREF_KEY_LAST_USED_LM_PREFAB_ASSET_PATH = 
            "TT:LastUsedLocalizationManager";

        private const string EDITOR_PREF_KEY_LAST_USED_LM_TYPE =
            "TT:LastUsedLocalizationManagerType";

        private const string EDITOR_PREF_LAST_OBJ_INSTANCE_ID_PRE_PLAYMODE =
            "TT:LastObjInstanceIdPrePlaymode";

        private const string EDITOR_PREF_SETTINGS =
            "TT:Settings";
        
        #endregion

        #region Element Sizing References

        private const float TREE_VIEW_EDITOR_TOOLBAR_BUTTON_SIZE = 40.0f;

        private const float EDITING_MODE_INLINE_BUTTON_SIZE = 28.0f;

        private const float WINDOW_MODE_BUTTON_HEIGHT = 40.0f;

        private const float DISPLAY_KEY_EDITOR_INLINE_NAVIGATOR_BUTTONS_SIZE = 30.0f;

        private const float ADDITIONAL_CONTENT_BUTTON_SIZES = 25.0f;

        #endregion

        #region General Window References

        [SerializeField] private LocalizationManager _localizationManager;

        private LocalizationManagerEditor _localizationManagerEditor;
        
        [SerializeField] private TongueTwisterWindowMode _windowMode = TongueTwisterWindowMode.Home;

        [SerializeField] private TreeViewState 
            _modelEditorGroupTreeState, 
            _localeEditorTreeState,
            _toolsModeTreeState;

        [SerializeField] private MultiColumnHeaderState 
            _modelEditorMultiColumnHeaderState, 
            _localeEditorMultiColumnHeaderState,
            _toolsModeMultiColumnHeaderState;

        private ModelEditorTreeView _modelEditorTreeView;

        private LocaleEditorTreeView _localeEditorTreeView;

        private ToolTreeView _toolsModeTreeView;
        
        /// <summary>
        /// The window mode options which are populated during initialization. Examples would be "Settings" and "Editor"
        /// modes that the user can fly between.
        /// </summary>
        private GUIContent[] _windowModeOptions;

        /// <summary>
        /// The scroll pos reference for the entire window.
        /// </summary>
        private Vector2 _windowScrollArea;

        #endregion

        #region Skin and Style References

        private GUIStyle 
            _selectedItemBackgroundStyle, 
            _unselectedItemBackgroundStyle,
            _adjustedLabelTextStyle,
            _linkStyle,
            _toolbar,
            _toolbarSearchField,
            _toolbarClearButton,
            _richStyle;

        #endregion

        #region Splitter Variables

        private bool 
            _draggingModelEditorSplitter, 
            _draggingLocaleEditorSplitter,
            _draggingToolsModeSplitter;

        private float
            _modelEditorHorizontalSplitterLerp = 0.33f,
            _localeEditorHorizontalSplitterLerp = 0.33f,
            _toolsModeHorizontalSplitterLerp = 0.33f;

        #endregion

        #region Editor Mode Variables

        private Vector2 
            _editorTreePanelScrollPos, 
            _editorModelViewScrollPos;
        
        private Rect _editorPanelRect;
        
        private string _editorTreeFilter = "";
        
        private IList<int> _editorCopiedModels = new List<int>();
        
        #endregion

        #region Locale Editor Mode Variables

        private Vector2 
            _localeEditorTreePanelScrollPos,
            _localeEditorEditorScrollPos;

        private Rect _localeEditorPanelRect;

        private string _localeEditorTreeFilter = "";

        #endregion

        #region Tools Mode Variables

        private string _toolFilter = "";

        private Vector2
            _toolsModeTreePanelScrollPos,
            _toolsModeEditorUiScrollPos;

        private Rect _toolsModePanelRect;

        #endregion

        #region Colors
        
        private const string COLOR_HTML_DARK_LABEL = "FFFFFFFF";
         
        private const string COLOR_HTML_LIGHT_LABEL = "111111FF";

        private Color COLOR_OBJ_WARNING => new Color(0.8f, 0.7f, 0.5f, 0.5f);

        private Color COLOR_OBJ_INFO => new Color(0.6f, 0.6f, 1.0f, 0.5f);

        private Color COLOR_OBJ_ERROR => new Color(1.0f, .4f, 0.4f, 0.5f);

        private Color COLOR_OBJ_DARK_SECTION_BACKGROUND => new Color(0, 0, 0, 0.5f);

        #endregion
        
        #region Properties

        /// <summary>
        /// Whether or not the <see cref="LocalizationManagerEditor"/> and <see cref="LocalizationManager"/> are
        /// both available and not null.
        /// </summary>
        public bool HasLocalizationManager
        {
            get => LocalizationManagerEditor && 
                   _localizationManagerEditor != null && 
                   _localizationManager &&
                   _localizationManager != null;
        }

        /// <summary>
        /// Whether or not the <see cref="modelEditorTreeView"/> object is not null.
        /// </summary>
        public bool HasTree => modelEditorTreeView != null;

        /// <summary>
        /// The <see cref="ModelEditorTreeView"/> object containing all tree data for this window.
        /// </summary>
        public ModelEditorTreeView modelEditorTreeView => _modelEditorTreeView;

        /// <summary>
        /// The <see cref="LocaleEditorTreeViewItem"/> object containing all tree data for this wondow's locales.
        /// </summary>
        public LocaleEditorTreeView localeEditorTreeView => _localeEditorTreeView;

        /// <summary>
        /// The localization manager editor reference tied to this window.
        /// </summary>
        public LocalizationManagerEditor LocalizationManagerEditor
        {
            get
            {
                // check to see if the localization manager editor is null but the reference to the actual localization
                // manager isn't null. This means the window has lost the reference to the editor.
                
                if (_localizationManager && !_localizationManagerEditor)
                {
                    _localizationManagerEditor = 
                        UnityEditor.Editor.CreateEditor(_localizationManager) as LocalizationManagerEditor;
                } 
                else if (!_localizationManager && _localizationManagerEditor)
                {
                    if (_localizationManagerEditor.target == null)
                    {
                        // lost reference. The target of the editor is null so everything must be reselected.
                        _localizationManagerEditor = null;
                        _localizationManager = null;
                    }
                }
                
                return _localizationManagerEditor;
            }
            set
            {
                _localizationManagerEditor = value;

                // update editor preferences

                if (_localizationManagerEditor)
                {
                    _localizationManager = (LocalizationManager) _localizationManagerEditor.target;

                    if (CurrentManagerIsPrefab)
                    {
                        var assetPath =
                            value 
                                ? AssetDatabase.GetAssetPath( _localizationManager.gameObject) 
                                : "";
                        
                        if (!string.IsNullOrWhiteSpace(assetPath))
                        {
                            EditorPrefs.SetString(EDITOR_PREF_KEY_LAST_EDITOR_STATE, assetPath);
                        }
                        
                        EditorPrefs.SetInt(EDITOR_PREF_KEY_LAST_USED_LM_TYPE,
                            (int) LastUsedLocalizationManagerType.Prefab);
                    }
                    else
                    {
                        if (!Application.isPlaying)
                        {
                            EditorPrefs.SetInt(EDITOR_PREF_LAST_OBJ_INSTANCE_ID_PRE_PLAYMODE,
                                _localizationManager.GetInstanceID());
                        }
                        
                        EditorPrefs.SetInt(EDITOR_PREF_KEY_LAST_USED_LM_TYPE,
                            (int) LastUsedLocalizationManagerType.SceneAsset);
                    }
                }
                else
                {
                    _localizationManager = null;
                }
                
                SaveEditorStateToPrefs();
                
                // update the tree now that a new object has been loaded 

                UpdateModelEditorTree(false);
                UpdateLocaleEditorTree(false);
            }
        }

        /// <summary>
        /// The current window's localization manager reference.
        /// </summary>
        public LocalizationManager LocalizationManager => _localizationManager;

        /// <summary>
        /// When enabled, denotes that the system should not run an error check when a model is changed. This will
        /// improve performance. Can be toggled on/off through the Settings page of the TTW.
        /// </summary>
        public bool ManualErrorChecks => _settings.manualErrorChecks;

        /// <summary>
        /// List of all currently used/loaded validation rules. Updates whenever the window has to re-initialize or
        /// <see cref="UpdateValidationRules"/> is called.
        /// </summary>
        public List<ValidationRule> AllValidationRules => _validationRules;


        /// <summary>
        /// List of enabled validation rules.
        /// </summary>
        public List<ValidationRule> EnabledValidationRules =>
            _validationRules
                .Where(validationRule => _settings.validationRuleStatusCollection[validationRule.Code])
                .ToList();

        /// <summary>
        /// True when the current <see cref="LocalizationManagerEditor"/> is a prefab and not a scene instance.
        /// </summary>
        public static bool CurrentManagerIsPrefab
        {
            get
            {
                if (!CurrentWindow || !CurrentWindow.LocalizationManagerEditor)
                {
                    return false;
                }

                var localizationManager = CurrentWindow.LocalizationManagerEditor.target as LocalizationManager;
                
                if (!localizationManager || localizationManager == null)
                {
                    return false;
                }

                var gameObject = localizationManager.gameObject;
                
#if UNITY_2018_3_OR_NEWER
                var prefabStage = UnityEditor.Experimental.SceneManagement.PrefabStageUtility.GetPrefabStage(gameObject);
                if (prefabStage != null)
                {
                    return true;
                }
#endif

                return gameObject.scene.rootCount == 0;
            }
        }

        /// <summary>
        /// Whether or not the TTW is open.
        /// </summary>
        public static bool isOpen => CurrentWindow;

        public static TongueTwisterWindowMode CurrentWindowMode => CurrentWindow._windowMode;

        /// <summary>
        /// The localization directory made from the current window's focused localization manager editor.
        /// </summary>
        public static LocalizationDictionary LocalizationDictionary =>
            ModelUtility.TreeToLocalizationDictionary(
                CurrentWindow._modelEditorTreeView.tree.Root,
                CurrentWindow.LocalizationManagerEditor.ConfiguredLocales.ToArray());
        
        /// <summary>
        /// The current TongueTwisterWindow reference.
        /// </summary>
        public static TongueTwisterWindow CurrentWindow { get; private set; }

        /// <summary>
        /// Retrieves all display keys in "full dot form" for the current configured LocalizationManager data.
        /// </summary>
        public static List<TongueTwisterModel> AllDisplayKeys =>
            CurrentWindow._modelEditorTreeView.tree.Data
                .Where(data => data.Type == TongueTwisterModel.ModelType.DisplayKey)
                .ToList();

        #endregion

        #region Private Misc

        /// <summary>
        /// Lust of the currently found validation rules. This list is refreshed whenever the window is opened or
        /// a re-compile is performed.
        /// </summary>
        private List<ValidationRule> _validationRules = new List<ValidationRule>();

        /// <summary>
        /// List of selection history of display keys.
        /// </summary>
        private IList<IList<int>> _modelEditorSelectionHistory = new List<IList<int>>();

        /// <summary>
        /// The current selection history index.
        /// </summary>
        private int _modelEditorSelectionHistoryIndex = 0;

        /// <summary>
        /// Used to keep track of when the system is exiting play mode so certain updates/changes can appear in the
        /// next frame.
        /// </summary>
        private bool _exitingPlayMode;

        /// <summary>
        /// True when <see cref="Initialize"/> has been called. If false and in update mode, will call
        /// <see cref="Initialize"/>.
        /// </summary>
        private bool _windowInitialized;

        /// <summary>
        /// The current Tongue Twister Window settings. They are stored in <see cref="TTWSettings"/> objects to better
        /// keep track of serialized preferences.
        /// </summary>
        private TTWSettings _settings = new TTWSettings();
        
        #endregion

        #region Unity Events

        private void OnEnable() => Initialize();

        private void OnDisable() => Shutdown();

        private void OnGUI() => WindowUpdate();

        #endregion

        #region Initialization

        /// <summary>
        /// Called at the start of this window, retrieves and sets up all resources.
        /// </summary>
        private void Initialize()
        {
            if (_windowInitialized)
            {
                return;
            }
            
            CurrentWindow = this;
            LoadEditorStateFromPrefs();
            CreateTitleContent();
            CreateWindowModeGuiContentMenuItems();
            CreateStyles();
            LoadLastLocalizationManagerAssetIfNull();
            SetupEvents();
            UpdateToolModeTree();
            UpdateValidationRules();
            LoadPreferenceSelections();
            UpdateValidationRuleSettings();
            GetAnnouncements();
            _windowInitialized = true;
        }

        /// <summary>
        /// Loads preferences saved by the window.
        /// </summary>
        private void LoadEditorStateFromPrefs()
        {
            if (EditorPrefs.HasKey(EDITOR_PREF_KEY_LAST_EDITOR_STATE))
                JsonUtility.FromJsonOverwrite(
                    EditorPrefs.GetString(EDITOR_PREF_KEY_LAST_EDITOR_STATE), 
                    this);
        }
        
        /// <summary>
        /// Sets the window title.
        /// </summary>
        private void CreateTitleContent()
        {
            titleContent = new GUIContent(
                EditorLabels.TongueTwisterWindow.Title,
                TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.LogoNoBackground)); 
        }

        /// <summary>
        /// Gets the last loaded manager editor if the current one is null. This only works if the last used
        /// localization manager editor was a prefab and not a scene object, as those are generally persisted.
        /// </summary>
        private void LoadLastLocalizationManagerAssetIfNull()
        {
            if (HasLocalizationManager)
            {
                // if the localization manager editor is not null and already present, there's no need to load anything.
                return;
            }
            
            if (EditorPrefs.HasKey(EDITOR_PREF_KEY_LAST_USED_LM_TYPE))
            {
                var lastUsedLocalizationManagerType =
                    (LastUsedLocalizationManagerType) EditorPrefs.GetInt(EDITOR_PREF_KEY_LAST_USED_LM_TYPE);

                // check the "last used localization manager" type to determine if the editor should get the most
                // recently used scene object (if available) or the last used prefab.
                    
                switch (lastUsedLocalizationManagerType)
                {
                    case LastUsedLocalizationManagerType.SceneAsset:
                        // do nothing - the object should maintain its reference through the recompile. This may not
                        // be true for every version of Unity, so if issues arise this may be a pain point to come
                        // back to.
                        break;
                        
                    case LastUsedLocalizationManagerType.Prefab:
                        // the prefab can only be loaded if an editor pref has been saved/set from a previous
                        // assignment call to the LocalizationManagerEditor
                        if (EditorPrefs.HasKey(EDITOR_PREF_KEY_LAST_USED_LM_PREFAB_ASSET_PATH))
                        {
                            var lastUsed = EditorPrefs.GetString(EDITOR_PREF_KEY_LAST_USED_LM_PREFAB_ASSET_PATH);
                            var localizationManagerAsset = AssetDatabase.LoadAssetAtPath<LocalizationManager>(lastUsed);
                            LocalizationManagerEditor = UnityEditor.Editor.CreateEditor(localizationManagerAsset) as LocalizationManagerEditor;
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Creates the GUI content menu items for the window modes.
        /// </summary>
        private void CreateWindowModeGuiContentMenuItems()
        {
            _windowModeOptions = new []
            {
                new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.Home.Label, 
                    TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.Home),
                    EditorLabels.LocalizationManager.EditorModes.Home.Tooltip), 
                
                new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Label, 
                    TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.Editor),
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Tooltip), 
                
                new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Text, 
                    TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.Locales),
                    EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Tooltip), 
            
                new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.Tools.Label, 
                    TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.Tools),
                    EditorLabels.LocalizationManager.EditorModes.Tools.Tooltip),

                new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.Settings.Text,
                    TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.Settings),
                    EditorLabels.LocalizationManager.EditorModes.Settings.Tooltip),
            };
        }

        /// <summary>
        /// Creates all of the used GUI styles. 
        /// </summary>
        private void CreateStyles()
        {
            _richStyle = new GUIStyle() { richText = true };
            _linkStyle = new GUIStyle();
            _linkStyle.normal.textColor = new Color(0f, 0.5f, 1f);
        }

        /// <summary>
        /// Loads styles that must reference the current GUI skin. This method must be called from within OnGUI() hence
        /// it's separate from the <c>CreateStyles()</c> method.
        /// </summary>
        private void LoadGuiStyles()
        {
            _toolbar = GUI.skin.FindStyle("toolbar");
            _toolbarSearchField = GUI.skin.FindStyle("ToolbarSeachTextField");
            _toolbarClearButton = GUI.skin.FindStyle("ToolbarSeachCancelButton");
            _adjustedLabelTextStyle = GUI.skin.label;
            _adjustedLabelTextStyle.richText = true;
        }

        /// <summary>
        /// Adds functions on this object to the various event method group chains like Undo and the treeview's stuff.
        /// </summary>
        private void SetupEvents()
        {
            Undo.undoRedoPerformed += OnUndoRedoPerformed;
            EditorApplication.playModeStateChanged += HandlePlayModeStateChange;
            
            // calling UpdateTree will register all events.
            UpdateModelEditorTree(false);
            UpdateLocaleEditorTree(false);
        }

        /// <summary>
        /// Updates the list of validation rules.
        /// </summary>
        private void UpdateValidationRules()
        {
            _validationRules = ValidationUtility.GetAllValidationRules();
        }

        private void LoadPreferenceSelections()
        {
            try
            {
                if (EditorPrefs.HasKey(EDITOR_PREF_SETTINGS))
                {
                    _settings = JsonUtility.FromJson<TTWSettings>(EditorPrefs.GetString(EDITOR_PREF_SETTINGS));
                }
                else
                {
                    _settings = new TTWSettings();
                    EditorPrefs.SetString(EDITOR_PREF_SETTINGS, JsonUtility.ToJson(_settings));
                }
            }
            catch (Exception exception)
            {
                Debug.LogError(string.Format(EditorLabels.TongueTwisterWindow.Errors.FailedToDeserializeTtwSettings, exception.Message));
            }
        }

        private void UpdateValidationRuleSettings()
        {
            _settings.validationRuleStatusCollection.UpdateList(_validationRules);
        }

        private void GetAnnouncements(bool force = false)
        {
            if (_settings.showAnnouncements &&
                !TTAnnouncementService.isGettingAnnouncements && 
                !TTAnnouncementService.hasAnnouncementsCollection)
            {
                TTAnnouncementService.UpdateAnnouncements();
            }
        }
        
        #endregion

        #region Destroy

        /// <summary>
        /// Called when the window is closing, it'll save editor preferences.
        /// </summary>
        private void Shutdown()
        {
            SaveEditorStateToPrefs();
            TTViewedAnnouncementsCollection.current?.Save();
            Undo.undoRedoPerformed -= OnUndoRedoPerformed;
            EditorApplication.playModeStateChanged -= HandlePlayModeStateChange;
            _windowInitialized = false;
            CurrentWindow = null;
        }

        #endregion

        #region Static Utilities

        /// <summary>
        /// Creates a new window or finds and focuses the existing one.
        /// </summary>
        /// note: unfortunately this cannot be converted into a language sensitive label
        [MenuItem("Tools/Tongue Twister/Manager")]
        public static TongueTwisterWindow Open()
        {
            return GetWindow<TongueTwisterWindow>();
        }
        
        /// <summary>
        /// Creates a new window or finds and focuses the existing one, populating it with the provided localization
        /// manager.
        /// </summary>
        public static void Open(LocalizationManagerEditor localizationManagerEditor)
        {
            var window = Open();
            window.LocalizationManagerEditor = localizationManagerEditor;
            window.RunModelEditorTreeModelErrorChecks();
        }

        #endregion

        #region Events

        /// <summary>
        /// Called whenever <c>_treeView</c> is changed.
        /// </summary>
        private void OnModelEditorTreeChanged()
        {
            if (!HasLocalizationManager)
            {
                return;
            }
            
            CommitModelEditorTreeModel();
            SetDirty();
        }

        /// <summary>
        /// Called whenever an undo occurs.
        /// </summary>
        private void OnUndoRedoPerformed()
        {
            if (!HasLocalizationManager)
            {
                return;
            }

            UnfocusAll();
            LocalizationManagerEditor.serializedObject.Update();
            _modelEditorTreeView?.tree.SetDataFromList(LocalizationManagerEditor.ModelCollection.Models);
            UpdateModelEditorTree();
            CommitModelEditorTreeModel();
            _localeEditorTreeView?.tree.SetDataFromList(LocalizationManagerEditor.ConfiguredLocales);
            UpdateLocaleEditorTree();
            CommitLocaleEditorTreeModel();
            SetDirty();
        }
        
        /// <summary>
        /// Called right before <c>_treeView</c>'s currently dragged items are dropped into another item.
        /// </summary>
        /// <param name="draggedRows"></param>
        private void OnModelEditorDragDroppedItems (IList<TreeViewItem> draggedRows)
        {
            RecordChange(
                _localizationManagerEditor.target,
                string.Format(
                    EditorLabels.LocalizationManager.EditorChanges.DragAndDrop,
                    draggedRows.Count.ToString(),
                    draggedRows.Count > 1 
                        ? EditorLabels.TongueTwisterWindow.Common.Rows
                        : EditorLabels.TongueTwisterWindow.Common.Row));
        }
        
        /// <summary>
        /// Called whenever the editor finishes reloading scripts. If there's a current window open, it will be found
        /// and "re-initialized" by calling <see cref="Initialize"/>. This process may also "reload" the last used
        /// Localization Manager Editor prefab reference.
        /// </summary> 
        [UnityEditor.Callbacks.DidReloadScripts]
        private static void OnScriptsReloaded()
        {
            var objectsOfTypeAll = Resources.FindObjectsOfTypeAll(typeof(TongueTwisterWindow));
            var editorWindow = objectsOfTypeAll.Length != 0 ? (EditorWindow) objectsOfTypeAll[0] : (EditorWindow) null;
            
            if (editorWindow == null)
            {
                return;
            }
            
            var ttw = (TongueTwisterWindow) editorWindow;
                
            if (ttw == null)
            {
                return;
            }
                
            ttw.Initialize();

            if (!ttw._settings.manualErrorChecks)
            {
                ttw.RunModelEditorTreeModelErrorChecks();
            }
        }

        #endregion

        #region Public Utilities

        /// <summary>
        /// Removes empty localizations (no translation text or locale) from the tree.
        /// </summary>
        public void RemoveEmptyLocalizations()
        {
            RecordLocalizationManagerEditorChange( 
                EditorLabels.LocalizationManager.EditorChanges.RemovedEmptyLocalizations);
            _modelEditorTreeView.Deselect();
            _modelEditorTreeView.tree.CleanLocalizations();
            SetDirty();
        }

        /// <summary>
        /// Removes empty display keys (no locales) from the tree.
        /// </summary>
        public void RemoveEmptyDisplayKeys()
        {
            RecordLocalizationManagerEditorChange( 
                EditorLabels.LocalizationManager.EditorChanges.RemovedEmptyDisplayKeys);
            _modelEditorTreeView.Deselect();
            _modelEditorTreeView.tree.CleanDisplayKeys();
            SetDirty();
        }
        
        /// <summary>
        /// Removes empty groups (no display keys) from the current tree.
        /// </summary>
        public void RemoveEmptyGroups()
        {
            RecordLocalizationManagerEditorChange( 
                EditorLabels.LocalizationManager.EditorChanges.RemovedEmptyGroups);
            _modelEditorTreeView.Deselect();
            _modelEditorTreeView.tree.CleanGroups();
            SetDirty();
        }

        /// <summary>
        /// Adds all sets found within the given <see cref="LocalizationDictionary"/> to the current
        /// <see cref="LocalizationManager"/>.
        /// </summary>
        /// <param name="localizationDictionary">The <see cref="LocalizationDictionary"/> to add content from.</param>
        public void MergeLocalizationDictionary(LocalizationDictionary localizationDictionary)
        {
            RecordLocalizationManagerEditorChange(EditorLabels.LocalizationManager.EditorChanges.AddedNewLocalizationDictionary);
            foreach (var key in localizationDictionary.Keys)
            {
                _modelEditorTreeView.tree.AddElement(key, localizationDictionary[key]);
            }
            SetDirty();
            Repaint();
        }

        /// <summary>
        /// Adds the given locale to the list of configured locales for the current localization manager.
        /// </summary>
        /// <param name="locale">The locale to add.</param>
        /// <param name="silent">When true, will not display dialogues for errors.</param>
        public void AddLocaleToConfiguredLocales(Locale locale = null)
        {
            var locales = LocalizationManagerEditor.ConfiguredLocales;
            
            RecordChange(
                LocalizationManagerEditor, 
                EditorLabels.LocalizationManager.EditorChanges.AddedLocaleToConfiguredLocaleList);
            
            locales.Add(locale ?? new Locale());
            LocalizationManagerEditor.ConfiguredLocales = locales;
            
            RunModelEditorTreeModelErrorChecks();
        }

        /// <summary>
        /// Adds a new model of the given type to the current selection. If no selection has been made, adds a new
        /// model to the root.
        /// </summary>
        /// <param name="modelType">The type of model to add.</param>
        /// <param name="repaint">Forces the window to repaint if true.</param>
        public List<TongueTwisterModel> AddNewModelToSelectionOrRoot(TongueTwisterModel.ModelType modelType, bool repaint = true)
        {
            var models = InternalAddNewModelToSelectionOrRoot(modelType);
            
            if (repaint)
            {
                Repaint();
            }

            return models;
        }

        /// <summary>
        /// Adds a new model of the given type to the given parent.
        /// </summary>
        /// <param name="modelType">The type of model to add.</param>
        /// <param name="parent">The parent to add the model to.</param>
        /// <param name="repaint">Forces the window to repaint if true.</param>
        public TongueTwisterModel AddNewModel(TongueTwisterModel.ModelType modelType, TongueTwisterModel parent, bool repaint = true)
        {
            var model = InternalAddNewModel(modelType, parent);

            if (repaint)
            {
                Repaint();
            }

            return model;
        }

        /// <summary>
        /// Duplicates the current selection.
        /// </summary>
        public void DuplicateModelEditorSelection()
        {
            DuplicateSelectedTreeViewItems();
            Repaint();
        }

        /// <summary>
        /// Internally copies the current selection.
        /// </summary>
        public void CopyModelEditorSelection()
        {
            CopySelectedTreeViewItems();
        }

        /// <summary>
        /// Internally pastes copied models to the current selection.
        /// </summary>
        public void PasteModelEditorSelection()
        {
            PasteCopiedTreeViewItems();
            Repaint();
        }

        /// <summary>
        /// Deletes the current selection.
        /// </summary>
        public void DeleteModelEditorSelection()
        {
            RemoveSelectedModelTreeViewItems();
            Repaint();
        }

        /// <summary>
        /// Calls <see cref="RecordChange"/> and uses the <see cref="LocalizationManagerEditor"/> target.
        /// </summary>
        /// <param name="change">The change to record.</param>
        public void RecordLocalizationManagerEditorChange(string change)
        {
            RecordChange(LocalizationManagerEditor.target, change);
            CommitModelEditorTreeModel();
            SetDirty();
        }

        /// <summary>
        /// Sets the current localization tree asset, localization settings, and active scene as dirty.
        /// </summary>
        public new void SetDirty()
        {
            EditorUtility.SetDirty(LocalizationManagerEditor.target);
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }

        public void SetCurrentLocaleISOCode(ISO639Alpha2 iso639Alpha2)
        {
            var locale = GetCurrentlySelectedLocale();

            if (locale.Metadata.ISOLanguageCode != iso639Alpha2)
            {
                RecordLocalizationManagerEditorChange(EditorLabels.LocalizationManager.EditorChanges.ChangedLocalesLanguageCode);
                locale.Metadata.ISOLanguageCode = iso639Alpha2;
                UpdateLocaleEditorTree();
                CommitLocaleEditorTreeModel();
                SetDirty();
                Repaint();
            }
        }

        public void SetCurrentLocaleISOCode(ISO3166Alpha2 iso3166Alpha2)
        {
            var locale = GetCurrentlySelectedLocale();

            if (locale.Metadata.ISOCountryCode != iso3166Alpha2)
            {
                RecordLocalizationManagerEditorChange(EditorLabels.LocalizationManager.EditorChanges.ChangedLocalesCountryCode);
                locale.Metadata.ISOCountryCode = iso3166Alpha2;
                UpdateLocaleEditorTree();
                CommitLocaleEditorTreeModel();
                SetDirty();
                Repaint();
            }
        }

        public Locale GetCurrentlySelectedLocale()
        {
            if (!_localeEditorTreeView.HasSelection())
            {
                return null;
            }

            var selectedItems = _localeEditorTreeView.GetSelection();

            if (selectedItems.Count > 1)
            {
                return null;
            }

            var firstIndex = selectedItems.First();

            if (firstIndex >= LocalizationManagerEditor.ConfiguredLocales.Count)
            {
                _localeEditorTreeView.Deselect();
                return null;
            }

            return LocalizationManagerEditor.ConfiguredLocales[firstIndex];
        }
        
        #endregion

        #region Private Utilities

        private void UnfocusAll(bool guiFocusControl = true, bool editorGuiFocusTextInControl = true, bool repaint = true)
        {
            if (guiFocusControl)
            {
                GUI.FocusControl(null);
            }

            if (editorGuiFocusTextInControl)
            {
                EditorGUI.FocusTextInControl(null);
            }

            if (repaint)
            {
                Repaint();
            }
        }

        /// <summary>
        /// Causes the current tree view to get updated. Called specifically whenever the localization manager editor
        /// has been changed or updated. The tree view will be re-instantiated here. 
        /// </summary>
        /// <param name="setDirty">Flag to determine whether or not to call <c>SetDirty()</c> as well.</param>
        private void UpdateModelEditorTree(bool setDirty = true)
        {
            if (_modelEditorTreeView != null)
            {
                // remove the "on tree changed" event because the tree view is about to be re-instantiated and any
                // previous connections have to be removed if there is currently a non-null tree.
                _modelEditorTreeView.OnTreeChanged -= OnModelEditorTreeChanged;
            }

            if (_modelEditorGroupTreeState == null)
            {
                _modelEditorGroupTreeState = new TreeViewState();
            }
            
            _modelEditorMultiColumnHeaderState = ModelEditorTreeView.CreateDefaultMultiColumnHeaderState();

            var models = 
                (HasLocalizationManager && LocalizationManagerEditor?.ModelCollection?.Models != null)
                    ? LocalizationManagerEditor.ModelCollection.Models
                    : new List<TongueTwisterModel>();

            _modelEditorTreeView = 
                new ModelEditorTreeView(
                    _modelEditorGroupTreeState,
                    new MultiColumnHeader(_modelEditorMultiColumnHeaderState), 
                    models != null && models.Count > 0
                        ? new ModelEditorTree(models)
                        : new ModelEditorTree(new List<TongueTwisterModel>()));

            // reminder that "HasVisibleSelection()" takes into account items that are in collapsed parents. The 
            // tree view API will consider something selected even if it's not visible. 
            
            if (_modelEditorTreeView.HasSelection() && !_modelEditorTreeView.HasVisibleSelection())
            {
                _modelEditorTreeView.SetSelection(new List<int>());
            }
            
            _modelEditorTreeView.OnTreeChanged += OnModelEditorTreeChanged;
            _modelEditorTreeView.OnDroppedDraggedItems += OnModelEditorDragDroppedItems;

            if (setDirty)
            {
                SetDirty();
            }

            if (LocalizationManagerEditor)
            {
                RunModelEditorTreeModelErrorChecks();
            }
        }

        private void UpdateLocaleEditorTree(bool setDirty = true)
        {
            if (_localeEditorTreeState == null)
            {
                _localeEditorTreeState = new TreeViewState();
            }

            _localeEditorMultiColumnHeaderState = LocaleEditorTreeView.CreateDefaultMultiColumnHeaderState();
            
            var locales = 
                (HasLocalizationManager && LocalizationManagerEditor?.ConfiguredLocales != null)
                    ? LocalizationManagerEditor.ConfiguredLocales
                    : new List<Locale>();
            
            _localeEditorTreeView = 
                new LocaleEditorTreeView(
                    _localeEditorTreeState,
                    new MultiColumnHeader(_localeEditorMultiColumnHeaderState), 
                    locales != null && locales.Count > 0
                        ? new LocaleEditorTree(locales)
                        : new LocaleEditorTree(new List<Locale>()));
            
            if (setDirty)
            {
                SetDirty();
            }
        }

        private void UpdateToolModeTree()
        {
            if (_toolsModeTreeState == null)
            {
                _toolsModeTreeState = new TreeViewState();
            }
            
            var toolsTypes = AppDomain.CurrentDomain.GetAllDerivedTypes(typeof(TongueTwisterTool));
            var tools = new List<TongueTwisterTool>();
            
            foreach (var toolType in toolsTypes)
            {
                try
                {
                    tools.Add((TongueTwisterTool)Activator.CreateInstance(toolType));
                }
                catch (Exception exception)
                {
                    Debug.LogError(exception);
                }
            }

            _toolsModeMultiColumnHeaderState = ToolTreeView.CreateDefaultMultiColumnHeaderState();

            _toolsModeTreeView = 
                new ToolTreeView(
                    _toolsModeTreeState,
                    new MultiColumnHeader(_toolsModeMultiColumnHeaderState), 
                    tools.Count > 0
                        ? new ToolTree(tools)
                        : new ToolTree(new List<TongueTwisterTool>()));
        }

        /// <summary>
        /// Records the state of an object and the string details of the change about to happen to it.
        /// </summary>
        /// <param name="o">The object to record a change on.</param>
        /// <param name="change">The change that is occurring.</param>
        private void RecordChange(Object o, string change) => Undo.RegisterCompleteObjectUndo(o, change);

        /// <summary>
        /// Saves the current state of the editor to EditorPrefs.
        /// </summary>
        private void SaveEditorStateToPrefs()
        {
            EditorPrefs.SetString(EDITOR_PREF_KEY_LAST_EDITOR_STATE, JsonUtility.ToJson(this));
            EditorPrefs.SetString(EDITOR_PREF_SETTINGS, JsonUtility.ToJson(_settings));
        }

        /// <summary>
        /// Starts an error check against all models.
        /// </summary>
        private void RunModelEditorTreeModelErrorChecks() => _modelEditorTreeView.RunRootErrorCheck(EnabledValidationRules);

        /// <summary>
        /// Draws a custom label with the given style details.
        /// </summary>
        /// <param name="label">The label text.</param>
        /// <param name="bold">Enables bold on the text.</param>
        /// <param name="italics">Enables italics on the text.</param>
        /// <param name="fontSize">The font size.</param>
        /// <param name="color">The HTML code color.</param>
        /// <param name="style">The gui style.</param>
        private void DrawCustomLabel(string label, bool bold, bool italics, float? fontSize, string color, GUIStyle style)
        {
            style.richText = bold || italics || fontSize != null;
            if (bold) label = $"<b>{label}</b>";
            if (italics) label = $"<i>{label}</i>";
            if (fontSize != null) label = $"<size={fontSize}>{label}</size>";
            if (!string.IsNullOrWhiteSpace(color)) label = $"<color=#{color}>{label}</color>";
            GUILayout.Label(label, style);
        }

        /// <summary>
        /// Draws an integer <see cref="num"/> and adds "1" to the value as if it were a for-loop index.
        /// </summary>
        /// <param name="num"></param>
        private void DrawFriendlyIndexNum(int num)
        {
            GUILayout.Label($"#{num + 1}", EditorStyles.boldLabel, GUILayout.Width(30));
        }

        private bool CheckIsProSkin() => EditorGUIUtility.isProSkin;

        private void GoBackModelEditorSelection()
        {
            _modelEditorSelectionHistoryIndex--;
            _modelEditorTreeView.SetSelection(_modelEditorSelectionHistory[_modelEditorSelectionHistoryIndex]);
        }

        private void GoForwardModelEditorSelection()
        {
            _modelEditorSelectionHistoryIndex++;
            _modelEditorTreeView.SetSelection(_modelEditorSelectionHistory[_modelEditorSelectionHistoryIndex]);
        }

        private bool CompareIListContent(IList<int> left, IList<int> right)
        {
            if (left.Count != right.Count) return false;

            for (int i = 0; i < left.Count; i++)
            {
                if (left[i] != right[i]) return false;
            }

            return true;
        }

        /// <summary>
        /// Serializes the current tree view data to the target.
        /// </summary>
        private void CommitModelEditorTreeModel()
        {
            LocalizationManagerEditor.ModelCollection = new TongueTwisterModelCollection(_modelEditorTreeView.tree.Data);
        }

        /// <summary>
        /// Serializes the current tree view data to the target.
        /// </summary>
        private void CommitLocaleEditorTreeModel()
        {
            LocalizationManagerEditor.ConfiguredLocales = _localeEditorTreeView.tree.Data.ToList();
        }

        #endregion

        #region Updates

        /// <summary>
        /// Calls various window related GUI area functions based on availability of current localization settings and
        /// localization tree asset.
        /// </summary>
        private void WindowUpdate()
        {
            if (!_windowInitialized)
            {
                Initialize();
            }
            
            LoadGuiStyles();

            if (EditorApplication.isCompiling)
            {
                DrawCompilingGui();
                return;
            }

            if (!EditorApplication.isPlaying)
            {
                EditorModeUpdate();
            }
            else
            {
                PlaybackModeUpdate();
            }
        }

        private void EditorModeUpdate()
        {
            GUILayout.BeginVertical();
            DrawWindowModeButtonGroup();
            DrawDefaultSettings();
            DrawAnnouncementsBanner();
            DrawWindowModeArea();
            GUILayout.EndVertical();
            EditorModeControlsUpdate();
            PostEditorModeUpdate();
        }

        private void PlaybackModeUpdate()
        {
            GUILayout.BeginVertical();
            DrawWindowModeArea();
            GUILayout.EndVertical();
        }

        private void PostEditorModeUpdate()
        {
            try
            {
                switch (_windowMode)
                {
                    case TongueTwisterWindowMode.DisplayKeyEditor:
                        EditorModeSelectionTrackingUpdate();
                        break;
                }
            }
            catch (Exception exception)
            {
                Debug.LogError($"Failed to do post update, reason: {exception.Message}");
            }
        }
        
        /// <summary>
        /// Evaluates the current event key presses to perform operations.
        /// </summary>
        private void EditorModeControlsUpdate()
        {
            var currentEvent = Event.current;

            if (currentEvent == null) return;
            
            switch (_windowMode)
            {
                case TongueTwisterWindowMode.DisplayKeyEditor:
                    if (currentEvent.rawType == EventType.KeyDown && _modelEditorTreeView != null)
                    {
                        if (currentEvent.keyCode == KeyCode.Delete)
                        {
                            RemoveSelectedModelTreeViewItems();
                        }
                        else if (currentEvent.keyCode == KeyCode.D && currentEvent.control)
                        {
                            DuplicateSelectedTreeViewItems();
                        }
                        else if (currentEvent.keyCode == KeyCode.C && currentEvent.control)
                        {
                            CopySelectedTreeViewItems();
                        }
                        else if (currentEvent.keyCode == KeyCode.V && currentEvent.control)
                        {
                            PasteCopiedTreeViewItems();
                        }
                    }
                    // bug: https://trello.com/c/x8fKZ2Kk/8-mouse3-mouse4-key-events-not-picking-up
                    else if (currentEvent.rawType == EventType.KeyDown && _modelEditorTreeView != null && _modelEditorSelectionHistory.Count > 0)
                    {
                        if (currentEvent.keyCode == KeyCode.Mouse3 && currentEvent.control && 
                            _modelEditorSelectionHistoryIndex < _modelEditorSelectionHistory.Count - 2)
                        {
                            GoForwardModelEditorSelection();
                        }
                        else if (currentEvent.keyCode == KeyCode.Mouse4 && currentEvent.control &&
                                 _modelEditorSelectionHistoryIndex > 0)
                        {
                            GoBackModelEditorSelection();
                        }
                    }
                    break;
                case TongueTwisterWindowMode.LocaleEditor:
                    if (currentEvent.rawType == EventType.KeyDown && _localeEditorTreeView != null)
                    {
                        if (currentEvent.keyCode == KeyCode.Delete)
                        {
                            RemoveSelectedLocaleTreeViewItems();
                        }
                    }
                    break;
            }
        }

        private void EditorModeSelectionTrackingUpdate()
        {
            if (!LocalizationManagerEditor) return;
            
            var selection = _modelEditorTreeView.GetSelection();
            
            if (_modelEditorSelectionHistory.Count <= _modelEditorSelectionHistoryIndex)
            {
                _modelEditorSelectionHistoryIndex = _modelEditorSelectionHistory.Count - 1;
            }

            if (_modelEditorSelectionHistoryIndex < 0)
            {
                _modelEditorSelectionHistoryIndex = 0;
            }

            if (_modelEditorSelectionHistory.Count == 0)
            {
                _modelEditorSelectionHistory.Add(selection);
            }
            else
            {
                if (!CompareIListContent(selection, _modelEditorSelectionHistory[_modelEditorSelectionHistoryIndex]))
                {
                    // rewrite history 
                    
                    if (_modelEditorSelectionHistoryIndex != _modelEditorSelectionHistory.Count - 1)
                    {
                        for (int i = _modelEditorSelectionHistoryIndex + 1; i < _modelEditorSelectionHistory.Count; i++)
                        {
                            _modelEditorSelectionHistory.RemoveAt(i);
                        }
                    }

                    _modelEditorSelectionHistory.Add(selection);
                    _modelEditorSelectionHistoryIndex++;
                }
            }
        }

        #endregion

        #region GUI Functions - General

        /// <summary>
        /// Checks to see if the editor is currently using the pro (dark) skin. Will provide the correct HTML based
        /// color string.
        /// </summary>
        /// <returns>An HTML based color string including the alpha value.</returns>
        private string GetLabelColor()
        {
            return CheckIsProSkin() ? COLOR_HTML_DARK_LABEL : COLOR_HTML_LIGHT_LABEL;
        }

        /// <summary>
        /// Draws a fully flexible string stating that the editor is currently compiling.
        /// </summary>
        private void DrawCompilingGui()
        {
            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(EditorLabels.LocalizationManager.Compiling.PleaseWait);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.FlexibleSpace();
        }

        /// <summary>
        /// Draws the announcements banner if there are any announcements and the system is allowed to show
        /// announcements.
        /// </summary>
        private void DrawAnnouncementsBanner()
        {
            if (!_settings.showAnnouncements || !TTViewedAnnouncementsCollection.current?.HasUnreadAnnouncements() == true)
            {
                return; 
            }

            DrawInfo(
                EditorLabels.TTAnnouncementsWindow.NewAnnouncements,
                () =>
                {
                    GUILayout.BeginVertical(GUILayout.Height(35));
                    GUILayout.FlexibleSpace();
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button(EditorLabels.TTAnnouncementsWindow.View))
                    {
                        TTAnnouncementsWindow.Open();
                    }
                    if (GUILayout.Button(EditorLabels.TTAnnouncementsWindow.MarkAsRead))
                    {
                        TTViewedAnnouncementsCollection.current?.MarkAllAsRead();
                        TTViewedAnnouncementsCollection.current?.Save();
                    }
                    if (GUILayout.Button(EditorLabels.TTAnnouncementsWindow.Hide))
                    {
                        _settings.showAnnouncements = false;
                        EditorUtility.DisplayDialog(
                            EditorLabels.TTAnnouncementsWindow.HideDialog.Title,
                            EditorLabels.TTAnnouncementsWindow.HideDialog.Message,
                            EditorLabels.TTAnnouncementsWindow.HideDialog.Ok);
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.FlexibleSpace();
                    GUILayout.EndVertical();
                });
        }
        
        /// <summary>
        /// Draws the controls for default settings such as editor language and asset selection.
        /// </summary>
        private void DrawDefaultSettings()
        {
            GUILayout.BeginVertical(GUI.skin.box);

            var localizationManager = (LocalizationManager) EditorGUILayout.ObjectField(
                new GUIContent(
                    EditorLabels.LocalizationManager.General.Title,
                    EditorLabels.LocalizationManager.General.Tooltip),
                LocalizationManager,
                typeof(LocalizationManager),
                true);

            if (LocalizationManager != localizationManager)
            {
                RecordChange(
                    this,
                    EditorLabels.TongueTwisterWindow.EditorChanges.ChangedLocalizationManager);
                LocalizationManagerEditor =
                    UnityEditor.Editor.CreateEditor(localizationManager) as LocalizationManagerEditor;
            }

            // other default settings should go here. Currently, there are none

            GUILayout.EndVertical();
        }

        /// <summary>
        /// Draws the window modes button groupEditor as a <c>GUILayout.SelectionGrid(...)</c>
        /// </summary>
        private void DrawWindowModeButtonGroup()
        {
            _windowMode = (TongueTwisterWindowMode) GUILayout.SelectionGrid(
                (int) _windowMode,
                _windowModeOptions,
                _windowModeOptions.Length,
                GUILayout.Height(WINDOW_MODE_BUTTON_HEIGHT));
        }

        /// <summary>
        /// Calls one of several methods dependent on the current window mode stored in <c>_windowMode</c>. 
        /// </summary>
        private void DrawWindowModeArea()
        {
            // if the editor app is in playback mode, force the home mode area and display warning
            
            if (EditorApplication.isPlaying)
            {
                DrawWarning(EditorLabels.TongueTwisterWindow.Warnings.PlaybackModeWarning);
                DrawHomeModeArea();
                return;
            }
            
            // only three window modes require the localization manager to not be null, the "About" mode can be 
            // called without an object. 
            
            if (HasLocalizationManager && !CurrentManagerIsPrefab)
            {
#if UNITY_2018_3_OR_NEWER
                var prefab = PrefabUtility.GetCorrespondingObjectFromSource(_localizationManager);
                if (prefab)
                {
                    DrawWarning(EditorLabels.TongueTwisterWindow.Warnings.PrefabWarning, () =>
                    {
                        GUILayout.BeginVertical(GUILayout.Height(35));
                        GUILayout.FlexibleSpace();
                        GUILayout.BeginHorizontal();
                        if (GUILayout.Button(
                            new GUIContent(
                                EditorLabels.TongueTwisterWindow.Warnings.PrefabWarningButtons.OpenPrefab.Text,
                                EditorLabels.TongueTwisterWindow.Warnings.PrefabWarningButtons.OpenPrefab.Tooltip)))
                        {
                            Open(UnityEditor.Editor.CreateEditor(prefab)
                                as LocalizationManagerEditor);
                        }
                        GUILayout.EndHorizontal();
                        GUILayout.FlexibleSpace();
                        GUILayout.EndVertical();
                    });
                }
                else if (!_settings.hidePrefabWarning)
                {
                    DrawWarning(EditorLabels.TongueTwisterWindow.Warnings.CreatePrefabWarning, () =>
                    {
                        GUILayout.BeginVertical(GUILayout.Height(35));
                        GUILayout.FlexibleSpace();
                        GUILayout.BeginHorizontal();
                        if (GUILayout.Button(
                            new GUIContent(
                                EditorLabels.TongueTwisterWindow.Warnings.PrefabWarningButtons.CreatePrefab.Text,
                                EditorLabels.TongueTwisterWindow.Warnings.PrefabWarningButtons.CreatePrefab
                                    .Tooltip)))
                        {
                            var localPath = "Assets/" + _localizationManager.name + ".prefab";
                            localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

                            var newPrefab = PrefabUtility.SaveAsPrefabAssetAndConnect(
                                _localizationManager.gameObject,
                                localPath,
                                InteractionMode.UserAction);

                            Open(UnityEditor.Editor.CreateEditor(newPrefab.GetComponent<LocalizationManager>())
                                as LocalizationManagerEditor);
                        }

                        if (GUILayout.Button(
                            new GUIContent(
                                EditorLabels.TongueTwisterWindow.Warnings.PrefabWarningButtons.HideWarning.Text,
                                EditorLabels.TongueTwisterWindow.Warnings.PrefabWarningButtons.HideWarning
                                    .Tooltip)))
                        {
                            _settings.hidePrefabWarning = true;
                        }
                        GUILayout.EndHorizontal();
                        GUILayout.FlexibleSpace();
                        GUILayout.EndVertical();
                    });
                }
#else
                DrawWarning(EditorLabels.TongueTwisterWindow.Warnings.PrefabWarning);
#endif
            }

            _windowScrollArea = GUILayout.BeginScrollView(_windowScrollArea, GUIStyle.none);

            // draw main scrollable window area

            switch (_windowMode)
            {
                case TongueTwisterWindowMode.Home:
                    DrawHomeModeArea();
                    break;

                case TongueTwisterWindowMode.Settings:
                    if (HasLocalizationManager)
                    {
                        DrawLocalizationSettingsSection();
                        DrawSettingsModePreferences();
                    }
                    else
                    {
                        DrawError(EditorLabels.LocalizationManager.Errors.NoLocalizationManagerSelected);
                        DrawSettingsModeNoLocalizationWarning();
                        DrawSettingsModePreferences();
                    }
                    DrawValidationRulesToggleSection();
                    break;

                case TongueTwisterWindowMode.LocaleEditor:
                    if (HasLocalizationManager)
                    {
                        DrawLocaleEditorModeArea();
                    }
                    else
                    {
                        DrawError(EditorLabels.LocalizationManager.Errors.NoLocalizationManagerSelected);
                    }

                    break;

                case TongueTwisterWindowMode.DisplayKeyEditor:
                    if (HasLocalizationManager)
                    {
                        DrawEditorModeArea();
                    }
                    else
                    {
                        DrawError(EditorLabels.LocalizationManager.Errors.NoLocalizationManagerSelected);
                    }

                    break;

                case TongueTwisterWindowMode.Tools:
                    if (HasLocalizationManager)
                    {
                        DrawToolsModeArea();
                    }
                    else
                    {
                        DrawError(EditorLabels.LocalizationManager.Errors.NoLocalizationManagerSelected);
                    }

                    break;
            }

            GUILayout.EndScrollView();
        }

        /// <summary>
        /// Draws a horizontal break bar.
        /// </summary>
        /// <param name="marginTop"></param>
        /// <param name="marginBottom"></param>
        /// <param name="marginLeft"></param>
        /// <param name="marginRight"></param>
        private void DrawHorizontalBreak(float marginTop = 0.0f, float marginBottom = 0.0f, float marginLeft = 0.0f,
            float marginRight = 0.0f)
            => DrawHorizontalBreak(2.0f, marginTop, marginBottom, marginLeft, marginRight); 
        
        /// <summary>
        /// Draws a horizontal break bar. 
        /// </summary>
        /// <param name="height"></param>
        /// <param name="marginTop"></param>
        /// <param name="marginBottom"></param>
        /// <param name="marginLeft"></param>
        /// <param name="marginRight"></param>
        private void DrawHorizontalBreak(float height, float marginTop = 0.0f, float marginBottom = 0.0f, 
            float marginLeft = 0.0f, float marginRight = 0.0f)
        {
            var hasProLicense = CheckIsProSkin();
            var color = Color.black;
            color.a = hasProLicense ? 0.9f : 0.1f; 
            
            GUILayout.BeginVertical();
            GUILayout.Space(marginTop); 
            GUILayout.BeginHorizontal();
            GUILayout.Space(marginLeft);
            var originalColor = GUI.color;
            GUI.color = color;
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(height));
            GUI.color = originalColor;
            GUILayout.Space(marginRight); 
            GUILayout.EndHorizontal();
            GUILayout.Space(marginBottom); 
            GUILayout.EndVertical();
        }

        /// <summary>
        /// Draws a neat section start area that comes with a title and description. Make sure to call
        /// <see cref="DrawSectionEnd"/> afterwards.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="titleSize"></param>
        private void DrawSectionStart(string title, string description, float titleSize = 14)
        {
            var originalColor = GUI.color;
            var proSkinColor = COLOR_OBJ_DARK_SECTION_BACKGROUND;
            if (CheckIsProSkin())
            {
                GUI.color = proSkinColor;
            }
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            GUILayout.Space(15.0f);
            GUILayout.BeginVertical();
            
            GUILayout.BeginVertical();
            DrawCustomLabel(title, true, false, titleSize, GetLabelColor(), _richStyle);
            DrawHorizontalBreak();
            GUILayout.EndVertical();
            
            DrawCustomLabel(description, false, true, null, GetLabelColor(), EditorStyles.wordWrappedLabel);

            GUILayout.Space(15.0f);
            GUI.color = originalColor;
        }
        
        /// <summary>
        /// Draws a neat section end area. Should follow GUILayout elements that are preceded by a
        /// <see cref="DrawSectionStart"/>.
        /// </summary>
        private void DrawSectionEnd()
        {
            GUILayout.Space(15.0f);
            GUILayout.EndVertical();
            GUILayout.Space(15.0f); 
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }

        /// <summary>
        /// Draws a sub section (similar to a normal section, see
        /// <see cref="DrawSectionStart"/>). This is the shortened version, see
        /// <see cref="DrawSubSectionStart"/> for more information.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="rightAlignedTitleGuiActions"></param>
        private void DrawSubSectionStart(string title, params Action[] rightAlignedTitleGuiActions)
        {
            DrawSubSectionStart(title, 12.0f, false, rightAlignedTitleGuiActions);
        }

        /// <summary>
        /// Draws a sub section (similar to a normal section, see <see cref="DrawSectionStart"/>), except with
        /// more control and no description text. Also has the ability to call upon several parameterized actions which
        /// can draw more content to the right of the title. Call <see cref="DrawSectionEnd"/> afterwards.
        /// </summary>
        /// <param name="title">The title of this sub section.</param>
        /// <param name="titleSize">The font size of the title text.</param>
        /// <param name="useGuiSkinBox">Determines whether or not the GUI.skin.box class style will get applied to he
        /// vertical area that makes up the section.</param>
        /// <param name="rightAlignedTitleGuiActions">Actions that'll get called after the title label is drawn. Can be
        /// used to draw buttons or more labels.</param>
        private void DrawSubSectionStart(string title, float titleSize = 12, bool useGuiSkinBox = false, 
            params Action[] rightAlignedTitleGuiActions)
        {
            if (useGuiSkinBox) GUILayout.BeginVertical(GUI.skin.box);
            else GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            GUILayout.Space(15.0f);
            GUILayout.BeginVertical();
            
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            DrawCustomLabel(title, true, false, titleSize, GetLabelColor(), EditorStyles.wordWrappedLabel);
            GUILayout.FlexibleSpace();
            if (rightAlignedTitleGuiActions != null)
                foreach (var action in rightAlignedTitleGuiActions)
                    action?.Invoke(); 
            GUILayout.EndHorizontal();
            DrawHorizontalBreak();
            GUILayout.EndVertical();

            GUILayout.Space(15.0f);
        }

        /// <summary>
        /// Draws a label that is centered horizontally and vertically.
        /// </summary>
        /// <param name="label"></param>
        private void DrawFullyFlexibleLabel(string label)
        {
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(label);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
        }

        /// <summary>
        /// Draws a label that is centered horizontally.
        /// </summary>
        /// <param name="label"></param>
        private void DrawHorizontallyFlexibleLabel(string label)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(label);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// Creates a draggable zone that modifies the width of a left and right panel, creating a split drag effect.
        /// </summary>
        /// <param name="drawLeftPanel">An action that draws GUILayout content, after drawing GUILayoutUtility is
        ///     called to retrieve the last drawn rect.</param>
        /// <param name="drawRightPanel">An action that draws GUILayout content, after drawing GUILayoutUtility is
        ///     called to retrieve the last drawn rect.</param>
        /// <param name="leftPanelStyle">The style used on the left panel.</param>
        /// <param name="rightPanelStyle">The style used on the right panel.</param>
        /// <param name="draggingSplitter">State of the mouse event determining whether or not the user is currently
        ///     dragging the split area.</param>
        /// <param name="splitLerp">The value representing where the split zone is on a scale of 0.0f to 1.0f
        ///     horizontally. Think of this as like a slider value.</param>
        /// <param name="lastLeftPanelDraw">The rect of result from <c>GUILayoutUtility.GetLastRect()</c> after
        /// drawing the left panel.</param>
        /// <param name="dragZoneWidth">The width of the area that exists as the drag zone. The higher this value is,
        ///     the easier it is to find it with the cursor and drag to either side.</param>
        /// <param name="leftPanelMinPercentageWidth">The min width that the left panel can be on a scale of 0.0f to
        ///     1.0f, this is alternatively the width of how large the right panel can be when subtracted from one.
        /// </param>
        /// <param name="leftPanelMaxPercentageWidth">The max width that the right panel can be on a scale of 0.0f to
        ///     1.0f, this is alternatively how small the right panel can be when subtracted from 1.0f.</param>
        private void DrawHorizontalSplitPanelGroup(Action drawLeftPanel, Action drawRightPanel, 
            GUIStyle leftPanelStyle, GUIStyle rightPanelStyle,
            ref bool draggingSplitter, ref float splitLerp, ref Rect lastLeftPanelDraw, float dragZoneWidth = 5.0f, 
            float leftPanelMinPercentageWidth = 0.25f, float leftPanelMaxPercentageWidth = 0.75f)
        {
            GUILayout.BeginHorizontal();

            GUILayout.BeginVertical(leftPanelStyle, GUILayout.Width(Screen.width * splitLerp));
            drawLeftPanel();
            GUILayout.EndVertical();

            var leftPanel = GUILayoutUtility.GetLastRect();
            
            if (leftPanel.width > 1)
            {
                // checking for > 1 prevents the horrible "empty rect" issue that sometimes comes out of GetLastRect()
                lastLeftPanelDraw = leftPanel;
            }

            GUILayout.BeginVertical(rightPanelStyle);
            drawRightPanel();
            GUILayout.EndVertical();

            var rightPanel = GUILayoutUtility.GetLastRect();
            
            GUILayout.EndHorizontal();
            
            EditorGUIUtility.AddCursorRect(
                new Rect(
                    // x = the nav panel's x PLUS the nav panel's width MINUS the splitter dragZoneWidth ... AKA
                    // x = navPanelX + navPanelWidth - dragZoneWidth
                    // this will provide the left-most x offset of the nav panel and add the width and of course take
                    // away the splitter dragZoneWidth value (which makes it easier to grab the splitter)
                    leftPanel.x + leftPanel.width - dragZoneWidth, 
                    // y is simple because it aligns directly with both panels/rects
                    leftPanel.y, 
                    // take the whole screen width, subtract both the groupEditor and nav rect's width, subtract both
                    // the groupEditor and nav rect's x (because their X position provides a slight margin from the
                    // actual screen edges which must be accounted for), and add double the splitter dragZoneWidth
                    // (to account for both sides of the splitter)
                    Screen.width - leftPanel.width - rightPanel.width - (leftPanel.x * 2) +
                    (dragZoneWidth * 2),
                    // height is as simple as the Y because this area should align direclty with those two locations
                    leftPanel.height), 
                MouseCursor.SplitResizeLeftRight);
            
            // do drag checking 

            if (Event.current != null)
            {
                var mousePosition = Event.current.mousePosition;

                switch (Event.current.rawType)
                {
                    case EventType.MouseDown:
                        if (mousePosition.x > leftPanel.x + leftPanel.width - dragZoneWidth &&
                            mousePosition.x < rightPanel.x + dragZoneWidth &&
                            mousePosition.y > leftPanel.y &&
                            mousePosition.y < leftPanel.y + leftPanel.height)
                            draggingSplitter = true;
                        break;
                    case EventType.MouseDrag:
                        if (draggingSplitter)
                        {
                            splitLerp =
                                Mathf.Clamp(
                                    Mathf.Lerp(
                                        0,
                                        Screen.width,
                                        mousePosition.x / Screen.width)
                                    / Screen.width,
                                    leftPanelMinPercentageWidth,
                                    leftPanelMaxPercentageWidth);
                            Repaint();
                        }

                        break;
                    case EventType.MouseUp:
                        if (draggingSplitter) draggingSplitter = false;
                        break;
                }
            }
        }

        private void DrawUrl(GUIContent guiContent, string url)
        {
            GUILayout.Label(guiContent, _linkStyle);
            var lastRect = GUILayoutUtility.GetLastRect(); 
            if (Event.current.rawType == EventType.MouseDown && lastRect.Contains(Event.current.mousePosition))
                Application.OpenURL(url);
        }

        #endregion

        #region GUI Functions - Settings Mode

        /// <summary>
        /// Draws the preferences for the settings mode.
        /// </summary>
        private void DrawSettingsModePreferences()
        {
            DrawSectionStart(
                EditorLabels.LocalizationManager.EditorModes.Settings.Preferences.Title,
                EditorLabels.LocalizationManager.EditorModes.Settings.Preferences.Description);

            DrawSubSectionStart(
                EditorLabels.LocalizationManager.EditorModes.Settings.Preferences.General);
            
            _settings.useDelayedTextFields = EditorGUILayout.ToggleLeft(
                new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.Settings.Preferences.UseDelayedTextFields.Text,
                    EditorLabels.LocalizationManager.EditorModes.Settings.Preferences.UseDelayedTextFields.Tooltip),
                _settings.useDelayedTextFields);
            
            _settings.manualErrorChecks = EditorGUILayout.ToggleLeft(
                new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.Settings.Preferences.ManualErrorChecks.Text,
                    EditorLabels.LocalizationManager.EditorModes.Settings.Preferences.ManualErrorChecks.Tooltip),
                _settings.manualErrorChecks);

            _settings.hidePrefabWarning = EditorGUILayout.ToggleLeft(
                new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.Settings.Preferences.HidePrefabWarning.Text,
                    EditorLabels.LocalizationManager.EditorModes.Settings.Preferences.HidePrefabWarning.Tooltip),
                _settings.hidePrefabWarning);

            _settings.showAnnouncements = EditorGUILayout.ToggleLeft(
                new GUIContent(
                    EditorLabels.TongueTwisterWindow.AnnouncementsSetting.Text,
                    EditorLabels.TongueTwisterWindow.AnnouncementsSetting.Tooltip),
                _settings.showAnnouncements);
            
            DrawSectionEnd();

            DrawSubSectionStart(EditorLabels.LocalizationManager.EditorModes.Settings.Preferences.Advanced);
            
            _settings.debugMode = EditorGUILayout.ToggleLeft(
                new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.Settings.Preferences.DebugMode.Text,
                    EditorLabels.LocalizationManager.EditorModes.Settings.Preferences.DebugMode.Tooltip),
                _settings.debugMode);
            
            DrawSectionEnd();

            DrawSectionEnd();
            GUILayout.Space(15);
        }

        /// <summary>
        /// Draws an extra label warning for when no LM has been selected and the user is editing the localization
        /// manager.
        /// </summary>
        private void DrawSettingsModeNoLocalizationWarning()
        {
            GUILayout.Label(EditorLabels.LocalizationManager.EditorModes.Settings.NoLocalizationManagerWarning);
        }
        
        /// <summary>
        /// Draws fields for users to toggle features and adjust behaviours of the Tongue Twister system.
        /// </summary>
        private void DrawLocalizationSettingsSection()
        {
            DrawSectionStart(
                EditorLabels.LocalizationManager.EditorModes.Settings.LocalizationSettings.Title,
                EditorLabels.LocalizationManager.EditorModes.Settings.LocalizationSettings.Description);

            GUILayout.BeginHorizontal();
            GUILayout.Space(15);
            GUILayout.BeginVertical();
            
            #region Default Locale

            var configuredLocales = LocalizationManagerEditor.ConfiguredLocales;
            var currentlySelectedDefaultLocaleIndex = 0;
            var options = new Dictionary<int, Locale>();
            options.Add(-1, null);
            
            for (int i = 0; i < configuredLocales.Count; i++)
            {
                if (LocalizationManagerEditor.DefaultLocaleId == configuredLocales[i].Id)
                {
                    currentlySelectedDefaultLocaleIndex = i + 1;
                }
                
                options.Add(i, configuredLocales[i]);
            }

            var newSelectedDefaultLocaleIndex = EditorGUILayout.Popup(
                new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.Settings.LocalizationSettings.DefaultLocale.Text,
                    EditorLabels.LocalizationManager.EditorModes.Settings.LocalizationSettings.DefaultLocale.Tooltip),
                currentlySelectedDefaultLocaleIndex,
                options.Values.Select(locale => locale?.Metadata?.DisplayName ?? EditorLabels.TongueTwisterWindow.Common.None).ToArray()
            );

            if (newSelectedDefaultLocaleIndex != currentlySelectedDefaultLocaleIndex)
            {
                RecordLocalizationManagerEditorChange(
                    EditorLabels.LocalizationManager.EditorChanges.ChangedDefaultLocale);
                
                LocalizationManagerEditor.DefaultLocaleId = options[newSelectedDefaultLocaleIndex - 1]?.Id;
                
                SetDirty();
            }

            #endregion

            #region Load Additional Localizations From File
            
            DrawHorizontalBreak(15, 15, 0, 0);
            
            var loadAdditionalLocalizationsFromFile = EditorGUILayout.ToggleLeft(
                new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.Settings.LocalizationSettings.AllowLoadingTongueTwisterLocalizationsFromFile.Text,
                    EditorLabels.LocalizationManager.EditorModes.Settings.LocalizationSettings.AllowLoadingTongueTwisterLocalizationsFromFile.Tooltip),
                LocalizationManagerEditor.LoadAdditionalLocalizationFiles);
            
            if (loadAdditionalLocalizationsFromFile != LocalizationManagerEditor.LoadAdditionalLocalizationFiles)
            {
                RecordLocalizationManagerEditorChange("Toggled \"Allow importing external localization files\"");
                LocalizationManagerEditor.LoadAdditionalLocalizationFiles = loadAdditionalLocalizationsFromFile; 
                SetDirty();
            }
            
            GUI.enabled = loadAdditionalLocalizationsFromFile;

            GUILayout.Space(15);

            GUILayout.Label(
                new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.Settings.LocalizationSettings.FolderPathBuilderModule.Text,
                    EditorLabels.LocalizationManager.EditorModes.Settings.LocalizationSettings.FolderPathBuilderModule.Tooltip));
                
            var additionalLocalizationsFolderPathBuilder = (FolderPathBuilder) EditorGUILayout.ObjectField(
                LocalizationManagerEditor.AdditionaLocalizationsFolderPathBuilder,
                typeof(FolderPathBuilder), 
                false);

            if (additionalLocalizationsFolderPathBuilder !=
                LocalizationManagerEditor.AdditionaLocalizationsFolderPathBuilder)
            {
                LocalizationManagerEditor.AdditionaLocalizationsFolderPathBuilder =
                    additionalLocalizationsFolderPathBuilder 
                        ? additionalLocalizationsFolderPathBuilder 
                        : null;
            }
                
            if (additionalLocalizationsFolderPathBuilder != null)
            {
                GUILayout.Space(15);
                GUILayout.BeginVertical(EditorStyles.helpBox);
                additionalLocalizationsFolderPathBuilder.DrawEditorUi(RecordChange);
                GUILayout.EndVertical();
            }
            
            GUI.enabled = true;
            
            #endregion

            #region Save Last Used Locale to Player Prefs
            
            DrawHorizontalBreak(15, 15, 0, 0);
            
            var saveLastUsedLocaleToPlayerPrefs = EditorGUILayout.ToggleLeft(
                new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.Settings.LocalizationSettings.SaveLastUsedLanguageToPlayerPrefs.Text,
                    EditorLabels.LocalizationManager.EditorModes.Settings.LocalizationSettings.SaveLastUsedLanguageToPlayerPrefs.Tooltip),
                LocalizationManagerEditor.SaveLastUsedLocaleToPlayerPrefs);
            
            if (saveLastUsedLocaleToPlayerPrefs != 
                LocalizationManagerEditor.SaveLastUsedLocaleToPlayerPrefs)
            {
                RecordLocalizationManagerEditorChange( 
                    EditorLabels.LocalizationManager.EditorChanges.ToggledSaveLastUsedLanguageToPlayerPrefs);
                LocalizationManagerEditor.SaveLastUsedLocaleToPlayerPrefs =
                    saveLastUsedLocaleToPlayerPrefs;
                SetDirty();
            }
            
            GUILayout.Space(15);
            
            GUI.enabled = saveLastUsedLocaleToPlayerPrefs;
            
            var lastUsedLocalePrefName = 
                EditorGUILayout.TextField(
                    new GUIContent(
                        EditorLabels.LocalizationManager.EditorModes.Settings.LocalizationSettings.SaveLastUsedLanguageToPlayerPrefs.PlayerPrefKey.Text,
                        EditorLabels.LocalizationManager.EditorModes.Settings.LocalizationSettings.SaveLastUsedLanguageToPlayerPrefs.PlayerPrefKey.Tooltip),
                LocalizationManagerEditor.LastUsedLocalePrefName);
            
            if (lastUsedLocalePrefName != LocalizationManagerEditor.LastUsedLocalePrefName)
            {
                RecordLocalizationManagerEditorChange( 
                    EditorLabels.LocalizationManager.EditorChanges.ChangedLastUsedSystemLanguagePlayerPrefKey);
                LocalizationManagerEditor.LastUsedLocalePrefName = lastUsedLocalePrefName;
                SetDirty();
            }
            
            GUI.enabled = true;
            
            #endregion

            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            GUILayout.Space(15);
            
            DrawSectionEnd();
            GUILayout.Space(15);
        }

        /// <summary>
        /// Draws the validation rule section for easily enabling/disabling validation rules.
        /// </summary>
        private void DrawValidationRulesToggleSection()
        {
            DrawSectionStart(
                EditorLabels.TongueTwisterWindow.ValidationRulesSection.Title, 
                EditorLabels.TongueTwisterWindow.ValidationRulesSection.Description);
            
            GUILayout.BeginHorizontal();
            GUILayout.Space(15);
            GUILayout.BeginVertical();

            if (AllValidationRules == null || AllValidationRules.Count == 0)
            {
                GUILayout.BeginVertical(GUILayout.Height(50));
                DrawFullyFlexibleLabel(EditorLabels.TongueTwisterWindow.ValidationRulesSection.NoValidationRules);
                GUILayout.EndVertical();
            }
            else
            {
                GUILayout.Label(EditorLabels.TongueTwisterWindow.ValidationRulesSection.ResetWarning);
                GUILayout.Space(15);
                
                foreach (var rule in AllValidationRules)
                {
                    var enabled = _settings.validationRuleStatusCollection[rule.Code];

                    var newEnabled = EditorGUILayout.ToggleLeft(
                        new GUIContent(
                            $"{rule.Name} ({rule.Code})",
                            TongueTwisterIconUtility.GetIcon(rule.ViolationLevel),
                            rule.Description),
                        enabled);

                    if (newEnabled != enabled)
                    {
                        _settings.validationRuleStatusCollection[rule.Code] = newEnabled;
                    }

                    GUILayout.Label(rule.Description, EditorStyles.wordWrappedMiniLabel);
                    GUILayout.Space(20);
                }
            }
            
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            
            DrawSectionEnd();
        }

        #endregion

        #region GUI Functions - Locale Editor

        /// <summary>
        /// Draws the locale editor mode.
        /// </summary>
        private void DrawLocaleEditorModeArea()
        {
            DrawHorizontalSplitPanelGroup(
                DrawLocaleEditorTree,
                DrawLocaleEditorEditor,
                EditorStyles.helpBox,
                EditorStyles.helpBox,
                ref _draggingLocaleEditorSplitter,
                ref _localeEditorHorizontalSplitterLerp,
                ref _localeEditorPanelRect);
        }

        private void DrawLocaleEditorTree()
        {
            DrawLocaleEditorFilter();
            DrawLocaleEditorTreeViewButtons();

            _localeEditorTreePanelScrollPos = GUILayout.BeginScrollView(_localeEditorTreePanelScrollPos, GUIStyle.none, GUI.skin.verticalScrollbar);
            _localeEditorTreeView.OnGUI(new Rect(0, 0, _localeEditorPanelRect.width, _localeEditorPanelRect.height));
            GUILayout.EndScrollView();
        }

        /// <summary>
        /// Draws editable fields for the currently selected locale of the <see cref="_localeEditorTreeView"/>.
        /// </summary>
        private void DrawLocaleEditorEditor()
        {
            if (!_localeEditorTreeView.HasSelection())
            {
                DrawFullyFlexibleLabel(EditorLabels.LocalizationManager.EditorModes.LocaleEditing.NothingSelected);
                return;
            }

            var selectedItems = _localeEditorTreeView.GetSelection();

            if (selectedItems.Count > 1)
            {
                DrawFullyFlexibleLabel(EditorLabels.LocalizationManager.EditorModes.LocaleEditing.MultiObjectNotSupported);
                return;
            }

            var configuredLocales = LocalizationManagerEditor.ConfiguredLocales;
            var configuredLocalesCount = LocalizationManagerEditor.ConfiguredLocales?.Count ?? 0;
            var firstIndex = selectedItems.First();

            if (firstIndex >= configuredLocalesCount)
            {
                _localeEditorTreeView.Deselect();
                return;
            }

            var locale = configuredLocales[firstIndex];
            var changeWasMade = false;

            _localeEditorEditorScrollPos = EditorGUILayout.BeginScrollView(
                _localeEditorEditorScrollPos,
                GUIStyle.none,
                GUI.skin.verticalScrollbar);
            
            if (_settings.debugMode)
            {
                DrawLocaleDebug(locale);
            }
            
            DrawSubSectionStart(EditorLabels.LocalizationManager.EditorModes.ModelView.Common.SelectionInformationSection);

            // GUID
            GUILayout.BeginHorizontal();
            GUI.enabled = false;
            EditorGUILayout.TextField(
                new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.Id.Text,
                    EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.Id.Tooltip),
                locale.Id);
            GUI.enabled = true;
            if (GUILayout.Button(
                EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.Id.Copy, 
                GUILayout.ExpandWidth(false)))
            {
                EditorGUIUtility.systemCopyBuffer = locale.Id;
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(15);
            
            // default
            var defaultLocaleId = LocalizationManagerEditor.DefaultLocaleId;
            var defaultLocale = EditorGUILayout.ToggleLeft(
                new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.Default.Text,
                    EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.Default.Tooltip),
                defaultLocaleId == locale.Id);
            if (defaultLocale && defaultLocaleId != locale.Id)
            {
                RecordLocalizationManagerEditorChange(EditorLabels.LocalizationManager.EditorChanges.ChangedDefaultLocale);
                LocalizationManagerEditor.DefaultLocaleId = locale.Id;
                changeWasMade = true;
            }

            GUILayout.Space(15);
            
            // name
            if (_settings.useDelayedTextFields)
            {
                var newName = EditorGUILayout.DelayedTextField(
                    new GUIContent(
                        EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.Name.Text,
                        EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.Name.Tooltip),
                    locale.Metadata.DisplayName);
                if (newName != locale.Metadata.DisplayName)
                {
                    RecordLocalizationManagerEditorChange(
                        string.Format(
                            EditorLabels.LocalizationManager.EditorChanges.ChangedDisplayKeyName,
                            locale.Metadata.DisplayName,
                            newName));
                    locale.Metadata.DisplayName = newName;
                    changeWasMade = true;
                }
            }
            else
            {
                var newName = EditorGUILayout.TextField(
                    new GUIContent(
                        EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.Name.Text,
                        EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.Name.Tooltip),
                    locale.Metadata.DisplayName);
                if (newName != locale.Metadata.DisplayName)
                {
                    RecordLocalizationManagerEditorChange(
                        string.Format(
                            EditorLabels.LocalizationManager.EditorChanges.ChangedDisplayKeyName,
                            locale.Metadata.DisplayName,
                            newName));
                    locale.Metadata.DisplayName = newName;
                    changeWasMade = true;
                }
            }
            
            // localized name
            var newLocalizedName = EditorGUILayout.TextField(
                new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.NativeName.Text,
                    EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.NativeName.Tooltip),
                locale.Metadata.NativeName);
            if (newLocalizedName != locale.Metadata.NativeName)
            {
                RecordLocalizationManagerEditorChange(EditorLabels.LocalizationManager.EditorChanges.ChangedLocalesNativeName);
                locale.Metadata.NativeName = newLocalizedName;
                changeWasMade = true;
            }

            // custom code
            var newLocalizationCode = EditorGUILayout.TextField(
                new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.CustomCode.Text,
                    EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.CustomCode.Tooltip),
                locale.Metadata.CustomCode);
            if (newLocalizationCode != locale.Metadata.CustomCode)
            {
                RecordLocalizationManagerEditorChange(EditorLabels.LocalizationManager.EditorChanges.ChangedLocalesCustomCode);
                locale.Metadata.CustomCode = newLocalizationCode;
                changeWasMade = true;
            }
            
            // system language
            var newSystemLanguage = (SystemLanguage) EditorGUILayout.EnumPopup(
                new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.BackingLanguage.Text,
                    EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.BackingLanguage.Tooltip),
                locale.Metadata.UnitySystemLanguage);
            if (newSystemLanguage != locale.Metadata.UnitySystemLanguage)
            {
                RecordLocalizationManagerEditorChange(EditorLabels.LocalizationManager.EditorChanges.ChangedLocaleBackingLanguage);
                locale.Metadata.UnitySystemLanguage = newSystemLanguage;
                changeWasMade = true;
            }
            
            // language code
            var newLanguageCode = (ISO639Alpha2) EditorGUILayout.EnumPopup(
                new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.LanguageCode.Text,
                    EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.LanguageCode.Tooltip),
                locale.Metadata.ISOLanguageCode);
            if (newLanguageCode != locale.Metadata.ISOLanguageCode)
            {
                RecordLocalizationManagerEditorChange(EditorLabels.LocalizationManager.EditorChanges.ChangedLocalesLanguageCode);
                locale.Metadata.ISOLanguageCode = newLanguageCode;
                changeWasMade = true;
            }
            
            // country code
            var newCountryCode = (ISO3166Alpha2) EditorGUILayout.EnumPopup(
                new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.CountryCode.Text,
                    EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.CountryCode.Tooltip),
                locale.Metadata.ISOCountryCode);
            if (newCountryCode != locale.Metadata.ISOCountryCode)
            {
                RecordLocalizationManagerEditorChange(EditorLabels.LocalizationManager.EditorChanges.ChangedLocalesCountryCode);
                locale.Metadata.ISOCountryCode = newCountryCode;
                changeWasMade = true;
            }

            GUILayout.Space(15);
            
            // change icon
            GUILayout.BeginHorizontal();
            var newIcon = (Texture) EditorGUILayout.ObjectField(
                new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.LocaleEditing.IconColumn.Title,
                    EditorLabels.LocalizationManager.EditorModes.LocaleEditing.IconColumn.Tooltip),
                locale.Icon,
                typeof(Texture),
                false,
                GUILayout.ExpandWidth(false));
            if (newIcon != locale.Icon)
            {
                RecordLocalizationManagerEditorChange(EditorLabels.LocalizationManager.EditorChanges.ChangedLocaleIcon);
                locale.Icon = newIcon;
                changeWasMade = true;
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            
            DrawSectionEnd();

            GUILayout.Space(15);
            
            DrawSubSectionStart(EditorLabels.LocalizationManager.EditorModes.LocaleEditing.UtilitiesSection);

            // set ISO code from system language
            if (GUILayout.Button(
                new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Buttons.SetLanguageCodeFromSystemLanguage.Text,
                    EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Buttons.SetLanguageCodeFromSystemLanguage.Tooltip
                )))
            {
                var foundLanguageCode = locale.Metadata.UnitySystemLanguage.ToISO639Alpha2();
                if (foundLanguageCode != locale.Metadata.ISOLanguageCode)
                {
                    RecordLocalizationManagerEditorChange(EditorLabels.LocalizationManager.EditorChanges.ChangedLocalesLanguageCode);
                    locale.Metadata.ISOLanguageCode = foundLanguageCode;
                    changeWasMade = true;
                }
            }
            
            // set system language from ISO code
            if (GUILayout.Button(
                new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Buttons.SetSystemLanguageFromLanguageCode.Text,
                    EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Buttons.SetSystemLanguageFromLanguageCode.Tooltip
                )))
            {
                var foundSystemLanguage = locale.Metadata.ISOLanguageCode.ToSystemLanguage();
                if (foundSystemLanguage != locale.Metadata.UnitySystemLanguage)
                {
                    RecordLocalizationManagerEditorChange(EditorLabels.LocalizationManager.EditorChanges.ChangedLocaleBackingLanguage);
                    locale.Metadata.UnitySystemLanguage = foundSystemLanguage;
                    changeWasMade = true;
                }
            }
            
            // open ISO window
            if (GUILayout.Button(EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Buttons.OpenISOWindow.Text))
            {
                LocalizationCodeWindow.Open();
            }

            GUILayout.Space(15);
            
            // notes
            GUILayout.Label(new GUIContent(
                EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.Notes.Text,
                EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.Notes.Tooltip));
            var notes = EditorGUILayout.TextArea(locale.Metadata.Notes, GUILayout.Height(100));
            if (notes != locale.Metadata.Notes)
            {
                RecordLocalizationManagerEditorChange(EditorLabels.LocalizationManager.EditorChanges.ChangedLocaleNotes);
                locale.Metadata.Notes = notes;
                changeWasMade = true;
            }
            
            DrawSectionEnd();

            EditorGUILayout.EndScrollView();

            if (changeWasMade)
            {
                UpdateLocaleEditorTree();
                CommitLocaleEditorTreeModel();
                SetDirty();
            }
        }

        /// <summary>
        /// Draws the filter for the locale editor's tree view.
        /// </summary>
        private void DrawLocaleEditorFilter()
        {
            GUILayout.BeginHorizontal(_toolbar);

            // filter
            
            GUILayout.Label(
                new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Filter.Text,
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Filter.Tooltip),
                GUILayout.ExpandWidth(false));
            
            _localeEditorTreeFilter = 
                EditorGUILayout.TextField(
                    _localeEditorTreeFilter, 
                    _toolbarSearchField);

            // clear button
            
            if (GUILayout.Button(
                new GUIContent(
                    "",
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Filter.ClearTooltip),
                _toolbarClearButton,
                GUILayout.ExpandWidth(false)))
            {
                _localeEditorTreeFilter = "";
                GUI.FocusControl(null);
            }
            
            // apply search string
            
            _localeEditorTreeView.searchString = _localeEditorTreeFilter;

            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// Draws buttons for the locale editor's tree view section.
        /// </summary>
        private void DrawLocaleEditorTreeViewButtons()
        {
            var buttonActionPerformed = false;
            
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(
                new GUIContent(
                    TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.AddLocale),
                    EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Buttons.Add.Tooltip),
                GUILayout.Height(TREE_VIEW_EDITOR_TOOLBAR_BUTTON_SIZE),
                GUILayout.Width(TREE_VIEW_EDITOR_TOOLBAR_BUTTON_SIZE)))
            {
                AddLocaleToConfiguredLocales();
                buttonActionPerformed = true;
            }
            GUI.enabled = _localeEditorTreeView.HasSelection();
            if (GUILayout.Button(
                new GUIContent(
                    TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.Duplicate),
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Groups.Buttons.Duplicate.Tooltip),
                GUILayout.Height(TREE_VIEW_EDITOR_TOOLBAR_BUTTON_SIZE),
                GUILayout.Width(TREE_VIEW_EDITOR_TOOLBAR_BUTTON_SIZE)))
            {
                var selectedLocale =
                    LocalizationManager.ConfiguredLocales[_localeEditorTreeView.GetSelection().First()];

                var duplicatedLocale =
                    new Locale(selectedLocale, true);

                AddLocaleToConfiguredLocales(duplicatedLocale);
                buttonActionPerformed = true;
            }
            if (GUILayout.Button(
                new GUIContent(
                    TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.Trash),
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Groups.Buttons.DeleteSelection.Tooltip),
                GUILayout.Height(TREE_VIEW_EDITOR_TOOLBAR_BUTTON_SIZE),
                GUILayout.Width(TREE_VIEW_EDITOR_TOOLBAR_BUTTON_SIZE)))
            {
                RemoveSelectedLocaleTreeViewItems();
                buttonActionPerformed = true;
                _localeEditorTreeView.Deselect();
            }
            GUI.enabled = true;
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            if (buttonActionPerformed)
            {
                UpdateLocaleEditorTree();
                CommitLocaleEditorTreeModel();
            }
        }
        
        /// <summary>
        /// Draws the given <see cref="Locale"/>'s debug view. As of version 1.3, there are no longer any locale
        /// debug values to display. This may change in the future so this function will remain here as a reminder.
        /// </summary>
        /// <param name="locale">The locale to debug.</param>
        private void DrawLocaleDebug(Locale locale)
        {
            // DrawSubSectionStart(EditorLabels.LocalizationManager.EditorModes.Settings.Preferences.DebugMode.LocaleEditor.LocaleEditorSectionTitle);
            //
            // var previousGuiState = GUI.enabled;
            // GUI.enabled = false;
            //
            // ... locale debug goes here ... 
            //
            // GUI.enabled = previousGuiState;
            //
            // DrawSectionEnd();
        }
        
        #endregion
        
        #region GUI Functions - Editor Mode

        /// <summary>
        /// Draws the editor mode area which consists of the navigation panel and editing panel.
        /// </summary>
        private void DrawEditorModeArea()
        {
            DrawHorizontalSplitPanelGroup(
                DrawEditorModelTree,
                DrawEditorModeModelViewPanel,
                EditorStyles.helpBox,
                EditorStyles.helpBox,
                ref _draggingModelEditorSplitter,
                ref _modelEditorHorizontalSplitterLerp,
                ref _editorPanelRect);
        }

        /// <summary>
        /// Draws the editor mode's navigation panel.
        /// </summary>
        private void DrawEditorModelTree()
        {
            if (_modelEditorTreeView?.tree?.Root != null)
            {
                DrawEditorModeFilter();
                DrawEditorModeModelViewNavPanelButtons();
                
                // draw scroll view
                
                _editorTreePanelScrollPos = GUILayout.BeginScrollView(_editorTreePanelScrollPos, GUIStyle.none, GUI.skin.verticalScrollbar);
                _modelEditorTreeView.OnGUI(new Rect(0, 0, _editorPanelRect.width, _editorPanelRect.height));
                GUILayout.EndScrollView();
            }
        }

        /// <summary>
        /// Draws a filter text box for the editing mode nav panel.
        /// </summary>
        private void DrawEditorModeFilter()
        {
            GUILayout.BeginHorizontal(_toolbar);

            // filter
            
            GUILayout.Label(
                new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Filter.Text,
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Filter.Tooltip),
                GUILayout.ExpandWidth(false));
            
            _editorTreeFilter = 
                EditorGUILayout.TextField(
                    _editorTreeFilter, 
                    _toolbarSearchField);

            // clear button
            
            if (GUILayout.Button(
                new GUIContent(
                    "",
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Filter.ClearTooltip),
                _toolbarClearButton,
                GUILayout.ExpandWidth(false)))
            {
                _editorTreeFilter = "";
                GUI.FocusControl(null);
            }
            
            // apply search string
            
            _modelEditorTreeView.searchString = _editorTreeFilter;

            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// Draws all nav panel buttons.
        /// </summary>
        private void DrawEditorModeModelViewNavPanelButtons()
        {
            // GUI is enabled/disabled/shown depending on what's selected

            var selection = _modelEditorTreeView.GetSelection();
            var groupsSelected = false;
            var displayKeysSelected = false;
            var localizationsSelected = false;

            for (int i = 0; i < selection.Count; i++)
            {
                var item = _modelEditorTreeView.tree.Find(selection[i]);
                if (item.Type == TongueTwisterModel.ModelType.Group) groupsSelected = true; 
                if (item.Type == TongueTwisterModel.ModelType.DisplayKey) displayKeysSelected = true; 
                if (item.Type == TongueTwisterModel.ModelType.Localization) localizationsSelected = true; 
            }

            GUI.enabled = !displayKeysSelected && !localizationsSelected;
            
            GUILayout.BeginHorizontal();
            
            if (GUILayout.Button(
                new GUIContent(
                    TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.AddGroup), 
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Groups.Buttons.AddNewGroup.Tooltip),
                GUILayout.Height(TREE_VIEW_EDITOR_TOOLBAR_BUTTON_SIZE),
                GUILayout.Width(TREE_VIEW_EDITOR_TOOLBAR_BUTTON_SIZE)))
            {
                AddNewModelToSelectionOrRoot(TongueTwisterModel.ModelType.Group);
            }
            
            if (GUILayout.Button(
                new GUIContent(
                    TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.AddDisplayKey),
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Groups.Buttons.AddDisplayKey.Tooltip),
                GUILayout.Height(TREE_VIEW_EDITOR_TOOLBAR_BUTTON_SIZE),
                GUILayout.Width(TREE_VIEW_EDITOR_TOOLBAR_BUTTON_SIZE)))
            {
                AddNewModelToSelectionOrRoot(TongueTwisterModel.ModelType.DisplayKey);
            }

            GUI.enabled = (!groupsSelected && displayKeysSelected && !localizationsSelected);
            
            if (GUILayout.Button(
                new GUIContent(
                    TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.AddLocalization), 
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Localizations.Buttons.AddLocalization.Tooltip),
                GUILayout.Height(TREE_VIEW_EDITOR_TOOLBAR_BUTTON_SIZE),
                GUILayout.Width(TREE_VIEW_EDITOR_TOOLBAR_BUTTON_SIZE)))
            {
                AddNewModelToSelectionOrRoot(TongueTwisterModel.ModelType.Localization);
            }
            
            GUI.enabled = selection.Count > 0;
            
            if (GUILayout.Button(
                new GUIContent(
                    TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.Duplicate), 
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Groups.Buttons.Duplicate.Tooltip),
                GUILayout.Height(TREE_VIEW_EDITOR_TOOLBAR_BUTTON_SIZE),
                GUILayout.Width(TREE_VIEW_EDITOR_TOOLBAR_BUTTON_SIZE)))
            {
                DuplicateSelectedTreeViewItems();
            }

            GUI.enabled = selection.Count > 0;
            
            if (GUILayout.Button(
                new GUIContent(
                    TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.DeselectAll), 
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Common.DeselectAllTooltip),
                GUILayout.Height(TREE_VIEW_EDITOR_TOOLBAR_BUTTON_SIZE),
                GUILayout.Width(TREE_VIEW_EDITOR_TOOLBAR_BUTTON_SIZE)))
            {
                _modelEditorTreeView.Deselect();
            }
            
            GUI.enabled = true;
            
            if (GUILayout.Button(
                new GUIContent(
                    TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.ReRunErrorCheck),
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Common.ReRunErrorCheckTooltip),
                GUILayout.Height(TREE_VIEW_EDITOR_TOOLBAR_BUTTON_SIZE),
                GUILayout.Width(TREE_VIEW_EDITOR_TOOLBAR_BUTTON_SIZE)))
            {
                RunModelEditorTreeModelErrorChecks();
                Debug.Log(EditorLabels.LocalizationManager.EditorModes.ModelView.Logging.RanErrorWarningCheck);
            }
            
            GUI.enabled = selection.Count > 0;

            if (GUILayout.Button(
                new GUIContent(
                    TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.Trash), 
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Groups.Buttons.DeleteSelection.Tooltip),
                GUILayout.Height(TREE_VIEW_EDITOR_TOOLBAR_BUTTON_SIZE),
                GUILayout.Width(TREE_VIEW_EDITOR_TOOLBAR_BUTTON_SIZE)))
            {
                RemoveSelectedModelTreeViewItems();
            }
            
            GUILayout.FlexibleSpace();
            
            GUILayout.EndHorizontal();

            GUI.enabled = true;
        }

        /// <summary>
        /// Draws the editor mode's edit panel for changing group, display key, and localization values.
        /// </summary>
        private void DrawEditorModeModelViewPanel()
        {
            var selectionList = _modelEditorTreeView.GetSelection();
            
            if (!_modelEditorTreeView.HasVisibleSelection() || selectionList.Count == 0)
            {
                DrawInlineEditorNavigationButtons(null);
                DrawFullyFlexibleLabel(EditorLabels.LocalizationManager.EditorModes.ModelView.Common.NothingSelected);
            }
            else if (selectionList.Count > 1)
            {
                DrawInlineEditorNavigationButtons(null);
                DrawFullyFlexibleLabel(EditorLabels.LocalizationManager.EditorModes.ModelView.Common.MultipleObjectEditingWarning);
            }
            else
            {
                var selectedModel = _modelEditorTreeView.tree.Find(selectionList[0]);

                DrawInlineEditorNavigationButtons(selectedModel);

                if (selectedModel.HasErrors || selectedModel.HasWarnings)
                {
                    DrawEditorModelViewErrorsAndWarnings(selectedModel);
                }

                _editorModelViewScrollPos = EditorGUILayout.BeginScrollView(
                    _editorModelViewScrollPos, 
                    GUIStyle.none,
                    GUI.skin.verticalScrollbar);

                if (_settings.debugMode)
                {
                    DrawEditorDebugForModel(selectedModel);
                }
                
                switch (selectedModel.Type)
                {
                    case TongueTwisterModel.ModelType.Group:
                        DrawGroupEditor(ref selectedModel);
                        break;
                    case TongueTwisterModel.ModelType.DisplayKey:
                        DrawDisplayKeyEditor(ref selectedModel);
                        break;
                    case TongueTwisterModel.ModelType.Localization:
                        DrawLocalizationEditor(ref selectedModel, 
                            false, 
                            true);
                        break;
                }
                
                EditorGUILayout.EndScrollView();
            }
        }

        /// <summary>
        /// Draws warning and error blocks for a tree element.
        /// </summary>
        /// <param name="model">The model to draw</param>
        private void DrawEditorModelViewErrorsAndWarnings(TongueTwisterModel model)
        {
            foreach (var modelError in model.ModelRuleViolations)
            {
                switch (modelError.RuleViolationSeverity)
                {
                    case RuleViolationSeverityType.Error:
                        DrawError(modelError.Message);
                        break;
                    case RuleViolationSeverityType.Warning:
                        DrawWarning(modelError.Message);
                        break;
                    default: // unsupported type
                        DrawInfo(modelError.Message);
                        break;
                }
            }
        }

        /// <summary>
        /// Draws an info block with the given text.
        /// </summary>
        /// <param name="info"></param>
        private void DrawInfo(string info, Action drawCustomActionContent = null)
        {
            var originalGuiColor = GUI.color;
            GUI.color = COLOR_OBJ_INFO;
            GUILayout.BeginHorizontal(EditorStyles.helpBox, GUILayout.ExpandWidth(true));
            GUI.color = originalGuiColor;
            GUILayout.Label(new GUIContent(info, TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.Info)), EditorStyles.wordWrappedLabel, GUILayout.ExpandWidth(true));
            drawCustomActionContent?.Invoke();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// Draws a warning block with the given text.
        /// </summary>
        /// <param name="warning"></param>
        /// <param name="drawCustomActionContent"></param>
        private void DrawWarning(string warning, Action drawCustomActionContent = null)
        {
            var originalGuiColor = GUI.color;
            GUI.color = COLOR_OBJ_WARNING;
            GUILayout.BeginHorizontal(EditorStyles.helpBox, GUILayout.ExpandWidth(true));
            GUI.color = originalGuiColor;
            GUILayout.Label(new GUIContent(warning, TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.Warning)), EditorStyles.wordWrappedLabel, GUILayout.ExpandWidth(true));
            drawCustomActionContent?.Invoke();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// Draws an error block with the given text.
        /// </summary>
        /// <param name="error">The error text.</param>
        private void DrawError(string error)
        {
            var originalGuiColor = GUI.color;
            GUI.color = COLOR_OBJ_ERROR;
            GUILayout.BeginHorizontal(EditorStyles.helpBox, GUILayout.ExpandWidth(true));
            GUI.color = originalGuiColor;
            GUILayout.Label(new GUIContent(error, TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.Error)), EditorStyles.wordWrappedLabel, GUILayout.ExpandWidth(true));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// Draws the Editor mode's group editor view.
        /// </summary>
        /// <param name="group">Group to edit.</param>
        /// <param name="showSectionTitle">Whether or not to show the section title.</param>
        /// <param name="showChildGroups">Whether or not to show the child groups section.</param>
        /// <param name="showDisplayKeys">Whether or not to show the display keys section.</param>
        /// <param name="showNotes">Whether or not to show the notes section.</param>
        /// <param name="showToolbarButtons">Whether or not to show the toolbar buttons.</param>
        private void DrawGroupEditor(
            ref TongueTwisterModel group, 
            bool showSectionTitle = true, 
            bool showChildGroups = true, 
            bool showDisplayKeys = true, 
            bool showNotes = true, 
            bool showToolbarButtons = false)
        {
            if (showSectionTitle)
            {
                DrawSubSectionStart(EditorLabels.LocalizationManager.EditorModes.ModelView.Common
                    .SelectionInformationSection);
            }

            #region Name
            
            GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
            GUILayout.BeginVertical(GUILayout.ExpandWidth(true));
            if (_settings.useDelayedTextFields)
            {
                var newName = EditorGUILayout.DelayedTextField(
                    new GUIContent(
                        EditorLabels.LocalizationManager.EditorModes.ModelView.Groups.FormFields.GroupName.Text,
                        TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.Group),
                        EditorLabels.LocalizationManager.EditorModes.ModelView.Groups.FormFields.GroupName
                            .Tooltip),
                    group.Name);

                if (newName != group.Name)
                {
                    RecordLocalizationManagerEditorChange(
                        string.Format(
                            EditorLabels.LocalizationManager.EditorChanges.ChangedGroupName,
                            group.Name,
                            newName));

                    group.Name = newName;
                    _modelEditorTreeView.MarkTreeModelAsChanged();
                }
            }
            else
            {
                var newName = EditorGUILayout.TextField(
                    new GUIContent(
                        EditorLabels.LocalizationManager.EditorModes.ModelView.Groups.FormFields.GroupName.Text,
                        TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.Group),
                        EditorLabels.LocalizationManager.EditorModes.ModelView.Groups.FormFields.GroupName
                            .Tooltip),
                    group.Name);

                if (newName != group.Name)
                {
                    RecordLocalizationManagerEditorChange(
                        string.Format(
                            EditorLabels.LocalizationManager.EditorChanges.ChangedGroupName,
                            group.Name,
                            newName));

                    group.Name = newName;
                    _modelEditorTreeView.MarkTreeModelAsChanged();
                }
            }

            GUI.enabled = false;
            EditorGUILayout.TextField(
                new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Common.FormattedNamePreview,
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Common.FormattedNamePreviewTooltip),
                group.FormattedName);
            GUI.enabled = true;
            
            GUILayout.EndVertical();
            
            if (showToolbarButtons)
            {
                GUILayout.Space(15);
                GUILayout.BeginVertical(GUILayout.ExpandWidth(false), GUILayout.Width(EDITING_MODE_INLINE_BUTTON_SIZE));
                if (GUILayout.Button(
                    new GUIContent(
                        TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.Forwards),
                        EditorLabels.TongueTwisterWindow.Common.Buttons.GoTo.Tooltip),
                    GUILayout.Width(EDITING_MODE_INLINE_BUTTON_SIZE),
                    GUILayout.Height(EDITING_MODE_INLINE_BUTTON_SIZE)))
                {
                    _modelEditorTreeView.SetSelection(group.Id);
                }
        
                if (GUILayout.Button(
                    new GUIContent(
                        TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.Trash), 
                        EditorLabels.TongueTwisterWindow.Common.Buttons.DeleteIconTooltip),
                    GUILayout.Width(EDITING_MODE_INLINE_BUTTON_SIZE),
                    GUILayout.Height(EDITING_MODE_INLINE_BUTTON_SIZE)))
                {
                    RemoveTreeViewElement(group.Id, TongueTwisterModel.ModelType.DisplayKey);
                }
                GUILayout.EndVertical();
            }
            
            GUILayout.EndHorizontal();

            GUILayout.Space(15.0f); 
            
            #endregion
            
            #region Notes

            if (showNotes)
            {
                GUILayout.Label(new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Groups.FormFields.Notes.Text,
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Groups.FormFields.Notes.Tooltip));

                var notes = EditorGUILayout.TextArea(
                    group.Notes,
                    EditorStyles.textArea,
                    GUILayout.MinHeight(50),
                    GUILayout.ExpandHeight(true));

                if (notes != group.Notes)
                {
                    RecordLocalizationManagerEditorChange(
                        EditorLabels.LocalizationManager.EditorChanges.ChangedGroupNotes);

                    group.Notes = notes;
                }
            }

            #endregion

            if (showSectionTitle)
            {
                DrawSectionEnd();
            }

            #region Child Groups

            if (showChildGroups)
            {
                DrawSubSectionStart(EditorLabels.LocalizationManager.EditorModes.ModelView.Groups.Sections.ChildGroups.Text);

                GUILayout.BeginHorizontal();
                group.FilterChildGroups =
                    EditorGUILayout.TextField(
                        group.FilterChildGroups, 
                        _toolbarSearchField);
                if (GUILayout.Button("", _toolbarClearButton))
                {
                    group.FilterChildGroups = "";
                    GUI.FocusControl(null);
                }
                GUILayout.EndHorizontal();
                
                var showHideLabel = group.ChildGroupsExpanded
                    ? EditorLabels.TongueTwisterWindow.Common.Hide
                    : EditorLabels.TongueTwisterWindow.Common.Show;
                
                GUILayout.BeginHorizontal();
                if (GUILayout.Button(
                    new GUIContent(
                        EditorLabels.LocalizationManager.EditorModes.ModelView.Groups.Buttons.AddChildGroup.Text,
                        EditorLabels.LocalizationManager.EditorModes.ModelView.Groups.Buttons.AddChildGroup.Tooltip)))
                {
                    AddNewModelToSelectionOrRoot(TongueTwisterModel.ModelType.Group);
                }
                if (GUILayout.Button(showHideLabel, GUILayout.ExpandWidth(false)))
                {
                    group.ChildGroupsExpanded = !group.ChildGroupsExpanded;
                }
                GUILayout.EndHorizontal();

                if (group.HasGroupChildren)
                {
                    if (group.ChildGroupsExpanded)
                    {
                        var childGroups = group.GetChildrenOfType(TongueTwisterModel.ModelType.Group);
                        for (int i = 0; i < childGroups.Length; i++)
                        {
                            GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));

                            DrawFriendlyIndexNum(i);
                            
                            GUILayout.BeginVertical();
                            
                            var child = childGroups[i];
                            
                            if (!string.IsNullOrWhiteSpace(group.FilterChildGroups) &&
                                !FuzzyMatchStrings(group.FilterChildGroups, child.Name))
                            {
                                continue;
                            }
                            
                            DrawHorizontalBreak(5.0f, 5.0f, 0.0f, 0.0f);
                            DrawGroupEditor(ref child, false, false, false, false, true);
                            GUILayout.EndVertical();
                            
                            GUILayout.EndHorizontal();
                        }
                    }
                    else
                    {
                        GUILayout.Space(15);
                        DrawHorizontallyFlexibleLabel(EditorLabels.LocalizationManager.EditorModes.ModelView.Groups.ChildGroupsHidden);
                    }
                }
                else
                {
                    GUILayout.Space(15);
                    DrawHorizontallyFlexibleLabel(EditorLabels.LocalizationManager.EditorModes.ModelView.Groups.NoChildGroups);
                }
                
                GUILayout.Space(15.0f);
                
                DrawSectionEnd();
            }

            #endregion

            #region Display Keys

            if (showDisplayKeys)
            {
                DrawSubSectionStart(EditorLabels.LocalizationManager.EditorModes.ModelView.Groups.Sections.DisplayKeys.Text);

                GUILayout.BeginHorizontal();
                group.FilterDisplayKeys =
                    EditorGUILayout.TextField(
                        group.FilterDisplayKeys, 
                        _toolbarSearchField);
                if (GUILayout.Button("", _toolbarClearButton))
                {
                    group.FilterDisplayKeys = "";
                    GUI.FocusControl(null);
                }
                GUILayout.EndHorizontal();
                
                var showHideLabel = group.DisplayKeysExpanded
                    ? EditorLabels.TongueTwisterWindow.Common.Hide
                    : EditorLabels.TongueTwisterWindow.Common.Show;
                
                GUILayout.BeginHorizontal();
                if (GUILayout.Button(
                    new GUIContent(
                        EditorLabels.LocalizationManager.EditorModes.ModelView.Groups.Buttons.AddDisplayKey.Text,
                        EditorLabels.LocalizationManager.EditorModes.ModelView.Groups.Buttons.AddDisplayKey.Tooltip)))
                {
                    AddNewModelToSelectionOrRoot(TongueTwisterModel.ModelType.DisplayKey);
                }
                if (GUILayout.Button(showHideLabel, GUILayout.ExpandWidth(false)))
                {
                    group.DisplayKeysExpanded = !group.DisplayKeysExpanded;
                }
                GUILayout.EndHorizontal();

                if (group.HasDisplayKeyChildren)
                {
                    if (group.DisplayKeysExpanded)
                    {
                        var displayKeys = group.GetChildrenOfType(TongueTwisterModel.ModelType.DisplayKey);
                        for (int i = 0; i < displayKeys.Length; i++)
                        {
                            GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));

                            DrawFriendlyIndexNum(i);
                            
                            GUILayout.BeginVertical();
                            
                            var displayKey = displayKeys[i];
                            
                            if (!string.IsNullOrWhiteSpace(group.FilterDisplayKeys) &&
                                !FuzzyMatchStrings(group.FilterDisplayKeys, displayKey.Name))
                            {
                                continue;
                            }
                            
                            DrawHorizontalBreak(5.0f, 5.0f, 0.0f, 0.0f);
                            DrawDisplayKeyEditor(ref displayKey, false, false, false, true);
                            
                            GUILayout.EndVertical();
                            GUILayout.EndHorizontal();
                        }
                    }
                    else
                    {
                        GUILayout.Space(15);
                        DrawHorizontallyFlexibleLabel(EditorLabels.LocalizationManager.EditorModes.ModelView.Groups.DisplayKeysHidden);
                    }
                }
                else
                {
                    GUILayout.Space(15);
                    DrawHorizontallyFlexibleLabel(EditorLabels.LocalizationManager.EditorModes.ModelView.Groups.NoDisplayKeys);
                }
                
                GUILayout.Space(15.0f);
                
                DrawSectionEnd();
            }

            #endregion
            
            GUILayout.FlexibleSpace();
        }

        /// <summary>
        /// Given a model, draws the back/forward/go-up buttons for navigation between selections.
        /// </summary>
        /// <param name="model">The model used to get the paraent of if the "Go Up" button is clicked.</param>
        private void DrawInlineEditorNavigationButtons(TongueTwisterModel model)
        {
            GUILayout.BeginHorizontal();
            
            GUI.enabled = _modelEditorSelectionHistoryIndex > 0;
            if (GUILayout.Button(
                new GUIContent(
                    "", 
                    TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.Backwards), 
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Buttons.Back.GoBackTooltip),
                GUILayout.Height(DISPLAY_KEY_EDITOR_INLINE_NAVIGATOR_BUTTONS_SIZE),
                GUILayout.Width(DISPLAY_KEY_EDITOR_INLINE_NAVIGATOR_BUTTONS_SIZE)))
            {
                GoBackModelEditorSelection();
            }
            
            GUI.enabled = model != null && model.Parent != null && model.Parent.Id != -1; 
            if (GUILayout.Button(
                new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Common.GoToParent, 
                    TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.GoUp)),
                GUILayout.Height(DISPLAY_KEY_EDITOR_INLINE_NAVIGATOR_BUTTONS_SIZE)))
            {
                _modelEditorTreeView.SetSelection(model?.Parent?.Id ?? -1);
            }
            
            GUI.enabled = _modelEditorSelectionHistoryIndex <= _modelEditorSelectionHistory.Count - 2;
            if (GUILayout.Button(
                new GUIContent(
                    "",
                    TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.Forwards),
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Buttons.Forward.GoForwardTooltip),
                GUILayout.Height(DISPLAY_KEY_EDITOR_INLINE_NAVIGATOR_BUTTONS_SIZE),
                GUILayout.Width(DISPLAY_KEY_EDITOR_INLINE_NAVIGATOR_BUTTONS_SIZE)))
            {
                GoForwardModelEditorSelection();
            }
            GUI.enabled = true;
            
            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// Draws the Editor mode's display key editor view.
        /// </summary>
        /// <param name="displayKey">The display key to edit.</param>
        /// <param name="showNotes">Whether or not the notes field will appear.</param>
        /// <param name="showLocalizations">Whether or not the localizations for this display key will appear.</param>
        /// <param name="showSectionTitle">Whether or not the title section will appear.</param>
        /// <param name="showToolbarButtons">Whether or not to show the toolbar buttons.</param>
        private void DrawDisplayKeyEditor(
            ref TongueTwisterModel displayKey, 
            bool showNotes = true, 
            bool showLocalizations = true, 
            bool showSectionTitle = true, 
            bool showToolbarButtons = false)
        {
            GUILayout.BeginVertical(GUILayout.ExpandWidth(true));
            if (showSectionTitle)
            {
                DrawSubSectionStart(EditorLabels.LocalizationManager.EditorModes.ModelView.Common
                    .SelectionInformationSection);
            }

            #region Naming & Toolbar
            
            GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
            GUILayout.BeginVertical(GUILayout.ExpandWidth(true));
            if (_settings.useDelayedTextFields)
            {
                var newName = EditorGUILayout.DelayedTextField(
                    new GUIContent(
                        EditorLabels.LocalizationManager.EditorModes.ModelView.DisplayKeys.FormFields
                            .DisplayKeyName.Text,
                        TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.DisplayKey),
                        EditorLabels.LocalizationManager.EditorModes.ModelView.DisplayKeys.FormFields
                            .DisplayKeyName.Tooltip),
                    displayKey.Name,
                    GUILayout.ExpandWidth(true));
                if (newName != displayKey.Name)
                {
                    RecordLocalizationManagerEditorChange(
                        string.Format(
                            EditorLabels.LocalizationManager.EditorChanges.ChangedDisplayKeyName,
                            displayKey.Name, newName));
                
                    displayKey.Name = newName;
                    _modelEditorTreeView.MarkTreeModelAsChanged();
                }
            }
            else
            {
                var newName = EditorGUILayout.TextField(
                    new GUIContent(
                        EditorLabels.LocalizationManager.EditorModes.ModelView.DisplayKeys.FormFields
                            .DisplayKeyName.Text,
                        TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.DisplayKey),
                        EditorLabels.LocalizationManager.EditorModes.ModelView.DisplayKeys.FormFields
                            .DisplayKeyName.Tooltip),
                    displayKey.Name,
                    GUILayout.ExpandWidth(true));
                if (newName != displayKey.Name)
                {
                    RecordLocalizationManagerEditorChange(
                        string.Format(
                            EditorLabels.LocalizationManager.EditorChanges.ChangedDisplayKeyName,
                            displayKey.Name, newName));
                
                    displayKey.Name = newName;
                    _modelEditorTreeView.MarkTreeModelAsChanged();
                }
            }

            GUILayout.Space(5);

            GUI.enabled = false;
            EditorGUILayout.TextField(
                new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Common.FormattedNamePreview,
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Common.FormattedNamePreviewTooltip),
                displayKey.FormattedName);
            EditorGUILayout.TextField(
                new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Common.ActualDisplayKeyNamePreview,
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Common.ActualDisplayKeyNamePreviewTooltip),
                displayKey.GetFullDotName());
            GUI.enabled = true;
            
            GUILayout.EndVertical();

            if (showToolbarButtons)
            {
                GUILayout.Space(15);
                GUILayout.BeginVertical(GUILayout.ExpandWidth(false), GUILayout.Width(EDITING_MODE_INLINE_BUTTON_SIZE));
                if (GUILayout.Button(
                    new GUIContent(
                        TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.Forwards),
                        EditorLabels.TongueTwisterWindow.Common.Buttons.GoTo.Tooltip),
                    GUILayout.Width(EDITING_MODE_INLINE_BUTTON_SIZE),
                    GUILayout.Height(EDITING_MODE_INLINE_BUTTON_SIZE)))
                {
                    _modelEditorTreeView.SetSelection(displayKey.Id);
                }
                        
                if (GUILayout.Button(
                    new GUIContent(
                        TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.Trash), 
                        EditorLabels.TongueTwisterWindow.Common.Buttons.DeleteIconTooltip),
                    GUILayout.Width(EDITING_MODE_INLINE_BUTTON_SIZE),
                    GUILayout.Height(EDITING_MODE_INLINE_BUTTON_SIZE)))
                {
                    RemoveTreeViewElement(displayKey.Id, TongueTwisterModel.ModelType.DisplayKey);
                }
                GUILayout.EndVertical();
            }
            
            GUILayout.EndHorizontal();
            
            #endregion
            
            #region Notes

            if (showNotes)
            {
                GUILayout.Space(15.0f);

                GUILayout.Label(new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Notes.Text,
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Notes.Tooltip));
                var notes = EditorGUILayout.TextArea(
                    displayKey.Notes,
                    EditorStyles.textArea,
                    GUILayout.MinHeight(50),
                    GUILayout.ExpandHeight(true));

                if (notes != displayKey.Notes)
                {
                    RecordLocalizationManagerEditorChange(
                        EditorLabels.LocalizationManager.EditorChanges.ChangedDisplayKeyNotes);
                    displayKey.Notes = notes;
                }
            }

            #endregion

            if (showSectionTitle)
            {
                DrawSectionEnd();
            }

            #region Localizations

            if (showLocalizations)
            {
                DrawSubSectionStart(EditorLabels.LocalizationManager.EditorModes.ModelView.DisplayKeys.Sections.Localizations.Text);
                
                GUILayout.BeginHorizontal();
                displayKey.FilterLocalizations =
                    EditorGUILayout.TextField(
                        displayKey.FilterLocalizations, 
                        _toolbarSearchField);
                if (GUILayout.Button("", _toolbarClearButton))
                {
                    displayKey.FilterLocalizations = "";
                    GUI.FocusControl(null);
                }
                GUILayout.EndHorizontal();
                
                var showHideLabel = displayKey.LocalizationsExpanded
                    ? EditorLabels.TongueTwisterWindow.Common.Hide
                    : EditorLabels.TongueTwisterWindow.Common.Show;
                
                GUILayout.BeginHorizontal();
                if (GUILayout.Button(
                    new GUIContent(
                        EditorLabels.LocalizationManager.EditorModes.ModelView.Localizations.Buttons.AddLocalization.Text,
                        EditorLabels.LocalizationManager.EditorModes.ModelView.Localizations.Buttons.AddLocalization.Tooltip)))
                {
                    AddNewModelToSelectionOrRoot(TongueTwisterModel.ModelType.Localization);
                }
                if (GUILayout.Button(showHideLabel, GUILayout.ExpandWidth(false)))
                {
                    displayKey.LocalizationsExpanded = !displayKey.LocalizationsExpanded;
                }
                GUILayout.EndHorizontal();
                
                if (displayKey.HasLocalizationChildren)
                {
                    if (displayKey.LocalizationsExpanded)
                    {
                        var children = displayKey.GetChildrenOfType(TongueTwisterModel.ModelType.Localization);

                        for (int i = 0; i < children.Length; i++)
                        {
                            var child = children[i];

                            if (!string.IsNullOrWhiteSpace(displayKey.FilterLocalizations) &&
                                !FuzzyMatchStrings(displayKey.FilterLocalizations, child.Text))
                            {
                                continue;
                            }

                            GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));

                            DrawFriendlyIndexNum(i);
                            
                            GUILayout.BeginVertical();
                            
                            DrawHorizontalBreak(5.0f, 5.0f, 0.0f, 0.0f);
                            DrawLocalizationEditor(ref child, true, false);
                            
                            GUILayout.EndVertical();
                            GUILayout.EndHorizontal();
                        }
                    }
                    else
                    {
                        GUILayout.Space(15);
                        DrawHorizontallyFlexibleLabel(EditorLabels.LocalizationManager.EditorModes.ModelView.Localizations.LocalizationsHidden);
                    }
                }
                else
                {
                    GUILayout.Space(15);
                    DrawHorizontallyFlexibleLabel(EditorLabels.LocalizationManager.EditorModes.ModelView.Localizations.NoLocalizations);
                }

                GUILayout.Space(15.0f);
                
                DrawSectionEnd();
            }

            #endregion
            
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
        }

        /// <summary>
        /// Draws the Editor mode's localization editor view.
        /// </summary>
        /// <param name="localization">The localization to edit.</param>
        /// <param name="showToolbarButtons">Flag for whether or not to show the toolbar buttons.</param>
        /// <param name="standaloneMode">If enabled, shows a draw section and will maximize the text editor field
        /// space.</param>
        private void DrawLocalizationEditor(
            ref TongueTwisterModel localization,
            bool showToolbarButtons, 
            bool standaloneMode)
        {
            if (standaloneMode)
            {
                DrawSubSectionStart(EditorLabels.LocalizationManager.EditorModes.ModelView.Common.SelectionInformationSection);
            }

            #region Locale Select (starts a horizontal group & vertical group)
            
            GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
            GUILayout.BeginVertical(GUILayout.ExpandWidth(true));
            
            var locales = LocalizationManagerEditor.ConfiguredLocales;

            if (locales.Count == 0)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(EditorLabels.LocalizationManager.EditorModes.LocaleEditing.NoLocalesConfigured);
                if (GUILayout.Button(
                    new GUIContent(
                        EditorLabels.LocalizationManager.EditorModes.LocaleEditing.CreateFirstLocale.Text,
                        EditorLabels.LocalizationManager.EditorModes.LocaleEditing.CreateFirstLocale.Tooltip)))
                {
                    _windowMode = TongueTwisterWindowMode.LocaleEditor;
                }
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }
            else
            {
                // drop down field for changing locale

                var localeOptions = new List<GUIContent>();
                localeOptions.Add(new GUIContent(EditorLabels.TongueTwisterWindow.Common.None));

                foreach (var locale in locales)
                {
                    var displayName = !string.IsNullOrWhiteSpace(locale.Metadata.DisplayName)
                        ? locale.Metadata.DisplayName
                        : EditorLabels.TongueTwisterWindow.Common.NoName;
                    localeOptions.Add(new GUIContent($"{displayName}", locale.Icon));
                }

                var currentChosenLocale = LocalizationManager.GetLocaleById(localization.LocaleId, false);

                var newLocaleIndex = EditorGUILayout.Popup(
                    new GUIContent(
                        EditorLabels.LocalizationManager.EditorModes.ModelView.Localizations.Locale.Text,
                        EditorLabels.LocalizationManager.EditorModes.ModelView.Localizations.Locale.Tooltip),
                    currentChosenLocale != null ? locales.IndexOf(currentChosenLocale) + 1 : 0,
                    localeOptions.ToArray()) - 1;
                
                var newLocale = newLocaleIndex == -1 ? null : locales[newLocaleIndex];

                if (newLocale != currentChosenLocale)
                {
                    RecordLocalizationManagerEditorChange(
                        EditorLabels.LocalizationManager.EditorChanges.ChangedLocalizationsLocale);

                    var displayName = newLocale != null
                        ? newLocale.Metadata.DisplayName
                        : EditorLabels.LocalizationManager.EditorModes.LocaleEditing.LocaleNotSelectedForLocalization;
                    
                    localization.Name = displayName;
                    localization.LocaleId = newLocale?.Id;
                    _modelEditorTreeView.MarkTreeModelAsChanged();
                }
            }
            
            #endregion

            #region Audio File (ends vertical group)
            
            var audioClip = (AudioClip) EditorGUILayout.ObjectField(
                new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Localizations.AudioClip.Text, 
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Localizations.AudioClip.Tooltip), 
                localization.AudioClip, 
                typeof(AudioClip), 
                false);

            if (audioClip != localization.AudioClip)
            {
                RecordLocalizationManagerEditorChange(
                    EditorLabels.LocalizationManager.EditorChanges.ChangedAudioClipForLocalization);
                localization.AudioClip = audioClip;
                _modelEditorTreeView.MarkTreeModelAsChanged();
            }

            #endregion

            #region Unity Object

            var unityObject = EditorGUILayout.ObjectField(
                new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Localizations.UnityObject.Text, 
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Localizations.UnityObject.Tooltip), 
                localization.UnityObject, 
                typeof(UnityEngine.Object), 
                true);

            if (unityObject != localization.UnityObject)
            {
                RecordLocalizationManagerEditorChange(
                    EditorLabels.LocalizationManager.EditorChanges.ChangedUnityObjectForLocalization);
                localization.UnityObject = unityObject;
                _modelEditorTreeView.MarkTreeModelAsChanged();
            }
            
            #endregion
            
            #region Texture (ends vertical group)

            var texture = (Texture) EditorGUILayout.ObjectField(
                new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Localizations.Texture.Text,
                    EditorLabels.LocalizationManager.EditorModes.ModelView.Localizations.Texture.Tooltip), 
                localization.Texture, 
                typeof(Texture), 
                false,
                GUILayout.ExpandWidth(false));

            if (texture != localization.Texture)
            {
                RecordLocalizationManagerEditorChange(
                    EditorLabels.LocalizationManager.EditorChanges.ChangedTextureForLocalization);
                localization.Texture = texture;
                _modelEditorTreeView.MarkTreeModelAsChanged();
            }
            
            GUILayout.EndVertical();

            #endregion

            #region Toolbar Buttons (ends horizontal group)
            
            if (showToolbarButtons)
            {
                GUILayout.Space(15);
                GUILayout.BeginVertical(GUILayout.ExpandWidth(false), GUILayout.Width(EDITING_MODE_INLINE_BUTTON_SIZE));
                if (GUILayout.Button(
                    new GUIContent(
                        TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.Forwards),
                        EditorLabels.TongueTwisterWindow.Common.Buttons.GoTo.Tooltip),
                    GUILayout.Width(EDITING_MODE_INLINE_BUTTON_SIZE),
                    GUILayout.Height(EDITING_MODE_INLINE_BUTTON_SIZE)))
                {
                    _modelEditorTreeView.SetSelection(localization.Id);
                }

                if (GUILayout.Button(
                    new GUIContent(
                        TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.Trash),
                        EditorLabels.TongueTwisterWindow.Common.Buttons.DeleteIconTooltip),
                    GUILayout.Width(EDITING_MODE_INLINE_BUTTON_SIZE),
                    GUILayout.Height(EDITING_MODE_INLINE_BUTTON_SIZE)))
                {
                    RemoveTreeViewElement(localization.Id, TongueTwisterModel.ModelType.Localization);
                }
                GUILayout.EndVertical();
            }

            GUILayout.EndHorizontal();
            
            #endregion

            #region Text

            GUILayout.Label(new GUIContent(
                EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.LocalizationText.Text,
                EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.LocalizationText.Tooltip));
            
            var localizationText = EditorGUILayout.TextArea(
                localization.Text, 
                EditorStyles.textArea,
                GUILayout.MinHeight(50));

            if (localizationText != localization.Text)
            {
                RecordLocalizationManagerEditorChange( 
                    EditorLabels.LocalizationManager.EditorChanges.ChangedLocalizationText);
                localization.Text = localizationText;
                _modelEditorTreeView.MarkTreeModelAsChanged();
            }
            
            #endregion
            
            if (standaloneMode)
            {
                // ends the main section
                DrawSectionEnd();
            }

            #region Additional Localized Content

            if (standaloneMode)
            {
                var hasAdditionalLocalizedContent = (localization?.AdditionalLocalizedContent?.Count ?? 0) > 0;
                var addLocalizedContent = false;
                var removeAll = false;

                DrawSubSectionStart(
                    EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.AdditionalContent.Text,
                    () =>
                    {
                        if (GUILayout.Button(
                            new GUIContent(
                                EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.AdditionalContent.Buttons.Add.Text,
                                EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.AdditionalContent.Buttons.Add.Tooltip)))
                        {
                            addLocalizedContent = true;
                        }
                    },
                    () =>
                    {
                        GUI.enabled = hasAdditionalLocalizedContent;

                        if (GUILayout.Button(
                                new GUIContent(
                                    EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.AdditionalContent.Buttons.RemoveAll.Text,
                                    EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.AdditionalContent.Buttons.RemoveAll.Tooltip)) &&
                            EditorUtility.DisplayDialog(
                                EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.AdditionalContent.Dialogs.AreYouSure.Title, 
                                EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.AdditionalContent.Dialogs.AreYouSure.Message,
                                EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.AdditionalContent.Dialogs.AreYouSure.Ok, 
                                EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.AdditionalContent.Dialogs.AreYouSure.Cancel))
                        {
                            removeAll = true;
                        }
                        
                        GUI.enabled = true;
                    });

                if (addLocalizedContent)
                {
                    RecordLocalizationManagerEditorChange(EditorLabels.LocalizationManager.EditorChanges.AddedNewAdditionalLocalizationContent);
                    localization.AdditionalLocalizedContent.Add(new LocalizedContent());
                    _modelEditorTreeView.MarkTreeModelAsChanged();
                    throw new ExitGUIException();
                }
                
                if (removeAll)
                {
                    RecordLocalizationManagerEditorChange(EditorLabels.LocalizationManager.EditorChanges.RemovedAllAdditionalLocalizedContent);
                    localization.AdditionalLocalizedContent = new LocalizedContentCollection();
                    _modelEditorTreeView.MarkTreeModelAsChanged();
                    throw new ExitGUIException();
                }

                if (hasAdditionalLocalizedContent)
                {
                    for (int i = 0; i < localization.AdditionalLocalizedContent.Count; i++)
                    {
                        var content = localization.AdditionalLocalizedContent[i];

                        GUILayout.BeginVertical(GUI.skin.box);
                        
                        GUILayout.BeginHorizontal();

                        DrawFriendlyIndexNum(i);
                        
                        GUILayout.BeginVertical(GUILayout.ExpandWidth(true));

                        GUI.enabled = false;
                        EditorGUILayout.IntField(
                            new GUIContent(
                                EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.AdditionalContent.Fields.Index.Text,
                                EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.AdditionalContent.Fields.Index.Tooltip),
                            i);
                        GUI.enabled = true;

                        var contentName = EditorGUILayout.TextField(
                            new GUIContent(
                                EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.AdditionalContent.Fields.Name.Text,
                                EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.AdditionalContent.Fields.Name.Tooltip),
                            content.Identifier);

                        if (contentName != content.Identifier)
                        {
                            RecordLocalizationManagerEditorChange(EditorLabels.LocalizationManager.EditorChanges
                                .ChangedAdditionalLocalizationContentName);
                            content.Identifier = contentName;
                            _modelEditorTreeView.MarkTreeModelAsChanged();
                        }

                        var contentObject = EditorGUILayout.ObjectField(
                            new GUIContent(
                                EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.AdditionalContent
                                    .Fields.Object.Text,
                                EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.AdditionalContent
                                    .Fields.Object.Tooltip),
                            content.Object, typeof(Object),
                            true);

                        if (contentObject != content.Object)
                        {
                            RecordLocalizationManagerEditorChange(EditorLabels.LocalizationManager.EditorChanges
                                .ChangedAdditionalLocalizationObject);
                            content.Object = contentObject;
                            _modelEditorTreeView.MarkTreeModelAsChanged();
                        }

                        GUILayout.EndVertical();

                        GUILayout.Space(15);

                        if (GUILayout.Button(
                            new GUIContent(
                                TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.Trash),
                                EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.AdditionalContent
                                    .Buttons.Remove.Tooltip),
                            GUILayout.Width(ADDITIONAL_CONTENT_BUTTON_SIZES),
                            GUILayout.Height(ADDITIONAL_CONTENT_BUTTON_SIZES)))
                        {
                            RecordLocalizationManagerEditorChange(EditorLabels.LocalizationManager.EditorChanges
                                .RemovedAdditionalLocalizedContentItem);
                            localization.AdditionalLocalizedContent.Remove(content);
                            _modelEditorTreeView.MarkTreeModelAsChanged();
                            throw new ExitGUIException();
                        }

                        GUILayout.EndHorizontal();
                        GUILayout.EndVertical();

                        GUILayout.Space(5);
                    }
                }
                else
                {
                    GUILayout.BeginVertical(GUILayout.Height(150));
                    DrawFullyFlexibleLabel(EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.AdditionalContent.NoAdditionalContent);
                    GUILayout.EndVertical();
                }

                DrawSectionEnd();

            }
            else
            {
                GUILayout.BeginVertical(GUILayout.Height(50));
                GUILayout.Space(5);
                DrawFullyFlexibleLabel(EditorLabels.LocalizationManager.EditorModes.LocaleEditing.Fields.AdditionalContent.GoToThisItemSuggestion);
                GUILayout.EndVertical();
            }
            
            GUILayout.Space(15);

            #endregion
        }

        /// <summary>
        /// Draws the debug section view for the given model.
        /// </summary>
        /// <param name="model">The model to display the debug section for.</param>
        private void DrawEditorDebugForModel(TongueTwisterModel model)
        {
            DrawSubSectionStart(EditorLabels.LocalizationManager.EditorModes.Settings.Preferences.DebugMode.Editor.EditorSectionTitle);
            var previousGuiEnabledValue = GUI.enabled;
            GUI.enabled = false;
            GUILayout.BeginVertical();
            
            EditorGUILayout.TextField(
                new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.Settings.Preferences.DebugMode.Editor.Id.Text,
                    EditorLabels.LocalizationManager.EditorModes.Settings.Preferences.DebugMode.Editor.Id.Tooltip),
                model.Id.ToString());
            
            GUILayout.BeginHorizontal();
            EditorGUILayout.TextField(
                new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.Settings.Preferences.DebugMode.Editor.ParentId.Text,
                    EditorLabels.LocalizationManager.EditorModes.Settings.Preferences.DebugMode.Editor.ParentId.Tooltip),
                model.Parent.Id.ToString());
            GUI.enabled = true;
            if (model.Parent.Id != -1 && 
                GUILayout.Button(
                    TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.Forwards), 
                    GUILayout.Height(18), GUILayout.Width(30))) 
            {
                _modelEditorTreeView.SetSelection(model.Parent.Id);
                GUIUtility.ExitGUI();
            }
            GUI.enabled = false;
            GUILayout.EndHorizontal();

            EditorGUILayout.TextField(
                new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.Settings.Preferences.DebugMode.Editor.Children.Text,
                    EditorLabels.LocalizationManager.EditorModes.Settings.Preferences.DebugMode.Editor.Children.Tooltip),
                (model.HasChildren ? model.Children.Count : 0).ToString());

            if (model.Type == TongueTwisterModel.ModelType.Localization)
            {
                EditorGUILayout.TextField(
                    new GUIContent(
                        EditorLabels.LocalizationManager.EditorModes.Settings.Preferences.DebugMode.Editor.LocaleId.Text,
                        EditorLabels.LocalizationManager.EditorModes.Settings.Preferences.DebugMode.Editor.LocaleId.Tooltip),
                    model?.LocaleId ?? EditorLabels.LocalizationManager.EditorModes.LocaleEditing.GenericNoLocale);
            }
            
            GUILayout.EndVertical();
            GUI.enabled = previousGuiEnabledValue;
            GUILayout.Space(15);
            
            DrawSectionEnd();
        }

        #endregion

        #region GUI Functions - Tools Mode

        /// <summary>
        /// Draws the tools mode area.
        /// </summary>
        private void DrawToolsModeArea()
        {
            DrawHorizontalSplitPanelGroup(
                DrawToolsModeTree,
                DrawToolsModeEditorUi,
                EditorStyles.helpBox,
                EditorStyles.helpBox,
                ref _draggingToolsModeSplitter,
                ref _toolsModeHorizontalSplitterLerp,
                ref _toolsModePanelRect);
        }

        private void DrawToolsModeTree()
        {
            #region Tool Filtering 
            
            GUILayout.BeginHorizontal(_toolbar);
            
            GUILayout.Label(new GUIContent(
                    EditorLabels.LocalizationManager.EditorModes.Tools.Filter.Text,
                    EditorLabels.LocalizationManager.EditorModes.Tools.Filter.Tooltip),
                GUILayout.ExpandWidth(false));
            
            _toolFilter = EditorGUILayout.TextField(_toolFilter, _toolbarSearchField,
                GUILayout.MinWidth(25), GUILayout.ExpandWidth(true));
            
            if (GUILayout.Button("", _toolbarClearButton))
            {
                _toolFilter = "";
                GUI.FocusControl(null);
            }

            _toolsModeTreeView.searchString = _toolFilter;
            
            GUILayout.EndHorizontal();
            
            #endregion
            
            #region Tools Loaded Specs
            
            GUILayout.BeginHorizontal();
            
            GUILayout.Label(
                string.Format(
                    EditorLabels.LocalizationManager.EditorModes.Tools.AllToolsLoadedCount,
                    _toolsModeTreeView.tree.Tools.Count));
            
            GUILayout.FlexibleSpace();

            if (GUILayout.Button(EditorLabels.TongueTwisterWindow.Common.Refresh))
            {
                UpdateToolModeTree();
            }
            
            GUILayout.EndHorizontal();
            
            #endregion

            _toolsModeTreePanelScrollPos = GUILayout.BeginScrollView(_toolsModeTreePanelScrollPos, GUIStyle.none, GUI.skin.verticalScrollbar);
            _toolsModeTreeView.OnGUI(new Rect(0, 0, _toolsModePanelRect.width, _toolsModePanelRect.height));
            GUILayout.EndScrollView();
        }

        private void DrawToolsModeEditorUi()
        {
            if (!_toolsModeTreeView.HasSelection())
            {
                DrawFullyFlexibleLabel(EditorLabels.LocalizationManager.EditorModes.Tools.NothingSelected);
                return;
            }
            
            var selection = _toolsModeTreeView.GetSelection();

            _toolsModeEditorUiScrollPos = GUILayout.BeginScrollView(_toolsModeEditorUiScrollPos, GUIStyle.none,
                GUI.skin.verticalScrollbar);

            foreach (var index in selection)
            {
                var tool = _toolsModeTreeView.GetTool(index);

                if (tool == null)
                {
                    continue;
                }

                DrawSectionStart($"{tool.Title} ({tool.Version})", tool.Description);

                try
                {
                    tool.DrawEditorUi();
                }
                catch (Exception exception)
                {
                    if (exception is ExitGUIException exitGUIException)
                    {
                        throw exitGUIException;
                    }

                    Debug.LogError($"Failed to draw tool UI for {tool?.Category}. Reason: {exception.Message}");
                }

                DrawSectionEnd();
            }

            GUILayout.EndScrollView();
        }

        #endregion

        #region GUI Functions - Home Mode ("About")

        private void DrawHomeModeArea()
        {
            // Actual about section
            DrawAboutSection();
            
            // Documentation section
            DrawDocumentationSection();
            
            // Controls section
            DrawControlsAboutSection();
        }

        private void DrawAboutLink(string title, string tooltip, string url)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("•");
            DrawUrl(new GUIContent(title, tooltip), url);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        private void DrawAboutSection()
        {
            DrawSectionStart(
                EditorLabels.LocalizationManager.EditorModes.Home.Sections.About.Title, 
                EditorLabels.LocalizationManager.EditorModes.Home.Sections.About.Description);
            
            GUILayout.BeginHorizontal();
            GUILayout.Space(15);
            GUILayout.BeginVertical();
            
            GUILayout.BeginHorizontal();

            GUILayout.Label(
                TongueTwisterIconUtility.GetIcon(TongueTwisterIconUtility.IconType.Logo),
                GUILayout.MaxHeight(50));
            
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            GUILayout.Label(
                EditorLabels.LocalizationManager.EditorModes.Home.Sections.Version.VersionLabel,
                EditorStyles.boldLabel,
                GUILayout.ExpandWidth(false));
            GUILayout.Space(15);
            GUILayout.Label(
                EditorLabels.LocalizationManager.EditorModes.Home.Sections.Version.VersionText, 
                EditorStyles.boldLabel,
                GUILayout.ExpandWidth(false));
            GUILayout.EndHorizontal();

            GUILayout.Space(15);
            
            GUILayout.Label(
                EditorLabels.LocalizationManager.EditorModes.Home.Sections.About.ExtendedDescription, 
                EditorStyles.wordWrappedLabel);
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            GUILayout.Space(15);
            DrawSectionEnd();
        }
        
        private void DrawControlsAboutSection()
        {
            DrawSectionStart(
                EditorLabels.LocalizationManager.EditorModes.Home.Sections.EditorControls.Title, 
                EditorLabels.LocalizationManager.EditorModes.Home.Sections.EditorControls.Description);
            
            GUILayout.BeginHorizontal();
            GUILayout.Space(15);
            GUILayout.BeginVertical();
                        
            GUILayout.Label(
                EditorLabels.LocalizationManager.EditorModes.Home.Sections.EditorControls.ControlsExplanation, 
                EditorStyles.wordWrappedLabel);
            
            GUILayout.Space(15);
            GUILayout.BeginHorizontal(EditorStyles.helpBox);
            GUILayout.BeginVertical();
            // todo: https://trello.com/c/ze7ARFR7/23-configurable-editor-controls configurable editor shortcuts
            // Also as a note, these labels/strings will remain here until the above feature is implemented
            var controls = new [] {"DEL", "CTRL + D", "CTRL + C", "CTRL + V", "ARROW KEYS"};
            foreach (var control in controls) GUILayout.Label(control, EditorStyles.boldLabel);
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            var descriptions = new [] {"Delete", "Duplicate", "Copy", "Paste", "Navigate, expand, and collapse tree elements"};
            foreach (var description in descriptions) GUILayout.Label(description);
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            GUILayout.Space(15);
            DrawSectionEnd();
        }

        private void DrawDocumentationSection()
        {
            DrawSectionStart(
                EditorLabels.LocalizationManager.EditorModes.Home.Sections.Documentation.Title, 
                EditorLabels.LocalizationManager.EditorModes.Home.Sections.Documentation.Description);
            
            GUILayout.BeginHorizontal();
            GUILayout.Space(30.0f); 
            GUILayout.BeginVertical();
            
            // Online documentation
            DrawAboutLink(
                EditorLabels.LocalizationManager.EditorModes.Home.Sections.Documentation.OnlineDocumentation.Title, 
                EditorLabels.LocalizationManager.EditorModes.Home.Sections.Documentation.OnlineDocumentation.Tooltip,
                EditorLabels.LocalizationManager.EditorModes.Home.Sections.Documentation.OnlineDocumentation.URL);
            
            // Discord URL
            DrawAboutLink(
                EditorLabels.LocalizationManager.EditorModes.Home.Sections.Documentation.Discord.Title, 
                EditorLabels.LocalizationManager.EditorModes.Home.Sections.Documentation.Discord.Tooltip, 
                EditorLabels.LocalizationManager.EditorModes.Home.Sections.Documentation.Discord.URL);

            // Road map
            DrawAboutLink(
                EditorLabels.LocalizationManager.EditorModes.Home.Sections.Documentation.TrelloRoadMap.Title, 
                EditorLabels.LocalizationManager.EditorModes.Home.Sections.Documentation.TrelloRoadMap.Tooltip,
                EditorLabels.LocalizationManager.EditorModes.Home.Sections.Documentation.TrelloRoadMap.URL);
            
            // Reporting
            DrawAboutLink(
                EditorLabels.LocalizationManager.EditorModes.Home.Sections.Documentation.TrelloBugs.Title, 
                EditorLabels.LocalizationManager.EditorModes.Home.Sections.Documentation.TrelloBugs.Tooltip,
                EditorLabels.LocalizationManager.EditorModes.Home.Sections.Documentation.TrelloBugs.URL);
            
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            GUILayout.Space(15);
            DrawSectionEnd();
        }

        #endregion

        #region Editor Utilities

        /// <summary>
        /// Does a fake "fuzzy" string comparison to see if the "left" and "right" strings match. Perfect for
        /// filtering or searching. 
        /// </summary>
        /// <param name="left">String #1 to compare to string #2.</param>
        /// <param name="right">String #2 to compare to string #1.</param>
        /// <returns>Returns true if the strings are a fuzzy match.</returns>
        private bool FuzzyMatchStrings(string left, string right)
        {
            if (string.IsNullOrWhiteSpace(left)) left = "";
            if (string.IsNullOrWhiteSpace(right)) right = "";
            if (string.IsNullOrWhiteSpace(left) && !string.IsNullOrWhiteSpace(right)) return false;
            if (string.IsNullOrWhiteSpace(right) && !string.IsNullOrWhiteSpace(left)) return false;
            var formattedLeft = left.ToLower().Replace(" ", "");
            var formattedRight = right.ToLower().Replace(" ", "");
            return formattedLeft.Contains(formattedRight) || formattedRight.Contains(formattedLeft);
        }
        
        /// <summary>
        /// Duplicates the selected tree view items.
        /// </summary>
        private void DuplicateSelectedTreeViewItems()
        {
            if (!_modelEditorTreeView.HasVisibleSelection()) return;
            
            RecordLocalizationManagerEditorChange( 
                EditorLabels.LocalizationManager.EditorChanges.DuplicatedSelection);
            
            _modelEditorTreeView.tree.Duplicate(_modelEditorTreeView.GetSelection());
        }

        /// <summary>
        /// Adds a new model object to the selected tree view item(s) of the given type. 
        /// </summary>
        /// <param name="modelType">The type of the model to create.</param>
        /// <returns></returns>
        private List<TongueTwisterModel> InternalAddNewModelToSelectionOrRoot(TongueTwisterModel.ModelType modelType)
        {
            // when nothing is selected, a "root" level model is constructed. 
            
            if (!_modelEditorTreeView.HasVisibleSelection())
            {
                if (modelType != TongueTwisterModel.ModelType.Localization)
                {
                    RecordLocalizationManagerEditorChange(
                        modelType == TongueTwisterModel.ModelType.Group
                            ? EditorLabels.LocalizationManager.EditorChanges.AddedNewGroups
                            : EditorLabels.LocalizationManager.EditorChanges.AddedNewDisplayKeys);
                    
                    _modelEditorTreeView.tree.AddNewRootElement(modelType);
                }
                else
                {
                    Debug.LogError(EditorLabels.LocalizationManager.Errors.CannotAddNewModelNothingSelected);
                }

                return new List<TongueTwisterModel>();
            }
            
            var selectedIndices = _modelEditorTreeView.GetSelection();
            var result = new List<TongueTwisterModel>();

            foreach (var selectedIndex in selectedIndices)
            {
                var model = InternalAddNewModel(modelType, _modelEditorTreeView.tree.Find(selectedIndex));
                if (model == null)
                {
                    continue;
                }
                result.Add(model);
            }

            return result;
        }

        /// <summary>
        /// Adds a new model object to the given parent.
        /// </summary>
        /// <param name="modelType">The type of model to create.</param>
        /// <param name="parent">The parent to assign the model to.</param>
        private TongueTwisterModel InternalAddNewModel(TongueTwisterModel.ModelType modelType, TongueTwisterModel parent)
        {
            // cannot add groups to non-groups
            if (modelType == TongueTwisterModel.ModelType.Group && 
                parent.Type != TongueTwisterModel.ModelType.Group)
            {
                return null;
            }

            // cannot add display keys to non-groups
            if (modelType == TongueTwisterModel.ModelType.DisplayKey &&
                parent.Type != TongueTwisterModel.ModelType.Group)
            {
                return null;
            }

            // cannot add localizations to non-display-keys
            if (modelType == TongueTwisterModel.ModelType.Localization &&
                parent.Type != TongueTwisterModel.ModelType.DisplayKey)
            {
                return null;
            }
            
            var changeString = ""; 

            switch (modelType)
            {
                case TongueTwisterModel.ModelType.Group:
                    changeString = EditorLabels.LocalizationManager.EditorChanges.AddedNewGroups;
                    break; 
                case TongueTwisterModel.ModelType.DisplayKey:
                    changeString = EditorLabels.LocalizationManager.EditorChanges.AddedNewDisplayKeys;
                    break;
                case TongueTwisterModel.ModelType.Localization:
                    changeString = EditorLabels.LocalizationManager.EditorChanges.AddedNewLocalizations;
                    break;
            }
            
            RecordLocalizationManagerEditorChange(changeString);

            return _modelEditorTreeView.tree.AddElement(parent, modelType);
        }

        /// <summary>
        /// Removes an individual tree view element with the given ID and type.
        /// </summary>
        /// <param name="elementId">The element with the given ID to remove.</param>
        /// <param name="modelType">The type of model that's being removed.</param>
        private void RemoveTreeViewElement(int elementId, TongueTwisterModel.ModelType modelType)
        {
            var changeString = ""; 

            switch (modelType)
            {
                case TongueTwisterModel.ModelType.Group:
                    changeString = EditorLabels.LocalizationManager.EditorChanges.RemovedGroups;
                    break; 
                case TongueTwisterModel.ModelType.DisplayKey:
                    changeString = EditorLabels.LocalizationManager.EditorChanges.RemovedDisplayKeys;
                    break;
                case TongueTwisterModel.ModelType.Localization:
                    changeString = EditorLabels.LocalizationManager.EditorChanges.RemovedLocalizations;
                    break;
            }

            RecordLocalizationManagerEditorChange(changeString);
            _modelEditorTreeView.tree.RemoveElement(elementId);
        }

        private void RemoveSelectedLocaleTreeViewItems()
        {
            if (!_localeEditorTreeView.HasSelection())
            {
                return;
            }
            
            var selectedLocales = new List<Locale>();
            var selectedIndices = _localeEditorTreeView.GetSelection();
            var configuredLocales = LocalizationManagerEditor.ConfiguredLocales;
            var models = LocalizationManagerEditor.ModelCollection.Models.Copy();
            var madeModelChange = false;

            for (int i = 0; i < selectedIndices.Count; i++)
            {
                selectedLocales.Add(configuredLocales[selectedIndices[i]]);
            }

            _localeEditorTreeView.Deselect();
            
            RecordLocalizationManagerEditorChange(
                EditorLabels.LocalizationManager.EditorChanges.RemovedLocaleFromConfiguredLocaleList);
            
            for (int i = selectedLocales.Count - 1; i >= 0; i--)
            {
                var locale = selectedLocales[i];
                
                if (LocalizationManagerEditor.DefaultLocaleId == locale.Id)
                {
                    LocalizationManagerEditor.DefaultLocaleId = null;
                }

                foreach (var model in models)
                {
                    if (model.Type != TongueTwisterModel.ModelType.Localization || model.LocaleId != locale.Id)
                    {
                        continue;
                    }
                        
                    model.LocaleId = null;
                    model.Name = EditorLabels.LocalizationManager.EditorModes.LocaleEditing.LocaleNotSelectedForLocalization;
                    madeModelChange = true;
                }
            }
            
            if (madeModelChange)
            {
                LocalizationManagerEditor.ModelCollection = new TongueTwisterModelCollection(models);
            }
            
            LocalizationManagerEditor.ConfiguredLocales = LocalizationManagerEditor.ConfiguredLocales
                .Where(existingConfiguredLocale => !selectedLocales.Select(loc => loc.Id).Contains(existingConfiguredLocale.Id))
                .ToList();
                    
            RunModelEditorTreeModelErrorChecks();
            UpdateLocaleEditorTree();
        }

        /// <summary>
        /// Removes all selected tree view items.
        /// </summary>
        private void RemoveSelectedModelTreeViewItems()
        {
            if (!_modelEditorTreeView.HasSelection())
            {
                return;
            }
            
            var items = _modelEditorTreeView.GetSelection();
            _modelEditorTreeView.Deselect();
            RecordLocalizationManagerEditorChange( 
                EditorLabels.LocalizationManager.EditorChanges.RemovedMultipleItems);
            _modelEditorTreeView.tree.RemoveElements(items);
        }

        /// <summary>
        /// Records the tree view's current selection as a list of int ID's. Also, gets the "full dot name" if the
        /// current selection is a single display key.
        /// </summary>
        private void CopySelectedTreeViewItems()
        {
            if (_modelEditorTreeView.HasVisibleSelection())
            {
                var selection = _modelEditorTreeView.GetSelection();
                var clearBufferOnFirstDisplayKey = true;
                _editorCopiedModels = selection;
                for (int i = 0; i < selection.Count; i++)
                {
                    var id = selection[i];
                    var model = _modelEditorTreeView.tree.Find(id);
                    if (model == null || model.Type != TongueTwisterModel.ModelType.DisplayKey) continue;
                    if (clearBufferOnFirstDisplayKey)
                    {
                        EditorGUIUtility.systemCopyBuffer = "";
                        clearBufferOnFirstDisplayKey = false;
                    }
                    EditorGUIUtility.systemCopyBuffer +=
                        model.GetFullDotName() + 
                        ((i == selection.Count - 1) ? "" : "\n");
                }
            }
            else
            {
                _editorCopiedModels = new List<int>();
            }
        }

        /// <summary>
        /// Duplicates all tree view items in the copy buffer (<c>_copiedModels</c>) to the selected parents.
        /// </summary>
        private void PasteCopiedTreeViewItems()
        {
            if (_editorCopiedModels == null || _editorCopiedModels.Count == 0)
            {
                Debug.LogWarning(EditorLabels.LocalizationManager.Warnings.NothingWasCopied);
                return;
            } 
            
            RecordLocalizationManagerEditorChange( 
                EditorLabels.LocalizationManager.EditorChanges.PastedMultipleItems);
            
            if (!_modelEditorTreeView.HasVisibleSelection())
            {
                _modelEditorTreeView.tree.DuplicateTo(_editorCopiedModels, _modelEditorTreeView.tree.Root.Id);
            }
            else
            {
                _modelEditorTreeView.tree.DuplicateTo(_editorCopiedModels, _modelEditorTreeView.GetSelection());
            }
        }

        /// <summary>
        /// Handles the occurrence of a play mode state change.
        /// </summary>
        /// <param name="playModeStateChange">The play mode state.</param>
        private void HandlePlayModeStateChange(PlayModeStateChange playModeStateChange)
        {
            switch (playModeStateChange)
            {
                case PlayModeStateChange.ExitingPlayMode:
                    _exitingPlayMode = true;
                    break;
                
                case PlayModeStateChange.EnteredEditMode:
                    if (_exitingPlayMode)
                    {
                        try
                        {
                            var localizationManager =
                                EditorUtility.InstanceIDToObject(
                                        EditorPrefs.GetInt(EDITOR_PREF_LAST_OBJ_INSTANCE_ID_PRE_PLAYMODE))
                                    as LocalizationManager;

                            if (localizationManager)
                            {
                                var localizationManagerEditor =
                                    UnityEditor.Editor.CreateEditor(localizationManager) as LocalizationManagerEditor;

                                LocalizationManagerEditor = localizationManagerEditor;
                                Repaint();
                            }
                        }
                        catch
                        {
                            /* we don't care if this fails but we'd rather not let the user know */
                        }

                        _exitingPlayMode = false;
                    }
                    break;
            }
        }

        #endregion
        
        #region Supporting Types

        public enum TongueTwisterWindowMode
        {
            Home = 0,
            DisplayKeyEditor = 1,
            LocaleEditor = 2,
            Tools = 3,
            Settings = 4,
        }

        private enum LastUsedLocalizationManagerType
        {
            Prefab = 0,
            SceneAsset = 1
        }

        [Serializable]
        private class TTWSettings
        {
            /// <summary>
            /// The TongueTwister window debug mode. When enabled, shows additional values for various objects.
            /// </summary>
            public bool debugMode;
            
            /// <summary>
            /// Forces the TongueTwister window to use delayed text fields for item naming if performance is taking a
            /// hit.
            /// </summary>
            public bool useDelayedTextFields;
            
            /// <summary>
            /// When a change is made to the tree, an error check will automatically run. Turn this off if you're
            /// experiencing performance issues.
            /// </summary>
            public bool manualErrorChecks;

            /// <summary>
            /// When the user is not editing a prefab, they normally would receive a warning. If this is enabled, the
            /// user will no longer see that warning.
            /// </summary>
            public bool hidePrefabWarning;
            
            /// <summary>
            /// When disabled, the system will not bother the user with announcements at all.
            /// </summary>
            public bool showAnnouncements = true;

            /// <summary>
            /// Keeps track of validation rules and whether or not they're enabled/disabled.
            /// </summary>
            public ValidationRuleStatusCollection validationRuleStatusCollection = new ValidationRuleStatusCollection();
        }

        #endregion
    }
}