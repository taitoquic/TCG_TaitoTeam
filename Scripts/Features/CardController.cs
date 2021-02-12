using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour, ISceneDragable
{
    public MeshCollider SceneDragableMesh
    {
        get
        {
            return GetComponent<MeshCollider>();
        }
    }
    public Transform SceneDragableTransform
    {
        get
        {
            return transform;
        }
    }
    public DragableActions OnDragableActions
    {
        get
        {
            return GameManager.instance.sceneDragableFeature.OnSceneDragableActions;
        }

        set
        {
            GameManager.instance.sceneDragableFeature.OnSceneDragableActions += value;
        }
    }
    public bool IsDraggingSceneDragable
    {
        set
        {
            GameManager.instance.sceneDragableFeature.IsDraggingCurrentSceneDragable = value;
        }
    }
    //public bool IsDroppingSceneDragable
    //{
    //    get
    //    {
    //        return OnCardDrop != null;
    //    }
    //}

    public CardAsset card;

    //public delegate void CardMoved(Transform cardTransform);
    //public static event CardMoved OnCardDrop;

    private void OnMouseDown()
    {
        IsDraggingSceneDragable = true;
        card.PlayCard();
        OnDragableActions?.Invoke(this);
    }

    private void OnMouseDrag()
    {
        OnDragableActions?.Invoke(this);
    }

    private void OnMouseUp()
    {
        IsDraggingSceneDragable = false;
        OnDragableActions.Invoke(this);
        //OnCardDrop?.Invoke(transform);
        //OnDragableActions?.Invoke(this);
    }
}
