using UnityEngine;

namespace TongueTwister.Editor.Tools.Custom.CSVImporter
{
    /// <summary>
    /// Represents a collection of <see cref="CsvImporterTool"/> settings in the form of a <c>ScriptableObject</c>.
    /// </summary>
    [CreateAssetMenu(menuName = "TongueTwister/CSV Importer/CSV Importer Settings")]
    public class CsvImporterSettings : ScriptableObject
    {
        /// <summary>
        /// The delimiter mode used to separate cells of the CSV data. In most normal cases, this should remain as
        /// <see cref="DelimiterModeType.Comma"/>. In the case of <see cref="DelimiterModeType.Custom"/>, a custom
        /// value must be given to <see cref="customCsvDelimiter"/>.
        /// </summary>
        [SerializeField]
        public DelimiterModeType delimiterMode = DelimiterModeType.Comma;
        
        /// <summary>
        /// The <see cref="CsvFileType"/> represented in this settings object.
        /// </summary>
        [SerializeField]
        public CsvFileType fileType = CsvFileType.None;

        /// <summary>
        /// The string used as a custom delimiter when <see cref="delimiterMode"/> is set to
        /// <see cref="DelimiterModeType.Custom"/>.
        /// </summary>
        [SerializeField] public string customCsvDelimiter = "_";
        
        /// <summary>
        /// The CSV file last used in this settings object.
        /// </summary>
        [SerializeField] public string file = ""; 
        
        /// <summary>
        /// The group delimiter string used to separate imported labels, display keys, dialogue lines, etc. which will
        /// be split by this string phrase and converted into individual groups within the editor.
        /// </summary>
        [SerializeField] public string groupDelimiter = ".";

        /// <summary>
        /// When enabled, a new locale object will be created within the system if one isn't found to match the given
        /// column or row within the CSV file.
        /// </summary>
        [SerializeField] 
        public bool createNewLocales = true;
        
        /// <summary>
        /// The <see cref="localeImportType"/> which determines which algorithm to use for importing locale information
        /// from the CSV file.
        /// </summary>
        [SerializeField]
        public LocaleImportType localeImportType = LocaleImportType.None;
    }
    
    /// <summary>
    /// Describes the CSV importer algorithm specification that determines how information is parsed from CSV files.
    /// </summary>
    public enum LocaleImportType
    {
        /// <summary>
        /// Value has not been set.
        /// </summary>
        None,
        /// <summary>
        /// Locales are represented by a localization code such as "EN-US" for American English or "ES-UK" for
        /// UK English.
        /// </summary>
        LocalizationCode,
        /// <summary>
        /// Locales are represented by their display name such as "American English" or "Mexican Spanish".
        /// </summary>
        DisplayName
    }

    /// <summary>
    /// Specifies how data in a CSV file is organized - currently the CSV importer algorithm only supports two types.
    /// </summary>
    public enum CsvFileType
    {
        /// <summary>
        /// Value has not been set.
        /// </summary>
        None,
        /// <summary>
        /// Every column is a locale except one which represents display keys.
        /// </summary>
        LocaleColumns,
        /// <summary>
        /// Every column is a display key except one which represents locale.
        /// </summary>
        DisplayKeyColumns
    }
}