// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "BOXOPHOBIC/The Vegetation Engine/Objects/Prop Standard Lit (Deferred)"
{
	Properties
	{
		[StyledBanner(Prop Standard Lit (Deferred))]_Banner("Banner", Float) = 0
		[StyledCategory(Render Settings, 5, 10)]_RenderingCat("[ Rendering Cat ]", Float) = 0
		[Enum(Opaque,0,Transparent,1)]_RenderMode("Render Mode", Float) = 0
		[Enum(Alpha Blend,0,Premultiply,1)]_RenderBlend("Render Blend", Float) = 0
		[Enum(Off,0,On,1)]_RenderZWrite("Render ZWrite", Float) = 1
		[IntRange]_RenderPriority("Render Priority", Range( -100 , 100)) = 0
		[Enum(Both,0,Back,1,Front,2)]_RenderCull("Render Faces", Float) = 0
		[Enum(Flip,0,Mirror,1,Same,2)]_RenderNormals("Render Normals", Float) = 0
		[Enum(Off,0,On,1)][Space(10)]_RenderClip("Alpha Clipping", Float) = 1
		_Cutoff("Alpha Treshold", Range( 0 , 1)) = 0.5
		[StyledSpace(10)]_FadeSpace("# Fade Space", Float) = 0
		[StyledCategory(Global Settings)]_GlobalCat("[ Global Cat ]", Float) = 0
		[StyledMessage(Warning, Procedural Variation in use. The Variation might not work as expected when switching from one LOD to another., _VertexVariationMode, 1 , 0, 10)]_VariationGlobalsMessage("# Variation Globals Message", Float) = 0
		_GlobalOverlay("Global Overlay", Range( 0 , 1)) = 1
		_GlobalWetness("Global Wetness", Range( 0 , 1)) = 1
		[StyledRemapSlider(_ColorsMaskMinValue, _ColorsMaskMaxValue, 0, 1, 10, 0)]_ColorsMaskRemap("Colors Mask", Vector) = (0,0,0,0)
		[StyledRemapSlider(_OverlayMaskMinValue, _OverlayMaskMaxValue, 0, 1, 10, 0)]_OverlayMaskRemap("Overlay Mask", Vector) = (0,0,0,0)
		[HideInInspector]_OverlayMaskMinValue("Overlay Mask Min Value", Range( 0 , 1)) = 0.45
		[HideInInspector]_OverlayMaskMaxValue("Overlay Mask Max Value", Range( 0 , 1)) = 0.55
		_OverlayBottomValue("Overlay Bottom", Range( 0 , 1)) = 0.5
		[StyledCategory(Main Settings)]_MainCat("[ Main Cat ]", Float) = 0
		[NoScaleOffset]_MainAlbedoTex("Main Albedo", 2D) = "white" {}
		[NoScaleOffset]_MainNormalTex("Main Normal", 2D) = "gray" {}
		[NoScaleOffset]_MainMaskTex("Main Mask", 2D) = "white" {}
		[Space(10)]_MainUVs("Main UVs", Vector) = (1,1,0,0)
		[HDR]_MainColor("Main Color", Color) = (1,1,1,1)
		_MainNormalValue("Main Normal", Range( -8 , 8)) = 1
		_MainMetallicValue("Main Metallic", Range( 0 , 1)) = 0
		_MainOcclusionValue("Main Occlusion", Range( 0 , 1)) = 1
		_MainSmoothnessValue("Main Smoothness", Range( 0 , 1)) = 1
		[StyledCategory(Detail Settings)]_DetailCat("[ Detail Cat ]", Float) = 0
		[Enum(Off,0,Overlay,1,Replace,2)]_DetailMode("Detail Mode", Float) = 0
		[Enum(Vertex Paint,0,Projection,1)]_DetailTypeMode("Detail Type", Float) = 0
		[Enum(UV 0,0,UV 2,1)]_DetailCoordMode("Detail Coord", Float) = 0
		[Enum(Top,0,Bottom,1)]_DetailProjectionMode("Detail Projection", Float) = 0
		[Enum(Standard,0,Packed,1)]_DetailMapsMode("Detail Maps", Float) = 0
		[StyledSpace(10)]_DetailSpace("# Detail Space", Float) = 0
		[NoScaleOffset]_SecondPackedTex("Detail Packed", 2D) = "white" {}
		[NoScaleOffset]_SecondAlbedoTex("Detail Albedo", 2D) = "white" {}
		[NoScaleOffset]_SecondNormalTex("Detail Normal", 2D) = "gray" {}
		[NoScaleOffset]_SecondMaskTex("Detail Mask", 2D) = "white" {}
		[Space(10)]_SecondUVs("Detail UVs", Vector) = (1,1,0,0)
		[HDR]_SecondColor("Detail Color", Color) = (1,1,1,1)
		_SecondNormalValue("Detail Normal", Range( -8 , 8)) = 1
		_SecondMetallicValue("Detail Metallic", Range( 0 , 1)) = 0
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
		[StyledRemapSlider(_VertexOcclusionMinValue, _VertexOcclusionMaxValue, 0, 1)]_VertexOcclusionRemap("Vertex Occlusion Mask", Vector) = (0,0,0,0)
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
		[StyledCategory(Motion Settings)]_MotionCat("[ Motion Cat ]", Float) = 0
		[StyledMessage(Info, The Baked pivots feature allows for using per mesh element interaction and elements influence. This feature requires pre baked pivots on prefab conversion. Useful for latge grass meshes., _VertexPivotMode, 1 , 0, 10)]_PivotsMessage("# Pivots Message", Float) = 0
		[StyledSpace(10)]_MotionSpace("# Motion Space", Float) = 0
		[StyledMessage(Warning, Procedural variation in use. Use the Scale settings if the Variation is breaking the bending and rolling animation., _VertexVariationMode, 1 , 0, 10)]_VariationMotionMessage("# Variation Motion Message", Float) = 0
		[StyledCategory(Advanced Settings)]_AdvancedCat("[ Advanced Cat]", Float) = 0
		[StyledMessage(Info, Use the Batching Support option when the object is statically batched. All vertex calculations are done in world space and features like Baked Pivots and Size options are not supported because the object pivot data is missing with static batching., _VertexDataMode, 1 , 2,10)]_BatchingMessage("# Batching Message", Float) = 0
		[HideInInspector]_IsTVEShader("_IsTVEShader", Float) = 1
		[HideInInspector]_IsVersion("_IsVersion", Float) = 310
		[HideInInspector]_Color("Legacy Color", Color) = (0,0,0,0)
		[HideInInspector]_MainTex("Legacy MainTex", 2D) = "white" {}
		[HideInInspector]_BumpMap("Legacy BumpMap", 2D) = "white" {}
		[HideInInspector]_MaxBoundsInfo("_MaxBoundsInfo", Vector) = (1,1,1,1)
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
		[HideInInspector]_IsPropShader("_IsPropShader", Float) = 1
		[HideInInspector]_IsAnyPathShader("_IsAnyPathShader", Float) = 1
		[HideInInspector]_IsTarget40Shader("_IsTarget40Shader", Float) = 1
		[HideInInspector]_IsBalancedShader("_IsBalancedShader", Float) = 1
		[HideInInspector]_IsStandardShader("_IsStandardShader", Float) = 1
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
		#include "UnityStandardUtils.cginc"
		#pragma target 4.0
		#pragma shader_feature_local TVE_ALPHA_CLIP
		#pragma shader_feature_local TVE_DETAIL_MODE_OFF TVE_DETAIL_MODE_OVERLAY TVE_DETAIL_MODE_REPLACE
		#pragma shader_feature_local TVE_DETAIL_MAPS_STANDARD TVE_DETAIL_MAPS_PACKED
		#pragma shader_feature_local TVE_DETAIL_TYPE_VERTEX_BLUE TVE_DETAIL_TYPE_PROJECTION
		#define TVE_IS_OBJECT_SHADER
		  
		#define THE_VEGETATION_ENGINE
		    
		//SHADER INJECTION POINT BEGIN
		//SHADER INJECTION POINT END
		      
		#define TVE_VERTEX_DATA_BATCHED
		#define TVE_USE_OBJECT_BUFFERS
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

		#pragma surface surf Standard keepalpha addshadow fullforwardshadows dithercrossfade vertex:vertexDataFunc 
		struct Input
		{
			float2 vertexToFrag11_g45416;
			float2 vertexToFrag11_g45425;
			float3 worldPos;
			float vertexToFrag11_g45439;
			half ASEVFace : VFACE;
			float3 worldNormal;
			INTERNAL_DATA
			float3 vertexToFrag3890_g45367;
		};

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
		uniform half _IsTarget40Shader;
		uniform half _render_zw;
		uniform half _render_src;
		uniform half _IsAnyPathShader;
		uniform half _render_dst;
		uniform half _render_cull;
		uniform half _Banner;
		uniform half _IsPropShader;
		uniform half _IsStandardShader;
		uniform half _IsBalancedShader;
		UNITY_DECLARE_TEX2D_NOSAMPLER(_MainNormalTex);
		uniform half4 _MainUVs;
		UNITY_DECLARE_TEX2D_NOSAMPLER(_MainAlbedoTex);
		SamplerState sampler_MainAlbedoTex;
		uniform half _MainNormalValue;
		UNITY_DECLARE_TEX2D_NOSAMPLER(_SecondNormalTex);
		uniform half _DetailCoordMode;
		uniform half4 _SecondUVs;
		UNITY_DECLARE_TEX2D_NOSAMPLER(_SecondMaskTex);
		SamplerState sampler_SecondMaskTex;
		uniform half _SecondNormalValue;
		UNITY_DECLARE_TEX2D_NOSAMPLER(_SecondPackedTex);
		uniform half _DetailMeshValue;
		uniform half _DetailProjectionMode;
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
		uniform half _MainMetallicValue;
		uniform half _SecondMetallicValue;
		uniform half _MainSmoothnessValue;
		uniform half _SecondSmoothnessValue;
		uniform half TVE_OverlaySmoothness;
		uniform half _GlobalWetness;
		uniform half _MainOcclusionValue;
		uniform half _SecondOcclusionValue;
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
			o.vertexToFrag11_g45416 = ( ( v.texcoord.xy * (_MainUVs).xy ) + (_MainUVs).zw );
			float2 lerpResult1545_g45367 = lerp( v.texcoord.xy , v.texcoord1.xy , _DetailCoordMode);
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			#if defined(TVE_DETAIL_TYPE_VERTEX_BLUE)
				float2 staticSwitch3466_g45367 = lerpResult1545_g45367;
			#elif defined(TVE_DETAIL_TYPE_PROJECTION)
				float2 staticSwitch3466_g45367 = (ase_worldPos).xz;
			#else
				float2 staticSwitch3466_g45367 = lerpResult1545_g45367;
			#endif
			o.vertexToFrag11_g45425 = ( ( staticSwitch3466_g45367 * (_SecondUVs).xy ) + (_SecondUVs).zw );
			half Mesh_DetailMask90_g45367 = v.color.b;
			float temp_output_989_0_g45367 = ( ( Mesh_DetailMask90_g45367 - 0.5 ) + _DetailMeshValue );
			float3 ase_worldNormal = UnityObjectToWorldNormal( v.normal );
			float3 lerpResult1537_g45367 = lerp( float3(0,1,0) , float3(0,-1,0) , _DetailProjectionMode);
			float dotResult1532_g45367 = dot( ase_worldNormal , lerpResult1537_g45367 );
			#if defined(TVE_DETAIL_TYPE_VERTEX_BLUE)
				float staticSwitch3467_g45367 = temp_output_989_0_g45367;
			#elif defined(TVE_DETAIL_TYPE_PROJECTION)
				float staticSwitch3467_g45367 = ( ( dotResult1532_g45367 * 0.5 ) + _DetailMeshValue );
			#else
				float staticSwitch3467_g45367 = temp_output_989_0_g45367;
			#endif
			o.vertexToFrag11_g45439 = staticSwitch3467_g45367;
			o.vertexToFrag3890_g45367 = ase_worldPos;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			half2 Main_UVs15_g45367 = i.vertexToFrag11_g45416;
			float4 tex2DNode117_g45367 = SAMPLE_TEXTURE2D( _MainNormalTex, sampler_MainAlbedoTex, Main_UVs15_g45367 );
			float2 appendResult88_g45386 = (float2(tex2DNode117_g45367.a , tex2DNode117_g45367.g));
			float2 temp_output_90_0_g45386 = ( (appendResult88_g45386*2.0 + -1.0) * _MainNormalValue );
			float3 appendResult91_g45386 = (float3(temp_output_90_0_g45386 , 1.0));
			half3 Main_Normal137_g45367 = appendResult91_g45386;
			half2 Second_UVs17_g45367 = i.vertexToFrag11_g45425;
			float4 tex2DNode145_g45367 = SAMPLE_TEXTURE2D( _SecondNormalTex, sampler_SecondMaskTex, Second_UVs17_g45367 );
			float2 appendResult88_g45377 = (float2(tex2DNode145_g45367.a , tex2DNode145_g45367.g));
			float2 temp_output_90_0_g45377 = ( (appendResult88_g45377*2.0 + -1.0) * _SecondNormalValue );
			float3 appendResult91_g45377 = (float3(temp_output_90_0_g45377 , 1.0));
			float4 tex2DNode3380_g45367 = SAMPLE_TEXTURE2D( _SecondPackedTex, sampler_SecondMaskTex, Second_UVs17_g45367 );
			half Packed_NormalX3387_g45367 = tex2DNode3380_g45367.a;
			half Packed_NormalY3386_g45367 = tex2DNode3380_g45367.g;
			float2 appendResult88_g45434 = (float2(Packed_NormalX3387_g45367 , Packed_NormalY3386_g45367));
			float2 temp_output_90_0_g45434 = ( (appendResult88_g45434*2.0 + -1.0) * _SecondNormalValue );
			float3 appendResult91_g45434 = (float3(temp_output_90_0_g45434 , 1.0));
			#if defined(TVE_DETAIL_MAPS_STANDARD)
				float3 staticSwitch3450_g45367 = appendResult91_g45377;
			#elif defined(TVE_DETAIL_MAPS_PACKED)
				float3 staticSwitch3450_g45367 = appendResult91_g45434;
			#else
				float3 staticSwitch3450_g45367 = appendResult91_g45377;
			#endif
			half3 Second_Normal179_g45367 = staticSwitch3450_g45367;
			half Blend_Source1540_g45367 = i.vertexToFrag11_g45439;
			float4 tex2DNode35_g45367 = SAMPLE_TEXTURE2D( _MainMaskTex, sampler_MainAlbedoTex, Main_UVs15_g45367 );
			half Main_Mask57_g45367 = tex2DNode35_g45367.b;
			float4 tex2DNode33_g45367 = SAMPLE_TEXTURE2D( _SecondMaskTex, sampler_SecondMaskTex, Second_UVs17_g45367 );
			half Second_Mask81_g45367 = tex2DNode33_g45367.b;
			float lerpResult1327_g45367 = lerp( Main_Mask57_g45367 , Second_Mask81_g45367 , _DetailMaskMode);
			float lerpResult4058_g45367 = lerp( lerpResult1327_g45367 , ( 1.0 - lerpResult1327_g45367 ) , _DetailMaskInvertMode);
			float temp_output_7_0_g45410 = _DetailBlendMinValue;
			half Mask_Detail147_g45367 = saturate( ( ( saturate( ( Blend_Source1540_g45367 + ( Blend_Source1540_g45367 * lerpResult4058_g45367 ) ) ) - temp_output_7_0_g45410 ) / ( _DetailBlendMaxValue - temp_output_7_0_g45410 ) ) );
			float3 lerpResult230_g45367 = lerp( float3( 0,0,1 ) , Second_Normal179_g45367 , Mask_Detail147_g45367);
			float3 lerpResult3372_g45367 = lerp( float3( 0,0,1 ) , Main_Normal137_g45367 , _DetailNormalValue);
			float3 lerpResult3376_g45367 = lerp( Main_Normal137_g45367 , BlendNormals( lerpResult3372_g45367 , Second_Normal179_g45367 ) , Mask_Detail147_g45367);
			#if defined(TVE_DETAIL_MODE_OFF)
				float3 staticSwitch267_g45367 = Main_Normal137_g45367;
			#elif defined(TVE_DETAIL_MODE_OVERLAY)
				float3 staticSwitch267_g45367 = BlendNormals( Main_Normal137_g45367 , lerpResult230_g45367 );
			#elif defined(TVE_DETAIL_MODE_REPLACE)
				float3 staticSwitch267_g45367 = lerpResult3376_g45367;
			#else
				float3 staticSwitch267_g45367 = Main_Normal137_g45367;
			#endif
			float3 temp_output_13_0_g45432 = staticSwitch267_g45367;
			float3 switchResult12_g45432 = (((i.ASEVFace>0)?(temp_output_13_0_g45432):(( temp_output_13_0_g45432 * _render_normals_options ))));
			half3 Blend_Normal312_g45367 = switchResult12_g45432;
			half3 Final_Normal366_g45367 = Blend_Normal312_g45367;
			o.Normal = Final_Normal366_g45367;
			float4 tex2DNode29_g45367 = SAMPLE_TEXTURE2D( _MainAlbedoTex, sampler_MainAlbedoTex, Main_UVs15_g45367 );
			float3 temp_output_3639_0_g45367 = (tex2DNode29_g45367).rgb;
			float3 temp_output_51_0_g45367 = ( (_MainColor).rgb * temp_output_3639_0_g45367 );
			half3 Main_Albedo99_g45367 = temp_output_51_0_g45367;
			half Packed_Albedo3385_g45367 = tex2DNode3380_g45367.r;
			float4 temp_cast_0 = (Packed_Albedo3385_g45367).xxxx;
			#if defined(TVE_DETAIL_MAPS_STANDARD)
				float4 staticSwitch3449_g45367 = SAMPLE_TEXTURE2D( _SecondAlbedoTex, sampler_SecondMaskTex, Second_UVs17_g45367 );
			#elif defined(TVE_DETAIL_MAPS_PACKED)
				float4 staticSwitch3449_g45367 = temp_cast_0;
			#else
				float4 staticSwitch3449_g45367 = SAMPLE_TEXTURE2D( _SecondAlbedoTex, sampler_SecondMaskTex, Second_UVs17_g45367 );
			#endif
			float3 temp_output_126_0_g45367 = (( _SecondColor * staticSwitch3449_g45367 )).rgb;
			half3 Second_Albedo153_g45367 = temp_output_126_0_g45367;
			#ifdef UNITY_COLORSPACE_GAMMA
				float staticSwitch1_g45435 = 2.0;
			#else
				float staticSwitch1_g45435 = 4.594794;
			#endif
			float3 lerpResult235_g45367 = lerp( Main_Albedo99_g45367 , ( Main_Albedo99_g45367 * Second_Albedo153_g45367 * staticSwitch1_g45435 ) , Mask_Detail147_g45367);
			float3 lerpResult208_g45367 = lerp( Main_Albedo99_g45367 , Second_Albedo153_g45367 , Mask_Detail147_g45367);
			#if defined(TVE_DETAIL_MODE_OFF)
				float3 staticSwitch255_g45367 = Main_Albedo99_g45367;
			#elif defined(TVE_DETAIL_MODE_OVERLAY)
				float3 staticSwitch255_g45367 = lerpResult235_g45367;
			#elif defined(TVE_DETAIL_MODE_REPLACE)
				float3 staticSwitch255_g45367 = lerpResult208_g45367;
			#else
				float3 staticSwitch255_g45367 = Main_Albedo99_g45367;
			#endif
			half3 Blend_Albedo265_g45367 = staticSwitch255_g45367;
			half3 Blend_AlbedoTinted2808_g45367 = ( float3(1,1,1) * float3(1,1,1) * float3(1,1,1) * Blend_Albedo265_g45367 );
			half3 Blend_AlbedoColored863_g45367 = Blend_AlbedoTinted2808_g45367;
			half3 Blend_AlbedoAndSubsurface149_g45367 = Blend_AlbedoColored863_g45367;
			half3 Global_OverlayColor1758_g45367 = (TVE_OverlayColor).rgb;
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float3 ase_normWorldNormal = normalize( ase_worldNormal );
			float lerpResult3567_g45367 = lerp( _OverlayBottomValue , 1.0 , ase_normWorldNormal.y);
			half Main_AlbedoTex_G3526_g45367 = tex2DNode29_g45367.g;
			half Second_AlbedoTex_G3581_g45367 = (staticSwitch3449_g45367).g;
			float lerpResult3579_g45367 = lerp( Main_AlbedoTex_G3526_g45367 , Second_AlbedoTex_G3581_g45367 , Mask_Detail147_g45367);
			#if defined(TVE_DETAIL_MODE_OFF)
				float staticSwitch3574_g45367 = Main_AlbedoTex_G3526_g45367;
			#elif defined(TVE_DETAIL_MODE_OVERLAY)
				float staticSwitch3574_g45367 = lerpResult3579_g45367;
			#elif defined(TVE_DETAIL_MODE_REPLACE)
				float staticSwitch3574_g45367 = lerpResult3579_g45367;
			#else
				float staticSwitch3574_g45367 = Main_AlbedoTex_G3526_g45367;
			#endif
			float3 PositionWS_PerVertex3905_g45367 = i.vertexToFrag3890_g45367;
			float3 Position82_g45396 = PositionWS_PerVertex3905_g45367;
			half4 Vegetation33_g45403 = SAMPLE_TEXTURE2D( TVE_ExtrasTex_Vegetation, samplerTVE_ExtrasTex_Vegetation, ( (TVE_ExtrasCoord_Vegetation).zw + ( (TVE_ExtrasCoord_Vegetation).xy * (Position82_g45396).xz ) ) );
			half4 Grass33_g45403 = SAMPLE_TEXTURE2D( TVE_ExtrasTex_Grass, samplerTVE_ExtrasTex_Grass, ( (TVE_ExtrasCoord_Grass).zw + ( (TVE_ExtrasCoord_Grass).xy * (Position82_g45396).xz ) ) );
			half4 Objects33_g45403 = SAMPLE_TEXTURE2D( TVE_ExtrasTex_Objects, samplerTVE_ExtrasTex_Objects, ( (TVE_ExtrasCoord_Objects).zw + ( (TVE_ExtrasCoord_Objects).xy * (Position82_g45396).xz ) ) );
			half4 localUSE_BUFFERS33_g45403 = USE_BUFFERS( Vegetation33_g45403 , Grass33_g45403 , Objects33_g45403 );
			float4 break49_g45396 = localUSE_BUFFERS33_g45403;
			half Global_Extras_Overlay156_g45367 = break49_g45396.z;
			float temp_output_1025_0_g45367 = ( _GlobalOverlay * Global_Extras_Overlay156_g45367 );
			half Overlay_Commons1365_g45367 = temp_output_1025_0_g45367;
			float temp_output_7_0_g45453 = _OverlayMaskMinValue;
			half Overlay_Mask269_g45367 = saturate( ( ( ( ( ( lerpResult3567_g45367 * 0.5 ) + staticSwitch3574_g45367 ) * Overlay_Commons1365_g45367 ) - temp_output_7_0_g45453 ) / ( _OverlayMaskMaxValue - temp_output_7_0_g45453 ) ) );
			float3 lerpResult336_g45367 = lerp( Blend_AlbedoAndSubsurface149_g45367 , Global_OverlayColor1758_g45367 , Overlay_Mask269_g45367);
			half3 Final_Albedo359_g45367 = lerpResult336_g45367;
			float Main_Alpha316_g45367 = ( _MainColor.a * tex2DNode29_g45367.a );
			float lerpResult354_g45367 = lerp( 1.0 , Main_Alpha316_g45367 , _render_premul);
			half Final_Premultiply355_g45367 = lerpResult354_g45367;
			float3 temp_output_410_0_g45367 = ( Final_Albedo359_g45367 * Final_Premultiply355_g45367 );
			o.Albedo = temp_output_410_0_g45367;
			half Main_Metallic237_g45367 = ( tex2DNode35_g45367.r * _MainMetallicValue );
			#if defined(TVE_DETAIL_MAPS_STANDARD)
				float staticSwitch3451_g45367 = ( tex2DNode33_g45367.r * _SecondMetallicValue );
			#elif defined(TVE_DETAIL_MAPS_PACKED)
				float staticSwitch3451_g45367 = 0.0;
			#else
				float staticSwitch3451_g45367 = ( tex2DNode33_g45367.r * _SecondMetallicValue );
			#endif
			half Second_Metallic226_g45367 = staticSwitch3451_g45367;
			float lerpResult278_g45367 = lerp( Main_Metallic237_g45367 , Second_Metallic226_g45367 , Mask_Detail147_g45367);
			#if defined(TVE_DETAIL_MODE_OFF)
				float staticSwitch299_g45367 = Main_Metallic237_g45367;
			#elif defined(TVE_DETAIL_MODE_OVERLAY)
				float staticSwitch299_g45367 = Main_Metallic237_g45367;
			#elif defined(TVE_DETAIL_MODE_REPLACE)
				float staticSwitch299_g45367 = lerpResult278_g45367;
			#else
				float staticSwitch299_g45367 = Main_Metallic237_g45367;
			#endif
			half Blend_Metallic306_g45367 = staticSwitch299_g45367;
			float lerpResult342_g45367 = lerp( Blend_Metallic306_g45367 , 0.0 , Overlay_Mask269_g45367);
			half Final_Metallic367_g45367 = lerpResult342_g45367;
			o.Metallic = Final_Metallic367_g45367;
			half Main_Smoothness227_g45367 = ( tex2DNode35_g45367.a * _MainSmoothnessValue );
			half Packed_Smoothness3388_g45367 = tex2DNode3380_g45367.b;
			#if defined(TVE_DETAIL_MAPS_STANDARD)
				float staticSwitch3456_g45367 = tex2DNode33_g45367.a;
			#elif defined(TVE_DETAIL_MAPS_PACKED)
				float staticSwitch3456_g45367 = Packed_Smoothness3388_g45367;
			#else
				float staticSwitch3456_g45367 = tex2DNode33_g45367.a;
			#endif
			half Second_Smoothness236_g45367 = ( staticSwitch3456_g45367 * _SecondSmoothnessValue );
			float lerpResult266_g45367 = lerp( Main_Smoothness227_g45367 , Second_Smoothness236_g45367 , Mask_Detail147_g45367);
			#if defined(TVE_DETAIL_MODE_OFF)
				float staticSwitch297_g45367 = Main_Smoothness227_g45367;
			#elif defined(TVE_DETAIL_MODE_OVERLAY)
				float staticSwitch297_g45367 = Main_Smoothness227_g45367;
			#elif defined(TVE_DETAIL_MODE_REPLACE)
				float staticSwitch297_g45367 = lerpResult266_g45367;
			#else
				float staticSwitch297_g45367 = Main_Smoothness227_g45367;
			#endif
			half Blend_Smoothness314_g45367 = staticSwitch297_g45367;
			half Global_OverlaySmoothness311_g45367 = TVE_OverlaySmoothness;
			float lerpResult343_g45367 = lerp( Blend_Smoothness314_g45367 , Global_OverlaySmoothness311_g45367 , Overlay_Mask269_g45367);
			half Final_Smoothness371_g45367 = lerpResult343_g45367;
			half Global_Extras_Wetness305_g45367 = break49_g45396.y;
			float lerpResult3673_g45367 = lerp( 0.0 , Global_Extras_Wetness305_g45367 , _GlobalWetness);
			o.Smoothness = saturate( ( Final_Smoothness371_g45367 + lerpResult3673_g45367 ) );
			float lerpResult240_g45367 = lerp( 1.0 , tex2DNode35_g45367.g , _MainOcclusionValue);
			half Main_Occlusion247_g45367 = lerpResult240_g45367;
			float lerpResult239_g45367 = lerp( 1.0 , tex2DNode33_g45367.g , _SecondOcclusionValue);
			#if defined(TVE_DETAIL_MAPS_STANDARD)
				float staticSwitch3455_g45367 = lerpResult239_g45367;
			#elif defined(TVE_DETAIL_MAPS_PACKED)
				float staticSwitch3455_g45367 = 1.0;
			#else
				float staticSwitch3455_g45367 = lerpResult239_g45367;
			#endif
			half Second_Occlusion251_g45367 = staticSwitch3455_g45367;
			float lerpResult294_g45367 = lerp( Main_Occlusion247_g45367 , Second_Occlusion251_g45367 , Mask_Detail147_g45367);
			#if defined(TVE_DETAIL_MODE_OFF)
				float staticSwitch310_g45367 = Main_Occlusion247_g45367;
			#elif defined(TVE_DETAIL_MODE_OVERLAY)
				float staticSwitch310_g45367 = ( Main_Occlusion247_g45367 * Second_Occlusion251_g45367 );
			#elif defined(TVE_DETAIL_MODE_REPLACE)
				float staticSwitch310_g45367 = lerpResult294_g45367;
			#else
				float staticSwitch310_g45367 = Main_Occlusion247_g45367;
			#endif
			half Blend_Occlusion323_g45367 = staticSwitch310_g45367;
			o.Occlusion = Blend_Occlusion323_g45367;
			float localCustomAlphaClip3735_g45367 = ( 0.0 );
			half Final_AlphaFade3727_g45367 = 1.0;
			float temp_output_41_0_g45373 = Final_AlphaFade3727_g45367;
			half AlphaTreshold2132_g45367 = _Cutoff;
			#ifdef TVE_ALPHA_CLIP
				float staticSwitch3792_g45367 = ( Main_Alpha316_g45367 - ( AlphaTreshold2132_g45367 - 0.5 ) );
			#else
				float staticSwitch3792_g45367 = Main_Alpha316_g45367;
			#endif
			half Final_Alpha3754_g45367 = staticSwitch3792_g45367;
			float temp_output_661_0_g45367 = ( saturate( ( temp_output_41_0_g45373 + ( temp_output_41_0_g45373 * SAMPLE_TEXTURE3D( TVE_ScreenTex3D, samplerTVE_ScreenTex3D, ( TVE_ScreenTexCoord * PositionWS_PerVertex3905_g45367 ) ).r ) ) ) * Final_Alpha3754_g45367 );
			float Alpha3735_g45367 = temp_output_661_0_g45367;
			float Treshold3735_g45367 = 0.5;
			{
			#if TVE_ALPHA_CLIP
				clip(Alpha3735_g45367 - Treshold3735_g45367);
			#endif
			}
			half Final_Clip914_g45367 = saturate( Alpha3735_g45367 );
			o.Alpha = Final_Clip914_g45367;
		}

		ENDCG
	}
	Fallback "Hidden/BOXOPHOBIC/The Vegetation Engine/Fallback"
	CustomEditor "TVEShaderCoreGUI"
}
/*ASEBEGIN
Version=18806
1920;1;1906;1021;2994.314;806.542;1;True;False
Node;AmplifyShaderEditor.FunctionNode;343;-2176,320;Inherit;False;Define TVE_IS_OBJECT_SHADER;-1;;39501;1237b3cc9fbfe714d8343c91216dc9b4;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;378;-1728,-896;Half;False;Property;_IsBalancedShader;_IsBalancedShader;238;1;[HideInInspector];Create;True;0;0;0;True;0;False;1;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;366;-2176,384;Inherit;False;Define TVE_USE_OBJECT_BUFFERS;-1;;45470;c20f59ce2bf28064a8b3b1101ae8d362;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;341;-2176,256;Inherit;False;Define TVE_VERTEX_DATA_BATCHED;-1;;45469;749c61e1189c7f8408d9e6db94560d1d;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;134;-1984,-1024;Half;False;Property;_IsStandardShader;_IsStandardShader;239;1;[HideInInspector];Create;True;0;0;0;True;0;False;1;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;127;-2176,-1024;Half;False;Property;_IsPropShader;_IsPropShader;235;1;[HideInInspector];Create;True;0;0;0;True;0;False;1;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;10;-2176,-768;Half;False;Property;_render_cull;_render_cull;240;1;[HideInInspector];Create;True;0;3;Both;0;Back;1;Front;2;0;True;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;31;-2176,-1152;Half;False;Property;_Banner;Banner;0;0;Create;True;0;0;0;True;1;StyledBanner(Prop Standard Lit (Deferred));False;0;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;342;-2176,-896;Half;False;Property;_IsAnyPathShader;_IsAnyPathShader;236;1;[HideInInspector];Create;True;0;0;0;True;0;False;1;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-1984,-768;Half;False;Property;_render_src;_render_src;241;1;[HideInInspector];Create;True;0;0;0;True;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-1600,-768;Half;False;Property;_render_zw;_render_zw;243;1;[HideInInspector];Create;True;0;2;Opaque;0;Transparent;1;0;True;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;371;-1952,-896;Half;False;Property;_IsTarget40Shader;_IsTarget40Shader;237;1;[HideInInspector];Create;True;0;0;0;True;0;False;1;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;382;-2176,-384;Inherit;False;Base Shader;1;;45367;856f7164d1c579d43a5cf4968a75ca43;72,3882,1,3880,1,3957,1,4028,1,4029,1,3904,1,3903,1,3900,1,3908,1,1300,1,1298,1,3586,0,1271,0,3889,1,3658,0,1708,0,3509,1,1712,0,3873,1,1714,1,1717,1,1718,1,1715,1,916,1,1762,0,1763,0,3568,1,1949,1,1776,0,3475,1,893,0,1745,0,3479,0,3501,0,3221,2,1646,0,1690,0,1757,0,3960,0,2807,0,3886,0,2953,0,3887,0,3243,0,3888,0,3728,0,3949,0,2172,0,3883,0,2658,0,1742,0,3484,0,1735,1,3575,1,1734,1,1736,1,1733,1,1737,1,878,1,1550,1,860,0,3544,1,2261,1,2260,1,2054,1,2032,1,2060,1,2036,1,2039,1,2062,1,3592,1,2750,0;0;15;FLOAT3;0;FLOAT3;528;FLOAT3;2489;FLOAT;3678;FLOAT;529;FLOAT;530;FLOAT;531;FLOAT;1235;FLOAT3;1230;FLOAT;1461;FLOAT;1290;FLOAT;721;FLOAT;532;FLOAT;629;FLOAT3;534
Node;AmplifyShaderEditor.RangedFloatNode;7;-1792,-768;Half;False;Property;_render_dst;_render_dst;242;1;[HideInInspector];Create;True;0;2;Opaque;0;Transparent;1;0;True;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;383;-1376,-384;Float;False;True;-1;4;TVEShaderCoreGUI;0;0;Standard;BOXOPHOBIC/The Vegetation Engine/Objects/Prop Standard Lit (Deferred);False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;False;True;Back;0;True;17;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;1;0;True;20;0;True;7;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Absolute;0;Hidden/BOXOPHOBIC/The Vegetation Engine/Fallback;244;-1;-1;-1;0;False;0;0;True;10;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;True;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
Node;AmplifyShaderEditor.CommentaryNode;33;-2176,-512;Inherit;False;1022.896;100;Final;0;;0,1,0.5,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;340;-2176,128;Inherit;False;1026.438;100;Features;0;;0,1,0.5,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;37;-2176,-1280;Inherit;False;1026.438;100;Internal;0;;1,0.252,0,1;0;0
WireConnection;383;0;382;0
WireConnection;383;1;382;528
WireConnection;383;3;382;529
WireConnection;383;4;382;530
WireConnection;383;5;382;531
WireConnection;383;9;382;532
ASEEND*/
//CHKSM=37D28B528A5A06E640385F359AB31D88C84B672B