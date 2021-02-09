using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDropeablePlace : MonoBehaviour
{
    public delegate void ActiveMinionPreview(Vector3 dropPosition);
    public static event ActiveMinionPreview OnActiveMinionPreview;

    public delegate Sprite MinionPreviewSprite();
    public static event MinionPreviewSprite OnMinionSpriteInImage;

    public delegate IEnumerator DropeableMovement(ISceneDragable currentSceneDragable, Vector3 dropPosition);
    public static event DropeableMovement OnDropMove;
    int PositionInLine
    {
        get
        {
            return transform.GetSiblingIndex();
        }
    }
    int IndexLine
    {
        get
        {
            return transform.parent.GetSiblingIndex();
        }
    }
    Transform BattleField
    {
        get
        {
            return transform.parent.parent.parent.GetChild(0);
        }
    }
    Transform LocalPlayerLines
    {
        get
        {
            return BattleField.GetChild(0);
        }
    }
    PreviewDropMinionManager PreviewBF
    {
        get
        {
            return BattleField.GetChild(1).GetComponent<PreviewDropMinionManager>();
        }
    }
    Vector3 DropPosition
    {
        get
        {
            return LocalPlayerLines.GetChild(IndexLine).GetChild(PositionInLine).position;
        }
    }

    private void OnMouseEnter()
    {
        if(OnMinionSpriteInImage!= null) PreviewBF.MinionPreview = OnMinionSpriteInImage?.Invoke();
        OnActiveMinionPreview?.Invoke(DropPosition);
        SceneDragableFeature.OnDrop += MoveCard;
    }

    private void OnMouseExit()
    {
        OnActiveMinionPreview?.Invoke(DropPosition);
        SceneDragableFeature.OnDrop -= MoveCard;
    }

    void MoveCard(ISceneDragable currentSceneDragable)
    {
        StartCoroutine(OnDropMove.Invoke(currentSceneDragable, DropPosition));
    }
}
