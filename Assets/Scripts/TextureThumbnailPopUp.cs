using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class TextureThumbnailPopUp : MonoBehaviour
{
    [SerializeField] private TextureThumbnailDetail thumbnailDetailPrefab;
    [SerializeField] private Transform thumbnailTran;
    [SerializeField] private List<TextureThumbnailDetail> thumbnailDetailList = new();

    [SerializeField] private CanvasGroup canvasGroup;
    private Vector3 touchPosition;

    
    /// <summary>
    /// ポップアップの初期設定
    /// </summary>
    public void SetUpPopUp() {
        canvasGroup.alpha = 0;
        
        // データベースに登録されている画像の数だけサムネイルを作成して並べて一覧表示
        for (int i = 0; i < DataBaseManager.instance.GetImageDataListCount(); i++) {
            TextureThumbnailDetail textureThumbnailDetail = Instantiate(thumbnailDetailPrefab, thumbnailTran, false);
            textureThumbnailDetail.SetUpTextureThumbnail(DataBaseManager.instance.GetImageData(i), this);
            thumbnailDetailList.Add(textureThumbnailDetail);
        }
    }

    /// <summary>
    /// ポップアップ表示
    /// </summary>
    /// <param name="pos"></param>
    public void ShowPopUp(Vector3 pos) {
        touchPosition = pos;
        gameObject.SetActive(true);
        canvasGroup.DOFade(1.0f, 0.5f).SetEase(Ease.InQuad);
    }

    /// <summary>
    /// ポップアップ非表示
    /// </summary>
    public void HidePopUp() {
        canvasGroup.DOFade(0, 0.5f).SetEase(Ease.InQuart)
            .OnComplete(() => gameObject.SetActive(false));
    }

    /// <summary>
    /// タップ地点の座標取得
    /// </summary>
    /// <returns></returns>
    public Vector3 GetTouchPosition() {
        return touchPosition;
    }
}
