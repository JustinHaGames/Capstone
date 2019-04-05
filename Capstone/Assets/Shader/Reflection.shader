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
			float clampResult108 = clamp( uv_TexCoord51.y , 0.0 , 1.0 );
			float4 lerpResult107 = lerp( tex2D( _RenderTexture, ( half3( uv_TexCoord51 ,  0.0 ) + ( UnpackNormal( tex2D( _NormalMap, ( _Tiling * panner48 ) ) ) * _Distortion ) ).xy ) , float4( 0,0,0,0 ) , clampResult108);
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
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
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
Version=16200
0;23;1280;698;3007.79;67.55415;1.75335;True;False
Node;AmplifyShaderEditor.Vector2Node;106;-2632.083,85.02396;Float;False;InstancedProperty;_Vector0;Vector 0;8;0;Create;True;0;0;False;0;0,0;1,10.05;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleTimeNode;50;-2247.112,822.0334;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;54;-2272.392,627.6247;Float;False;Property;_Speed;Speed;3;0;Create;True;0;0;False;0;0,0;0.01,0.05;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;51;-2416.631,151.8344;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;55;-2110.043,393.6585;Float;False;Property;_Tiling;Tiling;5;0;Create;True;0;0;False;0;0,0;1,0.2;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.PannerNode;48;-2153.074,564.5556;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;56;-1901.869,511.3479;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;46;-1313.098,488.187;Float;False;Property;_Distortion;Distortion;4;0;Create;True;0;0;False;0;0.05;0.01;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;44;-1649.216,586.5809;Float;True;Property;_NormalMap;Normal Map;2;0;Create;True;0;0;False;0;None;9a4a55d8d2e54394d97426434477cdcf;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;47;-1330.317,313.8593;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;52;-1852.771,109.3243;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;38;-1377.973,-141.0117;Float;True;Property;_RenderTexture;Render Texture;1;0;Create;True;0;0;False;0;None;274a76e45cb6e434283d8d8fe0a68330;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;108;-1084.761,231.5152;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;103;-823.3153,286.0584;Float;False;Property;_TopC;TopC;6;0;Create;True;0;0;False;0;0,0,0,0;0,0.05438296,0.6029412,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;107;-942.6807,133.112;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;104;-890.1205,-179.0052;Float;False;Property;_BottomC;BottomC;7;0;Create;True;0;0;False;0;0,0,0,0;0.6102941,0.7097362,1,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;102;-589.3707,214.0172;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;100;-609.4692,-33.1004;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;105;-406.7963,69.13749;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-387.1723,371.3289;Float;False;Property;_Transparency;Transparency;0;0;Create;True;0;0;False;0;0.1;0.875;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;3;0,-1.041101;Half;False;True;2;Half;ASEMaterialInspector;0;0;Unlit;Reflection;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;51;0;106;0
WireConnection;48;0;51;0
WireConnection;48;2;54;0
WireConnection;48;1;50;0
WireConnection;56;0;55;0
WireConnection;56;1;48;0
WireConnection;44;1;56;0
WireConnection;47;0;44;0
WireConnection;47;1;46;0
WireConnection;52;0;51;0
WireConnection;52;1;47;0
WireConnection;38;1;52;0
WireConnection;108;0;51;2
WireConnection;107;0;38;0
WireConnection;107;2;108;0
WireConnection;102;0;107;0
WireConnection;102;1;103;0
WireConnection;100;0;104;0
WireConnection;100;1;107;0
WireConnection;105;0;100;0
WireConnection;105;1;102;0
WireConnection;105;2;51;2
WireConnection;3;2;105;0
WireConnection;3;9;11;0
ASEEND*/
//CHKSM=203B512F1BD5CBE835BBD51160C1A6C93392FC1B