//XE@SJOnline

Shader "XE/Unlit-DoubleSide-Alpha" {
	Properties {
		_Color ("Main Color", Color) = (0.5,0.5,0.5,1.0)	
		_AlphaCutoff ("AlphaCutoff", Range (0, 1)) = 0.5		
		_MainTex ("Base (RGB)", 2D) = "white" { }
		_HitColor ("HitColor(Black)", Color) = (0.0, 0.0, 0.0, 0.0)
	}
	
	SubShader {
		Tags { "RenderType"="TransparentCutout" "Queue"="Geometry+460"}

		Pass {
			Material {
				Diffuse [_Color]
				Ambient [_Color]				
			}
			Lighting OFF
			AlphaTest Greater [_AlphaCutoff]
			Cull Off			
			SetTexture [_MainTex] {
				constantColor [_Color]
				Combine texture * constant DOUBLE
			}
			SetTexture [_MainTex] {
				constantColor [_HitColor]
				Combine previous + constant
			}
		}
	}
	FallBack "Diffuse"
}
