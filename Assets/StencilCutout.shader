Shader "Custom/StencilCutout"
{
    SubShader
    {
        Tags { "Queue"="Geometry+1" }
        Stencil
        {
            Ref 1
            Comp Always
            Pass Replace
        }
        ColorMask 0

        Pass
        {
            // Hier muss ein leerer Pass sein, damit der Shader gültig ist
        }
    }
}
