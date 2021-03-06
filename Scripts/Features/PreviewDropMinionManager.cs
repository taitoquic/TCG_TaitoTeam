using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewDropMinionManager : MonoBehaviour
{
    public Image minionPreviewImage;

    public delegate void OnImageValid();
    public static event OnImageValid OnSpriteInImageAction;
    public Image MinionPreviewImage
    {
        get
        {
            BattleDropeablePlace.OnActiveMinionPreview += ActiveMinionPreview;
            OnSpriteInImageAction?.Invoke();
            SceneDragableFeature.OnSceneDragableStopDrag += EndMinionPreview;
            return minionPreviewImage;
        }
    }

    private void OnEnable()
    {
        BattleDropeablePlace.OnActiveMinionPreview -= ActiveMinionPreview;
        BattleDropeablePlace.OnActiveMinionPreview += DesactiveMinionPreview;
    }
    private void OnDisable()
    {
        BattleDropeablePlace.OnActiveMinionPreview -= DesactiveMinionPreview;
        BattleDropeablePlace.OnActiveMinionPreview += ActiveMinionPreview;
    }

    bool ActiveMinionPreview(Vector3 placeToMoveMP)
    {
        minionPreviewImage.transform.position = placeToMoveMP;
        gameObject.SetActive(true);
        return true;
    }

    bool DesactiveMinionPreview(Vector3 placeToMoveMP)
    {
        gameObject.SetActive(false);
        return false;
    }

    void EndMinionPreview()
    {
        if (gameObject.activeInHierarchy) gameObject.SetActive(false);
        OnSpriteInImageAction?.Invoke();
        BattleDropeablePlace.OnActiveMinionPreview -= ActiveMinionPreview;
        minionPreviewImage.sprite = null;
        SceneDragableFeature.OnSceneDragableStopDrag -= EndMinionPreview;
    }
}
