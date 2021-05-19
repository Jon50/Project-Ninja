using System;
using System.Collections.Generic;
using System.IO;
using TongueTwister.StaticLabels;
using UnityEngine;

namespace TongueTwister
{
    /// <summary>
    /// Represents an array of localized display keys given some <see cref="TongueTwister.LocaleMetadata"/> that it has been paired
    /// with. Useful for serializing a large quantity of data and importing to/from a JSON file.
    /// </summary>
    [Serializable]
    public class LocaleLocalizationImport
    {
        /// <summary>
        /// The serialized locale.
        /// </summary>
        [SerializeField] 
        public LocaleMetadata LocaleMetadata;
        
        /// <summary>
        /// An array of key-value pairs where the key is a display key and the pair is localized text for that display
        /// key.
        /// </summary>
        [SerializeField]
        public DisplayKeyLocalizationImport[] Localizations;

        /// <summary>
        /// Create a <see cref="LocaleLocalizationImport"/> from a JSON file at the given <see cref="filePath"/>.
        /// </summary>
        /// <param name="filePath">A JSON file to deserialize and convert into a <see cref="LocaleLocalizationImport"/>
        /// object.</param>
        /// <returns>A new <see cref="LocaleLocalizationImport"/> based on the contents of a JSON file.</returns>
        public static LocaleLocalizationImport FromFile(string filePath)
        {
            try
            {
                VerifyJsonFile(filePath);
                var fileContent = File.ReadAllText(filePath);
                return JsonUtility.FromJson<LocaleLocalizationImport>(fileContent);
            }
            catch (Exception ex)
            {
                Debug.LogError(
                    string.Format(
                        RuntimeLabels.Errors.JsonLocalizationSet.FailedToLoadLocalizationFromFile,
                        filePath,
                        ex.Message));
                return null;
            }
        }

        /// <summary>
        /// Ensures the given file exists 
        /// </summary>
        /// <param name="file"></param>
        /// <exception cref="Exception"></exception>
        private static void VerifyJsonFile(string file)
        {
            if (!File.Exists(file))
            {
                throw new Exception(
                    string.Format(
                        RuntimeLabels.Errors.JsonLocalizationSet.FileDoesNotExist,
                        file));
            }
        }
    }
}