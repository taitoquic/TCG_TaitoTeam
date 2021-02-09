using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IDropeable 
{

    public void PlayDropeable();
    public void SetPreviewSprte(Image previewImage);
    public void NullPreviewSprite();
    IEnumerator MoveDropeable(ISceneDragable currentSceneDragable, Vector3 dropPosition);
}
