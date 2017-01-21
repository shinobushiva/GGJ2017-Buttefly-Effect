Shader "Custom/RotatingTexture"
{
    Properties
    {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _TransX ("Trans X", float) = 0
        _TransY ("Trans Y", float) = 0
        _Scale ("Scale", float) = 1
        _Rotation ("Rotation", float) = 0
        _RotationOffset("Rotation Offset", float) = 0
    }
    SubShader
    {
        Pass
        {
			Name "BASE"
            Lighting On
        	Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            float4x4 _TextureRotation;
            float _Rotation, _RotationOffset, _TransX, _TransY, _Scale;

            v2f vert (float4 pos : POSITION, float2 uv : TEXCOORD0)
            {
            	float sinF = sin(radians(_Rotation));
				float cosF = cos(radians(_Rotation));
				float tx = _TransX;
				float ty = _TransY;
				_TransX = (cosF * tx) - (sinF * ty);
				_TransY = (sinF * tx) + (cosF * ty);

            	float4x4 translateMatrix = float4x4(1,	0,	0,	_TransX,
											 		0,	1,	0,	_TransY,
									  				0,	0,	1,	0,
									  				0,	0,	0,	1);
	
				float4x4 scaleMatrix 	= float4x4(_Scale,	0,	0,	0,
											 		0,	_Scale,0,	0,
								  					0,	0,	_Scale, 0,
								  					0,	0,	0,	    1);
            	float angleZ = radians(_Rotation+_RotationOffset);
				float c = cos(angleZ);
				float s = sin(angleZ);
				float4x4 rotateZMatrix	= float4x4(	c,	-s,	0,	0,
											 		s,	c,	0,	0,
								  					0,	0,	1,	0,
								  					0,	0,	0,	1);

				float4x4 localTranslated = translateMatrix;
  				float4x4 localScaledTranslated = mul(localTranslated,scaleMatrix);
  				float4x4 localScaledTranslatedRotZ = mul(localScaledTranslated,rotateZMatrix);

				_TextureRotation = localScaledTranslatedRotZ;
				 
                v2f o;
                o.pos = mul(UNITY_MATRIX_MVP, pos);
                o.uv = mul(_TextureRotation, float4(uv,0,1)).xy;
                return o;
            }
            
            sampler2D _MainTex;
            fixed4 frag (v2f i) : SV_Target
            {
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }

    }
}