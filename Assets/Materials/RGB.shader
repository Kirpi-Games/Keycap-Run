// Upgrade NOTE: upgraded instancing buffer 'RGB' to new syntax.

// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "RGB"
{
	Properties
	{
		[HDR]_ColorR("ColorR", Color) = (22.62742,0,0,1)
		[HDR]_ColorG("ColorG", Color) = (0,16.94838,0,1)
		[HDR]_ColorB("ColorB", Color) = (0,0,30.20938,1)
		_Value1("Value1", Range( 0 , 1)) = 0
		_Value2("Value2", Range( 0 , 1)) = 0
		_WithoutRGB("WithoutRGB", Color) = (0,0,0,0)
		_Rgb("Rgb", Range( 0 , 1)) = 0
		_Alpha("Alpha", Color) = (0,0,0,1)
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Custom"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull Back
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGINCLUDE
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#pragma multi_compile_instancing
		struct Input
		{
			half filler;
		};

		UNITY_INSTANCING_BUFFER_START(RGB)
			UNITY_DEFINE_INSTANCED_PROP(float4, _WithoutRGB)
#define _WithoutRGB_arr RGB
			UNITY_DEFINE_INSTANCED_PROP(float4, _ColorR)
#define _ColorR_arr RGB
			UNITY_DEFINE_INSTANCED_PROP(float4, _ColorG)
#define _ColorG_arr RGB
			UNITY_DEFINE_INSTANCED_PROP(float4, _ColorB)
#define _ColorB_arr RGB
			UNITY_DEFINE_INSTANCED_PROP(float4, _Alpha)
#define _Alpha_arr RGB
			UNITY_DEFINE_INSTANCED_PROP(float, _Value1)
#define _Value1_arr RGB
			UNITY_DEFINE_INSTANCED_PROP(float, _Value2)
#define _Value2_arr RGB
			UNITY_DEFINE_INSTANCED_PROP(float, _Rgb)
#define _Rgb_arr RGB
		UNITY_INSTANCING_BUFFER_END(RGB)

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 _WithoutRGB_Instance = UNITY_ACCESS_INSTANCED_PROP(_WithoutRGB_arr, _WithoutRGB);
			float4 _ColorR_Instance = UNITY_ACCESS_INSTANCED_PROP(_ColorR_arr, _ColorR);
			float4 _ColorG_Instance = UNITY_ACCESS_INSTANCED_PROP(_ColorG_arr, _ColorG);
			float _Value1_Instance = UNITY_ACCESS_INSTANCED_PROP(_Value1_arr, _Value1);
			float4 lerpResult7 = lerp( _ColorR_Instance , _ColorG_Instance , _Value1_Instance);
			float4 _ColorB_Instance = UNITY_ACCESS_INSTANCED_PROP(_ColorB_arr, _ColorB);
			float _Value2_Instance = UNITY_ACCESS_INSTANCED_PROP(_Value2_arr, _Value2);
			float4 lerpResult16 = lerp( lerpResult7 , _ColorB_Instance , _Value2_Instance);
			float _Rgb_Instance = UNITY_ACCESS_INSTANCED_PROP(_Rgb_arr, _Rgb);
			float4 lerpResult19 = lerp( _WithoutRGB_Instance , lerpResult16 , _Rgb_Instance);
			o.Emission = lerpResult19.rgb;
			float4 _Alpha_Instance = UNITY_ACCESS_INSTANCED_PROP(_Alpha_arr, _Alpha);
			o.Alpha = _Alpha_Instance.a;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows 

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
				float3 worldPos : TEXCOORD1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
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
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
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
Version=18900
7;6;1906;1013;1634.891;577.2778;1.388368;True;True
Node;AmplifyShaderEditor.ColorNode;1;-747.3867,-265.9008;Inherit;False;InstancedProperty;_ColorR;ColorR;1;1;[HDR];Create;True;0;0;0;False;0;False;22.62742,0,0,1;30.20939,0,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;5;-752.1578,-83.40425;Inherit;False;InstancedProperty;_ColorG;ColorG;2;1;[HDR];Create;True;0;0;0;False;0;False;0,16.94838,0,1;0,16.94838,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;12;-867.8575,147.9966;Inherit;False;InstancedProperty;_Value1;Value1;4;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;7;-446.8037,-45.23499;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-494.515,398.4823;Inherit;False;InstancedProperty;_Value2;Value2;5;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;6;-442.0328,200.4794;Inherit;False;InstancedProperty;_ColorB;ColorB;3;1;[HDR];Create;True;0;0;0;False;0;False;0,0,30.20938,1;0,0,22.62742,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;16;-184.3901,69.27264;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;18;-410.1857,-421.7804;Inherit;False;InstancedProperty;_WithoutRGB;WithoutRGB;6;0;Create;True;0;0;0;False;0;False;0,0,0,0;0.7075471,0.7075471,0.7075471,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;20;-78.53088,544.5235;Inherit;False;InstancedProperty;_Rgb;Rgb;7;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;19;131.1126,-17.76553;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;23;221.3567,252.9662;Inherit;False;InstancedProperty;_Alpha;Alpha;8;0;Create;True;0;0;0;False;0;False;0,0,0,1;0,0,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;460.3682,-56.06353;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;RGB;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;Custom;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;7;0;1;0
WireConnection;7;1;5;0
WireConnection;7;2;12;0
WireConnection;16;0;7;0
WireConnection;16;1;6;0
WireConnection;16;2;17;0
WireConnection;19;0;18;0
WireConnection;19;1;16;0
WireConnection;19;2;20;0
WireConnection;0;2;19;0
WireConnection;0;9;23;4
ASEEND*/
//CHKSM=57D562C5BAEA5121CC05CA3E4F03F4A93AD6F324