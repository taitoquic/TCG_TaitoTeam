using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    private void OnMouseDown()
    {
        GetComponent<MeshCollider>().enabled = false;
    }

    private void OnMouseDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            transform.position = new Vector3(hitInfo.point.x, 7.0f, hitInfo.point.z);
        }
    }

    private void OnMouseUp()
    {
        GetComponent<MeshCollider>().enabled = true;
    }
}
