using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropeableFeature : MonoBehaviour
{
    public Transform CollidersForDropToBF;
    public Transform dropMinionTransform;
    public List<BoxCollider> occupiedPositions = new List<BoxCollider>();

    public delegate void DropeableActions(ISceneDragable currentSceneDragable);
    public DropeableActions OnDropingActions;
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
            return dropMinionTransform.position;
        }
    }
    public IDropeable CurrentDraggedDropeable 
    {
        set
        {
            SetDropMinionPreview(value);
        }
    }
    void SetDropMinionPreview(IDropeable currentDropeable)
    {
        PreviewDropMinionManager.OnSpriteInImageAction += ActiveDropeableBoardPlace;
        BattleDropeablePlace.OnMPIsActive += SelectIfDropeableCanDrop;
        DropMinionImage.MinionPreviewImage.sprite = currentDropeable.DropeablePreviewSprite;
    }

    void ActiveDropeableBoardPlace()
    {
        CollidersForDropToBF.gameObject.SetActive(true);
        PreviewDropMinionManager.OnSpriteInImageAction += DesActiveDropeableBoardPlace;
        PreviewDropMinionManager.OnSpriteInImageAction -= ActiveDropeableBoardPlace;
    }
    void DesActiveDropeableBoardPlace()
    {
        CollidersForDropToBF.gameObject.SetActive(false);
        BattleDropeablePlace.OnMPIsActive -= SelectIfDropeableCanDrop;
        PreviewDropMinionManager.OnSpriteInImageAction -= DesActiveDropeableBoardPlace;
    }

    void SelectIfDropeableCanDrop(bool mpActive)
    {
        if (mpActive) SceneDragableFeature.OnDrop += DropSceneDragableToDropPosition;
        else SceneDragableFeature.OnDrop -= DropSceneDragableToDropPosition;
    }

    void ResetMeshColliderAtEndDrop(ISceneDragable currentSceneDragable)
    {
        currentSceneDragable.SceneDragableMesh.enabled = true;
        OnDropingActions -= ResetMeshColliderAtEndDrop;
    }

    void DropSceneDragableToDropPosition(ISceneDragable currentSceneDragable)
    {
        OnDropingActions += ResetMeshColliderAtEndDrop;
        StartCoroutine(MoveDropeable(currentSceneDragable));
        SceneDragableFeature.OnDrop -= DropSceneDragableToDropPosition;
    }
    IEnumerator MoveDropeable(ISceneDragable currentSceneDragable)
    {
        float sMoothing = 10.0f;
        while (Vector3.Distance(currentSceneDragable.SceneDragableTransform.position, DropPosition) > 0.05f)
        {
            currentSceneDragable.SceneDragableTransform.position = Vector3.Lerp(currentSceneDragable.SceneDragableTransform.position, DropPosition, sMoothing * Time.deltaTime);
            yield return null;
        }
        currentSceneDragable.SceneDragableTransform.position = DropPosition;
        OnDropingActions?.Invoke(currentSceneDragable);
    }
}
//OnDropeablePlace?.Invoke(CollidersForDropToBF);
//OnMinionCanDrop += DropMinionActivated; 
//SceneDragableFeature.OnSceneDragableDragEnd += EndActionsWhenMinionIsNotDropped;

//public bool MinionCanDrop
//{
//    set
//    {
//        OnMinionCanDrop?.Invoke();
//        if (value)
//        {
//            SceneDragableFeature.OnSceneDragableDragEnd += EndActionsWhenMinionDropped;
//            SceneDragableFeature.OnSceneDragableDragEnd -= EndActionsWhenMinionIsNotDropped;
//        }
//        else
//        {
//            SceneDragableFeature.OnSceneDragableDragEnd += EndActionsWhenMinionIsNotDropped;
//            SceneDragableFeature.OnSceneDragableDragEnd -= EndActionsWhenMinionDropped;
//        }
//    }
//}

//void DropMinionActivated()
//{
//    //SceneDragableFeature.OnDrop += DropSceneDragableToDropPosition;
//    OnMinionCanDrop += DropMinionDesActivated;
//    OnMinionCanDrop -= DropMinionActivated;
//}
//void DropMinionDesActivated()
//{
//    //SceneDragableFeature.OnDrop -= DropSceneDragableToDropPosition;
//    OnMinionCanDrop += DropMinionActivated;
//    OnMinionCanDrop -= DropMinionDesActivated;
//}
//void EndActionsWhenMinionDropped()
//{
//    OnMinionCanDrop -= DropMinionDesActivated;
//    OnDropeablePlace?.Invoke(CollidersForDropToBF);
//    SceneDragableFeature.OnSceneDragableDragEnd -= EndActionsWhenMinionDropped;
//}
//void EndActionsWhenMinionIsNotDropped()
//{
//    OnMinionCanDrop -= DropMinionActivated;
//    OnDropeablePlace?.Invoke(CollidersForDropToBF);
//    SceneDragableFeature.OnSceneDragableDragEnd -= EndActionsWhenMinionIsNotDropped;
//}