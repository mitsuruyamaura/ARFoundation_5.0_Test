using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// ARGameManager の Texture 生成バージョン
/// </summary>
public class TextureGenerator : MonoBehaviour
{
    public static TextureGenerator instance;
    
    [SerializeField, Tooltip("AR空間に召喚する画像設定のないスクリーン")]
    private TextureView textureViewPrefab;
    private TextureView spawnedObject;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void GenerateTextureImage(int no, Vector3 pos) {

        spawnedObject = Instantiate(textureViewPrefab, pos, Quaternion.identity);
        spawnedObject.SetUpTextureView(no);
        Debug.Log("生成");
    }


    public void MoveTextureImage(Vector3 pos) {
        spawnedObject.transform.position = pos;
    }


    public bool GetSpawnedObject() {
        return spawnedObject != null;
    }
}

