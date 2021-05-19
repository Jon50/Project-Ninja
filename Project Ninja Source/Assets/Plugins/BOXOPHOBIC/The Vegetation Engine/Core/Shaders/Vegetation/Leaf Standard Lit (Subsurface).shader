// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "BOXOPHOBIC/The Vegetation Engine/Vegetation/Leaf Standard Lit (Subsurface)"
{
	Properties
	{
		[StyledBanner(Leaf Standard Lit (Subsurface))]_Banner("Banner", Float) = 0
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
		_FadeGlancingValue("Fade by Glancing Angle", Range( 0 , 1)) = 0
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
		_OverlayBottomValue("Overlay Bottom", Range( 0 , 1)) = 0.5
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
		[Space(10)]_MotionAmplitude_30("Leaves Amplitude", Range( 0 , 2)) = 0.5
		[IntRange]_MotionSpeed_30("Leaves Speed", Range( 0 , 60)) = 6
		_MotionScale_30("Leaves Scale", Range( 0 , 20)) = 2
		_MotionVariation_30("Leaves Variation", Range( 0 , 20)) = 2
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
		[HideInInspector]_subsurface_shadow("_subsurface_shadow", Float) = 1
		[HideInInspector]_IsLeafShader("_IsLeafShader", Float) = 1
		[HideInInspector]_IsStandardShader("_IsStandardShader", Float) = 1
		[HideInInspector]_IsForwardPathShader("_IsForwardPathShader", Float) = 1
		[HideInInspector]_IsTarget40Shader("_IsTarget40Shader", Float) = 1
		[HideInInspector]_IsSubsurfaceShader("_IsSubsurfaceShader", Float) = 1
		[HideInInspector]_IsBalancedShader("_IsBalancedShader", Float) = 1
		[HideInInspector]_render_cull("_render_cull", Float) = 0
		[HideInInspector]_render_src("_render_src", Float) = 5
		[HideInInspector]_render_dst("_render_dst", Float) = 10
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
		#include "UnityPBSLighting.cginc"
		#pragma target 4.0
		#pragma shader_feature_local TVE_ALPHA_CLIP
		#pragma shader_feature_local TVE_VERTEX_DATA_BATCHED
		  
		#define THE_VEGETATION_ENGINE
		    
		//SHADER INJECTION POINT BEGIN
		//SHADER INJECTION POINT END
		      
		#define TVE_USE_VEGETATION_BUFFERS
		#define TVE_IS_VEGETATION_SHADER
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

		#pragma surface surf StandardSpecularCustom keepalpha addshadow fullforwardshadows exclude_path:deferred dithercrossfade vertex:vertexDataFunc 
		struct Input
		{
			float3 worldPos;
			float2 vertexToFrag11_g61130;
			half ASEVFace : VFACE;
			float3 vertexToFrag11_g61151;
			float3 vertexToFrag11_g61150;
			float vertexToFrag11_g61129;
			float3 vertexToFrag3890_g61081;
			float4 vertexColor : COLOR;
			float3 worldNormal;
			INTERNAL_DATA
			float3 vertexToFrag11_g61137;
			float vertexToFrag11_g61082;
		};

		struct SurfaceOutputStandardSpecularCustom
		{
			half3 Albedo;
			half3 Normal;
			half3 Emission;
			half3 Specular;
			half Smoothness;
			half Occlusion;
			half Alpha;
			half3 Transmission;
		};

		uniform half _render_cull;
		uniform half _render_zw;
		uniform half _IsStandardShader;
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
		uniform half _IsSubsurfaceShader;
		uniform half _IsLeafShader;
		uniform half _IsTarget40Shader;
		uniform half _IsForwardPathShader;
		uniform half _render_src;
		uniform half _subsurface_shadow;
		uniform half _Banner;
		uniform half _IsBalancedShader;
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
		uniform float _MotionScale_30;
		uniform float _MotionSpeed_30;
		uniform float _MotionVariation_30;
		uniform half _MotionAmplitude_30;
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
		uniform half _ColorsVariationValue;
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
		uniform half _OverlayVariationValue;
		uniform half _OverlayMaskMinValue;
		uniform half _OverlayMaskMaxValue;
		uniform half _render_premul;
		uniform half4 _VertexOcclusionColor;
		uniform half _VertexOcclusionMinValue;
		uniform half _VertexOcclusionMaxValue;
		uniform half _RenderSpecular;
		uniform half _MainSmoothnessValue;
		uniform half TVE_OverlaySmoothness;
		uniform half _GlobalWetness;
		uniform half _MainOcclusionValue;
		uniform half _FadeGlancingValue;
		uniform half TVE_CameraFadeStart;
		uniform half TVE_CameraFadeEnd;
		uniform half _FadeCameraValue;
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
			float3 PositionOS3588_g61081 = ase_vertex3Pos;
			half3 _Vector1 = half3(0,0,0);
			half3 Mesh_PivotsOS2291_g61081 = _Vector1;
			float3 temp_output_2283_0_g61081 = ( PositionOS3588_g61081 - Mesh_PivotsOS2291_g61081 );
			half3 VertexPos40_g61155 = temp_output_2283_0_g61081;
			float3 appendResult74_g61155 = (float3(0.0 , VertexPos40_g61155.y , 0.0));
			float3 VertexPosRotationAxis50_g61155 = appendResult74_g61155;
			float3 break84_g61155 = VertexPos40_g61155;
			float3 appendResult81_g61155 = (float3(break84_g61155.x , 0.0 , break84_g61155.z));
			float3 VertexPosOtherAxis82_g61155 = appendResult81_g61155;
			half MotionAmplitude203095_g61081 = _MotionAmplitude_20;
			float ObjectData20_g61123 = 3.14;
			float Bounds_Radius121_g61081 = _MaxBoundsInfo.x;
			float WorldData19_g61123 = Bounds_Radius121_g61081;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float staticSwitch14_g61123 = WorldData19_g61123;
			#else
				float staticSwitch14_g61123 = ObjectData20_g61123;
			#endif
			float Motion_Max_Rolling1137_g61081 = staticSwitch14_g61123;
			float4x4 break19_g61093 = unity_ObjectToWorld;
			float3 appendResult20_g61093 = (float3(break19_g61093[ 0 ][ 3 ] , break19_g61093[ 1 ][ 3 ] , break19_g61093[ 2 ][ 3 ]));
			half3 Off19_g61094 = appendResult20_g61093;
			float4 ase_vertex4Pos = v.vertex;
			float4 transform68_g61093 = mul(unity_ObjectToWorld,ase_vertex4Pos);
			float3 appendResult95_g61093 = (float3(v.texcoord.z , 0.0 , v.texcoord.w));
			float4 transform62_g61093 = mul(unity_ObjectToWorld,float4( ( ase_vertex3Pos - ( appendResult95_g61093 * _vertex_pivot_mode ) ) , 0.0 ));
			float3 ObjectPositionWithPivots28_g61093 = ( (transform68_g61093).xyz - (transform62_g61093).xyz );
			half3 On20_g61094 = ObjectPositionWithPivots28_g61093;
			#ifdef TVE_PIVOT_DATA_BAKED
				float3 staticSwitch14_g61094 = On20_g61094;
			#else
				float3 staticSwitch14_g61094 = Off19_g61094;
			#endif
			half3 ObjectData20_g61095 = staticSwitch14_g61094;
			half3 WorldData19_g61095 = Off19_g61094;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float3 staticSwitch14_g61095 = WorldData19_g61095;
			#else
				float3 staticSwitch14_g61095 = ObjectData20_g61095;
			#endif
			float3 temp_output_42_0_g61093 = staticSwitch14_g61095;
			half3 ObjectData20_g61099 = temp_output_42_0_g61093;
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			half3 WorldData19_g61099 = ase_worldPos;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float3 staticSwitch14_g61099 = WorldData19_g61099;
			#else
				float3 staticSwitch14_g61099 = ObjectData20_g61099;
			#endif
			float3 Position83_g61092 = staticSwitch14_g61099;
			half4 Vegetation33_g61098 = SAMPLE_TEXTURE2D_LOD( TVE_VertexTex_Vegetation, samplerTVE_VertexTex_Vegetation, ( (TVE_VertexCoord_Vegetation).zw + ( (TVE_VertexCoord_Vegetation).xy * (Position83_g61092).xz ) ), 0.0 );
			half4 Grass33_g61098 = SAMPLE_TEXTURE2D_LOD( TVE_VertexTex_Grass, samplerTVE_VertexTex_Grass, ( (TVE_VertexCoord_Grass).zw + ( (TVE_VertexCoord_Grass).xy * (Position83_g61092).xz ) ), 0.0 );
			half4 Objects33_g61098 = SAMPLE_TEXTURE2D_LOD( TVE_VertexTex_Objects, samplerTVE_VertexTex_Objects, ( (TVE_VertexCoord_Objects).zw + ( (TVE_VertexCoord_Objects).xy * (Position83_g61092).xz ) ), 0.0 );
			half4 localUSE_BUFFERS33_g61098 = USE_BUFFERS( Vegetation33_g61098 , Grass33_g61098 , Objects33_g61098 );
			half4 Global_Motion_Params3909_g61081 = localUSE_BUFFERS33_g61098;
			float4 break322_g61160 = Global_Motion_Params3909_g61081;
			half Wind_Power369_g61160 = break322_g61160.z;
			float temp_output_7_0_g61162 = 0.5;
			float temp_output_404_0_g61160 = (Wind_Power369_g61160*2.0 + -1.0);
			float temp_output_406_0_g61160 = saturate( sign( temp_output_404_0_g61160 ) );
			float lerpResult401_g61160 = lerp( 0.0 , ( ( Wind_Power369_g61160 - temp_output_7_0_g61162 ) / ( 1.0 - temp_output_7_0_g61162 ) ) , temp_output_406_0_g61160);
			float lerpResult410_g61160 = lerp( 0.2 , 1.0 , lerpResult401_g61160);
			half Wind_Power_203109_g61081 = lerpResult410_g61160;
			half Mesh_Motion_260_g61081 = v.texcoord3.y;
			#ifdef TVE_IS_GRASS_SHADER
				float2 staticSwitch160_g61168 = TVE_NoiseSpeed_Grass;
			#else
				float2 staticSwitch160_g61168 = TVE_NoiseSpeed_Vegetation;
			#endif
			float4x4 break19_g61170 = unity_ObjectToWorld;
			float3 appendResult20_g61170 = (float3(break19_g61170[ 0 ][ 3 ] , break19_g61170[ 1 ][ 3 ] , break19_g61170[ 2 ][ 3 ]));
			half3 Off19_g61171 = appendResult20_g61170;
			float4 transform68_g61170 = mul(unity_ObjectToWorld,ase_vertex4Pos);
			float3 appendResult95_g61170 = (float3(v.texcoord.z , 0.0 , v.texcoord.w));
			float4 transform62_g61170 = mul(unity_ObjectToWorld,float4( ( ase_vertex3Pos - ( appendResult95_g61170 * _vertex_pivot_mode ) ) , 0.0 ));
			float3 ObjectPositionWithPivots28_g61170 = ( (transform68_g61170).xyz - (transform62_g61170).xyz );
			half3 On20_g61171 = ObjectPositionWithPivots28_g61170;
			#ifdef TVE_PIVOT_DATA_BAKED
				float3 staticSwitch14_g61171 = On20_g61171;
			#else
				float3 staticSwitch14_g61171 = Off19_g61171;
			#endif
			half3 ObjectData20_g61172 = staticSwitch14_g61171;
			half3 WorldData19_g61172 = Off19_g61171;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float3 staticSwitch14_g61172 = WorldData19_g61172;
			#else
				float3 staticSwitch14_g61172 = ObjectData20_g61172;
			#endif
			float3 temp_output_42_0_g61170 = staticSwitch14_g61172;
			half3 ObjectData20_g61169 = temp_output_42_0_g61170;
			half3 WorldData19_g61169 = ase_worldPos;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float3 staticSwitch14_g61169 = WorldData19_g61169;
			#else
				float3 staticSwitch14_g61169 = ObjectData20_g61169;
			#endif
			#ifdef TVE_IS_GRASS_SHADER
				float2 staticSwitch164_g61168 = (ase_worldPos).xz;
			#else
				float2 staticSwitch164_g61168 = (staticSwitch14_g61169).xz;
			#endif
			#ifdef TVE_IS_GRASS_SHADER
				float staticSwitch161_g61168 = TVE_NoiseSize_Grass;
			#else
				float staticSwitch161_g61168 = TVE_NoiseSize_Vegetation;
			#endif
			float2 panner73_g61168 = ( _Time.y * staticSwitch160_g61168 + ( staticSwitch164_g61168 * staticSwitch161_g61168 ));
			float4 tex2DNode75_g61168 = SAMPLE_TEXTURE2D_LOD( TVE_NoiseTex, samplerTVE_NoiseTex, panner73_g61168, 0.0 );
			float4 saferPower77_g61168 = max( abs( tex2DNode75_g61168 ) , 0.0001 );
			half Wind_Power2223_g61081 = lerpResult401_g61160;
			float temp_output_167_0_g61168 = Wind_Power2223_g61081;
			float lerpResult168_g61168 = lerp( 1.5 , 0.25 , temp_output_167_0_g61168);
			float4 temp_cast_5 = (lerpResult168_g61168).xxxx;
			float4 break142_g61168 = pow( saferPower77_g61168 , temp_cast_5 );
			half Global_NoiseTex_R34_g61081 = break142_g61168.r;
			float4x4 break19_g61105 = unity_ObjectToWorld;
			float3 appendResult20_g61105 = (float3(break19_g61105[ 0 ][ 3 ] , break19_g61105[ 1 ][ 3 ] , break19_g61105[ 2 ][ 3 ]));
			half3 Off19_g61106 = appendResult20_g61105;
			float4 transform68_g61105 = mul(unity_ObjectToWorld,ase_vertex4Pos);
			float3 appendResult95_g61105 = (float3(v.texcoord.z , 0.0 , v.texcoord.w));
			float4 transform62_g61105 = mul(unity_ObjectToWorld,float4( ( ase_vertex3Pos - ( appendResult95_g61105 * _vertex_pivot_mode ) ) , 0.0 ));
			float3 ObjectPositionWithPivots28_g61105 = ( (transform68_g61105).xyz - (transform62_g61105).xyz );
			half3 On20_g61106 = ObjectPositionWithPivots28_g61105;
			#ifdef TVE_PIVOT_DATA_BAKED
				float3 staticSwitch14_g61106 = On20_g61106;
			#else
				float3 staticSwitch14_g61106 = Off19_g61106;
			#endif
			half3 ObjectData20_g61107 = staticSwitch14_g61106;
			half3 WorldData19_g61107 = Off19_g61106;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float3 staticSwitch14_g61107 = WorldData19_g61107;
			#else
				float3 staticSwitch14_g61107 = ObjectData20_g61107;
			#endif
			float3 temp_output_42_0_g61105 = staticSwitch14_g61107;
			float3 break9_g61105 = temp_output_42_0_g61105;
			half Variation_Complex102_g61103 = frac( ( v.color.r + ( break9_g61105.x + break9_g61105.z ) ) );
			float ObjectData20_g61104 = Variation_Complex102_g61103;
			half Variation_Simple105_g61103 = v.color.r;
			float WorldData19_g61104 = Variation_Simple105_g61103;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float staticSwitch14_g61104 = WorldData19_g61104;
			#else
				float staticSwitch14_g61104 = ObjectData20_g61104;
			#endif
			half Motion_Variation3073_g61081 = staticSwitch14_g61104;
			float temp_output_3154_0_g61081 = ( _MotionVariation_20 * Motion_Variation3073_g61081 );
			float temp_output_116_0_g61166 = temp_output_3154_0_g61081;
			float temp_output_108_0_g61166 = ceil( saturate( ( frac( ( temp_output_116_0_g61166 + 0.3576 ) ) - 0.3 ) ) );
			float mulTime98_g61166 = _Time.y * 0.5;
			float lerpResult110_g61166 = lerp( temp_output_108_0_g61166 , ceil( saturate( ( frac( ( temp_output_116_0_g61166 + 0.1258 ) ) - 0.8 ) ) ) , (sin( mulTime98_g61166 )*0.5 + 0.5));
			float lerpResult118_g61166 = lerp( 0.25 , 0.75 , Wind_Power2223_g61081);
			float lerpResult111_g61166 = lerp( lerpResult110_g61166 , 1.0 , ( lerpResult118_g61166 * lerpResult118_g61166 * lerpResult118_g61166 * lerpResult118_g61166 ));
			half Input_Speed62_g61164 = _MotionSpeed_20;
			float mulTime354_g61164 = _Time.y * Input_Speed62_g61164;
			float Motion_Variation284_g61164 = temp_output_3154_0_g61081;
			float Motion_Scale287_g61164 = ( _MotionScale_20 * ase_worldPos.x );
			half Motion_Rolling138_g61081 = ( ( MotionAmplitude203095_g61081 * Motion_Max_Rolling1137_g61081 ) * ( Wind_Power_203109_g61081 * Mesh_Motion_260_g61081 * Global_NoiseTex_R34_g61081 ) * lerpResult111_g61166 * sin( ( mulTime354_g61164 + Motion_Variation284_g61164 + Motion_Scale287_g61164 ) ) );
			half Angle44_g61155 = Motion_Rolling138_g61081;
			half3 VertexPos40_g61154 = ( VertexPosRotationAxis50_g61155 + ( VertexPosOtherAxis82_g61155 * cos( Angle44_g61155 ) ) + ( cross( float3(0,1,0) , VertexPosOtherAxis82_g61155 ) * sin( Angle44_g61155 ) ) );
			float3 appendResult74_g61154 = (float3(VertexPos40_g61154.x , 0.0 , 0.0));
			half3 VertexPosRotationAxis50_g61154 = appendResult74_g61154;
			float3 break84_g61154 = VertexPos40_g61154;
			float3 appendResult81_g61154 = (float3(0.0 , break84_g61154.y , break84_g61154.z));
			half3 VertexPosOtherAxis82_g61154 = appendResult81_g61154;
			float ObjectData20_g61147 = 3.14;
			float Bounds_Height374_g61081 = _MaxBoundsInfo.y;
			float WorldData19_g61147 = ( Bounds_Height374_g61081 * 3.14 );
			#ifdef TVE_VERTEX_DATA_BATCHED
				float staticSwitch14_g61147 = WorldData19_g61147;
			#else
				float staticSwitch14_g61147 = ObjectData20_g61147;
			#endif
			float Motion_Max_Bending1133_g61081 = staticSwitch14_g61147;
			float lerpResult376_g61160 = lerp( 0.1 , 1.0 , lerpResult401_g61160);
			half Wind_Power_103106_g61081 = lerpResult376_g61160;
			float3 appendResult397_g61160 = (float3(break322_g61160.x , 0.0 , break322_g61160.y));
			float3 temp_output_398_0_g61160 = (appendResult397_g61160*2.0 + -1.0);
			float3 ase_parentObjectScale = (1.0/float3( length( unity_WorldToObject[ 0 ].xyz ), length( unity_WorldToObject[ 1 ].xyz ), length( unity_WorldToObject[ 2 ].xyz ) ));
			float3 temp_output_339_0_g61160 = ( mul( unity_WorldToObject, float4( temp_output_398_0_g61160 , 0.0 ) ).xyz * ase_parentObjectScale );
			half2 Wind_DirectionOS39_g61081 = (temp_output_339_0_g61160).xz;
			half Input_Speed62_g61157 = _MotionSpeed_10;
			float mulTime373_g61157 = _Time.y * Input_Speed62_g61157;
			half Motion_Variation284_g61157 = ( _MotionVariation_10 * Motion_Variation3073_g61081 );
			float2 appendResult344_g61157 = (float2(ase_worldPos.x , ase_worldPos.z));
			float2 Motion_Scale287_g61157 = ( _MotionScale_10 * appendResult344_g61157 );
			half2 Sine_MinusOneToOne281_g61157 = sin( ( mulTime373_g61157 + Motion_Variation284_g61157 + Motion_Scale287_g61157 ) );
			float2 temp_cast_9 = (1.0).xx;
			half Input_Turbulence327_g61157 = Global_NoiseTex_R34_g61081;
			float2 lerpResult321_g61157 = lerp( Sine_MinusOneToOne281_g61157 , temp_cast_9 , Input_Turbulence327_g61157);
			half2 Motion_Bending2258_g61081 = ( ( _MotionAmplitude_10 * Motion_Max_Bending1133_g61081 ) * Wind_Power_103106_g61081 * Wind_DirectionOS39_g61081 * Global_NoiseTex_R34_g61081 * lerpResult321_g61157 );
			float lerpResult402_g61160 = lerp( abs( temp_output_404_0_g61160 ) , 0.0 , temp_output_406_0_g61160);
			half Motion_InteractionMask66_g61081 = lerpResult402_g61160;
			float lerpResult3307_g61081 = lerp( 1.0 , Motion_Variation3073_g61081 , _InteractionVariation);
			half2 Motion_Interaction53_g61081 = ( _InteractionAmplitude * Motion_Max_Bending1133_g61081 * Motion_InteractionMask66_g61081 * Motion_InteractionMask66_g61081 * Wind_DirectionOS39_g61081 * lerpResult3307_g61081 );
			float2 lerpResult109_g61081 = lerp( Motion_Bending2258_g61081 , Motion_Interaction53_g61081 , Motion_InteractionMask66_g61081);
			half Mesh_Motion_182_g61081 = v.texcoord3.x;
			float2 break143_g61081 = ( lerpResult109_g61081 * Mesh_Motion_182_g61081 );
			half Motion_Z190_g61081 = break143_g61081.y;
			half Angle44_g61154 = Motion_Z190_g61081;
			half3 VertexPos40_g61090 = ( VertexPosRotationAxis50_g61154 + ( VertexPosOtherAxis82_g61154 * cos( Angle44_g61154 ) ) + ( cross( float3(1,0,0) , VertexPosOtherAxis82_g61154 ) * sin( Angle44_g61154 ) ) );
			float3 appendResult74_g61090 = (float3(0.0 , 0.0 , VertexPos40_g61090.z));
			half3 VertexPosRotationAxis50_g61090 = appendResult74_g61090;
			float3 break84_g61090 = VertexPos40_g61090;
			float3 appendResult81_g61090 = (float3(break84_g61090.x , break84_g61090.y , 0.0));
			half3 VertexPosOtherAxis82_g61090 = appendResult81_g61090;
			half Motion_X216_g61081 = break143_g61081.x;
			half Angle44_g61090 = -Motion_X216_g61081;
			half Motion_Scale321_g61118 = ( _MotionScale_30 * 10.0 );
			half Input_Speed62_g61118 = _MotionSpeed_30;
			float mulTime350_g61118 = _Time.y * Input_Speed62_g61118;
			float Motion_Variation330_g61118 = ( _MotionVariation_30 * Motion_Variation3073_g61081 );
			half Input_Amplitude58_g61118 = ( _MotionAmplitude_30 * Bounds_Radius121_g61081 * 0.1 );
			float3 ase_vertexNormal = v.normal.xyz;
			half Global_NoiseTex_G38_g61081 = break142_g61168.g;
			half Global_NoiseTex_B132_g61081 = break142_g61168.b;
			half Mesh_Motion_3144_g61081 = v.texcoord3.z;
			float lerpResult378_g61160 = lerp( 0.3 , 1.0 , lerpResult401_g61160);
			half Wind_Power_303112_g61081 = lerpResult378_g61160;
			half3 Motion_Leaves1988_g61081 = ( ( sin( ( ( ( ase_worldPos.x + ase_worldPos.y + ase_worldPos.z ) * Motion_Scale321_g61118 ) + mulTime350_g61118 + Motion_Variation330_g61118 ) ) * Input_Amplitude58_g61118 * ase_vertexNormal ) * ( ( Global_NoiseTex_G38_g61081 + Global_NoiseTex_B132_g61081 ) * Mesh_Motion_3144_g61081 * Wind_Power_303112_g61081 ) );
			half Motion_Scale321_g61158 = ( _MotionScale_32 * 10.0 );
			half Input_Speed62_g61158 = _MotionSpeed_32;
			float mulTime349_g61158 = _Time.y * Input_Speed62_g61158;
			float Motion_Variation330_g61158 = ( _MotionVariation_32 * Motion_Variation3073_g61081 );
			half Input_Amplitude58_g61158 = ( _MotionAmplitude_32 * Bounds_Radius121_g61081 * 0.1 );
			float temp_output_299_0_g61158 = ( sin( ( ( ( ase_worldPos.x + ase_worldPos.y + ase_worldPos.z ) * Motion_Scale321_g61158 ) + mulTime349_g61158 + Motion_Variation330_g61158 ) ) * Input_Amplitude58_g61158 );
			float3 appendResult354_g61158 = (float3(temp_output_299_0_g61158 , 0.0 , temp_output_299_0_g61158));
			half Global_NoiseTex_A139_g61081 = break142_g61168.a;
			half Wind_Power_323115_g61081 = lerpResult376_g61160;
			float temp_output_7_0_g61102 = TVE_MotionFadeEnd;
			half Wind_FadeOut4005_g61081 = saturate( ( ( distance( ase_worldPos , _WorldSpaceCameraPos ) - temp_output_7_0_g61102 ) / ( TVE_MotionFadeStart - temp_output_7_0_g61102 ) ) );
			half3 Motion_Detail263_g61081 = ( appendResult354_g61158 * ( Global_NoiseTex_A139_g61081 * Mesh_Motion_3144_g61081 * Wind_Power_323115_g61081 ) * Wind_FadeOut4005_g61081 );
			float3 Vertex_Motion_Object833_g61081 = ( ( ( VertexPosRotationAxis50_g61090 + ( VertexPosOtherAxis82_g61090 * cos( Angle44_g61090 ) ) + ( cross( float3(0,0,1) , VertexPosOtherAxis82_g61090 ) * sin( Angle44_g61090 ) ) ) + Motion_Leaves1988_g61081 ) + Motion_Detail263_g61081 );
			float3 temp_output_3474_0_g61081 = ( PositionOS3588_g61081 - Mesh_PivotsOS2291_g61081 );
			float3 appendResult2047_g61081 = (float3(Motion_Rolling138_g61081 , 0.0 , -Motion_Rolling138_g61081));
			float3 appendResult2043_g61081 = (float3(Motion_X216_g61081 , 0.0 , Motion_Z190_g61081));
			float3 Vertex_Motion_World1118_g61081 = ( ( ( ( temp_output_3474_0_g61081 + appendResult2047_g61081 ) + appendResult2043_g61081 ) + Motion_Leaves1988_g61081 ) + Motion_Detail263_g61081 );
			#ifdef TVE_VERTEX_DATA_BATCHED
				float3 staticSwitch3312_g61081 = Vertex_Motion_World1118_g61081;
			#else
				float3 staticSwitch3312_g61081 = ( Vertex_Motion_Object833_g61081 + ( 0.0 * _VertexDataMode ) );
			#endif
			half Global_Vertex_Size174_g61081 = break322_g61160.w;
			float lerpResult346_g61081 = lerp( 1.0 , Global_Vertex_Size174_g61081 , _GlobalSize);
			float temp_output_2626_0_g61081 = ( lerpResult346_g61081 * _LocalSize );
			float3 appendResult3480_g61081 = (float3(temp_output_2626_0_g61081 , temp_output_2626_0_g61081 , temp_output_2626_0_g61081));
			half3 ObjectData20_g61131 = appendResult3480_g61081;
			half3 _Vector11 = half3(1,1,1);
			half3 WorldData19_g61131 = _Vector11;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float3 staticSwitch14_g61131 = WorldData19_g61131;
			#else
				float3 staticSwitch14_g61131 = ObjectData20_g61131;
			#endif
			half3 Vertex_Size1741_g61081 = staticSwitch14_g61131;
			half3 _Vector5 = half3(1,1,1);
			float4x4 break19_g61132 = unity_ObjectToWorld;
			float3 appendResult20_g61132 = (float3(break19_g61132[ 0 ][ 3 ] , break19_g61132[ 1 ][ 3 ] , break19_g61132[ 2 ][ 3 ]));
			half3 Off19_g61133 = appendResult20_g61132;
			float4 transform68_g61132 = mul(unity_ObjectToWorld,ase_vertex4Pos);
			float3 appendResult95_g61132 = (float3(v.texcoord.z , 0.0 , v.texcoord.w));
			float4 transform62_g61132 = mul(unity_ObjectToWorld,float4( ( ase_vertex3Pos - ( appendResult95_g61132 * _vertex_pivot_mode ) ) , 0.0 ));
			float3 ObjectPositionWithPivots28_g61132 = ( (transform68_g61132).xyz - (transform62_g61132).xyz );
			half3 On20_g61133 = ObjectPositionWithPivots28_g61132;
			#ifdef TVE_PIVOT_DATA_BAKED
				float3 staticSwitch14_g61133 = On20_g61133;
			#else
				float3 staticSwitch14_g61133 = Off19_g61133;
			#endif
			half3 ObjectData20_g61134 = staticSwitch14_g61133;
			half3 WorldData19_g61134 = Off19_g61133;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float3 staticSwitch14_g61134 = WorldData19_g61134;
			#else
				float3 staticSwitch14_g61134 = ObjectData20_g61134;
			#endif
			float3 temp_output_42_0_g61132 = staticSwitch14_g61134;
			float temp_output_7_0_g61138 = _SizeFadeEndValue;
			float temp_output_335_0_g61081 = saturate( ( ( ( distance( _WorldSpaceCameraPos , temp_output_42_0_g61132 ) * ( 1.0 / TVE_DistanceFadeBias ) ) - temp_output_7_0_g61138 ) / ( _SizeFadeStartValue - temp_output_7_0_g61138 ) ) );
			float3 appendResult3482_g61081 = (float3(temp_output_335_0_g61081 , temp_output_335_0_g61081 , temp_output_335_0_g61081));
			float3 lerpResult3556_g61081 = lerp( _Vector5 , appendResult3482_g61081 , _SizeFadeMode);
			half3 ObjectData20_g61143 = lerpResult3556_g61081;
			half3 WorldData19_g61143 = _Vector5;
			#ifdef TVE_VERTEX_DATA_BATCHED
				float3 staticSwitch14_g61143 = WorldData19_g61143;
			#else
				float3 staticSwitch14_g61143 = ObjectData20_g61143;
			#endif
			float3 Vertex_SizeFade1740_g61081 = staticSwitch14_g61143;
			half3 Grass_Coverage2661_g61081 = half3(0,0,0);
			float3 Final_VertexPosition890_g61081 = ( ( staticSwitch3312_g61081 * Vertex_Size1741_g61081 * Vertex_SizeFade1740_g61081 ) + Mesh_PivotsOS2291_g61081 + Grass_Coverage2661_g61081 );
			v.vertex.xyz = Final_VertexPosition890_g61081;
			v.vertex.w = 1;
			o.vertexToFrag11_g61130 = ( ( v.texcoord.xy * (_MainUVs).xy ) + (_MainUVs).zw );
			float temp_output_7_0_g61089 = _GradientMinValue;
			float4 lerpResult2779_g61081 = lerp( _GradientColorTwo , _GradientColorOne , saturate( ( ( v.color.a - temp_output_7_0_g61089 ) / ( _GradientMaxValue - temp_output_7_0_g61089 ) ) ));
			half3 Gradient_Tint2784_g61081 = (lerpResult2779_g61081).rgb;
			o.vertexToFrag11_g61151 = Gradient_Tint2784_g61081;
			float3 temp_cast_11 = (_NoiseScaleValue).xxx;
			float3 PositionWS_PerVertex3905_g61081 = ase_worldPos;
			float temp_output_7_0_g61140 = _NoiseMinValue;
			half Noise_Mask3162_g61081 = saturate( ( ( SAMPLE_TEXTURE3D_LOD( TVE_WorldTex3D, samplerTVE_WorldTex3D, ( temp_cast_11 * PositionWS_PerVertex3905_g61081 * 0.1 ), 0.0 ).r - temp_output_7_0_g61140 ) / ( _NoiseMaxValue - temp_output_7_0_g61140 ) ) );
			float4 lerpResult2800_g61081 = lerp( _NoiseColorTwo , _NoiseColorOne , Noise_Mask3162_g61081);
			half3 Noise_Tint2802_g61081 = (lerpResult2800_g61081).rgb;
			o.vertexToFrag11_g61150 = Noise_Tint2802_g61081;
			float3 Position58_g61175 = PositionWS_PerVertex3905_g61081;
			half4 Vegetation33_g61182 = SAMPLE_TEXTURE2D_LOD( TVE_ColorsTex_Vegetation, samplerTVE_ColorsTex_Vegetation, ( (TVE_ColorsCoord_Vegetation).zw + ( (TVE_ColorsCoord_Vegetation).xy * (Position58_g61175).xz ) ), 0.0 );
			half4 Grass33_g61182 = SAMPLE_TEXTURE2D_LOD( TVE_ColorsTex_Grass, samplerTVE_ColorsTex_Grass, ( (TVE_ColorsCoord_Grass).zw + ( (TVE_ColorsCoord_Grass).xy * (Position58_g61175).xz ) ), 0.0 );
			half4 Objects33_g61182 = SAMPLE_TEXTURE2D_LOD( TVE_ColorsTex_Objects, samplerTVE_ColorsTex_Objects, ( (TVE_ColorsCoord_Objects).zw + ( (TVE_ColorsCoord_Objects).xy * (Position58_g61175).xz ) ), 0.0 );
			half4 localUSE_BUFFERS33_g61182 = USE_BUFFERS( Vegetation33_g61182 , Grass33_g61182 , Objects33_g61182 );
			float4 temp_output_46_0_g61175 = localUSE_BUFFERS33_g61182;
			half Global_ColorsTex_A1701_g61081 = (temp_output_46_0_g61175).w;
			o.vertexToFrag11_g61129 = Global_ColorsTex_A1701_g61081;
			o.vertexToFrag3890_g61081 = ase_worldPos;
			float3 temp_cast_15 = (1.0).xxx;
			float Mesh_Occlusion318_g61081 = v.color.g;
			float temp_output_7_0_g61126 = _VertexOcclusionMinValue;
			float3 lerpResult2945_g61081 = lerp( (_VertexOcclusionColor).rgb , temp_cast_15 , saturate( ( ( Mesh_Occlusion318_g61081 - temp_output_7_0_g61126 ) / ( _VertexOcclusionMaxValue - temp_output_7_0_g61126 ) ) ));
			float3 Vertex_Occlusion648_g61081 = lerpResult2945_g61081;
			o.vertexToFrag11_g61137 = Vertex_Occlusion648_g61081;
			float temp_output_7_0_g61127 = TVE_CameraFadeStart;
			float saferPower3976_g61081 = max( saturate( ( ( distance( ase_worldPos , _WorldSpaceCameraPos ) - temp_output_7_0_g61127 ) / ( TVE_CameraFadeEnd - temp_output_7_0_g61127 ) ) ) , 0.0001 );
			float temp_output_3976_0_g61081 = pow( saferPower3976_g61081 , _FadeCameraValue );
			o.vertexToFrag11_g61082 = temp_output_3976_0_g61081;
		}

		inline half4 LightingStandardSpecularCustom(SurfaceOutputStandardSpecularCustom s, half3 viewDir, UnityGI gi )
		{
			half3 transmission = max(0 , -dot(s.Normal, gi.light.dir)) * gi.light.color * s.Transmission;
			half4 d = half4(s.Albedo * transmission , 0);

			SurfaceOutputStandardSpecular r;
			r.Albedo = s.Albedo;
			r.Normal = s.Normal;
			r.Emission = s.Emission;
			r.Specular = s.Specular;
			r.Smoothness = s.Smoothness;
			r.Occlusion = s.Occlusion;
			r.Alpha = s.Alpha;
			return LightingStandardSpecular (r, viewDir, gi) + d;
		}

		inline void LightingStandardSpecularCustom_GI(SurfaceOutputStandardSpecularCustom s, UnityGIInput data, inout UnityGI gi )
		{
			#if defined(UNITY_PASS_DEFERRED) && UNITY_ENABLE_REFLECTION_BUFFERS
				gi = UnityGlobalIllumination(data, s.Occlusion, s.Normal);
			#else
				UNITY_GLOSSY_ENV_FROM_SURFACE( g, s, data );
				gi = UnityGlobalIllumination( data, s.Occlusion, s.Normal, g );
			#endif
		}

		void surf( Input i , inout SurfaceOutputStandardSpecularCustom o )
		{
			half2 Main_UVs15_g61081 = i.vertexToFrag11_g61130;
			float4 tex2DNode117_g61081 = SAMPLE_TEXTURE2D( _MainNormalTex, sampler_MainAlbedoTex, Main_UVs15_g61081 );
			float2 appendResult88_g61100 = (float2(tex2DNode117_g61081.a , tex2DNode117_g61081.g));
			float2 temp_output_90_0_g61100 = ( (appendResult88_g61100*2.0 + -1.0) * _MainNormalValue );
			float3 appendResult91_g61100 = (float3(temp_output_90_0_g61100 , 1.0));
			half3 Main_Normal137_g61081 = appendResult91_g61100;
			float3 temp_output_13_0_g61146 = Main_Normal137_g61081;
			float3 switchResult12_g61146 = (((i.ASEVFace>0)?(temp_output_13_0_g61146):(( temp_output_13_0_g61146 * _render_normals_options ))));
			half3 Blend_Normal312_g61081 = switchResult12_g61146;
			half3 Final_Normal366_g61081 = Blend_Normal312_g61081;
			o.Normal = Final_Normal366_g61081;
			float4 tex2DNode29_g61081 = SAMPLE_TEXTURE2D( _MainAlbedoTex, sampler_MainAlbedoTex, Main_UVs15_g61081 );
			float3 temp_output_3639_0_g61081 = (tex2DNode29_g61081).rgb;
			float3 temp_output_51_0_g61081 = ( (_MainColor).rgb * temp_output_3639_0_g61081 );
			half3 Main_Albedo99_g61081 = temp_output_51_0_g61081;
			half3 Blend_Albedo265_g61081 = Main_Albedo99_g61081;
			half3 Blend_AlbedoTinted2808_g61081 = ( i.vertexToFrag11_g61151 * i.vertexToFrag11_g61150 * float3(1,1,1) * Blend_Albedo265_g61081 );
			float dotResult3616_g61081 = dot( Blend_AlbedoTinted2808_g61081 , float3(0.2126,0.7152,0.0722) );
			float3 temp_cast_0 = (dotResult3616_g61081).xxx;
			half Global_Colors_Influence3668_g61081 = i.vertexToFrag11_g61129;
			float3 lerpResult3618_g61081 = lerp( Blend_AlbedoTinted2808_g61081 , temp_cast_0 , Global_Colors_Influence3668_g61081);
			float3 PositionWS_PerVertex3905_g61081 = i.vertexToFrag3890_g61081;
			float3 Position58_g61175 = PositionWS_PerVertex3905_g61081;
			half4 Vegetation33_g61182 = SAMPLE_TEXTURE2D( TVE_ColorsTex_Vegetation, samplerTVE_ColorsTex_Vegetation, ( (TVE_ColorsCoord_Vegetation).zw + ( (TVE_ColorsCoord_Vegetation).xy * (Position58_g61175).xz ) ) );
			half4 Grass33_g61182 = SAMPLE_TEXTURE2D( TVE_ColorsTex_Grass, samplerTVE_ColorsTex_Grass, ( (TVE_ColorsCoord_Grass).zw + ( (TVE_ColorsCoord_Grass).xy * (Position58_g61175).xz ) ) );
			half4 Objects33_g61182 = SAMPLE_TEXTURE2D( TVE_ColorsTex_Objects, samplerTVE_ColorsTex_Objects, ( (TVE_ColorsCoord_Objects).zw + ( (TVE_ColorsCoord_Objects).xy * (Position58_g61175).xz ) ) );
			half4 localUSE_BUFFERS33_g61182 = USE_BUFFERS( Vegetation33_g61182 , Grass33_g61182 , Objects33_g61182 );
			float4 temp_output_46_0_g61175 = localUSE_BUFFERS33_g61182;
			half3 Global_ColorsTex_RGB1700_g61081 = (temp_output_46_0_g61175).xyz;
			#ifdef UNITY_COLORSPACE_GAMMA
				float staticSwitch1_g61144 = 2.0;
			#else
				float staticSwitch1_g61144 = 4.594794;
			#endif
			half3 Global_Colors1954_g61081 = ( (_LocalColors).rgb * ( Global_ColorsTex_RGB1700_g61081 * staticSwitch1_g61144 ) );
			float lerpResult3870_g61081 = lerp( 1.0 , i.vertexColor.r , _ColorsVariationValue);
			half Global_Colors_Value3650_g61081 = ( _GlobalColors * lerpResult3870_g61081 );
			float4 tex2DNode35_g61081 = SAMPLE_TEXTURE2D( _MainMaskTex, sampler_MainAlbedoTex, Main_UVs15_g61081 );
			half Main_Mask57_g61081 = tex2DNode35_g61081.b;
			float temp_output_7_0_g61145 = _ColorsMaskMinValue;
			half Global_Colors_Mask3692_g61081 = saturate( ( ( Main_Mask57_g61081 - temp_output_7_0_g61145 ) / ( _ColorsMaskMaxValue - temp_output_7_0_g61145 ) ) );
			float3 lerpResult3628_g61081 = lerp( Blend_AlbedoTinted2808_g61081 , ( lerpResult3618_g61081 * Global_Colors1954_g61081 ) , ( Global_Colors_Value3650_g61081 * Global_Colors_Mask3692_g61081 ));
			half3 Blend_AlbedoColored863_g61081 = lerpResult3628_g61081;
			float3 temp_output_799_0_g61081 = (_SubsurfaceColor).rgb;
			float dotResult3930_g61081 = dot( temp_output_799_0_g61081 , float3(0.2126,0.7152,0.0722) );
			float3 temp_cast_4 = (dotResult3930_g61081).xxx;
			float3 lerpResult3932_g61081 = lerp( temp_output_799_0_g61081 , temp_cast_4 , Global_Colors_Influence3668_g61081);
			float3 lerpResult3942_g61081 = lerp( temp_output_799_0_g61081 , ( lerpResult3932_g61081 * Global_Colors1954_g61081 ) , ( Global_Colors_Value3650_g61081 * Global_Colors_Mask3692_g61081 ));
			half3 Subsurface_Color1722_g61081 = lerpResult3942_g61081;
			half MainLight_Subsurface4041_g61081 = TVE_MainLightParams.a;
			half Subsurface_Intensity1752_g61081 = ( _SubsurfaceValue * MainLight_Subsurface4041_g61081 );
			float temp_output_7_0_g61142 = _SubsurfaceMaskMinValue;
			half Subsurface_Mask1557_g61081 = saturate( ( ( Main_Mask57_g61081 - temp_output_7_0_g61142 ) / ( _SubsurfaceMaskMaxValue - temp_output_7_0_g61142 ) ) );
			half3 Subsurface_Transmission884_g61081 = ( Subsurface_Color1722_g61081 * Subsurface_Intensity1752_g61081 * Subsurface_Mask1557_g61081 );
			half3 MainLight_Direction3926_g61081 = TVE_MainLightDirection;
			float3 ase_worldPos = i.worldPos;
			float3 normalizeResult2169_g61081 = normalize( ( _WorldSpaceCameraPos - ase_worldPos ) );
			float3 ViewDir_Normalized3963_g61081 = normalizeResult2169_g61081;
			float dotResult785_g61081 = dot( -MainLight_Direction3926_g61081 , ViewDir_Normalized3963_g61081 );
			float saferPower1624_g61081 = max( (dotResult785_g61081*0.5 + 0.5) , 0.0001 );
			#ifdef UNITY_PASS_FORWARDADD
				float staticSwitch1602_g61081 = 0.0;
			#else
				float staticSwitch1602_g61081 = ( pow( saferPower1624_g61081 , _MainLightAngleValue ) * _MainLightScatteringValue );
			#endif
			half Mask_Subsurface_View782_g61081 = staticSwitch1602_g61081;
			half3 Subsurface_Forward1691_g61081 = ( Subsurface_Transmission884_g61081 * Mask_Subsurface_View782_g61081 * Blend_AlbedoColored863_g61081 );
			half3 Blend_AlbedoAndSubsurface149_g61081 = ( Blend_AlbedoColored863_g61081 + Subsurface_Forward1691_g61081 );
			half3 Global_OverlayColor1758_g61081 = (TVE_OverlayColor).rgb;
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float3 ase_normWorldNormal = normalize( ase_worldNormal );
			float lerpResult3567_g61081 = lerp( _OverlayBottomValue , 1.0 , ase_normWorldNormal.y);
			half Main_AlbedoTex_G3526_g61081 = tex2DNode29_g61081.g;
			float3 Position82_g61110 = PositionWS_PerVertex3905_g61081;
			half4 Vegetation33_g61117 = SAMPLE_TEXTURE2D( TVE_ExtrasTex_Vegetation, samplerTVE_ExtrasTex_Vegetation, ( (TVE_ExtrasCoord_Vegetation).zw + ( (TVE_ExtrasCoord_Vegetation).xy * (Position82_g61110).xz ) ) );
			half4 Grass33_g61117 = SAMPLE_TEXTURE2D( TVE_ExtrasTex_Grass, samplerTVE_ExtrasTex_Grass, ( (TVE_ExtrasCoord_Grass).zw + ( (TVE_ExtrasCoord_Grass).xy * (Position82_g61110).xz ) ) );
			half4 Objects33_g61117 = SAMPLE_TEXTURE2D( TVE_ExtrasTex_Objects, samplerTVE_ExtrasTex_Objects, ( (TVE_ExtrasCoord_Objects).zw + ( (TVE_ExtrasCoord_Objects).xy * (Position82_g61110).xz ) ) );
			half4 localUSE_BUFFERS33_g61117 = USE_BUFFERS( Vegetation33_g61117 , Grass33_g61117 , Objects33_g61117 );
			float4 break49_g61110 = localUSE_BUFFERS33_g61117;
			half Global_Extras_Overlay156_g61081 = break49_g61110.z;
			float temp_output_1025_0_g61081 = ( _GlobalOverlay * Global_Extras_Overlay156_g61081 );
			float lerpResult1065_g61081 = lerp( 1.0 , i.vertexColor.r , _OverlayVariationValue);
			half Overlay_Commons1365_g61081 = ( temp_output_1025_0_g61081 * lerpResult1065_g61081 );
			float temp_output_7_0_g61167 = _OverlayMaskMinValue;
			half Overlay_Mask269_g61081 = saturate( ( ( ( ( ( lerpResult3567_g61081 * 0.5 ) + Main_AlbedoTex_G3526_g61081 ) * Overlay_Commons1365_g61081 ) - temp_output_7_0_g61167 ) / ( _OverlayMaskMaxValue - temp_output_7_0_g61167 ) ) );
			float3 lerpResult336_g61081 = lerp( Blend_AlbedoAndSubsurface149_g61081 , Global_OverlayColor1758_g61081 , Overlay_Mask269_g61081);
			half3 Final_Albedo359_g61081 = lerpResult336_g61081;
			float Main_Alpha316_g61081 = ( _MainColor.a * tex2DNode29_g61081.a );
			float lerpResult354_g61081 = lerp( 1.0 , Main_Alpha316_g61081 , _render_premul);
			half Final_Premultiply355_g61081 = lerpResult354_g61081;
			float3 temp_output_410_0_g61081 = ( Final_Albedo359_g61081 * Final_Premultiply355_g61081 );
			o.Albedo = ( temp_output_410_0_g61081 * i.vertexToFrag11_g61137 );
			float3 temp_cast_8 = (( 0.04 * _RenderSpecular )).xxx;
			o.Specular = temp_cast_8;
			half Main_Smoothness227_g61081 = ( tex2DNode35_g61081.a * _MainSmoothnessValue );
			half Blend_Smoothness314_g61081 = Main_Smoothness227_g61081;
			half Global_OverlaySmoothness311_g61081 = TVE_OverlaySmoothness;
			float lerpResult343_g61081 = lerp( Blend_Smoothness314_g61081 , Global_OverlaySmoothness311_g61081 , Overlay_Mask269_g61081);
			half Final_Smoothness371_g61081 = lerpResult343_g61081;
			half Global_Extras_Wetness305_g61081 = break49_g61110.y;
			float lerpResult3673_g61081 = lerp( 0.0 , Global_Extras_Wetness305_g61081 , _GlobalWetness);
			o.Smoothness = saturate( ( Final_Smoothness371_g61081 + lerpResult3673_g61081 ) );
			float lerpResult240_g61081 = lerp( 1.0 , tex2DNode35_g61081.g , _MainOcclusionValue);
			half Main_Occlusion247_g61081 = lerpResult240_g61081;
			half Blend_Occlusion323_g61081 = Main_Occlusion247_g61081;
			o.Occlusion = Blend_Occlusion323_g61081;
			o.Transmission = Subsurface_Transmission884_g61081;
			float localCustomAlphaClip3735_g61081 = ( 0.0 );
			float3 normalizeResult3971_g61081 = normalize( cross( ddy( ase_worldPos ) , ddx( ase_worldPos ) ) );
			float3 NormalsWS_Derivates3972_g61081 = normalizeResult3971_g61081;
			float dotResult3851_g61081 = dot( ViewDir_Normalized3963_g61081 , NormalsWS_Derivates3972_g61081 );
			float lerpResult3993_g61081 = lerp( 1.0 , abs( dotResult3851_g61081 ) , _FadeGlancingValue);
			half Fade_Glancing3853_g61081 = lerpResult3993_g61081;
			half Fade_Camera3743_g61081 = i.vertexToFrag11_g61082;
			half Final_AlphaFade3727_g61081 = ( Fade_Glancing3853_g61081 * Fade_Camera3743_g61081 );
			float temp_output_41_0_g61087 = Final_AlphaFade3727_g61081;
			float Mesh_Variation16_g61081 = i.vertexColor.r;
			float lerpResult4033_g61081 = lerp( 0.9 , (Mesh_Variation16_g61081*0.5 + 0.5) , _AlphaVariationValue);
			half Global_Extras_Alpha1033_g61081 = break49_g61110.w;
			float temp_output_4022_0_g61081 = ( lerpResult4033_g61081 - ( 1.0 - Global_Extras_Alpha1033_g61081 ) );
			half AlphaTreshold2132_g61081 = _Cutoff;
			#ifdef TVE_ALPHA_CLIP
				float staticSwitch4017_g61081 = ( temp_output_4022_0_g61081 + AlphaTreshold2132_g61081 );
			#else
				float staticSwitch4017_g61081 = temp_output_4022_0_g61081;
			#endif
			float lerpResult4011_g61081 = lerp( 1.0 , staticSwitch4017_g61081 , _GlobalAlpha);
			half Global_Alpha315_g61081 = saturate( ( lerpResult4011_g61081 * _LocalAlpha ) );
			#ifdef TVE_ALPHA_CLIP
				float staticSwitch3792_g61081 = ( ( Main_Alpha316_g61081 * Global_Alpha315_g61081 ) - ( AlphaTreshold2132_g61081 - 0.5 ) );
			#else
				float staticSwitch3792_g61081 = ( Main_Alpha316_g61081 * Global_Alpha315_g61081 );
			#endif
			half Final_Alpha3754_g61081 = staticSwitch3792_g61081;
			float temp_output_661_0_g61081 = ( saturate( ( temp_output_41_0_g61087 + ( temp_output_41_0_g61087 * SAMPLE_TEXTURE3D( TVE_ScreenTex3D, samplerTVE_ScreenTex3D, ( TVE_ScreenTexCoord * PositionWS_PerVertex3905_g61081 ) ).r ) ) ) * Final_Alpha3754_g61081 );
			float Alpha3735_g61081 = temp_output_661_0_g61081;
			float Treshold3735_g61081 = 0.5;
			{
			#if TVE_ALPHA_CLIP
				clip(Alpha3735_g61081 - Treshold3735_g61081);
			#endif
			}
			half Final_Clip914_g61081 = saturate( Alpha3735_g61081 );
			o.Alpha = Final_Clip914_g61081;
		}

		ENDCG
	}
	Fallback "Hidden/BOXOPHOBIC/The Vegetation Engine/Fallback"
	CustomEditor "TVEShaderCoreGUI"
}
/*ASEBEGIN
Version=18806
1920;1;1906;1021;2747.781;405.3591;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;10;-2176,-640;Half;False;Property;_render_cull;_render_cull;242;1;[HideInInspector];Create;True;0;3;Both;0;Back;1;Front;2;0;True;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;994;-1696,-768;Half;False;Property;_IsBalancedShader;_IsBalancedShader;241;1;[HideInInspector];Create;True;0;0;0;True;0;False;1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;31;-2176,-1024;Half;False;Property;_Banner;Banner;0;0;Create;True;0;0;0;True;1;StyledBanner(Leaf Standard Lit (Subsurface));False;0;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;907;-1456,-640;Half;False;Property;_subsurface_shadow;_subsurface_shadow;235;1;[HideInInspector];Create;True;0;0;0;True;0;False;1;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;471;-2176,384;Inherit;False;Define TVE_IS_VEGETATION_SHADER;-1;;61184;b458122dd75182d488380bd0f592b9e6;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-2000,-640;Half;False;Property;_render_src;_render_src;243;1;[HideInInspector];Create;True;0;0;0;True;0;False;5;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;824;-1920,-768;Half;False;Property;_IsTarget40Shader;_IsTarget40Shader;239;1;[HideInInspector];Create;True;0;0;0;True;0;False;1;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;404;-2176,-768;Half;False;Property;_IsForwardPathShader;_IsForwardPathShader;238;1;[HideInInspector];Create;True;0;0;0;True;0;False;1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;698;-2176,448;Inherit;False;Define TVE_USE_VEGETATION_BUFFERS;-1;;61183;1ad73017b051a444d8dd4dba6e00b9ca;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;822;-1760,-896;Half;False;Property;_IsSubsurfaceShader;_IsSubsurfaceShader;240;1;[HideInInspector];Create;True;0;0;0;True;0;False;1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;1055;-2176,-258;Inherit;False;Base Shader;1;;61081;856f7164d1c579d43a5cf4968a75ca43;72,3882,1,3880,1,3957,1,4028,1,4029,1,3904,1,3903,1,3900,1,3908,1,1300,1,1298,1,3586,0,1271,1,3889,0,3658,1,1708,1,3509,1,1712,2,3873,1,1714,1,1717,1,1718,1,1715,1,916,1,1762,0,1763,0,3568,1,1949,1,1776,1,3475,1,893,1,1745,1,3479,0,3501,1,3221,2,1646,1,1690,0,1757,0,3960,0,2807,1,3886,0,2953,1,3887,0,3243,0,3888,0,3728,1,3949,0,2172,1,3883,0,2658,0,1742,1,3484,0,1735,0,3575,0,1734,0,1736,0,1733,0,1737,0,878,0,1550,0,860,1,3544,1,2261,1,2260,1,2054,1,2032,1,2060,1,2036,1,2039,1,2062,1,3592,1,2750,0;0;15;FLOAT3;0;FLOAT3;528;FLOAT3;2489;FLOAT;3678;FLOAT;529;FLOAT;530;FLOAT;531;FLOAT;1235;FLOAT3;1230;FLOAT;1461;FLOAT;1290;FLOAT;721;FLOAT;532;FLOAT;629;FLOAT3;534
Node;AmplifyShaderEditor.RangedFloatNode;7;-1808,-640;Half;False;Property;_render_dst;_render_dst;244;1;[HideInInspector];Create;True;0;2;Opaque;0;Transparent;1;0;True;0;False;10;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;81;-1984,-896;Half;False;Property;_IsStandardShader;_IsStandardShader;237;1;[HideInInspector];Create;True;0;0;0;True;0;False;1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-1632,-640;Half;False;Property;_render_zw;_render_zw;245;1;[HideInInspector];Create;True;0;2;Opaque;0;Transparent;1;0;True;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;168;-2176,-896;Half;False;Property;_IsLeafShader;_IsLeafShader;236;1;[HideInInspector];Create;True;0;0;0;True;0;False;1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;1032;-1376,-256;Float;False;True;-1;4;TVEShaderCoreGUI;0;0;StandardSpecular;BOXOPHOBIC/The Vegetation Engine/Vegetation/Leaf Standard Lit (Subsurface);False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;False;True;Back;0;True;17;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Opaque;;Geometry;ForwardOnly;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;1;0;True;20;0;True;7;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Absolute;0;Hidden/BOXOPHOBIC/The Vegetation Engine/Fallback;246;-1;-1;-1;0;False;0;0;True;10;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;True;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
Node;AmplifyShaderEditor.CommentaryNode;449;-2176,256;Inherit;False;1026.438;100;Features;0;;0,1,0.5,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;37;-2176,-1152;Inherit;False;1024.392;100;Internal;0;;1,0.252,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;33;-2176,-384;Inherit;False;1024.392;100;Final;0;;0,1,0.5,1;0;0
WireConnection;1032;0;1055;0
WireConnection;1032;1;1055;528
WireConnection;1032;3;1055;3678
WireConnection;1032;4;1055;530
WireConnection;1032;5;1055;531
WireConnection;1032;6;1055;1230
WireConnection;1032;9;1055;532
WireConnection;1032;11;1055;534
ASEEND*/
//CHKSM=F7BB2AE9B21D7C9315E520EF1EA109C7E5D042F4