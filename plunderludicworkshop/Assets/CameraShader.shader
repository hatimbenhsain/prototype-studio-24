// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader "FadeDisFromCam" {
// 	Properties {
// 	  _MainTex ("Texture", 2D) = "white" {}
// 	  _FadeStart ("Fade Start", Float) = 0
// 	  _FadeDis ("Fade End", Float) = 10
// 	}
	
// 	SubShader {
// 	  Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
// 	  LOD 200

// 	  ZWrite Off
// 	  Blend SrcAlpha OneMinusSrcAlpha

// 	  CGPROGRAM

// 	  #pragma surface surf Lambert finalcolor:mycolor vertex:myvert
// 	  struct Input {
// 		  float2 uv_MainTex;
// 		  half alpha;
// 	  };

// 	  sampler2D _MainTex;
// 	  half _FadeStart;
// 	  half _FadeDis;

// 	  void myvert (inout appdata_full v, out Input data)
// 	  {
//         UNITY_INITIALIZE_OUTPUT(Input,data);
// 		  float4 vertexPos = mul (unity_ObjectToWorld, v.vertex);
// 		  float4 camPos = float4(_WorldSpaceCameraPos.x,_WorldSpaceCameraPos.y,_WorldSpaceCameraPos.z,0) ;
// 		  float vDis = distance(camPos, vertexPos);
// 		  data.alpha = 1 -  ((vDis-_FadeStart)/(_FadeDis-_FadeStart));
// 	  }
	  
// 	  void mycolor (Input IN, SurfaceOutput o, inout fixed4 color) {
// 		  color.a = IN.alpha;
// 	  }
	  
// 	  void surf (Input IN, inout SurfaceOutput o) {
// 		  o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
// 	  }
	  
// 	  ENDCG
// 	}
// 	Fallback "Diffuse"
//   }

Shader "Kamaly/FarAlpha"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_MinVisDistance("MinDistance",Float) = 0
		_MaxVisDistance("MaxDistance",Float) = 20
			_Color("Color",Color) = (0,0,0,1)
	}
	SubShader
	{
			ZWrite Off
			Blend SrcAlpha OneMinusSrcAlpha
		Tags { "RenderType"="Transparent" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag 
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

		struct v2f
	{
		half4 pos       : POSITION;
		fixed4 color : COLOR0;
		half2 uv        : TEXCOORD0;
	};



	sampler2D   _MainTex;
	half        _MinVisDistance;
	half        _MaxVisDistance;



	v2f vert(appdata_full v)
	{
		v2f o;

		o.pos = mul((half4x4)UNITY_MATRIX_MVP, v.vertex);
		o.uv = v.texcoord.xy;
		o.color = v.color;

		half3 viewDirW = _WorldSpaceCameraPos - mul((half4x4)unity_ObjectToWorld, v.vertex);
		half viewDist = length(viewDirW);
		half falloff = saturate((viewDist - _MinVisDistance) / (_MaxVisDistance - _MinVisDistance));
		o.color.a *= (1.0f - falloff);
		return o;
	}


	fixed4 frag(v2f i) : COLOR
	{
		fixed4 color = tex2D(_MainTex, i.uv) * i.color;
	return color;
	}
			ENDCG
		}
	}
}