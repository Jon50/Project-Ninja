using System.Collections.Generic;
using UnityEngine;

public static class TransformExtension
{
    public static T[] GetAllOfTheType<T>(this Transform transform)
    {
        var _listOfT = new List<T>();

        AddObjectAndRenderer(transform);

        void AddObjectAndRenderer(Transform currentParent)
        {
            for (int i = 0; i < currentParent.childCount; i++)
            {
                var child = currentParent.GetChild(i);

                if (child.TryGetComponent<T>(out var type))
                {
                    _listOfT.Add(type);
                }

                AddObjectAndRenderer(child);
            }
        }

        return _listOfT.ToArray();
    }
}
