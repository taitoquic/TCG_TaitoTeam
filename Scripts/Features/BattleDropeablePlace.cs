using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDropeablePlace : MonoBehaviour
{
    //public static List<int[]> filledPositions;

    public delegate bool ActiveMinionPreview(Vector3 dropPosition);
    public static event ActiveMinionPreview OnActiveMinionPreview;

    public delegate void IsCardPicked(bool isPicked);
    public static event IsCardPicked OnMinionPicked;

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
    Vector3 DropPosition
    {
        get
        {
            return LocalPlayerLines.GetChild(IndexLine).GetChild(PositionInLine).position;
        }
    }

    private void OnMouseEnter()
    {
        OnMinionPicked?.Invoke(OnActiveMinionPreview.Invoke(DropPosition));
    }

    private void OnMouseExit()
    {
        OnMinionPicked?.Invoke(OnActiveMinionPreview.Invoke(DropPosition));
    }
}
