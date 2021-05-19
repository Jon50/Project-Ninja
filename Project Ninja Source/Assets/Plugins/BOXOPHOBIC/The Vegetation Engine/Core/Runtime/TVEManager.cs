// Cristian Pop - https://boxophobic.com/

using UnityEngine;
using Boxophobic.StyledGUI;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

namespace TheVegetationEngine
{
    [ExecuteInEditMode]
    public class TVEManager : StyledMonoBehaviour
    {
        public static TVEManager Instance;

        [StyledBanner(0.890f, 0.745f, 0.309f, "The Vegetation Engine", "", "https://docs.google.com/document/d/145JOVlJ1tE-WODW45YoJ6Ixg23mFc56EnB_8Tbwloz8/edit#heading=h.hbq3w8ae720x")]
        public bool styledBanner;

        int assetVersion = 300;
        [HideInInspector]
        public int userVersion;

        [HideInInspector]
        public bool isInitialized = false;

        [HideInInspector]
        public TVEGlobalMotion globalMotion;
        [HideInInspector]
        public TVEGlobalSeasons globalSeasons;
        [HideInInspector]
        public TVEGlobalOverlay globalOverlay;
        [HideInInspector]
        public TVEGlobalWetness globalWetness;
        [HideInInspector]
        public TVEGlobalDetails globalDetails;
        [HideInInspector]
        public TVEGlobalSettings globalSettings;
        [HideInInspector]
        public TVEGlobalVolume globalVolume;

#if !UNITY_2019_3_OR_NEWER
        [StyledSpace(5)]
        public bool styledSpace0;
#endif

        void OnEnable()
        {
            Instance = this;

            if (globalMotion == null)
            {
                GameObject go = new GameObject();

                go.AddComponent<MeshFilter>();
                go.GetComponent<MeshFilter>().mesh = Resources.Load<Mesh>("ArrowMesh");

                go.AddComponent<MeshRenderer>();
                go.GetComponent<MeshRenderer>().sharedMaterial = Resources.Load<Material>("ArrowMotion");

                go.AddComponent<TVEGlobalMotion>();

                SetParent(go);

                go.transform.localPosition = new Vector3(0, 2f, 0);

                globalMotion = go.GetComponent<TVEGlobalMotion>();
            }
            else
            {
                globalMotion.enabled = true;
            }

            if (globalSeasons == null)
            {
                GameObject go = new GameObject();
                go.AddComponent<TVEGlobalSeasons>();
                SetParent(go);

                globalSeasons = go.GetComponent<TVEGlobalSeasons>();
            }
            else
            {
                globalSeasons.enabled = true;
            }

            if (globalOverlay == null)
            {
                GameObject go = new GameObject();
                go.AddComponent<TVEGlobalOverlay>();

                SetParent(go);

                globalOverlay = go.GetComponent<TVEGlobalOverlay>();
            }
            else
            {
                globalOverlay.enabled = true;
            }

            if (globalWetness == null)
            {
                GameObject go = new GameObject();
                go.AddComponent<TVEGlobalWetness>();
                SetParent(go);

                globalWetness = go.GetComponent<TVEGlobalWetness>();
            }
            else
            {
                globalWetness.enabled = true;
            }

            if (globalSettings == null)
            {
                GameObject go = new GameObject();
                go.AddComponent<TVEGlobalSettings>();
                SetParent(go);

                globalSettings = go.GetComponent<TVEGlobalSettings>();
            }
            else
            {
                globalSettings.enabled = true;
            }

            if (globalDetails == null)
            {
                GameObject go = new GameObject();
                go.AddComponent<TVEGlobalDetails>();
                SetParent(go);

                globalDetails = go.GetComponent<TVEGlobalDetails>();
            }
            else
            {
                globalDetails.enabled = true;
            }

            if (globalVolume == null)
            {
                GameObject go = new GameObject();
                go.AddComponent<TVEGlobalVolume>();
                SetParent(go);

                globalVolume = go.GetComponent<TVEGlobalVolume>();

                SetDefaultVolumeEntities();
            }
            else
            {
                globalVolume.enabled = true;
            }

            if (isInitialized == false)
            {
                Debug.Log("[The Vegetation Engine] " + "The Vegetation Engine is set in the current scene! Check the Documentation for the next steps!");
                userVersion = assetVersion;
                isInitialized = true;
            }

            if (userVersion < 150)
            {
                UpgradeTo150();
            }

            if (userVersion < 200)
            {
                UpgradeTo200();
            }

            if (userVersion < 210)
            {
                UpgradeTo210();
            }

            if (userVersion < 230)
            {
                UpgradeTo230();
            }

            if (userVersion < 300)
            {
                UpgradeTo300();
            }
        }

