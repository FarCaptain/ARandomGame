Shader "Hidden/Custom/PPS_CRT_Monitor" {
	
	HLSLINCLUDE
		#include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"
		
	ENDHLSL
	
	Properties{
		//_MainTex("Base (RGB)", 2D) = "white" {}
		_CRT_Tex("Second", 2D) = "white" {}
		_Moire_Tex("Third", 2D) = "white" {}
		_IsLight("IsLight", Float) = 0.0
		_Scale("Scale", Range(0.0, 2.0)) = 0.0
		_OverallLight("OverallLight", Range(0.0, 1.0)) = 0.0
		_CenterLightScale("CenterLightScale", Range(0.0, 1.0)) = 0.0
	}
	SubShader{
		Cull Off ZWrite Off ZTest Always
		Pass
		{
			HLSLPROGRAM
			
			#pragma vertex VertDefault
			#pragma fragment frag

			TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
			uniform sampler2D _CRT_Tex;
			uniform sampler2D _Moire_Tex;

			float _Scale;
			float _IsLight;
			float _OverallLight;
			float _CenterLightScale;

			float4 frag(VaryingsDefault i) : SV_Target
			{
				float zero_p = 0.5;
				float2 fragment;
				float4 finalColor;
				float p_x = i.texcoord.x - zero_p;
				float p_y = i.texcoord.y - zero_p;
				float k = _Scale;
				float radius = sqrt(p_x*p_x + p_y*p_y);
				fragment = float2(zero_p + (1-k/5)*(1 + k*radius)*p_x, zero_p + (1-k/5)*(1 + k*radius)*p_y);
				if (fragment[0] < 0) {
					fragment[0] = 0;
				}if (fragment[0] > 1) {
					fragment[0] = 1;
				}if (fragment[1] < 0) {
					fragment[1] = 0;
				}if (fragment[1] > 1 ) {
					fragment[1] = 1;
				}
				
				finalColor = tex2D(_Moire_Tex, fragment) * tex2D(_CRT_Tex, fragment) * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, fragment);
				if (_IsLight) {
					float4 borderlight = (1.0, 1.0, 1.0, 1.0) * _OverallLight;
					float lightColor = borderlight + (_CenterLightScale / radius );
					return finalColor  * lightColor;
				}else {
					return finalColor;
				}
			}
			ENDHLSL
		}
	}
	//FallBack "Diffuse"
}
