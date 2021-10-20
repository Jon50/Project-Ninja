namespace TongueTwister.StaticLabels
{
    /// <summary>
    /// Represents a static set of localized text for errors and messages that may occur during runtime.
    /// </summary>
    public static class RuntimeLabels
    {
        public static class Errors
        {
            public static class JsonLocalizationSet
            {
                public const string FailedToLoadLocalizationFromFile =
                    "Failed to create a JSON locale localization set from file: {0}\nReason: {1}";

                public const string FileDoesNotExist =
                    "File does not exist: {0}";

                public const string NotAJsonFile =
                    "File is not a JSON file: {0}";
            }

            public static class LocalizationManager
            {
                public const string NullSetLocale =
                    "Cannot set current locale to null.";

                public const string NoConfiguredLocales =
                    "No configured locales.";
            }
        }
        
        public static class Exceptions
        {
            public const string MissingLocalizationDictionary =
                "Cannot load external localizations. Directory does not exist: {0}";

            public const string NoDisplayKeyWithName =
                "No display key with name \"{0}\" exists.";
            
            public const string NoDisplayKeyWithNameForLocale =
                "No display key with the name \"{0}\" exists for locale \"{1}\".";
            
            public const string NoLocalizationManagerInstance =
                "No LocalizationManager instance is available. Please make sure one is in the scene and enabled.";

            public const string NullLocale =
                "Given locale is null.";

            public const string InvalidDisplayKey =
                "Invalid display key. Neither null or whitespace is allowed.";

            public static class AssetUtility
            {
                public const string AssetNotFound =
                    "No asset found for GUID: {0}";
                
                public const string InvalidGuid =
                    "Invalid GUID: cannot be null, empty, or whitespace.";
            }
        }

        public static class Logging
        {
            public const string LoadedTongueTwisterLocalizationFile =
                "Loaded TongueTwister localization file: {0}";

            public static class Errors
            {
                public const string FailedToGetDataFromTongueTwisterLocalizationFile =
                    "Failed to get localization data from file:\n\"{0}\"\n\nreason:\n{1}";

                public const string InvalidTongueTwisterLocalizationFile =
                    "Invalid JSON localization set file.";

                public const string FailedToDeserializeJsonFile =
                    "Failed to deserialize given JSON file.";
            }
            
            public static class Warnings
            {
                public const string FileInLocalizationDirectoryNotParseable =
                    "File found in the localization directory is not parseable or invalid:\n{0}";

                public static class LocalizationManager
                {
                    public const string NoDefaultLocale =
                        "No default locale has been set or its ID is invalid.";
                }
            }
        }

        public static class ModelNames
        {
            public const string DisplayKey =
                "Display Key";
            
            public const string Group =
                "Group";

            public const string Localization =
                "Localization";
        }
    }
}