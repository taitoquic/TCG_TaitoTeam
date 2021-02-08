using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/Minion", order = 0)]
public class Minion : CardAsset
{
    public Sprite minionPreview;
    public override void PlayCard()
    {
        BattleDropeablePlace.OnMinionSpriteInImage += SetMinionPreviewSprite;
        CardController.OnCardEnd += NullMinionSprite;
    }

    Sprite SetMinionPreviewSprite()
    {
        CardController.OnCardEnd -= NullMinionSprite;
        BattleDropeablePlace.OnMinionSpriteInImage -= SetMinionPreviewSprite;
        return minionPreview;
    }

    void NullMinionSprite()
    {
        BattleDropeablePlace.OnMinionSpriteInImage -= SetMinionPreviewSprite;
        CardController.OnCardEnd -= NullMinionSprite;
    }
}
