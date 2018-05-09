Shader "Unlit/Rainbow (Transparent)" {
	Properties {
        _Freq ("Color Freq", Vector) = (1, 1, 1, 1)
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		LOD 100
		Blend SrcAlpha OneMinusSrcAlpha

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
            #define TWO_PI 6.283185            

            float4 _Freq;

			struct appdata {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f {
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
                float4 clipPos : TEXCOORD1;
			};

            float3 H2RGB(float3 c) {
                float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
                float3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
                return saturate(p - K.xxx);
            }

			v2f vert (appdata v) {
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
                o.clipPos = o.vertex;
				o.uv = v.uv;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target {
                float t = _Time.y;
                return float4(H2RGB(t * _Freq.x) * _Freq.y, _Freq.z);

                //float3 c = i.clipPos.xyz / i.clipPos.w;
                //c.xy = 0.5 * (c.xy + 1.0);
                //return float4(c.xy, 0, 1);
			}
			ENDCG
		}
	}
}
