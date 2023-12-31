using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// ARGameManager の Texture 生成バージョン
/// </summary>
[RequireComponent(typeof(ARRaycastManager))]
public class ARTextureManager : MonoBehaviour
{
    private ARRaycastManager raycastManager;

    [SerializeField] private TextureThumbnailPopUp textureThumbnailPopUp;

    private void Awake() {
        TryGetComponent(out raycastManager);
    }

    private void Start() {
        textureThumbnailPopUp.gameObject.SetActive(false);
        
        // ポップアップの準備
        textureThumbnailPopUp.SetUpPopUp();
    }

    void Update() {
        // Editor 用
        if (Input.GetMouseButtonDown(0)) {
            Vector2 touchPosition = Input.mousePosition;

            //List<ARRaycastHit> hits = new();

            Debug.Log("クリック地点 : " + touchPosition);
            //
            // if (raycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon)) {
            //     // Raycastの衝突情報は距離によってソートされるため、0番目が最も近い場所でヒットした情報となります
            //     var hitPose = hits[0].pose;
            //
            //     Debug.Log("感知");
            
            CheckingARObject(Vector2.zero);

            // if (TextureGenerator.instance.GetSpawnedObject()) {
            //     TextureGenerator.instance.MoveTextureImage(touchPosition);
            //     //spawnedObject.transform.position = touchPosition;
            // }
            // else {
            //
            //     //TextureGenerator.instance.GenerateTextureImage(0, touchPosition);
            //     textureThumbnailPopUp.ShowPopUp(Vector3.zero);
            //
            //
            //     // spawnedObject = Instantiate(textureViewPrefab, touchPosition, Quaternion.identity);  // , hitPose.position, Quaternion.identity
            //     // spawnedObject.SetUpTextureView(0);
            //     Debug.Log("生成");
            // }
            //}
        }

        if (Input.touchCount > 0) {
            Vector2 touchPosition = Input.GetTouch(0).position;

            List<ARRaycastHit> hits = new();

            Debug.Log(touchPosition);

            if (raycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon)) {
                // Raycastの衝突情報は距離によってソートされるため、0番目が最も近い場所でヒットした情報となります
                var hitPose = hits[0].pose;

                
                Debug.Log("感知");

                // if (spawnedObject) {
                //     spawnedObject.transform.position = hitPose.position;
                // } else {
                //     spawnedObject = Instantiate(textureViewPrefab, hitPose.position, Quaternion.identity);
                //     spawnedObject.SetUpTextureView(0);
                //     Debug.Log("生成");
                // }
                
                
                CheckingARObject(hitPose.position);
                
                // if (TextureGenerator.instance.GetSpawnedObject()) {
                //     TextureGenerator.instance.MoveTextureImage(hitPose.position);
                //     //spawnedObject.transform.position = touchPosition;
                // } else {
                //     
                //     textureThumbnailPopUp.ShowPopUp(hitPose.position);
                //     
                //     
                //     // spawnedObject = Instantiate(textureViewPrefab, touchPosition, Quaternion.identity);  // , hitPose.position, Quaternion.identity
                //     // spawnedObject.SetUpTextureView(0);
                //     Debug.Log("生成");
                // }
            }
        }
    }

    /// <summary>
    /// TextureView ゲームオブジェクトの移動か生成の判定
    /// </summary>
    /// <param name="touchPosition"></param>
    private void CheckingARObject(Vector2 touchPosition) {
        if (TextureGenerator.instance.GetSpawnedObject()) {
            TextureGenerator.instance.MoveTextureImage(touchPosition);
            //spawnedObject.transform.position = touchPosition;
        }
        else {

            TextureGenerator.instance.GenerateTextureImage(0, touchPosition);
            //textureThumbnailPopUp.ShowPopUp(Vector3.zero);


            // spawnedObject = Instantiate(textureViewPrefab, touchPosition, Quaternion.identity);  // , hitPose.position, Quaternion.identity
            // spawnedObject.SetUpTextureView(0);
            Debug.Log("生成");
        }
    }
}
