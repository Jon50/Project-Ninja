// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "BOXOPHOBIC/The Vegetation Engine/Elements/Default/Colors Default"
{
	Properties
	{
		[StyledBanner(Color Element)]_Banner("Banner", Float) = 0
		[StyledMessage(Info, Use the Colors elements to add color tinting to the vegetation assets. Element Texture R is used as alpha mask. Particle Color RGB is used as Main multiplier and Alpha as Element Intensity multiplier., 0,0)]_Message("Message", Float) = 0
		[StyledCategory(Render Settings)]_RenderCat("[ Render Cat ]", Float) = 0
		_ElementIntensity("Element Intensity", Range( 0 , 1)) = 1
		[StyledMessage(Info, This Element requires a Volume Buffer set to Vegetation to render to and it will affect the Vegetation shaders only. Read more about Volume Buffers and Layers in the documentation., _ElementLayer, 10, 0, 10)]_ElementLayerVegetationMessage("Element Layer Vegetation Message", Float) = 0
		[StyledMessage(Info, This Element requires a Volume Buffer set to Objects to render to and it will affect the Objects shaders only. Read more about Volume Buffers and Layers in the documentation., _ElementLayer, 30, 0, 10)]_ElementLayerObjectsMessage("Element Layer Objects Message", Float) = 0
		[StyledMessage(Info, This Element requires a Volume Buffer set to Grass to render to and it will affect the Grass shaders only. Read more about Volume Buffers and Layers in the documentation., _ElementLayer, 20, 0, 10)]_ElementLayerGrassMessage("Element Layer Grass Message", Float) = 0
		[StyledMessage(Info, This Element requires a Volume Buffer set to Custom to render to and it will affect the Custom shaders only. Read more about Volume Buffers and Layers in the documentation., _ElementLayer, 100, 0, 10)]_ElementLayerCustomMessage("Element Layer Custom Message", Float) = 0
		[Enum(Any,0,Vegetation,10,Grass,20,Objects,30)]_ElementLayer("Element Layer", Float) = 0
		[Enum(Main,0,Seasons,1)]_ElementMode("Element Mode", Float) = 0
		[Enum(Multiply Material Colors,0,Replace Material Colors,1)]_ElementEffect("Element Effect", Float) = 0
		[StyledCategory(Element Settings)]_ElementCat("[ Element Cat ]", Float) = 0
		[NoScaleOffset][Space(10)]_MainTex("Element Texture", 2D) = "white" {}
		[StyledRemapSlider(_MainTexMinValue, _MainTexMaxValue, 0, 1)]_MainTexRemap("Element Remap", Vector) = (0,0,0,0)
		[HideInInspector]_MainTexMinValue("Element Min", Range( 0 , 1)) = 0
		[HideInInspector]_MainTexMaxValue("Element Max", Range( 0 , 1)) = 1
		_MainUVs("Element UVs", Vector) = (1,1,0,0)
		[HDR][Gamma]_MainColor("Main", Color) = (0.5019608,0.5019608,0.5019608,1)
		[HDR][Gamma]_AdditionalColor1("Winter", Color) = (0.5019608,0.5019608,0.5019608,1)
		[HDR][Gamma]_AdditionalColor2("Spring", Color) = (0.5019608,0.5019608,0.5019608,1)
		[HDR][Gamma]_AdditionalColor3("Summer", Color) = (0.5019608,0.5019608,0.5019608,1)
		[HDR][Gamma]_AdditionalColor4("Autumn", Color) = (0.5019608,0.5019608,0.5019608,1)
		[StyledRemapSlider(_NoiseMinValue, _NoiseMaxValue, 0, 1)]_NoiseRemap("Noise Remap", Vector) = (0,0,0,0)
		[StyledCategory(Advanced)]_AdvancedCat("[ Advanced Cat ]", Float) = 0
		[StyledMessage(Info, Use this feature to fade out elements at a distance to the camera to avoid rendering issues when the element is close to a volume edge. The Fade Start and End values are set on the Global Volume gameobject. Available in play mode only., _ElementFadeSupport, 1, 2, 10)]_ElementFadeMessage("Enable Fade Message", Float) = 0
		[ASEEnd][StyledToggle]_ElementFadeSupport("Enable Distance Fade Support", Float) = 1
		[HideInInspector]_IsVersion("_IsVersion", Float) = 300
		[HideInInspector]_IsElementShader("_IsElementShader", Float) = 1
		[HideInInspector]_WinterColor("_WinterColor", Color) = (0.5019608,0.5019608,0.5019608,1)
		[HideInInspector]_SpringColor("_SpringColor", Color) = (0.5019608,0.5019608,0.5019608,1)
		[HideInInspector]_SummerColor("_SummerColor", Color) = (0.5019608,0.5019608,0.5019608,1)
		[HideInInspector]_AutumnColor("_AutumnColor", Color) = (0.5019608,0.5019608,0.5019608,1)
		[HideInInspector]_WinterValue("_WinterValue", Float) = 0
		[HideInInspector]_SpringValue("_SpringValue", Float) = 0
		[HideInInspector]_SummerValue("_SummerValue", Float) = 0
		[HideInInspector]_AutumnValue("_AutumnValue", Float) = 0
		[HideInInspector]_IsColorsShader("_IsColorsShader", Float) = 1
		[HideInInspector]_render_colormask("_render_colormask", Float) = 14

	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Transparent" "Queue"="Transparent" "PreviewType"="Plane" }
	LOD 0

		CGINCLUDE
		#pragma target 2.0
		ENDCG
		Blend SrcAlpha OneMinusSrcAlpha
		AlphaToMask Off
		Cull Off
		ColorMask [_render_colormask]
		ZWrite Off
		ZTest LEqual
		
		
		
		Pass
		{
			Name "Unlit"
			CGPROGRAM

			

			#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
			//only defining to not throw compilation error over Unity 5.5
			#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
			#endif
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"
			#define ASE_NEEDS_FRAG_COLOR
			#define ASE_NEEDS_FRAG_WORLD_POSITION


			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 worldPos : TEXCOORD0;
				#endif
				float4 ase_color : COLOR;
				float4 ase_texcoord1 : TEXCOORD1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			uniform half _IsColorsShader;
			uniform half _Banner;
			uniform half _Message;
			uniform half _render_colormask;
			uniform half _SummerValue;
			uniform half _ElementLayerGrassMessage;
			uniform half4 _SummerColor;
			uniform half _ElementLayerVegetationMessage;
			uniform half4 _NoiseRemap;
			uniform half4 _AutumnColor;
			uniform half _ElementCat;
			uniform half4 _WinterColor;
			uniform half _AutumnValue;
			uniform half _RenderCat;
			uniform half4 _MainTexRemap;
			uniform half _IsElementShader;
			uniform half4 _SpringColor;
			uniform half _AdvancedCat;
			uniform half _SpringValue;
			uniform half _WinterValue;
			uniform half _ElementFadeMessage;
			uniform half _ElementLayerCustomMessage;
			uniform half _ElementLayer;
			uniform half _IsVersion;
			uniform half _ElementLayerObjectsMessage;
			uniform half _ElementEffect;
			uniform half4 _MainColor;
			uniform half4 TVE_SeasonOptions;
			uniform half4 _AdditionalColor1;
			uniform half4 _AdditionalColor2;
			uniform half TVE_SeasonLerp;
			uniform half4 _AdditionalColor3;
			uniform half4 _AdditionalColor4;
			uniform half _ElementMode;
			uniform sampler2D _MainTex;
			uniform half4 _MainUVs;
			uniform half _MainTexMinValue;
			uniform half _MainTexMaxValue;
			uniform half _ElementIntensity;
			uniform half TVE_ElementsFadeEnd;
			uniform half TVE_ElementsFadeStart;
			uniform half _ElementFadeSupport;

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.ase_color = v.color;
				o.ase_texcoord1.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord1.zw = 0;
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = vertexValue;
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);

				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				#endif
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				fixed4 finalColor;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 WorldPosition = i.worldPos;
				#endif
				half4 Color_Main_RGBA49_g18919 = _MainColor;
				half TVE_SeasonOptions_X50_g18919 = TVE_SeasonOptions.x;
				half4 Color_Winter_RGBA58_g18919 = _AdditionalColor1;
				half4 Color_Spring_RGBA59_g18919 = _AdditionalColor2;
				half TVE_SeasonLerp54_g18919 = TVE_SeasonLerp;
				half4 lerpResult13_g18919 = lerp( Color_Winter_RGBA58_g18919 , Color_Spring_RGBA59_g18919 , TVE_SeasonLerp54_g18919);
				half TVE_SeasonOptions_Y51_g18919 = TVE_SeasonOptions.y;
				half4 Color_Summer_RGBA60_g18919 = _AdditionalColor3;
				half4 lerpResult14_g18919 = lerp( Color_Spring_RGBA59_g18919 , Color_Summer_RGBA60_g18919 , TVE_SeasonLerp54_g18919);
				half TVE_SeasonOptions_Z52_g18919 = TVE_SeasonOptions.z;
				half4 Color_Autumn_RGBA61_g18919 = _AdditionalColor4;
				half4 lerpResult15_g18919 = lerp( Color_Summer_RGBA60_g18919 , Color_Autumn_RGBA61_g18919 , TVE_SeasonLerp54_g18919);
				half TVE_SeasonOptions_W53_g18919 = TVE_SeasonOptions.w;
				half4 lerpResult12_g18919 = lerp( Color_Autumn_RGBA61_g18919 , Color_Winter_RGBA58_g18919 , TVE_SeasonLerp54_g18919);
				half Element_Mode55_g18919 = _ElementMode;
				half4 lerpResult30_g18919 = lerp( Color_Main_RGBA49_g18919 , ( ( TVE_SeasonOptions_X50_g18919 * lerpResult13_g18919 ) + ( TVE_SeasonOptions_Y51_g18919 * lerpResult14_g18919 ) + ( TVE_SeasonOptions_Z52_g18919 * lerpResult15_g18919 ) + ( TVE_SeasonOptions_W53_g18919 * lerpResult12_g18919 ) ) , Element_Mode55_g18919);
				half4 temp_output_487_0_g18919 = ( lerpResult30_g18919 * i.ase_color );
				half3 temp_output_486_0_g18919 = (temp_output_487_0_g18919).rgb;
				half3 Final_Colors_RGB142_g18919 = temp_output_486_0_g18919;
				half4 tex2DNode17_g18919 = tex2D( _MainTex, ( ( ( 1.0 - i.ase_texcoord1.xy ) * (_MainUVs).xy ) + (_MainUVs).zw ) );
				half temp_output_7_0_g19036 = _MainTexMinValue;
				half4 temp_cast_0 = (temp_output_7_0_g19036).xxxx;
				half4 break469_g18919 = saturate( ( ( tex2DNode17_g18919 - temp_cast_0 ) / ( _MainTexMaxValue - temp_output_7_0_g19036 ) ) );
				half MainTex_R73_g18919 = break469_g18919.r;
				half temp_output_7_0_g19034 = TVE_ElementsFadeEnd;
				half Enable_Fade_Support454_g18919 = _ElementFadeSupport;
				half lerpResult654_g18919 = lerp( 1.0 , saturate( ( ( distance( WorldPosition , _WorldSpaceCameraPos ) - temp_output_7_0_g19034 ) / ( TVE_ElementsFadeStart - temp_output_7_0_g19034 ) ) ) , Enable_Fade_Support454_g18919);
				half FadeOut_Mask656_g18919 = lerpResult654_g18919;
				half Element_Intensity56_g18919 = ( _ElementIntensity * i.ase_color.a * FadeOut_Mask656_g18919 );
				half Final_Colors_A144_g18919 = ( temp_output_487_0_g18919.a * MainTex_R73_g18919 * Element_Intensity56_g18919 );
				half4 appendResult470_g18919 = (half4(Final_Colors_RGB142_g18919 , Final_Colors_A144_g18919));
				
				
				finalColor = ( ( _ElementEffect * 0.0 ) + appendResult470_g18919 );
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "TVEShaderElementGUI"
	
	
}
/*ASEBEGIN
Version=18806
1920;13;1906;1009;1204.167;1075.816;1;True;False
Node;AmplifyShaderEditor.FunctionNode;108;-304,-768;Inherit;False;Is Colors Element;57;;18813;378049ebac362e14aae08c2daa8ed737;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;97;-640,-768;Half;False;Property;_Banner;Banner;0;0;Create;True;0;0;0;True;1;StyledBanner(Color Element);False;0;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;100;-480,-768;Half;False;Property;_Message;Message;1;0;Create;True;0;0;0;True;1;StyledMessage(Info, Use the Colors elements to add color tinting to the vegetation assets. Element Texture R is used as alpha mask. Particle Color RGB is used as Main multiplier and Alpha as Element Intensity multiplier., 0,0);False;0;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;178;-640,-640;Half;False;Property;_render_colormask;_render_colormask;59;1;[HideInInspector];Create;True;0;0;0;True;0;False;14;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;181;-640,-512;Inherit;False;Base Element;2;;18919;0e972c73cae2ee54ea51acc9738801d0;6,477,0,478,0,145,0,481,0,576,1,491,1;0;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;-304,-512;Half;False;True;-1;2;TVEShaderElementGUI;0;1;BOXOPHOBIC/The Vegetation Engine/Elements/Default/Colors Default;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;True;True;2;5;False;-1;10;False;-1;0;1;False;-1;1;False;-1;True;0;False;-1;0;False;-1;False;False;False;False;False;False;False;False;False;True;0;False;-1;False;True;2;False;-1;True;True;True;True;True;False;0;True;178;False;False;False;False;False;False;False;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;False;True;2;False;-1;True;0;False;-1;True;False;0;False;-1;0;False;-1;True;3;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;PreviewType=Plane;True;0;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;1;True;False;;False;0
WireConnection;0;0;181;0
ASEEND*/
//CHKSM=E4275FDB86A0EA7F61A392FD96D17DB595CB02A1