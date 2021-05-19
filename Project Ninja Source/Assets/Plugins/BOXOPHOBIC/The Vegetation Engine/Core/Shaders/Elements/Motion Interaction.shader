// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "BOXOPHOBIC/The Vegetation Engine/Elements/Default/Motion Interaction"
{
	Properties
	{
		[StyledBanner(Motion Interaction Element)]_Banner("Banner", Float) = 0
		[StyledMessage(Info, Use the Motion Interaction elements to add touch bending to the vegetation objects. Element Texture XY is used a World XZ bending and Texture A is used as interaction mask. Particle Color A is used as Element Intensity multiplier., 0,0)]_Message("Message", Float) = 0
		[StyledCategory(Render Settings)]_RenderCat("[ Render Cat ]", Float) = 0
		_ElementIntensity("Element Intensity", Range( 0 , 1)) = 1
		[StyledMessage(Info, This Element requires a Volume Buffer set to Vegetation to render to and it will affect the Vegetation shaders only. Read more about Volume Buffers and Layers in the documentation., _ElementLayer, 10, 0, 10)]_ElementLayerVegetationMessage("Element Layer Vegetation Message", Float) = 0
		[StyledMessage(Info, This Element requires a Volume Buffer set to Objects to render to and it will affect the Objects shaders only. Read more about Volume Buffers and Layers in the documentation., _ElementLayer, 30, 0, 10)]_ElementLayerObjectsMessage("Element Layer Objects Message", Float) = 0
		[StyledMessage(Info, This Element requires a Volume Buffer set to Grass to render to and it will affect the Grass shaders only. Read more about Volume Buffers and Layers in the documentation., _ElementLayer, 20, 0, 10)]_ElementLayerGrassMessage("Element Layer Grass Message", Float) = 0
		[StyledMessage(Info, This Element requires a Volume Buffer set to Custom to render to and it will affect the Custom shaders only. Read more about Volume Buffers and Layers in the documentation., _ElementLayer, 100, 0, 10)]_ElementLayerCustomMessage("Element Layer Custom Message", Float) = 0
		[Enum(Any,0,Vegetation,10,Grass,20,Objects,30)]_ElementLayer("Element Layer", Float) = 0
		[StyledCategory(Element Settings)]_ElementCat("[ Element Cat ]", Float) = 0
		[NoScaleOffset][Space(10)]_MainTex("Element Texture", 2D) = "white" {}
		[StyledRemapSlider(_MainTexMinValue, _MainTexMaxValue, 0, 1)]_MainTexRemap("Element Remap", Vector) = (0,0,0,0)
		[StyledToggle]_InvertX("Invert Texture X", Float) = 0
		[StyledToggle]_InvertY("Invert Texture Y", Float) = 0
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
		[HideInInspector]_IsVertexShader("_IsVertexShader", Float) = 1

	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Transparent" "Queue"="Transparent" "PreviewType"="Plane" }
	LOD 0

		CGINCLUDE
		#pragma target 2.0
		ENDCG
		Blend SrcAlpha OneMinusSrcAlpha, SrcAlpha OneMinusSrcAlpha
		AlphaToMask Off
		Cull Off
		ColorMask RGB
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
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			uniform half _IsVertexShader;
			uniform half _Message;
			uniform half _Banner;
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
			uniform sampler2D _MainTex;
			uniform float _InvertX;
			uniform float _InvertY;
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

				o.ase_texcoord1.xy = v.ase_texcoord.xy;
				o.ase_color = v.color;
				
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
				float4 tex2DNode17_g19051 = tex2D( _MainTex, ( 1.0 - i.ase_texcoord1.xy ) );
				float4 break469_g19051 = tex2DNode17_g19051;
				half MainTex_R73_g19051 = break469_g19051.r;
				half MainTex_G265_g19051 = break469_g19051.g;
				float3 appendResult274_g19051 = (float3((MainTex_R73_g19051*2.0 + -1.0) , 0.0 , (MainTex_G265_g19051*2.0 + -1.0)));
				float3 temp_output_275_0_g19051 = mul( unity_ObjectToWorld, float4( appendResult274_g19051 , 0.0 ) ).xyz;
				float3 ase_objectScale = float3( length( unity_ObjectToWorld[ 0 ].xyz ), length( unity_ObjectToWorld[ 1 ].xyz ), length( unity_ObjectToWorld[ 2 ].xyz ) );
				float temp_output_7_0_g19054 = -ase_objectScale.x;
				half normalX_WS284_g19051 = ( ( temp_output_275_0_g19051.x - temp_output_7_0_g19054 ) / ( ase_objectScale.x - temp_output_7_0_g19054 ) );
				half Invert_Texture_X489_g19051 = _InvertX;
				float lerpResult353_g19051 = lerp( normalX_WS284_g19051 , ( 1.0 - normalX_WS284_g19051 ) , Invert_Texture_X489_g19051);
				float temp_output_7_0_g19059 = -ase_objectScale.z;
				half normalZ_WS285_g19051 = ( ( temp_output_275_0_g19051.z - temp_output_7_0_g19059 ) / ( ase_objectScale.z - temp_output_7_0_g19059 ) );
				half Invert_Texture_Y545_g19051 = _InvertY;
				float lerpResult354_g19051 = lerp( normalZ_WS285_g19051 , ( 1.0 - normalZ_WS285_g19051 ) , Invert_Texture_Y545_g19051);
				half MainTex_A74_g19051 = break469_g19051.a;
				float3 appendResult292_g19051 = (float3(lerpResult353_g19051 , lerpResult354_g19051 , ( 1.0 - MainTex_A74_g19051 )));
				half3 Final_MotionInteraction_RGB303_g19051 = appendResult292_g19051;
				float temp_output_7_0_g19055 = TVE_ElementsFadeEnd;
				half Enable_Fade_Support454_g19051 = _ElementFadeSupport;
				float lerpResult654_g19051 = lerp( 1.0 , saturate( ( ( distance( WorldPosition , _WorldSpaceCameraPos ) - temp_output_7_0_g19055 ) / ( TVE_ElementsFadeStart - temp_output_7_0_g19055 ) ) ) , Enable_Fade_Support454_g19051);
				half FadeOut_Mask656_g19051 = lerpResult654_g19051;
				half Element_Intensity56_g19051 = ( _ElementIntensity * i.ase_color.a * FadeOut_Mask656_g19051 );
				half Final_MotionInteraction_A305_g19051 = ( MainTex_A74_g19051 * Element_Intensity56_g19051 );
				float4 appendResult479_g19051 = (float4(Final_MotionInteraction_RGB303_g19051 , Final_MotionInteraction_A305_g19051));
				
				
				finalColor = appendResult479_g19051;
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "TVEShaderElementGUI"
	
	
}
/*ASEBEGIN
Version=18806
1920;13;1906;1009;1280.803;1616.291;1;True;False
Node;AmplifyShaderEditor.FunctionNode;117;-304,-1280;Inherit;False;Is Vertex Element;57;;18553;6eebc31017d99e84e811285e6a5d199d;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;115;-480,-1280;Half;False;Property;_Message;Message;1;0;Create;True;0;0;0;True;1;StyledMessage(Info, Use the Motion Interaction elements to add touch bending to the vegetation objects. Element Texture XY is used a World XZ bending and Texture A is used as interaction mask. Particle Color A is used as Element Intensity multiplier., 0,0);False;0;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;95;-640,-1280;Half;False;Property;_Banner;Banner;0;0;Create;True;0;0;0;True;1;StyledBanner(Motion Interaction Element);False;0;0;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;163;-640,-1024;Inherit;False;Base Element;2;;19051;0e972c73cae2ee54ea51acc9738801d0;6,477,2,478,0,145,3,481,0,576,0,491,0;0;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;-320,-1024;Float;False;True;-1;2;TVEShaderElementGUI;0;1;BOXOPHOBIC/The Vegetation Engine/Elements/Default/Motion Interaction;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;False;True;2;5;False;-1;10;False;-1;2;5;False;-1;10;False;-1;True;0;False;-1;0;False;-1;False;False;False;False;False;False;False;False;False;True;0;False;-1;False;True;2;False;-1;True;True;True;True;True;False;0;False;-1;False;False;False;False;False;False;False;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;False;True;2;False;-1;True;0;False;-1;True;False;0;False;-1;0;False;-1;True;3;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;PreviewType=Plane;True;0;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;1;True;False;;False;0
WireConnection;0;0;163;0
ASEEND*/
//CHKSM=D15BBA7F43628F78D535D941003D445CF121A4D4