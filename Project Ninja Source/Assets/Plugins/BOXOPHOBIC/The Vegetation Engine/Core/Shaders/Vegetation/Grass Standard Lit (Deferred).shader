// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "BOXOPHOBIC/The Vegetation Engine/Vegetation/Grass Standard Lit (Deferred)"
{
	Properties
	{
		[StyledBanner(Grass Standard Lit (Deferred))]_Banner("Banner", Float) = 0
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
		[StyledCategory(Global Settings)]_GlobalCat("[ Global Cat ]", Float) = 0
		[StyledMessage(Warning, Procedural Variation in use. The Variation might not work as expected when switching from one LOD to another., _VertexVariationMode, 1 , 0, 10)]_VariationGlobalsMessage("# Variation Globals Message", Float) = 0
		_GlobalColors("Global Colors", Range( 0 , 1)) = 1
		_GlobalOverlay("Global Overlay", Range( 0 , 1)) = 1
		_GlobalWetness("Global Wetness", Range( 0 , 1)) = 1
		_GlobalAlpha("Global Alpha", Range( 0 , 1)) = 1
		_GlobalSize("Global Size", Range( 0 , 1)) = 1
		[StyledRemapSlider(_ColorsMaskMinValue, _ColorsMaskMaxValue, 0, 1, 10, 0)]_ColorsMaskRemap("Colors Mask", Vector) = (0,0,0,0)
		[HideInInspector]_ColorsMaskMinValue("Colors Mask Min Value", Range( 0 , 1)) = 0
		[HideInInspector]_ColorsMaskMaxValue("Colors Mask Max Value", Range( 0 , 1)) = 1
		_ColorsVariationValue("Colors Variation", Range( 0 , 1)) = 0
		[StyledRemapSlider(_OverlayMaskMinValue, _OverlayMaskMaxValue, 0, 1, 10, 0)]_OverlayMaskRemap("Overlay Mask", Vector) = (0,0,0,0)
		[HideInInspector]_OverlayMaskMinValue("Overlay Mask Min Value", Range( 0 , 1)) = 0.45
		[HideInInspector]_OverlayMaskMaxValue("Overlay Mask Max Value", Range( 0 , 1)) = 0.55
		_OverlayVariationValue("Overlay Variation", Range( 0 , 1)) = 0
		[Space(10)]_AlphaVariationValue("Alpha Variation", Range( 0 , 1)) = 1
		[HideInInspector][HDR]_LocalColors("Local Colors", Color) = (1,1,1,1)
		[HideInInspector]_LocalAlpha("Local Alpha", Range( 0 , 1)) = 1
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
		[HDR]_VertexOcclusionColor("Vertex Occlusion Color", Color) = (1,1,1,1)
		[StyledRemapSlider(_VertexOcclusionMinValue, _VertexOcclusionMaxValue, 0, 1)]_VertexOcclusionRemap("Vertex Occlusion Mask", Vector) = (0,0,0,0)
		[HideInInspector]_VertexOcclusionMinValue("Vertex Occlusion Min Value", Range( 0 , 1)) = 0
		[HideInInspector]_VertexOcclusionMaxValue("Vertex Occlusion Max Value", Range( 0 , 1)) = 1
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
		_PerspectivePushValue("Perspective Push", Range( 0 , 4)) = 0
		_PerspectiveNoiseValue("Perspective Noise", Range( 0 , 4)) = 0
		_PerspectiveAngleValue("Perspective Angle", Range( 0 , 8)) = 1
		[StyledCategory(Size Fade Settings)]_SizeFadeCat("[ Size Fade Cat ]", Float) = 0
		[StyledMessage(Info, The Size Fade feature is recommended to be used to fade out vegetation at a distance in combination with the LOD Groups or with a 3rd party culling system., _SizeFadeMode, 1, 0, 10)]_SizeFadeMessage("# Size Fade Message", Float) = 0
		[Enum(Off,0,On,1)]_SizeFadeMode("Size Fade Mode", Float) = 0
		_SizeFadeStartValue("Size Fade Start", Float) = 200
		_SizeFadeEndValue("Size Fade End", Float) = 300
		[StyledCategory(Motion Settings)]_MotionCat("[ Motion Cat ]", Float) = 0
		[StyledMessage(Info, The Baked pivots feature allows for using per mesh element interaction and elements influence. This feature requires pre baked pivots on prefab conversion. Useful for latge grass meshes., _VertexPivotMode, 1 , 0, 10)]_PivotsMessage("# Pivots Message", Float) = 0
		[Enum(Single Pivot,0,Baked Pivots,1)]_VertexPivotMode("Vertex Pivot", Float) = 0
		[HDR][Space(10)]_MotionHighlightColor("Motion Highlight", Color) = (2,2,2,1)
		[StyledSpace(10)]_MotionSpace("# Motion Space", Float) = 0
		[StyledMessage(Warning, Procedural variation in use. Use the Scale settings if the Variation is breaking the bending and rolling animation., _VertexVariationMode, 1 , 0, 10)]_VariationMotionMessage("# Variation Motion Message", Float) = 0
		_MotionAmplitude_10("Bending Amplitude", Range( 0 , 2)) = 0.05
		[IntRange]_MotionSpeed_10("Bending Speed", Range( 0 , 60)) = 2
		_MotionScale_10("Bending Scale", Range( 0 , 20)) = 0
		_MotionVariation_10("Bending Variation", Range( 0 , 20)) = 0
		[Space(10)]_MotionAmplitude_32("Flutter Amplitude", Range( 0 , 2)) = 0.2
		[IntRange]_MotionSpeed_32("Flutter Speed", Range( 0 , 60)) = 20
		_MotionScale_32("Flutter Scale", Range( 0 , 20)) = 2
		_MotionVariation_32("Flutter Variation", Range( 0 , 20)) = 2
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
		[HideInInspector]_IsGrassShader("_IsGrassShader", Float) = 1
		[HideInInspector]_IsStandardShader("_IsStandardShader", Float) = 1
		[HideInInspector]_IsAnyPathShader("_IsAnyPathShader", Float) = 1
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
		#define TVE_IS_GRASS_SHADER
		#define TVE_USE_GRASS_BUFFERS
		  
		#define THE_VEGETATION_ENGINE
		    
		//SHADER INJECTION POINT BEGIN
		//SHADER INJECTION POINT END
		      
		#define TVE_PIVOT_DATA_BAKED
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
			float2 vertexToFrag11_g46543;
			half ASEVFace : VFACE;
			float3 vertexToFrag11_g46564;
			float3 vertexToFrag11_g46563;
			float3 vertexToFrag11_g46569;
			float vertexToFrag11_g46542;
			float3 vertexToFrag3890_g46494;
			float4 vertexColor : COLOR;
			float3 vertexToFrag11_g46550;
		};

		uniform half _render_cull;
		uniform half _render_zw;
		uniform half _IsTarget40Shader;
		uniform half _IsSubsurfaceApproxShader;
		uniform half _IsStandardShader;
		uniform half _render_src;
		uniform half _IsGrassShader;
		uniform half _IsBalancedShader;
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
		uniform half _IsAnyPathShader;
		uniform half _Banner;
		uniform half _VertexPivotMode;
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
		uniform float _MotionScale_32;
		uniform float _MotionSpeed_32;
		uniform float _MotionVariation_32;
		uniform half _MotionAmplitude_32;
		uniform half TVE_MotionFadeEnd;
		uniform half TVE_MotionFadeStart;
		uniform half _VertexDataMode;
		uniform half _GlobalSize;
		uniform half _LocalSize;
		uniform half TVE_DistanceFadeBias;
		uniform half _SizeFadeEndValue;
		uniform half _SizeFadeStartValue;
		uniform half _SizeFadeMode;
		uniform half _PerspectivePushValue;
		uniform half _PerspectiveNoiseValue;
		uniform half _PerspectiveAngleValue;
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
		uniform half4 _MotionHighlightColor;
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
		uniform half _ColorsVariationValue;
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
		uniform half _OverlayVariationValue;
		uniform half _OverlayMaskMinValue;
		uniform half _OverlayMaskMaxValue;
		uniform half _render_premul;
		uniform half4 _VertexOcclusionColor;
		uniform half _VertexOcclusionMinValue;
		uniform half _VertexOcclusionMaxValue;
		uniform half _RenderSpecular;
		UNITY_DECLARE_TEX2D_NOSAMPLER(_MainMaskTex);
		uniform half _MainSmoothnessValue;
		uniform half TVE_OverlaySmoothness;
		uniform half _GlobalWetness;
		uniform half _MainOcclusionValue;
		UNITY_DECLARE_TEX3D_NOSAMPLER(TVE_ScreenTex3D);
		uniform half TVE_ScreenTexCoord;
		SamplerState samplerTVE_ScreenTex3D;
		uniform half _AlphaVariationValue;
		uniform half _GlobalAlpha;
		uniform half _LocalAlpha;


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
			float3 PositionOS3588_g46494 = ase_vertex3Pos;
			half3 _Vector1 = half3(0,0,0);
			half3 Off19_g46497 = _Vector1;
			float3 appendResult2827_g46494 = (float3(v.texcoord.z , v.texcoord3.w , v.texcoord.w));
			half3 Mesh_PivotsData2831_g46494 = ( appendResult2827_g46494 * _VertexPivotMode );
			half3 On20_g46497 = Mesh_PivotsData2831_g46494;
			#ifdef TVE_PIVOT_DATA_BAKED
				float3 staticSwitch14_g46497 = On20_g46497;
			#else
				float3 staticSwitch14_g46497 = Off19_g46497;
			#endif
			half3 ObjectData20_g46498 = staticSwitch14_g46497;
			half3 WorldData19_g46498 = Off19_g46497;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float3 staticSwitch14_g46498 = WorldData19_g46498;
			#else
				float3 staticSwitch14_g46498 = ObjectData20_g46498;
			#endif
			half3 Mesh_PivotsOS2291_g46494 = staticSwitch14_g46498;
			float3 temp_output_2283_0_g46494 = ( PositionOS3588_g46494 - Mesh_PivotsOS2291_g46494 );
			half3 VertexPos40_g46567 = temp_output_2283_0_g46494;
			float3 appendResult74_g46567 = (float3(VertexPos40_g46567.x , 0.0 , 0.0));
			half3 VertexPosRotationAxis50_g46567 = appendResult74_g46567;
			float3 break84_g46567 = VertexPos40_g46567;
			float3 appendResult81_g46567 = (float3(0.0 , break84_g46567.y , break84_g46567.z));
			half3 VertexPosOtherAxis82_g46567 = appendResult81_g46567;
			float ObjectData20_g46560 = 3.14;
			float Bounds_Height374_g46494 = _MaxBoundsInfo.y;
			float WorldData19_g46560 = ( Bounds_Height374_g46494 * 3.14 );
			#ifdef TVE_VERTEX_DATA_BATCHED
				float staticSwitch14_g46560 = WorldData19_g46560;
			#else
				float staticSwitch14_g46560 = ObjectData20_g46560;
			#endif
			float Motion_Max_Bending1133_g46494 = staticSwitch14_g46560;
			float4x4 break19_g46506 = unity_ObjectToWorld;
			float3 appendResult20_g46506 = (float3(break19_g46506[ 0 ][ 3 ] , break19_g46506[ 1 ][ 3 ] , break19_g46506[ 2 ][ 3 ]));
			half3 Off19_g46507 = appendResult20_g46506;
			float4 ase_vertex4Pos = v.vertex;
			float4 transform68_g46506 = mul(unity_ObjectToWorld,ase_vertex4Pos);
			float3 appendResult95_g46506 = (float3(v.texcoord.z , 0.0 , v.texcoord.w));
			float4 transform62_g46506 = mul(unity_ObjectToWorld,float4( ( ase_vertex3Pos - ( appendResult95_g46506 * _vertex_pivot_mode ) ) , 0.0 ));
			float3 ObjectPositionWithPivots28_g46506 = ( (transform68_g46506).xyz - (transform62_g46506).xyz );
			half3 On20_g46507 = ObjectPositionWithPivots28_g46506;
			#ifdef TVE_PIVOT_DATA_BAKED
				float3 staticSwitch14_g46507 = On20_g46507;
			#else
				float3 staticSwitch14_g46507 = Off19_g46507;
			#endif
			half3 ObjectData20_g46508 = staticSwitch14_g46507;
			half3 WorldData19_g46508 = Off19_g46507;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float3 staticSwitch14_g46508 = WorldData19_g46508;
			#else
				float3 staticSwitch14_g46508 = ObjectData20_g46508;
			#endif
			float3 temp_output_42_0_g46506 = staticSwitch14_g46508;
			half3 ObjectData20_g46512 = temp_output_42_0_g46506;
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			half3 WorldData19_g46512 = ase_worldPos;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float3 staticSwitch14_g46512 = WorldData19_g46512;
			#else
				float3 staticSwitch14_g46512 = ObjectData20_g46512;
			#endif
			float3 Position83_g46505 = staticSwitch14_g46512;
			half4 Vegetation33_g46511 = SAMPLE_TEXTURE2D_LOD( TVE_VertexTex_Vegetation, samplerTVE_VertexTex_Vegetation, ( (TVE_VertexCoord_Vegetation).zw + ( (TVE_VertexCoord_Vegetation).xy * (Position83_g46505).xz ) ), 0.0 );
			half4 Grass33_g46511 = SAMPLE_TEXTURE2D_LOD( TVE_VertexTex_Grass, samplerTVE_VertexTex_Grass, ( (TVE_VertexCoord_Grass).zw + ( (TVE_VertexCoord_Grass).xy * (Position83_g46505).xz ) ), 0.0 );
			half4 Objects33_g46511 = SAMPLE_TEXTURE2D_LOD( TVE_VertexTex_Objects, samplerTVE_VertexTex_Objects, ( (TVE_VertexCoord_Objects).zw + ( (TVE_VertexCoord_Objects).xy * (Position83_g46505).xz ) ), 0.0 );
			half4 localUSE_BUFFERS33_g46511 = USE_BUFFERS( Vegetation33_g46511 , Grass33_g46511 , Objects33_g46511 );
			half4 Global_Motion_Params3909_g46494 = localUSE_BUFFERS33_g46511;
			float4 break322_g46573 = Global_Motion_Params3909_g46494;
			half Wind_Power369_g46573 = break322_g46573.z;
			float temp_output_7_0_g46575 = 0.5;
			float temp_output_404_0_g46573 = (Wind_Power369_g46573*2.0 + -1.0);
			float temp_output_406_0_g46573 = saturate( sign( temp_output_404_0_g46573 ) );
			float lerpResult401_g46573 = lerp( 0.0 , ( ( Wind_Power369_g46573 - temp_output_7_0_g46575 ) / ( 1.0 - temp_output_7_0_g46575 ) ) , temp_output_406_0_g46573);
			float lerpResult376_g46573 = lerp( 0.1 , 1.0 , lerpResult401_g46573);
			half Wind_Power_103106_g46494 = lerpResult376_g46573;
			float3 appendResult397_g46573 = (float3(break322_g46573.x , 0.0 , break322_g46573.y));
			float3 temp_output_398_0_g46573 = (appendResult397_g46573*2.0 + -1.0);
			float3 ase_parentObjectScale = (1.0/float3( length( unity_WorldToObject[ 0 ].xyz ), length( unity_WorldToObject[ 1 ].xyz ), length( unity_WorldToObject[ 2 ].xyz ) ));
			float3 temp_output_339_0_g46573 = ( mul( unity_WorldToObject, float4( temp_output_398_0_g46573 , 0.0 ) ).xyz * ase_parentObjectScale );
			half2 Wind_DirectionOS39_g46494 = (temp_output_339_0_g46573).xz;
			#ifdef TVE_IS_GRASS_SHADER
				float2 staticSwitch160_g46581 = TVE_NoiseSpeed_Grass;
			#else
				float2 staticSwitch160_g46581 = TVE_NoiseSpeed_Vegetation;
			#endif
			float4x4 break19_g46583 = unity_ObjectToWorld;
			float3 appendResult20_g46583 = (float3(break19_g46583[ 0 ][ 3 ] , break19_g46583[ 1 ][ 3 ] , break19_g46583[ 2 ][ 3 ]));
			half3 Off19_g46584 = appendResult20_g46583;
			float4 transform68_g46583 = mul(unity_ObjectToWorld,ase_vertex4Pos);
			float3 appendResult95_g46583 = (float3(v.texcoord.z , 0.0 , v.texcoord.w));
			float4 transform62_g46583 = mul(unity_ObjectToWorld,float4( ( ase_vertex3Pos - ( appendResult95_g46583 * _vertex_pivot_mode ) ) , 0.0 ));
			float3 ObjectPositionWithPivots28_g46583 = ( (transform68_g46583).xyz - (transform62_g46583).xyz );
			half3 On20_g46584 = ObjectPositionWithPivots28_g46583;
			#ifdef TVE_PIVOT_DATA_BAKED
				float3 staticSwitch14_g46584 = On20_g46584;
			#else
				float3 staticSwitch14_g46584 = Off19_g46584;
			#endif
			half3 ObjectData20_g46585 = staticSwitch14_g46584;
			half3 WorldData19_g46585 = Off19_g46584;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float3 staticSwitch14_g46585 = WorldData19_g46585;
			#else
				float3 staticSwitch14_g46585 = ObjectData20_g46585;
			#endif
			float3 temp_output_42_0_g46583 = staticSwitch14_g46585;
			half3 ObjectData20_g46582 = temp_output_42_0_g46583;
			half3 WorldData19_g46582 = ase_worldPos;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float3 staticSwitch14_g46582 = WorldData19_g46582;
			#else
				float3 staticSwitch14_g46582 = ObjectData20_g46582;
			#endif
			#ifdef TVE_IS_GRASS_SHADER
				float2 staticSwitch164_g46581 = (ase_worldPos).xz;
			#else
				float2 staticSwitch164_g46581 = (staticSwitch14_g46582).xz;
			#endif
			#ifdef TVE_IS_GRASS_SHADER
				float staticSwitch161_g46581 = TVE_NoiseSize_Grass;
			#else
				float staticSwitch161_g46581 = TVE_NoiseSize_Vegetation;
			#endif
			float2 panner73_g46581 = ( _Time.y * staticSwitch160_g46581 + ( staticSwitch164_g46581 * staticSwitch161_g46581 ));
			float4 tex2DNode75_g46581 = SAMPLE_TEXTURE2D_LOD( TVE_NoiseTex, samplerTVE_NoiseTex, panner73_g46581, 0.0 );
			float4 saferPower77_g46581 = max( abs( tex2DNode75_g46581 ) , 0.0001 );
			half Wind_Power2223_g46494 = lerpResult401_g46573;
			float temp_output_167_0_g46581 = Wind_Power2223_g46494;
			float lerpResult168_g46581 = lerp( 1.5 , 0.25 , temp_output_167_0_g46581);
			float4 temp_cast_7 = (lerpResult168_g46581).xxxx;
			float4 break142_g46581 = pow( saferPower77_g46581 , temp_cast_7 );
			half Global_NoiseTex_R34_g46494 = break142_g46581.r;
			half Input_Speed62_g46570 = _MotionSpeed_10;
			float mulTime373_g46570 = _Time.y * Input_Speed62_g46570;
			float4x4 break19_g46518 = unity_ObjectToWorld;
			float3 appendResult20_g46518 = (float3(break19_g46518[ 0 ][ 3 ] , break19_g46518[ 1 ][ 3 ] , break19_g46518[ 2 ][ 3 ]));
			half3 Off19_g46519 = appendResult20_g46518;
			float4 transform68_g46518 = mul(unity_ObjectToWorld,ase_vertex4Pos);
			float3 appendResult95_g46518 = (float3(v.texcoord.z , 0.0 , v.texcoord.w));
			float4 transform62_g46518 = mul(unity_ObjectToWorld,float4( ( ase_vertex3Pos - ( appendResult95_g46518 * _vertex_pivot_mode ) ) , 0.0 ));
			float3 ObjectPositionWithPivots28_g46518 = ( (transform68_g46518).xyz - (transform62_g46518).xyz );
			half3 On20_g46519 = ObjectPositionWithPivots28_g46518;
			#ifdef TVE_PIVOT_DATA_BAKED
				float3 staticSwitch14_g46519 = On20_g46519;
			#else
				float3 staticSwitch14_g46519 = Off19_g46519;
			#endif
			half3 ObjectData20_g46520 = staticSwitch14_g46519;
			half3 WorldData19_g46520 = Off19_g46519;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float3 staticSwitch14_g46520 = WorldData19_g46520;
			#else
				float3 staticSwitch14_g46520 = ObjectData20_g46520;
			#endif
			float3 temp_output_42_0_g46518 = staticSwitch14_g46520;
			float3 break9_g46518 = temp_output_42_0_g46518;
			half Variation_Complex102_g46516 = frac( ( v.color.r + ( break9_g46518.x + break9_g46518.z ) ) );
			float ObjectData20_g46517 = Variation_Complex102_g46516;
			half Variation_Simple105_g46516 = v.color.r;
			float WorldData19_g46517 = Variation_Simple105_g46516;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float staticSwitch14_g46517 = WorldData19_g46517;
			#else
				float staticSwitch14_g46517 = ObjectData20_g46517;
			#endif
			half Motion_Variation3073_g46494 = staticSwitch14_g46517;
			half Motion_Variation284_g46570 = ( _MotionVariation_10 * Motion_Variation3073_g46494 );
			float2 appendResult344_g46570 = (float2(ase_worldPos.x , ase_worldPos.z));
			float2 Motion_Scale287_g46570 = ( _MotionScale_10 * appendResult344_g46570 );
			half2 Sine_MinusOneToOne281_g46570 = sin( ( mulTime373_g46570 + Motion_Variation284_g46570 + Motion_Scale287_g46570 ) );
			float2 temp_cast_9 = (1.0).xx;
			half Input_Turbulence327_g46570 = Global_NoiseTex_R34_g46494;
			float2 lerpResult321_g46570 = lerp( Sine_MinusOneToOne281_g46570 , temp_cast_9 , Input_Turbulence327_g46570);
			half2 Motion_Bending2258_g46494 = ( ( _MotionAmplitude_10 * Motion_Max_Bending1133_g46494 ) * Wind_Power_103106_g46494 * Wind_DirectionOS39_g46494 * Global_NoiseTex_R34_g46494 * lerpResult321_g46570 );
			float lerpResult402_g46573 = lerp( abs( temp_output_404_0_g46573 ) , 0.0 , temp_output_406_0_g46573);
			half Motion_InteractionMask66_g46494 = lerpResult402_g46573;
			float lerpResult3307_g46494 = lerp( 1.0 , Motion_Variation3073_g46494 , _InteractionVariation);
			half2 Motion_Interaction53_g46494 = ( _InteractionAmplitude * Motion_Max_Bending1133_g46494 * Motion_InteractionMask66_g46494 * Motion_InteractionMask66_g46494 * Wind_DirectionOS39_g46494 * lerpResult3307_g46494 );
			float2 lerpResult109_g46494 = lerp( Motion_Bending2258_g46494 , Motion_Interaction53_g46494 , Motion_InteractionMask66_g46494);
			half Mesh_Motion_182_g46494 = v.texcoord3.x;
			float2 break143_g46494 = ( lerpResult109_g46494 * Mesh_Motion_182_g46494 );
			half Motion_Z190_g46494 = break143_g46494.y;
			half Angle44_g46567 = Motion_Z190_g46494;
			half3 VertexPos40_g46503 = ( VertexPosRotationAxis50_g46567 + ( VertexPosOtherAxis82_g46567 * cos( Angle44_g46567 ) ) + ( cross( float3(1,0,0) , VertexPosOtherAxis82_g46567 ) * sin( Angle44_g46567 ) ) );
			float3 appendResult74_g46503 = (float3(0.0 , 0.0 , VertexPos40_g46503.z));
			half3 VertexPosRotationAxis50_g46503 = appendResult74_g46503;
			float3 break84_g46503 = VertexPos40_g46503;
			float3 appendResult81_g46503 = (float3(break84_g46503.x , break84_g46503.y , 0.0));
			half3 VertexPosOtherAxis82_g46503 = appendResult81_g46503;
			half Motion_X216_g46494 = break143_g46494.x;
			half Angle44_g46503 = -Motion_X216_g46494;
			half Motion_Scale321_g46571 = ( _MotionScale_32 * 10.0 );
			half Input_Speed62_g46571 = _MotionSpeed_32;
			float mulTime349_g46571 = _Time.y * Input_Speed62_g46571;
			float Motion_Variation330_g46571 = ( _MotionVariation_32 * Motion_Variation3073_g46494 );
			float Bounds_Radius121_g46494 = _MaxBoundsInfo.x;
			half Input_Amplitude58_g46571 = ( _MotionAmplitude_32 * Bounds_Radius121_g46494 * 0.1 );
			float temp_output_299_0_g46571 = ( sin( ( ( ( ase_worldPos.x + ase_worldPos.y + ase_worldPos.z ) * Motion_Scale321_g46571 ) + mulTime349_g46571 + Motion_Variation330_g46571 ) ) * Input_Amplitude58_g46571 );
			float3 appendResult354_g46571 = (float3(temp_output_299_0_g46571 , 0.0 , temp_output_299_0_g46571));
			half Global_NoiseTex_A139_g46494 = break142_g46581.a;
			half Mesh_Motion_3144_g46494 = v.texcoord3.z;
			half Wind_Power_323115_g46494 = lerpResult376_g46573;
			float temp_output_7_0_g46515 = TVE_MotionFadeEnd;
			half Wind_FadeOut4005_g46494 = saturate( ( ( distance( ase_worldPos , _WorldSpaceCameraPos ) - temp_output_7_0_g46515 ) / ( TVE_MotionFadeStart - temp_output_7_0_g46515 ) ) );
			half3 Motion_Detail263_g46494 = ( appendResult354_g46571 * ( Global_NoiseTex_A139_g46494 * Mesh_Motion_3144_g46494 * Wind_Power_323115_g46494 ) * Wind_FadeOut4005_g46494 );
			float3 Vertex_Motion_Object833_g46494 = ( ( VertexPosRotationAxis50_g46503 + ( VertexPosOtherAxis82_g46503 * cos( Angle44_g46503 ) ) + ( cross( float3(0,0,1) , VertexPosOtherAxis82_g46503 ) * sin( Angle44_g46503 ) ) ) + Motion_Detail263_g46494 );
			float3 temp_output_3474_0_g46494 = ( PositionOS3588_g46494 - Mesh_PivotsOS2291_g46494 );
			float3 appendResult2043_g46494 = (float3(Motion_X216_g46494 , 0.0 , Motion_Z190_g46494));
			float3 Vertex_Motion_World1118_g46494 = ( ( temp_output_3474_0_g46494 + appendResult2043_g46494 ) + Motion_Detail263_g46494 );
			#ifdef TVE_VERTEX_DATA_BATCHED
				float3 staticSwitch3312_g46494 = Vertex_Motion_World1118_g46494;
			#else
				float3 staticSwitch3312_g46494 = ( Vertex_Motion_Object833_g46494 + ( 0.0 * _VertexDataMode ) );
			#endif
			half Global_Vertex_Size174_g46494 = break322_g46573.w;
			float lerpResult346_g46494 = lerp( 1.0 , Global_Vertex_Size174_g46494 , _GlobalSize);
			float temp_output_2626_0_g46494 = ( lerpResult346_g46494 * _LocalSize );
			float3 appendResult3480_g46494 = (float3(temp_output_2626_0_g46494 , temp_output_2626_0_g46494 , temp_output_2626_0_g46494));
			half3 ObjectData20_g46544 = appendResult3480_g46494;
			half3 _Vector11 = half3(1,1,1);
			half3 WorldData19_g46544 = _Vector11;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float3 staticSwitch14_g46544 = WorldData19_g46544;
			#else
				float3 staticSwitch14_g46544 = ObjectData20_g46544;
			#endif
			half3 Vertex_Size1741_g46494 = staticSwitch14_g46544;
			half3 _Vector5 = half3(1,1,1);
			float4x4 break19_g46545 = unity_ObjectToWorld;
			float3 appendResult20_g46545 = (float3(break19_g46545[ 0 ][ 3 ] , break19_g46545[ 1 ][ 3 ] , break19_g46545[ 2 ][ 3 ]));
			half3 Off19_g46546 = appendResult20_g46545;
			float4 transform68_g46545 = mul(unity_ObjectToWorld,ase_vertex4Pos);
			float3 appendResult95_g46545 = (float3(v.texcoord.z , 0.0 , v.texcoord.w));
			float4 transform62_g46545 = mul(unity_ObjectToWorld,float4( ( ase_vertex3Pos - ( appendResult95_g46545 * _vertex_pivot_mode ) ) , 0.0 ));
			float3 ObjectPositionWithPivots28_g46545 = ( (transform68_g46545).xyz - (transform62_g46545).xyz );
			half3 On20_g46546 = ObjectPositionWithPivots28_g46545;
			#ifdef TVE_PIVOT_DATA_BAKED
				float3 staticSwitch14_g46546 = On20_g46546;
			#else
				float3 staticSwitch14_g46546 = Off19_g46546;
			#endif
			half3 ObjectData20_g46547 = staticSwitch14_g46546;
			half3 WorldData19_g46547 = Off19_g46546;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float3 staticSwitch14_g46547 = WorldData19_g46547;
			#else
				float3 staticSwitch14_g46547 = ObjectData20_g46547;
			#endif
			float3 temp_output_42_0_g46545 = staticSwitch14_g46547;
			float temp_output_7_0_g46551 = _SizeFadeEndValue;
			float temp_output_335_0_g46494 = saturate( ( ( ( distance( _WorldSpaceCameraPos , temp_output_42_0_g46545 ) * ( 1.0 / TVE_DistanceFadeBias ) ) - temp_output_7_0_g46551 ) / ( _SizeFadeStartValue - temp_output_7_0_g46551 ) ) );
			float3 appendResult3482_g46494 = (float3(temp_output_335_0_g46494 , temp_output_335_0_g46494 , temp_output_335_0_g46494));
			float3 lerpResult3556_g46494 = lerp( _Vector5 , appendResult3482_g46494 , _SizeFadeMode);
			half3 ObjectData20_g46556 = lerpResult3556_g46494;
			half3 WorldData19_g46556 = _Vector5;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float3 staticSwitch14_g46556 = WorldData19_g46556;
			#else
				float3 staticSwitch14_g46556 = ObjectData20_g46556;
			#endif
			float3 Vertex_SizeFade1740_g46494 = staticSwitch14_g46556;
			float3 normalizeResult2696_g46494 = normalize( ( _WorldSpaceCameraPos - ase_worldPos ) );
			float3 break2709_g46494 = cross( normalizeResult2696_g46494 , half3(0,1,0) );
			float3 appendResult2710_g46494 = (float3(-break2709_g46494.z , 0.0 , break2709_g46494.x));
			float3 appendResult2667_g46494 = (float3(v.color.r , 0.5 , v.color.r));
			float3 normalizeResult2169_g46494 = normalize( ( _WorldSpaceCameraPos - ase_worldPos ) );
			float3 ViewDir_Normalized3963_g46494 = normalizeResult2169_g46494;
			float dotResult2212_g46494 = dot( ViewDir_Normalized3963_g46494 , float3(0,1,0) );
			half Mask_HView2656_g46494 = dotResult2212_g46494;
			float saferPower2652_g46494 = max( Mask_HView2656_g46494 , 0.0001 );
			half3 Grass_Coverage2661_g46494 = ( ( ( mul( unity_WorldToObject, float4( appendResult2710_g46494 , 0.0 ) ).xyz * _PerspectivePushValue ) + ( (appendResult2667_g46494*2.0 + -1.0) * _PerspectiveNoiseValue ) ) * v.color.a * pow( saferPower2652_g46494 , _PerspectiveAngleValue ) );
			float3 Final_VertexPosition890_g46494 = ( ( staticSwitch3312_g46494 * Vertex_Size1741_g46494 * Vertex_SizeFade1740_g46494 ) + Mesh_PivotsOS2291_g46494 + Grass_Coverage2661_g46494 );
			v.vertex.xyz = Final_VertexPosition890_g46494;
			v.vertex.w = 1;
			o.vertexToFrag11_g46543 = ( ( v.texcoord.xy * (_MainUVs).xy ) + (_MainUVs).zw );
			float temp_output_7_0_g46502 = _GradientMinValue;
			float4 lerpResult2779_g46494 = lerp( _GradientColorTwo , _GradientColorOne , saturate( ( ( v.color.a - temp_output_7_0_g46502 ) / ( _GradientMaxValue - temp_output_7_0_g46502 ) ) ));
			half3 Gradient_Tint2784_g46494 = (lerpResult2779_g46494).rgb;
			o.vertexToFrag11_g46564 = Gradient_Tint2784_g46494;
			float3 temp_cast_13 = (_NoiseScaleValue).xxx;
			float3 PositionWS_PerVertex3905_g46494 = ase_worldPos;
			float temp_output_7_0_g46553 = _NoiseMinValue;
			half Noise_Mask3162_g46494 = saturate( ( ( SAMPLE_TEXTURE3D_LOD( TVE_WorldTex3D, samplerTVE_WorldTex3D, ( temp_cast_13 * PositionWS_PerVertex3905_g46494 * 0.1 ), 0.0 ).r - temp_output_7_0_g46553 ) / ( _NoiseMaxValue - temp_output_7_0_g46553 ) ) );
			float4 lerpResult2800_g46494 = lerp( _NoiseColorTwo , _NoiseColorOne , Noise_Mask3162_g46494);
			half3 Noise_Tint2802_g46494 = (lerpResult2800_g46494).rgb;
			o.vertexToFrag11_g46563 = Noise_Tint2802_g46494;
			float lerpResult169_g46581 = lerp( 4.0 , 2.0 , temp_output_167_0_g46581);
			half Global_NoiseTex_H2869_g46494 = pow( abs( tex2DNode75_g46581.r ) , lerpResult169_g46581 );
			half3 Highlight_Tint3231_g46494 = ( ( (_MotionHighlightColor).rgb * Global_NoiseTex_H2869_g46494 * Wind_Power_103106_g46494 * v.color.r ) + float3( 1,1,1 ) );
			o.vertexToFrag11_g46569 = Highlight_Tint3231_g46494;
			float3 Position58_g46588 = PositionWS_PerVertex3905_g46494;
			half4 Vegetation33_g46595 = SAMPLE_TEXTURE2D_LOD( TVE_ColorsTex_Vegetation, samplerTVE_ColorsTex_Vegetation, ( (TVE_ColorsCoord_Vegetation).zw + ( (TVE_ColorsCoord_Vegetation).xy * (Position58_g46588).xz ) ), 0.0 );
			half4 Grass33_g46595 = SAMPLE_TEXTURE2D_LOD( TVE_ColorsTex_Grass, samplerTVE_ColorsTex_Grass, ( (TVE_ColorsCoord_Grass).zw + ( (TVE_ColorsCoord_Grass).xy * (Position58_g46588).xz ) ), 0.0 );
			half4 Objects33_g46595 = SAMPLE_TEXTURE2D_LOD( TVE_ColorsTex_Objects, samplerTVE_ColorsTex_Objects, ( (TVE_ColorsCoord_Objects).zw + ( (TVE_ColorsCoord_Objects).xy * (Position58_g46588).xz ) ), 0.0 );
			half4 localUSE_BUFFERS33_g46595 = USE_BUFFERS( Vegetation33_g46595 , Grass33_g46595 , Objects33_g46595 );
			float4 temp_output_46_0_g46588 = localUSE_BUFFERS33_g46595;
			half Global_ColorsTex_A1701_g46494 = (temp_output_46_0_g46588).w;
			o.vertexToFrag11_g46542 = Global_ColorsTex_A1701_g46494;
			o.vertexToFrag3890_g46494 = ase_worldPos;
			float3 temp_cast_17 = (1.0).xxx;
			float Mesh_Occlusion318_g46494 = v.color.g;
			float temp_output_7_0_g46539 = _VertexOcclusionMinValue;
			float3 lerpResult2945_g46494 = lerp( (_VertexOcclusionColor).rgb , temp_cast_17 , saturate( ( ( Mesh_Occlusion318_g46494 - temp_output_7_0_g46539 ) / ( _VertexOcclusionMaxValue - temp_output_7_0_g46539 ) ) ));
			float3 Vertex_Occlusion648_g46494 = lerpResult2945_g46494;
			o.vertexToFrag11_g46550 = Vertex_Occlusion648_g46494;
		}

		void surf( Input i , inout SurfaceOutputStandardSpecular o )
		{
			half2 Main_UVs15_g46494 = i.vertexToFrag11_g46543;
			float4 tex2DNode117_g46494 = SAMPLE_TEXTURE2D( _MainNormalTex, sampler_MainAlbedoTex, Main_UVs15_g46494 );
			float2 appendResult88_g46513 = (float2(tex2DNode117_g46494.a , tex2DNode117_g46494.g));
			float2 temp_output_90_0_g46513 = ( (appendResult88_g46513*2.0 + -1.0) * _MainNormalValue );
			float3 appendResult91_g46513 = (float3(temp_output_90_0_g46513 , 1.0));
			half3 Main_Normal137_g46494 = appendResult91_g46513;
			float3 temp_output_13_0_g46559 = Main_Normal137_g46494;
			float3 switchResult12_g46559 = (((i.ASEVFace>0)?(temp_output_13_0_g46559):(( temp_output_13_0_g46559 * _render_normals_options ))));
			half3 Blend_Normal312_g46494 = switchResult12_g46559;
			half3 Final_Normal366_g46494 = Blend_Normal312_g46494;
			o.Normal = Final_Normal366_g46494;
			float4 tex2DNode29_g46494 = SAMPLE_TEXTURE2D( _MainAlbedoTex, sampler_MainAlbedoTex, Main_UVs15_g46494 );
			float3 temp_output_3639_0_g46494 = (tex2DNode29_g46494).rgb;
			float3 temp_output_51_0_g46494 = ( (_MainColor).rgb * temp_output_3639_0_g46494 );
			half3 Main_Albedo99_g46494 = temp_output_51_0_g46494;
			half3 Blend_Albedo265_g46494 = Main_Albedo99_g46494;
			half3 Blend_AlbedoTinted2808_g46494 = ( i.vertexToFrag11_g46564 * i.vertexToFrag11_g46563 * i.vertexToFrag11_g46569 * Blend_Albedo265_g46494 );
			float dotResult3616_g46494 = dot( Blend_AlbedoTinted2808_g46494 , float3(0.2126,0.7152,0.0722) );
			float3 temp_cast_0 = (dotResult3616_g46494).xxx;
			half Global_Colors_Influence3668_g46494 = i.vertexToFrag11_g46542;
			float3 lerpResult3618_g46494 = lerp( Blend_AlbedoTinted2808_g46494 , temp_cast_0 , Global_Colors_Influence3668_g46494);
			float3 PositionWS_PerVertex3905_g46494 = i.vertexToFrag3890_g46494;
			float3 Position58_g46588 = PositionWS_PerVertex3905_g46494;
			half4 Vegetation33_g46595 = SAMPLE_TEXTURE2D( TVE_ColorsTex_Vegetation, samplerTVE_ColorsTex_Vegetation, ( (TVE_ColorsCoord_Vegetation).zw + ( (TVE_ColorsCoord_Vegetation).xy * (Position58_g46588).xz ) ) );
			half4 Grass33_g46595 = SAMPLE_TEXTURE2D( TVE_ColorsTex_Grass, samplerTVE_ColorsTex_Grass, ( (TVE_ColorsCoord_Grass).zw + ( (TVE_ColorsCoord_Grass).xy * (Position58_g46588).xz ) ) );
			half4 Objects33_g46595 = SAMPLE_TEXTURE2D( TVE_ColorsTex_Objects, samplerTVE_ColorsTex_Objects, ( (TVE_ColorsCoord_Objects).zw + ( (TVE_ColorsCoord_Objects).xy * (Position58_g46588).xz ) ) );
			half4 localUSE_BUFFERS33_g46595 = USE_BUFFERS( Vegetation33_g46595 , Grass33_g46595 , Objects33_g46595 );
			float4 temp_output_46_0_g46588 = localUSE_BUFFERS33_g46595;
			half3 Global_ColorsTex_RGB1700_g46494 = (temp_output_46_0_g46588).xyz;
			#ifdef UNITY_COLORSPACE_GAMMA
				float staticSwitch1_g46557 = 2.0;
			#else
				float staticSwitch1_g46557 = 4.594794;
			#endif
			half3 Global_Colors1954_g46494 = ( (_LocalColors).rgb * ( Global_ColorsTex_RGB1700_g46494 * staticSwitch1_g46557 ) );
			float lerpResult3870_g46494 = lerp( 1.0 , i.vertexColor.r , _ColorsVariationValue);
			half Global_Colors_Value3650_g46494 = ( _GlobalColors * lerpResult3870_g46494 );
			float temp_output_7_0_g46558 = _ColorsMaskMinValue;
			half Global_Colors_Mask3692_g46494 = saturate( ( ( ( 1.0 - i.vertexColor.a ) - temp_output_7_0_g46558 ) / ( _ColorsMaskMaxValue - temp_output_7_0_g46558 ) ) );
			float3 lerpResult3628_g46494 = lerp( Blend_AlbedoTinted2808_g46494 , ( lerpResult3618_g46494 * Global_Colors1954_g46494 ) , ( Global_Colors_Value3650_g46494 * Global_Colors_Mask3692_g46494 ));
			half3 Blend_AlbedoColored863_g46494 = lerpResult3628_g46494;
			float3 temp_output_799_0_g46494 = (_SubsurfaceColor).rgb;
			float dotResult3930_g46494 = dot( temp_output_799_0_g46494 , float3(0.2126,0.7152,0.0722) );
			float3 temp_cast_4 = (dotResult3930_g46494).xxx;
			float3 lerpResult3932_g46494 = lerp( temp_output_799_0_g46494 , temp_cast_4 , Global_Colors_Influence3668_g46494);
			float3 lerpResult3942_g46494 = lerp( temp_output_799_0_g46494 , ( lerpResult3932_g46494 * Global_Colors1954_g46494 ) , ( Global_Colors_Value3650_g46494 * Global_Colors_Mask3692_g46494 ));
			half3 Subsurface_Color1722_g46494 = lerpResult3942_g46494;
			half MainLight_Subsurface4041_g46494 = TVE_MainLightParams.a;
			half Subsurface_Intensity1752_g46494 = ( _SubsurfaceValue * MainLight_Subsurface4041_g46494 );
			float temp_output_7_0_g46555 = _SubsurfaceMaskMinValue;
			half Subsurface_Mask1557_g46494 = saturate( ( ( i.vertexColor.a - temp_output_7_0_g46555 ) / ( _SubsurfaceMaskMaxValue - temp_output_7_0_g46555 ) ) );
			half3 Subsurface_Transmission884_g46494 = ( Subsurface_Color1722_g46494 * Subsurface_Intensity1752_g46494 * Subsurface_Mask1557_g46494 );
			half3 MainLight_Direction3926_g46494 = TVE_MainLightDirection;
			float3 ase_worldPos = i.worldPos;
			float3 normalizeResult2169_g46494 = normalize( ( _WorldSpaceCameraPos - ase_worldPos ) );
			float3 ViewDir_Normalized3963_g46494 = normalizeResult2169_g46494;
			float dotResult785_g46494 = dot( -MainLight_Direction3926_g46494 , ViewDir_Normalized3963_g46494 );
			float saferPower1624_g46494 = max( (dotResult785_g46494*0.5 + 0.5) , 0.0001 );
			#ifdef UNITY_PASS_FORWARDADD
				float staticSwitch1602_g46494 = 0.0;
			#else
				float staticSwitch1602_g46494 = ( pow( saferPower1624_g46494 , _MainLightAngleValue ) * _MainLightScatteringValue );
			#endif
			half Mask_Subsurface_View782_g46494 = staticSwitch1602_g46494;
			half3 Subsurface_Deferred1693_g46494 = ( Subsurface_Transmission884_g46494 * Mask_Subsurface_View782_g46494 * Blend_AlbedoColored863_g46494 );
			half3 Blend_AlbedoAndSubsurface149_g46494 = ( Blend_AlbedoColored863_g46494 + Subsurface_Deferred1693_g46494 );
			half3 Global_OverlayColor1758_g46494 = (TVE_OverlayColor).rgb;
			half Main_AlbedoTex_G3526_g46494 = tex2DNode29_g46494.g;
			float3 Position82_g46523 = PositionWS_PerVertex3905_g46494;
			half4 Vegetation33_g46530 = SAMPLE_TEXTURE2D( TVE_ExtrasTex_Vegetation, samplerTVE_ExtrasTex_Vegetation, ( (TVE_ExtrasCoord_Vegetation).zw + ( (TVE_ExtrasCoord_Vegetation).xy * (Position82_g46523).xz ) ) );
			half4 Grass33_g46530 = SAMPLE_TEXTURE2D( TVE_ExtrasTex_Grass, samplerTVE_ExtrasTex_Grass, ( (TVE_ExtrasCoord_Grass).zw + ( (TVE_ExtrasCoord_Grass).xy * (Position82_g46523).xz ) ) );
			half4 Objects33_g46530 = SAMPLE_TEXTURE2D( TVE_ExtrasTex_Objects, samplerTVE_ExtrasTex_Objects, ( (TVE_ExtrasCoord_Objects).zw + ( (TVE_ExtrasCoord_Objects).xy * (Position82_g46523).xz ) ) );
			half4 localUSE_BUFFERS33_g46530 = USE_BUFFERS( Vegetation33_g46530 , Grass33_g46530 , Objects33_g46530 );
			float4 break49_g46523 = localUSE_BUFFERS33_g46530;
			half Global_Extras_Overlay156_g46494 = break49_g46523.z;
			float temp_output_1025_0_g46494 = ( _GlobalOverlay * Global_Extras_Overlay156_g46494 );
			float lerpResult1065_g46494 = lerp( 1.0 , i.vertexColor.r , _OverlayVariationValue);
			half Overlay_Commons1365_g46494 = ( temp_output_1025_0_g46494 * lerpResult1065_g46494 );
			float temp_output_7_0_g46580 = _OverlayMaskMinValue;
			half Overlay_Mask269_g46494 = saturate( ( ( ( ( ( i.vertexColor.a * 0.5 ) + Main_AlbedoTex_G3526_g46494 ) * Overlay_Commons1365_g46494 ) - temp_output_7_0_g46580 ) / ( _OverlayMaskMaxValue - temp_output_7_0_g46580 ) ) );
			float3 lerpResult336_g46494 = lerp( Blend_AlbedoAndSubsurface149_g46494 , Global_OverlayColor1758_g46494 , Overlay_Mask269_g46494);
			half3 Final_Albedo359_g46494 = lerpResult336_g46494;
			float Main_Alpha316_g46494 = ( _MainColor.a * tex2DNode29_g46494.a );
			float lerpResult354_g46494 = lerp( 1.0 , Main_Alpha316_g46494 , _render_premul);
			half Final_Premultiply355_g46494 = lerpResult354_g46494;
			float3 temp_output_410_0_g46494 = ( Final_Albedo359_g46494 * Final_Premultiply355_g46494 );
			o.Albedo = ( temp_output_410_0_g46494 * i.vertexToFrag11_g46550 );
			float3 temp_cast_8 = (( 0.04 * _RenderSpecular )).xxx;
			o.Specular = temp_cast_8;
			float4 tex2DNode35_g46494 = SAMPLE_TEXTURE2D( _MainMaskTex, sampler_MainAlbedoTex, Main_UVs15_g46494 );
			half Main_Smoothness227_g46494 = ( tex2DNode35_g46494.a * _MainSmoothnessValue );
			half Blend_Smoothness314_g46494 = Main_Smoothness227_g46494;
			half Global_OverlaySmoothness311_g46494 = TVE_OverlaySmoothness;
			float lerpResult343_g46494 = lerp( Blend_Smoothness314_g46494 , Global_OverlaySmoothness311_g46494 , Overlay_Mask269_g46494);
			half Final_Smoothness371_g46494 = lerpResult343_g46494;
			half Global_Extras_Wetness305_g46494 = break49_g46523.y;
			float lerpResult3673_g46494 = lerp( 0.0 , Global_Extras_Wetness305_g46494 , _GlobalWetness);
			o.Smoothness = saturate( ( Final_Smoothness371_g46494 + lerpResult3673_g46494 ) );
			float lerpResult240_g46494 = lerp( 1.0 , tex2DNode35_g46494.g , _MainOcclusionValue);
			half Main_Occlusion247_g46494 = lerpResult240_g46494;
			half Blend_Occlusion323_g46494 = Main_Occlusion247_g46494;
			o.Occlusion = Blend_Occlusion323_g46494;
			float localCustomAlphaClip3735_g46494 = ( 0.0 );
			half Final_AlphaFade3727_g46494 = 1.0;
			float temp_output_41_0_g46500 = Final_AlphaFade3727_g46494;
			float Mesh_Variation16_g46494 = i.vertexColor.r;
			float lerpResult4033_g46494 = lerp( 0.9 , (Mesh_Variation16_g46494*0.5 + 0.5) , _AlphaVariationValue);
			half Global_Extras_Alpha1033_g46494 = break49_g46523.w;
			float temp_output_4022_0_g46494 = ( lerpResult4033_g46494 - ( 1.0 - Global_Extras_Alpha1033_g46494 ) );
			half AlphaTreshold2132_g46494 = _Cutoff;
			#ifdef TVE_ALPHA_CLIP
				float staticSwitch4017_g46494 = ( temp_output_4022_0_g46494 + AlphaTreshold2132_g46494 );
			#else
				float staticSwitch4017_g46494 = temp_output_4022_0_g46494;
			#endif
			float lerpResult4011_g46494 = lerp( 1.0 , staticSwitch4017_g46494 , _GlobalAlpha);
			half Global_Alpha315_g46494 = saturate( ( lerpResult4011_g46494 * _LocalAlpha ) );
			#ifdef TVE_ALPHA_CLIP
				float staticSwitch3792_g46494 = ( ( Main_Alpha316_g46494 * Global_Alpha315_g46494 ) - ( AlphaTreshold2132_g46494 - 0.5 ) );
			#else
				float staticSwitch3792_g46494 = ( Main_Alpha316_g46494 * Global_Alpha315_g46494 );
			#endif
			half Final_Alpha3754_g46494 = staticSwitch3792_g46494;
			float temp_output_661_0_g46494 = ( saturate( ( temp_output_41_0_g46500 + ( temp_output_41_0_g46500 * SAMPLE_TEXTURE3D( TVE_ScreenTex3D, samplerTVE_ScreenTex3D, ( TVE_ScreenTexCoord * PositionWS_PerVertex3905_g46494 ) ).r ) ) ) * Final_Alpha3754_g46494 );
			float Alpha3735_g46494 = temp_output_661_0_g46494;
			float Treshold3735_g46494 = 0.5;
			{
			#if TVE_ALPHA_CLIP
				clip(Alpha3735_g46494 - Treshold3735_g46494);
			#endif
			}
			half Final_Clip914_g46494 = saturate( Alpha3735_g46494 );
			o.Alpha = Final_Clip914_g46494;
		}

		ENDCG
	}
	Fallback "Hidden/BOXOPHOBIC/The Vegetation Engine/Fallback"
	CustomEditor "TVEShaderCoreGUI"
}
/*ASEBEGIN
Version=18806
1920;1;1906;1021;2719.199;895.445;1;True;False
Node;AmplifyShaderEditor.FunctionNode;535;-2176,320;Inherit;False;Define TVE_IS_GRASS_SHADER;-1;;40222;921559c53826c0142ba6e27dd03eaef2;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;31;-2176,-1152;Half;False;Property;_Banner;Banner;0;0;Create;True;0;0;0;True;1;StyledBanner(Grass Standard Lit (Deferred));False;0;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;530;-2176,-896;Half;False;Property;_IsAnyPathShader;_IsAnyPathShader;237;1;[HideInInspector];Create;True;0;0;0;True;0;False;1;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;534;-2176,256;Inherit;False;Define TVE_PIVOT_DATA_BAKED;-1;;46596;8da5867b3f9f1834693af40d3eff73f4;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;569;-2176,-384;Inherit;False;Base Shader;1;;46494;856f7164d1c579d43a5cf4968a75ca43;72,3882,1,3880,1,3957,1,4028,1,4029,1,3904,1,3903,1,3900,1,3908,1,1300,1,1298,1,3586,0,1271,1,3889,0,3658,1,1708,1,3509,1,1712,0,3873,1,1714,1,1717,1,1718,1,1715,1,916,0,1762,0,1763,0,3568,1,1949,1,1776,1,3475,1,893,1,1745,1,3479,0,3501,1,3221,1,1646,1,1690,1,1757,0,3960,0,2807,1,3886,0,2953,1,3887,0,3243,1,3888,0,3728,0,3949,0,2172,0,3883,0,2658,1,1742,1,3484,0,1735,0,3575,0,1734,0,1736,0,1733,0,1737,0,878,0,1550,0,860,1,3544,1,2261,1,2260,1,2054,0,2032,0,2060,0,2036,0,2039,1,2062,1,3592,1,2750,1;0;15;FLOAT3;0;FLOAT3;528;FLOAT3;2489;FLOAT;3678;FLOAT;529;FLOAT;530;FLOAT;531;FLOAT;1235;FLOAT3;1230;FLOAT;1461;FLOAT;1290;FLOAT;721;FLOAT;532;FLOAT;629;FLOAT3;534
Node;AmplifyShaderEditor.RangedFloatNode;7;-1792,-768;Half;False;Property;_render_dst;_render_dst;243;1;[HideInInspector];Create;True;0;2;Opaque;0;Transparent;1;0;True;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;168;-2176,-1024;Half;False;Property;_IsGrassShader;_IsGrassShader;235;1;[HideInInspector];Create;True;0;0;0;True;0;False;1;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;580;-1728,-896;Half;False;Property;_IsBalancedShader;_IsBalancedShader;239;1;[HideInInspector];Create;True;0;0;0;True;0;False;1;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;570;-2176,384;Inherit;False;Define TVE_USE_GRASS_BUFFERS;-1;;46493;2797ee8cd2eb2624c9f33ca7047d93d1;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;81;-1968,-1024;Half;False;Property;_IsStandardShader;_IsStandardShader;236;1;[HideInInspector];Create;True;0;0;0;True;0;False;1;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;572;-1744,-1024;Half;False;Property;_IsSubsurfaceApproxShader;_IsSubsurfaceApproxShader;240;1;[HideInInspector];Create;True;0;0;0;True;0;False;1;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;573;-1952,-896;Half;False;Property;_IsTarget40Shader;_IsTarget40Shader;238;1;[HideInInspector];Create;True;0;0;0;True;0;False;1;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-1600,-768;Half;False;Property;_render_zw;_render_zw;244;1;[HideInInspector];Create;True;0;2;Opaque;0;Transparent;1;0;True;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;10;-2176,-768;Half;False;Property;_render_cull;_render_cull;241;1;[HideInInspector];Create;True;0;3;Both;0;Back;1;Front;2;0;True;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-1984,-768;Half;False;Property;_render_src;_render_src;242;1;[HideInInspector];Create;True;0;0;0;True;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;581;-1376,-384;Float;False;True;-1;4;TVEShaderCoreGUI;0;0;StandardSpecular;BOXOPHOBIC/The Vegetation Engine/Vegetation/Grass Standard Lit (Deferred);False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;False;True;Back;0;True;17;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;1;0;True;20;0;True;7;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Absolute;0;Hidden/BOXOPHOBIC/The Vegetation Engine/Fallback;245;-1;-1;-1;0;False;0;0;True;10;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;True;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
Node;AmplifyShaderEditor.CommentaryNode;37;-2176,-1280;Inherit;False;1023.392;100;Internal;0;;1,0.252,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;33;-2176,-512;Inherit;False;1024.392;100;Final;0;;0,1,0.5,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;408;-2176,128;Inherit;False;1026.438;100;Features;0;;0,1,0.5,1;0;0
WireConnection;581;0;569;0
WireConnection;581;1;569;528
WireConnection;581;3;569;3678
WireConnection;581;4;569;530
WireConnection;581;5;569;531
WireConnection;581;9;569;532
WireConnection;581;11;569;534
ASEEND*/
//CHKSM=E9E6FB80ED7F78B79351B2B10EE08E75D17A2753