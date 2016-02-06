Shader "Sugiyama/Toon/Texture_penetrate" {
  // @version 1.1.0
  // @date $date$
  // @author sugiyama-mitsunari

  Properties {
    _InkSize ("Ink Size", Float) = 5.0
    _InkSampler ("Ink Color Texture", 2D) = "white" {}
    _ColorBase ("Diffuse", 2D) = "white" {}
    _ShadowSampler ("Shadow Texture", 2D) = "gray" {}
  }

  SubShader {

    Tags {
      "Queue" = "Geometry+1"
      "LightMode" = "ForwardBase"
    }

    // Outline pass
    Pass {
      Cull Front
      ZTest GEqual

CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

#include "_toon_ink_texture.cginc"

ENDCG
    }
    
    // Main pass
    Pass {
      Cull Back
      ZTest LEqual

CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

#include "_toon_mesh.cginc"

ENDCG
    }
  }

  FallBack "VertexLit"
}
