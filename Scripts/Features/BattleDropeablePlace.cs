using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDropeablePlace : MonoBehaviour
{
    public int PositionInLine
    {
        get
        {
            return transform.GetSiblingIndex();
        }
    }
    public int IndexLine
    {
        get
        {
            return transform.parent.GetSiblingIndex();
        }
    }
    Transform LocalPlayerLines
    {
        get
        {
            return transform.parent.parent.parent.GetChild(0).GetChild(0);
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
        CardController.OnDrop += MoveCard;
    }

    private void OnMouseExit()
    {
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
