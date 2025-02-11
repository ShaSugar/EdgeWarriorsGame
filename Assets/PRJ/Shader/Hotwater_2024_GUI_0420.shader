// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Hotwater/2024/All_GUI_0420"
{
	Properties
	{
		[Main(g17, _, off, off)]_Color_Group3("基础设置", Float) = 0
		[SubEnum(g17,UnityEngine.Rendering.BlendMode)][SubEnum(g17,add,1,blend,10)]_Float1("材质模式", Float) = 10
		[SubEnum(g17,UnityEngine.Rendering.CullMode)]_Float2("双面模式", Float) = 0
		[SubToggle(g17,_)]_Float4("深度写入", Float) = 0
		[SubEnum(g17,UnityEngine.Rendering.CompareFunction)]_Ztestmode("深度测试", Float) = 4
		[SubEnum(g17,UnityEngine.Rendering.ColorWriteMask)]_Float60("colormask", Float) = 15
		[SubEnum(g17,UnityEngine.Rendering.CompareFunction)]_Ztestmode1("stencil_comp", Float) = 0
		[SubEnum(g17,UnityEngine.Rendering.StencilOp)]_Ztestmode2("stencil_pass", Float) = 0
		[Sub(g17)]_Float46("stencil_reference", Float) = 0
		[Toggle][SubToggle(g17,_)]_Float131("启用2u(需关闭customdata)", Float) = 0
		[Main(g10, _, off, off)]_Color_Group2("颜色", Float) = 0
		[HDR][Sub(g10)]_Color0("颜色", Color) = (1,1,1,1)
		[Sub(g10)]_Float14("整体颜色强度", Float) = 1
		[SubToggle(g10, _)]_Float35("双面颜色（默认关闭，勾上开启）", Float) = 0
		[HDR][Sub(g10)]_Color3("颜色2", Color) = (1,1,1,1)
		[Sub(g10)]_Float15("alpha强度", Float) = 1
		[SubToggle(g10, _)]_Float70("限制alpha值为0-1", Float) = 0
		[Main(g9, _, off, off)]_Depthfade_Group1("Depthfade", Float) = 0
		[Sub(g9)]_Float16("软粒子（羽化边缘）", Float) = 0
		[SubToggle(g9, _)]_Float5("反向软粒子(强化边缘）", Float) = 0
		[Sub(g9)]_Float28("边缘强度", Float) = 1
		[Sub(g9)]_Float30("边缘收窄", Float) = 1
		[Sub(g9)]_Float55("相机软粒子（贴脸羽化）距离", Float) = 0
		[Sub(g9)]_Float0("相机软粒子（贴脸羽化）位置", Float) = 0
		[Main(g8, _, off, off)]_Fresnel_Group2("菲尼尔", Float) = 0
		[Enum(off,0,on,1)][KWEnum(g8,off,_0,on,_1)]_Float33("单独菲尼尔开关", Float) = 0
		[SubToggle(g8, _)]_Float145("开启外边缘", Float) = 0
		[Sub(g8)]_power3("菲尼尔范围", Float) = 1
		[HDR][Sub(g8)]_Color6("外边缘颜色", Color) = (1,1,1,1)
		[Sub(g8)]_Float19("菲尼尔软硬", Float) = 1
		[SubToggle(g8, _)]_Float20("反向菲尼尔（虚化边缘）", Float) = 0
		[Main(g1, _, off, off)]_Maintex_Group("主贴图", Float) = 0
		[SubToggle(g1, _)]_Float144("使用屏幕uv", Float) = 0
		[Sub(g1)]_maintex("主贴图", 2D) = "white" {}
		[Enum(A,0,R,1)][KWEnum(g1,A,_0,R,_1)]_maintex_alpha("主贴图通道", Float) = 0
		[SubToggle(g1, _)]_Float62("主贴图x轴clamp", Float) = 0
		[SubToggle(g1, _)]_Float71("主贴图y轴clamp", Float) = 0
		[SubToggle(g1, _)]_Float49("主贴图极坐标（竖向贴图）", Float) = 0
		[Sub(g1)]_Float39("主贴图旋转", Range( -1 , 1)) = 0
		[HideInInspector]_maintex_ST("主贴图tilling&offset", Vector) = (1,1,0,0)
		[Sub(g1)]_Float34("主贴图细节对比度", Float) = 1
		[Sub(g1)]_Float37("主贴图细节提亮", Float) = 1
		[Sub(g1)]_Float36("细节平滑过渡", Range( 0 , 1)) = 1
		[Sub(g1)]_Vector16("色散", Vector) = (0,0,0,0)
		[Sub(g1)]_Vector0("主贴图流动&斜切", Vector) = (0,0,0,0)
		[Main(g11, _, off, off)]_Gradtex("颜色混合", Float) = 0
		[SubToggle(g11, _)]_Float159("颜色混合图极坐标（竖向贴图）", Float) = 0
		[Ramp(g11)]_Gradienttex("混合颜色贴图", 2D) = "white" {}
		[SubToggle(g11, _)]_Float63("颜色混合贴图x轴Clamp", Float) = 0
		[SubToggle(g11, _)]_Float81("颜色混合贴图y轴Clamp", Float) = 0
		[Sub(g11)]_Float40("颜色混合贴图旋转", Range( -1 , 1)) = 0
		[Sub(g11)]_Vector9("颜色混合图tilling&offset", Vector) = (1,1,0,0)
		[Sub(g11)]_Vector7("颜色图流动速度", Vector) = (0,0,0,0)
		[SubToggle(g11, _)]_Float61("切换混合模式（默认lerp，勾上multiply）", Float) = 0
		[Sub(g11)]_Float29("颜色混合（lerp模式）", Range( 0 , 1)) = 0
		[Main(g2, _, off, off)]_Mask_Group("遮罩", Float) = 0
		[Sub(g2)]_Mask("遮罩01", 2D) = "white" {}
		[Enum(A,0,R,1)][KWEnum(g2,A,_0,R,_1)]_mask01_alpha("遮罩01通道", Float) = 0
		[SubToggle(g2, _)]_Float64("遮罩01x轴Clamp", Float) = 0
		[SubToggle(g2, _)]_Float82("遮罩01y轴Clamp", Float) = 0
		[SubToggle(g2, _)]_Float51("遮罩01极坐标（竖向贴图）", Float) = 0
		[Sub(g2)]_Float43("遮罩01旋转", Range( -1 , 1)) = 0
		[HideInInspector]_Mask_ST("_Mask_ST", Vector) = (1,1,0,0)
		[Sub(g2)]_Vector11("遮罩01流动速度&斜切", Vector) = (0,0,0,0)
		[Sub(g2)]_Mask1("遮罩02", 2D) = "white" {}
		[Enum(A,0,R,1)][KWEnum(g2,A,_0,R,_1)]_mask02_alpha("遮罩02通道", Float) = 0
		[SubToggle(g2, _)]_Float65("遮罩02x轴Clamp", Float) = 0
		[SubToggle(g2, _)]_Float83("遮罩02y轴Clamp", Float) = 0
		[SubToggle(g2, _)]_Float52("遮罩02极坐标（竖向贴图）", Float) = 0
		[Sub(g2)]_Float42("遮罩02旋转", Range( -1 , 1)) = 0
		[HideInInspector]_Mask1_ST("_Mask1_ST", Vector) = (1,1,0,0)
		[Sub(g2)]_Vector13("遮罩02流动&斜切", Vector) = (0,0,0,0)
		[Main(g3, _, off, off)]_Disolove_Group("溶解", Float) = 0
		[Sub(g3)]_dissolvetex("溶解贴图", 2D) = "white" {}
		[SubToggle(g3, _)]_Float66("溶解贴图x轴Clamp", Float) = 0
		[SubToggle(g3, _)]_Float87("溶解贴图y轴Clamp", Float) = 0
		[SubToggle(g3, _)]_Float53("溶解极坐标（竖向贴图）", Float) = 0
		[Sub(g3)]_Float41("溶解贴图旋转", Range( -1 , 1)) = 0
		[HideInInspector]_dissolvetex_ST("_dissolvetex_ST", Vector) = (1,1,0,0)
		[Sub(g3)]_Vector15("溶解流动&斜切", Vector) = (0,0,0,0)
		[Ramp(g3)]_TextureSample1("溶解方向贴图（渐变）", 2D) = "white" {}
		[Sub(g3)]_Float47("溶解方向旋转", Range( -1 , 1)) = 0
		[Sub(g3)]_Float130("混合溶解强度", Range( 0 , 1)) = 0
		[Sub(g3)]_Float6("溶解", Float) = 0
		[Sub(g3)]_Float8("软硬", Range( 0 , 0.5)) = 0.5
		[SubToggle(g3, _)]_Float25("亮边溶解（默认关闭，勾上开启）", Float) = 0
		[Sub(g3)]_Float17("一层亮边宽度", Float) = 0
		[Sub(g3)]_Float160("二层亮边宽度", Float) = 0
		[HDR][Sub(g3)]_Color1("一层亮边颜色", Color) = (1,1,1,1)
		[HDR][Sub(g3)]_Color7("二层亮边颜色（软边颜色）", Color) = (1,1,1,1)
		[SubToggle(g3, _)]_Float139("开洞（开启后方向失效）", Float) = 0
		[Enum(local,0,world,1)][KWEnum(g3,local,_0,world,_1)]_Float155("local/world", Float) = 0
		[Sub(g3)]_Vector35("开洞坐标", Vector) = (0,0,0,0)
		[Sub(g3)]_Float72("alphaclip溶解（层级2000以下使用）", Float) = 0
		[Main(g4, _, off, off)]_Noise_Group("扰动", Float) = 0
		[Sub(g4)]_noise("扰动贴图", 2D) = "white" {}
		[SubToggle(g4, _)]_Float67("扰动贴图x轴Clamp", Float) = 0
		[SubToggle(g4, _)]_Float85("扰动贴图y轴Clamp", Float) = 0
		[SubToggle(g4, _)]_Float54("扰动极坐标（竖向贴图）", Float) = 0
		[Sub(g4)]_Float44("扰动贴图旋转", Range( -1 , 1)) = 0
		[HideInInspector]_noise_ST("_noise_ST", Vector) = (1,1,0,0)
		[Sub(g4)]_Vector17("扰动流动&斜切", Vector) = (0,0,0,0)
		[Enum(multiply,0,add,1)][KWEnum(g4,multiply,_0,add,_1)]_Float76("扰动遮罩/双重扰动（add为双重扰动）", Float) = 0
		[Sub(g4)]_noisemask("扰动遮罩", 2D) = "white" {}
		[SubToggle(g4, _)]_Float73("扰动遮罩x轴Clamp", Float) = 0
		[SubToggle(g4, _)]_Float84("扰动遮罩y轴Clamp", Float) = 0
		[SubToggle(g4, _)]_Float57("扰动遮罩极坐标", Float) = 0
		[Sub(g4)]_Float74("扰动遮罩旋转", Range( -1 , 1)) = 0
		[HideInInspector]_noisemask_ST("_noisemask_ST", Vector) = (1,1,0,0)
		[Sub(g4)]_Vector23("扰动遮罩流动&斜切", Vector) = (0,0,0,0)
		[Sub(g4)]_Vector33("扰动贴图remap", Vector) = (0,1,0,1)
		[Sub(g4)]_Float9("主贴图扰动强度", Float) = 0
		[Sub(g4)]_Float80("mask扰动强度", Float) = 0
		[Sub(g4)]_Float79("溶解扰动强度", Float) = 0
		[Main(g14, _, off, off)]_Maintex_Group4("Flowmap", Float) = 0
		[Sub(g14)]_flowmaptex("flowmaptex", 2D) = "white" {}
		[Sub(g14)]_Float32("flowmap扰动", Range( 0 , 1)) = 0
		[Main(g5, _, off, off)]_Vertex_Group("顶点偏移", Float) = 0
		[Enum(off,0,on,1)][KWEnum(g5,off,_0,on,_1)]_Float135("顶点法线", Float) = 1
		[Sub(g5)]_vertextex("顶点偏移贴图", 2D) = "white" {}
		[Sub(g5)]_Vector32("顶点偏移贴图remap", Vector) = (0,1,0,1)
		[SubToggle(g5, _)]_Float68("顶点偏移贴图x轴Clamp", Float) = 0
		[SubToggle(g5, _)]_Float89("顶点偏移贴图y轴Clamp", Float) = 0
		[SubToggle(g5, _)]_Float56("顶点偏移极坐标（竖向贴图）", Float) = 0
		[Sub(g5)]_Float45("顶点贴图旋转", Range( -1 , 1)) = 0
		[HideInInspector]_vertextex_ST("_vertextex_ST", Vector) = (1,1,0,0)
		[Sub(g5)]_Vector19("顶点偏移流动&斜切", Vector) = (0,0,0,0)
		[Sub(g5)]_Vector5("顶点偏移xyz强度", Vector) = (0,0,0,0)
		[Sub(g5)]_vertextex1("顶点偏移遮罩", 2D) = "white" {}
		[SubToggle(g5, _)]_Float78("顶点偏移遮罩x轴Clamp", Float) = 0
		[SubToggle(g5, _)]_Float88("顶点偏移遮罩y轴Clamp", Float) = 0
		[SubToggle(g5, _)]_Float75("顶点偏移遮罩极坐标", Float) = 0
		[Sub(g5)]_Float77("顶点遮罩旋转", Range( -1 , 1)) = 0
		[HideInInspector]_vertextex1_ST("_vertextex1_ST", Vector) = (1,1,0,0)
		[Sub(g5)]_Vector25("顶点偏移遮罩流动＆斜切", Vector) = (0,0,0,0)
		[Main(g6, _, off, off)]_Ref_Group("折射", Float) = 0
		[Enum(off,0,on,1)][KWEnum(g6,off,_0,on,_1)]_Float48("折射开关", Float) = 0
		[Sub(g6)]_reftex(" 折射贴图（法线）", 2D) = "white" {}
		[SubToggle(g6, _)]_Float69("折射贴图x轴Clamp", Float) = 0
		[SubToggle(g6, _)]_Float86("折射贴图y轴Clamp", Float) = 0
		[SubToggle(g6, _)]_Float58("折射极坐标（竖向贴图）", Float) = 0
		[Sub(g6)]_Float59("折射贴图旋转", Range( -1 , 1)) = 1
		[HideInInspector]_reftex_ST("_reftex_ST", Vector) = (1,1,0,0)
		[Sub(g6)]_Vector21("折射流动&斜切", Vector) = (0,0,0,0)
		[HDR][Sub(g6)]_Color2("折射颜色", Color) = (1,1,1,1)
		[Sub(g6)]_Float23("折射强度", Float) = 0
		[Main(g12, _, off, off)]_Maintex_Group2("阴影", Float) = 0
		[Enum(off,0,20,1)][KWEnum(g12,off,_0,on,_1)]_Float134("阴影开关", Float) = 0
		[Sub(g12)]_normallight("法线", 2D) = "white" {}
		[Sub(g12)]_Vector10("法线流动&斜切", Vector) = (0,0,0,0)
		[Sub(g12)]_Float146("法线强度", Float) = 0
		[Sub(g12)]_Float133("阴影软硬", Range( 0.5 , 1)) = 0.5
		[Sub(g12)]_Float132("阴影范围", Float) = 0.5
		[HDR][Sub(g12)]_Color5("亮部颜色", Color) = (1,1,1,1)
		[HDR][Sub(g12)]_Color4("暗部颜色", Color) = (0.490566,0.490566,0.490566,1)
		[SubToggle(g12, _)]_Float137("切换为假点光（默认为平行光）", Float) = 0
		[Sub(g12)]_Vector34("假点光坐标", Vector) = (0,0,0,0)
		[Main(g13, _, off, off)]_Maintex_Group3("Matcap&Cubemap", Float) = 0
		[Enum(off,0,on,1)][KWEnum(g13,off,_0,on,_1)]_Float141("反射开关", Float) = 0
		[Sub(g13)]_matcap("matcap", 2D) = "white" {}
		[Sub(g13)]_Float157("matcap去色", Range( 0 , 1)) = 0
		[Sub(g13)]_Float140("matcap强度", Float) = 0
		[NoScaleOffset][Sub(g13)]_cubemap("cubemap", CUBE) = "white" {}
		[Sub(g13)]_Float258("cube强度", Float) = 0
		[Main(g15, _, off, off)]_Maintex_Group5("视差", Float) = 0
		[Enum(off,0,on,1)][KWEnum(g15,off,_0,on,_1)]_Float143("开启视差映射(mesh模式下使用）", Float) = 0
		[Sub(g15)]_parallax("视差贴图", 2D) = "white" {}
		[Sub(g15)]_Float38("视差缩放", Float) = 0
		[Sub(g15)]_refplane("refplane(0黑色下沉,1白色抬高)", Range( 0 , 1)) = 1
		[Main(g7, _, off, off)]_Custom_Group1("custom控制", Float) = 0
		[SubToggle(g7, _)]_Float10("主贴图自定义偏移", Float) = 0
		[Enum(x1,0,y1,1,z1,2,w1,3)][KWEnum(g7,custom1x,_0,custom1y,_1,custom1z,_2,custom1w,_3)]_Custom1("主贴图x轴", Float) = 0
		[Enum(x1,0,y1,1,z1,2,w1,3)][KWEnum(g7,custom1x,_0,custom1y,_1,custom1z,_2,custom1w,_3)]_Custom2("主贴图y轴", Float) = 0
		[SubToggle(g7, _)][Space][Space]_Float12("mask01自定义偏移", Float) = 0
		[Enum(x1,0,y1,1,z1,2,w1,3)][KWEnum(g7,custom1x,_0,custom1y,_1,custom1z,_2,custom1w,_3)]_Custom3("Maskx轴", Float) = 0
		[Enum(x1,0,y1,1,z1,2,w1,3)][KWEnum(g7,custom1x,_0,custom1y,_1,custom1z,_2,custom1w,_3)]_Custom4("Masky轴", Float) = 0
		[SubToggle(g7, _)][Space][Space]_Float50("溶解自定义偏移", Float) = 0
		[Enum(x1,0,y1,1,z1,2,w1,3)][KWEnum(g7,custom1x,_0,custom1y,_1,custom1z,_2,custom1w,_3)]_Custom5("溶解x轴", Float) = 0
		[Enum(x1,0,y1,1,z1,2,w1,3)][KWEnum(g7,custom1x,_0,custom1y,_1,custom1z,_2,custom1w,_3)]_Custom6("溶解y轴", Float) = 0
		[SubToggle(g7, _)][Space][Space]_Float11("custom控制溶解", Float) = 0
		[SubToggle(g7, _)]_Float142("custom控制溶解软硬", Float) = 0
		[Enum(off,0,20,1)][KWEnum(g7,off,_0,on,_1)]_Float129("粒子alpha控制溶解（溶解拖尾使用）", Float) = 0
		[Enum(x2,0,y2,1,z2,2,w2,3)][KWEnum(g7,custom2x,_0,custom2y,_1,custom2z,_2,custom2w,_3)]_Custom7("自定义溶解", Float) = 0
		[Enum(x2,0,y2,1,z2,2,w2,3)][KWEnum(g7,custom2x,_0,custom2y,_1,custom2z,_2,custom2w,_3)]_Custom9("自定义溶解软硬", Float) = 0
		[SubToggle(g7, _)][Space][Space]_Float31("custom控制flowmap扭曲", Float) = 0
		[Enum(x2,0,y2,1,z2,2,w2,3)][KWEnum(g7,custom2x,_0,custom2y,_1,custom2z,_2,custom2w,_3)]_Custom8("自定义flowmap扭曲", Float) = 0
		[SubToggle(g7, _)][Space][Space]_Float257("custom控制扰动总强度", Float) = 0
		[Enum(x2,0,y2,1,z2,2,w2,3)][KWEnum(g7,custom2x,_0,custom2y,_1,custom2z,_2,custom2w,_3)]_Custom12("自定义扰动强度", Float) = 0
		[SubToggle(g7, _)][Space][Space]_Float24("custom控制折射", Float) = 0
		[Enum(x2,0,y2,1,z2,2,w2,3)][KWEnum(g7,custom2x,_0,custom2y,_1,custom2z,_2,custom2w,_3)]_Custom10("自定义折射", Float) = 0
		[SubToggle(g7, _)][Space][Space]_Float22("custom控制顶点偏移强度", Float) = 0
		[ASEEnd][Enum(x2,0,y2,1,z2,2,w2,3)][KWEnum(g7,custom2x,_0,custom2y,_1,custom2z,_2,custom2w,_3)]_Custom11("自定义顶点偏移强度", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Opaque" "Queue"="Transparent" }
	LOD 100

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend SrcAlpha [_Float1]
		AlphaToMask Off
		Cull [_Float2]
		ColorMask [_Float60]
		ZWrite [_Float4]
		ZTest [_Ztestmode]
		Stencil
		{
			Ref [_Float46]
			CompFront [_Ztestmode1]
			PassFront [_Ztestmode2]
			FailFront Keep
			ZFailFront Keep
			CompBack [_Ztestmode1]
			PassBack [_Ztestmode2]
			FailBack Keep
			ZFailBack Keep
		}
		
		GrabPass{ }

		Pass
		{
			Name "Unlit"
			Tags { "LightMode"="ForwardBase" }
			CGPROGRAM

			#if defined(UNITY_STEREO_INSTANCING_ENABLED) || defined(UNITY_STEREO_MULTIVIEW_ENABLED)
			#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex);
			#else
			#define ASE_DECLARE_SCREENSPACE_TEXTURE(tex) UNITY_DECLARE_SCREENSPACE_TEXTURE(tex)
			#endif


			#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
			//only defining to not throw compilation error over Unity 5.5
			#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
			#endif
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"
			#include "UnityStandardBRDF.cginc"
			#include "UnityStandardUtils.cginc"
			#include "Lighting.cginc"
			#include "AutoLight.cginc"
			#define ASE_NEEDS_VERT_POSITION
			#define ASE_NEEDS_FRAG_WORLD_POSITION
			#define ASE_NEEDS_FRAG_COLOR


			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float3 ase_normal : NORMAL;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_tangent : TANGENT;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 worldPos : TEXCOORD0;
				#endif
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_texcoord3 : TEXCOORD3;
				float4 ase_texcoord4 : TEXCOORD4;
				float4 ase_texcoord5 : TEXCOORD5;
				float4 ase_texcoord6 : TEXCOORD6;
				float4 ase_texcoord7 : TEXCOORD7;
				float4 ase_texcoord8 : TEXCOORD8;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			//This is a late directive
			
			uniform float _Maintex_Group3;
			uniform float _Maintex_Group4;
			uniform float _Ztestmode;
			uniform float _Custom_Group1;
			uniform float _Ztestmode2;
			uniform float _Gradtex;
			uniform float _Depthfade_Group1;
			uniform float _Float46;
			uniform float _Color_Group3;
			uniform float _Fresnel_Group2;
			uniform float _Ref_Group;
			uniform float _Maintex_Group5;
			uniform float _Noise_Group;
			uniform float _Ztestmode1;
			uniform float _Disolove_Group;
			uniform float _Float4;
			uniform float _Float1;
			uniform float _Mask_Group;
			uniform float _Float60;
			uniform float _Maintex_Group;
			uniform float _Float2;
			uniform float _Vertex_Group;
			uniform float _Maintex_Group2;
			uniform float _Color_Group2;
			uniform sampler2D _vertextex;
			uniform float4 _Vector19;
			uniform float4 _vertextex_ST;
			uniform float _Float131;
			uniform float _Float56;
			uniform float _Float45;
			uniform float _Float68;
			uniform float _Float89;
			uniform float4 _Vector32;
			uniform float _Float135;
			uniform float3 _Vector5;
			uniform float _Custom11;
			uniform float _Float22;
			uniform sampler2D _vertextex1;
			uniform float4 _Vector25;
			uniform float4 _vertextex1_ST;
			uniform float _Float75;
			uniform float _Float77;
			uniform float _Float78;
			uniform float _Float88;
			uniform float _Float28;
			uniform float _Float55;
			uniform float _Float0;
			UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
			uniform float4 _CameraDepthTexture_TexelSize;
			uniform float _Float16;
			uniform float _Float5;
			uniform float _Float30;
			uniform sampler2D _maintex;
			uniform float4 _Vector0;
			uniform float4 _maintex_ST;
			uniform float _Float144;
			uniform float _Custom1;
			uniform float _Custom2;
			uniform float _Float10;
			uniform float _Float49;
			uniform sampler2D _parallax;
			uniform float _Float38;
			uniform float _refplane;
			uniform float4 _parallax_ST;
			uniform float _Float143;
			uniform sampler1D _noisemask;
			uniform float4 _Vector23;
			uniform float4 _noisemask_ST;
			uniform float _Float57;
			uniform float _Float74;
			uniform float _Float73;
			uniform float _Float84;
			uniform sampler2D _noise;
			uniform float4 _Vector17;
			uniform float4 _noise_ST;
			uniform float _Float54;
			uniform float _Float44;
			uniform float _Float67;
			uniform float _Float85;
			uniform float4 _Vector33;
			uniform float _Float76;
			uniform float _Float9;
			uniform float _Custom12;
			uniform float _Float257;
			uniform sampler2D _flowmaptex;
			uniform float4 _flowmaptex_ST;
			uniform float _Float32;
			uniform float _Float31;
			uniform float _Float39;
			uniform float _Float62;
			uniform float _Float71;
			uniform float4 _Vector16;
			uniform sampler2D _Gradienttex;
			uniform float4 _Vector7;
			uniform float4 _Gradienttex_ST;
			uniform float4 _Vector9;
			uniform float _Float159;
			uniform float _Float40;
			uniform float _Float63;
			uniform float _Float81;
			uniform float _Float29;
			uniform float _Float61;
			uniform float4 _Color0;
			uniform float4 _Color3;
			uniform float _Float35;
			uniform float4 _Color6;
			uniform float _power3;
			uniform float _Float19;
			uniform float _Float20;
			uniform float _Float145;
			uniform float _Float33;
			uniform float4 _Color7;
			uniform float4 _Color1;
			uniform float _Float6;
			uniform float _Custom7;
			uniform float _Float11;
			uniform float _Float129;
			uniform sampler2D _dissolvetex;
			uniform float4 _Vector15;
			uniform float4 _dissolvetex_ST;
			uniform float _Custom5;
			uniform float _Custom6;
			uniform float _Float50;
			uniform float _Float53;
			uniform float _Float79;
			uniform float _Custom8;
			uniform float _Float41;
			uniform float _Float66;
			uniform float _Float87;
			uniform sampler2D _TextureSample1;
			uniform float4 _TextureSample1_ST;
			uniform float _Float47;
			uniform float4 _Vector35;
			uniform float _Float155;
			uniform float _Float139;
			uniform float _Float130;
			uniform float _Float17;
			uniform float _Float160;
			uniform float _Float8;
			uniform float _Custom9;
			uniform float _Float142;
			uniform float _Float25;
			ASE_DECLARE_SCREENSPACE_TEXTURE( _GrabTexture )
			uniform sampler2D _reftex;
			uniform float4 _Vector21;
			uniform float4 _reftex_ST;
			uniform float _Float58;
			uniform float _Float59;
			uniform float _Float69;
			uniform float _Float86;
			uniform float _Float23;
			uniform float _Custom10;
			uniform float _Float24;
			uniform float4 _Color2;
			uniform float _Float48;
			uniform float4 _Color4;
			uniform float4 _Color5;
			uniform float _Float133;
			uniform sampler2D _normallight;
			uniform float4 _Vector10;
			uniform float4 _normallight_ST;
			uniform float _Float146;
			uniform float4 _Vector34;
			uniform float _Float137;
			uniform float _Float132;
			uniform float _Float134;
			uniform sampler2D _matcap;
			uniform float _Float157;
			uniform float _Float140;
			uniform samplerCUBE _cubemap;
			uniform float _Float258;
			uniform float _Float141;
			uniform float _Float14;
			uniform float _maintex_alpha;
			uniform float _Float34;
			uniform float _Float37;
			uniform float _Float36;
			uniform float _Float15;
			uniform sampler2D _Mask;
			uniform float4 _Vector11;
			uniform float4 _Mask_ST;
			uniform float _Custom3;
			uniform float _Custom4;
			uniform float _Float12;
			uniform float _Float51;
			uniform float _Float80;
			uniform float _Float43;
			uniform float _Float64;
			uniform float _Float82;
			uniform float _mask01_alpha;
			uniform sampler2D _Mask1;
			uniform float4 _Vector13;
			uniform float4 _Mask1_ST;
			uniform float _Float52;
			uniform float _Float42;
			uniform float _Float65;
			uniform float _Float83;
			uniform float _mask02_alpha;
			uniform float _Float70;
			uniform float _Float72;
			inline float2 POM( sampler2D heightMap, float2 uvs, float2 dx, float2 dy, float3 normalWorld, float3 viewWorld, float3 viewDirTan, int minSamples, int maxSamples, float parallax, float refPlane, float2 tilling, float2 curv, int index )
			{
				float3 result = 0;
				int stepIndex = 0;
				int numSteps = ( int )lerp( (float)maxSamples, (float)minSamples, saturate( dot( normalWorld, viewWorld ) ) );
				float layerHeight = 1.0 / numSteps;
				float2 plane = parallax * ( viewDirTan.xy / viewDirTan.z );
				uvs.xy += refPlane * plane;
				float2 deltaTex = -plane * layerHeight;
				float2 prevTexOffset = 0;
				float prevRayZ = 1.0f;
				float prevHeight = 0.0f;
				float2 currTexOffset = deltaTex;
				float currRayZ = 1.0f - layerHeight;
				float currHeight = 0.0f;
				float intersection = 0;
				float2 finalTexOffset = 0;
				while ( stepIndex < numSteps + 1 )
				{
				 	currHeight = tex2Dgrad( heightMap, uvs + currTexOffset, dx, dy ).r;
				 	if ( currHeight > currRayZ )
				 	{
				 	 	stepIndex = numSteps + 1;
				 	}
				 	else
				 	{
				 	 	stepIndex++;
				 	 	prevTexOffset = currTexOffset;
				 	 	prevRayZ = currRayZ;
				 	 	prevHeight = currHeight;
				 	 	currTexOffset += deltaTex;
				 	 	currRayZ -= layerHeight;
				 	}
				}
				int sectionSteps = 10;
				int sectionIndex = 0;
				float newZ = 0;
				float newHeight = 0;
				while ( sectionIndex < sectionSteps )
				{
				 	intersection = ( prevHeight - prevRayZ ) / ( prevHeight - currHeight + currRayZ - prevRayZ );
				 	finalTexOffset = prevTexOffset + intersection * deltaTex;
				 	newZ = prevRayZ - intersection * layerHeight;
				 	newHeight = tex2Dgrad( heightMap, uvs + finalTexOffset, dx, dy ).r;
				 	if ( newHeight > newZ )
				 	{
				 	 	currTexOffset = finalTexOffset;
				 	 	currHeight = newHeight;
				 	 	currRayZ = newZ;
				 	 	deltaTex = intersection * deltaTex;
				 	 	layerHeight = intersection * layerHeight;
				 	}
				 	else
				 	{
				 	 	prevTexOffset = finalTexOffset;
				 	 	prevHeight = newHeight;
				 	 	prevRayZ = newZ;
				 	 	deltaTex = ( 1 - intersection ) * deltaTex;
				 	 	layerHeight = ( 1 - intersection ) * layerHeight;
				 	}
				 	sectionIndex++;
				}
				return uvs.xy + finalTexOffset;
			}
			
			inline float4 ASE_ComputeGrabScreenPos( float4 pos )
			{
				#if UNITY_UV_STARTS_AT_TOP
				float scale = -1.0;
				#else
				float scale = 1.0;
				#endif
				float4 o = pos;
				o.y = pos.w * 0.5f;
				o.y = ( pos.y - o.y ) * _ProjectionParams.x * scale + o.y;
				return o;
			}
			

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				float2 appendResult557 = (float2(_Vector19.x , _Vector19.y));
				float2 uv_vertextex = v.ase_texcoord.xyz * _vertextex_ST.xy + _vertextex_ST.zw;
				float2 uv2_vertextex = v.ase_texcoord1 * _vertextex_ST.xy + _vertextex_ST.zw;
				float uv2930 = _Float131;
				float2 lerpResult949 = lerp( uv_vertextex , uv2_vertextex , uv2930);
				float3 appendResult895 = (float3(1.0 , _Vector19.z , 0.0));
				float3 appendResult897 = (float3(_Vector19.w , 1.0 , 0.0));
				float2 temp_output_898_0 = mul( float3( lerpResult949 ,  0.0 ), float3x3(appendResult895, appendResult897, float3(0,0,1)) ).xy;
				float2 appendResult546 = (float2(_vertextex_ST.z , _vertextex_ST.w));
				float2 CenteredUV15_g137 = ( v.ase_texcoord.xy - float2( 0.5,0.5 ) );
				float2 break17_g137 = CenteredUV15_g137;
				float2 appendResult23_g137 = (float2(( length( CenteredUV15_g137 ) * _vertextex_ST.x * 2.0 ) , ( atan2( break17_g137.x , break17_g137.y ) * ( 1.0 / 6.28318548202515 ) * _vertextex_ST.y )));
				float2 lerpResult556 = lerp( ( temp_output_898_0 + appendResult546 ) , (( appendResult23_g137 * temp_output_898_0 )*float2( 1,1 ) + appendResult546) , _Float56);
				float2 panner168 = ( 1.0 * _Time.y * appendResult557 + lerpResult556);
				float cos398 = cos( ( _Float45 * UNITY_PI ) );
				float sin398 = sin( ( _Float45 * UNITY_PI ) );
				float2 rotator398 = mul( panner168 - float2( 0.5,0.5 ) , float2x2( cos398 , -sin398 , sin398 , cos398 )) + float2( 0.5,0.5 );
				float2 break644 = rotator398;
				float clampResult646 = clamp( break644.x , 0.0 , 1.0 );
				float lerpResult790 = lerp( break644.x , clampResult646 , _Float68);
				float clampResult645 = clamp( break644.y , 0.0 , 1.0 );
				float lerpResult791 = lerp( break644.y , clampResult645 , _Float89);
				float2 appendResult647 = (float2(lerpResult790 , lerpResult791));
				float3 temp_cast_4 = (1.0).xxx;
				float3 lerpResult993 = lerp( temp_cast_4 , v.ase_normal , _Float135);
				float4 texCoord1545 = v.ase_texcoord2;
				texCoord1545.xy = v.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				float ifLocalVar1546 = 0;
				if( _Custom11 == 0.0 )
				ifLocalVar1546 = 1.0;
				else
				ifLocalVar1546 = 0.0;
				float ifLocalVar1543 = 0;
				if( _Custom11 == 1.0 )
				ifLocalVar1543 = 1.0;
				else
				ifLocalVar1543 = 0.0;
				float ifLocalVar1544 = 0;
				if( _Custom11 == 2.0 )
				ifLocalVar1544 = 1.0;
				else
				ifLocalVar1544 = 0.0;
				float ifLocalVar1547 = 0;
				if( _Custom11 == 3.0 )
				ifLocalVar1547 = 1.0;
				else
				ifLocalVar1547 = 0.0;
				float custom111553 = ( ( texCoord1545.x * ifLocalVar1546 ) + ( texCoord1545.y * ifLocalVar1543 ) + ( texCoord1545.z * ifLocalVar1544 ) + ( texCoord1545.w * ifLocalVar1547 ) );
				float lerpResult176 = lerp( 1.0 , custom111553 , _Float22);
				float2 appendResult719 = (float2(_Vector25.x , _Vector25.y));
				float2 uv_vertextex1 = v.ase_texcoord.xy * _vertextex1_ST.xy + _vertextex1_ST.zw;
				float2 uv2_vertextex1 = v.ase_texcoord1.xy * _vertextex1_ST.xy + _vertextex1_ST.zw;
				float2 lerpResult946 = lerp( uv_vertextex1 , uv2_vertextex1 , uv2930);
				float3 appendResult886 = (float3(1.0 , _Vector25.z , 0.0));
				float3 appendResult888 = (float3(_Vector25.w , 1.0 , 0.0));
				float2 appendResult710 = (float2(_vertextex1_ST.z , _vertextex1_ST.w));
				float2 CenteredUV15_g138 = ( v.ase_texcoord.xy - float2( 0.5,0.5 ) );
				float2 break17_g138 = CenteredUV15_g138;
				float2 appendResult23_g138 = (float2(( length( CenteredUV15_g138 ) * _vertextex1_ST.x * 2.0 ) , ( atan2( break17_g138.x , break17_g138.y ) * ( 1.0 / 6.28318548202515 ) * _vertextex1_ST.y )));
				float2 lerpResult728 = lerp( ( mul( float3( lerpResult946 ,  0.0 ), float3x3(appendResult886, appendResult888, float3(0,0,1)) ).xy + appendResult710 ) , (mul( float3( appendResult23_g138 ,  0.0 ), float3x3(appendResult886, appendResult888, float3(0,0,1)) ).xy*float2( 1,1 ) + appendResult710) , _Float75);
				float2 panner725 = ( 1.0 * _Time.y * appendResult719 + lerpResult728);
				float cos726 = cos( ( _Float77 * UNITY_PI ) );
				float sin726 = sin( ( _Float77 * UNITY_PI ) );
				float2 rotator726 = mul( panner725 - float2( 0.5,0.5 ) , float2x2( cos726 , -sin726 , sin726 , cos726 )) + float2( 0.5,0.5 );
				float2 break720 = rotator726;
				float clampResult721 = clamp( break720.x , 0.0 , 1.0 );
				float lerpResult787 = lerp( break720.x , clampResult721 , _Float78);
				float clampResult722 = clamp( break720.y , 0.0 , 1.0 );
				float lerpResult789 = lerp( break720.y , clampResult722 , _Float88);
				float2 appendResult723 = (float2(lerpResult787 , lerpResult789));
				float3 vertexoffset181 = ( (_Vector32.z + (tex2Dlod( _vertextex, float4( appendResult647, 0, 0.0) ).r - _Vector32.x) * (_Vector32.w - _Vector32.z) / (_Vector32.y - _Vector32.x)) * lerpResult993 * _Vector5 * lerpResult176 * tex2Dlod( _vertextex1, float4( appendResult723, 0, 0.0) ).r );
				
				float3 customSurfaceDepth754 = v.vertex.xyz;
				float customEye754 = -UnityObjectToViewPos(customSurfaceDepth754).z;
				o.ase_texcoord1.x = customEye754;
				float3 vertexPos97 = v.vertex.xyz;
				float4 ase_clipPos97 = UnityObjectToClipPos(vertexPos97);
				float4 screenPos97 = ComputeScreenPos(ase_clipPos97);
				o.ase_texcoord2 = screenPos97;
				float4 ase_clipPos = UnityObjectToClipPos(v.vertex);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				o.ase_texcoord3 = screenPos;
				float3 ase_worldTangent = UnityObjectToWorldDir(v.ase_tangent);
				o.ase_texcoord5.xyz = ase_worldTangent;
				float3 ase_worldNormal = UnityObjectToWorldNormal(v.ase_normal);
				o.ase_texcoord6.xyz = ase_worldNormal;
				float ase_vertexTangentSign = v.ase_tangent.w * unity_WorldTransformParams.w;
				float3 ase_worldBitangent = cross( ase_worldNormal, ase_worldTangent ) * ase_vertexTangentSign;
				o.ase_texcoord7.xyz = ase_worldBitangent;
				
				o.ase_texcoord1.yzw = v.ase_texcoord.xyz;
				o.ase_texcoord4 = v.ase_texcoord1;
				o.ase_texcoord8 = v.ase_texcoord2;
				o.ase_color = v.color;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord5.w = 0;
				o.ase_texcoord6.w = 0;
				o.ase_texcoord7.w = 0;
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = vertexoffset181;
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
			
			fixed4 frag (v2f i , half ase_vface : VFACE) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				fixed4 finalColor;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 WorldPosition = i.worldPos;
				#endif
				float customEye754 = i.ase_texcoord1.x;
				float cameraDepthFade754 = (( customEye754 -_ProjectionParams.y - _Float0 ) / _Float55);
				float4 screenPos97 = i.ase_texcoord2;
				float4 ase_screenPosNorm97 = screenPos97 / screenPos97.w;
				ase_screenPosNorm97.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm97.z : ase_screenPosNorm97.z * 0.5 + 0.5;
				float screenDepth97 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm97.xy ));
				float distanceDepth97 = saturate( abs( ( screenDepth97 - LinearEyeDepth( ase_screenPosNorm97.z ) ) / ( _Float16 ) ) );
				float depthfade_switch334 = _Float5;
				float lerpResult336 = lerp( distanceDepth97 , ( 1.0 - distanceDepth97 ) , depthfade_switch334);
				float depthfade126 = ( saturate( cameraDepthFade754 ) * lerpResult336 );
				float lerpResult330 = lerp( 0.0 , depthfade126 , depthfade_switch334);
				float2 appendResult440 = (float2(_Vector0.x , _Vector0.y));
				float2 uv_maintex = i.ase_texcoord1.yzw.xy * _maintex_ST.xy + _maintex_ST.zw;
				float4 screenPos = i.ase_texcoord3;
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float2 appendResult1132 = (float2(ase_screenPosNorm.x , ase_screenPosNorm.y));
				float2 appendResult1131 = (float2(_maintex_ST.x , _maintex_ST.y));
				float2 appendResult1130 = (float2(_maintex_ST.z , _maintex_ST.w));
				float2 lerpResult1127 = lerp( uv_maintex , (appendResult1132*appendResult1131 + appendResult1130) , _Float144);
				float3 appendResult799 = (float3(1.0 , _Vector0.z , 0.0));
				float3 appendResult803 = (float3(_Vector0.w , 1.0 , 0.0));
				float2 appendResult433 = (float2(_maintex_ST.z , _maintex_ST.w));
				float4 texCoord1281 = i.ase_texcoord4;
				texCoord1281.xy = i.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float ifLocalVar1283 = 0;
				if( _Custom1 == 0.0 )
				ifLocalVar1283 = 1.0;
				else
				ifLocalVar1283 = 0.0;
				float ifLocalVar1288 = 0;
				if( _Custom1 == 1.0 )
				ifLocalVar1288 = 1.0;
				else
				ifLocalVar1288 = 0.0;
				float ifLocalVar1293 = 0;
				if( _Custom1 == 2.0 )
				ifLocalVar1293 = 1.0;
				else
				ifLocalVar1293 = 0.0;
				float ifLocalVar1299 = 0;
				if( _Custom1 == 3.0 )
				ifLocalVar1299 = 1.0;
				else
				ifLocalVar1299 = 0.0;
				float custom11336 = ( ( texCoord1281.x * ifLocalVar1283 ) + ( texCoord1281.y * ifLocalVar1288 ) + ( texCoord1281.z * ifLocalVar1293 ) + ( texCoord1281.w * ifLocalVar1299 ) );
				float4 texCoord1330 = i.ase_texcoord4;
				texCoord1330.xy = i.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float ifLocalVar1324 = 0;
				if( _Custom2 == 0.0 )
				ifLocalVar1324 = 1.0;
				else
				ifLocalVar1324 = 0.0;
				float ifLocalVar1317 = 0;
				if( _Custom2 == 1.0 )
				ifLocalVar1317 = 1.0;
				else
				ifLocalVar1317 = 0.0;
				float ifLocalVar1333 = 0;
				if( _Custom2 == 2.0 )
				ifLocalVar1333 = 1.0;
				else
				ifLocalVar1333 = 0.0;
				float ifLocalVar1334 = 0;
				if( _Custom2 == 3.0 )
				ifLocalVar1334 = 1.0;
				else
				ifLocalVar1334 = 0.0;
				float custom21337 = ( ( texCoord1330.x * ifLocalVar1324 ) + ( texCoord1330.y * ifLocalVar1317 ) + ( texCoord1330.z * ifLocalVar1333 ) + ( texCoord1330.w * ifLocalVar1334 ) );
				float2 appendResult42 = (float2(custom11336 , custom21337));
				float2 lerpResult59 = lerp( appendResult433 , appendResult42 , _Float10);
				float2 CenteredUV15_g98 = ( i.ase_texcoord1.yzw.xy - float2( 0.5,0.5 ) );
				float2 break17_g98 = CenteredUV15_g98;
				float2 appendResult23_g98 = (float2(( length( CenteredUV15_g98 ) * _maintex_ST.x * 2.0 ) , ( atan2( break17_g98.x , break17_g98.y ) * ( 1.0 / 6.28318548202515 ) * _maintex_ST.y )));
				float2 lerpResult449 = lerp( appendResult433 , appendResult42 , _Float10);
				float2 lerpResult444 = lerp( ( mul( float3( lerpResult1127 ,  0.0 ), float3x3(appendResult799, appendResult803, float3(0,0,1)) ).xy + lerpResult59 ) , (mul( float3( appendResult23_g98 ,  0.0 ), float3x3(appendResult799, appendResult803, float3(0,0,1)) ).xy*float2( 1,1 ) + lerpResult449) , _Float49);
				float2 panner36 = ( 1.0 * _Time.y * appendResult440 + lerpResult444);
				float2 maintexUV_00161 = panner36;
				float3 ase_worldTangent = i.ase_texcoord5.xyz;
				float3 ase_worldNormal = i.ase_texcoord6.xyz;
				float3 ase_worldBitangent = i.ase_texcoord7.xyz;
				float3 tanToWorld0 = float3( ase_worldTangent.x, ase_worldBitangent.x, ase_worldNormal.x );
				float3 tanToWorld1 = float3( ase_worldTangent.y, ase_worldBitangent.y, ase_worldNormal.y );
				float3 tanToWorld2 = float3( ase_worldTangent.z, ase_worldBitangent.z, ase_worldNormal.z );
				float3 ase_worldViewDir = UnityWorldSpaceViewDir(WorldPosition);
				ase_worldViewDir = normalize(ase_worldViewDir);
				float3 ase_tanViewDir =  tanToWorld0 * ase_worldViewDir.x + tanToWorld1 * ase_worldViewDir.y  + tanToWorld2 * ase_worldViewDir.z;
				ase_tanViewDir = normalize(ase_tanViewDir);
				float2 OffsetPOM1092 = POM( _parallax, maintexUV_00161, ddx(maintexUV_00161), ddy(maintexUV_00161), ase_worldNormal, ase_worldViewDir, ase_tanViewDir, 128, 128, ( _Float38 * 0.1 ), _refplane, _parallax_ST.xy, float2(0,0), 0 );
				float2 parallax1097 = OffsetPOM1092;
				float2 lerpResult1099 = lerp( maintexUV_00161 , parallax1097 , _Float143);
				float2 appendResult687 = (float2(_Vector23.x , _Vector23.y));
				float2 uv_noisemask = i.ase_texcoord1.yzw.xy * _noisemask_ST.xy + _noisemask_ST.zw;
				float2 uv2_noisemask = i.ase_texcoord4.xy * _noisemask_ST.xy + _noisemask_ST.zw;
				float uv2930 = _Float131;
				float2 lerpResult938 = lerp( uv_noisemask , uv2_noisemask , uv2930);
				float3 appendResult866 = (float3(1.0 , _Vector23.z , 0.0));
				float3 appendResult865 = (float3(_Vector23.w , 1.0 , 0.0));
				float2 appendResult679 = (float2(_noisemask_ST.z , _noisemask_ST.w));
				float2 CenteredUV15_g47 = ( i.ase_texcoord1.yzw.xy - float2( 0.5,0.5 ) );
				float2 break17_g47 = CenteredUV15_g47;
				float2 appendResult23_g47 = (float2(( length( CenteredUV15_g47 ) * _noisemask_ST.x * 2.0 ) , ( atan2( break17_g47.x , break17_g47.y ) * ( 1.0 / 6.28318548202515 ) * _noisemask_ST.y )));
				float2 lerpResult688 = lerp( ( mul( float3( lerpResult938 ,  0.0 ), float3x3(appendResult866, appendResult865, float3(0,0,1)) ).xy + appendResult679 ) , (mul( float3( appendResult23_g47 ,  0.0 ), float3x3(appendResult866, appendResult865, float3(0,0,1)) ).xy*float2( 1,1 ) + appendResult679) , _Float57);
				float2 panner697 = ( 1.0 * _Time.y * appendResult687 + lerpResult688);
				float cos698 = cos( ( _Float74 * UNITY_PI ) );
				float sin698 = sin( ( _Float74 * UNITY_PI ) );
				float2 rotator698 = mul( panner697 - float2( 0.5,0.5 ) , float2x2( cos698 , -sin698 , sin698 , cos698 )) + float2( 0.5,0.5 );
				float2 break689 = rotator698;
				float clampResult690 = clamp( break689.x , 0.0 , 1.0 );
				float lerpResult775 = lerp( break689.x , clampResult690 , _Float73);
				float clampResult691 = clamp( break689.y , 0.0 , 1.0 );
				float lerpResult776 = lerp( break689.x , clampResult691 , _Float84);
				float2 appendResult693 = (float2(lerpResult775 , lerpResult776));
				float4 tex1DNode564 = tex1D( _noisemask, appendResult693.x );
				float2 appendResult530 = (float2(_Vector17.x , _Vector17.y));
				float2 uv_noise = i.ase_texcoord1.yzw.xy * _noise_ST.xy + _noise_ST.zw;
				float2 uv2_noise = i.ase_texcoord4.xy * _noise_ST.xy + _noise_ST.zw;
				float2 lerpResult941 = lerp( uv_noise , uv2_noise , uv2930);
				float3 appendResult876 = (float3(1.0 , _Vector17.z , 0.0));
				float3 appendResult878 = (float3(_Vector17.w , 1.0 , 0.0));
				float2 appendResult531 = (float2(_noise_ST.z , _noise_ST.w));
				float2 CenteredUV15_g46 = ( i.ase_texcoord1.yzw.xy - float2( 0.5,0.5 ) );
				float2 break17_g46 = CenteredUV15_g46;
				float2 appendResult23_g46 = (float2(( length( CenteredUV15_g46 ) * _noise_ST.x * 2.0 ) , ( atan2( break17_g46.x , break17_g46.y ) * ( 1.0 / 6.28318548202515 ) * _noise_ST.y )));
				float2 lerpResult539 = lerp( ( mul( float3( lerpResult941 ,  0.0 ), float3x3(appendResult876, appendResult878, float3(0,0,1)) ).xy + appendResult531 ) , (mul( float3( appendResult23_g46 ,  0.0 ), float3x3(appendResult876, appendResult878, float3(0,0,1)) ).xy*float2( 1,1 ) + appendResult531) , _Float54);
				float2 panner53 = ( 1.0 * _Time.y * appendResult530 + lerpResult539);
				float cos395 = cos( ( _Float44 * UNITY_PI ) );
				float sin395 = sin( ( _Float44 * UNITY_PI ) );
				float2 rotator395 = mul( panner53 - float2( 0.5,0.5 ) , float2x2( cos395 , -sin395 , sin395 , cos395 )) + float2( 0.5,0.5 );
				float2 break638 = rotator395;
				float clampResult640 = clamp( break638.x , 0.0 , 1.0 );
				float lerpResult778 = lerp( break638.x , clampResult640 , _Float67);
				float clampResult639 = clamp( break638.y , 0.0 , 1.0 );
				float lerpResult780 = lerp( break638.y , clampResult639 , _Float85);
				float2 appendResult641 = (float2(lerpResult778 , lerpResult780));
				float temp_output_923_0 = (_Vector33.z + (tex2D( _noise, appendResult641 ).r - _Vector33.x) * (_Vector33.w - _Vector33.z) / (_Vector33.y - _Vector33.x));
				float lerpResult701 = lerp( ( tex1DNode564.r * temp_output_923_0 ) , ( tex1DNode564.r + temp_output_923_0 ) , _Float76);
				float noise70 = lerpResult701;
				float4 texCoord1566 = i.ase_texcoord8;
				texCoord1566.xy = i.ase_texcoord8.xy * float2( 1,1 ) + float2( 0,0 );
				float ifLocalVar1567 = 0;
				if( _Custom12 == 0.0 )
				ifLocalVar1567 = 1.0;
				else
				ifLocalVar1567 = 0.0;
				float ifLocalVar1564 = 0;
				if( _Custom12 == 1.0 )
				ifLocalVar1564 = 1.0;
				else
				ifLocalVar1564 = 0.0;
				float ifLocalVar1565 = 0;
				if( _Custom12 == 2.0 )
				ifLocalVar1565 = 1.0;
				else
				ifLocalVar1565 = 0.0;
				float ifLocalVar1568 = 0;
				if( _Custom12 == 3.0 )
				ifLocalVar1568 = 1.0;
				else
				ifLocalVar1568 = 0.0;
				float custom121574 = ( ( texCoord1566.x * ifLocalVar1567 ) + ( texCoord1566.y * ifLocalVar1564 ) + ( texCoord1566.z * ifLocalVar1565 ) + ( texCoord1566.w * ifLocalVar1568 ) );
				float lerpResult1575 = lerp( 1.0 , custom121574 , _Float257);
				float noise_intensity_main67 = ( _Float9 * 0.1 * lerpResult1575 );
				float2 temp_output_354_0 = ( lerpResult1099 + ( noise70 * noise_intensity_main67 ) );
				float2 uv_flowmaptex = i.ase_texcoord1.yzw.xy * _flowmaptex_ST.xy + _flowmaptex_ST.zw;
				float4 tex2DNode241 = tex2D( _flowmaptex, uv_flowmaptex );
				float2 appendResult242 = (float2(tex2DNode241.r , tex2DNode241.g));
				float2 flowmap285 = appendResult242;
				float flowmap_intensity311 = _Float32;
				float4 texCoord100 = i.ase_texcoord8;
				texCoord100.xy = i.ase_texcoord8.xy * float2( 1,1 ) + float2( 0,0 );
				float flpwmap_custom_switch316 = _Float31;
				float lerpResult99 = lerp( flowmap_intensity311 , texCoord100.y , flpwmap_custom_switch316);
				float2 lerpResult283 = lerp( temp_output_354_0 , flowmap285 , lerpResult99);
				float cos377 = cos( ( _Float39 * UNITY_PI ) );
				float sin377 = sin( ( _Float39 * UNITY_PI ) );
				float2 rotator377 = mul( lerpResult283 - float2( 0.5,0.5 ) , float2x2( cos377 , -sin377 , sin377 , cos377 )) + float2( 0.5,0.5 );
				float2 break603 = rotator377;
				float clampResult604 = clamp( break603.x , 0.0 , 1.0 );
				float lerpResult607 = lerp( break603.x , clampResult604 , _Float62);
				float clampResult605 = clamp( break603.y , 0.0 , 1.0 );
				float lerpResult764 = lerp( break603.y , clampResult605 , _Float71);
				float2 appendResult606 = (float2(lerpResult607 , lerpResult764));
				float2 appendResult1225 = (float2(_Vector16.x , _Vector16.y));
				float2 temp_output_1229_0 = ( 0.01 * appendResult1225 );
				float4 tex2DNode1 = tex2D( _maintex, appendResult606 );
				float3 appendResult1228 = (float3(tex2D( _maintex, ( appendResult606 - temp_output_1229_0 ) ).r , tex2DNode1.g , tex2D( _maintex, ( temp_output_1229_0 + appendResult606 ) ).b));
				float2 appendResult464 = (float2(_Vector7.x , _Vector7.y));
				float2 uv_Gradienttex = i.ase_texcoord1.yzw.xy * _Gradienttex_ST.xy + _Gradienttex_ST.zw;
				float3 appendResult851 = (float3(1.0 , _Vector0.z , 0.0));
				float3 appendResult852 = (float3(_Vector0.w , 1.0 , 0.0));
				float2 temp_output_854_0 = mul( float3( uv_Gradienttex ,  0.0 ), float3x3(appendResult851, appendResult852, float3(0,0,1)) ).xy;
				float2 appendResult454 = (float2(( _Vector9.x - 1.0 ) , ( _Vector9.y - 1.0 )));
				float2 appendResult457 = (float2(_Vector9.z , _Vector9.w));
				float2 CenteredUV15_g100 = ( uv_Gradienttex - float2( 0.5,0.5 ) );
				float2 break17_g100 = CenteredUV15_g100;
				float2 appendResult23_g100 = (float2(( length( CenteredUV15_g100 ) * _Vector9.x * 2.0 ) , ( atan2( break17_g100.x , break17_g100.y ) * ( 1.0 / 6.28318548202515 ) * _Vector9.y )));
				float2 lerpResult451 = lerp( (temp_output_854_0*appendResult454 + ( temp_output_854_0 + appendResult457 )) , (mul( float3( appendResult23_g100 ,  0.0 ), float3x3(appendResult851, appendResult852, float3(0,0,1)) ).xy*float2( 1,1 ) + appendResult457) , _Float159);
				float2 panner229 = ( 1.0 * _Time.y * appendResult464 + lerpResult451);
				float2 Gradienttex231 = panner229;
				float2 temp_cast_20 = (noise70).xx;
				float2 lerpResult235 = lerp( Gradienttex231 , temp_cast_20 , noise_intensity_main67);
				float cos383 = cos( ( _Float40 * UNITY_PI ) );
				float sin383 = sin( ( _Float40 * UNITY_PI ) );
				float2 rotator383 = mul( lerpResult235 - float2( 0.5,0.5 ) , float2x2( cos383 , -sin383 , sin383 , cos383 )) + float2( 0.5,0.5 );
				float2 break609 = rotator383;
				float clampResult610 = clamp( break609.x , 0.0 , 1.0 );
				float lerpResult765 = lerp( break609.x , clampResult610 , _Float63);
				float clampResult611 = clamp( break609.y , 0.0 , 1.0 );
				float lerpResult767 = lerp( break609.y , clampResult611 , _Float81);
				float2 appendResult768 = (float2(lerpResult765 , lerpResult767));
				float4 tex2DNode212 = tex2D( _Gradienttex, appendResult768 );
				float4 lerpResult211 = lerp( float4( appendResult1228 , 0.0 ) , tex2DNode212 , _Float29);
				float4 lerpResult600 = lerp( lerpResult211 , ( float4( appendResult1228 , 0.0 ) * tex2DNode212 ) , _Float61);
				float4 lerpResult359 = lerp( _Color0 , _Color3 , _Float35);
				float4 switchResult356 = (((ase_vface>0)?(_Color0):(lerpResult359)));
				ase_worldViewDir = Unity_SafeNormalize( ase_worldViewDir );
				float3 normalizedWorldNormal = normalize( ase_worldNormal );
				float fresnelNdotV139 = dot( normalize( ( normalizedWorldNormal * ase_vface ) ), ase_worldViewDir );
				float fresnelNode139 = ( 0.0 + _power3 * pow( max( 1.0 - fresnelNdotV139 , 0.0001 ), _Float19 ) );
				float temp_output_140_0 = saturate( fresnelNode139 );
				float lerpResult144 = lerp( temp_output_140_0 , ( 1.0 - temp_output_140_0 ) , _Float20);
				float fresnel147 = lerpResult144;
				float4 lerpResult1135 = lerp( switchResult356 , _Color6 , fresnel147);
				float4 lerpResult1145 = lerp( switchResult356 , lerpResult1135 , _Float145);
				float lerpResult347 = lerp( 1.0 , fresnel147 , _Float33);
				float4 texCoord1451 = i.ase_texcoord8;
				texCoord1451.xy = i.ase_texcoord8.xy * float2( 1,1 ) + float2( 0,0 );
				float ifLocalVar1455 = 0;
				if( _Custom7 == 0.0 )
				ifLocalVar1455 = 1.0;
				else
				ifLocalVar1455 = 0.0;
				float ifLocalVar1453 = 0;
				if( _Custom7 == 1.0 )
				ifLocalVar1453 = 1.0;
				else
				ifLocalVar1453 = 0.0;
				float ifLocalVar1452 = 0;
				if( _Custom7 == 2.0 )
				ifLocalVar1452 = 1.0;
				else
				ifLocalVar1452 = 0.0;
				float ifLocalVar1454 = 0;
				if( _Custom7 == 3.0 )
				ifLocalVar1454 = 1.0;
				else
				ifLocalVar1454 = 0.0;
				float custom71466 = ( ( texCoord1451.x * ifLocalVar1455 ) + ( texCoord1451.y * ifLocalVar1453 ) + ( texCoord1451.z * ifLocalVar1452 ) + ( texCoord1451.w * ifLocalVar1454 ) );
				float lerpResult62 = lerp( _Float6 , custom71466 , _Float11);
				float lerpResult913 = lerp( lerpResult62 , ( 1.0 - i.ase_color.a ) , _Float129);
				float2 appendResult501 = (float2(_Vector15.x , _Vector15.y));
				float2 uv_dissolvetex = i.ase_texcoord1.yzw.xy * _dissolvetex_ST.xy + _dissolvetex_ST.zw;
				float2 uv2_dissolvetex = i.ase_texcoord4.xy * _dissolvetex_ST.xy + _dissolvetex_ST.zw;
				float2 lerpResult952 = lerp( uv_dissolvetex , uv2_dissolvetex , uv2930);
				float3 appendResult810 = (float3(1.0 , _Vector15.z , 0.0));
				float3 appendResult811 = (float3(_Vector15.w , 1.0 , 0.0));
				float2 appendResult502 = (float2(_dissolvetex_ST.z , _dissolvetex_ST.w));
				float4 texCoord1391 = i.ase_texcoord4;
				texCoord1391.xy = i.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float ifLocalVar1395 = 0;
				if( _Custom5 == 0.0 )
				ifLocalVar1395 = 1.0;
				else
				ifLocalVar1395 = 0.0;
				float ifLocalVar1393 = 0;
				if( _Custom5 == 1.0 )
				ifLocalVar1393 = 1.0;
				else
				ifLocalVar1393 = 0.0;
				float ifLocalVar1392 = 0;
				if( _Custom5 == 2.0 )
				ifLocalVar1392 = 1.0;
				else
				ifLocalVar1392 = 0.0;
				float ifLocalVar1394 = 0;
				if( _Custom5 == 3.0 )
				ifLocalVar1394 = 1.0;
				else
				ifLocalVar1394 = 0.0;
				float custom51401 = ( ( texCoord1391.x * ifLocalVar1395 ) + ( texCoord1391.y * ifLocalVar1393 ) + ( texCoord1391.z * ifLocalVar1392 ) + ( texCoord1391.w * ifLocalVar1394 ) );
				float4 texCoord1410 = i.ase_texcoord4;
				texCoord1410.xy = i.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float ifLocalVar1414 = 0;
				if( _Custom6 == 0.0 )
				ifLocalVar1414 = 1.0;
				else
				ifLocalVar1414 = 0.0;
				float ifLocalVar1412 = 0;
				if( _Custom6 == 1.0 )
				ifLocalVar1412 = 1.0;
				else
				ifLocalVar1412 = 0.0;
				float ifLocalVar1411 = 0;
				if( _Custom6 == 2.0 )
				ifLocalVar1411 = 1.0;
				else
				ifLocalVar1411 = 0.0;
				float ifLocalVar1413 = 0;
				if( _Custom6 == 3.0 )
				ifLocalVar1413 = 1.0;
				else
				ifLocalVar1413 = 0.0;
				float custom61420 = ( ( texCoord1410.x * ifLocalVar1414 ) + ( texCoord1410.y * ifLocalVar1412 ) + ( texCoord1410.z * ifLocalVar1411 ) + ( texCoord1410.w * ifLocalVar1413 ) );
				float2 appendResult1425 = (float2(custom51401 , custom61420));
				float2 lerpResult1426 = lerp( appendResult502 , appendResult1425 , _Float50);
				float2 CenteredUV15_g99 = ( i.ase_texcoord1.yzw.xy - float2( 0.5,0.5 ) );
				float2 break17_g99 = CenteredUV15_g99;
				float2 appendResult23_g99 = (float2(( length( CenteredUV15_g99 ) * _dissolvetex_ST.x * 2.0 ) , ( atan2( break17_g99.x , break17_g99.y ) * ( 1.0 / 6.28318548202515 ) * _dissolvetex_ST.y )));
				float2 lerpResult1422 = lerp( appendResult502 , appendResult1425 , _Float50);
				float2 lerpResult511 = lerp( ( mul( float3( lerpResult952 ,  0.0 ), float3x3(appendResult810, appendResult811, float3(0,0,1)) ).xy + lerpResult1426 ) , (mul( float3( appendResult23_g99 ,  0.0 ), float3x3(appendResult810, appendResult811, float3(0,0,1)) ).xy*float2( 1,1 ) + lerpResult1422) , _Float53);
				float2 panner58 = ( 1.0 * _Time.y * appendResult501 + lerpResult511);
				float noise_intensity_dis733 = ( _Float79 * 0.1 * lerpResult1575 );
				float4 texCoord1483 = i.ase_texcoord8;
				texCoord1483.xy = i.ase_texcoord8.xy * float2( 1,1 ) + float2( 0,0 );
				float ifLocalVar1479 = 0;
				if( _Custom8 == 0.0 )
				ifLocalVar1479 = 1.0;
				else
				ifLocalVar1479 = 0.0;
				float ifLocalVar1481 = 0;
				if( _Custom8 == 1.0 )
				ifLocalVar1481 = 1.0;
				else
				ifLocalVar1481 = 0.0;
				float ifLocalVar1482 = 0;
				if( _Custom8 == 2.0 )
				ifLocalVar1482 = 1.0;
				else
				ifLocalVar1482 = 0.0;
				float ifLocalVar1480 = 0;
				if( _Custom8 == 3.0 )
				ifLocalVar1480 = 1.0;
				else
				ifLocalVar1480 = 0.0;
				float custom81489 = ( ( texCoord1483.x * ifLocalVar1479 ) + ( texCoord1483.y * ifLocalVar1481 ) + ( texCoord1483.z * ifLocalVar1482 ) + ( texCoord1483.w * ifLocalVar1480 ) );
				float lerpResult307 = lerp( flowmap_intensity311 , custom81489 , flpwmap_custom_switch316);
				float2 lerpResult309 = lerp( ( panner58 + ( noise70 * noise_intensity_dis733 ) ) , flowmap285 , lerpResult307);
				float2 dissolveUV92 = lerpResult309;
				float cos386 = cos( ( _Float41 * UNITY_PI ) );
				float sin386 = sin( ( _Float41 * UNITY_PI ) );
				float2 rotator386 = mul( dissolveUV92 - float2( 0.5,0.5 ) , float2x2( cos386 , -sin386 , sin386 , cos386 )) + float2( 0.5,0.5 );
				float2 break635 = rotator386;
				float clampResult632 = clamp( break635.x , 0.0 , 1.0 );
				float lerpResult784 = lerp( break635.x , clampResult632 , _Float66);
				float clampResult633 = clamp( break635.y , 0.0 , 1.0 );
				float lerpResult786 = lerp( break635.y , clampResult633 , _Float87);
				float2 appendResult631 = (float2(lerpResult784 , lerpResult786));
				float2 uv_TextureSample1 = i.ase_texcoord1.yzw.xy * _TextureSample1_ST.xy + _TextureSample1_ST.zw;
				float2 uv2_TextureSample1 = i.ase_texcoord4.xy * _TextureSample1_ST.xy + _TextureSample1_ST.zw;
				float2 lerpResult955 = lerp( uv_TextureSample1 , uv2_TextureSample1 , uv2930);
				float2 CenteredUV15_g126 = ( i.ase_texcoord1.yzw.xy - float2( 0.5,0.5 ) );
				float2 break17_g126 = CenteredUV15_g126;
				float2 appendResult23_g126 = (float2(( length( CenteredUV15_g126 ) * 1.0 * 2.0 ) , ( atan2( break17_g126.x , break17_g126.y ) * ( 1.0 / 6.28318548202515 ) * 1.0 )));
				float2 lerpResult568 = lerp( lerpResult955 , appendResult23_g126 , _Float53);
				float cos417 = cos( ( _Float47 * UNITY_PI ) );
				float sin417 = sin( ( _Float47 * UNITY_PI ) );
				float2 rotator417 = mul( lerpResult568 - float2( 0.5,0.5 ) , float2x2( cos417 , -sin417 , sin417 , cos417 )) + float2( 0.5,0.5 );
				float3 objToWorld1197 = mul( unity_ObjectToWorld, float4( float3( 0,0,0 ), 1 ) ).xyz;
				float3 appendResult1017 = (float3(_Vector35.x , _Vector35.y , _Vector35.z));
				float3 lerpResult1199 = lerp( ( objToWorld1197 + appendResult1017 ) , appendResult1017 , _Float155);
				float lerpResult1057 = lerp( tex2D( _TextureSample1, rotator417 ).r , distance( lerpResult1199 , WorldPosition ) , _Float139);
				float dis_direction277 = lerpResult1057;
				float lerpResult1060 = lerp( _Float130 , ( 1.0 - _Float130 ) , _Float139);
				float lerpResult916 = lerp( tex2D( _dissolvetex, appendResult631 ).r , dis_direction277 , lerpResult1060);
				float dis_tex661 = lerpResult916;
				float temp_output_130_0 = (0.0 + (dis_tex661 - -0.5) * (1.0 - 0.0) / (1.5 - -0.5));
				float temp_output_105_0 = step( lerpResult913 , temp_output_130_0 );
				float temp_output_109_0 = ( lerpResult913 + ( _Float17 * 0.1 ) );
				float temp_output_1263_0 = ( temp_output_105_0 - step( temp_output_109_0 , temp_output_130_0 ) );
				float dis_edge21241 = ( temp_output_1263_0 - step( ( temp_output_109_0 + ( _Float160 * -0.1 ) ) , temp_output_130_0 ) );
				float4 lerpResult1242 = lerp( _Color7 , _Color1 , dis_edge21241);
				float temp_output_45_0 = saturate( ( ( lerpResult916 + 1.0 ) - ( lerpResult913 * 2.0 ) ) );
				float4 texCoord1501 = i.ase_texcoord8;
				texCoord1501.xy = i.ase_texcoord8.xy * float2( 1,1 ) + float2( 0,0 );
				float ifLocalVar1503 = 0;
				if( _Custom9 == 0.0 )
				ifLocalVar1503 = 1.0;
				else
				ifLocalVar1503 = 0.0;
				float ifLocalVar1505 = 0;
				if( _Custom9 == 1.0 )
				ifLocalVar1505 = 1.0;
				else
				ifLocalVar1505 = 0.0;
				float ifLocalVar1504 = 0;
				if( _Custom9 == 2.0 )
				ifLocalVar1504 = 1.0;
				else
				ifLocalVar1504 = 0.0;
				float ifLocalVar1502 = 0;
				if( _Custom9 == 3.0 )
				ifLocalVar1502 = 1.0;
				else
				ifLocalVar1502 = 0.0;
				float custom91511 = ( ( texCoord1501.x * ifLocalVar1503 ) + ( texCoord1501.y * ifLocalVar1505 ) + ( texCoord1501.z * ifLocalVar1504 ) + ( texCoord1501.w * ifLocalVar1502 ) );
				float lerpResult1090 = lerp( _Float8 , custom91511 , _Float142);
				float smoothstepResult32 = smoothstep( lerpResult1090 , ( 1.0 - lerpResult1090 ) , temp_output_45_0);
				float dis_soft_edge1271 = saturate( ( temp_output_45_0 - smoothstepResult32 ) );
				float dis_edge133 = temp_output_1263_0;
				float lerpResult1272 = lerp( dis_soft_edge1271 , dis_edge133 , _Float25);
				float4 lerpResult131 = lerp( ( ( ( _Float28 * pow( lerpResult330 , _Float30 ) ) + lerpResult600 ) * i.ase_color * lerpResult1145 * lerpResult347 ) , lerpResult1242 , lerpResult1272);
				float4 ase_grabScreenPos = ASE_ComputeGrabScreenPos( screenPos );
				float4 ase_grabScreenPosNorm = ase_grabScreenPos / ase_grabScreenPos.w;
				float2 appendResult580 = (float2(_Vector21.x , _Vector21.y));
				float2 uv_reftex = i.ase_texcoord1.yzw.xy * _reftex_ST.xy + _reftex_ST.zw;
				float3 appendResult906 = (float3(1.0 , _Vector21.z , 0.0));
				float3 appendResult908 = (float3(_Vector21.w , 1.0 , 0.0));
				float2 appendResult579 = (float2(_reftex_ST.z , _reftex_ST.w));
				float2 CenteredUV15_g127 = ( i.ase_texcoord1.yzw.xy - float2( 0.5,0.5 ) );
				float2 break17_g127 = CenteredUV15_g127;
				float2 appendResult23_g127 = (float2(( length( CenteredUV15_g127 ) * _reftex_ST.x * 2.0 ) , ( atan2( break17_g127.x , break17_g127.y ) * ( 1.0 / 6.28318548202515 ) * _reftex_ST.y )));
				float2 lerpResult585 = lerp( ( mul( float3( uv_reftex ,  0.0 ), float3x3(appendResult906, appendResult908, float3(0,0,1)) ).xy + appendResult579 ) , (mul( float3( appendResult23_g127 ,  0.0 ), float3x3(appendResult906, appendResult908, float3(0,0,1)) ).xy*float2( 1,1 ) + appendResult579) , _Float58);
				float2 panner188 = ( 1.0 * _Time.y * appendResult580 + lerpResult585);
				float cos589 = cos( ( _Float59 * UNITY_PI ) );
				float sin589 = sin( ( _Float59 * UNITY_PI ) );
				float2 rotator589 = mul( panner188 - float2( 0.5,0.5 ) , float2x2( cos589 , -sin589 , sin589 , cos589 )) + float2( 0.5,0.5 );
				float2 break650 = rotator589;
				float clampResult652 = clamp( break650.x , 0.0 , 1.0 );
				float lerpResult781 = lerp( break650.x , clampResult652 , _Float69);
				float clampResult651 = clamp( break650.y , 0.0 , 1.0 );
				float lerpResult782 = lerp( break650.y , clampResult651 , _Float86);
				float2 appendResult653 = (float2(lerpResult781 , lerpResult782));
				float4 texCoord1523 = i.ase_texcoord8;
				texCoord1523.xy = i.ase_texcoord8.xy * float2( 1,1 ) + float2( 0,0 );
				float ifLocalVar1522 = 0;
				if( _Custom10 == 0.0 )
				ifLocalVar1522 = 1.0;
				else
				ifLocalVar1522 = 0.0;
				float ifLocalVar1524 = 0;
				if( _Custom10 == 1.0 )
				ifLocalVar1524 = 1.0;
				else
				ifLocalVar1524 = 0.0;
				float ifLocalVar1525 = 0;
				if( _Custom10 == 2.0 )
				ifLocalVar1525 = 1.0;
				else
				ifLocalVar1525 = 0.0;
				float ifLocalVar1521 = 0;
				if( _Custom10 == 3.0 )
				ifLocalVar1521 = 1.0;
				else
				ifLocalVar1521 = 0.0;
				float custom101531 = ( ( texCoord1523.x * ifLocalVar1522 ) + ( texCoord1523.y * ifLocalVar1524 ) + ( texCoord1523.z * ifLocalVar1525 ) + ( texCoord1523.w * ifLocalVar1521 ) );
				float lerpResult193 = lerp( _Float23 , custom101531 , _Float24);
				float4 screenColor183 = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_GrabTexture,( ase_grabScreenPosNorm + float4( UnpackScaleNormal( tex2D( _reftex, appendResult653 ), ( 0.01 * lerpResult193 ) ) , 0.0 ) ).xy);
				float4 ref196 = screenColor183;
				float4 lerpResult422 = lerp( lerpResult131 , ( ref196 * _Color2 * i.ase_color ) , _Float48);
				float3 worldSpaceLightDir = UnityWorldSpaceLightDir(WorldPosition);
				float2 appendResult1178 = (float2(_Vector10.x , _Vector10.y));
				float3 appendResult1172 = (float3(1.0 , _Vector10.z , 0.0));
				float3 appendResult1173 = (float3(_Vector10.w , 1.0 , 0.0));
				float2 uv_normallight = i.ase_texcoord1.yzw.xy * _normallight_ST.xy + _normallight_ST.zw;
				float2 panner1177 = ( 1.0 * _Time.y * appendResult1178 + mul( float3x3(appendResult1172, appendResult1173, float3(0,0,1)), float3( uv_normallight ,  0.0 ) ).xy);
				float3 tex2DNode1147 = UnpackScaleNormal( tex2D( _normallight, panner1177 ), _Float146 );
				float3 tanNormal957 = tex2DNode1147;
				float3 worldNormal957 = normalize( float3(dot(tanToWorld0,tanNormal957), dot(tanToWorld1,tanNormal957), dot(tanToWorld2,tanNormal957)) );
				float dotResult960 = dot( worldSpaceLightDir , worldNormal957 );
				float3 appendResult996 = (float3(_Vector34.x , _Vector34.y , _Vector34.z));
				float pointlight1007 = distance( appendResult996 , WorldPosition );
				float lerpResult1008 = lerp( dotResult960 , pointlight1007 , _Float137);
				float smoothstepResult968 = smoothstep( ( 1.0 - _Float133 ) , _Float133 , ( ( lerpResult1008 + 1.0 ) - ( _Float132 * 2.0 ) ));
				float lit971 = smoothstepResult968;
				float4 lerpResult969 = lerp( ( _Color4 * lerpResult422 ) , ( _Color5 * lerpResult422 ) , lit971);
				float4 lerpResult977 = lerp( lerpResult422 , lerpResult969 , _Float134);
				float4 temp_cast_34 = (1.0).xxxx;
				float3 normal_1111598 = tex2DNode1147;
				float3 tanNormal1064 = normal_1111598;
				float3 worldNormal1064 = float3(dot(tanToWorld0,tanNormal1064), dot(tanToWorld1,tanNormal1064), dot(tanToWorld2,tanNormal1064));
				float3 desaturateInitialColor1210 = tex2D( _matcap, ( ( (mul( UNITY_MATRIX_V, float4( worldNormal1064 , 0.0 ) ).xyz).xy + float2( 1,1 ) ) * float2( 0.5,0.5 ) ) ).rgb;
				float desaturateDot1210 = dot( desaturateInitialColor1210, float3( 0.299, 0.587, 0.114 ));
				float3 desaturateVar1210 = lerp( desaturateInitialColor1210, desaturateDot1210.xxx, _Float157 );
				float3 tanNormal1591 = normal_1111598;
				float3 worldNormal1591 = float3(dot(tanToWorld0,tanNormal1591), dot(tanToWorld1,tanNormal1591), dot(tanToWorld2,tanNormal1591));
				float4 cubemap1593 = ( texCUBE( _cubemap, reflect( -ase_worldViewDir , worldNormal1591 ) ) * _Float258 );
				float4 matcap1074 = ( float4( ( desaturateVar1210 * _Float140 ) , 0.0 ) + cubemap1593 );
				float4 lerpResult1078 = lerp( temp_cast_34 , matcap1074 , _Float141);
				float lerpResult402 = lerp( tex2DNode1.a , tex2DNode1.r , _maintex_alpha);
				float lerpResult374 = lerp( lerpResult402 , ( pow( abs( lerpResult402 ) , _Float34 ) * _Float37 ) , _Float36);
				float dis_soft122 = smoothstepResult32;
				float dis_bright124 = temp_output_105_0;
				float lerpResult413 = lerp( dis_soft122 , dis_bright124 , _Float25);
				float lerpResult338 = lerp( depthfade126 , 1.0 , depthfade_switch334);
				float2 appendResult480 = (float2(_Vector11.x , _Vector11.y));
				float2 uv_Mask = i.ase_texcoord1.yzw.xy * _Mask_ST.xy + _Mask_ST.zw;
				float2 uv2_Mask = i.ase_texcoord4.xy * _Mask_ST.xy + _Mask_ST.zw;
				float2 lerpResult931 = lerp( uv_Mask , uv2_Mask , uv2930);
				float3 appendResult823 = (float3(1.0 , _Vector11.z , 0.0));
				float3 appendResult824 = (float3(_Vector11.w , 1.0 , 0.0));
				float2 appendResult481 = (float2(_Mask_ST.z , _Mask_ST.w));
				float4 texCoord1353 = i.ase_texcoord4;
				texCoord1353.xy = i.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float ifLocalVar1352 = 0;
				if( _Custom3 == 0.0 )
				ifLocalVar1352 = 1.0;
				else
				ifLocalVar1352 = 0.0;
				float ifLocalVar1349 = 0;
				if( _Custom3 == 1.0 )
				ifLocalVar1349 = 1.0;
				else
				ifLocalVar1349 = 0.0;
				float ifLocalVar1350 = 0;
				if( _Custom3 == 2.0 )
				ifLocalVar1350 = 1.0;
				else
				ifLocalVar1350 = 0.0;
				float ifLocalVar1351 = 0;
				if( _Custom3 == 3.0 )
				ifLocalVar1351 = 1.0;
				else
				ifLocalVar1351 = 0.0;
				float custom31359 = ( ( texCoord1353.x * ifLocalVar1352 ) + ( texCoord1353.y * ifLocalVar1349 ) + ( texCoord1353.z * ifLocalVar1350 ) + ( texCoord1353.w * ifLocalVar1351 ) );
				float4 texCoord1373 = i.ase_texcoord4;
				texCoord1373.xy = i.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float ifLocalVar1372 = 0;
				if( _Custom4 == 0.0 )
				ifLocalVar1372 = 1.0;
				else
				ifLocalVar1372 = 0.0;
				float ifLocalVar1369 = 0;
				if( _Custom4 == 1.0 )
				ifLocalVar1369 = 1.0;
				else
				ifLocalVar1369 = 0.0;
				float ifLocalVar1370 = 0;
				if( _Custom4 == 2.0 )
				ifLocalVar1370 = 1.0;
				else
				ifLocalVar1370 = 0.0;
				float ifLocalVar1371 = 0;
				if( _Custom4 == 3.0 )
				ifLocalVar1371 = 1.0;
				else
				ifLocalVar1371 = 0.0;
				float custom41379 = ( ( texCoord1373.x * ifLocalVar1372 ) + ( texCoord1373.y * ifLocalVar1369 ) + ( texCoord1373.z * ifLocalVar1370 ) + ( texCoord1373.w * ifLocalVar1371 ) );
				float2 appendResult75 = (float2(custom31359 , custom41379));
				float2 lerpResult474 = lerp( appendResult481 , appendResult75 , _Float12);
				float2 CenteredUV15_g135 = ( i.ase_texcoord1.yzw.xy - float2( 0.5,0.5 ) );
				float2 break17_g135 = CenteredUV15_g135;
				float2 appendResult23_g135 = (float2(( length( CenteredUV15_g135 ) * _Mask_ST.x * 2.0 ) , ( atan2( break17_g135.x , break17_g135.y ) * ( 1.0 / 6.28318548202515 ) * _Mask_ST.y )));
				float2 lerpResult471 = lerp( appendResult481 , appendResult75 , _Float12);
				float2 lerpResult467 = lerp( ( mul( float3( lerpResult931 ,  0.0 ), float3x3(appendResult823, appendResult824, float3(0,0,1)) ).xy + lerpResult474 ) , (mul( float3( appendResult23_g135 ,  0.0 ), float3x3(appendResult823, appendResult824, float3(0,0,1)) ).xy*float2( 1,1 ) + lerpResult471) , _Float51);
				float2 panner79 = ( 1.0 * _Time.y * appendResult480 + lerpResult467);
				float noise_intensity_mask736 = ( _Float80 * 0.1 * lerpResult1575 );
				float temp_output_322_0 = ( noise70 * noise_intensity_mask736 );
				float cos392 = cos( ( _Float43 * UNITY_PI ) );
				float sin392 = sin( ( _Float43 * UNITY_PI ) );
				float2 rotator392 = mul( ( panner79 + temp_output_322_0 ) - float2( 0.5,0.5 ) , float2x2( cos392 , -sin392 , sin392 , cos392 )) + float2( 0.5,0.5 );
				float2 break617 = rotator392;
				float clampResult618 = clamp( break617.x , 0.0 , 1.0 );
				float lerpResult769 = lerp( break617.x , clampResult618 , _Float64);
				float clampResult619 = clamp( break617.y , 0.0 , 1.0 );
				float lerpResult771 = lerp( break617.y , clampResult619 , _Float82);
				float2 appendResult616 = (float2(lerpResult769 , lerpResult771));
				float4 tex2DNode8 = tex2D( _Mask, appendResult616 );
				float lerpResult406 = lerp( tex2DNode8.a , tex2DNode8.r , _mask01_alpha);
				float2 appendResult485 = (float2(_Vector13.x , _Vector13.y));
				float2 uv_Mask1 = i.ase_texcoord1.yzw.xy * _Mask1_ST.xy + _Mask1_ST.zw;
				float2 uv2_Mask1 = i.ase_texcoord4.xy * _Mask1_ST.xy + _Mask1_ST.zw;
				float2 lerpResult934 = lerp( uv_Mask1 , uv2_Mask1 , uv2930);
				float3 appendResult833 = (float3(1.0 , _Vector13.z , 0.0));
				float3 appendResult834 = (float3(_Vector13.w , 1.0 , 0.0));
				float2 appendResult486 = (float2(_Mask1_ST.z , _Mask1_ST.w));
				float2 CenteredUV15_g136 = ( i.ase_texcoord1.yzw.xy - float2( 0.5,0.5 ) );
				float2 break17_g136 = CenteredUV15_g136;
				float2 appendResult23_g136 = (float2(( length( CenteredUV15_g136 ) * _Mask1_ST.x * 2.0 ) , ( atan2( break17_g136.x , break17_g136.y ) * ( 1.0 / 6.28318548202515 ) * _Mask1_ST.y )));
				float2 lerpResult495 = lerp( ( mul( float3( lerpResult934 ,  0.0 ), float3x3(appendResult833, appendResult834, float3(0,0,1)) ).xy + appendResult486 ) , (mul( float3( appendResult23_g136 ,  0.0 ), float3x3(appendResult833, appendResult834, float3(0,0,1)) ).xy*float2( 1,1 ) + appendResult486) , _Float52);
				float2 panner216 = ( 1.0 * _Time.y * appendResult485 + lerpResult495);
				float cos389 = cos( ( _Float42 * UNITY_PI ) );
				float sin389 = sin( ( _Float42 * UNITY_PI ) );
				float2 rotator389 = mul( ( temp_output_322_0 + panner216 ) - float2( 0.5,0.5 ) , float2x2( cos389 , -sin389 , sin389 , cos389 )) + float2( 0.5,0.5 );
				float2 break623 = rotator389;
				float clampResult624 = clamp( break623.x , 0.0 , 1.0 );
				float lerpResult772 = lerp( break623.x , clampResult624 , _Float65);
				float clampResult625 = clamp( break623.y , 0.0 , 1.0 );
				float lerpResult773 = lerp( break623.y , clampResult625 , _Float83);
				float2 appendResult622 = (float2(lerpResult772 , lerpResult773));
				float4 tex2DNode218 = tex2D( _Mask1, appendResult622 );
				float lerpResult407 = lerp( tex2DNode218.a , tex2DNode218.r , _mask02_alpha);
				float Mask82 = ( lerpResult406 * lerpResult407 );
				float temp_output_6_0 = ( lerpResult374 * i.ase_color.a * _Color0.a * lerpResult413 * _Float15 * lerpResult338 * Mask82 * lerpResult347 );
				float clampResult602 = clamp( temp_output_6_0 , 0.0 , 1.0 );
				float lerpResult656 = lerp( temp_output_6_0 , clampResult602 , _Float70);
				clip( dis_tex661 - _Float72);
				float4 appendResult7 = (float4(( ( lerpResult977 * lerpResult1078 ) * _Float14 ).rgb , lerpResult656));
				
				
				finalColor = appendResult7;
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "LWGUI.LWGUI"
	
	
}
/*ASEBEGIN
Version=18900
1640;64;1354;859;3732.651;3252.414;1;True;False
Node;AmplifyShaderEditor.CommentaryNode;944;-7554,-2866;Inherit;False;3602;2249;扰动;91;701;70;529;531;530;684;863;864;861;862;867;865;866;535;676;868;869;682;679;880;536;540;538;870;683;393;694;539;685;696;394;53;688;687;695;697;395;698;638;640;779;643;639;689;690;691;778;780;777;692;776;775;641;50;693;924;923;564;703;700;731;567;732;733;55;360;734;67;735;736;680;940;873;872;871;874;876;878;875;877;528;879;938;939;941;942;51;943;1575;1576;1577;扰动;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;929;-8118.507,-2083.851;Inherit;False;Property;_Float131;启用2u(需关闭customdata);9;1;[Toggle];Create;False;0;0;1;UnityEngine.Rendering.CullMode;True;1;SubToggle(g17,_);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;529;-7440,-848;Inherit;False;Property;_Vector17;扰动流动&斜切;101;0;Create;False;0;0;0;False;1;Sub(g4);False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;930;-7818.894,-2089.837;Inherit;False;uv2;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;872;-7504,-1312;Inherit;False;Constant;_Float114;Float 114;138;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;874;-7504,-1136;Inherit;False;Constant;_Float116;Float 116;138;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;871;-7504,-1360;Inherit;False;Constant;_Float113;Float 113;138;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;873;-7488,-1232;Inherit;False;Constant;_Float115;Float 115;138;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;943;-7459.56,-1496.888;Inherit;False;1;50;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;862;-7456,-2320;Inherit;False;Constant;_Float110;Float 110;138;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;684;-7440,-1792;Inherit;False;Property;_Vector23;扰动遮罩流动&斜切;109;0;Create;False;0;0;0;False;1;Sub(g4);False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;51;-7456.003,-1628.444;Inherit;False;0;50;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;942;-7264,-1424;Inherit;False;930;uv2;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;861;-7488,-2224;Inherit;False;Constant;_Float109;Float 109;138;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;864;-7472,-2400;Inherit;False;Constant;_Float112;Float 112;138;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;876;-7248,-1344;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.Vector4Node;528;-7212.879,-1018.36;Inherit;False;Property;_noise_ST;_noise_ST;100;1;[HideInInspector];Create;True;0;0;0;False;0;False;1,1,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;863;-7488,-2448;Inherit;False;Constant;_Float111;Float 111;138;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;878;-7232,-1232;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.Vector3Node;875;-7504,-1056;Inherit;False;Constant;_Vector27;Vector 27;138;0;Create;True;0;0;0;False;0;False;0,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.TextureCoordinatesNode;680;-7447,-2820;Inherit;False;0;564;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;676;-7440,-1984;Inherit;False;Property;_noisemask_ST;_noisemask_ST;108;1;[HideInInspector];Create;True;0;0;0;False;0;False;1,1,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;939;-7360,-2530;Inherit;False;930;uv2;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.MatrixFromVectors;877;-7056,-1264;Inherit;False;FLOAT3x3;True;4;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3x3;0
Node;AmplifyShaderEditor.FunctionNode;535;-6912,-1632;Inherit;False;Polar Coordinates;-1;;46;7dab8e02884cf104ebefaa2e788e4162;0;4;1;FLOAT2;0,0;False;2;FLOAT2;0.5,0.5;False;3;FLOAT;1;False;4;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;940;-7447,-2676;Inherit;False;1;564;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector3Node;867;-7488,-2144;Inherit;False;Constant;_Vector26;Vector 26;138;0;Create;True;0;0;0;False;0;False;0,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DynamicAppendNode;866;-7232,-2432;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DynamicAppendNode;865;-7200,-2320;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;941;-7072,-1536;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;880;-6656,-1600;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT3x3;0,0,0,1,1,1,1,0,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;531;-6955.771,-886.0676;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.MatrixFromVectors;868;-7008,-2336;Inherit;False;FLOAT3x3;True;4;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3x3;0
Node;AmplifyShaderEditor.FunctionNode;682;-6838.7,-2652.4;Inherit;False;Polar Coordinates;-1;;47;7dab8e02884cf104ebefaa2e788e4162;0;4;1;FLOAT2;0,0;False;2;FLOAT2;0.5,0.5;False;3;FLOAT;1;False;4;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;879;-6818.776,-1301.555;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT3x3;0,0,0,1,1,1,1,0,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;938;-7138,-2672;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;869;-6736,-2336;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT3x3;0,0,0,1,1,1,1,0,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;679;-6928,-1920;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;870;-6640,-2496;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT3x3;0,0,0,1,1,1,1,0,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;536;-6504.002,-1288.443;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;540;-6592,-954;Inherit;False;Property;_Float54;扰动极坐标（竖向贴图）;98;0;Create;False;0;0;0;False;1;SubToggle(g4, _);False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;538;-6464,-1616;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;1,1;False;2;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;685;-6608,-1920;Inherit;False;Property;_Float57;扰动遮罩极坐标;106;0;Create;False;0;0;0;False;1;SubToggle(g4, _);False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;683;-6562.586,-2156.872;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;694;-6480,-2512;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;1,1;False;2;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;539;-6144,-1456;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;530;-6976,-752;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;393;-6320,-1136;Inherit;False;Property;_Float44;扰动贴图旋转;99;0;Create;False;0;0;0;False;1;Sub(g4);False;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;696;-6336,-2032;Inherit;False;Property;_Float74;扰动遮罩旋转;107;0;Create;False;0;0;0;False;1;Sub(g4);False;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;688;-6176,-2352;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;687;-6896,-1824;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PiNode;394;-6048,-1248;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;53;-5856,-1424;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RotatorNode;395;-5648,-1392;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;697;-5872,-2320;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PiNode;695;-6064,-2144;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1408;-673.419,9816.222;Inherit;False;Constant;_Float207;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1383;-535.8872,7393.259;Inherit;False;Constant;_Float194;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1285;-6006.145,7691.355;Inherit;False;Constant;_Float161;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1404;-661.658,9220.209;Inherit;False;Constant;_Float203;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1382;-523.3252,7296.961;Inherit;False;Constant;_Float193;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1556;7787.61,9803.365;Inherit;False;Constant;_Float250;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;638;-6016,-1104;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.RangedFloatNode;1326;-5919.731,9063.288;Inherit;False;Constant;_Float172;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1389;-582.1931,8298.297;Inherit;False;Constant;_Float199;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1384;-984.6282,7996.692;Inherit;False;Property;_Custom5;溶解x轴;178;1;[Enum];Create;False;0;4;x1;0;y1;1;z1;2;w1;3;0;True;1;KWEnum(g7,custom1x,_0,custom1y,_1,custom1z,_2,custom1w,_3);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1388;-520.269,7929.422;Inherit;False;Constant;_Float198;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1289;-6042.228,7913.844;Inherit;False;Constant;_Float163;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1295;-5988.989,8314.056;Inherit;False;Constant;_Float166;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1405;-609.9571,9533.884;Inherit;False;Constant;_Float204;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1402;-614.5511,8814.886;Inherit;False;Constant;_Float201;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1323;-5916.675,9695.75;Inherit;False;Constant;_Float170;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1325;-5978.6,10064.62;Inherit;False;Constant;_Float171;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1406;-671.8809,9902.759;Inherit;False;Constant;_Float205;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1294;-5990.527,8227.519;Inherit;False;Constant;_Float165;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1329;-5966.838,9468.611;Inherit;False;Constant;_Float175;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1284;-6454.886,8294.788;Inherit;False;Property;_Custom1;主贴图x轴;172;1;[Enum];Create;False;0;4;x1;0;y1;1;z1;2;w1;3;0;True;1;KWEnum(g7,custom1x,_0,custom1y,_1,custom1z,_2,custom1w,_3);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1328;-5968.376,9382.075;Inherit;False;Constant;_Float174;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1421;-1075.854,9514.617;Inherit;False;Property;_Custom6;溶解y轴;179;1;[Enum];Create;False;0;4;x1;0;y1;1;z1;2;w1;3;0;True;1;KWEnum(g7,custom1x,_0,custom1y,_1,custom1z,_2,custom1w,_3);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1386;-518.7312,8015.959;Inherit;False;Constant;_Float196;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1387;-580.655,8384.834;Inherit;False;Constant;_Float197;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1290;-6040.69,8000.381;Inherit;False;Constant;_Float164;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1407;-611.4949,9447.347;Inherit;False;Constant;_Float206;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1298;-6052.451,8596.393;Inherit;False;Constant;_Float168;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;698;-5664,-2288;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;1296;-6050.913,8682.93;Inherit;False;Constant;_Float167;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1403;-627.1131,8911.184;Inherit;False;Constant;_Float202;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1331;-5915.137,9782.287;Inherit;False;Constant;_Float176;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1319;-5932.293,9159.586;Inherit;False;Constant;_Float169;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1390;-571.9702,7615.748;Inherit;False;Constant;_Float200;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1327;-5977.062,10151.16;Inherit;False;Constant;_Float173;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1286;-5993.583,7595.057;Inherit;False;Constant;_Float162;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1562;7734.372,9403.154;Inherit;False;Constant;_Float256;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1409;-663.1961,9133.673;Inherit;False;Constant;_Float208;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1385;-570.4321,7702.284;Inherit;False;Constant;_Float195;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1555;7770.454,9180.664;Inherit;False;Constant;_Float249;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1557;7783.016,9084.367;Inherit;False;Constant;_Float251;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1558;7786.071,9716.828;Inherit;False;Constant;_Float252;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1559;7724.148,10085.7;Inherit;False;Constant;_Float253;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1560;7735.909,9489.689;Inherit;False;Constant;_Float254;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1563;7321.712,9784.098;Inherit;False;Property;_Custom12;自定义扰动强度;188;1;[Enum];Create;False;0;4;x2;0;y2;1;z2;2;w2;3;0;True;1;KWEnum(g7,custom2x,_0,custom2y,_1,custom2z,_2,custom2w,_3);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1335;-6381.035,9763.02;Inherit;False;Property;_Custom2;主贴图y轴;173;1;[Enum];Create;False;0;4;x1;0;y1;1;z1;2;w1;3;0;True;1;KWEnum(g7,custom1x,_0,custom1y,_1,custom1z,_2,custom1w,_3);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1561;7725.688,10172.24;Inherit;False;Constant;_Float255;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;643;-5568,-1504;Inherit;False;Property;_Float67;扰动贴图x轴Clamp;96;0;Create;False;0;0;0;True;1;SubToggle(g4, _);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;1283;-5805.812,7506.409;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;1394;-372.7312,8264.961;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;3;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;1324;-5731.96,8974.641;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;1281;-6820.94,7590.453;Inherit;False;1;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ConditionalIfNode;1395;-335.5542,7208.313;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;1392;-310.8071,7896.087;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;2;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;1410;-1441.908,8810.281;Inherit;False;1;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ConditionalIfNode;1413;-463.9571,9782.886;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;3;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;639;-5776,-1024;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;1293;-5781.065,8194.184;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;2;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;1299;-5842.989,8563.058;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;3;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;1288;-5832.766,7880.509;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;779;-5584,-928;Inherit;False;Property;_Float85;扰动贴图y轴Clamp;97;0;Create;False;0;0;0;True;1;SubToggle(g4, _);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;1317;-5758.915,9348.74;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;1334;-5769.137,10031.29;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;3;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;1412;-453.734,9100.337;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;1564;7943.833,9369.816;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;689;-6032,-2000;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.ConditionalIfNode;1414;-426.7802,8726.238;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;1330;-6747.088,9058.685;Inherit;False;1;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ConditionalIfNode;1567;7970.787,8995.719;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;1568;7933.61,10052.37;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;3;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;1333;-5707.213,9662.414;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;2;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;1566;6955.659,9079.764;Inherit;False;2;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ConditionalIfNode;1393;-362.5081,7582.412;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;1411;-402.0331,9414.012;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;2;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;640;-5744,-1184;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;1565;7995.534,9683.493;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;2;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;1391;-1350.682,7292.357;Inherit;False;1;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1416;-106.6471,9297.156;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;777;-5808,-1680;Inherit;False;Property;_Float84;扰动遮罩y轴Clamp;105;0;Create;False;0;0;0;True;1;SubToggle(g4, _);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1292;-5485.679,8077.327;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1417;-155.5502,8623.209;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1399;-77.34619,8148.105;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1282;-5534.582,7403.381;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1297;-5547.604,8446.201;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;690;-5728,-1984;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1321;-5445.455,9227.712;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1569;8228.994,9935.512;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1322;-5411.828,9545.559;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1396;-49.0481,7461.385;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;817;-3436.151,-5587.112;Inherit;False;2519.616;1473.895;主贴图;28;439;802;801;798;796;803;799;804;431;39;42;433;795;60;793;59;443;43;806;449;446;445;444;440;36;161;1338;1339;主贴图;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1291;-5519.306,7759.481;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;692;-5760,-2064;Inherit;False;Property;_Float73;扰动遮罩x轴Clamp;104;0;Create;False;0;0;0;True;1;SubToggle(g4, _);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1398;-64.32422,7105.284;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;780;-5392,-1040;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1415;-140.274,8979.31;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1571;8290.92,9566.637;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1418;-168.5721,9666.029;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1572;8242.018,8892.689;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1570;8257.293,9248.79;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;691;-5776,-1808;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1397;-15.42114,7779.231;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1320;-5460.73,8871.612;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;778;-5472,-1280;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1332;-5473.752,9914.432;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;641;-5296,-1328;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;1573;8619.533,9441.76;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;431;-2906.728,-4874.591;Inherit;False;Property;_maintex_ST;主贴图tilling&offset;39;1;[HideInInspector];Create;False;0;0;0;False;0;False;1,1,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;776;-5504,-1760;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;1400;313.1929,7654.354;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;1287;-5157.065,7952.45;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;1128;-2938.528,-6078.787;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;1419;221.967,9172.279;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;1318;-5050.476,9387.942;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;512;-7099.938,4466.086;Inherit;False;2803.434;1805.293;Comment;42;499;502;57;506;509;510;507;500;511;501;58;92;314;317;302;304;306;307;309;303;738;737;807;808;809;810;811;812;813;814;815;816;951;952;953;1422;1423;1424;1425;1426;1427;1490;溶解uv;1,1,1,1;0;0
Node;AmplifyShaderEditor.LerpOp;775;-5456,-2112;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;798;-3227.035,-5274.781;Inherit;False;Constant;_Float91;Float 91;138;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;439;-3316.591,-4580.434;Inherit;False;Property;_Vector0;主贴图流动&斜切;44;0;Create;False;0;0;0;False;1;Sub(g1);False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;807;-7004.14,5196.008;Inherit;False;Constant;_Float95;Float 95;138;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;1132;-2686.065,-6064.699;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;802;-3239.448,-5134.948;Inherit;False;Constant;_Float94;Float 94;138;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;693;-5312,-1936;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;809;-7006.597,5116.734;Inherit;False;Constant;_Float96;Float 96;138;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1337;-4874.448,9371.005;Inherit;False;custom2;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1401;504.3208,7635.698;Inherit;False;custom5;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;814;-7000.918,4956.575;Inherit;False;Constant;_Float90;Float 90;138;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;1130;-2724.223,-5692.827;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;801;-3269.093,-5029.263;Inherit;False;Constant;_Float93;Float 93;138;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;500;-6988.023,5728.638;Inherit;False;Property;_Vector15;溶解流动&斜切;79;0;Create;False;0;0;0;False;1;Sub(g3);False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;924;-5072,-1456;Inherit;False;Property;_Vector33;扰动贴图remap;110;0;Create;False;0;0;0;False;1;Sub(g4);False;0,1,0,1;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;1420;413.0948,9153.623;Inherit;False;custom6;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;50;-5184,-1648;Inherit;True;Property;_noise;扰动贴图;95;0;Create;False;0;0;0;False;1;Sub(g4);False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;1574;8810.662,9423.104;Inherit;False;custom12;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;796;-3201.203,-5412.148;Inherit;False;Constant;_Float13;Float 13;138;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;808;-7005.3,5037.587;Inherit;False;Constant;_Float92;Float 92;138;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;1131;-2821.891,-5866.04;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1336;-4965.937,7933.795;Inherit;False;custom1;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;811;-6778.965,5067.933;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;57;-7049.938,4545.794;Inherit;False;0;23;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;1474;3108.378,9680.79;Inherit;False;Constant;_Float219;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1473;3056.677,9367.115;Inherit;False;Constant;_Float218;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;953;-7055.457,4686.631;Inherit;False;1;23;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCRemapNode;923;-4688,-1648;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1424;-6498.086,5258.427;Inherit;False;1420;custom6;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;499;-7001.597,5451.402;Inherit;False;Property;_dissolvetex_ST;_dissolvetex_ST;78;1;[HideInInspector];Create;True;0;0;0;False;0;False;1,1,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;1476;3106.84,9594.254;Inherit;False;Constant;_Float221;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;564;-5136,-1984;Inherit;True;Property;_noisemask;扰动遮罩;103;0;Create;False;0;0;0;False;1;Sub(g4);False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture1D;8;0;SAMPLER1D;;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;1477;3044.916,9963.129;Inherit;False;Constant;_Float222;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;810;-6774.274,4950.791;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;35;-3018.212,-6276.304;Inherit;False;0;1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;1472;3103.784,8961.793;Inherit;False;Constant;_Float217;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1475;3046.454,10049.67;Inherit;False;Constant;_Float220;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1576;-4902.91,-1110.768;Inherit;False;1574;custom12;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;951;-6823.457,4823.631;Inherit;False;930;uv2;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1470;2642.481,9661.523;Inherit;False;Property;_Custom8;自定义flowmap扭曲;186;1;[Enum];Create;False;0;4;x2;0;y2;1;z2;2;w2;3;0;True;1;KWEnum(g7,custom2x,_0,custom2y,_1,custom2z,_2,custom2w,_3);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;812;-7004.092,5281.997;Inherit;False;Constant;_Vector2;Vector 2;138;0;Create;True;0;0;0;False;0;False;0,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;1577;-4822.536,-945.9539;Inherit;False;Property;_Float257;custom控制扰动总强度;187;0;Create;False;0;0;0;True;3;SubToggle(g7, _);Space;Space;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;1133;-2516.062,-5977.973;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;1,0;False;2;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;799;-2969.437,-5363.44;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;1338;-2738.875,-4436.459;Inherit;False;1336;custom1;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1339;-2727.703,-4343.889;Inherit;False;1337;custom2;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1134;-2401.689,-5860.368;Inherit;False;Property;_Float144;使用屏幕uv;32;0;Create;False;0;0;0;False;1;SubToggle(g1, _);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;803;-2980.828,-5171.457;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;1471;3055.139,9280.579;Inherit;False;Constant;_Float216;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1478;3091.222,9058.09;Inherit;False;Constant;_Float224;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;804;-3263.981,-4906.542;Inherit;False;Constant;_Vector1;Vector 1;138;0;Create;True;0;0;0;False;0;False;0,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.GetLocalVarNode;1423;-6490.43,5183.409;Inherit;False;1401;custom5;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.MatrixFromVectors;813;-6592.797,5026.306;Inherit;False;FLOAT3x3;True;4;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3x3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;567;-4448,-1728;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;506;-6419.332,4526.875;Inherit;False;Polar Coordinates;-1;;99;7dab8e02884cf104ebefaa2e788e4162;0;4;1;FLOAT2;0,0;False;2;FLOAT2;0.5,0.5;False;3;FLOAT;1;False;4;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;1127;-2142.009,-5946.699;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FunctionNode;443;-2416.695,-5506.128;Inherit;False;Polar Coordinates;-1;;98;7dab8e02884cf104ebefaa2e788e4162;0;4;1;FLOAT2;0,0;False;2;FLOAT2;0.5,0.5;False;3;FLOAT;1;False;4;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ConditionalIfNode;1480;3254.378,9929.793;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;3;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;433;-2453.469,-4780.213;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;1425;-6295.405,5219.151;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;703;-4797,-1357;Inherit;False;Property;_Float76;扰动遮罩/双重扰动（add为双重扰动）;102;1;[Enum];Create;False;0;2;multiply;0;add;1;0;True;1;KWEnum(g4,multiply,_0,add,_1);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;1479;3291.555,8873.145;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.MatrixFromVectors;795;-2788.061,-5155.683;Inherit;False;FLOAT3x3;True;4;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3x3;0
Node;AmplifyShaderEditor.DynamicAppendNode;42;-2443.154,-4429.736;Inherit;True;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;700;-4480,-1966;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;1575;-4682.111,-1165.668;Inherit;False;3;0;FLOAT;1;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;60;-2162.941,-4370.634;Inherit;False;Property;_Float10;主贴图自定义偏移;171;0;Create;False;0;0;0;True;1;SubToggle(g7, _);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;1483;2276.427,8957.188;Inherit;False;2;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;731;-5045.2,-927.9001;Inherit;False;Property;_Float79;溶解扰动强度;113;0;Create;False;0;0;0;False;1;Sub(g4);False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1427;-6322.462,5400.813;Inherit;False;Property;_Float50;溶解自定义偏移;177;0;Create;False;0;0;0;True;3;SubToggle(g7, _);Space;Space;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;1482;3316.302,9560.918;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;2;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;1481;3264.601,9247.243;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;502;-6613.739,5533.312;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;952;-6661.457,4653.631;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;701;-4256,-1824;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;59;-2191.258,-4707.628;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;815;-6279.47,4838.089;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT3x3;0,0,0,1,1,1,1,0,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;793;-2394.479,-5170.1;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT3x3;0,0,0,1,1,1,1,0,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;1426;-6038.052,5312.617;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;312;-4755.34,-2986.389;Inherit;False;1094.708;330.5801;flowmap;7;241;242;285;310;311;305;316;flowmap;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1485;3549.763,9812.936;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;816;-6132.315,4641.277;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT3x3;0,0,0,1,1,1,1,0,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;449;-2015.384,-5158.836;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;732;-4427.401,-878.5001;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0.1;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;806;-2216.55,-5291.314;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT3x3;0,0,0,1,1,1,1,0,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1487;3562.785,8770.115;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;1422;-6102.359,4962.002;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1484;3578.061,9126.217;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1486;3611.688,9444.063;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;310;-4167,-2749;Inherit;False;Property;_Float32;flowmap扰动;116;0;Create;False;0;0;0;False;1;Sub(g14);False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;241;-4704,-2944;Inherit;True;Property;_flowmaptex;flowmaptex;115;0;Create;True;0;0;0;False;1;Sub(g14);False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;305;-4173.694,-2833.015;Inherit;False;Property;_Float31;custom控制flowmap扭曲;185;0;Create;False;0;0;0;True;3;SubToggle(g7, _);Space;Space;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;70;-4176,-1584;Inherit;False;noise;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;507;-5882.689,4579.836;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;1,1;False;2;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;509;-5880.8,5079.665;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;733;-4250.401,-857.7;Inherit;False;noise_intensity_dis;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;446;-1884.304,-5308.937;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;1,1;False;2;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;445;-1719.982,-4678.461;Inherit;False;Property;_Float49;主贴图极坐标（竖向贴图）;37;0;Create;False;0;0;0;False;1;SubToggle(g1, _);False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;1488;3940.302,9319.186;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;510;-5703.289,5085.664;Inherit;False;Property;_Float53;溶解极坐标（竖向贴图）;76;0;Create;False;0;0;0;False;1;SubToggle(g3, _);False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;43;-1915.256,-4887.891;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;501;-6587.124,5737.68;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;302;-5621.904,5755.349;Inherit;False;733;noise_intensity_dis;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;311;-3871.549,-2738.436;Inherit;False;flowmap_intensity;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;856;-3364.765,-3725.929;Inherit;False;2282.703;1348.265;ramptex;26;849;848;847;846;850;852;851;853;459;226;460;456;457;854;455;454;855;458;463;461;453;464;451;229;231;1261;ramptex;1,1,1,1;0;0
Node;AmplifyShaderEditor.LerpOp;444;-1705.038,-5172.987;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1489;4131.43,9300.529;Inherit;False;custom8;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;304;-5628.349,5620.964;Inherit;False;70;noise;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;242;-4384,-2912;Inherit;True;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;316;-3887.81,-2853.027;Inherit;False;flpwmap_custom_switch;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;440;-2597.618,-4561.853;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;511;-5533.591,4668.26;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;846;-3269.412,-3123.404;Inherit;False;Constant;_Float105;Float 105;138;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;285;-4104.301,-2936.389;Inherit;False;flowmap;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;849;-3250.307,-3221.214;Inherit;False;Constant;_Float108;Float 108;138;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;848;-3171.032,-3485.935;Inherit;False;Constant;_Float107;Float 107;138;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;58;-5158.785,4911.808;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;1490;-5394.773,5996.486;Inherit;False;1489;custom8;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;738;-5298.685,5539.158;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;314;-5512.011,5892.948;Inherit;False;311;flowmap_intensity;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;317;-5337.241,6132.879;Inherit;False;316;flpwmap_custom_switch;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;847;-3229.639,-3358.295;Inherit;False;Constant;_Float106;Float 106;138;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;1204;3936.073,-4862.643;Inherit;False;1112.73;708.7222;视差;8;1095;1096;1121;1104;1103;1094;1092;1097;视差;1,1,1,1;0;0
Node;AmplifyShaderEditor.PannerNode;36;-1540.054,-4877.719;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;307;-5046.755,5915.552;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;851;-2954.724,-3437.085;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DynamicAppendNode;852;-3014.983,-3255.047;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.Vector3Node;850;-3275.955,-2999.158;Inherit;False;Constant;_Vector6;Vector 6;138;0;Create;True;0;0;0;False;0;False;0,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;1095;4049.456,-4442.891;Inherit;False;Property;_Float38;视差缩放;168;0;Create;False;0;0;0;False;1;Sub(g15);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;161;-1168.652,-4864.328;Inherit;False;maintexUV_00;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;737;-5036.836,5456.622;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;306;-5341.993,5815.078;Inherit;False;285;flowmap;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;1360;-3502.155,9648.918;Inherit;False;Constant;_Float185;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1121;3986.073,-4812.643;Inherit;False;161;maintexUV_00;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;1343;-3404.328,8572.428;Inherit;False;Constant;_Float180;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1368;-3968.053,9629.65;Inherit;False;Property;_Custom4;Masky轴;176;1;[Enum];Create;False;0;4;x1;0;y1;1;z1;2;w1;3;0;True;1;KWEnum(g7,custom1x,_0,custom1y,_1,custom1z,_2,custom1w,_3);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;1096;4032.951,-4341.921;Inherit;False;Tangent;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;1340;-3342.404,8203.553;Inherit;False;Constant;_Float177;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;309;-4865.985,5570.581;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.MatrixFromVectors;853;-2773.873,-3281.284;Inherit;False;FLOAT3x3;True;4;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3x3;0
Node;AmplifyShaderEditor.RangedFloatNode;1362;-3553.856,9335.242;Inherit;False;Constant;_Float187;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;459;-3008.131,-2848.674;Inherit;False;Property;_Vector9;颜色混合图tilling&offset;51;0;Create;False;0;0;0;False;1;Sub(g11);False;1,1,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;226;-3114.721,-3675.929;Inherit;False;0;212;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;636;-3862.247,-1551.912;Inherit;False;2648.927;985.3096;Comment;44;385;386;635;633;632;634;631;29;281;49;23;25;62;31;24;30;26;33;45;34;32;122;61;384;93;661;784;785;786;912;913;914;915;916;918;1060;1062;1089;1090;1091;1262;1269;1469;1512;软溶解;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;278;-3229.496,771.2501;Inherit;False;2403.086;1048.659;溶解方向+开洞l;23;277;415;416;417;418;568;571;954;955;956;1027;1035;1015;1017;1057;1059;1197;1199;1200;1203;1255;1256;1257;溶解方向+开洞;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;55;-5056,-1184;Inherit;False;Property;_Float9;主贴图扰动强度;111;0;Create;False;0;0;0;False;1;Sub(g4);False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1367;-3555.395,9248.706;Inherit;False;Constant;_Float192;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1341;-3346.998,7484.555;Inherit;False;Constant;_Float178;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1347;-3395.643,7803.342;Inherit;False;Constant;_Float184;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1366;-3503.693,9562.381;Inherit;False;Constant;_Float191;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1342;-3394.105,7889.878;Inherit;False;Constant;_Float179;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1363;-3564.08,10017.79;Inherit;False;Constant;_Float188;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1361;-3506.75,8929.919;Inherit;False;Constant;_Float186;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1104;4358.416,-4320.314;Inherit;False;Property;_refplane;refplane(0黑色下沉,1白色抬高);169;0;Create;False;0;0;0;False;1;Sub(g15);False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1344;-3359.56,7580.853;Inherit;False;Constant;_Float181;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1103;4316.197,-4525.853;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1345;-3405.866,8485.891;Inherit;False;Constant;_Float182;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1346;-3343.942,8117.016;Inherit;False;Constant;_Float183;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1365;-3565.618,9931.254;Inherit;False;Constant;_Float190;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1348;-3808.301,8184.286;Inherit;False;Property;_Custom3;Maskx轴;175;1;[Enum];Create;False;0;4;x1;0;y1;1;z1;2;w1;3;0;True;1;KWEnum(g7,custom1x,_0,custom1y,_1,custom1z,_2,custom1w,_3);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1364;-3519.312,9026.217;Inherit;False;Constant;_Float189;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;1094;4000.179,-4650.874;Inherit;True;Property;_parallax;视差贴图;167;0;Create;False;0;0;0;False;1;Sub(g15);False;None;None;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.FunctionNode;460;-2413.342,-3578.787;Inherit;False;Polar Coordinates;-1;;100;7dab8e02884cf104ebefaa2e788e4162;0;4;1;FLOAT2;0,0;False;2;FLOAT2;0.5,0.5;False;3;FLOAT;1;False;4;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;588;-3244.336,1907.054;Inherit;False;3124.205;1004.605;Comment;44;194;193;201;188;191;190;202;185;183;196;576;580;582;584;585;579;186;195;192;575;581;586;589;590;591;650;651;652;653;655;781;782;783;901;902;903;904;905;906;907;908;909;910;1533;折射;1,1,1,1;0;0
Node;AmplifyShaderEditor.ConditionalIfNode;1371;-3356.155,9897.92;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;3;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;1351;-3196.404,8452.555;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;3;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;1369;-3345.933,9215.371;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;457;-2516.021,-2777.428;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;954;-3172.57,1169.743;Inherit;False;930;uv2;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;854;-2583.405,-3410.126;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT3x3;0,0,0,1,1,1,1,0,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;92;-4520.506,4822.985;Inherit;False;dissolveUV;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector4Node;1015;-2407.97,1178.731;Inherit;False;Property;_Vector35;开洞坐标;92;0;Create;False;0;0;0;False;1;Sub(g3);False;0,0,0,0;-0.15,1.8,-0.41,0.4;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;384;-3836.248,-1350.417;Inherit;False;Property;_Float41;溶解贴图旋转;77;0;Create;False;0;0;0;False;1;Sub(g3);False;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ParallaxOcclusionMappingNode;1092;4540.363,-4659.027;Inherit;False;0;128;False;-1;128;False;-1;10;0.02;1;False;1,1;False;0,0;8;0;FLOAT2;0,0;False;1;SAMPLER2D;;False;7;SAMPLERSTATE;;False;2;FLOAT;0.02;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT2;0,0;False;6;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ConditionalIfNode;1350;-3134.48,8083.681;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;2;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;1352;-3159.227,7395.907;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;455;-2536.359,-3048.364;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;1373;-4334.106,8925.314;Inherit;False;1;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;1353;-4174.355,7479.951;Inherit;False;1;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ConditionalIfNode;1372;-3318.979,8841.271;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;360;-4455.5,-1258.5;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0.1;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;1370;-3294.231,9529.045;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;2;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;956;-3198.492,1051.38;Inherit;False;1;1257;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;456;-2515.028,-2919.449;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;418;-3198.437,915.9819;Inherit;False;0;1257;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ConditionalIfNode;1349;-3186.181,7770.006;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;454;-2340.95,-2994.7;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector4Node;581;-3147.44,2700.61;Inherit;False;Property;_Vector21;折射流动&斜切;143;0;Create;False;0;0;0;False;1;Sub(g6);False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;902;-3177.425,2098.158;Inherit;False;Constant;_Float126;Float 126;138;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;904;-3186.555,2268.514;Inherit;False;Constant;_Float128;Float 128;138;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;855;-2188.006,-3362.838;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT3x3;0,0,0,1,1,1,1,0,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;1430;2801.254,7776.759;Inherit;False;Constant;_Float211;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1429;2749.553,7463.084;Inherit;False;Constant;_Float210;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;903;-3165.06,2176.704;Inherit;False;Constant;_Float127;Float 127;138;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;955;-2926.77,1032.643;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FunctionNode;571;-2842.621,842.5984;Inherit;False;Polar Coordinates;-1;;126;7dab8e02884cf104ebefaa2e788e4162;0;4;1;FLOAT2;0,0;False;2;FLOAT2;0.5,0.5;False;3;FLOAT;1;False;4;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;1428;2796.66,7057.761;Inherit;False;Constant;_Float209;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;901;-3188.09,2040.586;Inherit;False;Constant;_Float125;Float 125;138;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1354;-2901.019,8335.699;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1433;2737.792,8059.097;Inherit;False;Constant;_Float214;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1435;2335.357,7757.492;Inherit;False;Property;_Custom7;自定义溶解;183;1;[Enum];Create;False;0;4;x2;0;y2;1;z2;2;w2;3;0;True;1;KWEnum(g7,custom2x,_0,custom2y,_1,custom2z,_2,custom2w,_3);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TransformPositionNode;1197;-2250.743,1376.547;Inherit;False;Object;World;False;Fast;True;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RegisterLocalVarNode;1097;4824.803,-4636.033;Inherit;False;parallax;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;93;-3639.004,-1501.912;Inherit;False;92;dissolveUV;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;1432;2799.716,7690.222;Inherit;False;Constant;_Float213;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;1017;-2159.614,1206.869;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;1443;2784.098,7154.059;Inherit;False;Constant;_Float223;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1357;-2887.997,7292.878;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;415;-2794.843,1480.791;Inherit;False;Property;_Float47;溶解方向旋转;81;0;Create;False;0;0;0;False;1;Sub(g3);False;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1431;2739.33,8145.634;Inherit;False;Constant;_Float212;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1377;-3047.749,8738.242;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1434;2748.015,7376.548;Inherit;False;Constant;_Float215;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1376;-3032.473,9094.343;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1375;-2998.846,9412.189;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1355;-2839.094,7966.825;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1374;-3060.771,9781.063;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;67;-4213,-1269.3;Inherit;False;noise_intensity_main;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1356;-2872.721,7648.979;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;458;-2264.886,-2811.176;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PiNode;385;-3549.538,-1253.424;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;906;-2929.735,2064.001;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RotatorNode;386;-3405.465,-1421.732;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ConditionalIfNode;1452;3009.178,7656.887;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;2;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;461;-1991.686,-3258.543;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;1,1;False;2;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;1101;-1563.714,-2246.726;Inherit;False;Property;_Float143;开启视差映射(mesh模式下使用）;166;1;[Enum];Create;False;0;2;off;0;on;1;0;False;1;KWEnum(g15,off,_0,on,_1);False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;905;-3186.874,2348.734;Inherit;False;Constant;_Vector30;Vector 30;138;0;Create;True;0;0;0;False;0;False;0,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.GetLocalVarNode;1100;-1425.389,-2325.209;Inherit;False;1097;parallax;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;568;-2507.247,913.2547;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;1261;-2177.884,-2710.843;Inherit;False;Property;_Float159;颜色混合图极坐标（竖向贴图）;46;0;Create;False;0;0;0;True;1;SubToggle(g11, _);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;575;-3206.392,2529.498;Inherit;False;Property;_reftex_ST;_reftex_ST;142;1;[HideInInspector];Create;True;0;0;0;False;0;False;1,1,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;1200;-2103.651,1511.521;Inherit;False;Property;_Float155;local/world;91;1;[Enum];Create;False;0;2;local;0;world;1;0;True;1;KWEnum(g3,local,_0,world,_1);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;463;-3000.783,-2608.419;Inherit;False;Property;_Vector7;颜色图流动速度;52;0;Create;False;0;0;0;False;1;Sub(g11);False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;1206;-618.7476,-6700.138;Inherit;False;2870.985;1564.313;阴影;39;1169;1171;1170;1180;1168;1173;1174;1172;1179;1175;1176;1178;1177;995;996;1002;1149;1001;1147;957;958;1011;960;1009;959;1008;963;962;961;965;964;968;1000;997;1013;1012;1007;971;1598;阴影;1,1,1,1;0;0
Node;AmplifyShaderEditor.ConditionalIfNode;1455;2984.431,6969.113;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;498;-7011.592,-5547.575;Inherit;False;3316.701;2494.345;Comment;84;478;74;73;75;481;483;486;474;77;217;477;469;471;491;497;479;496;468;472;321;323;484;494;322;485;495;467;480;79;390;216;387;319;388;320;391;392;389;408;405;218;8;406;407;220;82;620;622;623;624;625;626;616;769;770;771;772;774;773;818;819;820;821;822;823;824;825;826;827;830;835;834;833;836;837;828;931;932;933;934;935;936;1380;1381;Mask;1,1,1,1;0;0
Node;AmplifyShaderEditor.DynamicAppendNode;908;-2912.077,2170.487;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;71;-1314.833,-2131.202;Inherit;False;70;noise;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;162;-1347.398,-2411.018;Inherit;False;161;maintexUV_00;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;1203;-1999.555,1302.004;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ConditionalIfNode;1454;2947.254,8025.761;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;3;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;1453;2957.477,7343.212;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;1451;1969.303,7053.157;Inherit;False;2;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PiNode;416;-2509.583,1483.961;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;72;-1313.013,-1995.727;Inherit;False;67;noise_intensity_main;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;1358;-2510.48,7841.948;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;453;-2109.892,-2994.504;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;1378;-2670.231,9287.313;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;830;-6968.703,-4128.217;Inherit;False;Constant;_Float103;Float 103;138;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1458;3304.564,7540.031;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;1180;-524.8001,-5901.105;Inherit;False;Property;_Vector10;法线流动&斜切;149;0;Create;False;0;0;0;False;1;Sub(g12);False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;831;-6970.522,-3875.013;Inherit;False;Constant;_Float104;Float 104;138;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;186;-3139.38,1953.014;Inherit;False;0;190;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BreakToComponentsNode;635;-3323.935,-1293.86;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.RangedFloatNode;1171;-474.0176,-6372.938;Inherit;False;Constant;_Float150;Float 150;138;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1170;-461.6045,-6512.771;Inherit;False;Constant;_Float149;Float 149;138;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;819;-6970.603,-5133.207;Inherit;False;Constant;_Float98;Float 98;138;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;353;-1099.468,-2114.346;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;576;-2640.736,1954.054;Inherit;False;Polar Coordinates;-1;;127;7dab8e02884cf104ebefaa2e788e4162;0;4;1;FLOAT2;0,0;False;2;FLOAT2;0.5,0.5;False;3;FLOAT;1;False;4;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;1099;-1063.338,-2335.242;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;1168;-503.6626,-6267.252;Inherit;False;Constant;_Float147;Float 147;138;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;818;-7000.641,-4975.674;Inherit;False;Constant;_Float97;Float 97;138;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.MatrixFromVectors;907;-2748.884,2167.934;Inherit;False;FLOAT3x3;True;4;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3x3;0
Node;AmplifyShaderEditor.LerpOp;1199;-1789.654,1239.106;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WorldPosInputsNode;1027;-1976.355,1604.836;Inherit;True;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1463;3255.661,6866.084;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;484;-6928,-3280;Inherit;False;Property;_Vector13;遮罩02流动&斜切;71;0;Create;False;0;0;0;False;1;Sub(g2);False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;821;-6994.715,-5063.575;Inherit;False;Constant;_Float100;Float 100;138;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1456;3242.639,7908.905;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;451;-1729.025,-3108.424;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;829;-6958.109,-4014.846;Inherit;False;Constant;_Float102;Float 102;138;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;100;-1229.3,-1804.712;Inherit;False;2;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1457;3270.937,7222.185;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;318;-988.9742,-1647.137;Inherit;False;316;flpwmap_custom_switch;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1379;-2479.104,9272.102;Inherit;False;custom4;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;417;-2124.709,910.199;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;464;-2292.159,-2553.768;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector4Node;479;-6983.609,-4667.555;Inherit;False;Property;_Vector11;遮罩01流动速度&斜切;63;0;Create;False;0;0;0;False;1;Sub(g2);False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;820;-6958.883,-5227.491;Inherit;False;Constant;_Float99;Float 99;138;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;828;-6999.883,-3777.416;Inherit;False;Constant;_Float101;Float 101;138;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1359;-2319.352,7823.292;Inherit;False;custom3;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;313;-995.7439,-1848.048;Inherit;False;311;flowmap_intensity;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1169;-435.7715,-6650.138;Inherit;False;Constant;_Float148;Float 148;138;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1257;-1639.416,830.1862;Inherit;True;Property;_TextureSample1;溶解方向贴图（渐变）;80;0;Create;False;0;0;0;False;1;Ramp(g3);False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector3Node;832;-7014.055,-3695.607;Inherit;False;Constant;_Vector4;Vector 4;138;0;Create;True;0;0;0;False;0;False;0,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.LerpOp;99;-697.2449,-1831.5;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1500;5035.415,7539.143;Inherit;False;Property;_Custom9;自定义溶解软硬;184;1;[Enum];Create;False;0;4;x2;0;y2;1;z2;2;w2;3;0;True;1;KWEnum(g7,custom2x,_0,custom2y,_1,custom2z,_2,custom2w,_3);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;284;-1017.467,-1990.128;Inherit;False;285;flowmap;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector4Node;483;-6991.586,-3518.17;Inherit;False;Property;_Mask1_ST;_Mask1_ST;70;1;[HideInInspector];Create;True;0;0;0;False;0;False;1,1,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;1499;5437.851,7840.748;Inherit;False;Constant;_Float232;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;936;-6989.049,-4266.879;Inherit;False;1;218;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;833;-6765.604,-4094.595;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;217;-6970.399,-4404.645;Inherit;False;0;218;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;1498;5449.611,7244.735;Inherit;False;Constant;_Float231;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;633;-3116.146,-1221.938;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;632;-3139.666,-1342.1;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;634;-3115.571,-1462.119;Inherit;False;Property;_Float66;溶解贴图x轴Clamp;74;0;Create;False;0;0;0;True;1;SubToggle(g3, _);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;229;-1498.935,-3046.488;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;1497;5496.718,6839.412;Inherit;False;Constant;_Float230;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;478;-6635.028,-4956.565;Inherit;False;Property;_Mask_ST;_Mask_ST;62;1;[HideInInspector];Create;True;0;0;0;False;0;False;1,1,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DistanceOpNode;1035;-1587.19,1244.4;Inherit;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;910;-2312.665,1978.759;Inherit;False;2;2;0;FLOAT2;0,1;False;1;FLOAT3x3;0,0,0,1,1,1,1,0,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;834;-6736.532,-3898.222;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;1496;5439.389,7927.285;Inherit;False;Constant;_Float229;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;824;-6633.684,-5178.038;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;1059;-1550.654,1576.436;Inherit;False;Property;_Float139;开洞（开启后方向失效）;90;0;Create;False;0;0;0;True;1;SubToggle(g3, _);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;380;-1023.201,-2469.299;Inherit;False;Property;_Float39;主贴图旋转;38;0;Create;False;0;0;0;False;1;Sub(g1);False;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;932;-7012.682,-5407.778;Inherit;False;1;8;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;1381;-6421.323,-4431.144;Inherit;False;1379;custom4;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;354;-853.5489,-2173.973;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector3Node;1174;-514.5505,-6109.532;Inherit;False;Constant;_Vector8;Vector 8;138;0;Create;True;0;0;0;False;0;False;0,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.GetLocalVarNode;933;-6830.65,-5338.782;Inherit;False;930;uv2;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;1465;3633.178,7415.154;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;823;-6625.031,-5318.283;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DynamicAppendNode;1173;-215.3965,-6409.447;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.Vector3Node;822;-6956.994,-4859.457;Inherit;False;Constant;_Vector3;Vector 3;138;0;Create;True;0;0;0;False;0;False;0,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;1495;5448.073,7158.199;Inherit;False;Constant;_Float228;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;785;-3257.257,-1093.851;Inherit;False;Property;_Float87;溶解贴图y轴Clamp;75;0;Create;False;0;0;0;True;1;SubToggle(g3, _);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;909;-2500.665,2142.259;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT3x3;0,0,0,1,1,1,1,0,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;1172;-204.0067,-6601.43;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;1494;5501.313,7558.41;Inherit;False;Constant;_Float227;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;579;-2779.336,2565.054;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;935;-6793.533,-4185.381;Inherit;False;930;uv2;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;77;-6986.136,-5532.217;Inherit;False;0;8;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;1380;-6430.081,-4540.629;Inherit;False;1359;custom3;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1492;5484.156,6935.71;Inherit;False;Constant;_Float225;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1493;5499.774,7471.873;Inherit;False;Constant;_Float226;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1520;5462.301,9263.805;Inherit;False;Constant;_Float240;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;734;-5049.1,-1033;Inherit;False;Property;_Float80;mask扰动强度;112;0;Create;False;0;0;0;False;1;Sub(g4);False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;231;-1306.062,-3047.576;Inherit;False;Gradienttex;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FunctionNode;497;-6209.446,-4202.971;Inherit;False;Polar Coordinates;-1;;136;7dab8e02884cf104ebefaa2e788e4162;0;4;1;FLOAT2;0,0;False;2;FLOAT2;0.5,0.5;False;3;FLOAT;1;False;4;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;1532;5001.162,9331.074;Inherit;False;Property;_Custom10;自定义折射;190;1;[Enum];Create;False;0;4;x2;0;y2;1;z2;2;w2;3;0;True;1;KWEnum(g7,custom2x,_0,custom2y,_1,custom2z,_2,custom2w,_3);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;1503;5684.489,6750.764;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1518;5412.138,9036.666;Inherit;False;Constant;_Float238;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;73;-5845.026,-4463.095;Inherit;False;Property;_Float12;mask01自定义偏移;174;0;Create;False;0;0;0;True;3;SubToggle(g7, _);Space;Space;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;1057;-1285.17,1063.989;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;586;-2330.196,2627.662;Inherit;False;Property;_Float58;折射极坐标（竖向贴图）;140;0;Create;False;0;0;0;False;1;SubToggle(g6, _);False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;758;-7016.301,-395.5196;Inherit;False;1519.266;836.9044;软粒子;15;96;98;327;333;334;337;97;126;752;756;753;757;336;754;755;软粒子;1,1,1,1;0;0
Node;AmplifyShaderEditor.LerpOp;283;-465.3266,-2047.924;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;1501;4669.361,6834.808;Inherit;False;2;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.MatrixFromVectors;835;-6594.048,-3903.101;Inherit;False;FLOAT3x3;True;4;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3x3;0
Node;AmplifyShaderEditor.RangedFloatNode;1519;5400.378,9632.68;Inherit;False;Constant;_Float239;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;918;-2825.641,-1506.184;Inherit;False;Property;_Float130;混合溶解强度;82;0;Create;False;0;0;0;False;1;Sub(g3);False;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;1502;5647.313,7807.412;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;3;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;1179;-143.1956,-6105.099;Inherit;False;0;1147;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PiNode;378;-692.8785,-2441.443;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;784;-2908.75,-1385.345;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1514;5463.84,9350.342;Inherit;False;Constant;_Float234;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.MatrixFromVectors;825;-6472.706,-5224.272;Inherit;False;FLOAT3x3;True;4;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3x3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1466;3824.306,7396.498;Inherit;False;custom7;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1517;5459.245,8631.344;Inherit;False;Constant;_Float237;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.MatrixFromVectors;1175;-22.63062,-6393.672;Inherit;False;FLOAT3x3;True;4;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3x3;0
Node;AmplifyShaderEditor.ConditionalIfNode;1505;5657.535,7124.863;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;934;-6594.565,-4286.377;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;481;-6351.458,-4939.401;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;75;-6200.814,-4552.459;Inherit;True;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;582;-2389.344,2394.38;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ConditionalIfNode;1504;5709.236,7438.538;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;2;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1513;5446.683,8727.642;Inherit;False;Constant;_Float233;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;786;-2902.257,-1167.851;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;931;-6622.682,-5476.778;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;1515;5410.601,8950.131;Inherit;False;Constant;_Float235;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;477;-6130.515,-5509.042;Inherit;False;Polar Coordinates;-1;;135;7dab8e02884cf104ebefaa2e788e4162;0;4;1;FLOAT2;0,0;False;2;FLOAT2;0.5,0.5;False;3;FLOAT;1;False;4;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;584;-2171.536,1998.739;Inherit;False;3;0;FLOAT2;0,1;False;1;FLOAT2;1,1;False;2;FLOAT2;0,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;1516;5401.917,9719.217;Inherit;False;Constant;_Float236;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;377;-440.2984,-2471.626;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1509;6004.622,7321.682;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;1062;-2611.686,-1412.892;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;827;-5865.49,-5417.849;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT3x3;0,0,0,1,1,1,1,0,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;631;-2755.902,-1259.116;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;233;-691.4322,-976.6682;Inherit;False;67;noise_intensity_main;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;1522;5647.016,8542.695;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;585;-1942.312,2106.268;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;486;-6540,-3344.619;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;580;-2763.697,2690.697;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;61;-3290.452,-765.5207;Inherit;False;Property;_Float11;custom控制溶解;180;0;Create;False;0;0;0;False;3;SubToggle(g7, _);Space;Space;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;912;-3044.244,-791.8413;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;474;-6092.26,-4945.602;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1176;198.3375,-6225.29;Inherit;False;2;2;0;FLOAT3x3;0,0,0,1,1,1,1,0,1;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;836;-6430.828,-4089.915;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT3x3;0,0,0,1,1,1,1,0,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ConditionalIfNode;1521;5609.84,9599.344;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;3;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;333;-6745.693,325.3848;Inherit;False;Property;_Float5;反向软粒子(强化边缘）;19;0;Create;False;0;0;0;True;1;SubToggle(g9, _);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;29;-3044.951,-1005.381;Inherit;False;Property;_Float6;溶解;83;0;Create;False;0;0;0;False;1;Sub(g3);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;826;-6298.804,-5417.229;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT3x3;0,0,0,1,1,1,1,0,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;1178;104.7235,-5916.098;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;146;-3141.72,3013.755;Inherit;False;1475.065;723.4756;菲尼尔;11;135;137;138;139;140;141;142;144;147;136;352;菲尼尔;1,1,1,1;0;0
Node;AmplifyShaderEditor.ConditionalIfNode;1525;5671.763,9230.47;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;2;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;591;-1991.792,2336.677;Inherit;False;Property;_Float59;折射贴图旋转;141;0;Create;False;0;0;0;False;1;Sub(g6);False;1;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1506;5970.995,7003.836;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;1523;4631.888,8626.74;Inherit;False;2;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1508;5955.719,6647.735;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1469;-3135.603,-909.5148;Inherit;False;1466;custom7;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1507;5942.697,7690.556;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;98;-6836.29,91.33076;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;381;-801.0881,-1354.992;Inherit;False;Property;_Float40;颜色混合贴图旋转;50;0;Create;False;0;0;0;False;1;Sub(g11);False;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;735;-4433.9,-1063.7;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0.1;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;471;-5883.976,-5237.453;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;277;-1017.08,968.086;Inherit;False;dis_direction;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;232;-662.8661,-1094.818;Inherit;False;70;noise;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;1524;5620.063,8916.795;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;837;-6078.913,-3944.78;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT3x3;0,0,0,1,1,1,1,0,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;96;-6851.671,253.7125;Inherit;False;Property;_Float16;软粒子（羽化边缘）;18;0;Create;False;0;0;0;False;1;Sub(g9);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;234;-674.2082,-1230.127;Inherit;False;231;Gradienttex;1;0;OBJECT;;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;756;-6966.301,-175.2479;Inherit;False;Property;_Float55;相机软粒子（贴脸羽化）距离;22;0;Create;False;0;0;0;False;1;Sub(g9);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1529;5933.522,8795.768;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;915;-2758.661,-830.8226;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldNormalVector;136;-3118.165,3057.115;Inherit;False;True;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1528;5905.224,9482.488;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;914;-2806.012,-689.4841;Inherit;False;Property;_Float129;粒子alpha控制溶解（溶解拖尾使用）;182;1;[Enum];Create;False;0;2;off;0;20;1;0;False;1;KWEnum(g7,off,_0,on,_1);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;736;-4229.1,-1125.2;Inherit;False;noise_intensity_mask;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;334;-6483.376,320.6759;Inherit;False;depthfade_switch;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;496;-5934.142,-3907.305;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;1,1;False;2;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PiNode;382;-471.7652,-1367.136;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1149;454.9354,-5808.885;Inherit;False;Property;_Float146;法线强度;150;0;Create;False;0;0;0;False;1;Sub(g12);False;0;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DepthFade;97;-6604.836,90.75565;Inherit;False;True;True;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;281;-2602.335,-1293.837;Inherit;False;277;dis_direction;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;468;-5619.626,-4864.986;Inherit;False;Property;_Float51;遮罩01极坐标（竖向贴图）;60;0;Create;False;0;0;0;False;1;SubToggle(g2, _);False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1527;5918.247,8439.666;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;752;-6964.098,-345.5196;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;1060;-2421.996,-1513.499;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;491;-6054.449,-3572.763;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;472;-5622.681,-5444.526;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;1,1;False;2;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;494;-5987.206,-3319.945;Inherit;False;Property;_Float52;遮罩02极坐标（竖向贴图）;68;0;Create;False;0;0;0;False;1;SubToggle(g2, _);False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;603;-285.3804,-2352.52;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.FaceVariableNode;352;-3012.43,3187.22;Inherit;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.PiNode;590;-1685.244,2354.179;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;730;-8714.083,633.6453;Inherit;False;3930.691;2494.765;顶点偏移;85;706;544;707;173;710;546;551;711;712;549;714;555;716;715;554;552;728;718;719;556;557;396;168;729;397;725;398;726;644;720;721;722;645;646;649;647;723;727;178;167;179;705;169;176;175;172;171;181;787;788;789;790;791;792;881;882;883;884;885;886;887;888;889;890;891;892;893;894;895;896;897;898;899;921;922;945;946;947;948;949;950;990;993;994;1554;顶点偏移;1,1,1,1;0;0
Node;AmplifyShaderEditor.PannerNode;1177;442.0487,-6076.574;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;188;-1789.108,2149.545;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;235;-406.7563,-1214.137;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;23;-2630.881,-1161.835;Inherit;True;Property;_dissolvetex;溶解贴图;73;0;Create;False;0;0;0;False;1;Sub(g3);False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;1510;6333.236,7196.805;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;469;-5733.47,-5008.931;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;62;-2740.221,-984.6865;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1526;5967.149,9113.613;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;753;-6951.867,-80.19434;Inherit;False;Property;_Float0;相机软粒子（贴脸羽化）位置;23;0;Create;False;0;0;0;False;1;Sub(g9);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;327;-6383.158,172.6414;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;605;15.1352,-2200.634;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;763;-214.7163,-2119.484;Inherit;False;Property;_Float71;主贴图y轴clamp;36;0;Create;False;0;0;0;True;1;SubToggle(g1, _);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;604;85.19678,-2345.694;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.CameraDepthFade;754;-6607.409,-272.9024;Inherit;False;3;2;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;351;-2795.43,3144.22;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;137;-3052.719,3270.273;Inherit;False;World;True;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RegisterLocalVarNode;1511;6524.364,7178.149;Inherit;False;custom9;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;1530;6295.763,8988.736;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;892;-8684.934,2006.185;Inherit;False;Constant;_Float123;Float 123;138;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;31;-2115.32,-914.2172;Inherit;False;Constant;_Float7;Float 7;11;0;Create;True;0;0;0;False;0;False;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;913;-2478.706,-938.4984;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;114;-3227.833,-169.203;Inherit;False;1936.036;770.2162; 亮边溶解;16;107;109;105;106;124;130;133;1053;1238;1239;1240;1241;1263;1264;1266;1267;亮边溶解;1,1,1,1;0;0
Node;AmplifyShaderEditor.RotatorNode;589;-1479.219,2228.388;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;321;-5456.687,-3892.311;Inherit;False;70;noise;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;383;-239.743,-1421.919;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;608;-304.5597,-2581.715;Inherit;False;Property;_Float62;主贴图x轴clamp;35;0;Create;False;0;0;0;True;1;SubToggle(g1, _);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;495;-5651.359,-3604.686;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;485;-6531.846,-3217.787;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;891;-8697.299,1927.639;Inherit;False;Constant;_Float122;Float 122;138;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;135;-3045.824,3462.564;Inherit;False;Property;_power3;菲尼尔范围;27;0;Create;False;0;0;0;False;1;Sub(g8);False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;555;-8581.451,2680.061;Inherit;False;Property;_Vector19;顶点偏移流动&斜切;126;0;Create;False;0;0;0;False;1;Sub(g5);False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;337;-6259.045,305.1179;Inherit;False;334;depthfade_switch;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;916;-2194.913,-1380.604;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;323;-5520.148,-3768.018;Inherit;False;736;noise_intensity_mask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;890;-8707.965,1870.067;Inherit;False;Constant;_Float121;Float 121;138;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1147;656.7075,-5998.508;Inherit;True;Property;_normallight;法线;148;0;Create;False;0;0;0;False;1;Sub(g12);False;-1;None;None;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;893;-8706.43,2097.995;Inherit;False;Constant;_Float124;Float 124;138;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;480;-6304,-4736;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;25;-2218.638,-1113.572;Inherit;False;Constant;_Float3;Float 3;11;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;1213;40.08984,-2883.879;Inherit;False;Property;_Vector16;色散;43;0;Create;False;0;0;0;False;1;Sub(g1);False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;467;-5403.192,-5152.456;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;138;-3076.711,3573.226;Inherit;False;Property;_Float19;菲尼尔软硬;29;0;Create;False;0;0;0;False;1;Sub(g8);False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;79;-5052.572,-5043.696;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;1205;3072.943,-6560.758;Inherit;False;3294.75;979.6411;matcap&cubemap;23;1064;1065;1066;1067;1068;1069;1063;1073;1072;1074;1209;1210;1578;1579;1580;1583;1584;1585;1587;1591;1593;1599;1597;matcap&cubemap;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;1091;-2005.488,-651.1614;Inherit;False;Property;_Float142;custom控制溶解软硬;181;0;Create;False;0;0;0;True;1;SubToggle(g7, _);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;1225;242.4564,-2876.727;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;1230;104.127,-3063.969;Inherit;False;Constant;_Float158;Float 158;178;0;Create;True;0;0;0;False;0;False;0.01;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;609;-58.68368,-1565.849;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.TextureCoordinatesNode;173;-8633.986,1655.241;Inherit;False;0;169;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FresnelNode;139;-2742.485,3325.885;Inherit;False;Standard;WorldNormal;ViewDir;True;True;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;897;-8431.951,1999.968;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;33;-2228.09,-821.8804;Inherit;False;Property;_Float8;软硬;84;0;Create;False;0;0;0;False;1;Sub(g3);False;0.5;1;0;0.5;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;895;-8449.609,1893.482;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SaturateNode;755;-6313.52,-267.9716;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;387;-5172.797,-3638.586;Inherit;False;Property;_Float42;遮罩02旋转;69;0;Create;False;0;0;0;False;1;Sub(g2);False;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;24;-2056.404,-1203.958;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;650;-1819.611,1958.332;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.RangedFloatNode;106;-3187.887,198.0487;Inherit;False;Property;_Float17;一层亮边宽度;86;0;Create;False;0;0;0;False;1;Sub(g3);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;390;-5236.398,-4824.823;Inherit;False;Property;_Float43;遮罩01旋转;61;0;Create;False;0;0;0;False;1;Sub(g2);False;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;336;-6100.238,75.26977;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;661;-1746.907,-1444.874;Inherit;False;dis_tex;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1512;-2049.588,-732.6893;Inherit;False;1511;custom9;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;894;-8706.748,2178.215;Inherit;False;Constant;_Vector29;Vector 29;138;0;Create;True;0;0;0;False;0;False;0,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.PannerNode;216;-5287.117,-3556.391;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1531;6486.891,8970.08;Inherit;False;custom10;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;950;-8627.366,1765.149;Inherit;False;1;169;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;-1944.236,-1007.107;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1598;813.3359,-6370.22;Inherit;False;normal_111;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;322;-5180.346,-3817.024;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;607;206.037,-2481.101;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;764;199.2137,-2219.356;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;948;-8467.366,1845.149;Inherit;False;930;uv2;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;882;-8678.968,926.0474;Inherit;False;Constant;_Float118;Float 118;138;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;611;118.1047,-1420.927;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;651;-1639.822,2042.254;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;766;-88.26966,-1298.326;Inherit;False;Property;_Float81;颜色混合贴图y轴Clamp;49;0;Create;False;0;0;0;True;1;SubToggle(g11, _);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1267;-2943.18,248.7166;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;716;-8466.573,1474.328;Inherit;False;Property;_Vector25;顶点偏移遮罩流动＆斜切;134;0;Create;False;0;0;0;False;1;Sub(g5);False;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;26;-1893.156,-1183.473;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;140;-2505.02,3326.082;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.MatrixFromVectors;896;-8274.712,2091.249;Inherit;False;FLOAT3x3;True;4;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3x3;0
Node;AmplifyShaderEditor.GetLocalVarNode;1599;4501.518,-6203.774;Inherit;False;1598;normal_111;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;192;-1585.979,2465.205;Inherit;False;Property;_Float23;折射强度;145;0;Create;False;0;0;0;False;1;Sub(g6);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1229;375.127,-3025.969;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;1239;-3092.695,413.1902;Inherit;False;Property;_Float160;二层亮边宽度;87;0;Create;False;0;0;0;False;1;Sub(g3);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;612;-244.1711,-1810.111;Inherit;False;Property;_Float63;颜色混合贴图x轴Clamp;48;0;Create;False;0;0;0;True;1;SubToggle(g11, _);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PiNode;391;-4952.867,-4744.793;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;995;563.2158,-5675.33;Inherit;False;Property;_Vector34;假点光坐标;156;0;Create;False;0;0;0;False;1;Sub(g12);False;0,0,0,0;-0.15,1.8,-0.41,0.4;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;544;-8685.137,2405.465;Inherit;False;Property;_vertextex_ST;_vertextex_ST;125;1;[HideInInspector];Create;True;0;0;0;False;0;False;1,1,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;757;-5952.993,-148.1963;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;949;-8275.366,1733.149;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;319;-5024.052,-4601.649;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;783;-1532.768,2168.542;Inherit;False;Property;_Float86;折射贴图y轴Clamp;139;0;Create;False;0;0;0;True;1;SubToggle(g6, _);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1533;-1649.501,2674.632;Inherit;False;1531;custom10;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;652;-1631.342,1933.092;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;195;-1641.269,2795.659;Inherit;False;Property;_Float24;custom控制折射;189;0;Create;False;0;0;0;True;3;SubToggle(g7, _);Space;Space;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;606;359.7148,-2367.942;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;655;-1482.538,2038.624;Inherit;False;Property;_Float69;折射贴图x轴Clamp;138;0;Create;False;0;0;0;True;1;SubToggle(g6, _);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;320;-4856.229,-3799.028;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PiNode;388;-5348.449,-3642.73;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;610;153.4854,-1586.689;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1053;-2893.244,-45.45695;Inherit;False;661;dis_tex;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;884;-8688.097,1096.403;Inherit;False;Constant;_Float120;Float 120;138;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;883;-8666.603,1004.593;Inherit;False;Constant;_Float119;Float 119;138;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;881;-8689.632,868.4754;Inherit;False;Constant;_Float117;Float 117;138;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;1090;-1835.93,-803.7596;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;898;-7913.26,2041.444;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT3x3;0,0,0,1,1,1,1,0,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.OneMinusNode;34;-1637.713,-791.3764;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;767;283.7303,-1466.326;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldNormalVector;1064;4842.099,-6228.99;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RegisterLocalVarNode;126;-5721.036,65.64495;Inherit;False;depthfade;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;886;-8391.942,873.0101;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ViewMatrixNode;1065;4867.248,-6431.993;Inherit;False;0;1;FLOAT4x4;0
Node;AmplifyShaderEditor.OneMinusNode;141;-2409.202,3428.264;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;549;-7961.628,1766.015;Inherit;False;Polar Coordinates;-1;;137;7dab8e02884cf104ebefaa2e788e4162;0;4;1;FLOAT2;0,0;False;2;FLOAT2;0.5,0.5;False;3;FLOAT;1;False;4;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;888;-8413.619,998.3764;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WorldPosInputsNode;1002;1291.197,-5360.825;Inherit;True;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1266;-2783.489,413.5929;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;-0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;1224;496.2668,-2773.436;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;707;-8518.483,633.5099;Inherit;False;0;705;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;1227;616.1271,-3069.172;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RotatorNode;389;-4786.38,-3667.767;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;765;291.6302,-1744.726;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;706;-8520.824,1235.588;Inherit;False;Property;_vertextex1_ST;_vertextex1_ST;133;1;[HideInInspector];Create;True;0;0;0;False;0;False;1,1,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;45;-1738.658,-1200.625;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;193;-1364.284,2656.537;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;947;-8522.157,762.2756;Inherit;False;1;705;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;201;-1380.826,2430.654;Inherit;False;Constant;_Float26;Float 26;43;0;Create;True;0;0;0;False;0;False;0.01;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;885;-8688.416,1176.623;Inherit;False;Constant;_Vector28;Vector 28;138;0;Create;True;0;0;0;False;0;False;0,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DynamicAppendNode;996;943.8885,-5683.873;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;109;-2674.31,238.7258;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;392;-4771.551,-4688.157;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;781;-1290.998,1938.46;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;945;-8362.157,842.2756;Inherit;False;930;uv2;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;142;-2560.026,3560.687;Inherit;False;Property;_Float20;反向菲尼尔（虚化边缘）;30;0;Create;False;0;0;0;True;1;SubToggle(g8, _);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;130;-2553.58,-13.45383;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;-0.5;False;2;FLOAT;1.5;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;782;-1331.228,2109.378;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;144;-2176.633,3335.982;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;712;-7832.597,680.8583;Inherit;False;Polar Coordinates;-1;;138;7dab8e02884cf104ebefaa2e788e4162;0;4;1;FLOAT2;0,0;False;2;FLOAT2;0.5,0.5;False;3;FLOAT;1;False;4;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;1;503.9102,-2257.324;Inherit;True;Property;_maintex;主贴图;33;0;Create;False;0;0;0;False;1;Sub(g1);False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;105;-2164.216,-46.14251;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;357;188.4027,-654.0822;Inherit;False;Property;_Color3;颜色2;14;1;[HDR];Create;False;0;0;0;False;1;Sub(g10);False;1,1,1,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BreakToComponentsNode;623;-5246.948,-3303.893;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.ColorNode;4;167.422,-922.795;Inherit;False;Property;_Color0;颜色;11;1;[HDR];Create;False;0;0;0;False;1;Sub(g10);False;1,1,1,1;0,0.6419505,2.270603,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;358;307.5624,-430.5294;Inherit;False;Property;_Float35;双面颜色（默认关闭，勾上开启）;13;0;Create;False;0;0;0;True;1;SubToggle(g10, _);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;107;-2215.584,180.9771;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DistanceOpNode;1001;1600.581,-5570.061;Inherit;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1226;831.442,-3082.568;Inherit;True;Property;_maintex2;主贴图;33;0;Create;False;0;0;0;False;0;False;1223;None;None;True;0;False;white;Auto;False;Instance;1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;191;-1137.447,2633.402;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;900;-7642.331,1881.517;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;946;-8170.157,730.2756;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;1223;767.2168,-2753.049;Inherit;True;Property;_maintex1;主贴图;33;0;Create;False;0;0;0;False;0;False;1223;None;None;True;0;False;white;Auto;False;Instance;1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1066;5057.868,-6341.431;Inherit;False;2;2;0;FLOAT4x4;0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;335;1544.042,-2454.097;Inherit;False;334;depthfade_switch;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;332;1549.109,-2551.261;Inherit;False;126;depthfade;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;1238;-2496.109,360.1773;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;617;-5164.772,-4251.176;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.DynamicAppendNode;653;-1144.161,1993.392;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;768;457.2302,-1541.226;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;546;-8207.091,2615.418;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SmoothstepOpNode;32;-1538.659,-954.8051;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.MatrixFromVectors;887;-8223.322,956.2864;Inherit;False;FLOAT3x3;True;4;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3x3;0
Node;AmplifyShaderEditor.ClampOpNode;625;-5067.159,-3221.971;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;359;437.7584,-839.4825;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GrabScreenPosition;202;-998.173,2010.73;Inherit;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;147;-1878.385,3301.25;Inherit;False;fresnel;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;330;1813.327,-2508.759;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;1263;-2071.25,117.8709;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;1578;4536.337,-5888.917;Inherit;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;770;-4993.379,-4019.428;Inherit;False;Property;_Float82;遮罩01y轴Clamp;59;0;Create;False;0;0;0;True;1;SubToggle(g2, _);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;551;-7817.1,2444.744;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;346;1823.676,-2327.679;Inherit;False;Property;_Float30;边缘收窄;21;0;Create;False;0;0;0;False;1;Sub(g9);False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;619;-4984.984,-4169.254;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;1067;5155.086,-6474.872;Inherit;False;True;True;False;True;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;710;-7982.148,1414.472;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;213;658.145,-1232.839;Inherit;False;Property;_Float29;颜色混合（lerp模式）;54;0;Create;False;0;0;0;False;1;Sub(g11);False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldNormalVector;957;1123.602,-6086.494;Inherit;False;True;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ScaleAndOffsetNode;554;-7599.292,2049.103;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;1,1;False;2;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;212;593.9272,-1530.928;Inherit;True;Property;_Gradienttex;混合颜色贴图;47;0;Create;False;0;0;0;False;1;Ramp(g11);False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;190;-1030.067,2242.761;Inherit;True;Property;_reftex; 折射贴图（法线）;137;0;Create;False;0;0;0;False;1;Sub(g6);False;-1;None;None;True;0;False;white;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;889;-7978.904,838.1533;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT3x3;0,0,0,1,1,1,1,0,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;899;-7603.787,803.4982;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT3x3;0,0,0,1,1,1,1,0,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;620;-5005.937,-4399.004;Inherit;False;Property;_Float64;遮罩01x轴Clamp;58;0;Create;False;0;0;0;True;1;SubToggle(g2, _);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;774;-4936.102,-3140.36;Inherit;False;Property;_Float83;遮罩02y轴Clamp;67;0;Create;False;0;0;0;True;1;SubToggle(g2, _);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;626;-5040,-3504;Inherit;False;Property;_Float65;遮罩02x轴Clamp;66;0;Create;False;0;0;0;True;1;SubToggle(g2, _);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;624;-5062.679,-3354.133;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;1269;-1313.649,-1010.673;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;552;-7757.952,2678.026;Inherit;False;Property;_Float56;顶点偏移极坐标（竖向贴图）;123;0;Create;False;0;0;0;False;1;SubToggle(g5, _);False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;1228;1100.231,-2579.384;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ClampOpNode;618;-4980.503,-4301.416;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;1240;-2147.242,331.1862;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1007;1960.999,-5554.02;Inherit;False;pointlight;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldSpaceLightDirHlpNode;958;1079.013,-6273.463;Inherit;False;False;1;0;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;601;1389.232,-1908.883;Inherit;False;Property;_Float61;切换混合模式（默认lerp，勾上multiply）;53;0;Create;False;0;0;0;True;1;SubToggle(g11, _);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;1270;-1152.271,-895.2566;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;960;1297.528,-6246.394;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;773;-4737.102,-3251.36;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;714;-7577.378,1355.553;Inherit;False;Property;_Float75;顶点偏移遮罩极坐标;131;0;Create;False;0;0;0;False;1;SubToggle(g5, _);False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;771;-4683.732,-4071.075;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;396;-7422.272,2480.826;Inherit;False;Property;_Float45;顶点贴图旋转;124;0;Create;False;0;0;0;False;1;Sub(g5);False;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;772;-4800,-3424;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1011;1186.795,-5775.913;Inherit;False;Property;_Float137;切换为假点光（默认为平行光）;155;0;Create;False;0;0;0;True;1;SubToggle(g12, _);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NegateNode;1579;4784.33,-5880.727;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;211;1309.983,-2228.117;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SwitchByFaceNode;356;594.5223,-986.8137;Inherit;False;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WorldNormalVector;1591;4806.087,-5765.469;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.GetLocalVarNode;1136;661.5511,-572.6855;Inherit;False;147;fresnel;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;556;-7324.799,2186.685;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;343;1758.36,-2745.725;Inherit;False;Property;_Float28;边缘强度;20;0;Create;False;0;0;0;False;1;Sub(g9);False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;599;1334.414,-2044.998;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;185;-683.3252,2113.102;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;715;-7421.063,733.3063;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;1,1;False;2;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PowerNode;345;2008.769,-2465.928;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1009;1270.072,-5906.697;Inherit;False;1007;pointlight;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;1264;-1869.149,254.776;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;711;-7635.226,1120.972;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ColorNode;1142;615.0529,-841.6212;Inherit;False;Property;_Color6;外边缘颜色;28;1;[HDR];Create;False;0;0;0;False;1;Sub(g8);False;1,1,1,1;0,0.6419505,2.270603,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;1068;5307.213,-6383.365;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;1,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;769;-4761.483,-4387.489;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;557;-8191.453,2741.061;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ReflectOpNode;1580;5019.922,-5900.456;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;959;1428.072,-5962.867;Inherit;False;Property;_Float132;阴影范围;152;0;Create;False;0;0;0;False;1;Sub(g12);False;0.5;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;209;868.8461,-518.2598;Inherit;False;Constant;_Float27;Float 27;43;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;728;-7142.924,862.9124;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;1146;948.497,-789.8212;Inherit;False;Property;_Float145;开启外边缘;26;0;Create;False;0;0;0;False;1;SubToggle(g8, _);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PiNode;397;-7252.301,2544.294;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;1008;1509.587,-6124.007;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;168;-7070.666,2215.753;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1069;5446.203,-6339.315;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;718;-7409.397,1203.054;Inherit;False;Property;_Float77;顶点遮罩旋转;132;0;Create;False;0;0;0;False;1;Sub(g5);False;0;0;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1271;-1016.86,-789.223;Inherit;False;dis_soft_edge;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;1135;941.9438,-983.0773;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1241;-1549.453,277.2612;Inherit;False;dis_edge2;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenColorNode;183;-539.8561,2108.272;Inherit;False;Global;_GrabScreen0;Grab Screen 0;1;0;Create;True;0;0;0;False;0;False;Object;-1;False;False;False;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;133;-1629.695,71.82189;Inherit;False;dis_edge;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;341;2086.196,-2623.802;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;719;-7945.774,1552.875;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.LerpOp;600;1819.274,-2122.513;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;622;-4619.498,-3376.833;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;350;1028.062,-346.0324;Inherit;False;Property;_Float33;单独菲尼尔开关;25;1;[Enum];Create;False;0;2;off;0;on;1;0;False;1;KWEnum(g8,off,_0,on,_1);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;150;874.6943,-441.6401;Inherit;False;147;fresnel;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;616;-4625.501,-4236.024;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;403;628.6151,-1984.579;Inherit;False;Property;_maintex_alpha;主贴图通道;34;1;[Enum];Create;False;0;2;A;0;R;1;0;False;1;KWEnum(g1,A,_0,R,_1);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PiNode;729;-7070.427,1220.521;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1587;5279.597,-5941.925;Inherit;True;Property;_cubemap;cubemap;163;1;[NoScaleOffset];Create;True;0;0;0;False;1;Sub(g13);False;-1;None;None;True;0;False;white;LockedToCube;False;Object;-1;Auto;Cube;8;0;SAMPLERCUBE;;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;347;1417.91,-554.7366;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;962;1487.493,-5805.321;Inherit;False;Property;_Float133;阴影软硬;151;0;Create;False;0;0;0;False;1;Sub(g12);False;0.5;0.5;0.5;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1584;5320.241,-5750.698;Inherit;False;Property;_Float258;cube强度;164;0;Create;False;0;0;0;False;1;Sub(g13);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;414;104.0469,32.22958;Inherit;False;Property;_Float25;亮边溶解（默认关闭，勾上开启）;85;0;Create;False;0;0;0;True;1;SubToggle(g3, _);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;1244;1511.238,-1122.437;Inherit;False;Property;_Color7;二层亮边颜色（软边颜色）;89;1;[HDR];Create;False;0;0;0;False;1;Sub(g3);False;1,1,1,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1063;5579.651,-6394.174;Inherit;True;Property;_matcap;matcap;160;0;Create;False;0;0;0;False;1;Sub(g13);False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;402;941.9371,-2120.329;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;196;-344.1315,2114.849;Inherit;False;ref;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;134;1733.751,-841.4792;Inherit;False;133;dis_edge;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;961;1602.372,-5949.066;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1537;7402.15,8137.497;Inherit;False;Property;_Custom11;自定义顶点偏移强度;192;1;[Enum];Create;False;0;4;x2;0;y2;1;z2;2;w2;3;0;True;1;KWEnum(g7,custom2x,_0,custom2y,_1,custom2z,_2,custom2w,_3);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1536;7863.453,7437.766;Inherit;False;Constant;_Float243;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;132;1516.966,-1302.056;Inherit;False;Property;_Color1;一层亮边颜色;88;1;[HDR];Create;False;0;0;0;False;1;Sub(g3);False;1,1,1,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;218;-4446.376,-3619.329;Inherit;True;Property;_Mask1;遮罩02;64;0;Create;False;0;0;0;False;1;Sub(g2);False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;3;313.8168,-1184.052;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;1541;7806.125,8525.639;Inherit;False;Constant;_Float247;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;963;1650.76,-6109.127;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1539;7804.586,8439.102;Inherit;False;Constant;_Float245;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;725;-6901.163,887.341;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;8;-4431.885,-4113.886;Inherit;True;Property;_Mask;遮罩01;56;0;Create;False;0;0;0;False;1;Sub(g2);False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;1538;7866.509,8070.227;Inherit;False;Constant;_Float244;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1540;7816.346,7843.088;Inherit;False;Constant;_Float246;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1273;1872.243,-1015.599;Inherit;False;1271;dis_soft_edge;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1534;7850.891,7534.063;Inherit;False;Constant;_Float241;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1542;7814.809,7756.553;Inherit;False;Constant;_Float248;Float 162;180;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;398;-6992.47,2384.011;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;405;-4474.457,-3882.028;Inherit;False;Property;_mask01_alpha;遮罩01通道;57;1;[Enum];Create;False;0;2;A;0;R;1;0;False;1;KWEnum(g2,A,_0,R,_1);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;329;2152.206,-1987.192;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;1145;1125.195,-1076.013;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;408;-4254.527,-3393.536;Inherit;False;Property;_mask02_alpha;遮罩02通道;65;1;[Enum];Create;False;0;2;A;0;R;1;0;False;1;KWEnum(g2,A,_0,R,_1);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1243;1512.795,-891.9415;Inherit;False;1241;dis_edge2;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1535;7868.048,8156.764;Inherit;False;Constant;_Float242;Float 161;180;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1209;5511.886,-6171.244;Inherit;False;Property;_Float157;matcap去色;161;0;Create;False;0;0;0;False;1;Sub(g13);False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;1272;2114.976,-1044.661;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;1547;8014.048,8405.766;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;3;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;1403.827,-1380.657;Inherit;False;4;4;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;964;1842.893,-5865.257;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;965;1831.671,-6068.604;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;1543;8024.271,7723.216;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;1544;8075.971,8036.892;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;2;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;1545;7036.096,7433.162;Inherit;False;2;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;760;2134.208,-726.4012;Inherit;False;Property;_Color2;折射颜色;144;1;[HDR];Create;False;0;0;0;False;1;Sub(g6);False;1,1,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;365;720.6054,-1830.794;Inherit;False;Property;_Float34;主贴图细节对比度;40;0;Create;False;0;0;0;False;1;Sub(g1);False;1;4.74;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;1242;1899.037,-1204.633;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;1073;5581.222,-6075.475;Inherit;False;Property;_Float140;matcap强度;162;0;Create;False;0;0;0;False;1;Sub(g13);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;406;-4062.611,-4056.421;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;407;-4010.675,-3616.126;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;644;-7470.53,2730.82;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.AbsOpNode;1195;1041.387,-1929.269;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;726;-6810.596,1060.239;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.5,0.5;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ConditionalIfNode;1546;8051.224,7349.118;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DesaturateOpNode;1210;5953.866,-6341.064;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;199;2163.413,-845.39;Inherit;False;196;ref;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1583;5733.783,-5847.451;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SmoothstepOpNode;968;2030.494,-5846.112;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;124;-1655.204,-111.5786;Inherit;False;dis_bright;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;759;2422.249,-866.407;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;131;2262.146,-1298.924;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1072;5961.61,-6131.731;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;424;2546.575,-774.0239;Inherit;False;Property;_Float48;折射开关;136;1;[Enum];Create;False;0;2;off;0;on;1;0;True;1;KWEnum(g6,off,_0,on,_1);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;720;-7275.088,1513.887;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1549;8337.73,7602.189;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;220;-4120.95,-3831.159;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;370;723.0806,-1733.819;Inherit;False;Property;_Float37;主贴图细节提亮;41;0;Create;False;0;0;0;False;1;Sub(g1);False;1;6.18;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;646;-7236.695,2773.42;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;792;-7124.727,3010.527;Inherit;False;Property;_Float89;顶点偏移贴图y轴Clamp;122;0;Create;False;0;0;0;True;1;SubToggle(g5, _);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1548;8309.432,8288.91;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;645;-7277.174,2921.583;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;372;1160.966,-1810.068;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1550;8371.357,7920.036;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;122;-1366.799,-1143.595;Inherit;False;dis_soft;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1551;8322.455,7246.088;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;649;-7337.818,2667.41;Inherit;False;Property;_Float68;顶点偏移贴图x轴Clamp;121;0;Create;False;0;0;0;True;1;SubToggle(g5, _);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1593;5942.591,-5880.852;Inherit;False;cubemap;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;1552;8699.971,7795.159;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;1597;6171.667,-5992.443;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;790;-7037.636,2635.646;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;727;-6902.836,1372.223;Inherit;False;Property;_Float78;顶点偏移遮罩x轴Clamp;129;0;Create;False;0;0;0;True;1;SubToggle(g5, _);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;788;-6926.462,1866.367;Inherit;False;Property;_Float88;顶点偏移遮罩y轴Clamp;130;0;Create;False;0;0;0;True;1;SubToggle(g5, _);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;367;699.2643,-1624.499;Inherit;False;Property;_Float36;细节平滑过渡;42;0;Create;False;0;0;0;False;1;Sub(g1);False;1;0.903;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;373;1307.896,-1697.069;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;422;2763.613,-953.0879;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;721;-6959.221,1543.474;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;128;1332.733,51.57904;Inherit;False;126;depthfade;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;791;-6944.545,2871.763;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;125;145.3781,-127.8404;Inherit;False;124;dis_bright;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;966;2195.65,-1773.588;Inherit;False;Property;_Color4;暗部颜色;154;1;[HDR];Create;False;0;0;0;False;1;Sub(g12);False;0.490566,0.490566,0.490566,1;0.6132076,0.2690014,0.2690014,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;82;-3918.89,-3831.313;Inherit;False;Mask;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;976;2070.305,-1585.495;Inherit;False;Property;_Color5;亮部颜色;153;1;[HDR];Create;False;0;0;0;False;1;Sub(g12);False;1,1,1,1;0.6132076,0.2690014,0.2690014,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;340;1301.027,256.3226;Inherit;False;334;depthfade_switch;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;722;-6966.713,1728.636;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;123;137.3978,-211.4309;Inherit;False;122;dis_soft;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;971;2028.237,-6006.439;Inherit;False;lit;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;647;-6801.513,2590.72;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;972;2530.839,-1461.655;Inherit;False;971;lit;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;374;1557.611,-1708.646;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;789;-6633.962,1793.554;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;787;-6626.899,1464.961;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;413;572.2303,-168.7144;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;221;1767.981,217.0276;Inherit;False;82;Mask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;95;1801.246,-18.55526;Inherit;False;Property;_Float15;alpha强度;15;0;Create;False;0;0;0;False;1;Sub(g10);False;1;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1074;6188.272,-6149.159;Inherit;False;matcap;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1553;8891.1,7776.502;Inherit;False;custom11;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;967;2542.133,-1711.938;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;973;2557.35,-1568.673;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;338;1718.498,82.17676;Inherit;False;3;0;FLOAT;1;False;1;FLOAT;1;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1554;-6224.732,2790.292;Inherit;False;1553;custom11;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1208;2529.122,-1247.655;Inherit;False;Constant;_Float156;Float 156;175;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;169;-6513.891,2001.706;Inherit;True;Property;_vertextex;顶点偏移贴图;119;0;Create;False;0;0;0;False;1;Sub(g5);False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;179;-6505.771,3000.662;Inherit;False;Property;_Float22;custom控制顶点偏移强度;191;0;Create;False;0;0;0;False;3;SubToggle(g7, _);Space;Space;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NormalVertexDataNode;172;-6107.594,2204.864;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;1080;2523.905,-1011.148;Inherit;False;Property;_Float141;反射开关;159;1;[Enum];Create;False;0;2;off;0;on;1;0;True;1;KWEnum(g13,off,_0,on,_1);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;990;-6065.756,2457.289;Inherit;False;Property;_Float135;顶点法线;118;1;[Enum];Create;False;0;2;off;0;on;1;0;True;1;KWEnum(g5,off,_0,on,_1);False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;969;2740.619,-1671.403;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.Vector4Node;922;-6311.267,2194.702;Inherit;False;Property;_Vector32;顶点偏移贴图remap;120;0;Create;False;0;0;0;False;1;Sub(g5);False;0,1,0,1;0,1,0,1;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;994;-6053.791,2340.632;Inherit;False;Constant;_Float136;Float 136;151;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;978;2489.47,-1346.76;Inherit;False;Property;_Float134;阴影开关;147;1;[Enum];Create;False;0;2;off;0;20;1;0;True;1;KWEnum(g12,off,_0,on,_1);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;2431.051,-273.4656;Inherit;False;8;8;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;723;-6464.121,1596.634;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;1077;2432.954,-1154.188;Inherit;False;1074;matcap;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;178;-6306.146,2674.208;Inherit;False;Constant;_Float21;Float 21;37;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;993;-5799.169,2186.453;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.Vector3Node;175;-6037.788,2568.436;Inherit;False;Property;_Vector5;顶点偏移xyz强度;127;0;Create;False;0;0;0;False;1;Sub(g5);False;0,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.LerpOp;176;-5977.17,2740.609;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;602;2682.383,-313.9317;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;657;2660.212,-127.3093;Inherit;False;Property;_Float70;限制alpha值为0-1;16;0;Create;False;0;0;0;True;1;SubToggle(g10, _);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;921;-5983.323,2018.482;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;705;-6324.812,1746.057;Inherit;True;Property;_vertextex1;顶点偏移遮罩;128;0;Create;False;0;0;0;False;1;Sub(g5);False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;1078;2829.775,-1162.485;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;977;2835.611,-1400.601;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;673;2869.026,-83.7159;Inherit;False;Property;_Float72;alphaclip溶解（层级2000以下使用）;93;0;Create;False;0;0;0;False;1;Sub(g3);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;94;3174.455,-859.9865;Inherit;False;Property;_Float14;整体颜色强度;12;0;Create;False;0;0;0;False;1;Sub(g10);False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1207;3037.328,-1162.856;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;672;2917.155,-199.7776;Inherit;False;661;dis_tex;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;171;-5495.304,1970.702;Inherit;False;5;5;0;FLOAT;0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;656;2920.57,-401.8413;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClipNode;671;3197.834,-482.4088;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;181;-5004.712,2097.153;Inherit;False;vertexoffset;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;928;3431.201,-1013.647;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1232;-739.0544,-2042.138;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;419;-8390.636,-1666.76;Inherit;False;Property;_Noise_Group;扰动;94;0;Create;False;0;0;0;True;1;Main(g4, _, off, off);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1102;-8058.011,-2287.211;Inherit;False;Property;_Maintex_Group5;视差;165;0;Create;False;0;0;0;True;1;Main(g15, _, off, off);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;409;-8385.255,-1756.505;Inherit;False;Property;_Disolove_Group;溶解;72;0;Create;False;0;0;0;True;1;Main(g3, _, off, off);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;7;3756.721,-1015.099;Inherit;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;1089;-2270.936,-749.8066;Inherit;False;2;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;49;-3374.072,-964.5662;Inherit;False;2;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;194;-1965.401,2529.374;Inherit;True;2;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;13;-8090.629,-1763.14;Inherit;False;Property;_Float1;材质模式;1;0;Create;False;0;2;blend;10;add;1;0;True;2;SubEnum(g17,UnityEngine.Rendering.BlendMode);SubEnum(g17,add,1,blend,10);False;10;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;404;-8387.01,-1846.811;Inherit;False;Property;_Mask_Group;遮罩;55;0;Create;False;0;0;0;True;1;Main(g2, _, off, off);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;598;-8081.473,-1473.006;Inherit;False;Property;_Float60;colormask;5;0;Create;False;0;0;1;UnityEngine.Rendering.ColorWriteMask;True;1;SubEnum(g17,UnityEngine.Rendering.ColorWriteMask);False;15;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;401;-8390.813,-1934.699;Inherit;False;Property;_Maintex_Group;主贴图;31;0;Create;False;0;0;0;True;1;Main(g1, _, off, off);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;420;-8383.59,-1562.18;Inherit;False;Property;_Vertex_Group;顶点偏移;117;0;Create;False;0;0;0;True;1;Main(g5, _, off, off);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1585;5312.365,-5670.556;Inherit;False;Constant;_Float259;Float 259;196;0;Create;True;0;0;0;False;0;False;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;979;-8073.319,-1928.804;Inherit;False;Property;_Maintex_Group2;阴影;146;0;Create;False;0;0;0;True;1;Main(g12, _, off, off);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;428;-8434.165,-2171.328;Inherit;False;Property;_Color_Group2;颜色;10;0;Create;False;0;0;0;True;1;Main(g10, _, off, off);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1013;1058.646,-5391.43;Inherit;False;Property;_Float138;开启世界坐标;157;0;Create;False;0;0;0;False;1;SubToggle(g12, _);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-8072.098,-1379.883;Inherit;False;Property;_Float2;双面模式;2;0;Create;False;0;0;1;UnityEngine.Rendering.CullMode;True;1;SubEnum(g17,UnityEngine.Rendering.CullMode);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;1231;-627.6905,-2275.138;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TransformPositionNode;1000;614.6118,-5464.272;Inherit;False;Object;World;False;Fast;True;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;426;-8391.188,-1231.882;Inherit;False;Property;_Fresnel_Group2;菲尼尔;24;0;Create;False;0;0;0;True;1;Main(g8, _, off, off);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;1256;-2547.024,1077.807;Inherit;False;Constant;_Vector18;Vector 18;179;0;Create;True;0;0;0;False;0;False;0.5,0.5;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;303;-5883.209,5961.582;Inherit;True;2;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;1088;-8062.813,-2179.41;Inherit;False;Property;_Maintex_Group4;Flowmap;114;0;Create;False;0;0;0;True;1;Main(g14, _, off, off);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1081;-8079.145,-1853.214;Inherit;False;Property;_Maintex_Group3;Matcap&Cubemap;158;0;Create;False;0;0;0;True;1;Main(g13, _, off, off);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;39;-3028.452,-4419.989;Inherit;True;1;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;1255;-2326.024,1016.807;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;425;-8395.455,-1341.458;Inherit;False;Property;_Custom_Group1;custom控制;170;0;Create;False;0;0;0;True;1;Main(g7, _, off, off);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;182;3701.979,-819.6403;Inherit;False;181;vertexoffset;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;1012;1339.792,-5535.062;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;595;-8082.98,-1300.832;Inherit;False;Property;_Ztestmode1;stencil_comp;6;0;Create;False;0;0;1;UnityEngine.Rendering.CompareFunction;True;1;SubEnum(g17,UnityEngine.Rendering.CompareFunction);False;0;4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;997;1029.241,-5511.873;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;596;-8085.98,-1205.832;Inherit;False;Property;_Ztestmode2;stencil_pass;7;0;Create;False;0;0;1;UnityEngine.Rendering.StencilOp;True;1;SubEnum(g17,UnityEngine.Rendering.StencilOp);False;0;4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;74;-6707.094,-4562.272;Inherit;True;1;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;1258;-8480.2,-2311.889;Inherit;False;Property;_Color_Group3;基础设置;0;0;Create;False;0;0;0;True;1;Main(g17, _, off, off);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1262;-1512.465,-1222.628;Inherit;False;dis_00;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;167;-6594.58,2699.5;Inherit;True;2;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;427;-8382.333,-2040.575;Inherit;False;Property;_Depthfade_Group1;Depthfade;17;0;Create;False;0;0;0;True;1;Main(g9, _, off, off);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;597;-8087.689,-1123.75;Inherit;False;Property;_Float46;stencil_reference;8;0;Create;False;0;0;0;True;1;Sub(g17);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-8077.943,-1574.899;Inherit;False;Property;_Ztestmode;深度测试;4;0;Create;False;0;0;0;True;1;SubEnum(g17,UnityEngine.Rendering.CompareFunction);False;4;4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;421;-8392.288,-1444.528;Inherit;False;Property;_Ref_Group;折射;135;0;Create;False;0;0;0;True;1;Main(g6, _, off, off);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-8074.956,-1669.507;Inherit;False;Property;_Float4;深度写入;3;0;Create;False;0;0;1;UnityEngine.Rendering.CullMode;True;1;SubToggle(g17,_);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1126;572.0652,-2425.413;Inherit;False;maintexuv;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;1260;-7922.651,-1987.868;Inherit;False;Property;_Gradtex;颜色混合;45;0;Create;False;0;0;0;True;1;Main(g11, _, off, off);False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;4241.351,-1035.048;Float;False;True;-1;2;LWGUI.LWGUI;100;1;Hotwater/2024/All_GUI_0420;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;True;True;1;5;False;12;0;True;13;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;False;False;False;False;False;False;False;False;False;True;0;False;-1;True;True;2;True;22;True;True;True;True;True;True;0;True;598;False;False;False;False;False;False;True;True;True;255;True;597;255;False;-1;255;False;-1;7;True;595;1;True;596;1;False;-1;1;False;-1;7;True;595;1;True;596;1;False;-1;1;False;-1;True;True;2;True;20;True;0;True;21;True;False;0;False;-1;0;False;-1;True;2;RenderType=Opaque=RenderType;Queue=Transparent=Queue=0;True;2;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=ForwardBase;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;1;True;False;;False;0
WireConnection;930;0;929;0
WireConnection;876;0;871;0
WireConnection;876;1;529;3
WireConnection;876;2;872;0
WireConnection;878;0;529;4
WireConnection;878;1;873;0
WireConnection;878;2;874;0
WireConnection;877;0;876;0
WireConnection;877;1;878;0
WireConnection;877;2;875;0
WireConnection;535;3;528;1
WireConnection;535;4;528;2
WireConnection;866;0;863;0
WireConnection;866;1;684;3
WireConnection;866;2;864;0
WireConnection;865;0;684;4
WireConnection;865;1;862;0
WireConnection;865;2;861;0
WireConnection;941;0;51;0
WireConnection;941;1;943;0
WireConnection;941;2;942;0
WireConnection;880;0;535;0
WireConnection;880;1;877;0
WireConnection;531;0;528;3
WireConnection;531;1;528;4
WireConnection;868;0;866;0
WireConnection;868;1;865;0
WireConnection;868;2;867;0
WireConnection;682;3;676;1
WireConnection;682;4;676;2
WireConnection;879;0;941;0
WireConnection;879;1;877;0
WireConnection;938;0;680;0
WireConnection;938;1;940;0
WireConnection;938;2;939;0
WireConnection;869;0;938;0
WireConnection;869;1;868;0
WireConnection;679;0;676;3
WireConnection;679;1;676;4
WireConnection;870;0;682;0
WireConnection;870;1;868;0
WireConnection;536;0;879;0
WireConnection;536;1;531;0
WireConnection;538;0;880;0
WireConnection;538;2;531;0
WireConnection;683;0;869;0
WireConnection;683;1;679;0
WireConnection;694;0;870;0
WireConnection;694;2;679;0
WireConnection;539;0;536;0
WireConnection;539;1;538;0
WireConnection;539;2;540;0
WireConnection;530;0;529;1
WireConnection;530;1;529;2
WireConnection;688;0;683;0
WireConnection;688;1;694;0
WireConnection;688;2;685;0
WireConnection;687;0;684;1
WireConnection;687;1;684;2
WireConnection;394;0;393;0
WireConnection;53;0;539;0
WireConnection;53;2;530;0
WireConnection;395;0;53;0
WireConnection;395;2;394;0
WireConnection;697;0;688;0
WireConnection;697;2;687;0
WireConnection;695;0;696;0
WireConnection;638;0;395;0
WireConnection;698;0;697;0
WireConnection;698;2;695;0
WireConnection;1283;0;1284;0
WireConnection;1283;2;1286;0
WireConnection;1283;3;1285;0
WireConnection;1283;4;1286;0
WireConnection;1394;0;1384;0
WireConnection;1394;2;1389;0
WireConnection;1394;3;1387;0
WireConnection;1394;4;1389;0
WireConnection;1324;0;1335;0
WireConnection;1324;2;1326;0
WireConnection;1324;3;1319;0
WireConnection;1324;4;1326;0
WireConnection;1395;0;1384;0
WireConnection;1395;2;1382;0
WireConnection;1395;3;1383;0
WireConnection;1395;4;1382;0
WireConnection;1392;0;1384;0
WireConnection;1392;2;1388;0
WireConnection;1392;3;1386;0
WireConnection;1392;4;1388;0
WireConnection;1413;0;1421;0
WireConnection;1413;2;1408;0
WireConnection;1413;3;1406;0
WireConnection;1413;4;1408;0
WireConnection;639;0;638;1
WireConnection;1293;0;1284;0
WireConnection;1293;2;1294;0
WireConnection;1293;3;1295;0
WireConnection;1293;4;1294;0
WireConnection;1299;0;1284;0
WireConnection;1299;2;1298;0
WireConnection;1299;3;1296;0
WireConnection;1299;4;1298;0
WireConnection;1288;0;1284;0
WireConnection;1288;2;1289;0
WireConnection;1288;3;1290;0
WireConnection;1288;4;1289;0
WireConnection;1317;0;1335;0
WireConnection;1317;2;1328;0
WireConnection;1317;3;1329;0
WireConnection;1317;4;1328;0
WireConnection;1334;0;1335;0
WireConnection;1334;2;1325;0
WireConnection;1334;3;1327;0
WireConnection;1334;4;1325;0
WireConnection;1412;0;1421;0
WireConnection;1412;2;1409;0
WireConnection;1412;3;1404;0
WireConnection;1412;4;1409;0
WireConnection;1564;0;1563;0
WireConnection;1564;2;1562;0
WireConnection;1564;3;1560;0
WireConnection;1564;4;1562;0
WireConnection;689;0;698;0
WireConnection;1414;0;1421;0
WireConnection;1414;2;1402;0
WireConnection;1414;3;1403;0
WireConnection;1414;4;1402;0
WireConnection;1567;0;1563;0
WireConnection;1567;2;1557;0
WireConnection;1567;3;1555;0
WireConnection;1567;4;1557;0
WireConnection;1568;0;1563;0
WireConnection;1568;2;1559;0
WireConnection;1568;3;1561;0
WireConnection;1568;4;1559;0
WireConnection;1333;0;1335;0
WireConnection;1333;2;1323;0
WireConnection;1333;3;1331;0
WireConnection;1333;4;1323;0
WireConnection;1393;0;1384;0
WireConnection;1393;2;1390;0
WireConnection;1393;3;1385;0
WireConnection;1393;4;1390;0
WireConnection;1411;0;1421;0
WireConnection;1411;2;1407;0
WireConnection;1411;3;1405;0
WireConnection;1411;4;1407;0
WireConnection;640;0;638;0
WireConnection;1565;0;1563;0
WireConnection;1565;2;1558;0
WireConnection;1565;3;1556;0
WireConnection;1565;4;1558;0
WireConnection;1416;0;1410;3
WireConnection;1416;1;1411;0
WireConnection;1292;0;1281;3
WireConnection;1292;1;1293;0
WireConnection;1417;0;1410;1
WireConnection;1417;1;1414;0
WireConnection;1399;0;1391;4
WireConnection;1399;1;1394;0
WireConnection;1282;0;1281;1
WireConnection;1282;1;1283;0
WireConnection;1297;0;1281;4
WireConnection;1297;1;1299;0
WireConnection;690;0;689;0
WireConnection;1321;0;1330;2
WireConnection;1321;1;1317;0
WireConnection;1569;0;1566;4
WireConnection;1569;1;1568;0
WireConnection;1322;0;1330;3
WireConnection;1322;1;1333;0
WireConnection;1396;0;1391;2
WireConnection;1396;1;1393;0
WireConnection;1291;0;1281;2
WireConnection;1291;1;1288;0
WireConnection;1398;0;1391;1
WireConnection;1398;1;1395;0
WireConnection;780;0;638;1
WireConnection;780;1;639;0
WireConnection;780;2;779;0
WireConnection;1415;0;1410;2
WireConnection;1415;1;1412;0
WireConnection;1571;0;1566;3
WireConnection;1571;1;1565;0
WireConnection;1418;0;1410;4
WireConnection;1418;1;1413;0
WireConnection;1572;0;1566;1
WireConnection;1572;1;1567;0
WireConnection;1570;0;1566;2
WireConnection;1570;1;1564;0
WireConnection;691;0;689;1
WireConnection;1397;0;1391;3
WireConnection;1397;1;1392;0
WireConnection;1320;0;1330;1
WireConnection;1320;1;1324;0
WireConnection;778;0;638;0
WireConnection;778;1;640;0
WireConnection;778;2;643;0
WireConnection;1332;0;1330;4
WireConnection;1332;1;1334;0
WireConnection;641;0;778;0
WireConnection;641;1;780;0
WireConnection;1573;0;1572;0
WireConnection;1573;1;1570;0
WireConnection;1573;2;1571;0
WireConnection;1573;3;1569;0
WireConnection;776;0;689;0
WireConnection;776;1;691;0
WireConnection;776;2;777;0
WireConnection;1400;0;1398;0
WireConnection;1400;1;1396;0
WireConnection;1400;2;1397;0
WireConnection;1400;3;1399;0
WireConnection;1287;0;1282;0
WireConnection;1287;1;1291;0
WireConnection;1287;2;1292;0
WireConnection;1287;3;1297;0
WireConnection;1419;0;1417;0
WireConnection;1419;1;1415;0
WireConnection;1419;2;1416;0
WireConnection;1419;3;1418;0
WireConnection;1318;0;1320;0
WireConnection;1318;1;1321;0
WireConnection;1318;2;1322;0
WireConnection;1318;3;1332;0
WireConnection;775;0;689;0
WireConnection;775;1;690;0
WireConnection;775;2;692;0
WireConnection;1132;0;1128;1
WireConnection;1132;1;1128;2
WireConnection;693;0;775;0
WireConnection;693;1;776;0
WireConnection;1337;0;1318;0
WireConnection;1401;0;1400;0
WireConnection;1130;0;431;3
WireConnection;1130;1;431;4
WireConnection;1420;0;1419;0
WireConnection;50;1;641;0
WireConnection;1574;0;1573;0
WireConnection;1131;0;431;1
WireConnection;1131;1;431;2
WireConnection;1336;0;1287;0
WireConnection;811;0;500;4
WireConnection;811;1;809;0
WireConnection;811;2;807;0
WireConnection;923;0;50;1
WireConnection;923;1;924;1
WireConnection;923;2;924;2
WireConnection;923;3;924;3
WireConnection;923;4;924;4
WireConnection;564;1;693;0
WireConnection;810;0;814;0
WireConnection;810;1;500;3
WireConnection;810;2;808;0
WireConnection;1133;0;1132;0
WireConnection;1133;1;1131;0
WireConnection;1133;2;1130;0
WireConnection;799;0;796;0
WireConnection;799;1;439;3
WireConnection;799;2;798;0
WireConnection;803;0;439;4
WireConnection;803;1;802;0
WireConnection;803;2;801;0
WireConnection;813;0;810;0
WireConnection;813;1;811;0
WireConnection;813;2;812;0
WireConnection;567;0;564;1
WireConnection;567;1;923;0
WireConnection;506;3;499;1
WireConnection;506;4;499;2
WireConnection;1127;0;35;0
WireConnection;1127;1;1133;0
WireConnection;1127;2;1134;0
WireConnection;443;3;431;1
WireConnection;443;4;431;2
WireConnection;1480;0;1470;0
WireConnection;1480;2;1477;0
WireConnection;1480;3;1475;0
WireConnection;1480;4;1477;0
WireConnection;433;0;431;3
WireConnection;433;1;431;4
WireConnection;1425;0;1423;0
WireConnection;1425;1;1424;0
WireConnection;1479;0;1470;0
WireConnection;1479;2;1472;0
WireConnection;1479;3;1478;0
WireConnection;1479;4;1472;0
WireConnection;795;0;799;0
WireConnection;795;1;803;0
WireConnection;795;2;804;0
WireConnection;42;0;1338;0
WireConnection;42;1;1339;0
WireConnection;700;0;564;1
WireConnection;700;1;923;0
WireConnection;1575;1;1576;0
WireConnection;1575;2;1577;0
WireConnection;1482;0;1470;0
WireConnection;1482;2;1476;0
WireConnection;1482;3;1474;0
WireConnection;1482;4;1476;0
WireConnection;1481;0;1470;0
WireConnection;1481;2;1471;0
WireConnection;1481;3;1473;0
WireConnection;1481;4;1471;0
WireConnection;502;0;499;3
WireConnection;502;1;499;4
WireConnection;952;0;57;0
WireConnection;952;1;953;0
WireConnection;952;2;951;0
WireConnection;701;0;567;0
WireConnection;701;1;700;0
WireConnection;701;2;703;0
WireConnection;59;0;433;0
WireConnection;59;1;42;0
WireConnection;59;2;60;0
WireConnection;815;0;952;0
WireConnection;815;1;813;0
WireConnection;793;0;1127;0
WireConnection;793;1;795;0
WireConnection;1426;0;502;0
WireConnection;1426;1;1425;0
WireConnection;1426;2;1427;0
WireConnection;1485;0;1483;4
WireConnection;1485;1;1480;0
WireConnection;816;0;506;0
WireConnection;816;1;813;0
WireConnection;449;0;433;0
WireConnection;449;1;42;0
WireConnection;449;2;60;0
WireConnection;732;0;731;0
WireConnection;732;2;1575;0
WireConnection;806;0;443;0
WireConnection;806;1;795;0
WireConnection;1487;0;1483;1
WireConnection;1487;1;1479;0
WireConnection;1422;0;502;0
WireConnection;1422;1;1425;0
WireConnection;1422;2;1427;0
WireConnection;1484;0;1483;2
WireConnection;1484;1;1481;0
WireConnection;1486;0;1483;3
WireConnection;1486;1;1482;0
WireConnection;70;0;701;0
WireConnection;507;0;816;0
WireConnection;507;2;1422;0
WireConnection;509;0;815;0
WireConnection;509;1;1426;0
WireConnection;733;0;732;0
WireConnection;446;0;806;0
WireConnection;446;2;449;0
WireConnection;1488;0;1487;0
WireConnection;1488;1;1484;0
WireConnection;1488;2;1486;0
WireConnection;1488;3;1485;0
WireConnection;43;0;793;0
WireConnection;43;1;59;0
WireConnection;501;0;500;1
WireConnection;501;1;500;2
WireConnection;311;0;310;0
WireConnection;444;0;43;0
WireConnection;444;1;446;0
WireConnection;444;2;445;0
WireConnection;1489;0;1488;0
WireConnection;242;0;241;1
WireConnection;242;1;241;2
WireConnection;316;0;305;0
WireConnection;440;0;439;1
WireConnection;440;1;439;2
WireConnection;511;0;509;0
WireConnection;511;1;507;0
WireConnection;511;2;510;0
WireConnection;285;0;242;0
WireConnection;58;0;511;0
WireConnection;58;2;501;0
WireConnection;738;0;304;0
WireConnection;738;1;302;0
WireConnection;36;0;444;0
WireConnection;36;2;440;0
WireConnection;307;0;314;0
WireConnection;307;1;1490;0
WireConnection;307;2;317;0
WireConnection;851;0;848;0
WireConnection;851;1;439;3
WireConnection;851;2;847;0
WireConnection;852;0;439;4
WireConnection;852;1;849;0
WireConnection;852;2;846;0
WireConnection;161;0;36;0
WireConnection;737;0;58;0
WireConnection;737;1;738;0
WireConnection;309;0;737;0
WireConnection;309;1;306;0
WireConnection;309;2;307;0
WireConnection;853;0;851;0
WireConnection;853;1;852;0
WireConnection;853;2;850;0
WireConnection;1103;0;1095;0
WireConnection;460;1;226;0
WireConnection;460;3;459;1
WireConnection;460;4;459;2
WireConnection;1371;0;1368;0
WireConnection;1371;2;1365;0
WireConnection;1371;3;1363;0
WireConnection;1371;4;1365;0
WireConnection;1351;0;1348;0
WireConnection;1351;2;1345;0
WireConnection;1351;3;1343;0
WireConnection;1351;4;1345;0
WireConnection;1369;0;1368;0
WireConnection;1369;2;1367;0
WireConnection;1369;3;1362;0
WireConnection;1369;4;1367;0
WireConnection;457;0;459;3
WireConnection;457;1;459;4
WireConnection;854;0;226;0
WireConnection;854;1;853;0
WireConnection;92;0;309;0
WireConnection;1092;0;1121;0
WireConnection;1092;1;1094;0
WireConnection;1092;2;1103;0
WireConnection;1092;3;1096;0
WireConnection;1092;4;1104;0
WireConnection;1350;0;1348;0
WireConnection;1350;2;1346;0
WireConnection;1350;3;1340;0
WireConnection;1350;4;1346;0
WireConnection;1352;0;1348;0
WireConnection;1352;2;1341;0
WireConnection;1352;3;1344;0
WireConnection;1352;4;1341;0
WireConnection;455;0;459;1
WireConnection;1372;0;1368;0
WireConnection;1372;2;1361;0
WireConnection;1372;3;1364;0
WireConnection;1372;4;1361;0
WireConnection;360;0;55;0
WireConnection;360;2;1575;0
WireConnection;1370;0;1368;0
WireConnection;1370;2;1366;0
WireConnection;1370;3;1360;0
WireConnection;1370;4;1366;0
WireConnection;456;0;459;2
WireConnection;1349;0;1348;0
WireConnection;1349;2;1347;0
WireConnection;1349;3;1342;0
WireConnection;1349;4;1347;0
WireConnection;454;0;455;0
WireConnection;454;1;456;0
WireConnection;855;0;460;0
WireConnection;855;1;853;0
WireConnection;955;0;418;0
WireConnection;955;1;956;0
WireConnection;955;2;954;0
WireConnection;1354;0;1353;4
WireConnection;1354;1;1351;0
WireConnection;1097;0;1092;0
WireConnection;1017;0;1015;1
WireConnection;1017;1;1015;2
WireConnection;1017;2;1015;3
WireConnection;1357;0;1353;1
WireConnection;1357;1;1352;0
WireConnection;1377;0;1373;1
WireConnection;1377;1;1372;0
WireConnection;1376;0;1373;2
WireConnection;1376;1;1369;0
WireConnection;1375;0;1373;3
WireConnection;1375;1;1370;0
WireConnection;1355;0;1353;3
WireConnection;1355;1;1350;0
WireConnection;1374;0;1373;4
WireConnection;1374;1;1371;0
WireConnection;67;0;360;0
WireConnection;1356;0;1353;2
WireConnection;1356;1;1349;0
WireConnection;458;0;854;0
WireConnection;458;1;457;0
WireConnection;385;0;384;0
WireConnection;906;0;901;0
WireConnection;906;1;581;3
WireConnection;906;2;902;0
WireConnection;386;0;93;0
WireConnection;386;2;385;0
WireConnection;1452;0;1435;0
WireConnection;1452;2;1432;0
WireConnection;1452;3;1430;0
WireConnection;1452;4;1432;0
WireConnection;461;0;855;0
WireConnection;461;2;457;0
WireConnection;568;0;955;0
WireConnection;568;1;571;0
WireConnection;568;2;510;0
WireConnection;1455;0;1435;0
WireConnection;1455;2;1428;0
WireConnection;1455;3;1443;0
WireConnection;1455;4;1428;0
WireConnection;908;0;581;4
WireConnection;908;1;903;0
WireConnection;908;2;904;0
WireConnection;1203;0;1197;0
WireConnection;1203;1;1017;0
WireConnection;1454;0;1435;0
WireConnection;1454;2;1433;0
WireConnection;1454;3;1431;0
WireConnection;1454;4;1433;0
WireConnection;1453;0;1435;0
WireConnection;1453;2;1434;0
WireConnection;1453;3;1429;0
WireConnection;1453;4;1434;0
WireConnection;416;0;415;0
WireConnection;1358;0;1357;0
WireConnection;1358;1;1356;0
WireConnection;1358;2;1355;0
WireConnection;1358;3;1354;0
WireConnection;453;0;854;0
WireConnection;453;1;454;0
WireConnection;453;2;458;0
WireConnection;1378;0;1377;0
WireConnection;1378;1;1376;0
WireConnection;1378;2;1375;0
WireConnection;1378;3;1374;0
WireConnection;1458;0;1451;3
WireConnection;1458;1;1452;0
WireConnection;635;0;386;0
WireConnection;353;0;71;0
WireConnection;353;1;72;0
WireConnection;576;3;575;1
WireConnection;576;4;575;2
WireConnection;1099;0;162;0
WireConnection;1099;1;1100;0
WireConnection;1099;2;1101;0
WireConnection;907;0;906;0
WireConnection;907;1;908;0
WireConnection;907;2;905;0
WireConnection;1199;0;1203;0
WireConnection;1199;1;1017;0
WireConnection;1199;2;1200;0
WireConnection;1463;0;1451;1
WireConnection;1463;1;1455;0
WireConnection;1456;0;1451;4
WireConnection;1456;1;1454;0
WireConnection;451;0;453;0
WireConnection;451;1;461;0
WireConnection;451;2;1261;0
WireConnection;1457;0;1451;2
WireConnection;1457;1;1453;0
WireConnection;1379;0;1378;0
WireConnection;417;0;568;0
WireConnection;417;2;416;0
WireConnection;464;0;463;1
WireConnection;464;1;463;2
WireConnection;1359;0;1358;0
WireConnection;1257;1;417;0
WireConnection;99;0;313;0
WireConnection;99;1;100;2
WireConnection;99;2;318;0
WireConnection;833;0;830;0
WireConnection;833;1;484;3
WireConnection;833;2;829;0
WireConnection;633;0;635;1
WireConnection;632;0;635;0
WireConnection;229;0;451;0
WireConnection;229;2;464;0
WireConnection;1035;0;1199;0
WireConnection;1035;1;1027;0
WireConnection;910;0;576;0
WireConnection;910;1;907;0
WireConnection;834;0;484;4
WireConnection;834;1;831;0
WireConnection;834;2;828;0
WireConnection;824;0;479;4
WireConnection;824;1;821;0
WireConnection;824;2;818;0
WireConnection;354;0;1099;0
WireConnection;354;1;353;0
WireConnection;1465;0;1463;0
WireConnection;1465;1;1457;0
WireConnection;1465;2;1458;0
WireConnection;1465;3;1456;0
WireConnection;823;0;820;0
WireConnection;823;1;479;3
WireConnection;823;2;819;0
WireConnection;1173;0;1180;4
WireConnection;1173;1;1171;0
WireConnection;1173;2;1168;0
WireConnection;909;0;186;0
WireConnection;909;1;907;0
WireConnection;1172;0;1169;0
WireConnection;1172;1;1180;3
WireConnection;1172;2;1170;0
WireConnection;579;0;575;3
WireConnection;579;1;575;4
WireConnection;231;0;229;0
WireConnection;497;3;483;1
WireConnection;497;4;483;2
WireConnection;1503;0;1500;0
WireConnection;1503;2;1497;0
WireConnection;1503;3;1492;0
WireConnection;1503;4;1497;0
WireConnection;1057;0;1257;1
WireConnection;1057;1;1035;0
WireConnection;1057;2;1059;0
WireConnection;283;0;354;0
WireConnection;283;1;284;0
WireConnection;283;2;99;0
WireConnection;835;0;833;0
WireConnection;835;1;834;0
WireConnection;835;2;832;0
WireConnection;1502;0;1500;0
WireConnection;1502;2;1499;0
WireConnection;1502;3;1496;0
WireConnection;1502;4;1499;0
WireConnection;378;0;380;0
WireConnection;784;0;635;0
WireConnection;784;1;632;0
WireConnection;784;2;634;0
WireConnection;825;0;823;0
WireConnection;825;1;824;0
WireConnection;825;2;822;0
WireConnection;1466;0;1465;0
WireConnection;1175;0;1172;0
WireConnection;1175;1;1173;0
WireConnection;1175;2;1174;0
WireConnection;1505;0;1500;0
WireConnection;1505;2;1495;0
WireConnection;1505;3;1498;0
WireConnection;1505;4;1495;0
WireConnection;934;0;217;0
WireConnection;934;1;936;0
WireConnection;934;2;935;0
WireConnection;481;0;478;3
WireConnection;481;1;478;4
WireConnection;75;0;1380;0
WireConnection;75;1;1381;0
WireConnection;582;0;909;0
WireConnection;582;1;579;0
WireConnection;1504;0;1500;0
WireConnection;1504;2;1493;0
WireConnection;1504;3;1494;0
WireConnection;1504;4;1493;0
WireConnection;786;0;635;1
WireConnection;786;1;633;0
WireConnection;786;2;785;0
WireConnection;931;0;77;0
WireConnection;931;1;932;0
WireConnection;931;2;933;0
WireConnection;477;3;478;1
WireConnection;477;4;478;2
WireConnection;584;0;910;0
WireConnection;584;2;579;0
WireConnection;377;0;283;0
WireConnection;377;2;378;0
WireConnection;1509;0;1501;3
WireConnection;1509;1;1504;0
WireConnection;1062;0;918;0
WireConnection;827;0;477;0
WireConnection;827;1;825;0
WireConnection;631;0;784;0
WireConnection;631;1;786;0
WireConnection;1522;0;1532;0
WireConnection;1522;2;1517;0
WireConnection;1522;3;1513;0
WireConnection;1522;4;1517;0
WireConnection;585;0;582;0
WireConnection;585;1;584;0
WireConnection;585;2;586;0
WireConnection;486;0;483;3
WireConnection;486;1;483;4
WireConnection;580;0;581;1
WireConnection;580;1;581;2
WireConnection;474;0;481;0
WireConnection;474;1;75;0
WireConnection;474;2;73;0
WireConnection;1176;0;1175;0
WireConnection;1176;1;1179;0
WireConnection;836;0;934;0
WireConnection;836;1;835;0
WireConnection;1521;0;1532;0
WireConnection;1521;2;1519;0
WireConnection;1521;3;1516;0
WireConnection;1521;4;1519;0
WireConnection;826;0;931;0
WireConnection;826;1;825;0
WireConnection;1178;0;1180;1
WireConnection;1178;1;1180;2
WireConnection;1525;0;1532;0
WireConnection;1525;2;1520;0
WireConnection;1525;3;1514;0
WireConnection;1525;4;1520;0
WireConnection;1506;0;1501;2
WireConnection;1506;1;1505;0
WireConnection;1508;0;1501;1
WireConnection;1508;1;1503;0
WireConnection;1507;0;1501;4
WireConnection;1507;1;1502;0
WireConnection;735;0;734;0
WireConnection;735;2;1575;0
WireConnection;471;0;481;0
WireConnection;471;1;75;0
WireConnection;471;2;73;0
WireConnection;277;0;1057;0
WireConnection;1524;0;1532;0
WireConnection;1524;2;1515;0
WireConnection;1524;3;1518;0
WireConnection;1524;4;1515;0
WireConnection;837;0;497;0
WireConnection;837;1;835;0
WireConnection;1529;0;1523;2
WireConnection;1529;1;1524;0
WireConnection;915;0;912;4
WireConnection;1528;0;1523;4
WireConnection;1528;1;1521;0
WireConnection;736;0;735;0
WireConnection;334;0;333;0
WireConnection;496;0;837;0
WireConnection;496;2;486;0
WireConnection;382;0;381;0
WireConnection;97;1;98;0
WireConnection;97;0;96;0
WireConnection;1527;0;1523;1
WireConnection;1527;1;1522;0
WireConnection;1060;0;918;0
WireConnection;1060;1;1062;0
WireConnection;1060;2;1059;0
WireConnection;491;0;836;0
WireConnection;491;1;486;0
WireConnection;472;0;827;0
WireConnection;472;2;471;0
WireConnection;603;0;377;0
WireConnection;590;0;591;0
WireConnection;1177;0;1176;0
WireConnection;1177;2;1178;0
WireConnection;188;0;585;0
WireConnection;188;2;580;0
WireConnection;235;0;234;0
WireConnection;235;1;232;0
WireConnection;235;2;233;0
WireConnection;23;1;631;0
WireConnection;1510;0;1508;0
WireConnection;1510;1;1506;0
WireConnection;1510;2;1509;0
WireConnection;1510;3;1507;0
WireConnection;469;0;826;0
WireConnection;469;1;474;0
WireConnection;62;0;29;0
WireConnection;62;1;1469;0
WireConnection;62;2;61;0
WireConnection;1526;0;1523;3
WireConnection;1526;1;1525;0
WireConnection;327;0;97;0
WireConnection;605;0;603;1
WireConnection;604;0;603;0
WireConnection;754;2;752;0
WireConnection;754;0;756;0
WireConnection;754;1;753;0
WireConnection;351;0;136;0
WireConnection;351;1;352;0
WireConnection;1511;0;1510;0
WireConnection;1530;0;1527;0
WireConnection;1530;1;1529;0
WireConnection;1530;2;1526;0
WireConnection;1530;3;1528;0
WireConnection;913;0;62;0
WireConnection;913;1;915;0
WireConnection;913;2;914;0
WireConnection;589;0;188;0
WireConnection;589;2;590;0
WireConnection;383;0;235;0
WireConnection;383;2;382;0
WireConnection;495;0;491;0
WireConnection;495;1;496;0
WireConnection;495;2;494;0
WireConnection;485;0;484;1
WireConnection;485;1;484;2
WireConnection;916;0;23;1
WireConnection;916;1;281;0
WireConnection;916;2;1060;0
WireConnection;1147;1;1177;0
WireConnection;1147;5;1149;0
WireConnection;480;0;479;1
WireConnection;480;1;479;2
WireConnection;467;0;469;0
WireConnection;467;1;472;0
WireConnection;467;2;468;0
WireConnection;79;0;467;0
WireConnection;79;2;480;0
WireConnection;1225;0;1213;1
WireConnection;1225;1;1213;2
WireConnection;609;0;383;0
WireConnection;139;0;351;0
WireConnection;139;4;137;0
WireConnection;139;2;135;0
WireConnection;139;3;138;0
WireConnection;897;0;555;4
WireConnection;897;1;892;0
WireConnection;897;2;893;0
WireConnection;895;0;890;0
WireConnection;895;1;555;3
WireConnection;895;2;891;0
WireConnection;755;0;754;0
WireConnection;24;0;916;0
WireConnection;24;1;25;0
WireConnection;650;0;589;0
WireConnection;336;0;97;0
WireConnection;336;1;327;0
WireConnection;336;2;337;0
WireConnection;661;0;916;0
WireConnection;216;0;495;0
WireConnection;216;2;485;0
WireConnection;1531;0;1530;0
WireConnection;30;0;913;0
WireConnection;30;1;31;0
WireConnection;1598;0;1147;0
WireConnection;322;0;321;0
WireConnection;322;1;323;0
WireConnection;607;0;603;0
WireConnection;607;1;604;0
WireConnection;607;2;608;0
WireConnection;764;0;603;1
WireConnection;764;1;605;0
WireConnection;764;2;763;0
WireConnection;611;0;609;1
WireConnection;651;0;650;1
WireConnection;1267;0;106;0
WireConnection;26;0;24;0
WireConnection;26;1;30;0
WireConnection;140;0;139;0
WireConnection;896;0;895;0
WireConnection;896;1;897;0
WireConnection;896;2;894;0
WireConnection;1229;0;1230;0
WireConnection;1229;1;1225;0
WireConnection;391;0;390;0
WireConnection;757;0;755;0
WireConnection;757;1;336;0
WireConnection;949;0;173;0
WireConnection;949;1;950;0
WireConnection;949;2;948;0
WireConnection;319;0;79;0
WireConnection;319;1;322;0
WireConnection;652;0;650;0
WireConnection;606;0;607;0
WireConnection;606;1;764;0
WireConnection;320;0;322;0
WireConnection;320;1;216;0
WireConnection;388;0;387;0
WireConnection;610;0;609;0
WireConnection;1090;0;33;0
WireConnection;1090;1;1512;0
WireConnection;1090;2;1091;0
WireConnection;898;0;949;0
WireConnection;898;1;896;0
WireConnection;34;0;1090;0
WireConnection;767;0;609;1
WireConnection;767;1;611;0
WireConnection;767;2;766;0
WireConnection;1064;0;1599;0
WireConnection;126;0;757;0
WireConnection;886;0;881;0
WireConnection;886;1;716;3
WireConnection;886;2;882;0
WireConnection;141;0;140;0
WireConnection;549;3;544;1
WireConnection;549;4;544;2
WireConnection;888;0;716;4
WireConnection;888;1;883;0
WireConnection;888;2;884;0
WireConnection;1266;0;1239;0
WireConnection;1224;0;1229;0
WireConnection;1224;1;606;0
WireConnection;1227;0;606;0
WireConnection;1227;1;1229;0
WireConnection;389;0;320;0
WireConnection;389;2;388;0
WireConnection;765;0;609;0
WireConnection;765;1;610;0
WireConnection;765;2;612;0
WireConnection;45;0;26;0
WireConnection;193;0;192;0
WireConnection;193;1;1533;0
WireConnection;193;2;195;0
WireConnection;996;0;995;1
WireConnection;996;1;995;2
WireConnection;996;2;995;3
WireConnection;109;0;913;0
WireConnection;109;1;1267;0
WireConnection;392;0;319;0
WireConnection;392;2;391;0
WireConnection;781;0;650;0
WireConnection;781;1;652;0
WireConnection;781;2;655;0
WireConnection;130;0;1053;0
WireConnection;782;0;650;1
WireConnection;782;1;651;0
WireConnection;782;2;783;0
WireConnection;144;0;140;0
WireConnection;144;1;141;0
WireConnection;144;2;142;0
WireConnection;712;3;706;1
WireConnection;712;4;706;2
WireConnection;1;1;606;0
WireConnection;105;0;913;0
WireConnection;105;1;130;0
WireConnection;623;0;389;0
WireConnection;107;0;109;0
WireConnection;107;1;130;0
WireConnection;1001;0;996;0
WireConnection;1001;1;1002;0
WireConnection;1226;1;1227;0
WireConnection;191;0;201;0
WireConnection;191;1;193;0
WireConnection;900;0;549;0
WireConnection;900;1;898;0
WireConnection;946;0;707;0
WireConnection;946;1;947;0
WireConnection;946;2;945;0
WireConnection;1223;1;1224;0
WireConnection;1066;0;1065;0
WireConnection;1066;1;1064;0
WireConnection;1238;0;109;0
WireConnection;1238;1;1266;0
WireConnection;617;0;392;0
WireConnection;653;0;781;0
WireConnection;653;1;782;0
WireConnection;768;0;765;0
WireConnection;768;1;767;0
WireConnection;546;0;544;3
WireConnection;546;1;544;4
WireConnection;32;0;45;0
WireConnection;32;1;1090;0
WireConnection;32;2;34;0
WireConnection;887;0;886;0
WireConnection;887;1;888;0
WireConnection;887;2;885;0
WireConnection;625;0;623;1
WireConnection;359;0;4;0
WireConnection;359;1;357;0
WireConnection;359;2;358;0
WireConnection;147;0;144;0
WireConnection;330;1;332;0
WireConnection;330;2;335;0
WireConnection;1263;0;105;0
WireConnection;1263;1;107;0
WireConnection;551;0;898;0
WireConnection;551;1;546;0
WireConnection;619;0;617;1
WireConnection;1067;0;1066;0
WireConnection;710;0;706;3
WireConnection;710;1;706;4
WireConnection;957;0;1147;0
WireConnection;554;0;900;0
WireConnection;554;2;546;0
WireConnection;212;1;768;0
WireConnection;190;1;653;0
WireConnection;190;5;191;0
WireConnection;889;0;946;0
WireConnection;889;1;887;0
WireConnection;899;0;712;0
WireConnection;899;1;887;0
WireConnection;624;0;623;0
WireConnection;1269;0;45;0
WireConnection;1269;1;32;0
WireConnection;1228;0;1226;1
WireConnection;1228;1;1;2
WireConnection;1228;2;1223;3
WireConnection;618;0;617;0
WireConnection;1240;0;1238;0
WireConnection;1240;1;130;0
WireConnection;1007;0;1001;0
WireConnection;1270;0;1269;0
WireConnection;960;0;958;0
WireConnection;960;1;957;0
WireConnection;773;0;623;1
WireConnection;773;1;625;0
WireConnection;773;2;774;0
WireConnection;771;0;617;1
WireConnection;771;1;619;0
WireConnection;771;2;770;0
WireConnection;772;0;623;0
WireConnection;772;1;624;0
WireConnection;772;2;626;0
WireConnection;1579;0;1578;0
WireConnection;211;0;1228;0
WireConnection;211;1;212;0
WireConnection;211;2;213;0
WireConnection;356;0;4;0
WireConnection;356;1;359;0
WireConnection;1591;0;1599;0
WireConnection;556;0;551;0
WireConnection;556;1;554;0
WireConnection;556;2;552;0
WireConnection;599;0;1228;0
WireConnection;599;1;212;0
WireConnection;185;0;202;0
WireConnection;185;1;190;0
WireConnection;715;0;899;0
WireConnection;715;2;710;0
WireConnection;345;0;330;0
WireConnection;345;1;346;0
WireConnection;1264;0;1263;0
WireConnection;1264;1;1240;0
WireConnection;711;0;889;0
WireConnection;711;1;710;0
WireConnection;1068;0;1067;0
WireConnection;769;0;617;0
WireConnection;769;1;618;0
WireConnection;769;2;620;0
WireConnection;557;0;555;1
WireConnection;557;1;555;2
WireConnection;1580;0;1579;0
WireConnection;1580;1;1591;0
WireConnection;728;0;711;0
WireConnection;728;1;715;0
WireConnection;728;2;714;0
WireConnection;397;0;396;0
WireConnection;1008;0;960;0
WireConnection;1008;1;1009;0
WireConnection;1008;2;1011;0
WireConnection;168;0;556;0
WireConnection;168;2;557;0
WireConnection;1069;0;1068;0
WireConnection;1271;0;1270;0
WireConnection;1135;0;356;0
WireConnection;1135;1;1142;0
WireConnection;1135;2;1136;0
WireConnection;1241;0;1264;0
WireConnection;183;0;185;0
WireConnection;133;0;1263;0
WireConnection;341;0;343;0
WireConnection;341;1;345;0
WireConnection;719;0;716;1
WireConnection;719;1;716;2
WireConnection;600;0;211;0
WireConnection;600;1;599;0
WireConnection;600;2;601;0
WireConnection;622;0;772;0
WireConnection;622;1;773;0
WireConnection;616;0;769;0
WireConnection;616;1;771;0
WireConnection;729;0;718;0
WireConnection;1587;1;1580;0
WireConnection;347;0;209;0
WireConnection;347;1;150;0
WireConnection;347;2;350;0
WireConnection;1063;1;1069;0
WireConnection;402;0;1;4
WireConnection;402;1;1;1
WireConnection;402;2;403;0
WireConnection;196;0;183;0
WireConnection;961;0;959;0
WireConnection;218;1;622;0
WireConnection;963;0;1008;0
WireConnection;725;0;728;0
WireConnection;725;2;719;0
WireConnection;8;1;616;0
WireConnection;398;0;168;0
WireConnection;398;2;397;0
WireConnection;329;0;341;0
WireConnection;329;1;600;0
WireConnection;1145;0;356;0
WireConnection;1145;1;1135;0
WireConnection;1145;2;1146;0
WireConnection;1272;0;1273;0
WireConnection;1272;1;134;0
WireConnection;1272;2;414;0
WireConnection;1547;0;1537;0
WireConnection;1547;2;1539;0
WireConnection;1547;3;1541;0
WireConnection;1547;4;1539;0
WireConnection;5;0;329;0
WireConnection;5;1;3;0
WireConnection;5;2;1145;0
WireConnection;5;3;347;0
WireConnection;964;0;962;0
WireConnection;965;0;963;0
WireConnection;965;1;961;0
WireConnection;1543;0;1537;0
WireConnection;1543;2;1542;0
WireConnection;1543;3;1540;0
WireConnection;1543;4;1542;0
WireConnection;1544;0;1537;0
WireConnection;1544;2;1538;0
WireConnection;1544;3;1535;0
WireConnection;1544;4;1538;0
WireConnection;1242;0;1244;0
WireConnection;1242;1;132;0
WireConnection;1242;2;1243;0
WireConnection;406;0;8;4
WireConnection;406;1;8;1
WireConnection;406;2;405;0
WireConnection;407;0;218;4
WireConnection;407;1;218;1
WireConnection;407;2;408;0
WireConnection;644;0;398;0
WireConnection;1195;0;402;0
WireConnection;726;0;725;0
WireConnection;726;2;729;0
WireConnection;1546;0;1537;0
WireConnection;1546;2;1536;0
WireConnection;1546;3;1534;0
WireConnection;1546;4;1536;0
WireConnection;1210;0;1063;0
WireConnection;1210;1;1209;0
WireConnection;1583;0;1587;0
WireConnection;1583;1;1584;0
WireConnection;968;0;965;0
WireConnection;968;1;964;0
WireConnection;968;2;962;0
WireConnection;124;0;105;0
WireConnection;759;0;199;0
WireConnection;759;1;760;0
WireConnection;759;2;3;0
WireConnection;131;0;5;0
WireConnection;131;1;1242;0
WireConnection;131;2;1272;0
WireConnection;1072;0;1210;0
WireConnection;1072;1;1073;0
WireConnection;720;0;726;0
WireConnection;1549;0;1545;2
WireConnection;1549;1;1543;0
WireConnection;220;0;406;0
WireConnection;220;1;407;0
WireConnection;646;0;644;0
WireConnection;1548;0;1545;4
WireConnection;1548;1;1547;0
WireConnection;645;0;644;1
WireConnection;372;0;1195;0
WireConnection;372;1;365;0
WireConnection;1550;0;1545;3
WireConnection;1550;1;1544;0
WireConnection;122;0;32;0
WireConnection;1551;0;1545;1
WireConnection;1551;1;1546;0
WireConnection;1593;0;1583;0
WireConnection;1552;0;1551;0
WireConnection;1552;1;1549;0
WireConnection;1552;2;1550;0
WireConnection;1552;3;1548;0
WireConnection;1597;0;1072;0
WireConnection;1597;1;1593;0
WireConnection;790;0;644;0
WireConnection;790;1;646;0
WireConnection;790;2;649;0
WireConnection;373;0;372;0
WireConnection;373;1;370;0
WireConnection;422;0;131;0
WireConnection;422;1;759;0
WireConnection;422;2;424;0
WireConnection;721;0;720;0
WireConnection;791;0;644;1
WireConnection;791;1;645;0
WireConnection;791;2;792;0
WireConnection;82;0;220;0
WireConnection;722;0;720;1
WireConnection;971;0;968;0
WireConnection;647;0;790;0
WireConnection;647;1;791;0
WireConnection;374;0;402;0
WireConnection;374;1;373;0
WireConnection;374;2;367;0
WireConnection;789;0;720;1
WireConnection;789;1;722;0
WireConnection;789;2;788;0
WireConnection;787;0;720;0
WireConnection;787;1;721;0
WireConnection;787;2;727;0
WireConnection;413;0;123;0
WireConnection;413;1;125;0
WireConnection;413;2;414;0
WireConnection;1074;0;1597;0
WireConnection;1553;0;1552;0
WireConnection;967;0;966;0
WireConnection;967;1;422;0
WireConnection;973;0;976;0
WireConnection;973;1;422;0
WireConnection;338;0;128;0
WireConnection;338;2;340;0
WireConnection;169;1;647;0
WireConnection;969;0;967;0
WireConnection;969;1;973;0
WireConnection;969;2;972;0
WireConnection;6;0;374;0
WireConnection;6;1;3;4
WireConnection;6;2;4;4
WireConnection;6;3;413;0
WireConnection;6;4;95;0
WireConnection;6;5;338;0
WireConnection;6;6;221;0
WireConnection;6;7;347;0
WireConnection;723;0;787;0
WireConnection;723;1;789;0
WireConnection;993;0;994;0
WireConnection;993;1;172;0
WireConnection;993;2;990;0
WireConnection;176;0;178;0
WireConnection;176;1;1554;0
WireConnection;176;2;179;0
WireConnection;602;0;6;0
WireConnection;921;0;169;1
WireConnection;921;1;922;1
WireConnection;921;2;922;2
WireConnection;921;3;922;3
WireConnection;921;4;922;4
WireConnection;705;1;723;0
WireConnection;1078;0;1208;0
WireConnection;1078;1;1077;0
WireConnection;1078;2;1080;0
WireConnection;977;0;422;0
WireConnection;977;1;969;0
WireConnection;977;2;978;0
WireConnection;1207;0;977;0
WireConnection;1207;1;1078;0
WireConnection;171;0;921;0
WireConnection;171;1;993;0
WireConnection;171;2;175;0
WireConnection;171;3;176;0
WireConnection;171;4;705;1
WireConnection;656;0;6;0
WireConnection;656;1;602;0
WireConnection;656;2;657;0
WireConnection;671;0;656;0
WireConnection;671;1;672;0
WireConnection;671;2;673;0
WireConnection;181;0;171;0
WireConnection;928;0;1207;0
WireConnection;928;1;94;0
WireConnection;1232;0;284;0
WireConnection;1232;1;313;0
WireConnection;7;0;928;0
WireConnection;7;3;671;0
WireConnection;1231;0;354;0
WireConnection;1231;1;1232;0
WireConnection;1255;0;568;0
WireConnection;1255;1;1256;0
WireConnection;1012;0;997;0
WireConnection;1012;1;996;0
WireConnection;1012;2;1013;0
WireConnection;997;0;996;0
WireConnection;997;1;1000;0
WireConnection;1262;0;45;0
WireConnection;1126;0;606;0
WireConnection;0;0;7;0
WireConnection;0;1;182;0
ASEEND*/
//CHKSM=373AD62E6392E791302B2446F60FF7840BFEEB21