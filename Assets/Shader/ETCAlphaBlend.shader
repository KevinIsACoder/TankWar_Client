// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/ETCAlphaBlend" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_AlphaTex {"Alpha Tex", 2D} = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" "Queue" = "Transparent+1"}
		LOD 200
		Lighting Off //关闭光照影响
		ZTest Off //关闭深度测试
		Blend SrcAlpha OneMinusSrcAlpha //正常模式（透明度混合）
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		//#pragma surface surf Standard fullforwardshadows
        
		#pragma vertex vert
		#pragma fragment frag
		
		#include "UnityCG.cginc"
		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _AlphaTex; //Alpha 图片
		float _AlphaFactor; //混合系数

		struct a2v
		{
			float4 pos : POSITION;
			float4 normal : NORMAL;
			float texcoord : TEXCOORD;
		};
		struct v2f //
		{
			float4 pos : SV_POSITION; //裁剪空间中的顶点坐标
			float2 uv : TEXCOORD0; //模型空间中的第n组纹理坐标
			float4 color : COLOR; //模型空间中的顶点颜色
		};
        half4 _MainTex_ST;
		half4 _AlphaTex_ST;
		v2f vert(appdata_full v)
		{
			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex); //顶点世界坐标
			o.uv = v.texcoord;
			o.color = v.color;
			return o;
		}
		half4 frag(v2f i) : SV_Target
		{
			half4 texColor = tex2D(_MainTex, i.uv);
			texColor.a *= tex2D(_AlphaTex, i.uv).r;
			return texColor;
		}

		// half _Glossiness;
		// half _Metallic;
		// fixed4 _Color;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		// UNITY_INSTANCING_BUFFER_START(Props)
		// 	// put more per-instance properties here
		// UNITY_INSTANCING_BUFFER_END(Props)

		// void surf (Input IN, inout SurfaceOutputStandard o) {
		// 	// Albedo comes from a texture tinted by color
		// 	fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
		// 	o.Albedo = c.rgb;
		// 	// Metallic and smoothness come from slider variables
		// 	o.Metallic = _Metallic;
		// 	o.Smoothness = _Glossiness;
		// 	o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
