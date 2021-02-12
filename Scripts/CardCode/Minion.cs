using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/Minion", order = 0)]
public class Minion : CardAsset, IDropeable
{
    public Sprite minionPreview;
    public Sprite DropeablePreviewSprite
    {
        get
        {
            return minionPreview;
        }
    }

    public override void PlayCard()
    {
        PlayDropeable();
    }
    public void PlayDropeable()
    {
        GameManager.instance.dropeableFeature.CurrentDraggedDropeable = this;
        //ActiveBoardDropPlaceable();
        //BattleDropeablePlace.OnMinionPicked += SetPreviewSprite;
        //BattleDropeablePlace.OnDropMove += MoveDropeable;
        //SceneDragableFeature.OnSceneDragableDragEnd += NullPreviewSprite;
    }
    //public void ActiveBoardDropPlaceable()
    //{
    //    GameManager.instance.dropeableBoardPlace.ActiveBFDropeableColliders();
    //}

    //public void SetPreviewSprite(Image previewImage)
    //{
    //    previewImage.sprite = minionPreview;
    //    BattleDropeablePlace.OnMinionPicked -= SetPreviewSprite;
    //}

    //public void NullPreviewSprite()
    //{
    //    //BattleDropeablePlace.OnMinionPicked -= SetPreviewSprite;
    //    //BattleDropeablePlace.OnDropMove -= MoveDropeable;
    //    //SceneDragableFeature.OnSceneDragableDragEnd -= NullPreviewSprite;
    //}

    //public IEnumerator MoveDropeable(Transform cardTransform, Vector3 dropPosition)
    //{
    //    float sMoothing = 10.0f;
    //    while (Vector3.Distance(cardTransform.position, dropPosition) > 0.05f)
    //    {
    //        cardTransform.position = Vector3.Lerp(cardTransform.position, dropPosition, sMoothing * Time.deltaTime);
    //        yield return null;
    //    }
    //    cardTransform.position = dropPosition;
    //    BattleDropeablePlace.OnDropMove -= MoveDropeable;
    //    SceneDragableFeature.OnSceneDragableDragEnd -= NullPreviewSprite;
    //}
}
