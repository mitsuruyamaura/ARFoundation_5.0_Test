#pragma warning disable 0649　// スクリプトではなくInspectorから参照を設定するような場合、定義されている変数に値が代入されていない警告であるCS0649が発生します。それを回避する
//#pragma warning restore 0649　有効にする場合

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.AR;

//namespace UnityAR {

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

    /// <summary>
    /// メッセージ表示
    /// </summary>
    /// <param name="text"></param>
    private void ShowMessage(string text) {
        message.text = $"{text}\r\n";
    }

    /// <summary>
    /// 追加のメッセージ表示
    /// </summary>
    /// <param name="text"></param>
    private void AddMessage(string text) {
        message.text += $"{text}\r\n";
    }


    void Awake() {
        // Text コンポーネントの情報がアサインされていない場合には、ゲームを終了する
        if (message == null) {
            Application.Quit();
        }

        // 必要なコンポーネントの情報が揃っているか、アサインがはずれていないか、確認
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
        // ARSelectionInteractable によりオブジェクトが配置されたときに実行するイベントハンドラーを設定する
        placementInteractable.objectPlaced.AddListener(OnObjectPlaced);

        // 旧式
        //gestureInteractor.onHoverEntered.AddListener(OnHoverEntered);
        // ARGestureIntaractor が Hover を開始したときに実行するイベントハンドラーを設定する
        gestureInteractor.hoverEntered.AddListener(OnHoverEntered);

        // 旧式
        //gestureInteractor.onHoverExited.AddListener(OnHoverExited);
        // ARGestureIntaractor が Hover を停止したときに実行するイベントハンドラーを設定する
        gestureInteractor.hoverExited.AddListener(OnHoverExited);
    }

    // 旧式用
    //private void OnObjectPlaced(ARPlacementInteractable arg0, GameObject placedObject) {
    //    if (hasPlaced) {
    //        Destroy(placedObject);
    //        return;
    //    }
    //}

    /// <summary>
    /// ARSelectionInteractable によりオブジェクトが配置されたときに実行する
    /// </summary>
    /// <param name="arg0"></param>
    private void OnObjectPlaced(ARObjectPlacementEventArgs arg0) {
        if (hasPlaced) {
            Destroy(arg0.placementObject);
            return;
        }

        if (arg0.placementObject.TryGetComponent(out ARSelectionInteractable selectInteractable)) {

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

    /// <summary>
    /// ホバー開始時に実行されるコールバック
    /// </summary>
    /// <param name="arg0"></param>
    private void OnHoverEntered(HoverEnterEventArgs arg0) {
        hoverStatus = $"対話可能 : {arg0.interactableObject.transform.name}";
    }

    /// <summary>
    /// ホバー停止時に実行されるコールバック
    /// </summary>
    /// <param name="arg0"></param>
    private void OnHoverExited(HoverExitEventArgs arg0) {
        hoverStatus = "対話不可";
    }

    /// <summary>
    /// オブジェクトをタップしたときに実行されるコールバック
    /// </summary>
    /// <param name="arg0"></param>
    private void OnSelectEntered(SelectEnterEventArgs arg0) {
        selectStatus = $"選択中 : {arg0.interactableObject.transform.name}";
    }

    /// <summary>
    /// オブジェクト以外の場所をタップしたときに実行されるコールバック
    /// </summary>
    /// <param name="arg0"></param>
    private void OnSelectExited(SelectExitEventArgs arg0) {
        selectStatus = "選択解除";
    }

    void Update() {
        // コンポーネントの準備ができていない、あるいは、ARSelectionInteractable によって、すでにゲームオブジェクトを配置済みの場合
        if (!isReady || !hasPlaced) {
            // 以下の処理は行わない
            return;
        }

        // 不要になった平面感知用のゲームオブジェクトをすべて非表示
        foreach (ARPlane plane in planeManager.trackables) {
            plane.gameObject.SetActive(false);
        }
        ShowMessage("インタラクション");

        // 状態表示の更新
        AddMessage(hoverStatus);
        AddMessage(selectStatus);
    }
}
    //}
//