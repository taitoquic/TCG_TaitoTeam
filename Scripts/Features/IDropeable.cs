using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDropeable 
{
    void PlayDropeable();
    Sprite DropeablePreviewSprite { get; }
    //public void ActiveBoardDropPlaceable();
    //public void SetPreviewSprite(Image previewImage);
    //public void NullPreviewSprite();
    //IEnumerator MoveDropeable(Transform dropeableTransform, Vector3 dropPosition);
}
