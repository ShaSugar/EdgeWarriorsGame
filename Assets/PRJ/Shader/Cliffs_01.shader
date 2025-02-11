// Made with Amplify Shader Editor v1.9.1.5
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/DragonsNotAllowed/BlazingHighlands/Cliffs_01"
{
	Properties
	{
		_Cliff_Top_BaseColor("Cliff_Top_BaseColor", 2D) = "white" {}
		_Cliff_Detail_BaseColor("Cliff_Detail_BaseColor", 2D) = "white" {}
		_MasterBaseColor("Master Base Color", 2D) = "white" {}
		_MasterNormal("Master Normal", 2D) = "bump" {}
		_Cliff_Top_Normal("Cliff_Top_Normal", 2D) = "bump" {}
		_Cliff_Detail_Normal("Cliff_Detail_Normal", 2D) = "bump" {}
		_Top_Normal_Power("Top_Normal_Power", Float) = 1
		_Cliff_Detail_Normal_Power("Cliff_Detail_Normal_Power", Float) = 0.8
		_TilingTop("Tiling Top", Float) = 50
		_TilingCliffs("Tiling Cliffs", Float) = 2
		_MinTilingCliffs("Min Tiling Cliffs", Float) = 1
		_MaxTilingCliffs("Max Tiling Cliffs", Float) = 4
		_Transition_Mask("Transition_Mask", 2D) = "white" {}
		_Specular_Top("Specular_Top", Float) = 0.1
		_Specular_Cliffs("Specular_Cliffs", Float) = 0.2
		_Smoothness_Top("Smoothness_Top", Float) = 0.8
		_Smotthness_Cliffs("Smotthness_Cliffs", Float) = 0.8
		_Transition_Mask_Tiling("Transition_Mask_Tiling", Float) = 75
		_TransitionContrast("Transition Contrast", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#pragma target 3.0
		#pragma surface surf StandardSpecular keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
			float4 vertexColor : COLOR;
		};

		uniform sampler2D _Cliff_Top_Normal;
		uniform float _TilingTop;
		uniform float _Top_Normal_Power;
		uniform sampler2D _Cliff_Detail_Normal;
		uniform float _MinTilingCliffs;
		uniform float _MaxTilingCliffs;
		uniform float _TilingCliffs;
		uniform float _Cliff_Detail_Normal_Power;
		uniform float _TransitionContrast;
		uniform sampler2D _Transition_Mask;
		uniform float _Transition_Mask_Tiling;
		uniform sampler2D _MasterNormal;
		uniform float4 _MasterNormal_ST;
		uniform sampler2D _Cliff_Top_BaseColor;
		uniform sampler2D _MasterBaseColor;
		uniform float4 _MasterBaseColor_ST;
		uniform sampler2D _Cliff_Detail_BaseColor;
		uniform float _Specular_Top;
		uniform float _Specular_Cliffs;
		uniform float _Smoothness_Top;
		uniform float _Smotthness_Cliffs;

		void surf( Input i , inout SurfaceOutputStandardSpecular o )
		{
			float3 ase_objectScale = float3( length( unity_ObjectToWorld[ 0 ].xyz ), length( unity_ObjectToWorld[ 1 ].xyz ), length( unity_ObjectToWorld[ 2 ].xyz ) );
			float temp_output_62_0 = ( ase_objectScale.x * 0.1 );
			float2 temp_output_6_0 = ( ( i.uv_texcoord * _TilingTop ) * temp_output_62_0 );
			float3 tex2DNode20 = UnpackNormal( tex2D( _Cliff_Top_Normal, temp_output_6_0 ) );
			float4 appendResult55 = (float4(( (tex2DNode20).xy * _Top_Normal_Power ) , tex2DNode20.b , 0.0));
			float clampResult11 = clamp( temp_output_62_0 , 0.0 , 1.0 );
			float lerpResult12 = lerp( _MinTilingCliffs , _MaxTilingCliffs , clampResult11);
			float2 temp_output_16_0 = ( lerpResult12 * ( i.uv_texcoord * _TilingCliffs ) );
			float3 tex2DNode21 = UnpackNormal( tex2D( _Cliff_Detail_Normal, temp_output_16_0 ) );
			float4 appendResult58 = (float4(( (tex2DNode21).xy * _Cliff_Detail_Normal_Power ) , tex2DNode21.b , 0.0));
			float lerpResult104 = lerp( tex2D( _Transition_Mask, ( ( i.uv_texcoord * _Transition_Mask_Tiling ) * temp_output_62_0 ) ).r , ( i.vertexColor.r + -0.25 ) , 0.5);
			float lerpResult97 = lerp( ( 0.0 - _TransitionContrast ) , ( 1.0 + _TransitionContrast ) , lerpResult104);
			float clampResult99 = clamp( lerpResult97 , 0.0 , 1.0 );
			float4 lerpResult38 = lerp( appendResult55 , appendResult58 , clampResult99);
			float2 uv_MasterNormal = i.uv_texcoord * _MasterNormal_ST.xy + _MasterNormal_ST.zw;
			o.Normal = BlendNormals( lerpResult38.xyz , UnpackNormal( tex2D( _MasterNormal, uv_MasterNormal ) ) );
			float4 tex2DNode18 = tex2D( _Cliff_Top_BaseColor, temp_output_6_0 );
			float2 uv_MasterBaseColor = i.uv_texcoord * _MasterBaseColor_ST.xy + _MasterBaseColor_ST.zw;
			float4 tex2DNode19 = tex2D( _Cliff_Detail_BaseColor, temp_output_16_0 );
			float4 lerpResult114 = lerp( tex2D( _MasterBaseColor, uv_MasterBaseColor ) , tex2DNode19 , 0.6);
			float4 lerpResult36 = lerp( tex2DNode18 , lerpResult114 , clampResult99);
			o.Albedo = lerpResult36.rgb;
			float lerpResult48 = lerp( _Specular_Top , _Specular_Cliffs , clampResult99);
			float3 temp_cast_2 = (lerpResult48).xxx;
			o.Specular = temp_cast_2;
			float lerpResult42 = lerp( ( _Smoothness_Top * tex2DNode18.r ) , ( _Smotthness_Cliffs * tex2DNode19.r ) , clampResult99);
			o.Smoothness = lerpResult42;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=19105
Node;AmplifyShaderEditor.ClampOpNode;11;-3359.568,447.1237;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;12;-3174.568,296.1237;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;-2752.567,299.1237;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-2912.567,505.1237;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;2;-3228.395,-210.8278;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-2975.567,-145.8763;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-2761.567,-49.87628;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;13;-3170.568,459.1237;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;-2163.383,285.3832;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;24;-2455.654,213.2562;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;26;-1800.637,274.5688;Inherit;True;Property;_Transition_Mask;Transition_Mask;12;0;Create;True;0;0;0;False;0;False;-1;9f49c0db47200e64189f9d00c6679033;9f49c0db47200e64189f9d00c6679033;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;807.7235,33.92811;Float;False;True;-1;2;ASEMaterialInspector;0;0;StandardSpecular;Custom/DragonsNotAllowed/BlazingHighlands/Cliffs_01;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;;0;False;;False;0;False;;0;False;;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;True;0;0;False;;0;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
Node;AmplifyShaderEditor.SamplerNode;19;-2398.788,-121.5757;Inherit;True;Property;_Cliff_Detail_BaseColor;Cliff_Detail_BaseColor;1;0;Create;True;0;0;0;False;0;False;-1;a926b8be7079846418099cb630f39b6e;a926b8be7079846418099cb630f39b6e;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;18;-2399.046,-326.5759;Inherit;True;Property;_Cliff_Top_BaseColor;Cliff_Top_BaseColor;0;0;Create;True;0;0;0;False;0;False;-1;3b29db12cc5636640a216adaa2b383dd;3b29db12cc5636640a216adaa2b383dd;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;59;-1716.524,880.4379;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;58;-1542.524,906.9515;Inherit;False;FLOAT4;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;53;-1729.436,628.3388;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;55;-1549.76,649.9445;Inherit;False;FLOAT4;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;62;-3535.252,288.4039;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ObjectScaleNode;1;-3724.616,139.2219;Inherit;False;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;52;-1968.421,727.8074;Inherit;False;Property;_Top_Normal_Power;Top_Normal_Power;6;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;56;-2000.294,1004.185;Inherit;False;Property;_Cliff_Detail_Normal_Power;Cliff_Detail_Normal_Power;7;0;Create;True;0;0;0;False;0;False;0.8;0.8;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;57;-1974.574,864.8024;Inherit;False;True;True;False;False;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;21;-2393.321,868.5045;Inherit;True;Property;_Cliff_Detail_Normal;Cliff_Detail_Normal;5;0;Create;True;0;0;0;False;0;False;-1;989a6d46e9353be489694e85035e8eec;989a6d46e9353be489694e85035e8eec;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;20;-2395.518,595.0046;Inherit;True;Property;_Cliff_Top_Normal;Cliff_Top_Normal;4;0;Create;True;0;0;0;False;0;False;-1;de43119f1ba92114b9773128b4dcacab;de43119f1ba92114b9773128b4dcacab;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;54;-1956.803,594.9785;Inherit;False;True;True;False;False;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;-1997.077,371.6338;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;48;549.25,103.8443;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;41;-46.9991,32.7581;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;42;118.0008,105.7581;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;45;-46.85466,202.1357;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;43;-204.9909,293.0187;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;39;-203.4635,100.8583;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BlendNormalsNode;51;301.3627,738.6773;Inherit;False;0;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;104;-1309.775,326.2291;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;97;-678.6296,279.0157;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;102;-832.2299,226.2147;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;103;-832.2723,405.4154;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;99;-518.4497,279.0152;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;110;-1519.376,101.8999;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;30;-1709.779,10.48044;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;36;-376.4685,-186.0489;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;38;-357.6969,739.994;Inherit;False;3;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;2;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;98;-985.8284,397.415;Inherit;False;Constant;_Float6;Float 6;13;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;100;-969.8292,202.2158;Inherit;False;Constant;_Float7;Float 7;15;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;112;-1704.587,181.5472;Inherit;False;Constant;_Float10;Float 10;14;0;Create;True;0;0;0;False;0;False;-0.25;1.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;106;-1492.509,371.9126;Inherit;False;Constant;_Float9;Float 9;14;0;Create;True;0;0;0;False;0;False;0.5;1.96;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-3383.568,274.1237;Inherit;False;Property;_MinTilingCliffs;Min Tiling Cliffs;10;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-3383.568,359.1237;Inherit;False;Property;_MaxTilingCliffs;Max Tiling Cliffs;11;0;Create;True;0;0;0;False;0;False;4;4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;63;-3687.183,379.0419;Inherit;False;Constant;_Float1;Float 1;12;0;Create;True;0;0;0;False;0;False;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;4;-3201.395,-63.82782;Inherit;False;Property;_TilingTop;Tiling Top;8;0;Create;True;0;0;0;False;0;False;50;50;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-2452.308,340.2248;Inherit;False;Property;_Transition_Mask_Tiling;Transition_Mask_Tiling;17;0;Create;True;0;0;0;False;0;False;75;75;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;46;341.0939,81.66377;Inherit;False;Property;_Specular_Top;Specular_Top;13;0;Create;True;0;0;0;False;0;False;0.1;0.04;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;47;337.6817,161.855;Inherit;False;Property;_Specular_Cliffs;Specular_Cliffs;14;0;Create;True;0;0;0;False;0;False;0.2;0.04;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;101;-1066.147,296.5827;Inherit;False;Property;_TransitionContrast;Transition Contrast;18;0;Create;True;0;0;0;False;0;False;1;1.89;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;40;-233.2191,10.44117;Inherit;False;Property;_Smoothness_Top;Smoothness_Top;15;0;Create;True;0;0;0;False;0;False;0.8;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;44;-240.642,184.6999;Inherit;False;Property;_Smotthness_Cliffs;Smotthness_Cliffs;16;0;Create;True;0;0;0;False;0;False;0.8;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;50;-36.88802,861.0723;Inherit;True;Property;_MasterNormal;Master Normal;3;0;Create;True;0;0;0;False;0;False;-1;40d7cf1271a72b04f9954e0b4d34219e;40d7cf1271a72b04f9954e0b4d34219e;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;14;-3110.567,584.1237;Inherit;False;Property;_TilingCliffs;Tiling Cliffs;9;0;Create;True;0;0;0;False;0;False;2;3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;114;-594.0833,-129.6484;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;113;-931.8101,-396.3076;Inherit;True;Property;_MasterBaseColor;Master Base Color;2;0;Create;True;0;0;0;False;0;False;-1;6fc20f1118a9c4a4c9aaeee6d9ab0757;6fc20f1118a9c4a4c9aaeee6d9ab0757;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;116;-770.4589,-25.32179;Inherit;False;Constant;_Float0;Float 0;19;0;Create;True;0;0;0;False;0;False;0.6;0.5;0;0;0;1;FLOAT;0
WireConnection;11;0;62;0
WireConnection;12;0;7;0
WireConnection;12;1;8;0
WireConnection;12;2;11;0
WireConnection;16;0;12;0
WireConnection;16;1;15;0
WireConnection;15;0;13;0
WireConnection;15;1;14;0
WireConnection;5;0;2;0
WireConnection;5;1;4;0
WireConnection;6;0;5;0
WireConnection;6;1;62;0
WireConnection;23;0;24;0
WireConnection;23;1;22;0
WireConnection;26;1;25;0
WireConnection;0;0;36;0
WireConnection;0;1;51;0
WireConnection;0;3;48;0
WireConnection;0;4;42;0
WireConnection;19;1;16;0
WireConnection;18;1;6;0
WireConnection;59;0;57;0
WireConnection;59;1;56;0
WireConnection;58;0;59;0
WireConnection;58;2;21;3
WireConnection;53;0;54;0
WireConnection;53;1;52;0
WireConnection;55;0;53;0
WireConnection;55;2;20;3
WireConnection;62;0;1;1
WireConnection;62;1;63;0
WireConnection;57;0;21;0
WireConnection;21;1;16;0
WireConnection;20;1;6;0
WireConnection;54;0;20;0
WireConnection;25;0;23;0
WireConnection;25;1;62;0
WireConnection;48;0;46;0
WireConnection;48;1;47;0
WireConnection;48;2;99;0
WireConnection;41;0;40;0
WireConnection;41;1;18;1
WireConnection;42;0;41;0
WireConnection;42;1;45;0
WireConnection;42;2;99;0
WireConnection;45;0;44;0
WireConnection;45;1;19;1
WireConnection;43;0;19;1
WireConnection;39;0;18;1
WireConnection;51;0;38;0
WireConnection;51;1;50;0
WireConnection;104;0;26;1
WireConnection;104;1;110;0
WireConnection;104;2;106;0
WireConnection;97;0;102;0
WireConnection;97;1;103;0
WireConnection;97;2;104;0
WireConnection;102;0;100;0
WireConnection;102;1;101;0
WireConnection;103;0;98;0
WireConnection;103;1;101;0
WireConnection;99;0;97;0
WireConnection;110;0;30;1
WireConnection;110;1;112;0
WireConnection;36;0;18;0
WireConnection;36;1;114;0
WireConnection;36;2;99;0
WireConnection;38;0;55;0
WireConnection;38;1;58;0
WireConnection;38;2;99;0
WireConnection;114;0;113;0
WireConnection;114;1;19;0
WireConnection;114;2;116;0
ASEEND*/
//CHKSM=BC3036C456BBD0EA6D709860DA7B663A7D0294F1