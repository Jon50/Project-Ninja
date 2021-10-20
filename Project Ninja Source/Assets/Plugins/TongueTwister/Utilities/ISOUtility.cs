using System;

namespace TongueTwister.Utilities
{
    public static class ISOUtility
    {
        /// <summary>
        /// Attempts to parse out a country code and language code matching the standard "CC-LL" format where "CC" is
        /// the 2-letter ISO 3166 (<see cref="ISO3166Alpha2"/>) code and "LL" is the 2-letter ISO 639 (<see cref="ISO639Alpha2"/>)
        /// language code.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="countryCode"></param>
        /// <param name="languageCode"></param>
        public static void ParseCodes(string code, out ISO3166Alpha2 countryCode, out ISO639Alpha2 languageCode)
        {
            var split = code.Split('-');

            countryCode = ISO3166Alpha2.NONE;
            languageCode = ISO639Alpha2.NONE;

            if (split.Length != 2)
            {
                return;
            }

            Enum.TryParse(split[0], true, out countryCode);
            Enum.TryParse(split[1], true, out languageCode);
        }
    }
}