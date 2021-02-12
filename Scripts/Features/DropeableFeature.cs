using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropeableFeature : MonoBehaviour
{
    public Transform CollidersForDropToBF;
    public Transform dropMinionTransform;

    public delegate void DropMinionAction();
    public DropMinionAction OnMinionCanDrop;
    //public static event DropMinionAction OnDropMinionEnd;

    PreviewDropMinionManager DropMinionImage
    {
        get
        {
            return dropMinionTransform.parent.gameObject.GetComponent<PreviewDropMinionManager>();
        }
    }
    Vector3 DropPosition
    {
        get
        {
            return DropMinionImage.minionPreviewImage.transform.position;
        }
    }
    public IDropeable CurrentDraggedDropeable 
    {
        set
        {
            OnMinionCanDrop += DropMinionActivated; 
            SceneDragableFeature.OnSceneDragableDragEnd += EndActionsWhenMinionIsNotDropped;
            SetDropMinionPreview(value);
        }
    }

    public bool MinionCanDrop
    {
        set
        {
            OnMinionCanDrop?.Invoke();
            if (value)
            {
                SceneDragableFeature.OnSceneDragableDragEnd += EndActionsWhenMinionDropped;
                SceneDragableFeature.OnSceneDragableDragEnd -= EndActionsWhenMinionIsNotDropped;
            }
            else
            {
                SceneDragableFeature.OnSceneDragableDragEnd += EndActionsWhenMinionIsNotDropped;
                SceneDragableFeature.OnSceneDragableDragEnd -= EndActionsWhenMinionDropped;
            }
        }
    }

    void SetDropMinionPreview(IDropeable currentDropeable)
    {
        DropMinionImage.MinionPreviewImage.sprite = currentDropeable.DropeablePreviewSprite;
    }

    void DropMinionActivated()
    {
        SceneDragableFeature.OnDrop += DropSceneDragableToDropPosition;
        OnMinionCanDrop += DropMinionDesActivated;
        OnMinionCanDrop -= DropMinionActivated;
    }
    void DropMinionDesActivated()
    {
        SceneDragableFeature.OnDrop -= DropSceneDragableToDropPosition;
        OnMinionCanDrop += DropMinionActivated;
        OnMinionCanDrop -= DropMinionDesActivated;
    }
    void EndActionsWhenMinionDropped()
    {
        OnMinionCanDrop -= DropMinionDesActivated;
        SceneDragableFeature.OnSceneDragableDragEnd -= EndActionsWhenMinionDropped;
    }
    void EndActionsWhenMinionIsNotDropped()
    {
        OnMinionCanDrop -= DropMinionActivated;
        SceneDragableFeature.OnSceneDragableDragEnd -= EndActionsWhenMinionIsNotDropped;
    }

    void DropSceneDragableToDropPosition(ISceneDragable currentSceneDragable)
    {
        StartCoroutine(MoveDropeable(currentSceneDragable.SceneDragableTransform));
        SceneDragableFeature.OnDrop -= DropSceneDragableToDropPosition;
    }
    IEnumerator MoveDropeable(Transform cardTransform)
    {
        float sMoothing = 10.0f;
        while (Vector3.Distance(cardTransform.position, DropPosition) > 0.05f)
        {
            cardTransform.position = Vector3.Lerp(cardTransform.position, DropPosition, sMoothing * Time.deltaTime);
            yield return null;
        }
        cardTransform.position = DropPosition;
    }
}