        void Start()
        {
            if (userVersion < 150)
            {
                userVersion = 150;
#if UNITY_EDITOR
                Debug.Log("[The Vegetation Engine] The Scene Manager has been ugraded to 1.5.0!");

                if (Application.isPlaying == false)
                {
                    EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                }
#endif
            }

            if (userVersion < 200)
            {
                userVersion = 200;
#if UNITY_EDITOR
                Debug.Log("[The Vegetation Engine] The Scene Manager has been ugraded to 2.0.0!");

                if (Application.isPlaying == false)
                {
                    EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                }
#endif
            }

            if (userVersion < 210)
            {
                userVersion = 210;
#if UNITY_EDITOR
                Debug.Log("[The Vegetation Engine] The Scene Manager has been ugraded to 2.1.0!");

                if (Application.isPlaying == false)
                {
                    EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                }
#endif
            }

            if (userVersion < 230)
            {
                userVersion = 230;
#if UNITY_EDITOR
                if (Application.isPlaying == false)
                {
                    EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                }
#endif
            }

            if (userVersion < 300)
            {
                userVersion = 300;
#if UNITY_EDITOR
                Debug.Log("[The Vegetation Engine] The Scene Manager has been ugraded to 3.0.0!");
                if (Application.isPlaying == false)
                {
                    EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                }
#endif
            }
        }

        void SetParent(GameObject go)
        {
            go.transform.parent = gameObject.transform;
            go.transform.localPosition = Vector3.zero;
            go.transform.eulerAngles = Vector3.zero;
            go.transform.localScale = Vector3.one;
        }

        void SetGlobalVolumeDefaultBuffers()
        {
            globalVolume.volumeBuffers = new List<TVEVolumeData>();
            globalVolume.volumeBuffers.Add(Resources.Load<TVEVolumeData>("Default Colors Buffer"));
            globalVolume.volumeBuffers.Add(Resources.Load<TVEVolumeData>("Default Extras Buffer"));
            globalVolume.volumeBuffers.Add(Resources.Load<TVEVolumeData>("Default Motion Buffer"));
        }

