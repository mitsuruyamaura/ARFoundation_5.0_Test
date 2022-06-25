using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ARGameManager : MonoBehaviour
{
    [SerializeField, Tooltip("AR��Ԃɏ������铤��")] GameObject tohu;

    private GameObject spawnedObject;
    private ARRaycastManager raycastManager;

    private void Awake() {
        TryGetComponent(out raycastManager);
    }

    void Update() {
        Debug.Log("Update");

        if (Input.GetMouseButtonDown(0)) {
            Vector2 touchPosition = Input.mousePosition;

            List<ARRaycastHit> hits = new();

            Debug.Log(touchPosition);

            if (raycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon)) {
                // Raycast�̏Փˏ��͋����ɂ���ă\�[�g����邽�߁A0�Ԗڂ��ł��߂��ꏊ�Ńq�b�g�������ƂȂ�܂�
                var hitPose = hits[0].pose;

                Debug.Log("���m");

                if (spawnedObject) {
                    spawnedObject.transform.position = hitPose.position;
                } else {
                    spawnedObject = Instantiate(tohu, hitPose.position, Quaternion.identity);
                    Debug.Log("����");
                }
            }
        }

        if (Input.touchCount > 0) {
            Vector2 touchPosition = Input.GetTouch(0).position;
            touchPosition = Input.mousePosition;

            List<ARRaycastHit> hits = new();

            Debug.Log(touchPosition);

            if (raycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon)) {
                // Raycast�̏Փˏ��͋����ɂ���ă\�[�g����邽�߁A0�Ԗڂ��ł��߂��ꏊ�Ńq�b�g�������ƂȂ�܂�
                var hitPose = hits[0].pose;

                Debug.Log("���m");

                if (spawnedObject) {
                    spawnedObject.transform.position = hitPose.position;
                } else {
                    spawnedObject = Instantiate(tohu, hitPose.position, Quaternion.identity);
                    Debug.Log("����");
                }
            }
        }
    }
}
