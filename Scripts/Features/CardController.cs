using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour, ISceneDragable
{
    #region DRAGABLE_VARIABLES
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
    public ISceneDragable CurrentDragable
    {
        set
        {
            GameManager.instance.sceneDragableFeature.CurrentDragableDragged = value;
        }
    }
    public void SetSceneDragableState(bool isCurrentDragableBeginDrag)
    {
        if (isCurrentDragableBeginDrag) GameManager.instance.sceneDragableFeature.CurrentDragableBeginDragged = this;
        else GameManager.instance.sceneDragableFeature.CurrentDragableEndDragged = this;
    }
    #endregion

    public CardAsset card;

    private void OnMouseDown()
    {
        card.PlayCard();
        SetSceneDragableState(true);
    }

    private void OnMouseDrag()
    {
        CurrentDragable = this;
    }

    private void OnMouseUp()
    {
        SetSceneDragableState(false);
    }
}
