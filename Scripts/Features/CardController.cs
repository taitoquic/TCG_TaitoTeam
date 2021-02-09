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
            return GameManager.instance.sceneDragableFeature.OnDragableSceneActions;
        }

        set
        {
            GameManager.instance.sceneDragableFeature.OnDragableSceneActions += value;
        }
    }
    public bool IsDraggingSceneDragable
    {
        set
        {
            GameManager.instance.sceneDragableFeature.DraggingCurrentSceneDragable = value;
        }
    }

    public CardAsset card;


    public delegate void onCardMove(Transform cardTransform);
    public static event onCardMove OnDrop;

    public delegate void CardActions();
    public static event CardActions OnCardEnd;

    private void OnMouseDown()
    {
        IsDraggingSceneDragable = true;
        card.PlayCard();
        OnDragableActions?.Invoke(this);
    }

    private void OnMouseDrag()
    {
        OnDragableActions?.Invoke(this);
        //if (Physics.Raycast(ray, out RaycastHit hitInfo))
        //{
        //    Debug.Log(hitInfo.collider.gameObject.name);
        //}
    }

    private void OnMouseUp()
    {
        IsDraggingSceneDragable = false;
        OnDragableActions?.Invoke(this);
        //if (OnDrop == null) OnDrop += CardResetPosition;
        OnDrop?.Invoke(transform);
        OnCardEnd?.Invoke();
        //currentSceneDragable.SceneDragableMesh.enabled = true;
    }
}
