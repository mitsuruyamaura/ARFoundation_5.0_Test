using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ImageDataSO", menuName = "Create ImageDataSO")]
public class ImageDataSO : ScriptableObject
{
    public List<ImageData> imageDataList = new();
}
