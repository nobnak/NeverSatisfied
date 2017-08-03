Shader "Hidden/NeverSatisfied" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
        _TargetTex ("Target Tex", 2D) = "black" {}
        _Lasting ("Params", Vector) = (1, 1, 0, 0)
	}
	SubShader {
		Cull Off ZWrite Off ZTest Always

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
            #pragma multi_compile __ TRANSPARENT
			
			#include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _TargetTex;
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
				float4 csrc = tex2D(_MainTex, i.uv);
                float4 ctar = tex2D(_TargetTex, i.uv);

                #ifdef TRANSPARENT
                csrc = float4(csrc.rgb, (1.0 - _Lasting.y * unity_DeltaTime.x) * csrc.a);
                #else
                csrc = (1.0 - _Lasting.y * unity_DeltaTime.x) * csrc;
                #endif

                return lerp(csrc, float4(ctar.rgb, 1), saturate(_Lasting.x * ctar.a));
			}
			ENDCG
		}
	}
}
