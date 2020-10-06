Shader "Custom/NewLiquidShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
	SubShader
	{
		Cull Off
		Blend One OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vertexFunc
			#pragma fragment fragmentFunc

			#include "UnityCG.cginc"

			sampler2D _MainTex;

			struct v2f
			{
				float4 pos : SV_POSITION;
				half2 uv : TEXCOORD0;
			};

			v2f vertexFunc(appdata_base v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord;
				return o;
			}

			fixed4 _Color;
			float4 _MainTex_TexelSize;

			fixed4 fragmentFunc(v2f i) : COLOR{
				half4 c = tex2D(_MainTex, i.uv);
				c.rgb *= c.a;
				//c.a = 1;
				half4 outlineC = _Color;
				outlineC.a *= ceil(c.a);
				
				outlineC.rgb *= outlineC.a;

				half4 color = half4(0.0, 0.0, 0.0, 0.0);

				fixed upAlpha = tex2D(_MainTex, i.uv + fixed2(0, _MainTex_TexelSize.y)).a;
				fixed downAlpha = tex2D(_MainTex, i.uv - fixed2(0, _MainTex_TexelSize.y)).a;
				fixed rightAlpha = tex2D(_MainTex, i.uv + fixed2(_MainTex_TexelSize.x, 0)).a;
				fixed leftAlpha = tex2D(_MainTex, i.uv - fixed2(_MainTex_TexelSize.x, 0)).a;

				fixed upRightAlpha = tex2D(_MainTex, i.uv + fixed2(_MainTex_TexelSize.x, _MainTex_TexelSize.y)).a;
				fixed downLeftAlpha = tex2D(_MainTex, i.uv + fixed2(-_MainTex_TexelSize.x, -_MainTex_TexelSize.y)).a;
				fixed downRightAlpha = tex2D(_MainTex, i.uv + fixed2(_MainTex_TexelSize.x, -_MainTex_TexelSize.y)).a;
				fixed upLeftAlpha = tex2D(_MainTex, i.uv + fixed2(-_MainTex_TexelSize.x, _MainTex_TexelSize.y)).a;

				//color = lerp(outlineC, c,
					//ceil(upAlpha * downAlpha * rightAlpha * leftAlpha *
					//upRightAlpha * downLeftAlpha * downRightAlpha * upLeftAlpha));
				//color = step()
				/*
					if c.a == 0
						return 1
					else
						return 0
				*/
				//return color;
				return c;
			}
				// lerp function:
				//	lerp returns a when w is zero and returns b when w is one.
				//	float3 lerp(float3 a, float3 b, float w)
				//	{ 
				//		return a + w * (b - a);
				//	}

				/* step(edge, x) function
					float step(float edge, float x)
					vec2 step(vec2 edge, vec2 x)
					vec3 step(vec3 edge, vec3 x)
					vec4 step(vec4 edge, vec4 x)

					vec2 step(float edge, vec2 x)
					vec3 step(float edge, vec3 x)
					vec4 step(float edge, vec4 x)

					edge specifies the location of the edge of the step function.

					x specify the value to be used to generate the step function.
				*/
			ENDCG
		}
	}
}
