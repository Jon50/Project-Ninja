namespace TongueTwister.Utilities
{
    public static class TongueTwisterStringUtility
    {
        /// <summary>
        /// Combines an <see cref="ISO3166Alpha2"/> country code and an <see cref="ISO639Alpha2"/> language code into a
        /// single string.
        /// </summary>
        /// <param name="countryCode"></param>
        /// <param name="languageCode"></param>
        /// <returns></returns>
        public static string CreateCodeString(ISO3166Alpha2 countryCode, ISO639Alpha2 languageCode)
        {
            return $"{countryCode}-{languageCode}";
        }
    }
}