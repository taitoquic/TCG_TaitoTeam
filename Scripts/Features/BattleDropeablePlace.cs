using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDropeablePlace : MonoBehaviour
{
    //public static List<int[]> filledPositions;

    public delegate bool ActiveMinionPreview(Vector3 dropPosition);
    public static event ActiveMinionPreview OnActiveMinionPreview;

    //public delegate void CardIsPicked(Image previewImage);
    //public static event CardIsPicked OnMinionPicked;

    //public delegate IEnumerator DropeableMovement(Transform cardTransform, Vector3 dropPosition);
    //public static event DropeableMovement OnDropMove;
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
    //PreviewDropMinionManager PreviewBF //TODO: cambiar el index cuando pongamos el nuevo remoteplayer lines
    //{
    //    get
    //    {
    //        return BattleField.GetChild(1).GetComponent<PreviewDropMinionManager>();
    //    }
    //}
    bool CanDrop
    {
        set
        {
            GameManager.instance.dropeableFeature.MinionCanDrop = value;
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
        //OnMinionPicked?.Invoke(PreviewBF.MinionPreviewImage);
        if (OnActiveMinionPreview == null) return;
        CanDrop = OnActiveMinionPreview.Invoke(DropPosition);
        //CardController.OnCardDrop += DropToBattlefield;
    }

    private void OnMouseExit()
    {
        if (OnActiveMinionPreview == null) return;
        CanDrop = OnActiveMinionPreview.Invoke(DropPosition);
        //CardController.OnCardDrop -= DropToBattlefield;
    }
}
