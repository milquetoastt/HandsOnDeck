Shader "Outlined/Uniform"
{
    Properties
    {
        _Outline ("Outline width", Float) = 0.03
        _OutlineColor ("Outline Color", Color) = (1,0,0,1)
    }

    SubShader
    {
        Tags { "Queue"="Transparent" }

        Pass
        {
            Cull Front
            ZWrite Off
            ColorMask RGB

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            float _Outline;
            float4 _OutlineColor;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            v2f vert(appdata v)
            {
                float3 norm = normalize(v.normal);
                float4 pos = v.vertex;
                pos.xyz += norm * _Outline;
                v2f o;
                o.pos = UnityObjectToClipPos(pos);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return _OutlineColor;
            }
            ENDCG
        }
    }
}