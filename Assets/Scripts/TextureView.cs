using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureView : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    public void SetUpTextureView(int dataNo) {
        
        if(TryGetComponent(out meshRenderer)) {
            meshRenderer.material = DataBaseManager.instance.GetMaterial(dataNo);
        }
    }
}
