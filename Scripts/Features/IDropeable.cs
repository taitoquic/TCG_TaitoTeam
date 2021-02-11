using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IDropeable 
{
    public void PlayDropeable();
    public void ActiveBoardDropPlaceable();
    public void SetPreviewSprite(Image previewImage);
    public void NullPreviewSprite();
    IEnumerator MoveDropeable(Transform dropeableTransform, Vector3 dropPosition);
}