        void SetDefaultVolumeEntities()
        {
            GameObject volumeColors = new GameObject();
            volumeColors.AddComponent<TVEVolume>();
            volumeColors.GetComponent<TVEVolume>().volumeGizmo = new Color(0.890f, 0.745f, 0.309f, 1f);
            volumeColors.GetComponent<TVEVolume>().volumeData = Resources.Load<TVEVolumeData>("Default Colors Buffer");
            volumeColors.name = "Volume (Colors)";
            volumeColors.transform.parent = globalVolume.transform;
            volumeColors.transform.localPosition = Vector3.zero;
            volumeColors.transform.localEulerAngles = Vector3.zero;
            volumeColors.transform.localScale = new Vector3(200, 50, 200);

            GameObject volumeExtras = new GameObject();
            volumeExtras.AddComponent<TVEVolume>();
            volumeExtras.GetComponent<TVEVolume>().volumeGizmo = new Color(0.890f, 0.745f, 0.309f, 1f);
            volumeExtras.GetComponent<TVEVolume>().volumeData = Resources.Load<TVEVolumeData>("Default Extras Buffer");
            volumeExtras.name = "Volume (Extras)";
            volumeExtras.transform.parent = globalVolume.transform;
            volumeExtras.transform.localPosition = Vector3.zero;
            volumeExtras.transform.localEulerAngles = Vector3.zero;
            volumeExtras.transform.localScale = new Vector3(200, 50, 200);

            GameObject volumeMotion = new GameObject();
            volumeMotion.AddComponent<TVEVolume>();
            volumeMotion.GetComponent<TVEVolume>().volumeGizmo = new Color(0.890f, 0.745f, 0.309f, 1f);
            volumeMotion.GetComponent<TVEVolume>().volumeData = Resources.Load<TVEVolumeData>("Default Vertex Buffer");
            volumeMotion.name = "Volume (Vertex)";
            volumeMotion.transform.parent = globalVolume.transform;
            volumeMotion.transform.localPosition = Vector3.zero;
            volumeMotion.transform.localEulerAngles = Vector3.zero;
            volumeMotion.transform.localScale = new Vector3(200, 50, 200);

            globalVolume.volumeEntities = new List<TVEVolume>();
            globalVolume.volumeEntities.Add(volumeColors.GetComponent<TVEVolume>());
            globalVolume.volumeEntities.Add(volumeExtras.GetComponent<TVEVolume>());
            globalVolume.volumeEntities.Add(volumeMotion.GetComponent<TVEVolume>());
        }

        void UpgradeTo150()
        {
            // Update elements to the new save system
            var elements = FindObjectsOfType<TVEElement>();

            for (int i = 0; i < elements.Length; i++)
            {
                var data = elements[i].data;

                if (data != null)
                {
                    if (data.elementShader != null)
                    {
                        var materialData = new TVEElementMaterialData();
                        materialData.props = new List<TVEElementPropertyData>();

                        materialData.shader = data.elementShader;

                        materialData.props.Add(new TVEElementPropertyData(PropertyType.Value, "_ElementIntensity", data.elementIntensity));
                        materialData.props.Add(new TVEElementPropertyData(PropertyType.Value, "_ElementMode", data.elementMode));

                        materialData.props.Add(new TVEElementPropertyData(PropertyType.Texture, "_MainTex", data.mainTex));
                        materialData.props.Add(new TVEElementPropertyData(PropertyType.Vector, "_MainUVs", data.mainUVs));

                        materialData.props.Add(new TVEElementPropertyData(PropertyType.Vector, "_MainColor", data.main));
                        materialData.props.Add(new TVEElementPropertyData(PropertyType.Vector, "_MainColorHDR", data.main));
                        materialData.props.Add(new TVEElementPropertyData(PropertyType.Vector, "_WinterColor", data.winter));
                        materialData.props.Add(new TVEElementPropertyData(PropertyType.Vector, "_SpringColor", data.spring));
                        materialData.props.Add(new TVEElementPropertyData(PropertyType.Vector, "_SummerColor", data.summer));
                        materialData.props.Add(new TVEElementPropertyData(PropertyType.Vector, "_AutumnColor", data.autumn));

                        materialData.props.Add(new TVEElementPropertyData(PropertyType.Value, "_MainValue", data.main.w));
                        materialData.props.Add(new TVEElementPropertyData(PropertyType.Value, "_WinterValue", data.winter.w));
                        materialData.props.Add(new TVEElementPropertyData(PropertyType.Value, "_SpringValue", data.spring.w));
                        materialData.props.Add(new TVEElementPropertyData(PropertyType.Value, "_SummerValue", data.summer.w));
                        materialData.props.Add(new TVEElementPropertyData(PropertyType.Value, "_AutumnValue", data.autumn.w));

                        elements[i].materialData = materialData;
                    }
                }
            }
        }

