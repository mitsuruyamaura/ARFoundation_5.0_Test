using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraModeSwitcher : MonoBehaviour
{
    [SerializeField]
    private GameObject editorCamera;

    [SerializeField]
    private GameObject arCamera;


    void Awake()
    {
# if UNITY_EDITOR
        arCamera.SetActive(false);
        LogDebugger.instance.DisplayLog("ARCamera を非アクティブ化");
# elif UNITY_ANDROID || UNITY_IOS
        editorCamera.SetActive(false);
        LogDebugger.instance.DisplayLog("MainCamera を非アクティブ化");
# endif
    }
}
