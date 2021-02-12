using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropeableFeature : MonoBehaviour
{
    public Transform CollidersForDropToBF;
    public Transform dropMinionTransform;

    public delegate void DropMinionAction();
    public DropMinionAction OnMinionCanDrop;
    public static event DropMinionAction OnDropMinionEnd;

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
            OnMinionCanDrop += PrepareSceneDragableToDrop; //evento queda abierto
            SetDropMinionPreview(value);
        }
    }
    public bool MinionCanDrop
    {
        set
        {
            if (value)
            {
                OnMinionCanDrop?.Invoke();
                OnMinionCanDrop -= PrepareSceneDragableToDrop;
            }
            else OnMinionCanDrop += PrepareSceneDragableToDrop;//evento queda abierto
        }
    }
    void SetDropMinionPreview(IDropeable currentDropeable)
    {
        DropMinionImage.MinionPreviewImage.sprite = currentDropeable.DropeablePreviewSprite;
    }
    void PrepareSceneDragableToDrop()
    {
        SceneDragableFeature.OnDrop += DropSceneDragableToDropPosition;
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
        //BattleDropeablePlace.OnDropMove -= MoveDropeable;
        //SceneDragableFeature.OnSceneDragableDragEnd -= NullPreviewSprite;
    }
    
}
