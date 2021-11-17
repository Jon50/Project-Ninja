using System;
using System.Text.RegularExpressions;
using Random = UnityEngine.Random;

public static class StringExtention
{
    public static string SplitCamelCase( this string element ) => Regex.Replace(Regex.Replace(element, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");

    private const int _seed = 404;
    private const int _maxRange = 123;
    public static string GetRandomString( this string element, char seperator = default, int seed = _seed )
    {
        var str1 = seperator == default ? string.Empty : element.Substring(0, element.LastIndexOf(seperator) + 1);
        var str2 = seperator == default ? element : element.Substring(element.LastIndexOf(seperator) + 1);

        Random.InitState(seed);
        var chars = new char[str2.Length];

        for(int i = 0; i < chars.Length; i++)
        {
            var rnd = Random.Range(48, _maxRange);
            var charToInt = Convert.ToInt32(str2[i]) / 10;
            var intToChar = Convert.ToChar(rnd + charToInt);

            if(intToChar < 48 || intToChar > _maxRange || intToChar == 58 || intToChar == 60 || intToChar == 62 || intToChar == 63 || intToChar == 92)
            {
                i--;
                continue;
            }

            chars[i] = intToChar;
        }

        return str1 + new string(chars);
    }
}
