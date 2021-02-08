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
    public DragableInScene OnDragableSelected { get; set; }

    public CardAsset card;


    public delegate void onCardMove(Transform cardTransform);
    public static event onCardMove OnDrop;

    public delegate void CardActions();
    public CardActions OnCardPickedByMouse;
    public static event CardActions OnCardEnd;

    private void OnMouseDown()
    {
        OnCardPickedByMouse += card.PlayCard;
        OnCardPickedByMouse += SetSceneDragable;
        OnCardPickedByMouse?.Invoke();
    }

    private void OnMouseDrag()
    {
        OnDragableSelected?.Invoke(this);
        //if (Physics.Raycast(ray, out RaycastHit hitInfo))
        //{
        //    Debug.Log(hitInfo.collider.gameObject.name);
        //}
    }

    private void OnMouseUp()
    {
        OnCardPickedByMouse?.Invoke();
        //if (OnDrop == null) OnDrop += CardResetPosition;
        OnDrop?.Invoke(transform);
        OnCardEnd?.Invoke();
        //currentSceneDragable.SceneDragableMesh.enabled = true;
    }

    public void SetSceneDragable()
    {
        GameManager.instance.sceneDragableFeature.CurrentSceneDragable = this;
        OnCardPickedByMouse += SetSceneDragableNull;
        OnCardPickedByMouse -= card.PlayCard;
        OnCardPickedByMouse -= SetSceneDragable;
    }
    public void SetSceneDragableNull()
    {
        GameManager.instance.sceneDragableFeature.CurrentSceneDragable = null;
        OnCardPickedByMouse -= SetSceneDragableNull;
    }
}
