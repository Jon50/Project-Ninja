// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "BOXOPHOBIC/The Vegetation Engine/Elements/Default/Vertex Size"
{
	Properties
	{
		[HideInInspector]_IsVertexShader("_IsVertexShader", Float) = 1
		[StyledBanner(Size Element)]_Banner("Banner", Float) = 0
		[StyledMessage(Info, Use the Size elements to add scale variation or combine it with seasons to add dynamic vegetation growing. Element Texture R is used as alpha mask. Particle Color R is used as values multiplier and Alpha as Element Intensity multiplier., 0,0)]_Message("Message", Float) = 0
		[StyledCategory(Render Settings)]_RenderCat("[ Render Cat ]", Float) = 0
		_ElementIntensity("Element Intensity", Range( 0 , 1)) = 1
		[StyledMessage(Info, This Element requires a Volume Buffer set to Vegetation to render to and it will affect the Vegetation shaders only. Read more about Volume Buffers and Layers in the documentation., _ElementLayer, 10, 0, 10)]_ElementLayerVegetationMessage("Element Layer Vegetation Message", Float) = 0
		[StyledMessage(Info, This Element requires a Volume Buffer set to Objects to render to and it will affect the Objects shaders only. Read more about Volume Buffers and Layers in the documentation., _ElementLayer, 30, 0, 10)]_ElementLayerObjectsMessage("Element Layer Objects Message", Float) = 0
		[StyledMessage(Info, This Element requires a Volume Buffer set to Grass to render to and it will affect the Grass shaders only. Read more about Volume Buffers and Layers in the documentation., _ElementLayer, 20, 0, 10)]_ElementLayerGrassMessage("Element Layer Grass Message", Float) = 0
		[StyledMessage(Info, This Element requires a Volume Buffer set to Custom to render to and it will affect the Custom shaders only. Read more about Volume Buffers and Layers in the documentation., _ElementLayer, 100, 0, 10)]_ElementLayerCustomMessage("Element Layer Custom Message", Float) = 0
		[Enum(Any,0,Vegetation,10,Grass,20,Objects,30)]_ElementLayer("Element Layer", Float) = 0
		[Enum(Main,0,Seasons,1)]_ElementMode("Element Mode", Float) = 0
		[StyledCategory(Element Settings)]_ElementCat("[ Element Cat ]", Float) = 0
		[NoScaleOffset][Space(10)]_MainTex("Element Texture", 2D) = "white" {}
		[StyledRemapSlider(_MainTexMinValue, _MainTexMaxValue, 0, 1)]_MainTexRemap("Element Remap", Vector) = (0,0,0,0)
		[HideInInspector]_MainTexMinValue("Element Min", Range( 0 , 1)) = 0
		[HideInInspector]_MainTexMaxValue("Element Max", Range( 0 , 1)) = 1
		_MainUVs("Element UVs", Vector) = (1,1,0,0)
		_MainValue("Main", Range( 0 , 1)) = 1
		_AdditionalValue1("Winter", Range( 0 , 1)) = 1
		_AdditionalValue2("Spring", Range( 0 , 1)) = 1
		_AdditionalValue3("Summer", Range( 0 , 1)) = 1
		_AdditionalValue4("Autumn", Range( 0 , 1)) = 1
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

	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Transparent" "Queue"="Transparent" "PreviewType"="Plane" }
	LOD 0

		CGINCLUDE
		#pragma target 2.0
		ENDCG
		Blend One Zero, DstColor Zero
		AlphaToMask Off
		Cull Off
		ColorMask A
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

			uniform half _IsVertexShader;
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
			uniform half _Banner;
			uniform half _Message;
			uniform half _MainValue;
			uniform half4 TVE_SeasonOptions;
			uniform half _AdditionalValue1;
			uniform half _AdditionalValue2;
			uniform half TVE_SeasonLerp;
			uniform half _AdditionalValue3;
			uniform half _AdditionalValue4;
			uniform float _ElementMode;
			uniform sampler2D _MainTex;
			uniform half4 _MainUVs;
			uniform half _MainTexMinValue;
			uniform half _MainTexMaxValue;
			uniform float _ElementIntensity;
			uniform float TVE_ElementsFadeEnd;
			uniform float TVE_ElementsFadeStart;
			uniform float _ElementFadeSupport;

			
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
				half3 Final_Size_RGB228_g18997 = half3(0,0,0);
				half Value_Main157_g18997 = _MainValue;
				half TVE_SeasonOptions_X50_g18997 = TVE_SeasonOptions.x;
				half Value_Winter158_g18997 = _AdditionalValue1;
				float temp_output_427_0_g18997 = ( 1.0 - Value_Winter158_g18997 );
				half Value_Spring159_g18997 = _AdditionalValue2;
				float temp_output_428_0_g18997 = ( 1.0 - Value_Spring159_g18997 );
				half TVE_SeasonLerp54_g18997 = TVE_SeasonLerp;
				float lerpResult419_g18997 = lerp( temp_output_427_0_g18997 , temp_output_428_0_g18997 , TVE_SeasonLerp54_g18997);
				half TVE_SeasonOptions_Y51_g18997 = TVE_SeasonOptions.y;
				half Value_Summer160_g18997 = _AdditionalValue3;
				float temp_output_429_0_g18997 = ( 1.0 - Value_Summer160_g18997 );
				float lerpResult422_g18997 = lerp( temp_output_428_0_g18997 , temp_output_429_0_g18997 , TVE_SeasonLerp54_g18997);
				half TVE_SeasonOptions_Z52_g18997 = TVE_SeasonOptions.z;
				half Value_Autumn161_g18997 = _AdditionalValue4;
				float temp_output_430_0_g18997 = ( 1.0 - Value_Autumn161_g18997 );
				float lerpResult407_g18997 = lerp( temp_output_429_0_g18997 , temp_output_430_0_g18997 , TVE_SeasonLerp54_g18997);
				half TVE_SeasonOptions_W53_g18997 = TVE_SeasonOptions.w;
				float lerpResult415_g18997 = lerp( temp_output_430_0_g18997 , temp_output_427_0_g18997 , TVE_SeasonLerp54_g18997);
				half Element_Mode55_g18997 = _ElementMode;
				float lerpResult413_g18997 = lerp( ( 1.0 - ( Value_Main157_g18997 * i.ase_color.r ) ) , ( ( ( TVE_SeasonOptions_X50_g18997 * lerpResult419_g18997 ) + ( TVE_SeasonOptions_Y51_g18997 * lerpResult422_g18997 ) + ( TVE_SeasonOptions_Z52_g18997 * lerpResult407_g18997 ) + ( TVE_SeasonOptions_W53_g18997 * lerpResult415_g18997 ) ) * i.ase_color.r ) , Element_Mode55_g18997);
				half Base_Extras_A423_g18997 = ( 1.0 - lerpResult413_g18997 );
				float4 tex2DNode17_g18997 = tex2D( _MainTex, ( ( ( 1.0 - i.ase_texcoord1.xy ) * (_MainUVs).xy ) + (_MainUVs).zw ) );
				float temp_output_7_0_g19036 = _MainTexMinValue;
				float4 temp_cast_0 = (temp_output_7_0_g19036).xxxx;
				float4 break469_g18997 = saturate( ( ( tex2DNode17_g18997 - temp_cast_0 ) / ( _MainTexMaxValue - temp_output_7_0_g19036 ) ) );
				half MainTex_R73_g18997 = break469_g18997.r;
				float lerpResult224_g18997 = lerp( 1.0 , Base_Extras_A423_g18997 , MainTex_R73_g18997);
				float temp_output_7_0_g19034 = TVE_ElementsFadeEnd;
				half Enable_Fade_Support454_g18997 = _ElementFadeSupport;
				float lerpResult654_g18997 = lerp( 1.0 , saturate( ( ( distance( WorldPosition , _WorldSpaceCameraPos ) - temp_output_7_0_g19034 ) / ( TVE_ElementsFadeStart - temp_output_7_0_g19034 ) ) ) , Enable_Fade_Support454_g18997);
				half FadeOut_Mask656_g18997 = lerpResult654_g18997;
				half Element_Intensity56_g18997 = ( _ElementIntensity * i.ase_color.a * FadeOut_Mask656_g18997 );
				float lerpResult799_g18997 = lerp( 1.0 , lerpResult224_g18997 , Element_Intensity56_g18997);
				half Final_Size_A229_g18997 = lerpResult799_g18997;
				float4 appendResult473_g18997 = (float4(Final_Size_RGB228_g18997 , Final_Size_A229_g18997));
				
				
				finalColor = appendResult473_g18997;
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "TVEShaderElementGUI"
	
	
}
/*ASEBEGIN
Version=18806
1920;13;1906;1009;1722.264;1605.589;1;True;False
Node;AmplifyShaderEditor.FunctionNode;126;-384,-1408;Inherit;False;Is Vertex Element;0;;18996;6eebc31017d99e84e811285e6a5d199d;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;128;-768,-1025;Inherit;False;Base Element;4;;18997;0e972c73cae2ee54ea51acc9738801d0;6,477,1,478,0,145,1,481,3,576,1,491,1;0;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;109;-768,-1408;Half;False;Property;_Banner;Banner;2;0;Create;True;0;0;0;True;1;StyledBanner(Size Element);False;0;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;112;-608,-1408;Half;False;Property;_Message;Message;3;0;Create;True;0;0;0;True;1;StyledMessage(Info, Use the Size elements to add scale variation or combine it with seasons to add dynamic vegetation growing. Element Texture R is used as alpha mask. Particle Color R is used as values multiplier and Alpha as Element Intensity multiplier., 0,0);False;0;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;-432,-1024;Float;False;True;-1;2;TVEShaderElementGUI;0;1;BOXOPHOBIC/The Vegetation Engine/Elements/Default/Vertex Size;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;True;True;0;5;False;-1;10;False;-1;6;2;False;-1;0;False;-1;True;0;False;-1;0;False;-1;False;False;False;False;False;False;False;False;False;True;0;False;-1;False;True;2;False;-1;True;True;False;False;False;True;0;False;-1;False;False;False;False;False;False;False;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;False;True;2;False;-1;True;0;False;-1;True;False;0;False;-1;0;False;-1;True;3;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;PreviewType=Plane;True;0;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;1;True;False;;False;0
WireConnection;0;0;128;0
ASEEND*/
//CHKSM=C3C1C0B499A5D4FE6D707B0F7E6F1B63DA29E0D5