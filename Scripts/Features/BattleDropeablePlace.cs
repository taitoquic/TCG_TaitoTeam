using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDropeablePlace : MonoBehaviour
{
    public delegate void ActiveMinionPreview(Vector3 dropPosition);
    public static event ActiveMinionPreview OnActiveMinionPreview;

    public delegate void MinionPreviewSprite(Image previewImage);
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
        OnMinionSpriteInImage?.Invoke(PreviewBF.MinionPreviewImage);
        OnActiveMinionPreview?.Invoke(DropPosition);
        SceneDragableFeature.OnDrop += DropSceneDraggable;
    }

    private void OnMouseExit()
    {
        OnActiveMinionPreview?.Invoke(DropPosition);
        SceneDragableFeature.OnDrop -= DropSceneDraggable;
    }

    void DropSceneDraggable(ISceneDragable currentSceneDragable)
    {
        StartCoroutine(OnDropMove.Invoke(currentSceneDragable, DropPosition));
    }
}
