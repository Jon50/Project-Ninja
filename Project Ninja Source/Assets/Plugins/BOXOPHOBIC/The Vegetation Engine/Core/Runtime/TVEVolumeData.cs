// Cristian Pop - https://boxophobic.com/

using Boxophobic.StyledGUI;
using UnityEngine;
using UnityEngine.Rendering;

namespace TheVegetationEngine
{
    [CreateAssetMenu(fileName = "New Volume Data", menuName = "BOXOPHOBIC/The Vegetation Engine/Volume Data")]
    public class TVEVolumeData : StyledScriptableObject
    {
        public enum BufferType
        {
            Colors = 10,
            Extras = 20,
            Vertex = 30,
            Custom = 100,
        }

        public enum BufferLayer
        {
            Default = 0,
            Vegetation = 10,
            Grass = 20,
            Objects = 30,
        }

        public enum TextureFormat
        {
            LowDynamicRange = 10,
            HighDynamicRange = 20,
        }

        public enum TextureSizes
        {
            _32 = 32,
            _64 = 64,
            _128 = 128,
            _256 = 256,
            _512 = 512,
            _1024 = 1024,
            _2048 = 2048,
            _4096 = 4096,
        }

        [StyledBanner(0.890f, 0.745f, 0.309f, "Volume Data", "", "https://docs.google.com/document/d/145JOVlJ1tE-WODW45YoJ6Ixg23mFc56EnB_8Tbwloz8/edit#heading=h.wx14v2fugsn4")]
        public bool styledBanner;

        [StyledCategory("Buffer Settings", 5, 10)]
        public bool bufferCat;

        [Tooltip("Set the Buffer name displayed in the Frame Degugger.")]
        public string bufferName = "TVE New Buffer";
        [Tooltip("Controls the Buffer type used to render the elements based on their type.")]
        public BufferType bufferType = BufferType.Colors;
        [Tooltip("Controls the Buffer layer used to render the elements based on their layer.")]
        public BufferLayer bufferLayer = BufferLayer.Default;

        [Space(10)]
        [Tooltip("Controls the Buffer Texture color format.")]
        public TextureFormat textureFormat = TextureFormat.HighDynamicRange;
        [Tooltip("Controls the Buffer Texture resolution.")]
        public TextureSizes textureResolution = TextureSizes._1024;
        [Tooltip("Controls the Buffer Texture wrap mode.")]
        public TextureWrapMode textureWrapMode = TextureWrapMode.Clamp;

        [StyledCategory("Custom Settings")]
        public bool customCat;

        [StyledMessage("Info", "Custom buffers can be used to create special effects such as tire or snow trails. They require the Buffer Type to be set to Custom, custom shaders using the parameters from below and using Custom type elements.", 0, 10)]
        public bool internalCustomMessage = true;

        [Tooltip("Sends a texture globally with the following name when the Buffer type is set to Custom.")]
        public string customBufferTexture = "GlobalTex";
        [Tooltip("Sends a vector globally used as texture coordinate, with the following name used when the Buffer type is set to Custom.")]
        public string customBufferCoords = "GlobalCoords";
        [Tooltip("Sets a background color for the Custom type buffers.")]
        public Color customBufferColor = Color.cyan;

        [StyledCategory("Buffer Preview")]
        public bool previewCat;

        [StyledTexturePreview]
        public RenderTexture internalTex;

        [StyledSpace(5)]
        public bool styledSpace0;

        [HideInInspector]
        public bool isUpdated = false;
        [HideInInspector]
        public int internalResolution;
        [HideInInspector]
        public RenderTextureFormat internalFormat;
        [HideInInspector]
        public CommandBuffer internalBuffer;

        [HideInInspector]
        public Color internalColor;

        [HideInInspector]
        public string internalTexVegetation;
        [HideInInspector]
        public string internalTexGrass;
        [HideInInspector]
        public string internalTexObjects;

        [HideInInspector]
        public string internalCoordVegetation;
        [HideInInspector]
        public string internalCoordGrass;
        [HideInInspector]
        public string internalCoordObjects;

        void OnValidate()
        {
            UpdateVolumeBufferData();
        }

        public void UpdateVolumeBufferData()
        {
            internalResolution = (int)textureResolution;

            if (bufferType != BufferType.Colors)
            {
                textureFormat = TextureFormat.LowDynamicRange;
            }

            if (bufferType == BufferType.Custom)
            {
                bufferLayer = BufferLayer.Default;
            }

            if (textureFormat == TextureFormat.LowDynamicRange)
            {
                internalFormat = RenderTextureFormat.Default;
            }
            else
            {
                internalFormat = RenderTextureFormat.ARGBHalf;
            }

            if (QualitySettings.activeColorSpace == ColorSpace.Linear)
            {
                if (bufferType == BufferType.Colors)
                {
                    internalColor = new Color(0.5f, 0.5f, 0.5f, 0.0f).linear;
                }
                else
                {
                    internalColor = customBufferColor.linear;
                }
            }
            else
            {
                if (bufferType == BufferType.Colors)
                {
                    internalColor = new Color(0.5f, 0.5f, 0.5f, 0.0f);
                }
                else
                {
                    internalColor = customBufferColor;
                }
            }

            var outputTex = "TVE_ColorsTex";
            var outputCoord = "TVE_ColorsCoord";

            if (bufferType == BufferType.Extras)
            {
                outputTex = "TVE_ExtrasTex";
                outputCoord = "TVE_ExtrasCoord";
            }

            if (bufferType == BufferType.Vertex)
            {
                outputTex = "TVE_VertexTex";
                outputCoord = "TVE_VertexCoord";
            }

            if (bufferLayer == BufferLayer.Default)
            {
                internalTexVegetation = outputTex + "_Vegetation";
                internalTexGrass = outputTex + "_Grass";
                internalTexObjects = outputTex + "_Objects";

                internalCoordVegetation = outputCoord + "_Vegetation";
                internalCoordGrass = outputCoord + "_Grass";
                internalCoordObjects = outputCoord + "_Objects";
            }

            if (bufferLayer == BufferLayer.Vegetation)
            {
                internalTexVegetation = outputTex + "_Vegetation";
                internalTexGrass = outputTex + "_Vegetation";
                internalTexObjects = outputTex + "_Vegetation";

                internalCoordVegetation = outputCoord + "_Vegetation";
                internalCoordGrass = outputCoord + "_Vegetation";
                internalCoordObjects = outputCoord + "_Vegetation";
            }

            if (bufferLayer == BufferLayer.Grass)
            {
                internalTexVegetation = outputTex + "_Grass";
                internalTexGrass = outputTex + "_Grass";
                internalTexObjects = outputTex + "_Grass";

                internalCoordVegetation = outputCoord + "_Grass";
                internalCoordGrass = outputCoord + "_Grass";
                internalCoordObjects = outputCoord + "_Grass";
            }

            if (bufferLayer == BufferLayer.Objects)
            {
                internalTexVegetation = outputTex + "_Objects";
                internalTexGrass = outputTex + "_Objects";
                internalTexObjects = outputTex + "_Objects";

                internalCoordVegetation = outputCoord + "_Objects";
                internalCoordGrass = outputCoord + "_Objects";
                internalCoordObjects = outputCoord + "_Objects";
            }

            if (bufferType == BufferType.Custom)
            {
                internalTexVegetation = customBufferTexture;
                internalTexGrass = customBufferTexture;
                internalTexObjects = customBufferTexture;

                internalCoordVegetation = customBufferCoords;
                internalCoordGrass = customBufferCoords;
                internalCoordObjects = customBufferCoords;
            }

            isUpdated = true;
        }
    }
}