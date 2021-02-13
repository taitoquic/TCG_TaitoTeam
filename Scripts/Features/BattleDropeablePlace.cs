using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDropeablePlace : MonoBehaviour
{
    public BoxCollider collidersToBF;

    public delegate bool ActiveMinionPreview(BattleDropeablePlace currentBattleDropeablePlace);
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
    public Vector3 DropPosition
    {
        get
        {
            return LocalPlayerLines.GetChild(IndexLine).GetChild(PositionInLine).position;
        }
    }
    List<BoxCollider> OccupiedPositions
    {
        get
        {
            return GameManager.instance.dropeableFeature.occupiedPositions;
        }
    }
    private void OnMouseEnter()
    {
        OnMinionPicked?.Invoke(OnActiveMinionPreview.Invoke(this));
    }

    private void OnMouseExit()
    {
        OnMinionPicked?.Invoke(OnActiveMinionPreview.Invoke(this));
    }
    public void AddOccupiedPosition()
    {
        OccupiedPositions.Add(collidersToBF);
        Debug.Log("Event fired and dictionary has " + OccupiedPositions.Count + "entries");
        DropeableFeature.OnMinionDrop -= AddOccupiedPosition;
    }
}
