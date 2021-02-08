using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    //public Camera cam;
    float distance;
    Ray ray;
    Vector3 initialPosition;

    public CardAsset card;

    public delegate void onCardMove(Transform cardTransform);
    public static event onCardMove OnDrop;

    public delegate void CardActions();
    public static event CardActions OnCardEnd;
    private void OnMouseDown()
    {
        GetComponent<MeshCollider>().enabled = false;
        distance = Vector3.Distance(Camera.main.transform.position, transform.position);
        initialPosition = transform.position;
        card.PlayCard();
    }

    private void OnMouseDrag()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        transform.position = ray.GetPoint(distance);

        //if (Physics.Raycast(ray, out RaycastHit hitInfo))
        //{
        //    Debug.Log(hitInfo.collider.gameObject.name);
        //}
    }

    private void OnMouseUp()
    {
        OnDrop?.Invoke(transform);
        OnCardEnd?.Invoke();
        GetComponent<MeshCollider>().enabled = true;
    }

    void CardResetPosition()
    {
        //transform.position =
    }
}
