// Upgrade NOTE: upgraded instancing buffer 'Reflection' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Reflection"
{
	Properties
	{
		_Transparency("Transparency", Float) = 0.1
		_RenderTexture("Render Texture", 2D) = "white" {}
		_NormalMap("Normal Map", 2D) = "bump" {}
		_Speed("Speed", Vector) = (0,0,0,0)
		_Distortion("Distortion", Float) = 0.05
		_Tiling("Tiling", Vector) = (0,0,0,0)
		_TopC("TopC", Color) = (0,0,0,0)
		_BottomC("BottomC", Color) = (0,0,0,0)
		_Vector0("Vector 0", Vector) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#pragma multi_compile_instancing
		struct Input
		{
			half2 uv_texcoord;
		};

		uniform half4 _BottomC;
		uniform sampler2D _RenderTexture;
		uniform sampler2D _NormalMap;
		uniform half2 _Tiling;
		uniform half2 _Speed;
		uniform half _Distortion;
		uniform half4 _TopC;
		uniform half _Transparency;

		UNITY_INSTANCING_BUFFER_START(Reflection)
			UNITY_DEFINE_INSTANCED_PROP(half2, _Vector0)
#define _Vector0_arr Reflection
		UNITY_INSTANCING_BUFFER_END(Reflection)

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 _Vector0_Instance = UNITY_ACCESS_INSTANCED_PROP(_Vector0_arr, _Vector0);
			float2 uv_TexCoord51 = i.uv_texcoord * _Vector0_Instance;
			float2 panner48 = ( _Time.y * _Speed + uv_TexCoord51);
			half3 tex2DNode44 = UnpackNormal( tex2D( _NormalMap, ( _Tiling * panner48 ) ) );
			float4 appendResult93 = (half4(( half3( uv_TexCoord51 ,  0.0 ) + ( tex2DNode44 * _Distortion ) ) , 0.0));
			float clampResult108 = clamp( uv_TexCoord51.y , 0.0 , 1.0 );
			float4 lerpResult107 = lerp( tex2D( _RenderTexture, appendResult93.xy ) , float4( 0,0,0,0 ) , clampResult108);
			float4 lerpResult105 = lerp( ( _BottomC * lerpResult107 ) , ( lerpResult107 * _TopC ) , uv_TexCoord51.y);
			o.Emission = lerpResult105.rgb;
			o.Alpha = _Transparency;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Unlit alpha:fade keepalpha fullforwardshadows exclude_path:deferred 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				SurfaceOutput o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutput, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15800
0;45;1280;698;1323.102;299.446;1.232528;True;False
Node;AmplifyShaderEditor.Vector2Node;106;-2632.083,85.02396;Float;False;InstancedProperty;_Vector0;Vector 0;11;0;Create;True;0;0;False;0;0,0;1,10.05;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;51;-2369.533,139.9721;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;50;-2247.112,822.0334;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;54;-2272.392,627.6247;Float;False;Property;_Speed;Speed;4;0;Create;True;0;0;False;0;0,0;0.01,0.05;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.PannerNode;48;-2025.08,571.569;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.WireNode;62;-1856.117,567.5988;Float;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;55;-1985.556,404.1786;Float;False;Property;_Tiling;Tiling;6;0;Create;True;0;0;False;0;0,0;1,0.2;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;56;-1784.395,411.4068;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;44;-1649.703,313.3379;Float;True;Property;_NormalMap;Normal Map;3;0;Create;True;0;0;False;0;None;9a4a55d8d2e54394d97426434477cdcf;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;46;-1201.127,445.7004;Float;False;Property;_Distortion;Distortion;5;0;Create;True;0;0;False;0;0.05;0.02;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;47;-1311.317,259.8593;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;52;-1852.771,109.3243;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DynamicAppendNode;93;-1512.697,-104.7323;Float;True;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ClampOpNode;108;-1084.761,231.5152;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;38;-1280.97,24.15537;Float;True;Property;_RenderTexture;Render Texture;1;0;Create;True;0;0;False;0;None;274a76e45cb6e434283d8d8fe0a68330;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;104;-890.1205,-179.0052;Float;False;Property;_BottomC;BottomC;10;0;Create;True;0;0;False;0;0,0,0,0;0.6102941,0.7097362,1,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;107;-942.6807,133.112;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;103;-823.3153,286.0584;Float;False;Property;_TopC;TopC;9;0;Create;True;0;0;False;0;0,0,0,0;0,0.05438296,0.6029412,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;100;-609.4692,-33.1004;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;102;-589.3707,214.0172;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.PiNode;66;-1018.831,664.7988;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;65;-640.2053,465.1893;Float;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleAddOpNode;99;-231.3496,-19.92654;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;64;-964.3042,497.7153;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ColorNode;39;-1194.618,-550.1979;Float;False;Constant;_Color0;Color 0;4;0;Create;True;0;0;False;0;0.8014706,0.8014706,0.8014706,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SinOpNode;72;-225.1431,471.5257;Float;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;59;-874.8249,-559.3806;Float;False;Property;_Blue;Blue;7;0;Create;True;0;0;False;0;0;0.19;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-387.1723,371.3289;Float;False;Property;_Transparency;Transparency;0;0;Create;True;0;0;False;0;0.1;0.75;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;67;-809.7155,542.8156;Float;False;3;3;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;74;-1380.996,579.4542;Float;False;Property;_LightDarkSlider;LightDarkSlider;8;0;Create;True;0;0;False;0;0;-0.4156798;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;60;-585.5462,-573.5358;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;68;-979.2261,742.4246;Float;False;Constant;_Float0;Float 0;8;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;73;-990.8613,409.0039;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;-0.45;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;70;-588.4548,679.5848;Float;False;Constant;_Float1;Float 1;8;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;-579.3521,-276.3258;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;71;-418.9434,479.9756;Float;False;3;3;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.LerpOp;105;-406.7963,69.13749;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;57;-951.282,-748.8358;Float;False;Constant;_Color1;Color 1;8;0;Create;True;0;0;False;0;0.4963235,0.7461969,0.9926471,0;0,0,0,0;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;61;-320.3538,-295.5239;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;43;-868.7769,-259.686;Float;False;Property;_Silver;Silver;2;0;Create;True;0;0;False;0;3;0.01;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PiNode;69;-628.0604,601.9592;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;3;0,-1.041101;Half;False;True;2;Half;ASEMaterialInspector;0;0;Unlit;Reflection;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;51;0;106;0
WireConnection;48;0;51;0
WireConnection;48;2;54;0
WireConnection;48;1;50;0
WireConnection;62;0;48;0
WireConnection;56;0;55;0
WireConnection;56;1;62;0
WireConnection;44;1;56;0
WireConnection;47;0;44;0
WireConnection;47;1;46;0
WireConnection;52;0;51;0
WireConnection;52;1;47;0
WireConnection;93;0;52;0
WireConnection;108;0;51;2
WireConnection;38;1;93;0
WireConnection;107;0;38;0
WireConnection;107;2;108;0
WireConnection;100;0;104;0
WireConnection;100;1;107;0
WireConnection;102;0;107;0
WireConnection;102;1;103;0
WireConnection;65;0;67;0
WireConnection;99;0;61;0
WireConnection;64;0;73;0
WireConnection;64;1;73;0
WireConnection;64;2;73;0
WireConnection;72;0;71;0
WireConnection;67;0;64;0
WireConnection;67;1;66;0
WireConnection;67;2;68;0
WireConnection;60;0;57;0
WireConnection;60;1;59;0
WireConnection;73;0;44;2
WireConnection;73;1;74;0
WireConnection;42;0;39;0
WireConnection;42;1;43;0
WireConnection;71;0;65;0
WireConnection;71;1;69;0
WireConnection;71;2;70;0
WireConnection;105;0;100;0
WireConnection;105;1;102;0
WireConnection;105;2;51;2
WireConnection;61;0;60;0
WireConnection;61;1;42;0
WireConnection;3;2;105;0
WireConnection;3;9;11;0
ASEEND*/
//CHKSM=DCFC82923A165EA31085AB88CB1E05F280CCF7E7