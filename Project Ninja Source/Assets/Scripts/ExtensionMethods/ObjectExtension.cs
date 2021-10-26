public static class ObjectExtension
{
    public static T ReturnDefaultIfNull<T>(this object reference, T defaultValue)
    {
        //if (reference != null)
        //    return reference;
        return defaultValue;
    }
}
