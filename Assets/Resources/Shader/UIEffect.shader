Shader "Rabi/UIEffect"
{
    Properties
    {
        [PerRendererData] _MainTex("Sprite Texture",2D) = "white" {}
        [HRD] _TintColor("Tint", Color) = (1,1,1,1)
        _StencilComp("Stencil Comparison",Float) = 8
        _Stencil("Stencil ID",Float) = 0
        _StencilOp("Stencil Operation", Float) = 0
        _StencilWriteMask("Stencil Write Mask", Float) = 255
        _StencilReadMask("Stencil Read Mask", Float) = 255
        _ColorMask("Color Mask", Float) = 15
        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip("Use Alpha Clip", Float) = 0
        _SrcBlend("Alpha Source Blend", Float) = 5.0
        _DstBlend("Alpha Destination Blend", Float) = 10.0
        [NoScaleOffset] _OverlayTex("Overlay Texture", 2D) = "white" {}
        _OverlaySpeed("Overlay Texture Speed", vector) = (0,0,0,0)
        _BlurDistance("Blur Distance", Float) = 0.015
        _GrayLerp("Gray Scale Lerp", Float) = 1
    }
    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend [_SrcBlend] [_DstBlend]
        ColorMask [_ColorMask]

        Pass
        {
            Name "Default"
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "Lighting.cginc"

            #pragma multi_compile __ UNITY_UI_CLIP_RECT
            #pragma multi_compile __ UNITY_UI_ALPHACLIP
            #pragma multi_compile __ UNITY_UI_UIEFFECT_OVERLAY
            #pragma multi_compile __ UNITY_UI_OVERLAY_ANIMATION
            #pragma multi_compile __ UNITY_UI_OVERLAY_TINT
            #pragma multi_compile __ UNITY_UI_BLUR
            #pragma multi_compile __ UNITY_UI_GRAYSCALE UIEFFECT_GRAYSCALE_LERP

            inline float UnityGet2DClipping (in float2 position, in float4 clipRect)
            {
                float2 inside = step(clipRect.xy, position.xy) * step(position.xy, clipRect.zw);
                return inside.x * inside.y;
            }

            struct appdata_t
            {
                float4 vertex : POSITION;
                fixed4 color : COLOR;
                half2 texcoord : TEXCOORD0;
#if defined(UIEFFECT_OVERLAY)
                half2 texcoord1 : TEXCOORD1;
#endif
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
#if defined(UIEFFECT_OVERLAY)
                half4 texcoord : TEXCOORD0;
#else
                half2 texcoord : TEXCOORD0;
#endif
                float4 worldPosition : TEXCOORD2;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            fixed4 _TintColor;
            sampler2D _MainTex;
            fixed4 _TextureSampleAdd;
            float4 _ClipRect;

            sampler2D _OverlayTex;
            fixed4 _OverlayTint;
            int _OverlayColorMode;
            half2 _OverlaySpeed;
            half _BlurDistance;
            half _GrayLerp;


            v2f vert(appdata_t v)
            {
                v2f OUT;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
                OUT.worldPosition = v.vertex;
                OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);
#if defined(UIEFFECT_OVERLAY)
                OUT.texcoord.xy = v.texcoord;
                OUT.texcoord.zw = v.texcoord1;
#ifdef UIEFFECT_OVERLAY_ANIMATION
                OUT.texcoord.zw += _Time.y * _OverlaySpeed;
#endif
#else
                OUT.texcoord = v.texcoord;
#endif
                
                OUT.color = v.color * _TintColor;
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                fixed4 color = (tex2D(_MainTex, IN.texcoord.xy) + _TextureSampleAdd) * IN.color;
#ifdef UIEFFECT_BLUR
                color += tex2D(_MainTex,half2(IN.texcoord.x + _BlurDistance, IN.texcoord.y + _BlurDistance)) * IN.color;
                color += tex2D(_MainTex,half2(IN.texcoord.x + _BlurDistance, IN.texcoord.y)) * IN.color;
                color += tex2D(_MainTex,half2(IN.texcoord.x, IN.texcoord.y + _BlurDistance)) * IN.color;
                color += tex2D(_MainTex,half2(IN.texcoord.x - _BlurDistance, IN.texcoord.y - _BlurDistance)) * IN.color;
                color += tex2D(_MainTex,half2(IN.texcoord.x + _BlurDistance, IN.texcoord.y - _BlurDistance)) * IN.color;
                color += tex2D(_MainTex,half2(IN.texcoord.x - _BlurDistance, IN.texcoord.y + _BlurDistance)) * IN.color;
                color += tex2D(_MainTex,half2(IN.texcoord.x - _BlurDistance, IN.texcoord.y)) * IN.color;
                color += tex2D(_MainTex,half2(IN.texcoord.x, IN.texcoord.y - _BlurDistance)) * IN.color;
                color = color/9;
#endif
                
#if defined(UIEFFECT_OVERLAY)
                half2 overlayUV = IN.texcoord.zw;
                fixed4 overlay = tex2D(_OverlayTex, overlayUV);
#ifdef UIEFFECT_OVERLAY_TINT
                overlay *= _OverlayTint;
#endif
                if (_OverlayColorMode == 0)
                {
                    color.rgb = color.rgb * (1 - overlay.a) + overlay.rgb * overlay.a;
                }
                else if(_OverlayColorMode == 1)
                {
                    color.rgb += overlay.rgb * overlay.a;
                }
                else if(_OverlayColorMode == 2)
                {
                    color.rgb = half3(1,1,1) - (half3(1,1,1) - color.rgb) * (half3(1,1,1) - overlay.rgb * overlay.a);
                }
#endif
                
#ifdef UIEFFECT_GRAYSCALE
                color.rgb = max(max(color.r,color.g),color.b) * 0.3;
#elif UIEFFECT_GRAYSCALE_LERP
                color.rgb = lerp(max(max(color.r,color.g),color.b)*0.3,color.rgb,_GrayLerp);
#endif
                
#ifdef UNITY_UI_CLIP_RECT
                color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
#endif
                
#ifdef UNITY_UI_ALPHACLIP
                clip(color.a - 0.001);
#endif
                
                return  color;
            }
            ENDCG
        }
    }
}