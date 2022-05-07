Shader "Wills Shaders/DissolveHueChange"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Noise", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Threshold ("Threshold", Range(0,1)) = 0.5
        _TimeOffset ("Time Offset", Float) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        half _TimeOffset;
        fixed4 _Color;
        float _Threshold;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            
            float4 noise = tex2D(_MainTex, IN.uv_MainTex);
            
            // Albedo comes from a texture tinted by color
            fixed4 c = fixed4(sin(_Time.y) + _TimeOffset, cos(_Time.y) + _TimeOffset, sin(_Time.z), 1);
            float t = sin(_Time.y + _TimeOffset) * .5 + .5;
            if(noise.r > t)
            {
                c = float4(0,0,0,0);
                clip(-1); //kills pixels
            }


            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
            if(noise.r > t - .1 && noise.r < t)
            {
            o.Emission = float4((1-c.r), (1-c.g), (1-c.b), 1);

            }
        }
        ENDCG
    }
    FallBack "Diffuse"
}