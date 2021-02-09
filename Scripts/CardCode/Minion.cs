using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Card/Minion", order = 0)]
public class Minion : CardAsset, IDropeable
{
    public Sprite minionPreview;
    
    public override void PlayCard()
    {
        PlayDropeable();
    }
    public void PlayDropeable()
    {
        BattleDropeablePlace.OnMinionSpriteInImage += SetPreviewSprte;
        BattleDropeablePlace.OnDropMove += MoveDropeable;
        SceneDragableFeature.OnSceneDragableEnd += NullPreviewSprite;
    }

    public void SetPreviewSprte(Image previewImage)
    {
        previewImage.sprite = minionPreview;
        BattleDropeablePlace.OnMinionSpriteInImage -= SetPreviewSprte;
    }

    public void NullPreviewSprite()
    {
        BattleDropeablePlace.OnMinionSpriteInImage -= SetPreviewSprte;
        BattleDropeablePlace.OnDropMove -= MoveDropeable;
        SceneDragableFeature.OnSceneDragableEnd -= NullPreviewSprite;
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
        SceneDragableFeature.OnSceneDragableEnd -= NullPreviewSprite;
    }
}
