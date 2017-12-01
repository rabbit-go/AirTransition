Shader "UI/TransitionCutOut"
{
	Properties
	{
		_MainTex("MainTex", 2D) = "black" {}
	_MaskTex("MaskTex", 2D) = "white" {}
	_Range("Range", Range(0, 1)) = 0
	}
		SubShader
	{
		Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }
		LOD 100
		ZWrite Off
		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag alpha
		// make fog work
#pragma multi_compile_fog
#include "UnityCG.cginc"
#include "UnityUI.cginc"
		struct appdata
	{
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
		float4 color    : COLOR;
	};

	struct v2f
	{
		float2 uv : TEXCOORD0;
		UNITY_FOG_COORDS(1)
			float4 vertex : SV_POSITION;
		float4 color    : COLOR;
	};

	sampler2D _MainTex;
	float4 _MainTex_ST;
	float Range;
	v2f vert(appdata v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.uv = TRANSFORM_TEX(v.uv, _MainTex);
		UNITY_TRANSFER_FOG(o,o.vertex);
		o.color = v.color;
		return o;
	}
	float _Range;
	sampler2D _MaskTex;
	fixed4 frag(v2f i) : SV_Target
	{
		// sample the texture
		half4 col = tex2D(_MainTex, i.uv)*i.color;
		half alpha = (1 - tex2D(_MaskTex, i.uv).r) - _Range;
		clip(alpha*-1);
		return col;
	}
		ENDCG
	}
	}
}