// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/ZoneShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,0.1)
    }
    SubShader
    {
        Tags{ "Queue" = "AlphaTest" "RenderType" = "TransparentCutout" "IgnoreProjector" = "True" }
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 200
        ZWrite Off
        CGPROGRAM

        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        struct Input
        {
            float3 worldPos;
            float3 worldNormal;
        };

        fixed4 _Color;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float height = IN.worldPos.y;
            float3 normal = IN.worldNormal;

            // Color if the normal is pointing up
            o.Albedo = _Color;
            o.Alpha = normal.y;
        }
        ENDCG
    }
}
