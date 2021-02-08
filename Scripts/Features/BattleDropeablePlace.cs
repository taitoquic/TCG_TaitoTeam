using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDropeablePlace : MonoBehaviour
{
    public delegate void ActiveMinionPreview(Vector3 dropPosition);
    public static event ActiveMinionPreview OnActiveMinionPreview;

    public delegate Sprite MinionPreviewSprite();
    public static event MinionPreviewSprite OnMinionSpriteInImage;
    int PositionInLine
    {
        get
        {
            return transform.GetSiblingIndex();
        }
    }
    int IndexLine
    {
        get
        {
            return transform.parent.GetSiblingIndex();
        }
    }
    Transform BattleField
    {
        get
        {
            return transform.parent.parent.parent.GetChild(0);
        }
    }
    Transform LocalPlayerLines
    {
        get
        {
            return BattleField.GetChild(0);
        }
    }
    PreviewDropMinionManager PreviewBF
    {
        get
        {
            return BattleField.GetChild(1).GetComponent<PreviewDropMinionManager>();
        }
    }
    Vector3 DropPosition
    {
        get
        {
            return LocalPlayerLines.GetChild(IndexLine).GetChild(PositionInLine).position;
        }
    }

    private void OnMouseEnter()
    {
        //Debug.Log("hola");
        if(OnMinionSpriteInImage!= null) PreviewBF.MinionPreview = OnMinionSpriteInImage?.Invoke();
        OnActiveMinionPreview?.Invoke(DropPosition);
        CardController.OnDrop += MoveCard;
    }

    private void OnMouseExit()
    {
        OnActiveMinionPreview?.Invoke(DropPosition);
        CardController.OnDrop -= MoveCard;
    }

    void MoveCard(Transform cardTransform)
    {
        //cardTransform.position = DropPosition;
        StartCoroutine(DropMinion(cardTransform));
    }

    IEnumerator DropMinion(Transform cardTransform)
    {
        float sMoothing = 10.0f;
        while (Vector3.Distance(cardTransform.position, DropPosition) > 0.05f)
        {
            cardTransform.position = Vector3.Lerp(cardTransform.position, DropPosition, sMoothing * Time.deltaTime);
            //cardTransform.rotation = Quaternion.Lerp(cardTransform.rotation, Quaternion.Euler(new Vector3(90f, 0, 0)), sMoothing * Time.deltaTime);
            yield return null;
        }
        cardTransform.position = DropPosition;
    }
}
