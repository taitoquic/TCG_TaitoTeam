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

    delegate void DropeablePlaceAction();
    DropeablePlaceAction OnDropeablePlaceAction;

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
    Vector3 DropPosition
    {
        get
        {
            return LocalPlayerLines.GetChild(IndexLine).GetChild(PositionInLine).position;
        }
    }
    bool IsMinionPreviewActive
    {
        set
        {
            OnMPIsActive?.Invoke(value);
            if (value) DropeableFeature.OnDropActions += DesactivateDropPosition;
            else DropeableFeature.OnDropActions -= DesactivateDropPosition;
        }
    }

    private void OnEnable()
    {
        OnDropeablePlaceAction += SetMP;
    }
    private void OnMouseEnter()
    {
        OnDropeablePlaceAction?.Invoke();
    }

    private void OnMouseExit()
    {
        OnDropeablePlaceAction?.Invoke();
    }

    private void OnDisable()
    {
        OnDropeablePlaceAction -= SetMP;
    }

    void SetMP()
    {
        IsMinionPreviewActive = OnActiveMinionPreview.Invoke(DropPosition);
    }
    void DesactivateDropPosition()
    {
        collidersToBF.enabled = false;
        DropeableFeature.OnDropActions -= DesactivateDropPosition;
    }
}
