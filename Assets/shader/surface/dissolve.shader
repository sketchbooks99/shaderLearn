Shader "Custom/dissolve" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
        _Threshold ("Threshold", Range(0,1)) = 0.0
 	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
            float2 uv_Detail;
            float3 worldPos;
        };

		half _Glossiness;
		half _Metallic;
        half _Threshold;
		fixed4 _Color;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

        // random 
        float random(float2 P) {
            return frac(sin(dot(P.xy, float2(12.9898,78.233))) + 43758.5453123);
        }

        // perlin noise
        float pnoise(float2 P) {
            float2 i = floor(P);
            float2 f = frac(P);

            float a = random(i);
            float b = random(i+float2(1.0, 0.0));
            float c = random(i+float2(0.0, 1.0));
            float d = random(i+float2(1.0, 1.0));

            float2 u = f*f*(3.0-2.0*f);

            return lerp(a,b,u.x) + (c-a)*u.y*(1.0-u.x) + (d-b)*u.x*u.y;
        }

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
            // float thres = pnoise(float2(IN.worldPos.z*13.3, IN.worldPos.x*16.9));
            float thres = pnoise(IN.worldPos.xy*IN.worldPos.yz*float2(35.87, 50.48));
            float t = abs(pow(sin(_Time*20.0), 5));
            fixed4 c = fixed4(0,0,0,0);
            if(thres < t) {
                discard;
            } else if(thres - t < 0.10 && t > 0.10) {
                c = fixed4(1.0, 0.6, 0.8, 1.0);
            } else if(thres - t < 0.20 && t > 0.20){
                c = fixed4(0.4, 0.7, 1.0, 1.0);
            } else {
                c = fixed4(1.0, 1.0, 0.7, 1.0);
            }
            o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
