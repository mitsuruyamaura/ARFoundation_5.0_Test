Shader "AR/Occlusion"
{
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        Tags { "Queue" = "Geometry-1" }
        ZWrite On
        ZTest LEqual
        ColorMask 0

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // 頂点シェーダーの入力用の構造体
            struct appdata
            {
                // ポリゴンの頂点のデータを画像処装置から入力するために、セマンティクス POSITION を指定。テクスチャは扱わない
                float4 vertex : POSITION;
            };

            // 頂点シェーダーの出力用、およびフラグメントシェーダーの入力用の構造体
            struct v2f
            {
                // 座標変換後の頂点のデータを画像処理装置へ出力するために、セマンティクス SV_POSITION を指定
                float4 vertex : SV_POSITION;
            };

            // 頂点シェーダーの vert 関数
            v2f vert (appdata v)   // 画像処理装置からの情報
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);  // 座標変換
                return o;  // 座標変換後の情報を戻す
            }

            // フラグメントシェーダーの frag 関数
            fixed4 frag (v2f i) : SV_Target   // 戻り値を画像処理装置へ出力するためにセマンティクス SV_Target を付記している
            {
                return fixed4(0.0, 0.0, 0.0, 0.0);  // すべての色の成分をマスクし描画しない(透明になる)
            }
            ENDCG
        }
    }
}
