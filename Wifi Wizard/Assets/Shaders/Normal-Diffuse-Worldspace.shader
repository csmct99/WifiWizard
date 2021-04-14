// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Legacy Shaders/Diffuse Worldspace" {
Properties {
    _Color ("Main Color", Color) = (1,1,1,1)
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _Scale("Texture Scale", Float) = 1.0
}
SubShader {
    Tags { "RenderType"="Opaque" }
    LOD 200

CGPROGRAM
#pragma surface surf Lambert

sampler2D _MainTex;
fixed4 _Color;
float _Scale;

struct Input {
    float3 worldNormal;
    float3 worldPos;
};

// https://www.youtube.com/watch?v=s79Zu0F8fuY
void surf (Input IN, inout SurfaceOutput o) {
    float2 UV;
    fixed4 c;

    if (abs(IN.worldNormal.x) > 0.5)
    {
        // side
        UV = IN.worldPos.yz;
        c = tex2D(_MainTex, UV * _Scale);
    }
    else if (abs(IN.worldNormal.z) > 0.5)
    {
        // front
        UV = IN.worldPos.xy;
        c = tex2D(_MainTex, UV * _Scale);
    }
    else
    {
        // top
        UV = IN.worldPos.xz;
        c = tex2D(_MainTex, UV * _Scale);
    }

    o.Albedo = c.rgb * _Color;
}
ENDCG
}

Fallback "Legacy Shaders/VertexLit"
}
