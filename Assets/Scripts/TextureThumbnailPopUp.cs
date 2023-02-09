using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TextureThumbnailPopUp : MonoBehaviour
{
    [SerializeField] private TextureThumbnailDetail thumbnailDetailPrefab;
    [SerializeField] private Transform thumbnailTran;
    [SerializeField] private List<TextureThumbnailDetail> thumbnailDetailList = new();

    [SerializeField] private CanvasGroup canvasGroup;
    private Vector3 touchPosition;

    public void SetUpPopUp() {
        canvasGroup.alpha = 0;
        for (int i = 0; i < DataBaseManager.instance.GetImageDataListCount(); i++) {
            int index = i;
            TextureThumbnailDetail textureThumbnailDetail = Instantiate(thumbnailDetailPrefab, thumbnailTran, false);
            textureThumbnailDetail.SetUpTextureThumbnail(DataBaseManager.instance.GetImageData(index), this);
            thumbnailDetailList.Add(textureThumbnailDetail);
        }
    }


    public void ShowPopUp(Vector3 pos) {
        touchPosition = pos;
        gameObject.SetActive(true);
        canvasGroup.DOFade(1.0f, 0.5f).SetEase(Ease.InQuad);
    }


    public void HidePopUp() {
        canvasGroup.DOFade(0, 0.5f).SetEase(Ease.InQuart)
            .OnComplete(() => gameObject.SetActive(false));
    }


    public Vector3 GetTouchPosition() {
        return touchPosition;
    }
}
