using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewDropMinionManager : MonoBehaviour
{
    public Image minionPreviewImage;

    public Sprite MinionPreview
    {
        set
        {
            if (value!=null)
            {
                BattleDropeablePlace.OnActiveMinionPreview += ActiveMinionPreview;
                CardController.OnCardEnd += EndMinionPreview;
            }
                minionPreviewImage.sprite = value;
        }
    }

    public void ActiveMinionPreview(Vector3 dropPosition)
    {
        gameObject.SetActive(true);
        transform.position = dropPosition;
        BattleDropeablePlace.OnActiveMinionPreview -= ActiveMinionPreview;
        BattleDropeablePlace.OnActiveMinionPreview += DesactiveMinionPreview;
    }

    public void DesactiveMinionPreview(Vector3 dropPosition)
    {
        gameObject.SetActive(false);
        BattleDropeablePlace.OnActiveMinionPreview -= DesactiveMinionPreview;
        BattleDropeablePlace.OnActiveMinionPreview += ActiveMinionPreview;
    }

    void EndMinionPreview()
    {
        if (gameObject.activeInHierarchy) DesactiveMinionPreview(Vector3.zero);
        BattleDropeablePlace.OnActiveMinionPreview -= ActiveMinionPreview;
        MinionPreview = null;
        CardController.OnCardEnd -= EndMinionPreview;
    }
}
