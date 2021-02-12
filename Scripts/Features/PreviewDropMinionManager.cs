using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewDropMinionManager : MonoBehaviour
{
    public Image minionPreviewImage;
    delegate bool MinionPreviewImageAppear();
    MinionPreviewImageAppear OnMPIsOn;

    public Image MinionPreviewImage
    {
        get
        {
            OnMPIsOn += MPIsOn;
            BattleDropeablePlace.OnActiveMinionPreview += ActiveMinionPreview;
            BattleDropeablePlace.OnMinionPicked += MinionPreviewReady;
            SceneDragableFeature.OnSceneDragableStopDrag += EndMinionPreview;
            return minionPreviewImage;
        }
    }
    bool ActiveMinionPreview(Vector3 dropPosition)
    {
        minionPreviewImage.transform.position = dropPosition;
        return OnMPIsOn.Invoke();
    }
    bool DesactiveMinionPreview(Vector3 dropPosition)
    {
        return OnMPIsOn.Invoke();
    }

    bool MPIsOn()
    {
        gameObject.SetActive(true);
        BattleDropeablePlace.OnActiveMinionPreview -= ActiveMinionPreview;
        BattleDropeablePlace.OnActiveMinionPreview += DesactiveMinionPreview;
        OnMPIsOn += MPIsOff;
        OnMPIsOn -= MPIsOn;
        return true;
    }
    bool MPIsOff()
    {
        gameObject.SetActive(false);
        BattleDropeablePlace.OnActiveMinionPreview -= DesactiveMinionPreview;
        BattleDropeablePlace.OnActiveMinionPreview += ActiveMinionPreview;
        OnMPIsOn += MPIsOn;
        OnMPIsOn -= MPIsOff;
        return false;
    }
    void MinionPreviewReady(bool isMinionPreviewReady)
    {
        GameManager.instance.dropeableFeature.MinionCanDrop = isMinionPreviewReady;
    }
    void EndMinionPreview()
    {
        if (gameObject.activeInHierarchy) OnMPIsOn.Invoke();
        BattleDropeablePlace.OnActiveMinionPreview -= ActiveMinionPreview;
        OnMPIsOn -= MPIsOn;
        minionPreviewImage.sprite = null;
        BattleDropeablePlace.OnMinionPicked -= MinionPreviewReady;
        SceneDragableFeature.OnSceneDragableStopDrag -= EndMinionPreview;
    }
}
