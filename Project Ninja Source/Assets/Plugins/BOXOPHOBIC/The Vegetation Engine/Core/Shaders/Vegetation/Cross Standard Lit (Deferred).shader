// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "BOXOPHOBIC/The Vegetation Engine/Vegetation/Cross Standard Lit (Deferred)"
{
	Properties
	{
		[StyledBanner(Cross Standard Lit (Deferred))]_Banner("Banner", Float) = 0
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
		_FadeHorizontalValue("Fade by Horizontal Angle", Range( 0 , 1)) = 0
		_FadeVerticalValue("Fade by Vertical Angle", Range( 0 , 1)) = 0
		[StyledCategory(Global Settings)]_GlobalCat("[ Global Cat ]", Float) = 0
		[StyledMessage(Warning, Procedural Variation in use. The Variation might not work as expected when switching from one LOD to another., _VertexVariationMode, 1 , 0, 10)]_VariationGlobalsMessage("# Variation Globals Message", Float) = 0
		_GlobalColors("Global Colors", Range( 0 , 1)) = 1
		_GlobalOverlay("Global Overlay", Range( 0 , 1)) = 1
		_GlobalWetness("Global Wetness", Range( 0 , 1)) = 1
		_GlobalSize("Global Size", Range( 0 , 1)) = 1
		[StyledRemapSlider(_ColorsMaskMinValue, _ColorsMaskMaxValue, 0, 1, 10, 0)]_ColorsMaskRemap("Colors Mask", Vector) = (0,0,0,0)
		[HideInInspector]_ColorsMaskMinValue("Colors Mask Min Value", Range( 0 , 1)) = 0
		[HideInInspector]_ColorsMaskMaxValue("Colors Mask Max Value", Range( 0 , 1)) = 1
		[StyledRemapSlider(_OverlayMaskMinValue, _OverlayMaskMaxValue, 0, 1, 10, 0)]_OverlayMaskRemap("Overlay Mask", Vector) = (0,0,0,0)
		[HideInInspector]_OverlayMaskMinValue("Overlay Mask Min Value", Range( 0 , 1)) = 0.45
		[HideInInspector]_OverlayMaskMaxValue("Overlay Mask Max Value", Range( 0 , 1)) = 0.55
		[HideInInspector][HDR]_LocalColors("Local Colors", Color) = (1,1,1,1)
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
		[StyledRemapSlider(_DetailBlendMinValue, _DetailBlendMaxValue,0,1)]_DetailBlendRemap("Detail Blending", Vector) = (0,0,0,0)
		[StyledCategory(Occlusion Settings)]_OcclusionCat("[ Occlusion Cat ]", Float) = 0
		[StyledRemapSlider(_VertexOcclusionMinValue, _VertexOcclusionMaxValue, 0, 1)]_VertexOcclusionRemap("Vertex Occlusion Mask", Vector) = (0,0,0,0)
		[StyledCategory(Emissive Settings)]_EmissiveCat("[ Emissive Cat]", Float) = 0
		[StyledCategory(Subsurface Settings)]_SubsurfaceCat("[ Subsurface Cat ]", Float) = 0
		_SubsurfaceValue("Subsurface Intensity", Range( 0 , 1)) = 1
		[DiffusionProfile]_SubsurfaceDiffusion("Subsurface Diffusion", Float) = 0
		[HideInInspector]_SubsurfaceDiffusion_Asset("Subsurface Diffusion", Vector) = (0,0,0,0)
		[HideInInspector][ASEDiffusionProfile(_SubsurfaceDiffusion)]_SubsurfaceDiffusion_asset("Subsurface Diffusion", Vector) = (0,0,0,0)
		[HDR]_SubsurfaceColor("Subsurface Color", Color) = (0.3315085,0.490566,0,1)
		[StyledRemapSlider(_SubsurfaceMaskMinValue, _SubsurfaceMaskMaxValue,0,1)]_SubsurfaceMaskRemap("Subsurface Mask", Vector) = (0,0,0,0)
		[HideInInspector]_SubsurfaceMaskMinValue("Subsurface Mask Min Value", Range( 0 , 1)) = 0
		[HideInInspector]_SubsurfaceMaskMaxValue("Subsurface Mask Max Value", Range( 0 , 1)) = 1
		[Space(10)]_MainLightScatteringValue("Light Scattering Intensity", Range( 0 , 16)) = 8
		_MainLightAngleValue("Light Scattering Angle", Range( 0 , 16)) = 8
		[Space(10)]_TranslucencyIntensityValue("Translucency Intensity", Range( 0 , 50)) = 1
		_TranslucencyNormalValue("Translucency Normal", Range( 0 , 1)) = 0.1
		_TranslucencyScatteringValue("Translucency Scattering", Range( 1 , 50)) = 2
		_TranslucencyDirectValue("Translucency Direct", Range( 0 , 1)) = 1
		_TranslucencyAmbientValue("Translucency Ambient", Range( 0 , 1)) = 0.2
		_TranslucencyShadowValue("Translucency Shadow", Range( 0 , 1)) = 1
		[StyledMessage(Warning,  Translucency is not supported in HDRP. Diffusion Profiles will be used instead., 10, 5)]_TranslucencyHDMessage("# Translucency HD Message", Float) = 0
		[StyledCategory(Gradient Settings)]_GradientCat("[ Gradient Cat ]", Float) = 0
		[HDR]_GradientColorOne("Gradient Color One", Color) = (1,1,1,1)
		[HDR]_GradientColorTwo("Gradient Color Two", Color) = (1,1,1,1)
		[StyledRemapSlider(_GradientMinValue, _GradientMaxValue, 0, 1)]_GradientMaskRemap("Gradient Mask", Vector) = (0,0,0,0)
		[HideInInspector]_GradientMinValue("Gradient Mask Min", Range( 0 , 1)) = 0
		[HideInInspector]_GradientMaxValue("Gradient Mask Max ", Range( 0 , 1)) = 1
		[StyledCategory(Noise Settings)]_NoiseCat("[ Noise Cat ]", Float) = 0
		[HDR]_NoiseColorOne("Noise Color One", Color) = (1,1,1,1)
		[HDR]_NoiseColorTwo("Noise Color Two", Color) = (1,1,1,1)
		[StyledRemapSlider(_NoiseMinValue, _NoiseMaxValue, 0, 1)]_NoiseMaskRemap("Noise Mask", Vector) = (0,0,0,0)
		[HideInInspector]_NoiseMinValue("Noise Mask Min", Range( 0 , 1)) = 0
		[HideInInspector]_NoiseMaxValue("Noise Mask Max ", Range( 0 , 1)) = 1
		_NoiseScaleValue("Noise Scale", Range( 0 , 1)) = 0.01
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
		[HideInInspector]_IsStandardShader("_IsStandardShader", Float) = 1
		[HideInInspector]_IsAnyPathShader("_IsAnyPathShader", Float) = 1
		[HideInInspector]_IsCrossShader("_IsCrossShader", Float) = 1
		[HideInInspector]_IsTarget40Shader("_IsTarget40Shader", Float) = 1
		[HideInInspector]_IsBalancedShader("_IsBalancedShader", Float) = 1
		[HideInInspector]_IsSubsurfaceApproxShader("_IsSubsurfaceApproxShader", Float) = 1
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
		#pragma target 4.0
		#pragma shader_feature_local TVE_ALPHA_CLIP
		#pragma shader_feature_local TVE_VERTEX_DATA_BATCHED
		  
		#define THE_VEGETATION_ENGINE
		    
		//SHADER INJECTION POINT BEGIN
		//SHADER INJECTION POINT END
		      
		#define TVE_IS_VEGETATION_SHADER
		#define TVE_USE_VEGETATION_BUFFERS
		#if defined(SHADER_API_D3D11) || defined(SHADER_API_XBOXONE) || defined(UNITY_COMPILER_HLSLCC) || defined(SHADER_API_PSSL) || (defined(SHADER_TARGET_SURFACE_ANALYSIS) && !defined(SHADER_TARGET_SURFACE_ANALYSIS_MOJOSHADER))//ASE Sampler Macros
		#define SAMPLE_TEXTURE2D(tex,samplerTex,coord) tex.Sample(samplerTex,coord)
		#define SAMPLE_TEXTURE2D_LOD(tex,samplerTex,coord,lod) tex.SampleLevel(samplerTex,coord, lod)
		#define SAMPLE_TEXTURE2D_BIAS(tex,samplerTex,coord,bias) tex.SampleBias(samplerTex,coord,bias)
		#define SAMPLE_TEXTURE2D_GRAD(tex,samplerTex,coord,ddx,ddy) tex.SampleGrad(samplerTex,coord,ddx,ddy)
		#define SAMPLE_TEXTURE3D(tex,samplerTex,coord) tex.Sample(samplerTex,coord)
		#define SAMPLE_TEXTURE3D_LOD(tex,samplerTex,coord,lod) tex.SampleLevel(samplerTex,coord, lod)
		#else//ASE Sampling Macros
		#define SAMPLE_TEXTURE2D(tex,samplerTex,coord) tex2D(tex,coord)
		#define SAMPLE_TEXTURE2D_LOD(tex,samplerTex,coord,lod) tex2Dlod(tex,float4(coord,0,lod))
		#define SAMPLE_TEXTURE2D_BIAS(tex,samplerTex,coord,bias) tex2Dbias(tex,float4(coord,0,bias))
		#define SAMPLE_TEXTURE2D_GRAD(tex,samplerTex,coord,ddx,ddy) tex2Dgrad(tex,coord,ddx,ddy)
		#define SAMPLE_TEXTURE3D(tex,samplerTex,coord) tex3D(tex,coord)
		#define SAMPLE_TEXTURE3D_LOD(tex,samplerTex,coord,lod) tex3Dlod(tex,float4(coord,lod))
		#endif//ASE Sampling Macros

		#pragma surface surf StandardSpecular keepalpha addshadow fullforwardshadows dithercrossfade vertex:vertexDataFunc 
		struct Input
		{
			float3 worldPos;
			float2 vertexToFrag11_g46187;
			half ASEVFace : VFACE;
			float3 vertexToFrag11_g46208;
			float3 vertexToFrag11_g46207;
			float vertexToFrag11_g46186;
			float3 vertexToFrag3890_g46138;
		};

		uniform half _IsBalancedShader;
		uniform half _IsTarget40Shader;
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
		uniform half _render_zw;
		uniform half _IsSubsurfaceApproxShader;
		uniform half _IsStandardShader;
		uniform half _Banner;
		uniform half _IsCrossShader;
		uniform half _IsAnyPathShader;
		uniform half _render_cull;
		uniform half _render_dst;
		uniform half _render_src;
		uniform half _MotionAmplitude_10;
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
		uniform half4 _GradientColorTwo;
		uniform half4 _GradientColorOne;
		uniform half _GradientMinValue;
		uniform half _GradientMaxValue;
		uniform half4 _NoiseColorTwo;
		uniform half4 _NoiseColorOne;
		UNITY_DECLARE_TEX3D_NOSAMPLER(TVE_WorldTex3D);
		uniform half _NoiseScaleValue;
		SamplerState samplerTVE_WorldTex3D;
		uniform half _NoiseMinValue;
		uniform half _NoiseMaxValue;
		uniform half4 _MainColor;
		UNITY_DECLARE_TEX2D_NOSAMPLER(TVE_ColorsTex_Vegetation);
		uniform half4 TVE_ColorsCoord_Vegetation;
		SamplerState samplerTVE_ColorsTex_Vegetation;
		UNITY_DECLARE_TEX2D_NOSAMPLER(TVE_ColorsTex_Grass);
		uniform half4 TVE_ColorsCoord_Grass;
		SamplerState samplerTVE_ColorsTex_Grass;
		UNITY_DECLARE_TEX2D_NOSAMPLER(TVE_ColorsTex_Objects);
		uniform half4 TVE_ColorsCoord_Objects;
		SamplerState samplerTVE_ColorsTex_Objects;
		uniform float4 _LocalColors;
		uniform half _GlobalColors;
		UNITY_DECLARE_TEX2D_NOSAMPLER(_MainMaskTex);
		uniform half _ColorsMaskMinValue;
		uniform half _ColorsMaskMaxValue;
		uniform half4 _SubsurfaceColor;
		uniform half _SubsurfaceValue;
		uniform half4 TVE_MainLightParams;
		uniform half _SubsurfaceMaskMinValue;
		uniform half _SubsurfaceMaskMaxValue;
		uniform half3 TVE_MainLightDirection;
		uniform half _MainLightAngleValue;
		uniform half _MainLightScatteringValue;
		uniform half4 TVE_OverlayColor;
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
		uniform half _RenderSpecular;
		uniform half _MainSmoothnessValue;
		uniform half TVE_OverlaySmoothness;
		uniform half _GlobalWetness;
		uniform half _MainOcclusionValue;
		uniform half _FadeHorizontalValue;
		uniform half _FadeVerticalValue;
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
			float3 PositionOS3588_g46138 = ase_vertex3Pos;
			half3 _Vector1 = half3(0,0,0);
			half3 Mesh_PivotsOS2291_g46138 = _Vector1;
			float3 temp_output_2283_0_g46138 = ( PositionOS3588_g46138 - Mesh_PivotsOS2291_g46138 );
			half3 VertexPos40_g46211 = temp_output_2283_0_g46138;
			float3 appendResult74_g46211 = (float3(VertexPos40_g46211.x , 0.0 , 0.0));
			half3 VertexPosRotationAxis50_g46211 = appendResult74_g46211;
			float3 break84_g46211 = VertexPos40_g46211;
			float3 appendResult81_g46211 = (float3(0.0 , break84_g46211.y , break84_g46211.z));
			half3 VertexPosOtherAxis82_g46211 = appendResult81_g46211;
			float ObjectData20_g46204 = 3.14;
			float Bounds_Height374_g46138 = _MaxBoundsInfo.y;
			float WorldData19_g46204 = ( Bounds_Height374_g46138 * 3.14 );
			#ifdef TVE_VERTEX_DATA_BATCHED
				float staticSwitch14_g46204 = WorldData19_g46204;
			#else
				float staticSwitch14_g46204 = ObjectData20_g46204;
			#endif
			float Motion_Max_Bending1133_g46138 = staticSwitch14_g46204;
			float4x4 break19_g46150 = unity_ObjectToWorld;
			float3 appendResult20_g46150 = (float3(break19_g46150[ 0 ][ 3 ] , break19_g46150[ 1 ][ 3 ] , break19_g46150[ 2 ][ 3 ]));
			half3 Off19_g46151 = appendResult20_g46150;
			float4 ase_vertex4Pos = v.vertex;
			float4 transform68_g46150 = mul(unity_ObjectToWorld,ase_vertex4Pos);
			float3 appendResult95_g46150 = (float3(v.texcoord.z , 0.0 , v.texcoord.w));
			float4 transform62_g46150 = mul(unity_ObjectToWorld,float4( ( ase_vertex3Pos - ( appendResult95_g46150 * _vertex_pivot_mode ) ) , 0.0 ));
			float3 ObjectPositionWithPivots28_g46150 = ( (transform68_g46150).xyz - (transform62_g46150).xyz );
			half3 On20_g46151 = ObjectPositionWithPivots28_g46150;
			#ifdef TVE_PIVOT_DATA_BAKED
				float3 staticSwitch14_g46151 = On20_g46151;
			#else
				float3 staticSwitch14_g46151 = Off19_g46151;
			#endif
			half3 ObjectData20_g46152 = staticSwitch14_g46151;
			half3 WorldData19_g46152 = Off19_g46151;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float3 staticSwitch14_g46152 = WorldData19_g46152;
			#else
				float3 staticSwitch14_g46152 = ObjectData20_g46152;
			#endif
			float3 temp_output_42_0_g46150 = staticSwitch14_g46152;
			half3 ObjectData20_g46156 = temp_output_42_0_g46150;
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			half3 WorldData19_g46156 = ase_worldPos;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float3 staticSwitch14_g46156 = WorldData19_g46156;
			#else
				float3 staticSwitch14_g46156 = ObjectData20_g46156;
			#endif
			float3 Position83_g46149 = staticSwitch14_g46156;
			half4 Vegetation33_g46155 = SAMPLE_TEXTURE2D_LOD( TVE_VertexTex_Vegetation, samplerTVE_VertexTex_Vegetation, ( (TVE_VertexCoord_Vegetation).zw + ( (TVE_VertexCoord_Vegetation).xy * (Position83_g46149).xz ) ), 0.0 );
			half4 Grass33_g46155 = SAMPLE_TEXTURE2D_LOD( TVE_VertexTex_Grass, samplerTVE_VertexTex_Grass, ( (TVE_VertexCoord_Grass).zw + ( (TVE_VertexCoord_Grass).xy * (Position83_g46149).xz ) ), 0.0 );
			half4 Objects33_g46155 = SAMPLE_TEXTURE2D_LOD( TVE_VertexTex_Objects, samplerTVE_VertexTex_Objects, ( (TVE_VertexCoord_Objects).zw + ( (TVE_VertexCoord_Objects).xy * (Position83_g46149).xz ) ), 0.0 );
			half4 localUSE_BUFFERS33_g46155 = USE_BUFFERS( Vegetation33_g46155 , Grass33_g46155 , Objects33_g46155 );
			half4 Global_Motion_Params3909_g46138 = localUSE_BUFFERS33_g46155;
			float4 break322_g46217 = Global_Motion_Params3909_g46138;
			half Wind_Power369_g46217 = break322_g46217.z;
			float temp_output_7_0_g46219 = 0.5;
			float temp_output_404_0_g46217 = (Wind_Power369_g46217*2.0 + -1.0);
			float temp_output_406_0_g46217 = saturate( sign( temp_output_404_0_g46217 ) );
			float lerpResult401_g46217 = lerp( 0.0 , ( ( Wind_Power369_g46217 - temp_output_7_0_g46219 ) / ( 1.0 - temp_output_7_0_g46219 ) ) , temp_output_406_0_g46217);
			float lerpResult376_g46217 = lerp( 0.1 , 1.0 , lerpResult401_g46217);
			half Wind_Power_103106_g46138 = lerpResult376_g46217;
			float3 appendResult397_g46217 = (float3(break322_g46217.x , 0.0 , break322_g46217.y));
			float3 temp_output_398_0_g46217 = (appendResult397_g46217*2.0 + -1.0);
			float3 ase_parentObjectScale = (1.0/float3( length( unity_WorldToObject[ 0 ].xyz ), length( unity_WorldToObject[ 1 ].xyz ), length( unity_WorldToObject[ 2 ].xyz ) ));
			float3 temp_output_339_0_g46217 = ( mul( unity_WorldToObject, float4( temp_output_398_0_g46217 , 0.0 ) ).xyz * ase_parentObjectScale );
			half2 Wind_DirectionOS39_g46138 = (temp_output_339_0_g46217).xz;
			#ifdef TVE_IS_GRASS_SHADER
				float2 staticSwitch160_g46225 = TVE_NoiseSpeed_Grass;
			#else
				float2 staticSwitch160_g46225 = TVE_NoiseSpeed_Vegetation;
			#endif
			float4x4 break19_g46227 = unity_ObjectToWorld;
			float3 appendResult20_g46227 = (float3(break19_g46227[ 0 ][ 3 ] , break19_g46227[ 1 ][ 3 ] , break19_g46227[ 2 ][ 3 ]));
			half3 Off19_g46228 = appendResult20_g46227;
			float4 transform68_g46227 = mul(unity_ObjectToWorld,ase_vertex4Pos);
			float3 appendResult95_g46227 = (float3(v.texcoord.z , 0.0 , v.texcoord.w));
			float4 transform62_g46227 = mul(unity_ObjectToWorld,float4( ( ase_vertex3Pos - ( appendResult95_g46227 * _vertex_pivot_mode ) ) , 0.0 ));
			float3 ObjectPositionWithPivots28_g46227 = ( (transform68_g46227).xyz - (transform62_g46227).xyz );
			half3 On20_g46228 = ObjectPositionWithPivots28_g46227;
			#ifdef TVE_PIVOT_DATA_BAKED
				float3 staticSwitch14_g46228 = On20_g46228;
			#else
				float3 staticSwitch14_g46228 = Off19_g46228;
			#endif
			half3 ObjectData20_g46229 = staticSwitch14_g46228;
			half3 WorldData19_g46229 = Off19_g46228;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float3 staticSwitch14_g46229 = WorldData19_g46229;
			#else
				float3 staticSwitch14_g46229 = ObjectData20_g46229;
			#endif
			float3 temp_output_42_0_g46227 = staticSwitch14_g46229;
			half3 ObjectData20_g46226 = temp_output_42_0_g46227;
			half3 WorldData19_g46226 = ase_worldPos;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float3 staticSwitch14_g46226 = WorldData19_g46226;
			#else
				float3 staticSwitch14_g46226 = ObjectData20_g46226;
			#endif
			#ifdef TVE_IS_GRASS_SHADER
				float2 staticSwitch164_g46225 = (ase_worldPos).xz;
			#else
				float2 staticSwitch164_g46225 = (staticSwitch14_g46226).xz;
			#endif
			#ifdef TVE_IS_GRASS_SHADER
				float staticSwitch161_g46225 = TVE_NoiseSize_Grass;
			#else
				float staticSwitch161_g46225 = TVE_NoiseSize_Vegetation;
			#endif
			float2 panner73_g46225 = ( _Time.y * staticSwitch160_g46225 + ( staticSwitch164_g46225 * staticSwitch161_g46225 ));
			float4 tex2DNode75_g46225 = SAMPLE_TEXTURE2D_LOD( TVE_NoiseTex, samplerTVE_NoiseTex, panner73_g46225, 0.0 );
			float4 saferPower77_g46225 = max( abs( tex2DNode75_g46225 ) , 0.0001 );
			half Wind_Power2223_g46138 = lerpResult401_g46217;
			float temp_output_167_0_g46225 = Wind_Power2223_g46138;
			float lerpResult168_g46225 = lerp( 1.5 , 0.25 , temp_output_167_0_g46225);
			float4 temp_cast_7 = (lerpResult168_g46225).xxxx;
			float4 break142_g46225 = pow( saferPower77_g46225 , temp_cast_7 );
			half Global_NoiseTex_R34_g46138 = break142_g46225.r;
			half Input_Speed62_g46214 = _MotionSpeed_10;
			float mulTime373_g46214 = _Time.y * Input_Speed62_g46214;
			float4x4 break19_g46162 = unity_ObjectToWorld;
			float3 appendResult20_g46162 = (float3(break19_g46162[ 0 ][ 3 ] , break19_g46162[ 1 ][ 3 ] , break19_g46162[ 2 ][ 3 ]));
			half3 Off19_g46163 = appendResult20_g46162;
			float4 transform68_g46162 = mul(unity_ObjectToWorld,ase_vertex4Pos);
			float3 appendResult95_g46162 = (float3(v.texcoord.z , 0.0 , v.texcoord.w));
			float4 transform62_g46162 = mul(unity_ObjectToWorld,float4( ( ase_vertex3Pos - ( appendResult95_g46162 * _vertex_pivot_mode ) ) , 0.0 ));
			float3 ObjectPositionWithPivots28_g46162 = ( (transform68_g46162).xyz - (transform62_g46162).xyz );
			half3 On20_g46163 = ObjectPositionWithPivots28_g46162;
			#ifdef TVE_PIVOT_DATA_BAKED
				float3 staticSwitch14_g46163 = On20_g46163;
			#else
				float3 staticSwitch14_g46163 = Off19_g46163;
			#endif
			half3 ObjectData20_g46164 = staticSwitch14_g46163;
			half3 WorldData19_g46164 = Off19_g46163;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float3 staticSwitch14_g46164 = WorldData19_g46164;
			#else
				float3 staticSwitch14_g46164 = ObjectData20_g46164;
			#endif
			float3 temp_output_42_0_g46162 = staticSwitch14_g46164;
			float3 break9_g46162 = temp_output_42_0_g46162;
			half Variation_Complex102_g46160 = frac( ( v.color.r + ( break9_g46162.x + break9_g46162.z ) ) );
			float ObjectData20_g46161 = Variation_Complex102_g46160;
			half Variation_Simple105_g46160 = v.color.r;
			float WorldData19_g46161 = Variation_Simple105_g46160;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float staticSwitch14_g46161 = WorldData19_g46161;
			#else
				float staticSwitch14_g46161 = ObjectData20_g46161;
			#endif
			half Motion_Variation3073_g46138 = staticSwitch14_g46161;
			half Motion_Variation284_g46214 = ( _MotionVariation_10 * Motion_Variation3073_g46138 );
			float2 appendResult344_g46214 = (float2(ase_worldPos.x , ase_worldPos.z));
			float2 Motion_Scale287_g46214 = ( _MotionScale_10 * appendResult344_g46214 );
			half2 Sine_MinusOneToOne281_g46214 = sin( ( mulTime373_g46214 + Motion_Variation284_g46214 + Motion_Scale287_g46214 ) );
			float2 temp_cast_9 = (1.0).xx;
			half Input_Turbulence327_g46214 = Global_NoiseTex_R34_g46138;
			float2 lerpResult321_g46214 = lerp( Sine_MinusOneToOne281_g46214 , temp_cast_9 , Input_Turbulence327_g46214);
			half2 Motion_Bending2258_g46138 = ( ( _MotionAmplitude_10 * Motion_Max_Bending1133_g46138 ) * Wind_Power_103106_g46138 * Wind_DirectionOS39_g46138 * Global_NoiseTex_R34_g46138 * lerpResult321_g46214 );
			float lerpResult402_g46217 = lerp( abs( temp_output_404_0_g46217 ) , 0.0 , temp_output_406_0_g46217);
			half Motion_InteractionMask66_g46138 = lerpResult402_g46217;
			float lerpResult3307_g46138 = lerp( 1.0 , Motion_Variation3073_g46138 , _InteractionVariation);
			half2 Motion_Interaction53_g46138 = ( _InteractionAmplitude * Motion_Max_Bending1133_g46138 * Motion_InteractionMask66_g46138 * Motion_InteractionMask66_g46138 * Wind_DirectionOS39_g46138 * lerpResult3307_g46138 );
			float2 lerpResult109_g46138 = lerp( Motion_Bending2258_g46138 , Motion_Interaction53_g46138 , Motion_InteractionMask66_g46138);
			half Mesh_Motion_182_g46138 = v.texcoord3.x;
			float2 break143_g46138 = ( lerpResult109_g46138 * Mesh_Motion_182_g46138 );
			half Motion_Z190_g46138 = break143_g46138.y;
			half Angle44_g46211 = Motion_Z190_g46138;
			half3 VertexPos40_g46147 = ( VertexPosRotationAxis50_g46211 + ( VertexPosOtherAxis82_g46211 * cos( Angle44_g46211 ) ) + ( cross( float3(1,0,0) , VertexPosOtherAxis82_g46211 ) * sin( Angle44_g46211 ) ) );
			float3 appendResult74_g46147 = (float3(0.0 , 0.0 , VertexPos40_g46147.z));
			half3 VertexPosRotationAxis50_g46147 = appendResult74_g46147;
			float3 break84_g46147 = VertexPos40_g46147;
			float3 appendResult81_g46147 = (float3(break84_g46147.x , break84_g46147.y , 0.0));
			half3 VertexPosOtherAxis82_g46147 = appendResult81_g46147;
			half Motion_X216_g46138 = break143_g46138.x;
			half Angle44_g46147 = -Motion_X216_g46138;
			float3 Vertex_Motion_Object833_g46138 = ( VertexPosRotationAxis50_g46147 + ( VertexPosOtherAxis82_g46147 * cos( Angle44_g46147 ) ) + ( cross( float3(0,0,1) , VertexPosOtherAxis82_g46147 ) * sin( Angle44_g46147 ) ) );
			float3 temp_output_3474_0_g46138 = ( PositionOS3588_g46138 - Mesh_PivotsOS2291_g46138 );
			float3 appendResult2043_g46138 = (float3(Motion_X216_g46138 , 0.0 , Motion_Z190_g46138));
			float3 Vertex_Motion_World1118_g46138 = ( temp_output_3474_0_g46138 + appendResult2043_g46138 );
			#ifdef TVE_VERTEX_DATA_BATCHED
				float3 staticSwitch3312_g46138 = Vertex_Motion_World1118_g46138;
			#else
				float3 staticSwitch3312_g46138 = ( Vertex_Motion_Object833_g46138 + ( 0.0 * _VertexDataMode ) );
			#endif
			half Global_Vertex_Size174_g46138 = break322_g46217.w;
			float lerpResult346_g46138 = lerp( 1.0 , Global_Vertex_Size174_g46138 , _GlobalSize);
			float temp_output_2626_0_g46138 = ( lerpResult346_g46138 * _LocalSize );
			float3 appendResult3480_g46138 = (float3(temp_output_2626_0_g46138 , temp_output_2626_0_g46138 , temp_output_2626_0_g46138));
			half3 ObjectData20_g46188 = appendResult3480_g46138;
			half3 _Vector11 = half3(1,1,1);
			half3 WorldData19_g46188 = _Vector11;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float3 staticSwitch14_g46188 = WorldData19_g46188;
			#else
				float3 staticSwitch14_g46188 = ObjectData20_g46188;
			#endif
			half3 Vertex_Size1741_g46138 = staticSwitch14_g46188;
			half3 _Vector5 = half3(1,1,1);
			float4x4 break19_g46189 = unity_ObjectToWorld;
			float3 appendResult20_g46189 = (float3(break19_g46189[ 0 ][ 3 ] , break19_g46189[ 1 ][ 3 ] , break19_g46189[ 2 ][ 3 ]));
			half3 Off19_g46190 = appendResult20_g46189;
			float4 transform68_g46189 = mul(unity_ObjectToWorld,ase_vertex4Pos);
			float3 appendResult95_g46189 = (float3(v.texcoord.z , 0.0 , v.texcoord.w));
			float4 transform62_g46189 = mul(unity_ObjectToWorld,float4( ( ase_vertex3Pos - ( appendResult95_g46189 * _vertex_pivot_mode ) ) , 0.0 ));
			float3 ObjectPositionWithPivots28_g46189 = ( (transform68_g46189).xyz - (transform62_g46189).xyz );
			half3 On20_g46190 = ObjectPositionWithPivots28_g46189;
			#ifdef TVE_PIVOT_DATA_BAKED
				float3 staticSwitch14_g46190 = On20_g46190;
			#else
				float3 staticSwitch14_g46190 = Off19_g46190;
			#endif
			half3 ObjectData20_g46191 = staticSwitch14_g46190;
			half3 WorldData19_g46191 = Off19_g46190;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float3 staticSwitch14_g46191 = WorldData19_g46191;
			#else
				float3 staticSwitch14_g46191 = ObjectData20_g46191;
			#endif
			float3 temp_output_42_0_g46189 = staticSwitch14_g46191;
			float temp_output_7_0_g46195 = _SizeFadeEndValue;
			float temp_output_335_0_g46138 = saturate( ( ( ( distance( _WorldSpaceCameraPos , temp_output_42_0_g46189 ) * ( 1.0 / TVE_DistanceFadeBias ) ) - temp_output_7_0_g46195 ) / ( _SizeFadeStartValue - temp_output_7_0_g46195 ) ) );
			float3 appendResult3482_g46138 = (float3(temp_output_335_0_g46138 , temp_output_335_0_g46138 , temp_output_335_0_g46138));
			float3 lerpResult3556_g46138 = lerp( _Vector5 , appendResult3482_g46138 , _SizeFadeMode);
			half3 ObjectData20_g46200 = lerpResult3556_g46138;
			half3 WorldData19_g46200 = _Vector5;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float3 staticSwitch14_g46200 = WorldData19_g46200;
			#else
				float3 staticSwitch14_g46200 = ObjectData20_g46200;
			#endif
			float3 Vertex_SizeFade1740_g46138 = staticSwitch14_g46200;
			half3 Grass_Coverage2661_g46138 = half3(0,0,0);
			float3 Final_VertexPosition890_g46138 = ( ( staticSwitch3312_g46138 * Vertex_Size1741_g46138 * Vertex_SizeFade1740_g46138 ) + Mesh_PivotsOS2291_g46138 + Grass_Coverage2661_g46138 );
			v.vertex.xyz = Final_VertexPosition890_g46138;
			v.vertex.w = 1;
			o.vertexToFrag11_g46187 = ( ( v.texcoord.xy * (_MainUVs).xy ) + (_MainUVs).zw );
			float temp_output_7_0_g46146 = _GradientMinValue;
			float4 lerpResult2779_g46138 = lerp( _GradientColorTwo , _GradientColorOne , saturate( ( ( v.color.a - temp_output_7_0_g46146 ) / ( _GradientMaxValue - temp_output_7_0_g46146 ) ) ));
			half3 Gradient_Tint2784_g46138 = (lerpResult2779_g46138).rgb;
			o.vertexToFrag11_g46208 = Gradient_Tint2784_g46138;
			float3 temp_cast_11 = (_NoiseScaleValue).xxx;
			float3 PositionWS_PerVertex3905_g46138 = ase_worldPos;
			float temp_output_7_0_g46197 = _NoiseMinValue;
			half Noise_Mask3162_g46138 = saturate( ( ( SAMPLE_TEXTURE3D_LOD( TVE_WorldTex3D, samplerTVE_WorldTex3D, ( temp_cast_11 * PositionWS_PerVertex3905_g46138 * 0.1 ), 0.0 ).r - temp_output_7_0_g46197 ) / ( _NoiseMaxValue - temp_output_7_0_g46197 ) ) );
			float4 lerpResult2800_g46138 = lerp( _NoiseColorTwo , _NoiseColorOne , Noise_Mask3162_g46138);
			half3 Noise_Tint2802_g46138 = (lerpResult2800_g46138).rgb;
			o.vertexToFrag11_g46207 = Noise_Tint2802_g46138;
			float3 Position58_g46232 = PositionWS_PerVertex3905_g46138;
			half4 Vegetation33_g46239 = SAMPLE_TEXTURE2D_LOD( TVE_ColorsTex_Vegetation, samplerTVE_ColorsTex_Vegetation, ( (TVE_ColorsCoord_Vegetation).zw + ( (TVE_ColorsCoord_Vegetation).xy * (Position58_g46232).xz ) ), 0.0 );
			half4 Grass33_g46239 = SAMPLE_TEXTURE2D_LOD( TVE_ColorsTex_Grass, samplerTVE_ColorsTex_Grass, ( (TVE_ColorsCoord_Grass).zw + ( (TVE_ColorsCoord_Grass).xy * (Position58_g46232).xz ) ), 0.0 );
			half4 Objects33_g46239 = SAMPLE_TEXTURE2D_LOD( TVE_ColorsTex_Objects, samplerTVE_ColorsTex_Objects, ( (TVE_ColorsCoord_Objects).zw + ( (TVE_ColorsCoord_Objects).xy * (Position58_g46232).xz ) ), 0.0 );
			half4 localUSE_BUFFERS33_g46239 = USE_BUFFERS( Vegetation33_g46239 , Grass33_g46239 , Objects33_g46239 );
			float4 temp_output_46_0_g46232 = localUSE_BUFFERS33_g46239;
			half Global_ColorsTex_A1701_g46138 = (temp_output_46_0_g46232).w;
			o.vertexToFrag11_g46186 = Global_ColorsTex_A1701_g46138;
			o.vertexToFrag3890_g46138 = ase_worldPos;
		}

		void surf( Input i , inout SurfaceOutputStandardSpecular o )
		{
			half2 Main_UVs15_g46138 = i.vertexToFrag11_g46187;
			float4 tex2DNode117_g46138 = SAMPLE_TEXTURE2D( _MainNormalTex, sampler_MainAlbedoTex, Main_UVs15_g46138 );
			float2 appendResult88_g46157 = (float2(tex2DNode117_g46138.a , tex2DNode117_g46138.g));
			float2 temp_output_90_0_g46157 = ( (appendResult88_g46157*2.0 + -1.0) * _MainNormalValue );
			float3 appendResult91_g46157 = (float3(temp_output_90_0_g46157 , 1.0));
			half3 Main_Normal137_g46138 = appendResult91_g46157;
			float3 temp_output_13_0_g46203 = Main_Normal137_g46138;
			float3 switchResult12_g46203 = (((i.ASEVFace>0)?(temp_output_13_0_g46203):(( temp_output_13_0_g46203 * _render_normals_options ))));
			half3 Blend_Normal312_g46138 = switchResult12_g46203;
			half3 Final_Normal366_g46138 = Blend_Normal312_g46138;
			o.Normal = Final_Normal366_g46138;
			float4 tex2DNode29_g46138 = SAMPLE_TEXTURE2D( _MainAlbedoTex, sampler_MainAlbedoTex, Main_UVs15_g46138 );
			float3 temp_output_3639_0_g46138 = (tex2DNode29_g46138).rgb;
			float3 temp_output_51_0_g46138 = ( (_MainColor).rgb * temp_output_3639_0_g46138 );
			half3 Main_Albedo99_g46138 = temp_output_51_0_g46138;
			half3 Blend_Albedo265_g46138 = Main_Albedo99_g46138;
			half3 Blend_AlbedoTinted2808_g46138 = ( i.vertexToFrag11_g46208 * i.vertexToFrag11_g46207 * float3(1,1,1) * Blend_Albedo265_g46138 );
			float dotResult3616_g46138 = dot( Blend_AlbedoTinted2808_g46138 , float3(0.2126,0.7152,0.0722) );
			float3 temp_cast_0 = (dotResult3616_g46138).xxx;
			half Global_Colors_Influence3668_g46138 = i.vertexToFrag11_g46186;
			float3 lerpResult3618_g46138 = lerp( Blend_AlbedoTinted2808_g46138 , temp_cast_0 , Global_Colors_Influence3668_g46138);
			float3 PositionWS_PerVertex3905_g46138 = i.vertexToFrag3890_g46138;
			float3 Position58_g46232 = PositionWS_PerVertex3905_g46138;
			half4 Vegetation33_g46239 = SAMPLE_TEXTURE2D( TVE_ColorsTex_Vegetation, samplerTVE_ColorsTex_Vegetation, ( (TVE_ColorsCoord_Vegetation).zw + ( (TVE_ColorsCoord_Vegetation).xy * (Position58_g46232).xz ) ) );
			half4 Grass33_g46239 = SAMPLE_TEXTURE2D( TVE_ColorsTex_Grass, samplerTVE_ColorsTex_Grass, ( (TVE_ColorsCoord_Grass).zw + ( (TVE_ColorsCoord_Grass).xy * (Position58_g46232).xz ) ) );
			half4 Objects33_g46239 = SAMPLE_TEXTURE2D( TVE_ColorsTex_Objects, samplerTVE_ColorsTex_Objects, ( (TVE_ColorsCoord_Objects).zw + ( (TVE_ColorsCoord_Objects).xy * (Position58_g46232).xz ) ) );
			half4 localUSE_BUFFERS33_g46239 = USE_BUFFERS( Vegetation33_g46239 , Grass33_g46239 , Objects33_g46239 );
			float4 temp_output_46_0_g46232 = localUSE_BUFFERS33_g46239;
			half3 Global_ColorsTex_RGB1700_g46138 = (temp_output_46_0_g46232).xyz;
			#ifdef UNITY_COLORSPACE_GAMMA
				float staticSwitch1_g46201 = 2.0;
			#else
				float staticSwitch1_g46201 = 4.594794;
			#endif
			half3 Global_Colors1954_g46138 = ( (_LocalColors).rgb * ( Global_ColorsTex_RGB1700_g46138 * staticSwitch1_g46201 ) );
			half Global_Colors_Value3650_g46138 = _GlobalColors;
			float4 tex2DNode35_g46138 = SAMPLE_TEXTURE2D( _MainMaskTex, sampler_MainAlbedoTex, Main_UVs15_g46138 );
			half Main_Mask57_g46138 = tex2DNode35_g46138.b;
			float temp_output_7_0_g46202 = _ColorsMaskMinValue;
			half Global_Colors_Mask3692_g46138 = saturate( ( ( Main_Mask57_g46138 - temp_output_7_0_g46202 ) / ( _ColorsMaskMaxValue - temp_output_7_0_g46202 ) ) );
			float3 lerpResult3628_g46138 = lerp( Blend_AlbedoTinted2808_g46138 , ( lerpResult3618_g46138 * Global_Colors1954_g46138 ) , ( Global_Colors_Value3650_g46138 * Global_Colors_Mask3692_g46138 ));
			half3 Blend_AlbedoColored863_g46138 = lerpResult3628_g46138;
			float3 temp_output_799_0_g46138 = (_SubsurfaceColor).rgb;
			float dotResult3930_g46138 = dot( temp_output_799_0_g46138 , float3(0.2126,0.7152,0.0722) );
			float3 temp_cast_4 = (dotResult3930_g46138).xxx;
			float3 lerpResult3932_g46138 = lerp( temp_output_799_0_g46138 , temp_cast_4 , Global_Colors_Influence3668_g46138);
			float3 lerpResult3942_g46138 = lerp( temp_output_799_0_g46138 , ( lerpResult3932_g46138 * Global_Colors1954_g46138 ) , ( Global_Colors_Value3650_g46138 * Global_Colors_Mask3692_g46138 ));
			half3 Subsurface_Color1722_g46138 = lerpResult3942_g46138;
			half MainLight_Subsurface4041_g46138 = TVE_MainLightParams.a;
			half Subsurface_Intensity1752_g46138 = ( _SubsurfaceValue * MainLight_Subsurface4041_g46138 );
			float temp_output_7_0_g46199 = _SubsurfaceMaskMinValue;
			half Subsurface_Mask1557_g46138 = saturate( ( ( Main_Mask57_g46138 - temp_output_7_0_g46199 ) / ( _SubsurfaceMaskMaxValue - temp_output_7_0_g46199 ) ) );
			half3 Subsurface_Transmission884_g46138 = ( Subsurface_Color1722_g46138 * Subsurface_Intensity1752_g46138 * Subsurface_Mask1557_g46138 );
			half3 MainLight_Direction3926_g46138 = TVE_MainLightDirection;
			float3 ase_worldPos = i.worldPos;
			float3 normalizeResult2169_g46138 = normalize( ( _WorldSpaceCameraPos - ase_worldPos ) );
			float3 ViewDir_Normalized3963_g46138 = normalizeResult2169_g46138;
			float dotResult785_g46138 = dot( -MainLight_Direction3926_g46138 , ViewDir_Normalized3963_g46138 );
			float saferPower1624_g46138 = max( (dotResult785_g46138*0.5 + 0.5) , 0.0001 );
			#ifdef UNITY_PASS_FORWARDADD
				float staticSwitch1602_g46138 = 0.0;
			#else
				float staticSwitch1602_g46138 = ( pow( saferPower1624_g46138 , _MainLightAngleValue ) * _MainLightScatteringValue );
			#endif
			half Mask_Subsurface_View782_g46138 = staticSwitch1602_g46138;
			half3 Subsurface_Deferred1693_g46138 = ( Subsurface_Transmission884_g46138 * Mask_Subsurface_View782_g46138 * Blend_AlbedoColored863_g46138 );
			half3 Blend_AlbedoAndSubsurface149_g46138 = ( Blend_AlbedoColored863_g46138 + Subsurface_Deferred1693_g46138 );
			half3 Global_OverlayColor1758_g46138 = (TVE_OverlayColor).rgb;
			half Main_AlbedoTex_G3526_g46138 = tex2DNode29_g46138.g;
			float3 Position82_g46167 = PositionWS_PerVertex3905_g46138;
			half4 Vegetation33_g46174 = SAMPLE_TEXTURE2D( TVE_ExtrasTex_Vegetation, samplerTVE_ExtrasTex_Vegetation, ( (TVE_ExtrasCoord_Vegetation).zw + ( (TVE_ExtrasCoord_Vegetation).xy * (Position82_g46167).xz ) ) );
			half4 Grass33_g46174 = SAMPLE_TEXTURE2D( TVE_ExtrasTex_Grass, samplerTVE_ExtrasTex_Grass, ( (TVE_ExtrasCoord_Grass).zw + ( (TVE_ExtrasCoord_Grass).xy * (Position82_g46167).xz ) ) );
			half4 Objects33_g46174 = SAMPLE_TEXTURE2D( TVE_ExtrasTex_Objects, samplerTVE_ExtrasTex_Objects, ( (TVE_ExtrasCoord_Objects).zw + ( (TVE_ExtrasCoord_Objects).xy * (Position82_g46167).xz ) ) );
			half4 localUSE_BUFFERS33_g46174 = USE_BUFFERS( Vegetation33_g46174 , Grass33_g46174 , Objects33_g46174 );
			float4 break49_g46167 = localUSE_BUFFERS33_g46174;
			half Global_Extras_Overlay156_g46138 = break49_g46167.z;
			float temp_output_1025_0_g46138 = ( _GlobalOverlay * Global_Extras_Overlay156_g46138 );
			half Overlay_Commons1365_g46138 = temp_output_1025_0_g46138;
			float temp_output_7_0_g46224 = _OverlayMaskMinValue;
			half Overlay_Mask269_g46138 = saturate( ( ( ( ( 0.5 + Main_AlbedoTex_G3526_g46138 ) * Overlay_Commons1365_g46138 ) - temp_output_7_0_g46224 ) / ( _OverlayMaskMaxValue - temp_output_7_0_g46224 ) ) );
			float3 lerpResult336_g46138 = lerp( Blend_AlbedoAndSubsurface149_g46138 , Global_OverlayColor1758_g46138 , Overlay_Mask269_g46138);
			half3 Final_Albedo359_g46138 = lerpResult336_g46138;
			float Main_Alpha316_g46138 = ( _MainColor.a * tex2DNode29_g46138.a );
			float lerpResult354_g46138 = lerp( 1.0 , Main_Alpha316_g46138 , _render_premul);
			half Final_Premultiply355_g46138 = lerpResult354_g46138;
			float3 temp_output_410_0_g46138 = ( Final_Albedo359_g46138 * Final_Premultiply355_g46138 );
			o.Albedo = temp_output_410_0_g46138;
			float3 temp_cast_8 = (( 0.04 * _RenderSpecular )).xxx;
			o.Specular = temp_cast_8;
			half Main_Smoothness227_g46138 = ( tex2DNode35_g46138.a * _MainSmoothnessValue );
			half Blend_Smoothness314_g46138 = Main_Smoothness227_g46138;
			half Global_OverlaySmoothness311_g46138 = TVE_OverlaySmoothness;
			float lerpResult343_g46138 = lerp( Blend_Smoothness314_g46138 , Global_OverlaySmoothness311_g46138 , Overlay_Mask269_g46138);
			half Final_Smoothness371_g46138 = lerpResult343_g46138;
			half Global_Extras_Wetness305_g46138 = break49_g46167.y;
			float lerpResult3673_g46138 = lerp( 0.0 , Global_Extras_Wetness305_g46138 , _GlobalWetness);
			o.Smoothness = saturate( ( Final_Smoothness371_g46138 + lerpResult3673_g46138 ) );
			float lerpResult240_g46138 = lerp( 1.0 , tex2DNode35_g46138.g , _MainOcclusionValue);
			half Main_Occlusion247_g46138 = lerpResult240_g46138;
			half Blend_Occlusion323_g46138 = Main_Occlusion247_g46138;
			o.Occlusion = Blend_Occlusion323_g46138;
			float localCustomAlphaClip3735_g46138 = ( 0.0 );
			float3 normalizeResult3971_g46138 = normalize( cross( ddy( ase_worldPos ) , ddx( ase_worldPos ) ) );
			float3 NormalsWS_Derivates3972_g46138 = normalizeResult3971_g46138;
			float dotResult2161_g46138 = dot( ViewDir_Normalized3963_g46138 , NormalsWS_Derivates3972_g46138 );
			float dotResult2212_g46138 = dot( ViewDir_Normalized3963_g46138 , float3(0,1,0) );
			half Mask_HView2656_g46138 = dotResult2212_g46138;
			float lerpResult2221_g46138 = lerp( _FadeHorizontalValue , _FadeVerticalValue , Mask_HView2656_g46138);
			float lerpResult3992_g46138 = lerp( 1.0 , abs( dotResult2161_g46138 ) , lerpResult2221_g46138);
			half Fade_Billboard2175_g46138 = lerpResult3992_g46138;
			half Final_AlphaFade3727_g46138 = ( 1.0 * Fade_Billboard2175_g46138 );
			float temp_output_41_0_g46144 = Final_AlphaFade3727_g46138;
			half AlphaTreshold2132_g46138 = _Cutoff;
			#ifdef TVE_ALPHA_CLIP
				float staticSwitch3792_g46138 = ( Main_Alpha316_g46138 - ( AlphaTreshold2132_g46138 - 0.5 ) );
			#else
				float staticSwitch3792_g46138 = Main_Alpha316_g46138;
			#endif
			half Final_Alpha3754_g46138 = staticSwitch3792_g46138;
			float temp_output_661_0_g46138 = ( saturate( ( temp_output_41_0_g46144 + ( temp_output_41_0_g46144 * SAMPLE_TEXTURE3D( TVE_ScreenTex3D, samplerTVE_ScreenTex3D, ( TVE_ScreenTexCoord * PositionWS_PerVertex3905_g46138 ) ).r ) ) ) * Final_Alpha3754_g46138 );
			float Alpha3735_g46138 = temp_output_661_0_g46138;
			float Treshold3735_g46138 = 0.5;
			{
			#if TVE_ALPHA_CLIP
				clip(Alpha3735_g46138 - Treshold3735_g46138);
			#endif
			}
			half Final_Clip914_g46138 = saturate( Alpha3735_g46138 );
			o.Alpha = Final_Clip914_g46138;
		}

		ENDCG
	}
	Fallback "Hidden/BOXOPHOBIC/The Vegetation Engine/Fallback"
	CustomEditor "TVEShaderCoreGUI"
}
/*ASEBEGIN
Version=18806
1920;1;1906;1021;2760.305;964.25;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;297;-1728,-896;Half;False;Property;_IsBalancedShader;_IsBalancedShader;239;1;[HideInInspector];Create;True;0;0;0;True;0;False;1;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-1984,-768;Half;False;Property;_render_src;_render_src;242;1;[HideInInspector];Create;True;0;0;0;True;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-1792,-768;Half;False;Property;_render_dst;_render_dst;243;1;[HideInInspector];Create;True;0;2;Opaque;0;Transparent;1;0;True;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;10;-2176,-768;Half;False;Property;_render_cull;_render_cull;241;1;[HideInInspector];Create;True;0;3;Both;0;Back;1;Front;2;0;True;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;268;-2176,-896;Half;False;Property;_IsAnyPathShader;_IsAnyPathShader;236;1;[HideInInspector];Create;True;0;0;0;True;0;False;1;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;81;-2176,-1024;Half;False;Property;_IsCrossShader;_IsCrossShader;237;1;[HideInInspector];Create;True;0;0;0;True;0;False;1;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;129;-1968,-1024;Half;False;Property;_IsStandardShader;_IsStandardShader;235;1;[HideInInspector];Create;True;0;0;0;True;0;False;1;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;31;-2176,-1152;Half;False;Property;_Banner;Banner;0;0;Create;True;0;0;0;True;1;StyledBanner(Cross Standard Lit (Deferred));False;0;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;289;-1744,-1024;Half;False;Property;_IsSubsurfaceApproxShader;_IsSubsurfaceApproxShader;240;1;[HideInInspector];Create;True;0;0;0;True;0;False;1;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;274;-2176,256;Inherit;False;Define TVE_IS_VEGETATION_SHADER;-1;;46240;b458122dd75182d488380bd0f592b9e6;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-1600,-768;Half;False;Property;_render_zw;_render_zw;244;1;[HideInInspector];Create;True;0;2;Opaque;0;Transparent;1;0;True;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;287;-2176,-384;Inherit;False;Base Shader;1;;46138;856f7164d1c579d43a5cf4968a75ca43;72,3882,1,3880,1,3957,1,4028,1,4029,1,3904,1,3903,1,3900,1,3908,1,1300,1,1298,1,3586,0,1271,0,3889,1,3658,1,1708,1,3509,1,1712,2,3873,0,1714,1,1717,1,1718,1,1715,1,916,0,1762,0,1763,0,3568,0,1949,1,1776,0,3475,1,893,0,1745,1,3479,0,3501,1,3221,2,1646,1,1690,1,1757,0,3960,0,2807,1,3886,0,2953,1,3887,0,3243,0,3888,0,3728,0,3949,0,2172,0,3883,1,2658,0,1742,1,3484,0,1735,0,3575,0,1734,0,1736,0,1733,0,1737,0,878,0,1550,0,860,1,3544,1,2261,1,2260,1,2054,0,2032,0,2060,0,2036,0,2039,0,2062,0,3592,1,2750,0;0;15;FLOAT3;0;FLOAT3;528;FLOAT3;2489;FLOAT;3678;FLOAT;529;FLOAT;530;FLOAT;531;FLOAT;1235;FLOAT3;1230;FLOAT;1461;FLOAT;1290;FLOAT;721;FLOAT;532;FLOAT;629;FLOAT3;534
Node;AmplifyShaderEditor.RangedFloatNode;290;-1952,-896;Half;False;Property;_IsTarget40Shader;_IsTarget40Shader;238;1;[HideInInspector];Create;True;0;0;0;True;0;False;1;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;285;-2176,320;Inherit;False;Define TVE_USE_VEGETATION_BUFFERS;-1;;46241;1ad73017b051a444d8dd4dba6e00b9ca;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;298;-1376,-384;Float;False;True;-1;4;TVEShaderCoreGUI;0;0;StandardSpecular;BOXOPHOBIC/The Vegetation Engine/Vegetation/Cross Standard Lit (Deferred);False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;False;True;Back;0;True;17;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;1;0;True;20;0;True;7;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Absolute;0;Hidden/BOXOPHOBIC/The Vegetation Engine/Fallback;245;-1;-1;-1;0;False;0;0;True;10;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;True;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
Node;AmplifyShaderEditor.CommentaryNode;37;-2176,-1280;Inherit;False;1023.392;100;Internal;0;;1,0.252,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;266;-2176,128;Inherit;False;1026.438;100;Features;0;;0,1,0.5,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;33;-2176,-512;Inherit;False;1024.392;100;Final;0;;0,1,0.5,1;0;0
WireConnection;298;0;287;0
WireConnection;298;1;287;528
WireConnection;298;3;287;3678
WireConnection;298;4;287;530
WireConnection;298;5;287;531
WireConnection;298;9;287;532
WireConnection;298;11;287;534
ASEEND*/
//CHKSM=ADEB111F1A0CEC3DCB0C7104FAC727BAD34B7EA6