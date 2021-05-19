using TongueTwister.Models;
using TongueTwister.StaticLabels;

namespace TongueTwister.Extensions
{
    public static class ModelTypeExtensions
    {
        /// <summary>
        /// Gets the string name for the given model type.
        /// </summary>
        /// <param name="modelType">The model type to convert to a string.</param>
        /// <returns>The string name for the given model type.</returns>
        public static string GetName(this TongueTwisterModel.ModelType modelType)
        {
            switch (modelType)
            {
                case TongueTwisterModel.ModelType.Group: return RuntimeLabels.ModelNames.Group;
                case TongueTwisterModel.ModelType.DisplayKey: return RuntimeLabels.ModelNames.DisplayKey; 
                case TongueTwisterModel.ModelType.Localization: return RuntimeLabels.ModelNames.Localization;
                default: return null;
            }
        }
    }
}