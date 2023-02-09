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
    /// 
    /// </summary>
    /// <param name="dataNo"></param>
    /// <returns></returns>
    public Material GetMaterial(int dataNo) {
        return imageDataSo.imageDataList.Find(x => x.id == dataNo).textureMaterial;
    }

    public int GetImageDataListCount() {
        return imageDataSo.imageDataList.Count;
    }


    public ImageData GetImageData(int searchNo) {
        return imageDataSo.imageDataList.Find(x => x.id == searchNo);
    }
}
