#pragma warning disable 0649�@// �X�N���v�g�ł͂Ȃ�Inspector����Q�Ƃ�ݒ肷��悤�ȏꍇ�A��`����Ă���ϐ��ɒl���������Ă��Ȃ��x���ł���CS0649���������܂��B������������
//#pragma warning restore 0649�@�L���ɂ���ꍇ

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
    /// ���b�Z�[�W�\��
    /// </summary>
    /// <param name="text"></param>
    private void ShowMessage(string text) {
        message.text = $"{text}\r\n";
    }

    /// <summary>
    /// �ǉ��̃��b�Z�[�W�\��
    /// </summary>
    /// <param name="text"></param>
    private void AddMessage(string text) {
        message.text += $"{text}\r\n";
    }


    void Awake() {
        // Text �R���|�[�l���g�̏�񂪃A�T�C������Ă��Ȃ��ꍇ�ɂ́A�Q�[�����I������
        if (message == null) {
            Application.Quit();
        }

        // �K�v�ȃR���|�[�l���g�̏�񂪑����Ă��邩�A�A�T�C�����͂���Ă��Ȃ����A�m�F
        if (planeManager == null
            || planeManager.planePrefab == null
            || placementInteractable == null
            || placementInteractable.placementPrefab == null
            || gestureInteractor == null) {
            isReady = false;
            ShowMessage("�G���[: SerializeField�Ȃǂ̐ݒ�s��");
        } else {
            isReady = true;
            ShowMessage("�����B�e���Ă��������B���΂炭����ƕ��ʂ����o����܂��B���ʂ��^�b�v����ƃI�u�W�F�N�g���\������܂��B");
        }
    }

    //[System.Obsolete]
    private void OnEnable() {

        // ����
        //placementInteractable.onObjectPlaced.AddListener(OnObjectPlaced);
        // ARSelectionInteractable �ɂ��I�u�W�F�N�g���z�u���ꂽ�Ƃ��Ɏ��s����C�x���g�n���h���[��ݒ肷��
        placementInteractable.objectPlaced.AddListener(OnObjectPlaced);

        // ����
        //gestureInteractor.onHoverEntered.AddListener(OnHoverEntered);
        // ARGestureIntaractor �� Hover ���J�n�����Ƃ��Ɏ��s����C�x���g�n���h���[��ݒ肷��
        gestureInteractor.hoverEntered.AddListener(OnHoverEntered);

        // ����
        //gestureInteractor.onHoverExited.AddListener(OnHoverExited);
        // ARGestureIntaractor �� Hover ���~�����Ƃ��Ɏ��s����C�x���g�n���h���[��ݒ肷��
        gestureInteractor.hoverExited.AddListener(OnHoverExited);
    }

    // �����p
    //private void OnObjectPlaced(ARPlacementInteractable arg0, GameObject placedObject) {
    //    if (hasPlaced) {
    //        Destroy(placedObject);
    //        return;
    //    }
    //}

    /// <summary>
    /// ARSelectionInteractable �ɂ��I�u�W�F�N�g���z�u���ꂽ�Ƃ��Ɏ��s����
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
            ShowMessage("�G���[ : ARSelectionInteractable �̐ݒ�s��");
        }
    }

    // �����p
    //private void OnHoverEntered(XRBaseInteractable arg0) {
    //    hoverStatus = $"�Θb�\ : { arg0.gameObject.name }";
    //}

    /// <summary>
    /// �z�o�[�J�n���Ɏ��s�����R�[���o�b�N
    /// </summary>
    /// <param name="arg0"></param>
    private void OnHoverEntered(HoverEnterEventArgs arg0) {
        hoverStatus = $"�Θb�\ : {arg0.interactableObject.transform.name}";
    }

    /// <summary>
    /// �z�o�[��~���Ɏ��s�����R�[���o�b�N
    /// </summary>
    /// <param name="arg0"></param>
    private void OnHoverExited(HoverExitEventArgs arg0) {
        hoverStatus = "�Θb�s��";
    }

    /// <summary>
    /// �I�u�W�F�N�g���^�b�v�����Ƃ��Ɏ��s�����R�[���o�b�N
    /// </summary>
    /// <param name="arg0"></param>
    private void OnSelectEntered(SelectEnterEventArgs arg0) {
        selectStatus = $"�I�� : {arg0.interactableObject.transform.name}";
    }

    /// <summary>
    /// �I�u�W�F�N�g�ȊO�̏ꏊ���^�b�v�����Ƃ��Ɏ��s�����R�[���o�b�N
    /// </summary>
    /// <param name="arg0"></param>
    private void OnSelectExited(SelectExitEventArgs arg0) {
        selectStatus = "�I������";
    }

    void Update() {
        // �R���|�[�l���g�̏������ł��Ă��Ȃ��A���邢�́AARSelectionInteractable �ɂ���āA���łɃQ�[���I�u�W�F�N�g��z�u�ς݂̏ꍇ
        if (!isReady || !hasPlaced) {
            // �ȉ��̏����͍s��Ȃ�
            return;
        }

        // �s�v�ɂȂ������ʊ��m�p�̃Q�[���I�u�W�F�N�g�����ׂĔ�\��
        foreach (ARPlane plane in planeManager.trackables) {
            plane.gameObject.SetActive(false);
        }
        ShowMessage("�C���^���N�V����");

        // ��ԕ\���̍X�V
        AddMessage(hoverStatus);
        AddMessage(selectStatus);
    }
}
    //}
//