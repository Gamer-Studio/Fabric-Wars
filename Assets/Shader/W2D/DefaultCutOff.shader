Shader "Sprites/DefaultCutoff"
{
    Properties
    {
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
        _Color("Tint", Color) = (1,1,1,1)
        transition_tex("Transition Texture", 2D) = "white" {}
        cutoff("Cutoff", Range(0, 1)) = 0
        [MaterialToggle] PixelSnap("Pixel snap", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
            "PreviewType" = "Plane"
            "CanUseSpriteAtlas" = "True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        //Blend One OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ PIXELSNAP_ON
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float4 color : COLOR;
                float2 tex_coord : TEXCOORD0;
            };

            struct v2_f
            {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
                float2 tex_coord : TEXCOORD0;
            };

            fixed4 _Color;
            float cutoff;
            sampler2D transition_tex;

            v2_f vert(const appdata_t IN)
            {
                v2_f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.tex_coord = IN.tex_coord;
                OUT.color = IN.color * _Color;
                #ifdef PIXELSNAP_ON
                    OUT.vertex = UnityPixelSnap(OUT.vertex);
                #endif

                return OUT;
            }

            sampler2D _MainTex;
            sampler2D alpha_tex;
            float alpha_split_enabled;

            fixed4 sample_sprite_texture(float2 uv)
            {
                fixed4 color = tex2D(_MainTex, uv);

                #if UNITY_TEXTURE_ALPHASPLIT_ALLOWED
                    if (_AlphaSplitEnabled)
                        color.a = tex2D(_AlphaTex, uv).r;
                #endif //UNITY_TEXTURE_ALPHASPLIT_ALLOWED

                return color;
            }

            fixed4 frag(const v2_f IN) : SV_Target
            {
                fixed4 c = sample_sprite_texture(IN.tex_coord) * IN.color;
                c.rgb *= c.a;

                fixed4 transit = tex2D(transition_tex, IN.tex_coord);
                if (transit.b > cutoff)
                    c.a = 0;

                return c;
            }
            ENDCG
        }
    }
}