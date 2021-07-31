Shader "Custom/ChangeColor"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert;
            #pragma fragment frag;

            fixed4 _Color;

            void vert()
            {

            }
            fixed4 frag(): SV_Target
            {
                return _Color;
            }
        
            ENDCG
        }
    }
}
