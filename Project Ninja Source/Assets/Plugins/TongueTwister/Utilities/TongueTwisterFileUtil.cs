using System;
using System.Linq;
using UnityEngine;

namespace TongueTwister.Utilities
{
    /// <summary>
    /// Utility class for easily performing file operations needed by the <see cref="LocalizationManager"/>.
    /// </summary>
    public static class TongueTwisterFileUtil
    {
        /// <summary>
        /// Gets all files from a directory.
        /// </summary>
        /// <param name="directory">The directory to search in.</param>
        /// <param name="searchPattern"> The search string to match against the names of files in path. This parameter
        /// can contain a combination of valid literal path and wildcard (* and ?) characters (see Remarks), but doesn't
        /// support regular expressions.</param>
        /// <returns>All files matching the search pattern from the given directory.</returns>
        public static string[] GetFiles(string directory, string searchPattern)
        {
            try
            {
                return string.IsNullOrWhiteSpace(searchPattern) 
                    ? System.IO.Directory.GetFiles(directory)
                    : System.IO.Directory.GetFiles(directory, string.Join(",", searchPattern));
            }
            catch (Exception exception)
            {
                Debug.LogError($"Couldn't get files from directory, reason: {exception.Message}");
                return new string[0];
            }
        }

        /// <summary>
        /// Removes the file name from a file path, leaving the directory the file is contained in.
        /// </summary>
        /// <param name="filePath">The file path to split apart.</param>
        /// <param name="directorySplitter">The character used to split the path.</param>
        /// <returns>The containing folder path.</returns>
        public static string RemoveFileNameFromPath(string filePath, char directorySplitter = '/')
        {
            var split = filePath.Split(directorySplitter);
            split = split.Take(split.Length - 1).ToArray();
            return string.Join(directorySplitter.ToString(), split);
        }
    }
}