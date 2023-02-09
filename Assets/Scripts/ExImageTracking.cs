#pragma warning disable 0649
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace UnityAR
{


    [RequireComponent(typeof(ARTrackedImageManager))]
    public class ExImageTracking : MonoBehaviour
    {
        [SerializeField] private Text message;
        [SerializeField] private List<GameObject> plasementPrefabs;
        private ARTrackedImageManager imageManager;
        private bool isReady;

        private Dictionary<string, GameObject> correnspodingChartForMakersAndPrefabs = new();
        private Dictionary<string, GameObject> instantiatedObjects = new();

        private void ShowMessage(string text) {
            message.text = $"{text} /r/r";
        }

        private void AddMessage(string text) {
            message.text += $"{text} /r/r";
        }

        void Awake() {
            if (message == null) Application.Quit();

            if (!TryGetComponent(out imageManager)) {
                isReady = false;
                ShowMessage("エラー : SerializeField などの設定不備");
            }
            else {
                isReady = true;
            }
        }

        private void OnEnable() {
            if (!isReady) {
                return;
            }

            List<string> makerList = new();
            for (int i = 0; i < imageManager.referenceLibrary.count; i++) {
                makerList.Add(imageManager.referenceLibrary[i].name);
            }

            makerList.Sort();

            for (int i = 0; i < plasementPrefabs.Count; i++) {
                correnspodingChartForMakersAndPrefabs.Add(makerList[i], plasementPrefabs[i]);
                instantiatedObjects.Add(makerList[i], null);
            }

            imageManager.trackedImagePrefab = null;
            imageManager.trackedImagesChanged += OnTrackedImagesChanged;

            ShowMessage("ARマーカーとプレハブの対応");

            foreach (var data in correnspodingChartForMakersAndPrefabs) {
                AddMessage($"{data.Key} : {data.Value}");
            }

            AddMessage("ARマーカーと配置するプレハブとの対応を確認後、ARマーカーを撮影してください。");
        }

        private void OnDisable() {
            if (!isReady) {
                return;
            }

            imageManager.trackedImagesChanged -= OnTrackedImagesChanged;
        }

        private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs) {
            ShowMessage("イメージ検出");

            // イメージ検出時に、どのイメージか確認する
            foreach (var trackedImage in eventArgs.added) {
                string imageName = trackedImage.referenceImage.name;

                // 名前で照合し、イメージと紐づくオブジェクトが生成されていない場合には生成し、Dictionary に追加
                if (correnspodingChartForMakersAndPrefabs.TryGetValue(imageName, out var prefab)) {
                    float scale = 0.2f;
                    trackedImage.transform.localScale = Vector3.one * scale;
                    instantiatedObjects[imageName] = Instantiate(prefab, trackedImage.transform);
                }
            }

            // イメージ検出時の更新を確認
            foreach (var trackedImage in eventArgs.updated) {
                string imageName = trackedImage.referenceImage.name;
                
                // 画像を検出できたか出来ないかでオブジェクトの表示/非表示を切り替える
                if (instantiatedObjects.TryGetValue(imageName, out var instantiatedObject)) {
                    if (trackedImage.trackingState != TrackingState.None) {
                        instantiatedObject.SetActive(true);
                    } else {
                        instantiatedObject.SetActive(false);
                    }

                    AddMessage($"{ imageName } : { trackedImage.trackingState }");
                } 
            }
        }
    }
}