        void UpgradeTo200()
        {
            if (globalVolume.volumeBuffers == null || globalVolume.volumeBuffers.Count == 0)
            {
                SetGlobalVolumeDefaultBuffers();
            }

            var elements = FindObjectsOfType<TVEElement>();

            for (int i = 0; i < elements.Length; i++)
            {
                var data = elements[i].materialData;

                if (data != null)
                {
                    if (data.props != null)
                    {
                        for (int p = 0; p < data.props.Count; p++)
                        {
                            var prop = data.props[p];

                            if (prop.prop == "_WinterColor")
                            {
                                prop.prop = "_AdditionalColor1";
                            }

                            if (prop.prop == "_SpringColor")
                            {
                                prop.prop = "_AdditionalColor2";
                            }

                            if (prop.prop == "_SummerColor")
                            {
                                prop.prop = "_AdditionalColor3";
                            }

                            if (prop.prop == "_AutumnColor")
                            {
                                prop.prop = "_AdditionalColor4";
                            }

                            if (prop.prop == "_WinterValue")
                            {
                                prop.prop = "_AdditionalValue1";
                            }

                            if (prop.prop == "_SpringValue")
                            {
                                prop.prop = "_AdditionalValue2";
                            }

                            if (prop.prop == "_SummerValue")
                            {
                                prop.prop = "_AdditionalValue3";
                            }

                            if (prop.prop == "_AutumnValue")
                            {
                                prop.prop = "_AdditionalValue4";
                            }
                        }
                    }
                }
            }
        }

        void UpgradeTo210()
        {
            if (GameObject.Find("Global Size Fade") == true)
            {
                DestroyImmediate(GameObject.Find("Global Size Fade"));
            }
        }

        void UpgradeTo230()
        {
            if (globalVolume != null)
            {
                if (globalVolume.volumeBuffers != null)
                {
                    for (int i = 0; i < globalVolume.volumeBuffers.Count; i++)
                    {
                        var oldBuffer = globalVolume.volumeBuffers[i];

                        if (oldBuffer != null)
                        {
                            GameObject volumeEntity = new GameObject();
                            volumeEntity.AddComponent<TVEVolume>();
                            volumeEntity.GetComponent<TVEVolume>().volumeGizmo = new Color(0.890f, 0.745f, 0.309f, 1f);
                            volumeEntity.GetComponent<TVEVolume>().followTransform = globalVolume.followTransform;
                            volumeEntity.GetComponent<TVEVolume>().volumeData = oldBuffer;

                            volumeEntity.transform.parent = globalVolume.transform;
                            volumeEntity.transform.localPosition = Vector3.zero;
                            volumeEntity.transform.localEulerAngles = Vector3.zero;
                            volumeEntity.transform.localScale = new Vector3(globalVolume.transform.lossyScale.x, globalVolume.transform.lossyScale.y, globalVolume.transform.lossyScale.z);

                            if (oldBuffer.bufferType == TVEVolumeData.BufferType.Colors)
                            {
                                volumeEntity.name = "Volume (Colors)";
                            }

                            if (oldBuffer.bufferType == TVEVolumeData.BufferType.Extras)
                            {
                                volumeEntity.name = "Volume (Extras)";
                            }

                            if (oldBuffer.bufferType == TVEVolumeData.BufferType.Vertex)
                            {
                                volumeEntity.name = "Volume (Vertex)";
                            }
                        }
                    }

                    globalVolume.volumeBuffers = null;
                }

                globalVolume.transform.localScale = Vector3.one;
                Debug.Log("[The Vegetation Engine Upgrader] The Global Volume has been ugraded to 2.3.0!");
            }

            if (globalOverlay != null)
            {
                if (globalOverlay.overlayMode == ToggleMode.Off)
                {
                    globalOverlay.overlayIntensity = 0;
                }

                Debug.Log("[The Vegetation Engine Upgrader] The Global Overlay has been ugraded to 2.3.0!");
            }
        }

        void UpgradeTo300()
        {
            if (GameObject.Find("Global Shading") == true)
            {
                DestroyImmediate(GameObject.Find("Global Shading"));
            }
        }
    }
}