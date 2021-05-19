namespace TongueTwister.StaticLabels
{
    /// <summary>
    /// Represents a static set of localizations for use within the editor. Also provides a means of setting the given
    /// Editor Language for Tongue Twister's window. These labels may be used anywhere in the project as they are not
    /// strictly tied to an Editor namespace.
    /// </summary>
    public static class EditorLabels
    {
        public static class TongueTwisterWindow
        {
            public const string Title = 
                "Tongue Twister";

            public static class Common
            {
                public const string ErrorCapitalized =
                    "Error";

                public const string No =
                    "No";

                public const string None =
                    "None";

                public const string Ok =
                    "Ok";

                public const string Open =
                    "Open";

                public const string Refresh =
                    "Refresh";
                
                public const string Row =
                    "row";

                public const string Rows =
                    "rows";
                
                public const string Yes =
                    "Yes";

                public const string Show =
                    "Show";

                public const string Hide =
                    "Hide";

                public const string Close =
                    "Close";

                public const string NoName =
                    "No Name";

                public const string Preview =
                    "Preview";

                public static class Buttons
                {
                    public const string DeleteIconTooltip =
                        "Delete";

                    public static class GoTo
                    {
                        public const string Text =
                            "Go To...";

                        public const string Tooltip =
                            "Go to this item";
                    }
                }
            }

            public static class Warnings
            {
                public const string PrefabWarning =
                    "You're currently editing a scene object, changes will not be saved to the prefab.";

                public const string CreatePrefabWarning =
                    "You're currently editing a Localization Manager that is not represented by a prefab.";

                public class PrefabWarningButtons
                {
                    public class OpenPrefab
                    {
                        public const string Text =
                            "Open Prefab";

                        public const string Tooltip =
                            "Open the prefab tied to this GameObject.";
                    }

                    public class HideWarning
                    {
                        public const string Text =
                            "Hide This Warning";

                        public const string Tooltip =
                            "Hide this warning and don't show it again. This can be changed in the TongueTwister preferences.";
                    }

                    public class CreatePrefab
                    {
                        public const string Text =
                            "Create Prefab";

                        public const string Tooltip =
                            "Create a new prefab based on the selected Localization Manager. It will appear in the Assets folder.";
                    }
                }

                public const string PlaybackModeWarning =
                    "Editing is not allowed when the Unity Editor is in playback mode.";
            }

            public static class Errors
            {
                public const string FailedToDeserializeTtwSettings =
                    "Failed to deserialize TTW settings, reason: {0}";
            }

            public static class EditorChanges
            {
                public const string ChangedLocalizationManager =
                    "Changed localization manager.";
            }

            public static class AnnouncementsSetting
            {
                public const string Text =
                    "Show announcements notifications";

                public const string Tooltip =
                    "Shows or hides announcement notifications displayed at the top of the TongueTwister window.";
            }

            public static class ValidationRulesSection
            {
                public const string Title =
                    "Validation Rules";

                public const string Description =
                    "Enable or disable validation rules.";

                public const string NoValidationRules =
                    "No validation rules to display.";

                public const string ResetWarning =
                    "Changing a validation's rules identification code may cause settings here to reset.";
            }
        }

        public static class TTAnnouncementsWindow
        {
            public const string NewAnnouncements =
                "There are new announcements!";

            public const string View =
                "View";

            public const string MarkAsRead =
                "Mark as Read";

            public const string MarkAllAsRead =
                "Mark All as Read";

            public const string MarkAllAsUnread =
                "Mark All as Unread";

            public const string Refresh =
                "Refresh";

            public const string Announcements =
                "Announcements";

            public const string Hide =
                "Hide Notifications";

            public const string GettingAnnouncements =
                "Getting announcements...";

            public const string NoAnnouncementsAtThisTime =
                "No announcements at this time. Check back later!";

            public const string WindowTitle =
                "Announcements (TongueTwister)";

            public const string NewLabel =
                "<color=#FF421AFF><b>[NEW]</b></color>";
            
            public static class HideDialog
            {
                public const string Title =
                    "Announcement Notifications Hidden";

                public const string Message =
                    "You will no longer see the \"new announcements\" banner.\n\nYou can undo this by visiting the TT settings or by going to \"Tools -> Tongue Twister -> Announcements\".";

                public const string Ok =
                    "OK";
            }
        }

        public static class LocalizationManager
        {
            public static class Compiling
            {
                public const string PleaseWait = 
                    "Compiling, please wait!...";
            }
            
            public static class EditorChanges
            {
                public const string AddedNewDisplayKeys =
                    "Added new display key(s)";

                public const string AddedNewLocalizations =
                    "Added new localization(s)";

                public const string AddedNewLocalizationDictionary =
                    "Added new localization dictionary";
                
                public const string AddedNewGroups =
                    "Added new group(s)";

                public const string AddedNewAdditionalLocalizationContent =
                    "Added new additional content to localization.";

                public const string ChangedAdditionalLocalizationContentName =
                    "Changed identifier of an additional localization content item";

                public const string ChangedAdditionalLocalizationObject =
                    "Changed object reference of an additional localization content item";

                public const string RemovedAdditionalLocalizedContentItem =
                    "Removed an additional localized content item.";

                public const string ChangedAdditionalLocalizationsFolderPath =
                    "Changed the \"additional localizations folder\" path.";

                public const string ChangedDisplayKeyName =
                    "Changed display key name from {0} to {1}";

                public const string ChangedGroupName =
                    "Changed group name from {0} to {1}";

                public const string ChangedGroupNotes =
                    "Changed group \"{0}\"'s notes";

                public const string ChangedLastUsedSystemLanguagePlayerPrefKey =
                    "Changed \"Last Used System Language Player Pref Key\" value";

                public const string ChangedLocalizationText =
                    "Changed localization text";

                public const string DragAndDrop =
                    "Dragged and dropped {0} {1}";

                public const string DuplicatedSelection =
                    "Duplicated selected items";

                public const string PastedMultipleItems =
                    "Pasted multiple items";

                public const string RemovedDisplayKeys =
                    "Removed display key(s)";

                public const string RemovedEmptyDisplayKeys =
                    "Removed all empty display keys";
                
                public const string RemovedEmptyGroups =
                    "Removed all empty groups";
                
                public const string RemovedEmptyLocalizations =
                    "Removed all empty localizations";

                public const string RemovedGroups =
                    "Removed group(s)";

                public const string RemovedLocalizations =
                    "Removed localization(s)";

                public const string RemovedMultipleItems =
                    "Removed multiple items";

                public const string RemovedAllAdditionalLocalizedContent =
                    "Removed all additional localized content from localization";

                public const string ToggledSaveLastUsedLanguageToPlayerPrefs =
                    "Toggled \"Save Last used locale to Player Prefs\"";

                public const string ChangedLocaleIcon =
                    "Changed locale's icon";

                public const string ChangedLocalesNativeName =
                    "Changed locale's native name";

                public const string ChangedLocalesCustomCode =
                    "Changed locale's custom code";

                public const string ChangedLocalesLanguageCode =
                    "Changed locale's language code";
                
                public const string ChangedLocalesCountryCode =
                    "Changed locale's country code";

                public const string ChangedLocaleNotes =
                    "Changed locale's notes";

                public const string ChangedLocaleBackingLanguage =
                    "Changed locale's backing language";

                public const string ChangedDisplayKeyNotes =
                    "Changed display key notes";

                public const string ChangedLocalizationsLocale =
                    "Changed localization's locale";

                public const string ChangedDefaultLocale =
                    "Changed default locale";

                public const string ChangedAudioClipForLocalization =
                    "Changed audio clip for localization";

                public const string ChangedTextureForLocalization =
                    "Changed texture for localization";

                public const string ChangedUnityObjectForLocalization =
                    "Changed Unity Object for localization";

                public const string AddedLocaleToConfiguredLocaleList =
                    "Added new locale to configured locales list";

                public const string RemovedLocaleFromConfiguredLocaleList =
                    "Removed locale from configured locales list";
            }
            
            public static class EditorModes
            {
                public static class Settings
                {
                    public const string Text =
                        "Settings";

                    public const string Tooltip =
                        "Configure TongueTwister editor behaviours and localization settings.";
                    
                    public const string NoLocalizationManagerWarning =
                        "Other settings will become available once a LocalizationManager has been selected.";

                    public static class Preferences
                    {
                        public const string Title =
                            "Preferences";

                        public const string Description =
                            "Change how the TongueTwister window works.";

                        public const string Advanced =
                            "Advanced";

                        public const string General =
                            "General";
                        
                        public static class UseDelayedTextFields
                        {
                            public const string Text =
                                "Use delayed text input fields";

                            public const string Tooltip =
                                "Forces the window editor to use delayed text input for some fields if performance has become an issue.";
                        }
                        
                        public static class ManualErrorChecks
                        {
                            public const string Text =
                                "Manual Validation Checks";

                            public const string Tooltip =
                                "Validation checks won't happen until the user presses the \"ReRun Validation Check\" button. Use this if you're experiencing performance issues.";
                        }

                        public static class HidePrefabWarning
                        {
                            public const string Text =
                                "Hide \"create prefab\" warning";

                            public const string Tooltip =
                                "When enabled, no longer shows the warning that states \"create a prefab\".";
                        }

                        public static class DebugMode
                        {
                            public const string Text =
                                "Enable debug mode";

                            public const string Tooltip =
                                "Enables debug mode for the TongueTwister window which lasts until it's closed. This will display additional information while editing groups, display keys, localizations, etc.";

                            public static class Editor
                            {
                                public const string EditorSectionTitle =
                                    "Debug Values";

                                public static class Id
                                {
                                    public const string Text =
                                        "ID";

                                    public const string Tooltip =
                                        "The ID of this model.";
                                }

                                public static class ParentId
                                {
                                    public const string Text =
                                        "Parent ID";

                                    public const string Tooltip =
                                        "The model's parent's ID. A value of -1 means the parent is the root model.";
                                }

                                public static class Children
                                {
                                    public const string Text =
                                        "Children Count";

                                    public const string Tooltip =
                                        "The number of children that this model is the parent to.";
                                }

                                public static class LocaleId
                                {
                                    public const string Text =
                                        "Locale ID";

                                    public const string Tooltip =
                                        "The ID of the selected locale. A value of -1 means no locale selected.";
                                }
                            }

                            public static class LocaleEditor
                            {
                                public const string LocaleEditorSectionTitle =
                                    "Debug Values";
                                
                                public static class Id
                                {
                                    public const string Text =
                                        "Id";

                                    public const string Tooltip =
                                        "The unique ID of this locale.";
                                }
                            }
                        }
                    }

                    public static class LocalizationSettings
                    {
                        public const string Title =
                            "Localization Settings";

                        public const string Description =
                            "Configure the selected localization manager object.";
                        
                        public static class AdditionalLocalizationsFolder
                        {
                            public const string Text =
                                "Folder Name";

                            public const string ApplicationDataPathInstructions =
                                "Choose a folder path which will be appended to the end of the game's \"Application.dataPath\"";
                            
                            public const string ApplicationDataPathTooltip =
                                "The folder that will be appended to the end of the value of \"Application.dataPath\".";
                            
                            public const string ApplicationPersistentDataPathInstructions =
                                "Choose a folder path which will be appended to the end of the game's \"Application.persistentDataPath\"";
                            
                            public const string ApplicationPersistentDataPathTooltip =
                                "The folder that will be appended to the end of the value of \"Application.persistentDataPath\".";

                            public const string SimplePathTooltip =
                                "The folder that additional localizations will be expected to exist in.";

                            public const string PathDoesNotExist =
                                "This path does not exist.";
                        }
                        
                        public static class AllowLoadingTongueTwisterLocalizationsFromFile
                        {
                            public const string Text =
                                "Enable loading additional localizations";

                            public const string Tooltip =
                                "When enabled, the LocalizationManager will check the below directory for any additional localization files that can be loaded into the game to provide additional supported languages and translations.";
                        }
                        
                        public static class FolderPathBuilderModule
                        {
                            public const string Text =
                                "Folder Path Builder Module";

                            public const string Tooltip =
                                "The folder path builder module based on a scriptable object which is responsible for creating the correct folder path.";
                        }
                        
                        public static class SaveLastUsedLanguageToPlayerPrefs
                        {
                            public const string Text =
                                "Save the player's last used locale to PlayerPrefs";

                            public const string Tooltip =
                                "When enabled, the player's last used locale will be saved using Unity's PlayerPrefs.";

                            public static class PlayerPrefKey
                            {
                                public const string Text =
                                    "Player Pref Key";

                                public const string Tooltip =
                                    "The name of the key used to store the player's last used locale in PlayerPrefs.";
                            }
                        }

                        public static class DefaultLocale
                        {
                            public const string Text =
                                "Default Locale";

                            public const string Tooltip =
                                "The locale the system will start up with.";
                        }
                    }
                }

                public static class LocaleEditing
                {
                    public const string Text =
                        "Locales";

                    public const string Tooltip =
                        "Manage all locales.";

                    public static class Buttons
                    {
                        public static class Add
                        {
                            public const string Tooltip =
                                "Add a new locale.";
                        }

                        public static class SetLanguageCodeFromSystemLanguage
                        {
                            public const string Text =
                                "Set Language Code from System Language";

                            public const string Tooltip =
                                "Takes the \"backing language\" and finds the closest resembled ISO 639 language code.";
                        }

                        public static class SetSystemLanguageFromLanguageCode
                        {
                            public const string Text =
                                "Set System Language from ISO Language Code";

                            public const string Tooltip =
                                "Takes the ISO 639 language code and finds the closest resembled System Language value.";
                        }

                        public static class OpenISOWindow
                        {
                            public const string Text =
                                "Open the Localization Code Window";
                        }
                    }

                    public static class Fields
                    {
                        public static class Id
                        {
                            public const string Text =
                                "ID (GUID)";

                            public const string Tooltip =
                                "The ID of this locale, which will always be represented through TongueTwister as a GUID.";

                            public const string Copy =
                                "Copy";
                        }
                        
                        public static class Name
                        {
                            public const string Text =
                                "Display Name";

                            public const string Tooltip =
                                "The name of the locale in its own localized spelling and characters.";
                        }

                        public static class LocalizationText
                        {
                            public const string Text =
                                "Localized Text";

                            public const string Tooltip =
                                "The actual localized text for this localization.";
                        }

                        public static class AdditionalContent
                        {
                            public const string Text =
                                "Additional Content";

                            public const string NoAdditionalContent =
                                "No additional localized content.";

                            public const string GoToThisItemSuggestion =
                                "Go to this localization to manage additional localized content.";

                            public static class Buttons
                            {
                                public static class Add
                                {
                                    public const string Text =
                                        "Add";

                                    public const string Tooltip =
                                        "Add a new additional content item.";
                                }

                                public static class Remove
                                {
                                    public const string Tooltip =
                                        "Remove this additional localized content item.";
                                }

                                public static class RemoveAll
                                {
                                    public const string Text =
                                        "Remove All";

                                    public const string Tooltip =
                                        "Removes all additional localized content items listed below.";
                                }
                            }

                            public static class Dialogs
                            {
                                public static class AreYouSure
                                {
                                    public const string Title =
                                        "Remove All Additional Localized Content Items?";

                                    public const string Message =
                                        "Are you sure you want to delete all additional localized content items? This can be undone.";

                                    public const string Ok =
                                        "Yes";

                                    public const string Cancel =
                                        "No";
                                }
                            }

                            public static class Fields
                            {
                                public static class Name
                                {
                                    public const string Text =
                                        "Identifier";

                                    public const string Tooltip =
                                        "A name or identifier used for easily retrieving this object.";
                                }

                                public static class Object
                                {
                                    public const string Text =
                                        "Object";

                                    public const string Tooltip =
                                        "A reference to a UnityEngine.Object.";
                                }

                                public static class Index
                                {
                                    public const string Text =
                                        "Index";

                                    public const string Tooltip =
                                        "The indexed position of this additional localized content within its parent collection. This number can be used to access the content like an array item.";
                                }
                            }
                        }
                        
                        public static class NativeName
                        {
                            public const string Text =
                                "Native Name";

                            public const string Tooltip =
                                "The name of this locale as it may appear to someone who would be using it.";
                        }
                        
                        public static class CustomCode
                        {
                            public const string Text =
                                "Custom Code";

                            public const string Tooltip =
                                "A custom code used for referencing a locale.";
                        }

                        public static class BackingLanguage
                        {
                            public const string Text =
                                "System Language (Unity)";

                            public const string Tooltip =
                                "The System Language value represented by this locale. System Language is a Unity type.";
                        }

                        public static class Default
                        {
                            public const string Text =
                                "Set as default locale";

                            public const string Tooltip =
                                "This locale will be used as the default locale for the system starting up. Only one locale can be configured to be the default.";
                        }
                        
                        public static class Notes
                        {
                            public const string Text =
                                "Notes";

                            public const string Tooltip =
                                "Information about this locale that should be recorded.";
                        }

                        public static class LanguageCode
                        {
                            public const string Text =
                                "Language Code (ISO 639)";

                            public const string Tooltip =
                                "The ISO 639 standardized language code related to this locale.";
                        }

                        public static class CountryCode
                        {
                            public const string Text =
                                "Country Code (ISO 3166)";

                            public const string Tooltip =
                                "The ISO 3166 standardized country code related to this locale.";
                        }
                    }

                    public const string LocaleNotSelectedForLocalization =
                        "Localization - No Locale";

                    public const string GenericNoLocale =
                        "No Locale";

                    public const string NoLocalesConfigured =
                        "No locales configured.";

                    public const string NothingSelected =
                        "Nothing selected.";

                    public const string MultiObjectNotSupported =
                        "Multi-Object editing is not currently supported for locales.";

                    public const string UtilitiesSection =
                        "Locale Utilities";

                    public class IconColumn
                    {
                        public const string Title =
                            "Icon";

                        public const string Tooltip =
                            "This icon appears in both the editor and the run time data.";
                    }

                    public static class CreateFirstLocale
                    {
                        public const string Text =
                            "Go to Locale Editor";

                        public const string Tooltip =
                            "Go to the locale editor to create the first locale.";
                    }
                }
                
                public static class ModelView
                {
                    public const string Label =
                        "Editor";

                    public const string Tooltip =
                        "Add, edit, or remove display keys.";

                    public static class Buttons
                    {
                        public static class Back
                        {
                            public const string GoBackTooltip =
                                "Go back to the previous selection.";
                        }

                        public static class Forward
                        {
                            public const string GoForwardTooltip =
                                "Go forward to the next selection.";
                        }
                    }
                    
                    public static class Common
                    {
                        public const string ActualDisplayKeyNamePreview =
                            "Full Name";

                        public const string ActualDisplayKeyNamePreviewTooltip =
                            "This is the actual name of the display key when all of its parents are combined.";

                        public const string FormattedNamePreview =
                            "Formatted Name";

                        public const string FormattedNamePreviewTooltip =
                            "This is the formatted name of the after applying a regular expression. It's used to generate the full name for runtime reference.";

                        public const string DeselectAll =
                            "Deselect All";

                        public const string DeselectAllTooltip =
                            "Deselect all currently selected items.";

                        public const string ReRunErrorCheckTooltip =
                            "Re-run the error and warning check.";

                        public const string GoToParent =
                            "Go To Parent";

                        public const string SelectionInformationSection =
                            "Information";
                        
                        public const string NothingSelected =
                            "Nothing Selected";

                        public const string MultipleObjectEditingWarning =
                            "Multiple objects cannot be edited at the same time.";
                        
                        public const string Localization =
                            "Localization";
                    }
                    
                    public static class DisplayKeys
                    {
                        public static class Buttons
                        {
                            public static class AddLocalization
                            {
                                public const string Text =
                                    "New Localization";

                                public const string Tooltip =
                                    "Add a localization to this display key.";
                            }
                        }

                        public static class FormFields
                        {
                            public static class DisplayKeyName
                            {
                                public const string Text =
                                    "Display Key Name";

                                public const string Tooltip =
                                    "The name of this display key. It must be unique amongst amongst its siblings.";
                            }

                            // NOTE: this is unused as it's currently up for debate whether or not display keys 
                            // should receive a notes field/section.
                            
                            public static class Notes
                            {
                                public const string Text =
                                    "Notes";

                                public const string Tooltip =
                                    "Keep track of information surrounding this display key.";
                            }
                        }

                        public static class Sections
                        {
                            public static class Localizations
                            {
                                public const string Text =
                                    "Localizations";

                                public const string Tooltip =
                                    "All localizations contained within this display key.";
                            }
                        }
                    }
                    
                    public static class Filter
                    {
                        public const string Text =
                            "Filter";

                        public const string Tooltip =
                            "Filter all groups, display keys, and localizations by name.";

                        public const string ClearTooltip =
                            "Clear filter";
                    }

                    public static class Groups
                    {
                        public const string ChildGroupsHidden =
                            "Child Groups Hidden";

                        public const string NoChildGroups =
                            "No Child Groups";

                        public const string DisplayKeysHidden =
                            "Display Keys Hidden";

                        public const string NoDisplayKeys =
                            "No Display Keys";
                        
                        public static class Buttons
                        {
                            public static class AddChildGroup
                            {
                                public const string Text =
                                    "Add Child Group";

                                public const string Tooltip =
                                    "Add a child group to this group.";
                            }

                            public static class AddDisplayKey
                            {
                                public const string Text =
                                    "New Display Key";

                                public const string Tooltip =
                                    "Add a new display key to this group to begin working on localizations.";
                            }
                            
                            public static class AddNewGroup
                            {
                                public const string Text =
                                    "New Group";
                                
                                public const string Tooltip =
                                    "Add a new group to help organize display keys and localizations.";
                            }
                            
                            public static class DeleteSelection
                            {
                                public const string Text =
                                    "Delete";
                                
                                public const string Tooltip =
                                    "Delete the selection.";
                            }
                            
                            public static class Duplicate
                            {
                                public const string Text =
                                    "Duplicate";
                                
                                public const string Tooltip =
                                    "Duplicate the selection.";
                            }
                        }

                        public static class FormFields
                        {
                            public static class GroupName
                            {
                                public const string Text =
                                    "Group Name";

                                public const string Tooltip =
                                    "The name of this group. It must be unique amongst amongst its siblings.";
                            }

                            public static class Notes
                            {
                                public const string Text =
                                    "Notes";

                                public const string Tooltip =
                                    "Keep track of information surrounding this group.";
                            }
                        }

                        public static class Sections
                        {
                            public static class ChildGroups
                            {
                                public const string Text =
                                    "Child Groups";

                                public const string Tooltip =
                                    "Child groups (or \"sub groups\") owned by this group.";
                            }

                            public static class DisplayKeys
                            {
                                public const string Text =
                                    "Display Keys";

                                public const string Tooltip =
                                    "Display keys owned by this group.";
                            }
                        }
                    }
                    
                    public static class Localizations
                    {
                        public static class AudioClip
                        {
                            public const string Text =
                                "Audio Clip";

                            public const string Tooltip =
                                "An audio clip to associate with this localization.";
                        }

                        public static class Texture
                        {
                            public const string Text =
                                "Texture";

                            public const string Tooltip =
                                "A texture to associate with this localization.";
                        }

                        public static class UnityObject
                        {
                            public const string Text =
                                "Unity Object";

                            public const string Tooltip =
                                "A generic UnityEngine.Object to associate with this localization.";
                        }
                        
                        public static class Buttons
                        {
                            public static class AddLocalization
                            {
                                public const string Text =
                                    "Add Localization";
                                
                                public const string Tooltip =
                                    "Add a localization to this display key.";
                            }
                        }

                        public static class Locale
                        {
                            public const string Text =
                                "Locale";

                            public const string Tooltip =
                                "The locale used by this localization.";
                        }

                        public const string LocalizationsHidden =
                            "Localizations Hidden";

                        public const string NoLocalizations =
                            "No Localizations";
                    }

                    public static class Notes
                    {
                        public const string Text =
                            "Notes";

                        public const string Tooltip =
                            "Information about this group that should be recorded.";
                    }

                    public static class Logging
                    {
                        public const string RanErrorWarningCheck =
                            "TongueTwister finished running error/warning check on all models.";
                    }
                }

                public static class Tools
                {
                    public const string AllToolsLoadedCount =
                        "{0} tool(s) loaded.";
                    
                    public const string Label =
                        "Tools";

                    public const string Tooltip =
                        "Utility features for Tongue Twister and development.";

                    public const string NothingSelected =
                        "Nothing selected.";

                    public static class Filter
                    {
                        public const string Text =
                            "Filter";

                        public const string Tooltip =
                            "Filter all tools by name.";
                    }
                }
                
                public static class Home
                {
                    public const string Label =
                        "Home";

                    public const string Tooltip =
                        "View documentation information or get help with Tongue Twister.";

                    public static class Sections
                    {
                        public static class Version
                        {
                            public const string VersionLabel =
                                "You are currently using version: ";
                            
                            public const string VersionText = 
                                "V1.3";
                        }
                        
                        public static class About
                        {
                            public const string Title =
                                "About";

                            public const string Description =
                                "Information about Tongue Twister";

                            public const string ExtendedDescription =
                                "TongueTwister is a powerful localization tool for Unity that offers an easy way to manage the locales and languages used by your project. As of right now, TongueTwister does not translate between localizations.\n\nThanks for supporting the project, please consider leaving a review/feedback!";
                        }

                        public static class Documentation
                        {
                            public const string Title =
                                "Documentation";

                            public const string Description =
                                "View usage guides and detailed information about the API.";

                            public static class Discord
                            {
                                public const string Title =
                                    "Discord Server";

                                public const string Tooltip =
                                    "Join the discussion! Get help, request features, socialize!";

                                public const string URL =
                                    "https://discord.gg/zpJJznH";
                            }

                            public static class OnlineDocumentation
                            {
                                public const string Title =
                                    "Online Documentation";

                                public const string Tooltip =
                                    "View the online documentation to learn about more about extending Tongue Twister or some of its advanced usage.";

                                public const string URL =
                                    "https://www.austephner.com/tongue-twister/index.html";
                            }

                            public static class TrelloBugs
                            {
                                public const string Title =
                                    "Trello Bug Reporting";

                                public const string Tooltip =
                                    "Report bugs or view the status of current fixes.";

                                public const string URL =
                                    "https://trello.com/b/GBl9g5MY/bugs";
                            }

                            public static class TrelloRoadMap
                            {
                                public const string Title =
                                    "Trello Board";

                                public const string Tooltip =
                                    "Track progress of new features and changes on Trello.";

                                public const string URL =
                                    "https://trello.com/b/T8TsJMl1/tonguetwister";
                            }
                        }

                        public static class EditorControls
                        {
                            public const string Title =
                                "Editor Controls";

                            public const string Description =
                                "Hotkeys to improve the speed of your workflow.";

                            public const string ControlsExplanation =
                                "* These controls only work for the navigator panel while in editor mode. They require you to have some selection of groups, display keys, and/or localizations.";
                        }
                    }
                }
            }
            
            public static class Errors
            {
                public const string CannotAddNewModelNothingSelected =
                    "Cannot add a localization when no display key has been selected.";

                public const string NoLocalizationManagerSelected =
                    "Please select a LocalizationManager to continue.";

                public const string FailedToGetAssetGuidFor =
                    "Failed to get asset GUID for {0}.";

                public const string FailedToDrawDefaultSettings =
                    "Failed to draw default settings section, reason: {0}";

                public const string FailedToDrawWindowModeButtonGroup =
                    "Failed to draw window mode button group, reason: {0}";

                public const string FailedToDrawWindowArea =
                    "Failed to draw window area, reason: {0}";
            }

            public static class General
            {
                public const string Title =
                    "Localization Manager";

                public const string Tooltip =
                    "Set the LocalizationManager object to edit in this window.";
            }

            public static class Warnings
            {
                public const string NothingWasCopied =
                    "Nothing was copied!";
            }
        }

        public static class LocalizationManagerEditor
        {
            public static class Sections
            {
                public static class Debug
                {
                    public const string Text =
                        "Debug Options (In-Game)";

                    public const string Tooltip =
                        "View in-game debug options.";

                    public static class DebugWindowId
                    {
                        public const string Label =
                            "Debug Window ID";

                        public const string Tooltip =
                            "The debug window's ID.";
                    }

                    public static class EnableDebugWindow
                    {
                        public const string Label =
                            "Enable the in-game debug window.";

                        public const string Tooltip =
                            "Enables a very basic in-game debug window that can only be seen during playback or runtime mode.";
                    }
                }
            }
            
            public static class Buttons
            {
                public static class Open
                {
                    public const string Text =
                        "Open TongueTwister Window";

                    public const string Tooltip =
                        "Edit this LocalizationManager within the TongueTwister window.";
                }
            }
        }

        public static class Model
        {
            public static class Errors
            {
                public const string SameNameAsSibling =
                    "This {0}'s name is not unique, it is shared with {1} other sibling(s).";

                public const string EmptyName =
                    "Name cannot be empty.";

                public const string LocaleAlreadyInUse =
                    "This localization shares the same locale selection with {0} other localization(s) in this display key.";

                public const string ChildErrors =
                    "There are errors in the child objects.";
            }

            public static class Warnings
            {
                public const string CompilerWarningNameDoesNotStartWithLetter =
                    "C# Compiler Warning: Formatted name does not start with a letter.";

                public const string CompilerWarningNameIsSameAsParent =
                    "C# Compiler Warning: Formatted name is same as parent.";

                public const string ChildWarnings =
                    "There are warnings in the child objects.";

                public const string AdditionalLocalizationContentWarning =
                    "Identifiers shared between additional localized content items makes it more difficult to access them. Duplicate name detected: {0}";
            }
        }

        public static class LocalizationCodeWindow
        {
            public const string Title =
                "ISO Localization Codes";

            public const string TypeSelector =
                "Code Type";
        }
    }
}