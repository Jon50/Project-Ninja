using UnityEditor;

namespace TongueTwister.Editor.Tools.Custom.CSVImporter
{
    /// <summary>
    /// Custom editor implementation for <see cref="CsvImporterSettings"/>. For more detailed documentation it's
    /// recommended to visit the <see cref="CsvImporterSettings"/> class.
    /// </summary>
    [CustomEditor(typeof(CsvImporterSettings))]
    public class CsvImporterSettingsEditor : UnityEditor.Editor
    {
        public string File
        {
            get => _file.stringValue;
            set
            {
                _file.stringValue = value;
                serializedObject.ApplyModifiedProperties();
            }
        }
        
        private SerializedProperty _file => serializedObject.FindProperty("file");
        
        public string CustomCsvDelimiter
        {
            get => _customCsvDelimiter.stringValue;
            set
            {
                _customCsvDelimiter.stringValue = value;
                serializedObject.ApplyModifiedProperties();
            }
        }
        
        private SerializedProperty _customCsvDelimiter => serializedObject.FindProperty("customCsvDelimiter");

        public DelimiterModeType DelimiterMode
        {
            get => (DelimiterModeType) _delimiterMode.intValue;
            set
            {
                _delimiterMode.intValue = (int) value;
                serializedObject.ApplyModifiedProperties();
            }
        }

        private SerializedProperty _delimiterMode => serializedObject.FindProperty("delimiterMode");

        public string GroupDelimiter
        {
            get => _groupDelimiter.stringValue;
            set
            {
                _groupDelimiter.stringValue = value;
                serializedObject.ApplyModifiedProperties();
            }
        }

        private SerializedProperty _groupDelimiter => serializedObject.FindProperty("groupDelimiter");

        public bool CreateNewLocales
        {
            get => _createNewLocales.boolValue;
            set
            {
                _createNewLocales.boolValue = value;
                serializedObject.ApplyModifiedProperties();
            }
        }

        private SerializedProperty _createNewLocales => serializedObject.FindProperty("createNewLocales");

        public CsvFileType FileType
        {
            get => (CsvFileType) _fileType.intValue;
            set
            {
                _fileType.intValue = (int) value;
                serializedObject.ApplyModifiedProperties();
            }
        }
        
        private SerializedProperty _fileType => serializedObject.FindProperty("fileType");

        public LocaleImportType LocaleImportType
        {
            get => (LocaleImportType) _localeImportType.intValue;
            set
            {
                _localeImportType.intValue = (int) value;
                serializedObject.ApplyModifiedProperties();
            }
        }
        
        private SerializedProperty _localeImportType => serializedObject.FindProperty("localeImportType");
    }
}