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

        [SerializeField, Header("タップ時に生成するプレファブ")]
        private GameObject placementPrefab;

        [SerializeField, Header("オクルージョン用の AROcclusionPlane プレファブ")]
        private GameObject occlusionPlane;

        [SerializeField, Header("平面オクルージョン用の枠線の幅(太さ)")]
        private float lineWidth = 0.001f;

        [SerializeField, Header("平面オクルージョン用の枠線の色")]
        private Color lineColor = Color.black;

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

            // 必要なコンポーネントが取得・用意できているか確認
            if (placementPrefab == null
                || occlusionPlane == null
                || planeManager == null
                || planeManager.planePrefab == null
                || raycastManager == null
                || playerInput == null
                || playerInput.actions == null)
            {
                isReady = false;
                ShowMessage("エラー: SerializeFieldなどの設定不備");
            } else {
                isReady = true;
                ShowMessage("床を撮影してください。しばらくすると平面が検出されます。平面をタップするとオブジェクトが表示されます。");
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

                // 平面感知したオブジェクトを非表示
                foreach (ARPlane plane in planeManager.trackables) {
                    plane.gameObject.SetActive(false);
                }

                // AROcclusionPlane ゲームオブジェクトの LineRenderer を取得して枠線の幅と色を変える
                if (occlusionPlane.TryGetComponent(out LineRenderer line)) {
                    line.startWidth = lineWidth;
                    line.endWidth = lineWidth;
                    line.startColor = lineColor;
                    line.endColor = lineColor;
                }

                // ARPlaneManager が平面を検出した際に表示する平面のプレファブをオクルージョン用の平面プレファブに変更
                // 以後、平面検出時に透明なオクルージョン用平面が表示され、その後方にある仮想物体やその一部を非表示することができる
                planeManager.planePrefab = occlusionPlane;
                ShowMessage("平面オクルージョン");

                AddMessage("仮想物体より手前にある平面を撮影・検出し、オクルージョンが生じていることを確認してください。");
            }
        }
    }
}