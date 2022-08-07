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

            // ���_�V�F�[�_�[�̓��͗p�̍\����
            struct appdata
            {
                // �|���S���̒��_�̃f�[�^���摜�����u������͂��邽�߂ɁA�Z�}���e�B�N�X POSITION ���w��B�e�N�X�`���͈���Ȃ�
                float4 vertex : POSITION;
            };

            // ���_�V�F�[�_�[�̏o�͗p�A����уt���O�����g�V�F�[�_�[�̓��͗p�̍\����
            struct v2f
            {
                // ���W�ϊ���̒��_�̃f�[�^���摜�������u�֏o�͂��邽�߂ɁA�Z�}���e�B�N�X SV_POSITION ���w��
                float4 vertex : SV_POSITION;
            };

            // ���_�V�F�[�_�[�� vert �֐�
            v2f vert (appdata v)   // �摜�������u����̏��
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);  // ���W�ϊ�
                return o;  // ���W�ϊ���̏���߂�
            }

            // �t���O�����g�V�F�[�_�[�� frag �֐�
            fixed4 frag (v2f i) : SV_Target   // �߂�l���摜�������u�֏o�͂��邽�߂ɃZ�}���e�B�N�X SV_Target ��t�L���Ă���
            {
                return fixed4(0.0, 0.0, 0.0, 0.0);  // ���ׂĂ̐F�̐������}�X�N���`�悵�Ȃ�(�����ɂȂ�)
            }
            ENDCG
        }
    }
}
