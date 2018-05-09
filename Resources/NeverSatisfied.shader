Shader "Hidden/NeverSatisfied" {
	Properties {
		_MainTex ("Current Texture", 2D) = "white" {}
        _SourceTex ("Source Texture", 2D) = "black" {}
        _Lasting ("Params", Vector) = (1, 1, 0, 0)
	}
	SubShader {
		Cull Off ZWrite Off ZTest Always

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _SourceTex;
            float4 _Lasting;

			struct appdata {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};
			struct v2f {
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v) {
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			float4 frag (v2f i) : SV_Target {
				float4 ccur = tex2D(_MainTex, i.uv);
                float4 csrc = tex2D(_SourceTex, i.uv);

                ccur *= (1.0 - _Lasting.x);
                ccur = lerp(ccur, float4(csrc.rgb, 1), saturate(_Lasting.y * csrc.a));

				return ccur;
			}
			ENDCG
		}
	}
}
