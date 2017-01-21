
Shader "Custom/Grid" {
     
    Properties {
      _GridThickness ("Grid Thickness", Float) = 0.01
      _GridSpacing ("Grid Spacing", Float) = 10.0
      _GridColour ("Grid Colour", Color) = (0.5, 1.0, 1.0, 1.0)
      _BaseColour ("Base Colour", Color) = (0.0, 0.0, 0.0, 0.0)
    }
     
    SubShader {
      Tags { "Queue" = "Transparent" }
     
      Pass {
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        Lighting Off
     
        CGPROGRAM
     
        // Define the vertex and fragment shader functions
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"
     
        // Access Shaderlab properties
        uniform float _GridThickness;
        uniform float _GridSpacing;
        uniform float4 _GridColour;
        uniform float4 _BaseColour;
        
     
        // Input into the vertex shader
        struct vertexInput {
            float4 pos : POSITION;
          	float4 texcoord : TEXCOORD0;
        };
 
        // Output from vertex shader into fragment shader
        struct vertexOutput {
          float4 pos : SV_POSITION;
          float4 texcoord : TEXCOORD0;
        };
     
        // VERTEX SHADER
        vertexOutput vert(vertexInput input) {
          vertexOutput output;
          output.pos = mul(UNITY_MATRIX_MVP, input.pos);
          
          // Calculate the world position coordinates to pass to the fragment shader
          output.texcoord =  input.texcoord; //mul(_Object2World, input.pos);
          return output;
        }
 
        // FRAGMENT SHADER
        float4 frag(vertexOutput output) : COLOR {
//          if (frac(output.pos.x/_GridSpacing) < _GridThickness 
//          || frac(output.pos.y/_GridSpacing) < _GridThickness
//          || frac(output.pos.z/_GridSpacing) < _GridThickness) {

				if(frac(output.texcoord.x*100/_GridSpacing) < _GridThickness
					||frac(output.texcoord.y*100/_GridSpacing) < _GridThickness){
            return _GridColour;
          }
          else {
            return _BaseColour;
          }
			
        }
    ENDCG
    }
  }
  
  FallBack "VertexLit"
}