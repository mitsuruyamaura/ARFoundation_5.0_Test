#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;

namespace UnityAR {

    [RequireComponent(typeof(ARPlaneManager), typeof(ARRaycastManager), typeof(PlayerInput))]

    public class ExPlaneOcclusion : MonoBehaviour {

        [SerializeField]
        private Text message;

        [SerializeField]
        private GameObject placementPrefab;

        [SerializeField]
        private GameObject occlusionPlane;

        private ARPlaneManager planeManager;
        private ARRaycastManager raycastManager;
        private PlayerInput playerInput;

        private bool isReady;

        private GameObject instantiateObject = null;

        private void ShowMessage(string text) {
            message.text = $"{text}\r\n";
        }

        private void AddMessage(string text) {
            message.text += $"{text} \r\n";
        }

        void Awake() {
            if (message == null) {
                Application.Quit();
            }

            TryGetComponent(out planeManager);
            TryGetComponent(out raycastManager);
            TryGetComponent(out playerInput);

            if (placementPrefab == null
                || occlusionPlane == null
                || planeManager == null
                || planeManager.planePrefab == null
                || raycastManager == null
                || playerInput == null
                || playerInput.actions == null)
            {
                isReady = false;
                ShowMessage("�G���[: SerializeField�Ȃǂ̐ݒ�s��");
            } else {
                isReady = true;
                ShowMessage("�����B�e���Ă��������B���΂炭����ƕ��ʂ����o����܂��B���ʂ��^�b�v����ƃI�u�W�F�N�g���\������܂��B");
            }
        }


        private void OnTouch(InputValue touchInfo) {

            if (!isReady || instantiateObject != null) {
                return;
            }

            Vector2 touchPosition = touchInfo.Get<Vector2>();
            var hits = new List<ARRaycastHit>();

            if (raycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon)) {
                Pose hitPose = hits[0].pose;

                instantiateObject = Instantiate(placementPrefab, hitPose.position, hitPose.rotation);

                // ���ʊ��m�����I�u�W�F�N�g���\��
                foreach (ARPlane plane in planeManager.trackables) {
                    plane.gameObject.SetActive(false);
                }

                // ARPlaneManager �����ʂ����o�����ۂɕ\�����镽�ʂ̃v���t�@�u���I�N���[�W�����p�̕��ʃv���t�@�u�ɕύX
                // �Ȍ�A���ʌ��o���ɓ����ȃI�N���[�W�����p���ʂ��\������A���̌���ɂ��鉼�z���̂₻�̈ꕔ���\�����邱�Ƃ��ł���
                planeManager.planePrefab = occlusionPlane;
                ShowMessage("���ʃI�N���[�W����");

                AddMessage("���z���̂���O�ɂ��镽�ʂ��B�e�E���o���A�I�N���[�W�����������Ă��邱�Ƃ��m�F���Ă��������B");
            }
        }
    }
}