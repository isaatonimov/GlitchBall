Shader "Custom/Dematerialize" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _BumpMap ("Bumpmap", 2D) = "bump" {}
        _MainColor ("Main Color", Color) = (1.0,1.0,1.0,1.0)
        _RimColor ("Rim Color", Color) = (1.0,1.0,1.0,1.0)
        _Amount ("Amount", Range(0.0, 1.0)) = 0
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf SimpleLambert

        sampler2D _MainTex;
        sampler2D _BumpMap;
        float4 _MainColor;
        float4 _RimColor;
        float _Amount;

        half4 LightingSimpleLambert (SurfaceOutput s, half3 lightDir, half atten) {
            half NdotL = dot (s.Normal, lightDir);
            half4 c;
            half t = (NdotL * atten * 2);
            t = lerp(t, 1, _Amount);
            c.rgb = s.Albedo * _LightColor0.rgb * t;
            c.a = s.Alpha;
            return c;
        }

        struct Input {
            float2 uv_MainTex;
            float2 uv_BumpMap;
            float3 viewDir;
        };

        void surf (Input IN, inout SurfaceOutput o) {
            half4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb*(1-_Amount) + (_MainColor*_Amount);
            o.Alpha = c.a;
            o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
            half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
            o.Emission = _RimColor.rgb * lerp(0, pow(rim, _Amount), _Amount);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
