using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureThumbnailDetail : MonoBehaviour
{
    [SerializeField] private Button btnThumbnail;

    [SerializeField] private Image imgThumbnail;

    private ImageData imageData;
    private TextureThumbnailPopUp textureThumbnailPopUp;
    
    public void SetUpTextureThumbnail(ImageData data, TextureThumbnailPopUp popUp) {
        imageData = data;
        textureThumbnailPopUp = popUp;
        
        imgThumbnail.sprite = data.spriteImage;
        btnThumbnail.onClick.AddListener(OnClickThumbnail);
    }


    private void OnClickThumbnail() {
        TextureGenerator.instance.GenerateTextureImage(imageData.id, textureThumbnailPopUp.GetTouchPosition());
        textureThumbnailPopUp.HidePopUp();
    }
}
