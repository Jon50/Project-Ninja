//Cristian Pop - https://boxophobic.com/

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace TheVegetationEngine
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(TVEVolume))]
    public class TVEInspectorVolume : Editor
    {
        private static readonly string excludeProps = "m_Script";
        private TVEVolume targetScript;

        void OnEnable()
        {
            targetScript = (TVEVolume)target;
        }

        public override void OnInspectorGUI()
        {
            DrawInspector();

            GUILayout.Space(5);

            if (targetScript.followTransform == null)
            {
                GUILayout.BeginHorizontal();
            }

            if (GUILayout.Button(new GUIContent("Select Volume Elements", "Select all the elements rendered by this volume buffer.")))
            {
                if (TVEManager.Instance == null)
                    return;

                var volumeElements = TVEManager.Instance.globalVolume.volumeElements;
                var volumeData = targetScript.volumeData;

                List<Object> elements = new List<Object>();

                if (volumeElements.Count > 0)
                {
                    for (int i = 0; i < volumeElements.Count; i++)
                    {
                        var elementData = volumeElements[i];

                        if ((int)elementData.elementType == (int)volumeData.bufferType)
                        {
                            if ((int)elementData.elementLayer == (int)volumeData.bufferLayer || elementData.elementLayer == ElementLayer.Any)
                            {
                                elements.Add((Object)elementData.gameobject);
                            }
                        }
                    }

                    if (elements.Count > 0)
                    {
                        Object[] elementsArr = new Object[elements.Count];

                        for (int i = 0; i < elements.Count; i++)
                        {
                            elementsArr[i] = elements[i];
                        }

                        Selection.objects = elementsArr;
                    }
                    else
                    {
                        Debug.Log("[The Vegetation Engine] There are no elements rendered by this volume!");
                    }
                }
            }

            if (targetScript.followTransform == null)
            {
                if (GUILayout.Button(new GUIContent("Update Volume Bounds", "Update volume bounds to encapsulate all elements rendered by this volume buffer.")))
                {
                    if (TVEManager.Instance == null)
                        return;

                    var volumeElements = TVEManager.Instance.globalVolume.volumeElements;
                    var volumeData = targetScript.volumeData;
                    var volumeGO = targetScript.gameObject;

                    if (volumeElements.Count > 0)
                    {
                        var updateBounds = false;
                        var firstBound = true;

                        Bounds volumeBounds = new Bounds();

                        for (int i = 0; i < volumeElements.Count; i++)
                        {
                            var elementData = volumeElements[i];

                            if ((int)elementData.elementType == (int)volumeData.bufferType)
                            {
                                if ((int)elementData.elementLayer == (int)volumeData.bufferLayer || elementData.elementLayer == ElementLayer.Any)
                                {
                                    if (firstBound)
                                    {
                                        volumeBounds = elementData.renderer.bounds;
                                        updateBounds = true;
                                        firstBound = false;
                                    }
                                    else
                                    {
                                        volumeBounds.Encapsulate(elementData.renderer.bounds);
                                    }
                                }
                            }
                        }

                        if (updateBounds)
                        {
                            volumeGO.transform.localScale = new Vector3(volumeBounds.size.x / volumeGO.transform.parent.lossyScale.x * 2, volumeGO.transform.localScale.y, volumeBounds.size.z / volumeGO.transform.parent.lossyScale.z * 2);
                            volumeGO.transform.position = volumeBounds.center;
                        }
                        else
                        {
                            Debug.Log("[The Vegetation Engine] There are no elements rendered by this volume!");
                        }
                    }
                }

                if (targetScript.followTransform == null)
                {
                    GUILayout.EndHorizontal();
                }

                GUILayout.Space(5);
            }
        }

        void DrawInspector()
        {
            serializedObject.Update();

            DrawPropertiesExcluding(serializedObject, excludeProps);

            serializedObject.ApplyModifiedProperties();
        }
    }
}


