using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDropeablePlace : MonoBehaviour
{
    public BoxCollider collidersToBF;

    public delegate bool ActiveMinionPreview(Vector3 placeToMoveMP);
    public static event ActiveMinionPreview OnActiveMinionPreview;

    public delegate void CanDropeableDropInPlace(bool mpIsActive);
    public static event CanDropeableDropInPlace OnMPIsActive;

    public delegate void MinionPreviewAction();
    public MinionPreviewAction OnMPAction;

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
    List<BoxCollider> OccupiedPositions
    {
        get
        {
            return GameManager.instance.dropeableFeature.occupiedPositions;
        }
    }
    Vector3 DropPosition
    {
        get
        {
            return LocalPlayerLines.GetChild(IndexLine).GetChild(PositionInLine).position;
        }
    }

    private void OnEnable()
    {
        OnMPAction += SetMP;
    }
    private void OnMouseEnter()
    {
        OnMPAction?.Invoke();
    }

    private void OnMouseExit()
    {
        OnMPAction?.Invoke();
    }

    private void OnDisable()
    {
        OnMPAction -= SetMP;
    }

    void SetMP()
    {
        OnMPIsActive?.Invoke(OnActiveMinionPreview.Invoke(DropPosition));
    }

    //public void AddOccupiedPosition()
    //{
    //    OccupiedPositions.Add(collidersToBF);
    //    Debug.Log("Event fired and dictionary has " + OccupiedPositions.Count + "entries");
    //    DropeableFeature.OnMinionDrop -= AddOccupiedPosition;
    //}
}
