Shader "Custom/ZoneShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,0.1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        #define _WorldSpaceNormal [[user:WorldNormal]]

        #pragma target 3.0

        fixed4 _Color;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            o.Albedo = IN.WorldPosition.zyx * _Color;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
