using System;
using TongueTwister.StaticLabels;
using UnityEditor;

namespace TongueTwister.Editor.Utilities
{
    /// <summary>
    /// Collection of asset utilities and functionalities that are useful for within the editor.
    /// </summary>
    public static class AssetUtility
    {
        /// <summary>
        /// Gets an asset of type T from the given asset GUID.
        /// </summary>
        /// <param name="guid">The Unity Asset GUID</param>
        /// <typeparam name="T">The type of the asset</typeparam>
        /// <returns>The asset if found.</returns>
        public static T GetAssetFromGuid<T>(string guid) where T : UnityEngine.Object
        {
            if (string.IsNullOrWhiteSpace(guid))
            {
                throw new Exception(RuntimeLabels.Exceptions.AssetUtility.InvalidGuid);
            }

            var assetPath = AssetDatabase.GUIDToAssetPath(guid);
            if (string.IsNullOrWhiteSpace(assetPath))
            {
                throw new Exception(
                    string.Format(
                        RuntimeLabels.Exceptions.AssetUtility.AssetNotFound,
                        guid));
            }

            return AssetDatabase.LoadAssetAtPath<T>(assetPath);
        }

        /// <summary>
        /// Tries to get an asset of type T from the given asset GUID.
        /// </summary>
        /// <param name="guid">The Unity Asset GUID.</param>
        /// <param name="asset">The "out" asset reference.</param>
        /// <typeparam name="T">The type of the asset.</typeparam>
        /// <returns>True if the asset was found, false if not.</returns>
        public static bool TryGetAssetFromGuid<T>(string guid, out T asset) where T : UnityEngine.Object
        {
            try
            {
                asset = GetAssetFromGuid<T>(guid);
                return true;
            }
            catch
            {
                asset = null;
                return false;
            }
        }

        /// <summary>
        /// Gets an asset's GUID.
        /// </summary>
        /// <param name="asset">The asset to get the GUID for</param>
        /// <returns>The asset's GUID</returns>
        public static string GetAssetGuid(UnityEngine.Object asset)
        {
            if (asset && AssetDatabase.TryGetGUIDAndLocalFileIdentifier(asset, out string guid, out long _))
            {
                return guid;
            }
            
            return null;
        }
    }
}