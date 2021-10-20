using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace TongueTwister.Editor.Utilities
{
    /// <summary>
    /// Provides a simple and easy means for running coroutines in the Unity editor.
    /// </summary>
    [InitializeOnLoad]
    public static class EditorCoroutineHook
    {
        // private static EditorCoroutineHook _hook;
        
        private static List<IEnumerator> _runningEnumerators = new List<IEnumerator>();
        
        static EditorCoroutineHook()
        {
            _runningEnumerators = new List<IEnumerator>();
            EditorApplication.update += Update;
        }

        private static void Update()
        {
            for (int i = 0; i < _runningEnumerators.Count && i >= 0; i++)
            {
                bool finished = false;

                try
                {
                    finished = !_runningEnumerators[i].MoveNext();
                }
                catch (Exception exception)
                {
                    Debug.LogError($"Failed to execute enumerator, reason: {exception.Message}");
                    finished = true;
                }

                if (finished)
                {
                    _runningEnumerators.RemoveAt(i);
                    i--;
                }
            }
        }

        public static void Add(IEnumerator enumerator)
        {
            _runningEnumerators.Add(enumerator);
        }
    }
}