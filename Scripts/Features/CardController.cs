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
            return OnCardDragged;
        }
        set
        {
            OnCardDragged += value;
        }
    }
    public ISceneDragable CurrentSceneDragable
    {
        set
        {
            GameManager.instance.sceneDragableFeature.CurrentSceneDragable = value;
        }
    }

    public CardAsset card;
    public DragableActions OnCardDragged;


    public delegate void onCardMove(Transform cardTransform);
    public static event onCardMove OnDrop;

    public delegate void CardActions();
    public static event CardActions OnCardEnd;

    private void OnMouseDown()
    {
        OnCardDragged += card.PlayCard;
        CurrentSceneDragable = this;
    }

    private void OnMouseDrag()
    {
        OnCardDragged?.Invoke();
        //if (Physics.Raycast(ray, out RaycastHit hitInfo))
        //{
        //    Debug.Log(hitInfo.collider.gameObject.name);
        //}
    }

    private void OnMouseUp()
    {
        CurrentSceneDragable = null;
        //if (OnDrop == null) OnDrop += CardResetPosition;
        OnDrop?.Invoke(transform);
        OnCardEnd?.Invoke();
        //currentSceneDragable.SceneDragableMesh.enabled = true;
    }
}
