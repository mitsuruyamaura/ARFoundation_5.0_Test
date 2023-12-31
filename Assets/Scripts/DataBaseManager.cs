using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager instance;

    [SerializeField] private ImageDataSO imageDataSo;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 指定した番号のマテリアルの取得
    /// </summary>
    /// <param name="dataNo"></param>
    /// <returns></returns>
    public Material GetMaterial(int dataNo) {
        return imageDataSo.imageDataList.Find(x => x.id == dataNo).textureMaterial;
    }

    /// <summary>
    /// ImageDataSO に登録されている ImageData の最大値の取得
    /// </summary>
    /// <returns></returns>
    public int GetImageDataListCount() {
        return imageDataSo.imageDataList.Count;
    }

    /// <summary>
    /// 指定した番号の ImageData の取得
    /// </summary>
    /// <param name="searchNo"></param>
    /// <returns></returns>
    public ImageData GetImageData(int searchNo) {
        return imageDataSo.imageDataList.Find(x => x.id == searchNo);
    }
}
