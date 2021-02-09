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
        BattleDropeablePlace.OnDropMove += MoveDropeable;
        SceneDragableFeature.OnSceneDragableEnd += NullMinionSprite;
    }

    Sprite SetMinionPreviewSprite()
    {
        SceneDragableFeature.OnSceneDragableEnd -= NullMinionSprite;
        BattleDropeablePlace.OnMinionSpriteInImage -= SetMinionPreviewSprite;
        return minionPreview;
    }

    void NullMinionSprite()
    {
        BattleDropeablePlace.OnMinionSpriteInImage -= SetMinionPreviewSprite;
        BattleDropeablePlace.OnDropMove -= MoveDropeable;
        SceneDragableFeature.OnSceneDragableEnd -= NullMinionSprite;
    }
    public IEnumerator MoveDropeable(ISceneDragable currentSceneDragable, Vector3 dropPosition)
    {
        float sMoothing = 10.0f;
        while (Vector3.Distance(currentSceneDragable.SceneDragableTransform.position, dropPosition) > 0.05f)
        {
            currentSceneDragable.SceneDragableTransform.position = Vector3.Lerp(currentSceneDragable.SceneDragableTransform.position, dropPosition, sMoothing * Time.deltaTime);
            yield return null;
        }
        currentSceneDragable.SceneDragableTransform.position = dropPosition;
        BattleDropeablePlace.OnDropMove -= MoveDropeable;
    }
}
