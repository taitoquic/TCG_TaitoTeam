using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    //public Camera cam;
    float distance;
    Ray ray;

    public delegate void onCardMove(Transform cardTransform);
    public static event onCardMove OnDrop;
    private void OnMouseDown()
    {
        GetComponent<MeshCollider>().enabled = false;
        distance = Vector3.Distance(Camera.main.transform.position, transform.position);
    }

    private void OnMouseDrag()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        transform.position = ray.GetPoint(distance);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            //Debug.Log(hitInfo.collider.gameObject.name);
        }
    }

    private void OnMouseUp()
    {
        OnDrop?.Invoke(transform);
        GetComponent<MeshCollider>().enabled = true;
    }
}
