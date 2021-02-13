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
        DropeableFeature.OnDropeablePlace += ActivateDropeablePlaceInBoard;
        GameManager.instance.dropeableFeature.CurrentDraggedDropeable = this;
    }
    public void ActivateDropeablePlaceInBoard(Transform dropeablePlaceInBoard)
    {
        dropeablePlaceInBoard.gameObject.SetActive(true);
        DropeableFeature.OnDropeablePlace += DesActivateDropeablePlaceInBoard;
        DropeableFeature.OnDropeablePlace -= ActivateDropeablePlaceInBoard;
        
    }
    public void DesActivateDropeablePlaceInBoard(Transform dropeablePlaceInBoard)
    {
        dropeablePlaceInBoard.gameObject.SetActive(false);
        DropeableFeature.OnDropeablePlace -= DesActivateDropeablePlaceInBoard;
    }
}
