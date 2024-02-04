Shader "CustomUI/ColorWheel"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Hue("Hue", Float) = 0
		_Sat("Saturation", Float) = 0
		_Val("Value", Float) = 0
		_ColorsCount("Colors Segments", int) = 4
		_WheelsCount("Number of Wheels", int) = 2
		_StartingAngle("Starting Angle",Range(0,360))=0
		_StencilComp("Stencil Comparison", Float) = 8
		_Stencil("Stencil ID", Float) = 0
		_StencilOp("Stencil Operation", Float) = 0
		_StencilWriteMask("Stencil Write Mask", Float) = 255
		_StencilReadMask("Stencil Read Mask", Float) = 255

		_ColorMask("Color Mask", Float) = 15

		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip("Use Alpha Clip", Float) = 0
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

			Stencil
			{
				Ref[_Stencil]
				Comp[_StencilComp]
				Pass[_StencilOp]
				ReadMask[_StencilReadMask]
				WriteMask[_StencilWriteMask]
			}

			Cull Off
			Lighting Off
			ZWrite Off
			ZTest[unity_GUIZTestMode]
			Blend SrcAlpha OneMinusSrcAlpha
			ColorMask[_ColorMask]

			Pass
			{
				Name "Default"
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 2.0

				#include "UnityCG.cginc"
				#include "UnityUI.cginc"

				#pragma multi_compile_local _ UNITY_UI_CLIP_RECT
				#pragma multi_compile_local _ UNITY_UI_ALPHACLIP

				struct appdata_t
				{
					float4 vertex   : POSITION;
					float4 color    : COLOR;
					float2 texcoord : TEXCOORD0;
					UNITY_VERTEX_INPUT_INSTANCE_ID
				};

				struct v2f
				{
					float4 vertex   : SV_POSITION;
					fixed4 color : COLOR;
					float2 texcoord  : TEXCOORD0;
					float4 worldPosition : TEXCOORD1;
					UNITY_VERTEX_OUTPUT_STEREO
				};

				sampler2D _MainTex;
				fixed4 _TextureSampleAdd;
				float4 _ClipRect;
				float4 _MainTex_ST;
				float _Hue;
				float _Sat;
				float _Val;
				float _ColorsCount;
				float _WheelsCount;
				float _StartingAngle;
				v2f vert(appdata_t v)
				{
					v2f OUT;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
					OUT.worldPosition = v.vertex;
					OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);
					OUT.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
					OUT.color = v.color;
					return OUT;
				}
				float3 HSV2RGB(float3 c)
				{
					float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
					float3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
					return c.z * lerp(K.xxx, saturate(p - K.xxx), c.y);
				}
				fixed4 frag(v2f IN) : SV_Target
{
    half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;

    // Saturation = la distance depuis le centre
    float s = distance(IN.texcoord, half2(0.5, 0.5)) / 0.5;

    // Hue = angle
    float h = atan2(IN.texcoord.y - 0.5, IN.texcoord.x - 0.5);

    // Ajoutez le décalage d'angle de départ pour contrôler l'angle de départ de la roue
    h = h + UNITY_PI * 2 * _StartingAngle / 360.0;

    // Convertir h de la plage (0-2*Pi) à la plage (0-1)
    h = (h > 0 ? h : (2 * UNITY_PI + h)) / (2 * UNITY_PI);

    // Ajoutez la valeur de Hue provenant de la rotation de la roue par l'utilisateur
    // et divisez par _WheelsCount pour distribuer les couleurs sur "_WheelsCount" roues
    float shiftedH = (h + _Hue) / _WheelsCount;
    shiftedH = fmod(shiftedH, 1);

    // Attribution d'une seule couleur pour toute la portion au lieu d'appliquer une valeur de dégradé de H
    float discretedH = float(floor(shiftedH * (_ColorsCount))) / (_ColorsCount);

    // Un léger décalage de la valeur h pour distribuer l'ensemble de la plage h (0-1) sur "_ColorsCount-1" portions
    // et laissez la dernière portion pour la couleur grise
    discretedH = discretedH * (_ColorsCount) / (_ColorsCount - 1);

    // Changez les couleurs existantes en noir, jaune et blanc
    if (shiftedH > 1.0 - 1.0 / (_ColorsCount) && shiftedH <= 1)
        color = half4(0, 0, 0, color.a); // Noir
    else if (discretedH < 1.0 / (_ColorsCount))
        color = half4(1, 1, 1, color.a); // Blanc
    else
        color = half4(1, 1, 0, color.a); // Jaune

    #ifdef UNITY_UI_CLIP_RECT
    color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
    #endif

    #ifdef UNITY_UI_ALPHACLIP
    clip(color.a - 0.001);
    #endif

    return color;
}


				
			ENDCG
			}
		}
}
