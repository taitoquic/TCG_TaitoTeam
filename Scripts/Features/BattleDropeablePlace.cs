using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDropeablePlace : MonoBehaviour
{
    public static List<int[]> filledPositions;

    public delegate void ActiveMinionPreview(Vector3 dropPosition);
    public static event ActiveMinionPreview OnActiveMinionPreview;

    public delegate void CardIsPicked(Image previewImage);
    public static event CardIsPicked OnMinionPicked;

    public delegate IEnumerator DropeableMovement(Transform cardTransform, Vector3 dropPosition);
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
    int[] ListIdentificator
    {
        get
        {
            return new int[2] { IndexLine, PositionInLine };
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
    //Transform CollidersBF
    //{
    //    get
    //    {
    //        return transform.parent.parent;
    //    }
    //}

    //public bool SetDropBF
    //{
    //    set
    //    {
    //        CollidersBF.gameObject.SetActive(value);
    //    }
    //}
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
        OnMinionPicked?.Invoke(PreviewBF.MinionPreviewImage);
        if (OnActiveMinionPreview == null) return;
        OnActiveMinionPreview?.Invoke(DropPosition);
        CardController.OnCardDrop += DropToBattlefield;
    }

    private void OnMouseExit()
    {
        if (OnActiveMinionPreview == null) return;
        OnActiveMinionPreview?.Invoke(DropPosition);
        CardController.OnCardDrop -= DropToBattlefield;
    }
    void DropToBattlefield(Transform cardTransform)
    {
        filledPositions.Add(ListIdentificator);
        StartCoroutine(OnDropMove.Invoke(cardTransform, DropPosition));
        CardController.OnCardDrop -= DropToBattlefield;
    }
}
