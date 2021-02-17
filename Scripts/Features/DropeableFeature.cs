using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropeableFeature : MonoBehaviour
{
    public GameObject CollidersForDropToBF;
    public Transform dropMinionTransform;

    public delegate void DropeableMoveActions(ISceneDragable currentSceneDragable);
    public DropeableMoveActions OnDropingMoveActions;

    public delegate void DropeableActions();
    public static event DropeableActions OnDropActions;
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
        CollidersForDropToBF.SetActive(true);
        PreviewDropMinionManager.OnSpriteInImageAction += DesActiveDropeableBoardPlace;
        PreviewDropMinionManager.OnSpriteInImageAction -= ActiveDropeableBoardPlace;
    }
    void DesActiveDropeableBoardPlace()
    {
        CollidersForDropToBF.SetActive(false);
        BattleDropeablePlace.OnMPIsActive -= SelectIfDropeableCanDrop;
        PreviewDropMinionManager.OnSpriteInImageAction -= DesActiveDropeableBoardPlace;
    }

    void SelectIfDropeableCanDrop(bool mpActive)
    {
        if (mpActive)
        {
            SceneDragableFeature.OnDrop += DropSceneDragableToDropPosition;
        } 
        else SceneDragableFeature.OnDrop -= DropSceneDragableToDropPosition;
    }

    void ResetMeshColliderAtEndDrop(ISceneDragable currentSceneDragable)
    {
        currentSceneDragable.SceneDragableMesh.enabled = true;
        OnDropingMoveActions -= ResetMeshColliderAtEndDrop;
    }

    void DropSceneDragableToDropPosition(ISceneDragable currentSceneDragable)
    {
        OnDropActions?.Invoke();
        OnDropingMoveActions += ResetMeshColliderAtEndDrop;
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
        OnDropingMoveActions?.Invoke(currentSceneDragable);
    }
}