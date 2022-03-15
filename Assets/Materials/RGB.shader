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
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma multi_compile_instancing
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
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
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
1920;0;1920;1019;1644.61;585.608;1.388368;True;True
Node;AmplifyShaderEditor.ColorNode;1;-747.3867,-265.9008;Inherit;False;InstancedProperty;_ColorR;ColorR;0;1;[HDR];Create;True;0;0;0;False;0;False;22.62742,0,0,1;30.20939,0,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;5;-752.1578,-83.40425;Inherit;False;InstancedProperty;_ColorG;ColorG;1;1;[HDR];Create;True;0;0;0;False;0;False;0,16.94838,0,1;0,16.94838,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;12;-867.8575,147.9966;Inherit;False;InstancedProperty;_Value1;Value1;3;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;7;-446.8037,-45.23499;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-494.515,398.4823;Inherit;False;InstancedProperty;_Value2;Value2;4;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;6;-442.0328,200.4794;Inherit;False;InstancedProperty;_ColorB;ColorB;2;1;[HDR];Create;True;0;0;0;False;0;False;0,0,30.20938,1;0,0,22.62742,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;16;-184.3901,69.27264;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;18;-410.1857,-421.7804;Inherit;False;InstancedProperty;_WithoutRGB;WithoutRGB;5;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;20;-78.53088,544.5235;Inherit;False;InstancedProperty;_Rgb;Rgb;6;0;Create;True;0;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;19;131.1126,-17.76553;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;418.7172,-71.33556;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;RGB;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
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
ASEEND*/
//CHKSM=2EC01B0B035087AC99F89D892A1079323A21E2EA