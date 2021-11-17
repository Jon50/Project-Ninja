using System.Collections.Generic;
using UnityEngine;

public static class BoolExtension
{
    public static bool DoesNotContain<T>( this List<T> list, T item ) => !list.Contains(item);

    public static bool NotTaggedAs( this Collider2D collider, string tag ) => !collider.CompareTag(tag);

    public static bool IsNull( this object instance )
    {
        if(instance == null)
            return true;
        return false;
    }

    public static bool IsNotNull( this object instance )
    {
        if(instance != null)
            return true;
        return false;
    }

    public static bool IsAnyNull( params object[] objs )
    {
        foreach(var obj in objs)
        {
            var check = obj == null;
            if(obj == null)
                return true;
        }

        return false;
    }
}