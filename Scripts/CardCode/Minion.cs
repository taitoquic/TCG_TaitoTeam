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
    }
}
