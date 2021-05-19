// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "BOXOPHOBIC/The Vegetation Engine/Vegetation/Bark Standard Lit (Deferred)"
{
	Properties
	{
		[StyledBanner(Bark Standard Lit (Deferred))]_Banner("Banner", Float) = 0
		[StyledCategory(Render Settings, 5, 10)]_RenderingCat("[ Rendering Cat ]", Float) = 0
		[Enum(Opaque,0,Transparent,1)]_RenderMode("Render Mode", Float) = 0
		[Enum(Alpha Blend,0,Premultiply,1)]_RenderBlend("Render Blend", Float) = 0
		[Enum(Off,0,On,1)]_RenderZWrite("Render ZWrite", Float) = 1
		[IntRange]_RenderPriority("Render Priority", Range( -100 , 100)) = 0
		[Enum(Both,0,Back,1,Front,2)]_RenderCull("Render Faces", Float) = 0
		[Enum(Flip,0,Mirror,1,Same,2)]_RenderNormals("Render Normals", Float) = 0
		[Enum(Off,0,On,1)]_RenderSpecular("Render Specular", Float) = 1
		[Enum(Off,0,On,1)][Space(10)]_RenderClip("Alpha Clipping", Float) = 1
		_Cutoff("Alpha Treshold", Range( 0 , 1)) = 0.5
		[StyledSpace(10)]_FadeSpace("# Fade Space", Float) = 0
		_FadeCameraValue("Fade by Camera Distance", Range( 0 , 1)) = 1
		[StyledCategory(Global Settings)]_GlobalCat("[ Global Cat ]", Float) = 0
		[StyledMessage(Warning, Procedural Variation in use. The Variation might not work as expected when switching from one LOD to another., _VertexVariationMode, 1 , 0, 10)]_VariationGlobalsMessage("# Variation Globals Message", Float) = 0
		_GlobalOverlay("Global Overlay", Range( 0 , 1)) = 1
		_GlobalWetness("Global Wetness", Range( 0 , 1)) = 1
		_GlobalSize("Global Size", Range( 0 , 1)) = 1
		[StyledRemapSlider(_ColorsMaskMinValue, _ColorsMaskMaxValue, 0, 1, 10, 0)]_ColorsMaskRemap("Colors Mask", Vector) = (0,0,0,0)
		[StyledRemapSlider(_OverlayMaskMinValue, _OverlayMaskMaxValue, 0, 1, 10, 0)]_OverlayMaskRemap("Overlay Mask", Vector) = (0,0,0,0)
		[HideInInspector]_OverlayMaskMinValue("Overlay Mask Min Value", Range( 0 , 1)) = 0.45
		[HideInInspector]_OverlayMaskMaxValue("Overlay Mask Max Value", Range( 0 , 1)) = 0.55
		_OverlayBottomValue("Overlay Bottom", Range( 0 , 1)) = 0.5
		[HideInInspector]_LocalSize("Local Size", Range( 0 , 1)) = 1
		[StyledCategory(Main Settings)]_MainCat("[ Main Cat ]", Float) = 0
		[NoScaleOffset]_MainAlbedoTex("Main Albedo", 2D) = "white" {}
		[NoScaleOffset]_MainNormalTex("Main Normal", 2D) = "gray" {}
		[NoScaleOffset]_MainMaskTex("Main Mask", 2D) = "white" {}
		[Space(10)]_MainUVs("Main UVs", Vector) = (1,1,0,0)
		[HDR]_MainColor("Main Color", Color) = (1,1,1,1)
		_MainNormalValue("Main Normal", Range( -8 , 8)) = 1
		_MainOcclusionValue("Main Occlusion", Range( 0 , 1)) = 1
		_MainSmoothnessValue("Main Smoothness", Range( 0 , 1)) = 1
		[StyledCategory(Detail Settings)]_DetailCat("[ Detail Cat ]", Float) = 0
		[Enum(Off,0,Overlay,1,Replace,2)]_DetailMode("Detail Mode", Float) = 0
		[Enum(Vertex Paint,0,Projection,1)]_DetailTypeMode("Detail Type", Float) = 0
		[Enum(Standard,0,Packed,1)]_DetailMapsMode("Detail Maps", Float) = 0
		[StyledSpace(10)]_DetailSpace("# Detail Space", Float) = 0
		[NoScaleOffset]_SecondPackedTex("Detail Packed", 2D) = "white" {}
		[NoScaleOffset]_SecondAlbedoTex("Detail Albedo", 2D) = "white" {}
		[NoScaleOffset]_SecondNormalTex("Detail Normal", 2D) = "gray" {}
		[NoScaleOffset]_SecondMaskTex("Detail Mask", 2D) = "white" {}
		[Space(10)]_SecondUVs("Detail UVs", Vector) = (1,1,0,0)
		[HDR]_SecondColor("Detail Color", Color) = (1,1,1,1)
		_SecondNormalValue("Detail Normal", Range( -8 , 8)) = 1
		_SecondOcclusionValue("Detail Occlusion", Range( 0 , 1)) = 1
		_SecondSmoothnessValue("Detail Smoothness", Range( 0 , 1)) = 1
		[Space(10)]_DetailNormalValue("Detail Use Main Normal", Range( 0 , 1)) = 0.5
		[Enum(Main Mask,0,Detail Mask,1)][Space(10)]_DetailMaskMode("Detail Mask Source", Float) = 0
		[Enum(Off,0,On,1)]_DetailMaskInvertMode("Detail Mask Invert", Float) = 0
		_DetailMeshValue("Detail Mask Offset", Range( -1 , 1)) = 0
		[StyledRemapSlider(_DetailBlendMinValue, _DetailBlendMaxValue,0,1)]_DetailBlendRemap("Detail Blending", Vector) = (0,0,0,0)
		[HideInInspector]_DetailBlendMinValue("Detail Blend Min Value", Range( 0 , 1)) = 0.2
		[HideInInspector]_DetailBlendMaxValue("Detail Blend Max Value", Range( 0 , 1)) = 0.3
		[StyledCategory(Occlusion Settings)]_OcclusionCat("[ Occlusion Cat ]", Float) = 0
		[HDR]_VertexOcclusionColor("Vertex Occlusion Color", Color) = (1,1,1,1)
		[StyledRemapSlider(_VertexOcclusionMinValue, _VertexOcclusionMaxValue, 0, 1)]_VertexOcclusionRemap("Vertex Occlusion Mask", Vector) = (0,0,0,0)
		[HideInInspector]_VertexOcclusionMinValue("Vertex Occlusion Min Value", Range( 0 , 1)) = 0
		[HideInInspector]_VertexOcclusionMaxValue("Vertex Occlusion Max Value", Range( 0 , 1)) = 1
		[StyledCategory(Emissive Settings)]_EmissiveCat("[ Emissive Cat]", Float) = 0
		[StyledCategory(Subsurface Settings)]_SubsurfaceCat("[ Subsurface Cat ]", Float) = 0
		[DiffusionProfile]_SubsurfaceDiffusion("Subsurface Diffusion", Float) = 0
		[HideInInspector]_SubsurfaceDiffusion_Asset("Subsurface Diffusion", Vector) = (0,0,0,0)
		[HideInInspector][ASEDiffusionProfile(_SubsurfaceDiffusion)]_SubsurfaceDiffusion_asset("Subsurface Diffusion", Vector) = (0,0,0,0)
		[StyledRemapSlider(_SubsurfaceMaskMinValue, _SubsurfaceMaskMaxValue,0,1)]_SubsurfaceMaskRemap("Subsurface Mask", Vector) = (0,0,0,0)
		[Space(10)]_TranslucencyIntensityValue("Translucency Intensity", Range( 0 , 50)) = 1
		_TranslucencyNormalValue("Translucency Normal", Range( 0 , 1)) = 0.1
		_TranslucencyScatteringValue("Translucency Scattering", Range( 1 , 50)) = 2
		_TranslucencyDirectValue("Translucency Direct", Range( 0 , 1)) = 1
		_TranslucencyAmbientValue("Translucency Ambient", Range( 0 , 1)) = 0.2
		_TranslucencyShadowValue("Translucency Shadow", Range( 0 , 1)) = 1
		[StyledMessage(Warning,  Translucency is not supported in HDRP. Diffusion Profiles will be used instead., 10, 5)]_TranslucencyHDMessage("# Translucency HD Message", Float) = 0
		[StyledCategory(Gradient Settings)]_GradientCat("[ Gradient Cat ]", Float) = 0
		[StyledRemapSlider(_GradientMinValue, _GradientMaxValue, 0, 1)]_GradientMaskRemap("Gradient Mask", Vector) = (0,0,0,0)
		[StyledCategory(Noise Settings)]_NoiseCat("[ Noise Cat ]", Float) = 0
		[StyledRemapSlider(_NoiseMinValue, _NoiseMaxValue, 0, 1)]_NoiseMaskRemap("Noise Mask", Vector) = (0,0,0,0)
		[StyledCategory(Perspective Settings)]_PerspectiveCat("[ Perspective Cat ]", Float) = 0
		[StyledCategory(Size Fade Settings)]_SizeFadeCat("[ Size Fade Cat ]", Float) = 0
		[StyledMessage(Info, The Size Fade feature is recommended to be used to fade out vegetation at a distance in combination with the LOD Groups or with a 3rd party culling system., _SizeFadeMode, 1, 0, 10)]_SizeFadeMessage("# Size Fade Message", Float) = 0
		[Enum(Off,0,On,1)]_SizeFadeMode("Size Fade Mode", Float) = 0
		_SizeFadeStartValue("Size Fade Start", Float) = 200
		_SizeFadeEndValue("Size Fade End", Float) = 300
		[StyledCategory(Motion Settings)]_MotionCat("[ Motion Cat ]", Float) = 0
		[StyledMessage(Info, The Baked pivots feature allows for using per mesh element interaction and elements influence. This feature requires pre baked pivots on prefab conversion. Useful for latge grass meshes., _VertexPivotMode, 1 , 0, 10)]_PivotsMessage("# Pivots Message", Float) = 0
		[StyledSpace(10)]_MotionSpace("# Motion Space", Float) = 0
		[StyledMessage(Warning, Procedural variation in use. Use the Scale settings if the Variation is breaking the bending and rolling animation., _VertexVariationMode, 1 , 0, 10)]_VariationMotionMessage("# Variation Motion Message", Float) = 0
		_MotionAmplitude_10("Bending Amplitude", Range( 0 , 2)) = 0.05
		[IntRange]_MotionSpeed_10("Bending Speed", Range( 0 , 60)) = 2
		_MotionScale_10("Bending Scale", Range( 0 , 20)) = 0
		_MotionVariation_10("Bending Variation", Range( 0 , 20)) = 0
		[Space(10)]_MotionAmplitude_20("Rolling Amplitude", Range( 0 , 2)) = 0.1
		[IntRange]_MotionSpeed_20("Rolling Speed", Range( 0 , 60)) = 6
		_MotionScale_20("Rolling Scale", Range( 0 , 60)) = 0
		_MotionVariation_20("Rolling Variation", Range( 0 , 60)) = 5
		[Space(10)]_InteractionAmplitude("Interaction Amplitude", Range( 0 , 10)) = 1
		_InteractionVariation("Interaction Variation", Range( 0 , 1)) = 0
		[StyledCategory(Advanced Settings)]_AdvancedCat("[ Advanced Cat]", Float) = 0
		[StyledMessage(Info, Use the Batching Support option when the object is statically batched. All vertex calculations are done in world space and features like Baked Pivots and Size options are not supported because the object pivot data is missing with static batching., _VertexDataMode, 1 , 2,10)]_BatchingMessage("# Batching Message", Float) = 0
		[StyledToggle]_VertexDataMode("Enable Batching Support", Float) = 0
		[HideInInspector]_IsTVEShader("_IsTVEShader", Float) = 1
		[HideInInspector]_IsVersion("_IsVersion", Float) = 310
		[HideInInspector]_Color("Legacy Color", Color) = (0,0,0,0)
		[HideInInspector]_MainTex("Legacy MainTex", 2D) = "white" {}
		[HideInInspector]_BumpMap("Legacy BumpMap", 2D) = "white" {}
		[HideInInspector]_MaxBoundsInfo("_MaxBoundsInfo", Vector) = (1,1,1,1)
		[HideInInspector]_vertex_pivot_mode("_vertex_pivot_mode", Float) = 0
		[HideInInspector]_render_normals_options("_render_normals_options", Vector) = (1,1,1,0)
		[HideInInspector]_ObjectOcclusionValue("_ObjectOcclusionValue", Float) = 0
		[HideInInspector]_ObjectSmoothnessValue("_ObjectSmoothnessValue", Float) = 0
		[HideInInspector]_ObjectMetallicValue("_ObjectMetallicValue", Float) = 0
		[HideInInspector]_VertexVariationMode("_VertexVariationMode", Float) = 0
		[HideInInspector]_VertexOcclusionValue("_VertexOcclusionValue", Range( 0 , 8)) = 0
		[HideInInspector]_VertexMasksMode("_VertexMasksMode", Float) = 0
		[HideInInspector]_GlobalSizeFade("_GlobalSizeFade", Range( 0 , 1)) = 0
		[HideInInspector]_GlobalLeaves("_GlobalLeaves", Range( 0 , 1)) = 1
		[HideInInspector]_DetailMaskContrast("_DetailMaskContrast", Range( 0 , 1)) = 0
		[HideInInspector]_LeavesVariationValue("_LeavesVariationValue", Range( 0 , 1)) = 0
		[HideInInspector]_MainMaskMaxValue("_MainMaskMaxValue", Float) = 1
		[HideInInspector]_MainMaskMinValue("_MainMaskMinValue", Float) = 0
		[HideInInspector]_SubsurfaceMaskValue("_SubsurfaceMaskValue", Range( 0 , 1)) = 1
		[HideInInspector][Enum(Main Mask,0,Detail Mask,1)]_MaskMode("_MaskMode", Float) = 0
		[HideInInspector]_OverlayContrast("_OverlayContrast", Float) = 0
		[HideInInspector]_OverlayVariation("_OverlayVariation", Float) = 0
		[HideInInspector]_GrassPerspectiveAngleValue("_GrassPerspectiveAngleValue", Float) = 0
		[HideInInspector]_GrassPerspectiveNoiseValue("_GrassPerspectiveNoiseValue", Float) = 0
		[HideInInspector]_GrassPerspectivePushValue("_GrassPerspectivePushValue", Float) = 0
		[HideInInspector]_NoiseTintOne("_NoiseTintOne", Color) = (1,1,1,1)
		[HideInInspector]_NoiseTintTwo("_NoiseTintTwo", Color) = (1,1,1,1)
		[HideInInspector]_BillboardFadeHValue("_BillboardFadeHValue", Range( 0 , 4)) = 0
		[HideInInspector]_BillboardFadeVValue("_BillboardFadeVValue", Range( 0 , 4)) = 0
		[HideInInspector]_material_batching("_material_batching", Float) = 0
		[HideInInspector]_render_mode("_render_mode", Float) = 0
		[HideInInspector]_render_normals("_render_normals", Float) = 0
		[HideInInspector]_render_blend("_render_blend", Float) = 0
		[HideInInspector]_render_priority("_render_priority", Float) = 0
		[HideInInspector]_render_premul("_render_premul", Float) = 0
		[HideInInspector]_IsBarkShader("_IsBarkShader", Float) = 1
		[HideInInspector]_IsAnyPathShader("_IsAnyPathShader", Float) = 1
		[HideInInspector]_IsStandardShader("_IsStandardShader", Float) = 1
		[HideInInspector]_IsTarget40Shader("_IsTarget40Shader", Float) = 1
		[HideInInspector]_IsBalancedShader("_IsBalancedShader", Float) = 1
		[HideInInspector]_render_cull("_render_cull", Float) = 0
		[HideInInspector]_render_src("_render_src", Float) = 1
		[HideInInspector]_render_dst("_render_dst", Float) = 0
		[HideInInspector]_render_zw("_render_zw", Float) = 1
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull [_render_cull]
		ZWrite [_render_zw]
		Blend [_render_src] [_render_dst]
		
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "UnityStandardUtils.cginc"
		#pragma target 4.0
		#pragma shader_feature_local TVE_ALPHA_CLIP
		#pragma shader_feature_local TVE_VERTEX_DATA_BATCHED
		#pragma shader_feature_local TVE_DETAIL_MODE_OFF TVE_DETAIL_MODE_OVERLAY TVE_DETAIL_MODE_REPLACE
		#pragma shader_feature_local TVE_DETAIL_MAPS_STANDARD TVE_DETAIL_MAPS_PACKED
		#define TVE_USE_VEGETATION_BUFFERS
		  
		#define THE_VEGETATION_ENGINE
		    
		//SHADER INJECTION POINT BEGIN
		//SHADER INJECTION POINT END
		      
		#define TVE_IS_VEGETATION_SHADER
		#if defined(SHADER_API_D3D11) || defined(SHADER_API_XBOXONE) || defined(UNITY_COMPILER_HLSLCC) || defined(SHADER_API_PSSL) || (defined(SHADER_TARGET_SURFACE_ANALYSIS) && !defined(SHADER_TARGET_SURFACE_ANALYSIS_MOJOSHADER))//ASE Sampler Macros
		#define SAMPLE_TEXTURE2D(tex,samplerTex,coord) tex.Sample(samplerTex,coord)
		#define SAMPLE_TEXTURE2D_LOD(tex,samplerTex,coord,lod) tex.SampleLevel(samplerTex,coord, lod)
		#define SAMPLE_TEXTURE2D_BIAS(tex,samplerTex,coord,bias) tex.SampleBias(samplerTex,coord,bias)
		#define SAMPLE_TEXTURE2D_GRAD(tex,samplerTex,coord,ddx,ddy) tex.SampleGrad(samplerTex,coord,ddx,ddy)
		#define SAMPLE_TEXTURE3D(tex,samplerTex,coord) tex.Sample(samplerTex,coord)
		#else//ASE Sampling Macros
		#define SAMPLE_TEXTURE2D(tex,samplerTex,coord) tex2D(tex,coord)
		#define SAMPLE_TEXTURE2D_LOD(tex,samplerTex,coord,lod) tex2Dlod(tex,float4(coord,0,lod))
		#define SAMPLE_TEXTURE2D_BIAS(tex,samplerTex,coord,bias) tex2Dbias(tex,float4(coord,0,bias))
		#define SAMPLE_TEXTURE2D_GRAD(tex,samplerTex,coord,ddx,ddy) tex2Dgrad(tex,coord,ddx,ddy)
		#define SAMPLE_TEXTURE3D(tex,samplerTex,coord) tex3D(tex,coord)
		#endif//ASE Sampling Macros

		#pragma surface surf StandardSpecular keepalpha addshadow fullforwardshadows dithercrossfade vertex:vertexDataFunc 
		struct Input
		{
			float3 worldPos;
			float2 vertexToFrag11_g46049;
			float2 vertexToFrag11_g46058;
			float vertexToFrag11_g46072;
			half ASEVFace : VFACE;
			float3 worldNormal;
			INTERNAL_DATA
			float3 vertexToFrag3890_g46000;
			float3 vertexToFrag11_g46056;
			float vertexToFrag11_g46001;
		};

		uniform half _Banner;
		uniform half _render_dst;
		uniform half _RenderNormals;
		uniform half _VertexOcclusionValue;
		uniform float _ObjectSmoothnessValue;
		uniform half _MotionCat;
		uniform half4 _NoiseTintOne;
		uniform float _GrassPerspectiveNoiseValue;
		uniform half _GlobalSizeFade;
		UNITY_DECLARE_TEX2D_NOSAMPLER(_MainTex);
		SamplerState sampler_MainTex;
		uniform half _RenderCull;
		uniform float4 _MaxBoundsInfo;
		uniform float _ObjectOcclusionValue;
		uniform half _GradientCat;
		uniform half _NoiseCat;
		uniform float4 _SubsurfaceDiffusion_Asset;
		uniform half _BillboardFadeHValue;
		uniform half4 _ColorsMaskRemap;
		uniform half _DetailCat;
		uniform half _Cutoff;
		uniform half _RenderBlend;
		uniform float4 _Color;
		uniform half _VertexVariationMode;
		uniform half _DetailSpace;
		uniform half _PerspectiveCat;
		uniform half _TranslucencyScatteringValue;
		UNITY_DECLARE_TEX2D_NOSAMPLER(_BumpMap);
		SamplerState sampler_BumpMap;
		uniform half4 _SubsurfaceMaskRemap;
		uniform half4 _NoiseTintTwo;
		uniform half _TranslucencyHDMessage;
		uniform float _render_blend;
		uniform float _GrassPerspectivePushValue;
		uniform half _DetailMaskContrast;
		uniform half _TranslucencyIntensityValue;
		uniform half _EmissiveCat;
		uniform half _SizeFadeCat;
		uniform float4 _GradientMaskRemap;
		uniform half _VariationMotionMessage;
		uniform half3 _render_normals_options;
		uniform float4 _NoiseMaskRemap;
		uniform half _GlobalCat;
		uniform half4 _DetailBlendRemap;
		uniform half _DetailMode;
		uniform half _MainCat;
		uniform float _render_priority;
		uniform float _OverlayContrast;
		uniform half _RenderZWrite;
		uniform half _VariationGlobalsMessage;
		uniform half _AdvancedCat;
		uniform half4 _VertexOcclusionRemap;
		uniform half _MainMaskMaxValue;
		uniform float _GrassPerspectiveAngleValue;
		uniform half _SizeFadeMessage;
		uniform half4 _OverlayMaskRemap;
		uniform half _DetailTypeMode;
		uniform half _MaskMode;
		uniform half _RenderingCat;
		uniform half _BillboardFadeVValue;
		uniform half _SubsurfaceCat;
		uniform half _GlobalLeaves;
		uniform half _RenderMode;
		uniform half _TranslucencyDirectValue;
		uniform half _TranslucencyAmbientValue;
		uniform float _ObjectMetallicValue;
		uniform half _DetailMapsMode;
		uniform half _VertexMasksMode;
		uniform half _RenderPriority;
		uniform half _MainMaskMinValue;
		uniform half _OcclusionCat;
		uniform half _LeavesVariationValue;
		uniform float _OverlayVariation;
		uniform half _PivotsMessage;
		uniform half _IsVersion;
		uniform half _RenderClip;
		uniform half _TranslucencyShadowValue;
		uniform half _IsTVEShader;
		uniform half _SubsurfaceMaskValue;
		uniform float _material_batching;
		uniform float4 _SubsurfaceDiffusion_asset;
		uniform half _TranslucencyNormalValue;
		uniform half _BatchingMessage;
		uniform half _FadeSpace;
		uniform float _SubsurfaceDiffusion;
		uniform float _render_mode;
		uniform half _MotionSpace;
		uniform float _render_normals;
		uniform half _render_cull;
		uniform half _IsAnyPathShader;
		uniform half _IsBalancedShader;
		uniform half _IsTarget40Shader;
		uniform half _render_zw;
		uniform half _render_src;
		uniform half _IsStandardShader;
		uniform half _IsBarkShader;
		uniform half _MotionAmplitude_20;
		UNITY_DECLARE_TEX2D_NOSAMPLER(TVE_VertexTex_Vegetation);
		uniform half4 TVE_VertexCoord_Vegetation;
		uniform half _vertex_pivot_mode;
		SamplerState samplerTVE_VertexTex_Vegetation;
		UNITY_DECLARE_TEX2D_NOSAMPLER(TVE_VertexTex_Grass);
		uniform half4 TVE_VertexCoord_Grass;
		SamplerState samplerTVE_VertexTex_Grass;
		UNITY_DECLARE_TEX2D_NOSAMPLER(TVE_VertexTex_Objects);
		uniform half4 TVE_VertexCoord_Objects;
		SamplerState samplerTVE_VertexTex_Objects;
		UNITY_DECLARE_TEX2D_NOSAMPLER(TVE_NoiseTex);
		uniform float2 TVE_NoiseSpeed_Vegetation;
		uniform float2 TVE_NoiseSpeed_Grass;
		uniform half TVE_NoiseSize_Vegetation;
		uniform half TVE_NoiseSize_Grass;
		SamplerState samplerTVE_NoiseTex;
		uniform half _MotionVariation_20;
		uniform half _MotionSpeed_20;
		uniform half _MotionScale_20;
		uniform half _MotionAmplitude_10;
		uniform float _MotionSpeed_10;
		uniform half _MotionVariation_10;
		uniform float _MotionScale_10;
		uniform half _InteractionAmplitude;
		uniform half _InteractionVariation;
		uniform half _VertexDataMode;
		uniform half _GlobalSize;
		uniform half _LocalSize;
		uniform half TVE_DistanceFadeBias;
		uniform half _SizeFadeEndValue;
		uniform half _SizeFadeStartValue;
		uniform half _SizeFadeMode;
		UNITY_DECLARE_TEX2D_NOSAMPLER(_MainNormalTex);
		uniform half4 _MainUVs;
		UNITY_DECLARE_TEX2D_NOSAMPLER(_MainAlbedoTex);
		SamplerState sampler_MainAlbedoTex;
		uniform half _MainNormalValue;
		UNITY_DECLARE_TEX2D_NOSAMPLER(_SecondNormalTex);
		uniform half4 _SecondUVs;
		UNITY_DECLARE_TEX2D_NOSAMPLER(_SecondMaskTex);
		SamplerState sampler_SecondMaskTex;
		uniform half _SecondNormalValue;
		UNITY_DECLARE_TEX2D_NOSAMPLER(_SecondPackedTex);
		uniform half _DetailMeshValue;
		UNITY_DECLARE_TEX2D_NOSAMPLER(_MainMaskTex);
		uniform half _DetailMaskMode;
		uniform half _DetailMaskInvertMode;
		uniform half _DetailBlendMinValue;
		uniform half _DetailBlendMaxValue;
		uniform half _DetailNormalValue;
		uniform half4 _MainColor;
		uniform half4 _SecondColor;
		UNITY_DECLARE_TEX2D_NOSAMPLER(_SecondAlbedoTex);
		uniform half4 TVE_OverlayColor;
		uniform half _OverlayBottomValue;
		uniform half _GlobalOverlay;
		UNITY_DECLARE_TEX2D_NOSAMPLER(TVE_ExtrasTex_Vegetation);
		uniform half4 TVE_ExtrasCoord_Vegetation;
		SamplerState samplerTVE_ExtrasTex_Vegetation;
		UNITY_DECLARE_TEX2D_NOSAMPLER(TVE_ExtrasTex_Grass);
		uniform half4 TVE_ExtrasCoord_Grass;
		SamplerState samplerTVE_ExtrasTex_Grass;
		UNITY_DECLARE_TEX2D_NOSAMPLER(TVE_ExtrasTex_Objects);
		uniform half4 TVE_ExtrasCoord_Objects;
		SamplerState samplerTVE_ExtrasTex_Objects;
		uniform half _OverlayMaskMinValue;
		uniform half _OverlayMaskMaxValue;
		uniform half _render_premul;
		uniform half4 _VertexOcclusionColor;
		uniform half _VertexOcclusionMinValue;
		uniform half _VertexOcclusionMaxValue;
		uniform half _RenderSpecular;
		uniform half _MainSmoothnessValue;
		uniform half _SecondSmoothnessValue;
		uniform half TVE_OverlaySmoothness;
		uniform half _GlobalWetness;
		uniform half _MainOcclusionValue;
		uniform half _SecondOcclusionValue;
		uniform half TVE_CameraFadeStart;
		uniform half TVE_CameraFadeEnd;
		uniform half _FadeCameraValue;
		UNITY_DECLARE_TEX3D_NOSAMPLER(TVE_ScreenTex3D);
		uniform half TVE_ScreenTexCoord;
		SamplerState samplerTVE_ScreenTex3D;


		half4 USE_BUFFERS( half4 Vegetation, half4 Grass, half4 Objects )
		{
			#if defined (TVE_USE_VEGETATION_BUFFERS)
			return Vegetation;
			#elif defined (TVE_USE_GRASS_BUFFERS)
			return Grass;
			#elif defined (TVE_USE_OBJECT_BUFFERS)
			return Objects;
			#else
			return Vegetation;
			#endif
		}


		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_vertex3Pos = v.vertex.xyz;
			float3 PositionOS3588_g46000 = ase_vertex3Pos;
			half3 _Vector1 = half3(0,0,0);
			half3 Mesh_PivotsOS2291_g46000 = _Vector1;
			float3 temp_output_2283_0_g46000 = ( PositionOS3588_g46000 - Mesh_PivotsOS2291_g46000 );
			half3 VertexPos40_g46074 = temp_output_2283_0_g46000;
			float3 appendResult74_g46074 = (float3(0.0 , VertexPos40_g46074.y , 0.0));
			float3 VertexPosRotationAxis50_g46074 = appendResult74_g46074;
			float3 break84_g46074 = VertexPos40_g46074;
			float3 appendResult81_g46074 = (float3(break84_g46074.x , 0.0 , break84_g46074.z));
			float3 VertexPosOtherAxis82_g46074 = appendResult81_g46074;
			half MotionAmplitude203095_g46000 = _MotionAmplitude_20;
			float ObjectData20_g46042 = 3.14;
			float Bounds_Radius121_g46000 = _MaxBoundsInfo.x;
			float WorldData19_g46042 = Bounds_Radius121_g46000;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float staticSwitch14_g46042 = WorldData19_g46042;
			#else
				float staticSwitch14_g46042 = ObjectData20_g46042;
			#endif
			float Motion_Max_Rolling1137_g46000 = staticSwitch14_g46042;
			float4x4 break19_g46012 = unity_ObjectToWorld;
			float3 appendResult20_g46012 = (float3(break19_g46012[ 0 ][ 3 ] , break19_g46012[ 1 ][ 3 ] , break19_g46012[ 2 ][ 3 ]));
			half3 Off19_g46013 = appendResult20_g46012;
			float4 ase_vertex4Pos = v.vertex;
			float4 transform68_g46012 = mul(unity_ObjectToWorld,ase_vertex4Pos);
			float3 appendResult95_g46012 = (float3(v.texcoord.z , 0.0 , v.texcoord.w));
			float4 transform62_g46012 = mul(unity_ObjectToWorld,float4( ( ase_vertex3Pos - ( appendResult95_g46012 * _vertex_pivot_mode ) ) , 0.0 ));
			float3 ObjectPositionWithPivots28_g46012 = ( (transform68_g46012).xyz - (transform62_g46012).xyz );
			half3 On20_g46013 = ObjectPositionWithPivots28_g46012;
			#ifdef TVE_PIVOT_DATA_BAKED
				float3 staticSwitch14_g46013 = On20_g46013;
			#else
				float3 staticSwitch14_g46013 = Off19_g46013;
			#endif
			half3 ObjectData20_g46014 = staticSwitch14_g46013;
			half3 WorldData19_g46014 = Off19_g46013;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float3 staticSwitch14_g46014 = WorldData19_g46014;
			#else
				float3 staticSwitch14_g46014 = ObjectData20_g46014;
			#endif
			float3 temp_output_42_0_g46012 = staticSwitch14_g46014;
			half3 ObjectData20_g46018 = temp_output_42_0_g46012;
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			half3 WorldData19_g46018 = ase_worldPos;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float3 staticSwitch14_g46018 = WorldData19_g46018;
			#else
				float3 staticSwitch14_g46018 = ObjectData20_g46018;
			#endif
			float3 Position83_g46011 = staticSwitch14_g46018;
			half4 Vegetation33_g46017 = SAMPLE_TEXTURE2D_LOD( TVE_VertexTex_Vegetation, samplerTVE_VertexTex_Vegetation, ( (TVE_VertexCoord_Vegetation).zw + ( (TVE_VertexCoord_Vegetation).xy * (Position83_g46011).xz ) ), 0.0 );
			half4 Grass33_g46017 = SAMPLE_TEXTURE2D_LOD( TVE_VertexTex_Grass, samplerTVE_VertexTex_Grass, ( (TVE_VertexCoord_Grass).zw + ( (TVE_VertexCoord_Grass).xy * (Position83_g46011).xz ) ), 0.0 );
			half4 Objects33_g46017 = SAMPLE_TEXTURE2D_LOD( TVE_VertexTex_Objects, samplerTVE_VertexTex_Objects, ( (TVE_VertexCoord_Objects).zw + ( (TVE_VertexCoord_Objects).xy * (Position83_g46011).xz ) ), 0.0 );
			half4 localUSE_BUFFERS33_g46017 = USE_BUFFERS( Vegetation33_g46017 , Grass33_g46017 , Objects33_g46017 );
			half4 Global_Motion_Params3909_g46000 = localUSE_BUFFERS33_g46017;
			float4 break322_g46079 = Global_Motion_Params3909_g46000;
			half Wind_Power369_g46079 = break322_g46079.z;
			float temp_output_7_0_g46081 = 0.5;
			float temp_output_404_0_g46079 = (Wind_Power369_g46079*2.0 + -1.0);
			float temp_output_406_0_g46079 = saturate( sign( temp_output_404_0_g46079 ) );
			float lerpResult401_g46079 = lerp( 0.0 , ( ( Wind_Power369_g46079 - temp_output_7_0_g46081 ) / ( 1.0 - temp_output_7_0_g46081 ) ) , temp_output_406_0_g46079);
			float lerpResult410_g46079 = lerp( 0.2 , 1.0 , lerpResult401_g46079);
			half Wind_Power_203109_g46000 = lerpResult410_g46079;
			half Mesh_Motion_260_g46000 = v.texcoord3.y;
			#ifdef TVE_IS_GRASS_SHADER
				float2 staticSwitch160_g46087 = TVE_NoiseSpeed_Grass;
			#else
				float2 staticSwitch160_g46087 = TVE_NoiseSpeed_Vegetation;
			#endif
			float4x4 break19_g46089 = unity_ObjectToWorld;
			float3 appendResult20_g46089 = (float3(break19_g46089[ 0 ][ 3 ] , break19_g46089[ 1 ][ 3 ] , break19_g46089[ 2 ][ 3 ]));
			half3 Off19_g46090 = appendResult20_g46089;
			float4 transform68_g46089 = mul(unity_ObjectToWorld,ase_vertex4Pos);
			float3 appendResult95_g46089 = (float3(v.texcoord.z , 0.0 , v.texcoord.w));
			float4 transform62_g46089 = mul(unity_ObjectToWorld,float4( ( ase_vertex3Pos - ( appendResult95_g46089 * _vertex_pivot_mode ) ) , 0.0 ));
			float3 ObjectPositionWithPivots28_g46089 = ( (transform68_g46089).xyz - (transform62_g46089).xyz );
			half3 On20_g46090 = ObjectPositionWithPivots28_g46089;
			#ifdef TVE_PIVOT_DATA_BAKED
				float3 staticSwitch14_g46090 = On20_g46090;
			#else
				float3 staticSwitch14_g46090 = Off19_g46090;
			#endif
			half3 ObjectData20_g46091 = staticSwitch14_g46090;
			half3 WorldData19_g46091 = Off19_g46090;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float3 staticSwitch14_g46091 = WorldData19_g46091;
			#else
				float3 staticSwitch14_g46091 = ObjectData20_g46091;
			#endif
			float3 temp_output_42_0_g46089 = staticSwitch14_g46091;
			half3 ObjectData20_g46088 = temp_output_42_0_g46089;
			half3 WorldData19_g46088 = ase_worldPos;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float3 staticSwitch14_g46088 = WorldData19_g46088;
			#else
				float3 staticSwitch14_g46088 = ObjectData20_g46088;
			#endif
			#ifdef TVE_IS_GRASS_SHADER
				float2 staticSwitch164_g46087 = (ase_worldPos).xz;
			#else
				float2 staticSwitch164_g46087 = (staticSwitch14_g46088).xz;
			#endif
			#ifdef TVE_IS_GRASS_SHADER
				float staticSwitch161_g46087 = TVE_NoiseSize_Grass;
			#else
				float staticSwitch161_g46087 = TVE_NoiseSize_Vegetation;
			#endif
			float2 panner73_g46087 = ( _Time.y * staticSwitch160_g46087 + ( staticSwitch164_g46087 * staticSwitch161_g46087 ));
			float4 tex2DNode75_g46087 = SAMPLE_TEXTURE2D_LOD( TVE_NoiseTex, samplerTVE_NoiseTex, panner73_g46087, 0.0 );
			float4 saferPower77_g46087 = max( abs( tex2DNode75_g46087 ) , 0.0001 );
			half Wind_Power2223_g46000 = lerpResult401_g46079;
			float temp_output_167_0_g46087 = Wind_Power2223_g46000;
			float lerpResult168_g46087 = lerp( 1.5 , 0.25 , temp_output_167_0_g46087);
			float4 temp_cast_5 = (lerpResult168_g46087).xxxx;
			float4 break142_g46087 = pow( saferPower77_g46087 , temp_cast_5 );
			half Global_NoiseTex_R34_g46000 = break142_g46087.r;
			float4x4 break19_g46024 = unity_ObjectToWorld;
			float3 appendResult20_g46024 = (float3(break19_g46024[ 0 ][ 3 ] , break19_g46024[ 1 ][ 3 ] , break19_g46024[ 2 ][ 3 ]));
			half3 Off19_g46025 = appendResult20_g46024;
			float4 transform68_g46024 = mul(unity_ObjectToWorld,ase_vertex4Pos);
			float3 appendResult95_g46024 = (float3(v.texcoord.z , 0.0 , v.texcoord.w));
			float4 transform62_g46024 = mul(unity_ObjectToWorld,float4( ( ase_vertex3Pos - ( appendResult95_g46024 * _vertex_pivot_mode ) ) , 0.0 ));
			float3 ObjectPositionWithPivots28_g46024 = ( (transform68_g46024).xyz - (transform62_g46024).xyz );
			half3 On20_g46025 = ObjectPositionWithPivots28_g46024;
			#ifdef TVE_PIVOT_DATA_BAKED
				float3 staticSwitch14_g46025 = On20_g46025;
			#else
				float3 staticSwitch14_g46025 = Off19_g46025;
			#endif
			half3 ObjectData20_g46026 = staticSwitch14_g46025;
			half3 WorldData19_g46026 = Off19_g46025;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float3 staticSwitch14_g46026 = WorldData19_g46026;
			#else
				float3 staticSwitch14_g46026 = ObjectData20_g46026;
			#endif
			float3 temp_output_42_0_g46024 = staticSwitch14_g46026;
			float3 break9_g46024 = temp_output_42_0_g46024;
			half Variation_Complex102_g46022 = frac( ( v.color.r + ( break9_g46024.x + break9_g46024.z ) ) );
			float ObjectData20_g46023 = Variation_Complex102_g46022;
			half Variation_Simple105_g46022 = v.color.r;
			float WorldData19_g46023 = Variation_Simple105_g46022;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float staticSwitch14_g46023 = WorldData19_g46023;
			#else
				float staticSwitch14_g46023 = ObjectData20_g46023;
			#endif
			half Motion_Variation3073_g46000 = staticSwitch14_g46023;
			float temp_output_3154_0_g46000 = ( _MotionVariation_20 * Motion_Variation3073_g46000 );
			float temp_output_116_0_g46085 = temp_output_3154_0_g46000;
			float temp_output_108_0_g46085 = ceil( saturate( ( frac( ( temp_output_116_0_g46085 + 0.3576 ) ) - 0.3 ) ) );
			float mulTime98_g46085 = _Time.y * 0.5;
			float lerpResult110_g46085 = lerp( temp_output_108_0_g46085 , ceil( saturate( ( frac( ( temp_output_116_0_g46085 + 0.1258 ) ) - 0.8 ) ) ) , (sin( mulTime98_g46085 )*0.5 + 0.5));
			float lerpResult118_g46085 = lerp( 0.25 , 0.75 , Wind_Power2223_g46000);
			float lerpResult111_g46085 = lerp( lerpResult110_g46085 , 1.0 , ( lerpResult118_g46085 * lerpResult118_g46085 * lerpResult118_g46085 * lerpResult118_g46085 ));
			half Input_Speed62_g46083 = _MotionSpeed_20;
			float mulTime354_g46083 = _Time.y * Input_Speed62_g46083;
			float Motion_Variation284_g46083 = temp_output_3154_0_g46000;
			float Motion_Scale287_g46083 = ( _MotionScale_20 * ase_worldPos.x );
			half Motion_Rolling138_g46000 = ( ( MotionAmplitude203095_g46000 * Motion_Max_Rolling1137_g46000 ) * ( Wind_Power_203109_g46000 * Mesh_Motion_260_g46000 * Global_NoiseTex_R34_g46000 ) * lerpResult111_g46085 * sin( ( mulTime354_g46083 + Motion_Variation284_g46083 + Motion_Scale287_g46083 ) ) );
			half Angle44_g46074 = Motion_Rolling138_g46000;
			half3 VertexPos40_g46073 = ( VertexPosRotationAxis50_g46074 + ( VertexPosOtherAxis82_g46074 * cos( Angle44_g46074 ) ) + ( cross( float3(0,1,0) , VertexPosOtherAxis82_g46074 ) * sin( Angle44_g46074 ) ) );
			float3 appendResult74_g46073 = (float3(VertexPos40_g46073.x , 0.0 , 0.0));
			half3 VertexPosRotationAxis50_g46073 = appendResult74_g46073;
			float3 break84_g46073 = VertexPos40_g46073;
			float3 appendResult81_g46073 = (float3(0.0 , break84_g46073.y , break84_g46073.z));
			half3 VertexPosOtherAxis82_g46073 = appendResult81_g46073;
			float ObjectData20_g46066 = 3.14;
			float Bounds_Height374_g46000 = _MaxBoundsInfo.y;
			float WorldData19_g46066 = ( Bounds_Height374_g46000 * 3.14 );
			#ifdef TVE_VERTEX_DATA_BATCHED
				float staticSwitch14_g46066 = WorldData19_g46066;
			#else
				float staticSwitch14_g46066 = ObjectData20_g46066;
			#endif
			float Motion_Max_Bending1133_g46000 = staticSwitch14_g46066;
			float lerpResult376_g46079 = lerp( 0.1 , 1.0 , lerpResult401_g46079);
			half Wind_Power_103106_g46000 = lerpResult376_g46079;
			float3 appendResult397_g46079 = (float3(break322_g46079.x , 0.0 , break322_g46079.y));
			float3 temp_output_398_0_g46079 = (appendResult397_g46079*2.0 + -1.0);
			float3 ase_parentObjectScale = (1.0/float3( length( unity_WorldToObject[ 0 ].xyz ), length( unity_WorldToObject[ 1 ].xyz ), length( unity_WorldToObject[ 2 ].xyz ) ));
			float3 temp_output_339_0_g46079 = ( mul( unity_WorldToObject, float4( temp_output_398_0_g46079 , 0.0 ) ).xyz * ase_parentObjectScale );
			half2 Wind_DirectionOS39_g46000 = (temp_output_339_0_g46079).xz;
			half Input_Speed62_g46076 = _MotionSpeed_10;
			float mulTime373_g46076 = _Time.y * Input_Speed62_g46076;
			half Motion_Variation284_g46076 = ( _MotionVariation_10 * Motion_Variation3073_g46000 );
			float2 appendResult344_g46076 = (float2(ase_worldPos.x , ase_worldPos.z));
			float2 Motion_Scale287_g46076 = ( _MotionScale_10 * appendResult344_g46076 );
			half2 Sine_MinusOneToOne281_g46076 = sin( ( mulTime373_g46076 + Motion_Variation284_g46076 + Motion_Scale287_g46076 ) );
			float2 temp_cast_9 = (1.0).xx;
			half Input_Turbulence327_g46076 = Global_NoiseTex_R34_g46000;
			float2 lerpResult321_g46076 = lerp( Sine_MinusOneToOne281_g46076 , temp_cast_9 , Input_Turbulence327_g46076);
			half2 Motion_Bending2258_g46000 = ( ( _MotionAmplitude_10 * Motion_Max_Bending1133_g46000 ) * Wind_Power_103106_g46000 * Wind_DirectionOS39_g46000 * Global_NoiseTex_R34_g46000 * lerpResult321_g46076 );
			float lerpResult402_g46079 = lerp( abs( temp_output_404_0_g46079 ) , 0.0 , temp_output_406_0_g46079);
			half Motion_InteractionMask66_g46000 = lerpResult402_g46079;
			float lerpResult3307_g46000 = lerp( 1.0 , Motion_Variation3073_g46000 , _InteractionVariation);
			half2 Motion_Interaction53_g46000 = ( _InteractionAmplitude * Motion_Max_Bending1133_g46000 * Motion_InteractionMask66_g46000 * Motion_InteractionMask66_g46000 * Wind_DirectionOS39_g46000 * lerpResult3307_g46000 );
			float2 lerpResult109_g46000 = lerp( Motion_Bending2258_g46000 , Motion_Interaction53_g46000 , Motion_InteractionMask66_g46000);
			half Mesh_Motion_182_g46000 = v.texcoord3.x;
			float2 break143_g46000 = ( lerpResult109_g46000 * Mesh_Motion_182_g46000 );
			half Motion_Z190_g46000 = break143_g46000.y;
			half Angle44_g46073 = Motion_Z190_g46000;
			half3 VertexPos40_g46009 = ( VertexPosRotationAxis50_g46073 + ( VertexPosOtherAxis82_g46073 * cos( Angle44_g46073 ) ) + ( cross( float3(1,0,0) , VertexPosOtherAxis82_g46073 ) * sin( Angle44_g46073 ) ) );
			float3 appendResult74_g46009 = (float3(0.0 , 0.0 , VertexPos40_g46009.z));
			half3 VertexPosRotationAxis50_g46009 = appendResult74_g46009;
			float3 break84_g46009 = VertexPos40_g46009;
			float3 appendResult81_g46009 = (float3(break84_g46009.x , break84_g46009.y , 0.0));
			half3 VertexPosOtherAxis82_g46009 = appendResult81_g46009;
			half Motion_X216_g46000 = break143_g46000.x;
			half Angle44_g46009 = -Motion_X216_g46000;
			float3 Vertex_Motion_Object833_g46000 = ( VertexPosRotationAxis50_g46009 + ( VertexPosOtherAxis82_g46009 * cos( Angle44_g46009 ) ) + ( cross( float3(0,0,1) , VertexPosOtherAxis82_g46009 ) * sin( Angle44_g46009 ) ) );
			float3 temp_output_3474_0_g46000 = ( PositionOS3588_g46000 - Mesh_PivotsOS2291_g46000 );
			float3 appendResult2047_g46000 = (float3(Motion_Rolling138_g46000 , 0.0 , -Motion_Rolling138_g46000));
			float3 appendResult2043_g46000 = (float3(Motion_X216_g46000 , 0.0 , Motion_Z190_g46000));
			float3 Vertex_Motion_World1118_g46000 = ( ( temp_output_3474_0_g46000 + appendResult2047_g46000 ) + appendResult2043_g46000 );
			#ifdef TVE_VERTEX_DATA_BATCHED
				float3 staticSwitch3312_g46000 = Vertex_Motion_World1118_g46000;
			#else
				float3 staticSwitch3312_g46000 = ( Vertex_Motion_Object833_g46000 + ( 0.0 * _VertexDataMode ) );
			#endif
			half Global_Vertex_Size174_g46000 = break322_g46079.w;
			float lerpResult346_g46000 = lerp( 1.0 , Global_Vertex_Size174_g46000 , _GlobalSize);
			float temp_output_2626_0_g46000 = ( lerpResult346_g46000 * _LocalSize );
			float3 appendResult3480_g46000 = (float3(temp_output_2626_0_g46000 , temp_output_2626_0_g46000 , temp_output_2626_0_g46000));
			half3 ObjectData20_g46050 = appendResult3480_g46000;
			half3 _Vector11 = half3(1,1,1);
			half3 WorldData19_g46050 = _Vector11;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float3 staticSwitch14_g46050 = WorldData19_g46050;
			#else
				float3 staticSwitch14_g46050 = ObjectData20_g46050;
			#endif
			half3 Vertex_Size1741_g46000 = staticSwitch14_g46050;
			half3 _Vector5 = half3(1,1,1);
			float4x4 break19_g46051 = unity_ObjectToWorld;
			float3 appendResult20_g46051 = (float3(break19_g46051[ 0 ][ 3 ] , break19_g46051[ 1 ][ 3 ] , break19_g46051[ 2 ][ 3 ]));
			half3 Off19_g46052 = appendResult20_g46051;
			float4 transform68_g46051 = mul(unity_ObjectToWorld,ase_vertex4Pos);
			float3 appendResult95_g46051 = (float3(v.texcoord.z , 0.0 , v.texcoord.w));
			float4 transform62_g46051 = mul(unity_ObjectToWorld,float4( ( ase_vertex3Pos - ( appendResult95_g46051 * _vertex_pivot_mode ) ) , 0.0 ));
			float3 ObjectPositionWithPivots28_g46051 = ( (transform68_g46051).xyz - (transform62_g46051).xyz );
			half3 On20_g46052 = ObjectPositionWithPivots28_g46051;
			#ifdef TVE_PIVOT_DATA_BAKED
				float3 staticSwitch14_g46052 = On20_g46052;
			#else
				float3 staticSwitch14_g46052 = Off19_g46052;
			#endif
			half3 ObjectData20_g46053 = staticSwitch14_g46052;
			half3 WorldData19_g46053 = Off19_g46052;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float3 staticSwitch14_g46053 = WorldData19_g46053;
			#else
				float3 staticSwitch14_g46053 = ObjectData20_g46053;
			#endif
			float3 temp_output_42_0_g46051 = staticSwitch14_g46053;
			float temp_output_7_0_g46057 = _SizeFadeEndValue;
			float temp_output_335_0_g46000 = saturate( ( ( ( distance( _WorldSpaceCameraPos , temp_output_42_0_g46051 ) * ( 1.0 / TVE_DistanceFadeBias ) ) - temp_output_7_0_g46057 ) / ( _SizeFadeStartValue - temp_output_7_0_g46057 ) ) );
			float3 appendResult3482_g46000 = (float3(temp_output_335_0_g46000 , temp_output_335_0_g46000 , temp_output_335_0_g46000));
			float3 lerpResult3556_g46000 = lerp( _Vector5 , appendResult3482_g46000 , _SizeFadeMode);
			half3 ObjectData20_g46062 = lerpResult3556_g46000;
			half3 WorldData19_g46062 = _Vector5;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float3 staticSwitch14_g46062 = WorldData19_g46062;
			#else
				float3 staticSwitch14_g46062 = ObjectData20_g46062;
			#endif
			float3 Vertex_SizeFade1740_g46000 = staticSwitch14_g46062;
			half3 Grass_Coverage2661_g46000 = half3(0,0,0);
			float3 Final_VertexPosition890_g46000 = ( ( staticSwitch3312_g46000 * Vertex_Size1741_g46000 * Vertex_SizeFade1740_g46000 ) + Mesh_PivotsOS2291_g46000 + Grass_Coverage2661_g46000 );
			v.vertex.xyz = Final_VertexPosition890_g46000;
			v.vertex.w = 1;
			o.vertexToFrag11_g46049 = ( ( v.texcoord.xy * (_MainUVs).xy ) + (_MainUVs).zw );
			float2 appendResult21_g46005 = (float2(v.texcoord.z , v.texcoord.w));
			float2 Mesh_DetailCoord3_g46000 = appendResult21_g46005;
			o.vertexToFrag11_g46058 = ( ( Mesh_DetailCoord3_g46000 * (_SecondUVs).xy ) + (_SecondUVs).zw );
			half Mesh_DetailMask90_g46000 = v.color.b;
			float temp_output_989_0_g46000 = ( ( Mesh_DetailMask90_g46000 - 0.5 ) + _DetailMeshValue );
			o.vertexToFrag11_g46072 = temp_output_989_0_g46000;
			o.vertexToFrag3890_g46000 = ase_worldPos;
			float3 temp_cast_11 = (1.0).xxx;
			float Mesh_Occlusion318_g46000 = v.color.g;
			float temp_output_7_0_g46045 = _VertexOcclusionMinValue;
			float3 lerpResult2945_g46000 = lerp( (_VertexOcclusionColor).rgb , temp_cast_11 , saturate( ( ( Mesh_Occlusion318_g46000 - temp_output_7_0_g46045 ) / ( _VertexOcclusionMaxValue - temp_output_7_0_g46045 ) ) ));
			float3 Vertex_Occlusion648_g46000 = lerpResult2945_g46000;
			o.vertexToFrag11_g46056 = Vertex_Occlusion648_g46000;
			float temp_output_7_0_g46046 = TVE_CameraFadeStart;
			float saferPower3976_g46000 = max( saturate( ( ( distance( ase_worldPos , _WorldSpaceCameraPos ) - temp_output_7_0_g46046 ) / ( TVE_CameraFadeEnd - temp_output_7_0_g46046 ) ) ) , 0.0001 );
			float temp_output_3976_0_g46000 = pow( saferPower3976_g46000 , _FadeCameraValue );
			o.vertexToFrag11_g46001 = temp_output_3976_0_g46000;
		}

		void surf( Input i , inout SurfaceOutputStandardSpecular o )
		{
			half2 Main_UVs15_g46000 = i.vertexToFrag11_g46049;
			float4 tex2DNode117_g46000 = SAMPLE_TEXTURE2D( _MainNormalTex, sampler_MainAlbedoTex, Main_UVs15_g46000 );
			float2 appendResult88_g46019 = (float2(tex2DNode117_g46000.a , tex2DNode117_g46000.g));
			float2 temp_output_90_0_g46019 = ( (appendResult88_g46019*2.0 + -1.0) * _MainNormalValue );
			float3 appendResult91_g46019 = (float3(temp_output_90_0_g46019 , 1.0));
			half3 Main_Normal137_g46000 = appendResult91_g46019;
			half2 Second_UVs17_g46000 = i.vertexToFrag11_g46058;
			float4 tex2DNode145_g46000 = SAMPLE_TEXTURE2D( _SecondNormalTex, sampler_SecondMaskTex, Second_UVs17_g46000 );
			float2 appendResult88_g46010 = (float2(tex2DNode145_g46000.a , tex2DNode145_g46000.g));
			float2 temp_output_90_0_g46010 = ( (appendResult88_g46010*2.0 + -1.0) * _SecondNormalValue );
			float3 appendResult91_g46010 = (float3(temp_output_90_0_g46010 , 1.0));
			float4 tex2DNode3380_g46000 = SAMPLE_TEXTURE2D( _SecondPackedTex, sampler_SecondMaskTex, Second_UVs17_g46000 );
			half Packed_NormalX3387_g46000 = tex2DNode3380_g46000.a;
			half Packed_NormalY3386_g46000 = tex2DNode3380_g46000.g;
			float2 appendResult88_g46067 = (float2(Packed_NormalX3387_g46000 , Packed_NormalY3386_g46000));
			float2 temp_output_90_0_g46067 = ( (appendResult88_g46067*2.0 + -1.0) * _SecondNormalValue );
			float3 appendResult91_g46067 = (float3(temp_output_90_0_g46067 , 1.0));
			#if defined(TVE_DETAIL_MAPS_STANDARD)
				float3 staticSwitch3450_g46000 = appendResult91_g46010;
			#elif defined(TVE_DETAIL_MAPS_PACKED)
				float3 staticSwitch3450_g46000 = appendResult91_g46067;
			#else
				float3 staticSwitch3450_g46000 = appendResult91_g46010;
			#endif
			half3 Second_Normal179_g46000 = staticSwitch3450_g46000;
			half Blend_Source1540_g46000 = i.vertexToFrag11_g46072;
			float4 tex2DNode35_g46000 = SAMPLE_TEXTURE2D( _MainMaskTex, sampler_MainAlbedoTex, Main_UVs15_g46000 );
			half Main_Mask57_g46000 = tex2DNode35_g46000.b;
			float4 tex2DNode33_g46000 = SAMPLE_TEXTURE2D( _SecondMaskTex, sampler_SecondMaskTex, Second_UVs17_g46000 );
			half Second_Mask81_g46000 = tex2DNode33_g46000.b;
			float lerpResult1327_g46000 = lerp( Main_Mask57_g46000 , Second_Mask81_g46000 , _DetailMaskMode);
			float lerpResult4058_g46000 = lerp( lerpResult1327_g46000 , ( 1.0 - lerpResult1327_g46000 ) , _DetailMaskInvertMode);
			float temp_output_7_0_g46043 = _DetailBlendMinValue;
			half Mask_Detail147_g46000 = saturate( ( ( saturate( ( Blend_Source1540_g46000 + ( Blend_Source1540_g46000 * lerpResult4058_g46000 ) ) ) - temp_output_7_0_g46043 ) / ( _DetailBlendMaxValue - temp_output_7_0_g46043 ) ) );
			float3 lerpResult230_g46000 = lerp( float3( 0,0,1 ) , Second_Normal179_g46000 , Mask_Detail147_g46000);
			float3 lerpResult3372_g46000 = lerp( float3( 0,0,1 ) , Main_Normal137_g46000 , _DetailNormalValue);
			float3 lerpResult3376_g46000 = lerp( Main_Normal137_g46000 , BlendNormals( lerpResult3372_g46000 , Second_Normal179_g46000 ) , Mask_Detail147_g46000);
			#if defined(TVE_DETAIL_MODE_OFF)
				float3 staticSwitch267_g46000 = Main_Normal137_g46000;
			#elif defined(TVE_DETAIL_MODE_OVERLAY)
				float3 staticSwitch267_g46000 = BlendNormals( Main_Normal137_g46000 , lerpResult230_g46000 );
			#elif defined(TVE_DETAIL_MODE_REPLACE)
				float3 staticSwitch267_g46000 = lerpResult3376_g46000;
			#else
				float3 staticSwitch267_g46000 = Main_Normal137_g46000;
			#endif
			float3 temp_output_13_0_g46065 = staticSwitch267_g46000;
			float3 switchResult12_g46065 = (((i.ASEVFace>0)?(temp_output_13_0_g46065):(( temp_output_13_0_g46065 * _render_normals_options ))));
			half3 Blend_Normal312_g46000 = switchResult12_g46065;
			half3 Final_Normal366_g46000 = Blend_Normal312_g46000;
			o.Normal = Final_Normal366_g46000;
			float4 tex2DNode29_g46000 = SAMPLE_TEXTURE2D( _MainAlbedoTex, sampler_MainAlbedoTex, Main_UVs15_g46000 );
			float3 temp_output_3639_0_g46000 = (tex2DNode29_g46000).rgb;
			float3 temp_output_51_0_g46000 = ( (_MainColor).rgb * temp_output_3639_0_g46000 );
			half3 Main_Albedo99_g46000 = temp_output_51_0_g46000;
			half Packed_Albedo3385_g46000 = tex2DNode3380_g46000.r;
			float4 temp_cast_0 = (Packed_Albedo3385_g46000).xxxx;
			#if defined(TVE_DETAIL_MAPS_STANDARD)
				float4 staticSwitch3449_g46000 = SAMPLE_TEXTURE2D( _SecondAlbedoTex, sampler_SecondMaskTex, Second_UVs17_g46000 );
			#elif defined(TVE_DETAIL_MAPS_PACKED)
				float4 staticSwitch3449_g46000 = temp_cast_0;
			#else
				float4 staticSwitch3449_g46000 = SAMPLE_TEXTURE2D( _SecondAlbedoTex, sampler_SecondMaskTex, Second_UVs17_g46000 );
			#endif
			float3 temp_output_126_0_g46000 = (( _SecondColor * staticSwitch3449_g46000 )).rgb;
			half3 Second_Albedo153_g46000 = temp_output_126_0_g46000;
			#ifdef UNITY_COLORSPACE_GAMMA
				float staticSwitch1_g46068 = 2.0;
			#else
				float staticSwitch1_g46068 = 4.594794;
			#endif
			float3 lerpResult235_g46000 = lerp( Main_Albedo99_g46000 , ( Main_Albedo99_g46000 * Second_Albedo153_g46000 * staticSwitch1_g46068 ) , Mask_Detail147_g46000);
			float3 lerpResult208_g46000 = lerp( Main_Albedo99_g46000 , Second_Albedo153_g46000 , Mask_Detail147_g46000);
			#if defined(TVE_DETAIL_MODE_OFF)
				float3 staticSwitch255_g46000 = Main_Albedo99_g46000;
			#elif defined(TVE_DETAIL_MODE_OVERLAY)
				float3 staticSwitch255_g46000 = lerpResult235_g46000;
			#elif defined(TVE_DETAIL_MODE_REPLACE)
				float3 staticSwitch255_g46000 = lerpResult208_g46000;
			#else
				float3 staticSwitch255_g46000 = Main_Albedo99_g46000;
			#endif
			half3 Blend_Albedo265_g46000 = staticSwitch255_g46000;
			half3 Blend_AlbedoTinted2808_g46000 = ( float3(1,1,1) * float3(1,1,1) * float3(1,1,1) * Blend_Albedo265_g46000 );
			half3 Blend_AlbedoColored863_g46000 = Blend_AlbedoTinted2808_g46000;
			half3 Blend_AlbedoAndSubsurface149_g46000 = Blend_AlbedoColored863_g46000;
			half3 Global_OverlayColor1758_g46000 = (TVE_OverlayColor).rgb;
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float3 ase_normWorldNormal = normalize( ase_worldNormal );
			float lerpResult3567_g46000 = lerp( _OverlayBottomValue , 1.0 , ase_normWorldNormal.y);
			half Main_AlbedoTex_G3526_g46000 = tex2DNode29_g46000.g;
			half Second_AlbedoTex_G3581_g46000 = (staticSwitch3449_g46000).g;
			float lerpResult3579_g46000 = lerp( Main_AlbedoTex_G3526_g46000 , Second_AlbedoTex_G3581_g46000 , Mask_Detail147_g46000);
			#if defined(TVE_DETAIL_MODE_OFF)
				float staticSwitch3574_g46000 = Main_AlbedoTex_G3526_g46000;
			#elif defined(TVE_DETAIL_MODE_OVERLAY)
				float staticSwitch3574_g46000 = lerpResult3579_g46000;
			#elif defined(TVE_DETAIL_MODE_REPLACE)
				float staticSwitch3574_g46000 = lerpResult3579_g46000;
			#else
				float staticSwitch3574_g46000 = Main_AlbedoTex_G3526_g46000;
			#endif
			float3 PositionWS_PerVertex3905_g46000 = i.vertexToFrag3890_g46000;
			float3 Position82_g46029 = PositionWS_PerVertex3905_g46000;
			half4 Vegetation33_g46036 = SAMPLE_TEXTURE2D( TVE_ExtrasTex_Vegetation, samplerTVE_ExtrasTex_Vegetation, ( (TVE_ExtrasCoord_Vegetation).zw + ( (TVE_ExtrasCoord_Vegetation).xy * (Position82_g46029).xz ) ) );
			half4 Grass33_g46036 = SAMPLE_TEXTURE2D( TVE_ExtrasTex_Grass, samplerTVE_ExtrasTex_Grass, ( (TVE_ExtrasCoord_Grass).zw + ( (TVE_ExtrasCoord_Grass).xy * (Position82_g46029).xz ) ) );
			half4 Objects33_g46036 = SAMPLE_TEXTURE2D( TVE_ExtrasTex_Objects, samplerTVE_ExtrasTex_Objects, ( (TVE_ExtrasCoord_Objects).zw + ( (TVE_ExtrasCoord_Objects).xy * (Position82_g46029).xz ) ) );
			half4 localUSE_BUFFERS33_g46036 = USE_BUFFERS( Vegetation33_g46036 , Grass33_g46036 , Objects33_g46036 );
			float4 break49_g46029 = localUSE_BUFFERS33_g46036;
			half Global_Extras_Overlay156_g46000 = break49_g46029.z;
			float temp_output_1025_0_g46000 = ( _GlobalOverlay * Global_Extras_Overlay156_g46000 );
			half Overlay_Commons1365_g46000 = temp_output_1025_0_g46000;
			float temp_output_7_0_g46086 = _OverlayMaskMinValue;
			half Overlay_Mask269_g46000 = saturate( ( ( ( ( ( lerpResult3567_g46000 * 0.5 ) + staticSwitch3574_g46000 ) * Overlay_Commons1365_g46000 ) - temp_output_7_0_g46086 ) / ( _OverlayMaskMaxValue - temp_output_7_0_g46086 ) ) );
			float3 lerpResult336_g46000 = lerp( Blend_AlbedoAndSubsurface149_g46000 , Global_OverlayColor1758_g46000 , Overlay_Mask269_g46000);
			half3 Final_Albedo359_g46000 = lerpResult336_g46000;
			float Main_Alpha316_g46000 = ( _MainColor.a * tex2DNode29_g46000.a );
			float lerpResult354_g46000 = lerp( 1.0 , Main_Alpha316_g46000 , _render_premul);
			half Final_Premultiply355_g46000 = lerpResult354_g46000;
			float3 temp_output_410_0_g46000 = ( Final_Albedo359_g46000 * Final_Premultiply355_g46000 );
			o.Albedo = ( temp_output_410_0_g46000 * i.vertexToFrag11_g46056 );
			float3 temp_cast_4 = (( 0.04 * _RenderSpecular )).xxx;
			o.Specular = temp_cast_4;
			half Main_Smoothness227_g46000 = ( tex2DNode35_g46000.a * _MainSmoothnessValue );
			half Packed_Smoothness3388_g46000 = tex2DNode3380_g46000.b;
			#if defined(TVE_DETAIL_MAPS_STANDARD)
				float staticSwitch3456_g46000 = tex2DNode33_g46000.a;
			#elif defined(TVE_DETAIL_MAPS_PACKED)
				float staticSwitch3456_g46000 = Packed_Smoothness3388_g46000;
			#else
				float staticSwitch3456_g46000 = tex2DNode33_g46000.a;
			#endif
			half Second_Smoothness236_g46000 = ( staticSwitch3456_g46000 * _SecondSmoothnessValue );
			float lerpResult266_g46000 = lerp( Main_Smoothness227_g46000 , Second_Smoothness236_g46000 , Mask_Detail147_g46000);
			#if defined(TVE_DETAIL_MODE_OFF)
				float staticSwitch297_g46000 = Main_Smoothness227_g46000;
			#elif defined(TVE_DETAIL_MODE_OVERLAY)
				float staticSwitch297_g46000 = Main_Smoothness227_g46000;
			#elif defined(TVE_DETAIL_MODE_REPLACE)
				float staticSwitch297_g46000 = lerpResult266_g46000;
			#else
				float staticSwitch297_g46000 = Main_Smoothness227_g46000;
			#endif
			half Blend_Smoothness314_g46000 = staticSwitch297_g46000;
			half Global_OverlaySmoothness311_g46000 = TVE_OverlaySmoothness;
			float lerpResult343_g46000 = lerp( Blend_Smoothness314_g46000 , Global_OverlaySmoothness311_g46000 , Overlay_Mask269_g46000);
			half Final_Smoothness371_g46000 = lerpResult343_g46000;
			half Global_Extras_Wetness305_g46000 = break49_g46029.y;
			float lerpResult3673_g46000 = lerp( 0.0 , Global_Extras_Wetness305_g46000 , _GlobalWetness);
			o.Smoothness = saturate( ( Final_Smoothness371_g46000 + lerpResult3673_g46000 ) );
			float lerpResult240_g46000 = lerp( 1.0 , tex2DNode35_g46000.g , _MainOcclusionValue);
			half Main_Occlusion247_g46000 = lerpResult240_g46000;
			float lerpResult239_g46000 = lerp( 1.0 , tex2DNode33_g46000.g , _SecondOcclusionValue);
			#if defined(TVE_DETAIL_MAPS_STANDARD)
				float staticSwitch3455_g46000 = lerpResult239_g46000;
			#elif defined(TVE_DETAIL_MAPS_PACKED)
				float staticSwitch3455_g46000 = 1.0;
			#else
				float staticSwitch3455_g46000 = lerpResult239_g46000;
			#endif
			half Second_Occlusion251_g46000 = staticSwitch3455_g46000;
			float lerpResult294_g46000 = lerp( Main_Occlusion247_g46000 , Second_Occlusion251_g46000 , Mask_Detail147_g46000);
			#if defined(TVE_DETAIL_MODE_OFF)
				float staticSwitch310_g46000 = Main_Occlusion247_g46000;
			#elif defined(TVE_DETAIL_MODE_OVERLAY)
				float staticSwitch310_g46000 = ( Main_Occlusion247_g46000 * Second_Occlusion251_g46000 );
			#elif defined(TVE_DETAIL_MODE_REPLACE)
				float staticSwitch310_g46000 = lerpResult294_g46000;
			#else
				float staticSwitch310_g46000 = Main_Occlusion247_g46000;
			#endif
			half Blend_Occlusion323_g46000 = staticSwitch310_g46000;
			o.Occlusion = Blend_Occlusion323_g46000;
			float localCustomAlphaClip3735_g46000 = ( 0.0 );
			half Fade_Camera3743_g46000 = i.vertexToFrag11_g46001;
			half Final_AlphaFade3727_g46000 = ( 1.0 * Fade_Camera3743_g46000 );
			float temp_output_41_0_g46006 = Final_AlphaFade3727_g46000;
			half AlphaTreshold2132_g46000 = _Cutoff;
			#ifdef TVE_ALPHA_CLIP
				float staticSwitch3792_g46000 = ( Main_Alpha316_g46000 - ( AlphaTreshold2132_g46000 - 0.5 ) );
			#else
				float staticSwitch3792_g46000 = Main_Alpha316_g46000;
			#endif
			half Final_Alpha3754_g46000 = staticSwitch3792_g46000;
			float temp_output_661_0_g46000 = ( saturate( ( temp_output_41_0_g46006 + ( temp_output_41_0_g46006 * SAMPLE_TEXTURE3D( TVE_ScreenTex3D, samplerTVE_ScreenTex3D, ( TVE_ScreenTexCoord * PositionWS_PerVertex3905_g46000 ) ).r ) ) ) * Final_Alpha3754_g46000 );
			float Alpha3735_g46000 = temp_output_661_0_g46000;
			float Treshold3735_g46000 = 0.5;
			{
			#if TVE_ALPHA_CLIP
				clip(Alpha3735_g46000 - Treshold3735_g46000);
			#endif
			}
			half Final_Clip914_g46000 = saturate( Alpha3735_g46000 );
			o.Alpha = Final_Clip914_g46000;
		}

		ENDCG
	}
	Fallback "Hidden/BOXOPHOBIC/The Vegetation Engine/Fallback"
	CustomEditor "TVEShaderCoreGUI"
}
/*ASEBEGIN
Version=18806
1920;1;1906;1021;2789.496;824.968;1;True;False
Node;AmplifyShaderEditor.FunctionNode;335;-2176,320;Inherit;False;Define TVE_USE_VEGETATION_BUFFERS;-1;;40735;1ad73017b051a444d8dd4dba6e00b9ca;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;127;-2176,-1024;Half;False;Property;_IsBarkShader;_IsBarkShader;235;1;[HideInInspector];Create;True;0;0;0;True;0;False;1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;307;-2176,256;Inherit;False;Define TVE_IS_VEGETATION_SHADER;-1;;46102;b458122dd75182d488380bd0f592b9e6;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;134;-1984,-1024;Half;False;Property;_IsStandardShader;_IsStandardShader;237;1;[HideInInspector];Create;True;0;0;0;True;0;False;1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-1984,-768;Half;False;Property;_render_src;_render_src;241;1;[HideInInspector];Create;True;0;0;0;True;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;352;-1952,-896;Half;False;Property;_IsTarget40Shader;_IsTarget40Shader;238;1;[HideInInspector];Create;True;0;0;0;True;0;False;1;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-1600,-768;Half;False;Property;_render_zw;_render_zw;243;1;[HideInInspector];Create;True;0;2;Opaque;0;Transparent;1;0;True;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;306;-2176,-896;Half;False;Property;_IsAnyPathShader;_IsAnyPathShader;236;1;[HideInInspector];Create;True;0;0;0;True;0;False;1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;10;-2176,-768;Half;False;Property;_render_cull;_render_cull;240;1;[HideInInspector];Create;True;0;3;Both;0;Back;1;Front;2;0;True;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;371;-2176,-384;Inherit;False;Base Shader;1;;46000;856f7164d1c579d43a5cf4968a75ca43;72,3882,1,3880,1,3957,1,4028,1,4029,1,3904,1,3903,1,3900,1,3908,1,1300,1,1298,1,3586,0,1271,1,3889,0,3658,0,1708,0,3509,1,1712,1,3873,1,1714,1,1717,1,1718,1,1715,1,916,1,1762,0,1763,0,3568,1,1949,1,1776,0,3475,1,893,0,1745,1,3479,0,3501,1,3221,2,1646,0,1690,0,1757,0,3960,0,2807,0,3886,0,2953,0,3887,0,3243,0,3888,0,3728,1,3949,0,2172,0,3883,0,2658,0,1742,1,3484,0,1735,1,3575,1,1734,1,1736,1,1733,1,1737,1,878,0,1550,0,860,1,3544,1,2261,1,2260,1,2054,1,2032,1,2060,0,2036,0,2039,0,2062,0,3592,1,2750,0;0;15;FLOAT3;0;FLOAT3;528;FLOAT3;2489;FLOAT;3678;FLOAT;529;FLOAT;530;FLOAT;531;FLOAT;1235;FLOAT3;1230;FLOAT;1461;FLOAT;1290;FLOAT;721;FLOAT;532;FLOAT;629;FLOAT3;534
Node;AmplifyShaderEditor.RangedFloatNode;7;-1792,-768;Half;False;Property;_render_dst;_render_dst;242;1;[HideInInspector];Create;True;0;2;Opaque;0;Transparent;1;0;True;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;31;-2176,-1152;Half;False;Property;_Banner;Banner;0;0;Create;True;0;0;0;True;1;StyledBanner(Bark Standard Lit (Deferred));False;0;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;359;-1728,-896;Half;False;Property;_IsBalancedShader;_IsBalancedShader;239;1;[HideInInspector];Create;True;0;0;0;True;0;False;1;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;360;-1376,-384;Float;False;True;-1;4;TVEShaderCoreGUI;0;0;StandardSpecular;BOXOPHOBIC/The Vegetation Engine/Vegetation/Bark Standard Lit (Deferred);False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;False;True;Back;0;True;17;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;True;20;10;True;7;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Absolute;0;Hidden/BOXOPHOBIC/The Vegetation Engine/Fallback;244;-1;-1;-1;0;False;0;0;True;10;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;True;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
Node;AmplifyShaderEditor.CommentaryNode;285;-2176,128;Inherit;False;1026.438;100;Features;0;;0,1,0.5,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;37;-2176,-1280;Inherit;False;1026.438;100;Internal;0;;1,0.252,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;33;-2176,-512;Inherit;False;1022.896;100;Final;0;;0,1,0.5,1;0;0
WireConnection;360;0;371;0
WireConnection;360;1;371;528
WireConnection;360;3;371;3678
WireConnection;360;4;371;530
WireConnection;360;5;371;531
WireConnection;360;9;371;532
WireConnection;360;11;371;534
ASEEND*/
//CHKSM=C0F15346C0807C724482A7825D51CDC42C97D8F0