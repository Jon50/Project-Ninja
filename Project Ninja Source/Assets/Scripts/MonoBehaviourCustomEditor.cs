#if UNITY_EDITOR
using System;
using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace TGM.Custom
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ExposeMethodInEditorAttribute : Attribute { }
}


namespace TGM.Custom
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(MonoBehaviour), editorForChildClasses: true)]
    public class MonoBehaviourCustomEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var type = target.GetType();

            foreach (var method in type.GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
            {
                var attributes = method.GetCustomAttributes(typeof(ExposeMethodInEditorAttribute), inherit: true);
                if (attributes.Length > 0)
                {
                    if (GUILayout.Button("Run: " + method.Name))
                    {
                        ( (MonoBehaviour)target ).Invoke(method.Name, 0f);
                        UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
                    }
                }
            }

        }
    }
}
#endif