// Cristian Pop - https://boxophobic.com/

using UnityEngine;
using Boxophobic.StyledGUI;
using UnityEngine.Rendering;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TheVegetationEngine
{
    [ExecuteInEditMode]
    [AddComponentMenu("BOXOPHOBIC/The Vegetation Engine/TVE Global Volume")]
    public class TVEGlobalVolume : StyledMonoBehaviour
    {
        public enum ElementsVisibility
        {
            AlwaysHidden = 0,
            AlwaysVisible = 10,
            HiddenInGame = 20,
        }

        public enum ElementsSorting
        {
            Off = 0,
            On = 10,
        }

        [StyledBanner(0.890f, 0.745f, 0.309f, "Global Volume", "", "https://docs.google.com/document/d/145JOVlJ1tE-WODW45YoJ6Ixg23mFc56EnB_8Tbwloz8/edit#heading=h.a39m1w5ouu94")]
        public bool styledBanner;

        [StyledCategory("Elements Settings", 5, 10)]
        public bool elementsCat;

#if UNITY_EDITOR
        [StyledMessage("Error", "Main Camera not found! Make sure you have a main camera with Main Camera tag in your scene! Particle elements updating will be skipped without it.", 0, 10)]
        public bool styledCameraMessaage = false;

        [StyledMessage("Info", "Realtime Sorting is not supported for elements with GPU Instanceing enabled!", 0, 10)]
        public bool styledSortingMessaage = true;
#endif

        [Tooltip("Controls the elements visibility in scene and game view.")]
        public ElementsVisibility elementsVisibility = ElementsVisibility.HiddenInGame;
        [HideInInspector]
        public ElementsVisibility elementsVisibilityOld = ElementsVisibility.HiddenInGame;

        [Tooltip("Controls the elements sorting by element position. Always on in scene view.")]
        public ElementsSorting elementsSorting = ElementsSorting.Off;

        [Space(10)]
        [Tooltip("Controls the elements fading by camera distance if the Enable Disatnce Fade Support is toggled on the element material.")]
        public float elementsFadeStart = 50;
        [Tooltip("Controls the elements fading by camera distance if the Enable Disatnce Fade Support is toggled on the element material.")]
        public float elementsFadeEnd = 100;

        [StyledCategory("Volume Settings")]
        public bool advancedCat;

        [StyledMessage("Info", "The Volume Buffers allows for more customization for the global elements rendering. Read more about Volume Buffers and Element Layers in the documentation. Enter playmode if the buffer settings are not updated!", 0, 10)]
        public bool styledBuffersMessage = true;

        [Tooltip("List containg all the Volume entities.")]
        public List<TVEVolume> volumeEntities;

        [Space(10)]
        [StyledInteractive("OFF")]
        public bool interactiveOff;

        [Tooltip("List containg all the Element entities.")]
        public List<TVEElementDrawerData> volumeElements;
        [Tooltip("List containg all the Element entities with GPU Instancing enabled.")]
        public List<TVEElementInstancedData> volumeInstanced;

        [StyledInteractive("ON")]
        public bool interactiveOn;

        [StyledSpace(10)]
        public bool styledSpace0;

        Matrix4x4 modelViewMatrix;
        Matrix4x4 projectionMatrix;
        Vector4 bufferParams;

        Camera mainCamera;
        Mesh elementMesh;

        Texture2D internalColorsTex;
        Texture2D internalExtrasTex;
        Texture2D internalMotionTex;

        // Deprecated
        [HideInInspector]
        public List<TVEVolumeData> volumeBuffers;
        [HideInInspector]
        public Transform followTransform;

        void Awake()
        {
            volumeElements = new List<TVEElementDrawerData>();
            volumeInstanced = new List<TVEElementInstancedData>();

            modelViewMatrix = new Matrix4x4
            (
                new Vector4(1f, 0f, 0f, 0f),
                new Vector4(0f, 0f, -1f, 0f),
                new Vector4(0f, -1f, 0f, 0f),
                new Vector4(0f, 0f, 0f, 1f)
            );
        }

        void Start()
        {
            gameObject.name = "Global Volume";
            gameObject.transform.SetSiblingIndex(6);

            GetDefaultGlobalTextures();
            SetDefaultGlobalTextures();

            elementMesh = Resources.Load<Mesh>("QuadMesh");

            ClearRenderBuffers();
            CreateRenderBuffers();

            SortElementObjects();
            SetElementsVisibility();

            if (Application.isPlaying)
            {
                GetInstancedElements();
            }

            GetMaincamera();

#if UNITY_EDITOR
            if (mainCamera == null)
            {
                Debug.Log("[The Vegetation Engine] Main Camera not found! Make sure you have a main camera with Main Camera tag in your scene! Particle elements updating will be skipped without it.");
                styledCameraMessaage = true;
            }
            else
            {
                styledCameraMessaage = false;
            }
#endif
        }

        void Update()
        {
            if (Application.isPlaying == false || elementsSorting == ElementsSorting.On)
            {
                SortElementObjects();
            }

#if UNITY_EDITOR
            if (elementsSorting == ElementsSorting.On)
            {
                styledSortingMessaage = true;
            }
            else
            {
                styledSortingMessaage = false;
            }
#endif


            if (mainCamera == null)
            {
                GetMaincamera();
            }

            if (mainCamera != null)
            {
                UpdateParticleRenderers();
            }

            if (elementsVisibilityOld != elementsVisibility)
            {
                SetElementsVisibility();

                elementsVisibilityOld = elementsVisibility;
            }

#if UNITY_EDITOR
            CheckRenderBuffers();
#endif

            UpdateRenderBuffers();
            ExecuteRenderBuffers();

            SetGlobalShaderParameters();
        }

        void GetMaincamera()
        {
            mainCamera = Camera.main;
        }

        void GetDefaultGlobalTextures()
        {
            if (QualitySettings.activeColorSpace == ColorSpace.Linear)
            {
                internalColorsTex = Resources.Load<Texture2D>("Internal BufferColorTex_sRGB");
            }
            else
            {
                internalColorsTex = Resources.Load<Texture2D>("Internal BufferColorTex");
            }

            internalExtrasTex = Resources.Load<Texture2D>("Internal BufferExtrasTex");
            internalMotionTex = Resources.Load<Texture2D>("Internal BufferMotionTex");
        }

        void SetDefaultGlobalTextures()
        {
            Shader.SetGlobalTexture("TVE_ColorsTex_Vegetation", internalColorsTex);
            Shader.SetGlobalTexture("TVE_ColorsTex_Grass", internalColorsTex);
            Shader.SetGlobalTexture("TVE_ColorsTex_Objects", internalColorsTex);

            Shader.SetGlobalTexture("TVE_ExtrasTex_Vegetation", internalExtrasTex);
            Shader.SetGlobalTexture("TVE_ExtrasTex_Grass", internalExtrasTex);
            Shader.SetGlobalTexture("TVE_ExtrasTex_Objects", internalExtrasTex);

            Shader.SetGlobalTexture("TVE_MotionTex_Vegetation", internalMotionTex);
            Shader.SetGlobalTexture("TVE_MotionTex_Grass", internalMotionTex);
            Shader.SetGlobalTexture("TVE_MotionTex_Objects", internalMotionTex);
        }

        void CreateRenderBuffers()
        {
            for (int i = 0; i < volumeEntities.Count; i++)
            {
                var volumeData = volumeEntities[i].volumeData;

                if (volumeData == null)
                {
                    continue;
                }

                volumeData.internalTex = new RenderTexture(volumeData.internalResolution, volumeData.internalResolution, 0, volumeData.internalFormat);
                volumeData.internalTex.name = volumeData.bufferName;
                volumeData.internalTex.wrapMode = volumeData.textureWrapMode;

                volumeData.internalBuffer = new CommandBuffer();
                volumeData.internalBuffer.name = volumeData.bufferName;

                Shader.SetGlobalTexture(volumeData.internalTexVegetation, volumeData.internalTex);
                Shader.SetGlobalTexture(volumeData.internalTexGrass, volumeData.internalTex);
                Shader.SetGlobalTexture(volumeData.internalTexObjects, volumeData.internalTex);
            }
        }

        void ClearRenderBuffers()
        {
            for (int i = 0; i < volumeEntities.Count; i++)
            {
                var volumeData = volumeEntities[i].volumeData;

                if (volumeData == null)
                {
                    continue;
                }

                if (volumeData.internalTex != null)
                {
                    volumeData.internalTex.Release();
                }

                if (volumeData.internalBuffer != null)
                {
                    volumeData.internalBuffer.Clear();
                }
            }
        }

        void UpdateRenderBuffers()
        {
            for (int i = 0; i < volumeEntities.Count; i++)
            {
                var volumeData = volumeEntities[i].volumeData;

                if (volumeData == null)
                {
                    continue;
                }

                if (volumeData.internalBuffer == null)
                {
                    continue;
                }

                volumeData.internalBuffer.Clear();

                if (volumeData.bufferType == TVEVolumeData.BufferType.Extras)
                {
                    bufferParams = new Vector4(1, Shader.GetGlobalFloat("TVE_WetnessValue"), Shader.GetGlobalFloat("TVE_OverlayValue"), 1);
                    Shader.SetGlobalVector("TVE_ExtrasParams", bufferParams);
                    volumeData.internalBuffer.ClearRenderTarget(true, true, bufferParams);
                }
                else if (volumeData.bufferType == TVEVolumeData.BufferType.Vertex)
                {
                    bufferParams = Shader.GetGlobalVector("TVE_VertexParams");
                    volumeData.internalBuffer.ClearRenderTarget(true, true, bufferParams);
                }
                else
                {
                    Shader.SetGlobalVector("TVE_ColorsParams", volumeData.internalColor);
                    volumeData.internalBuffer.ClearRenderTarget(true, true, volumeData.internalColor);
                }

                for (int e = 0; e < volumeElements.Count; e++)
                {
                    var elementData = volumeElements[e];

                    if ((int)elementData.elementType == (int)volumeData.bufferType)
                    {
                        if ((int)elementData.elementLayer == (int)volumeData.bufferLayer || elementData.elementLayer == ElementLayer.Any)
                        {
                            if (elementData.rendererType == RendererType.Mesh)
                            {
                                volumeData.internalBuffer.DrawMesh(elementData.mesh, elementData.renderer.localToWorldMatrix, elementData.renderer.sharedMaterial);
                            }
                            else
                            {
                                volumeData.internalBuffer.DrawMesh(elementData.mesh, Matrix4x4.identity, elementData.renderer.sharedMaterial);
                            }
                        }
                    }
                }

                if (!Application.isPlaying)
                {
                    continue;
                }

                for (int g = 0; g < volumeInstanced.Count; g++)
                {
                    var elementData = volumeInstanced[g];

                    if ((int)elementData.elementType == (int)volumeData.bufferType)
                    {
                        if ((int)elementData.elementLayer == (int)volumeData.bufferLayer || elementData.elementLayer == ElementLayer.Any)
                        {
                            Matrix4x4[] matrix4X4s = new Matrix4x4[elementData.instancedRenderers.Count];

                            for (int m = 0; m < elementData.instancedRenderers.Count; m++)
                            {
                                matrix4X4s[m] = elementData.instancedRenderers[m].localToWorldMatrix;
                            }

                            volumeData.internalBuffer.DrawMeshInstanced(elementMesh, 0, elementData.instancedMaterial, 0, matrix4X4s);
                        }
                    }
                }
            }
        }

        void ExecuteRenderBuffers()
        {
            GL.PushMatrix();
            RenderTexture currentRenderTexture = RenderTexture.active;

            for (int i = 0; i < volumeEntities.Count; i++)
            {
                var volumeEntity = volumeEntities[i];
                var volumeData = volumeEntities[i].volumeData;

                if (volumeData == null)
                {
                    continue;
                }

                if (volumeData.internalBuffer == null)
                {
                    continue;
                }

                var position = volumeEntity.transform.position;
                var scale = volumeEntity.transform.lossyScale;

                projectionMatrix = Matrix4x4.Ortho(-scale.x / 2 + position.x,
                                                    scale.x / 2 + position.x,
                                                    scale.z / 2 + -position.z,
                                                    -scale.z / 2 + -position.z,
                                                    -scale.y / 2 + position.y,
                                                    scale.y / 2 + position.y);

                var x = 1 / scale.x;
                var y = 1 / scale.z;
                var z = 1 / scale.x * position.x - 0.5f;
                var w = 1 / scale.z * position.z - 0.5f;
                var coord = new Vector4(x, y, -z, -w);

                //Shader.SetGlobalVector("TVE_VolumeCoord", new Vector4(x, y, -z, -w));

                Graphics.SetRenderTarget(volumeData.internalTex);
                GL.LoadProjectionMatrix(projectionMatrix);
                GL.modelview = modelViewMatrix;
                Graphics.ExecuteCommandBuffer(volumeData.internalBuffer);

                Shader.SetGlobalTexture(volumeData.internalTexVegetation, volumeData.internalTex);
                Shader.SetGlobalTexture(volumeData.internalTexGrass, volumeData.internalTex);
                Shader.SetGlobalTexture(volumeData.internalTexObjects, volumeData.internalTex);

                Shader.SetGlobalVector(volumeData.internalCoordVegetation, coord);
                Shader.SetGlobalVector(volumeData.internalCoordGrass, coord);
                Shader.SetGlobalVector(volumeData.internalCoordObjects, coord);
            }

            RenderTexture.active = currentRenderTexture;
            GL.PopMatrix();
        }

        void CheckRenderBuffers()
        {
            if (Application.isPlaying)
            {
                return;
            }

            for (int i = 0; i < volumeEntities.Count; i++)
            {
                var volumeData = volumeEntities[i].volumeData;

                if (volumeData == null)
                {
                    continue;
                }

                if (volumeData.isUpdated)
                {
                    SetDefaultGlobalTextures();
                    ClearRenderBuffers();
                    CreateRenderBuffers();
                    volumeData.isUpdated = false;
                }
            }
        }

        void SetGlobalShaderParameters()
        {
            if (Application.isPlaying)
            {
                Shader.SetGlobalFloat("TVE_ElementsFadeStart", elementsFadeStart);
                Shader.SetGlobalFloat("TVE_ElementsFadeEnd", elementsFadeEnd);
            }
            else
            {
                Shader.SetGlobalFloat("TVE_ElementsFadeStart", 9000);
                Shader.SetGlobalFloat("TVE_ElementsFadeEnd", 10000);
            }
        }

        void UpdateParticleRenderers()
        {
            for (int i = 0; i < volumeElements.Count; i++)
            {
                if (volumeElements[i] != null)
                {
                    if (volumeElements[i].rendererType == RendererType.Particle)
                    {
                        var renderer = (ParticleSystemRenderer)volumeElements[i].renderer;
                        renderer.BakeMesh(volumeElements[i].mesh, true);
                    }
                    else if (volumeElements[i].rendererType == RendererType.Trail)
                    {
                        var renderer = (TrailRenderer)volumeElements[i].renderer;
                        renderer.BakeMesh(volumeElements[i].mesh, true);
                    }
                    //else if (volumeElements[i].rendererType == RendererType.Line)
                    //{
                    //    var renderer = (LineRenderer)volumeElements[i].renderer;
                    //    renderer.BakeMesh(volumeElements[i].mesh, false);
                    //}
                }
            }
        }

        void SortElementObjects()
        {
            for (int i = 0; i < volumeElements.Count - 1; i++)
            {
                for (int j = 0; j < volumeElements.Count - 1; j++)
                {
                    if (volumeElements[j] != null && volumeElements[j].gameobject.transform.position.y > volumeElements[j + 1].gameobject.transform.position.y)
                    {
                        var temp = volumeElements[j + 1];
                        volumeElements[j + 1] = volumeElements[j];
                        volumeElements[j] = temp;
                    }
                }
            }
        }

        void GetInstancedElements()
        {
            if (volumeElements.Count == 0)
            {
                return;
            }

            var existingMaterials = new List<Material>();

            for (int i = 0; i < volumeElements.Count; i++)
            {
                var element = volumeElements[i];
                var material = element.renderer.sharedMaterial;

                if (material.enableInstancing == true && !existingMaterials.Contains(material) && element.rendererType != RendererType.Particle)
                {
                    existingMaterials.Add(material);
                    volumeInstanced.Add(new TVEElementInstancedData(element.elementType, element.elementLayer, material, null));
                }
            }

            for (int i = 0; i < volumeInstanced.Count; i++)
            {
                var renderersList = new List<Renderer>();

                for (int j = 0; j < volumeElements.Count; j++)
                {
                    var instancedMaterial = volumeInstanced[i].instancedMaterial;
                    var elementMaterial = volumeElements[j].renderer.sharedMaterial;

                    if (instancedMaterial == elementMaterial)
                    {
                        renderersList.Add(volumeElements[j].renderer);
                        volumeElements.Remove(volumeElements[j]);
                        j--;
                    }
                }

                volumeInstanced[i].instancedRenderers = renderersList;
            }
        }

        void SetElementsVisibility()
        {
            if (elementsVisibility == ElementsVisibility.AlwaysHidden)
            {
                DisableElementsVisibility();
            }
            else if (elementsVisibility == ElementsVisibility.AlwaysVisible)
            {
                EnableElementsVisibility();
            }
            else if (elementsVisibility == ElementsVisibility.HiddenInGame)
            {
                if (Application.isPlaying)
                {
                    DisableElementsVisibility();
                }
                else
                {
                    EnableElementsVisibility();
                }
            }
        }

        void EnableElementsVisibility()
        {
            for (int i = 0; i < volumeElements.Count; i++)
            {
                if (volumeElements[i] != null)
                {
#if UNITY_2019_3_OR_NEWER
                    volumeElements[i].renderer.forceRenderingOff = false;
#else
                    volumeElements[i].renderer.enabled = true;
#endif
                }
            }
        }

        void DisableElementsVisibility()
        {
            for (int i = 0; i < volumeElements.Count; i++)
            {
                if (volumeElements[i] != null)
                {
#if UNITY_2019_3_OR_NEWER
                    volumeElements[i].renderer.forceRenderingOff = true;
#else
                    volumeElements[i].renderer.enabled = false;
#endif
                }
            }
        }
    }
}
