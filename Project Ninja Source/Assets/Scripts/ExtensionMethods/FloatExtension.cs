using System.Text;
using UnityEngine;

public static class FloatExtension
{
    private static StringBuilder _builder = new StringBuilder();

    public static float LinearToDecibel(this float linear)
    {
        return ( linear != 0 ) ? 20f * Mathf.Log10(linear) : -144f;
    }

    public static string ConvertTimeToString(this float time)
    {
        int minutes = (int)( time / 60 );
        float seconds = time % 60f;

        _builder.AppendFormat("{0:00}", minutes);
        _builder.Append(":");
        _builder.AppendFormat("{0:00.000}", seconds);

        string output = _builder.ToString();
        _builder.Clear();
        return output;
    }

    public static float Clamp(this float value, float min, float max)
    {
        if (value <= min)
            value = min;
        else if (value >= max)
            value = max;

        return value;
    }
}