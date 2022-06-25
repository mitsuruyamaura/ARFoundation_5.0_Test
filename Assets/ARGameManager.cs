using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ARGameManager : MonoBehaviour
{
    [SerializeField, Tooltip("AR空間に召喚する豆腐")] GameObject tohu;

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
                // Raycastの衝突情報は距離によってソートされるため、0番目が最も近い場所でヒットした情報となります
                var hitPose = hits[0].pose;

                Debug.Log("感知");

                if (spawnedObject) {
                    spawnedObject.transform.position = hitPose.position;
                } else {
                    spawnedObject = Instantiate(tohu, hitPose.position, Quaternion.identity);
                    Debug.Log("生成");
                }
            }
        }

        if (Input.touchCount > 0) {
            Vector2 touchPosition = Input.GetTouch(0).position;
            touchPosition = Input.mousePosition;

            List<ARRaycastHit> hits = new();

            Debug.Log(touchPosition);

            if (raycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon)) {
                // Raycastの衝突情報は距離によってソートされるため、0番目が最も近い場所でヒットした情報となります
                var hitPose = hits[0].pose;

                Debug.Log("感知");

                if (spawnedObject) {
                    spawnedObject.transform.position = hitPose.position;
                } else {
                    spawnedObject = Instantiate(tohu, hitPose.position, Quaternion.identity);
                    Debug.Log("生成");
                }
            }
        }
    }
}
