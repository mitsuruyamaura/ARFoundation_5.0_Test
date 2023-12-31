using System;
using UnityEngine;

/// <summary>
/// AR 空間の画像を添付したオブジェクトを生成するシングルトンクラス
/// </summary>
public class TextureGenerator : MonoBehaviour
{
    public static TextureGenerator instance;
    
    [SerializeField, Tooltip("AR空間に生成する画像オブジェクト")]
    private TextureView textureViewPrefab;
    
    private TextureView spawnedObject;    // 生成した画像オブジェクトを代入しておく

    
    void Awake() {
        // シングルトン化する
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        //GenerateTextureImage(0, Vector3.zero);
    }

    /// <summary>
    /// 画像オブジェクトの生成
    /// </summary>
    /// <param name="textureId"></param>
    /// <param name="generatePosition"></param>
    public void GenerateTextureImage(int textureId, Vector3 generatePosition) {
        spawnedObject = Instantiate(textureViewPrefab, generatePosition, Quaternion.identity);
        spawnedObject.SetUpTextureView(textureId);
        Debug.Log("生成");
        LogDebugger.instance.DisplayLog("生成");
    }

    /// <summary>
    /// 生成されている画像オブジェクトの移動
    /// </summary>
    /// <param name="nextPosition"></param>
    public void MoveTextureImage(Vector3 nextPosition) {
        spawnedObject.transform.position = nextPosition;
    }

    /// <summary>
    /// 画像オブジェクトの生成の有無
    /// true なら生成済
    /// </summary>
    /// <returns></returns>
    public bool GetSpawnedObject() {
        return spawnedObject != null;
    }
}

