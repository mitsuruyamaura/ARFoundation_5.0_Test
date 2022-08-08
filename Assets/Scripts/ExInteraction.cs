#pragma warning disable 0649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.AR;

namespace UnityAR {

    public class ExInteraction : MonoBehaviour {

        [SerializeField]
        private Text message;

        [SerializeField]
        private ARPlaneManager planeManager;

        [SerializeField]
        private ARPlacementInteractable placementInteractable;

        [SerializeField]
        private ARGestureInteractor gestureInteractor;

        private bool isReady = false;
        private bool hasPlaced = false;

        private string hoverStatus = "";
        private string selectStatus = "";


        private void ShowMessage(string text) {
            message.text = $"{text}\r\n";
        }

        private void AddMessage(string text) {
            message.text += $"{text}\r\n";
        }


        void Awake() {
            if (message == null) {
                Application.Quit();
            }

            if (planeManager == null
                || planeManager.planePrefab == null
                || placementInteractable == null
                || placementInteractable.placementPrefab == null
                || gestureInteractor == null) {
                isReady = false;
                ShowMessage("エラー: SerializeFieldなどの設定不備");
            } else {
                isReady = true;
                ShowMessage("床を撮影してください。しばらくすると平面が検出されます。平面をタップするとオブジェクトが表示されます。");
            }
        }

        //[System.Obsolete]
        private void OnEnable() {

            // 旧式
            //placementInteractable.onObjectPlaced.AddListener(OnObjectPlaced);
            placementInteractable.objectPlaced.AddListener(OnObjectPlaced);

            // 旧式
            //gestureInteractor.onHoverEntered.AddListener(OnHoverEntered);
            gestureInteractor.hoverEntered.AddListener(OnHoverEntered);

            // 旧式
            //gestureInteractor.onHoverExited.AddListener(OnHoverExited);
            gestureInteractor.hoverExited.AddListener(OnHoverExited);
        }

        // 旧式用
        //private void OnObjectPlaced(ARPlacementInteractable arg0, GameObject placedObject) {
        //    if (hasPlaced) {
        //        Destroy(placedObject);
        //        return;
        //    }
        //}

         private void OnObjectPlaced(ARObjectPlacementEventArgs arg0) {
            if (hasPlaced) {
                Destroy(arg0.placementObject);
                return;
            }

            if(arg0.placementObject.TryGetComponent(out ARSelectionInteractable selectInteractable)){

                selectInteractable.selectEntered.AddListener(OnSelectEntered);
                selectInteractable.selectExited.AddListener(OnSelectExited);
                hasPlaced = true;
            } else {
                isReady = false;
                ShowMessage("エラー : ARSelectionInteractable の設定不備");
            }
        }

        // 旧式用
        //private void OnHoverEntered(XRBaseInteractable arg0) {
        //    hoverStatus = $"対話可能 : { arg0.gameObject.name }";
        //}


        private void OnHoverEntered(HoverEnterEventArgs arg0) {
            hoverStatus = $"対話可能 : { arg0.interactableObject.transform.name }";
        }


        private void OnHoverExited(HoverExitEventArgs arg0) {
            hoverStatus = "対話不可";
        }

        private void OnSelectEntered(SelectEnterEventArgs arg0) {
            selectStatus = $"選択中 : { arg0.interactableObject.transform.name }";
        }

        private void OnSelectExited(SelectExitEventArgs arg0) {
            selectStatus = "選択解除";
        }

        void Update() {
            if (!isReady || !hasPlaced) {
                return;
            }

            foreach (ARPlane plane in planeManager.trackables) {
                plane.gameObject.SetActive(false);
            }
            ShowMessage("インタラクション");

            AddMessage(hoverStatus);
            AddMessage(selectStatus);
        }
    }
}