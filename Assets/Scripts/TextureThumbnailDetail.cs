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
    
    /// <summary>
    /// サムネイルの初期設定
    /// </summary>
    /// <param name="data"></param>
    /// <param name="popUp"></param>
    public void SetUpTextureThumbnail(ImageData data, TextureThumbnailPopUp popUp) {
        imageData = data;
        textureThumbnailPopUp = popUp;
        
        // サムネイルの画像を変更
        imgThumbnail.sprite = data.spriteImage;
        
        // ボタンにメソッドを登録して、クリックした際の処理を紐づける
        btnThumbnail.onClick.AddListener(OnClickThumbnail);
    }

    /// <summary>
    /// このサムネイルをクリック(タップ)したときの処理
    /// </summary>
    private void OnClickThumbnail() {
        // この画像の情報を提供してゲームオブジェクトを生成する
        TextureGenerator.instance.GenerateTextureImage(imageData.id, textureThumbnailPopUp.GetTouchPosition());
        
        // ポップアップウインドウを閉じる
        textureThumbnailPopUp.HidePopUp();
    }
}
