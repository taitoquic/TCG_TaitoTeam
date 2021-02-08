using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDragableFeature : MonoBehaviour
{
    public Camera cam;
    float distance;
    Ray ray;
    Vector3 initialPosition;
    ISceneDragable currentSceneDragable;

    //public delegate void onCardMove(Transform cardTransform);
    //public static event onCardMove OnDrop;

    //public delegate void CardActions();
    //public static event CardActions OnCardEnd;

    public ISceneDragable CurrentSceneDragable
    {
        get
        {
            return currentSceneDragable;
        }
        set
        {
            if(value != null)
            {
                value.OnDragableSelected += OnSceneDragableMouseDown;
                value.OnDragableSelected?.Invoke(value);
            }
            else
            {
                currentSceneDragable.OnDragableSelected -= OnSceneDragableMouseDrag;
                currentSceneDragable.OnDragableSelected += OnSceneDragableMouseUp;
                currentSceneDragable.OnDragableSelected?.Invoke(currentSceneDragable);
            }
            currentSceneDragable = value;
        }
    }
    
    void OnSceneDragableMouseDown(ISceneDragable currentSceneDragable)
    {
        currentSceneDragable.SceneDragableMesh.enabled = false;
        distance = Vector3.Distance(cam.transform.position, currentSceneDragable.SceneDragableTransform.position);
        initialPosition = currentSceneDragable.SceneDragableTransform.position;
        currentSceneDragable.OnDragableSelected -= OnSceneDragableMouseDown;
        currentSceneDragable.OnDragableSelected += OnSceneDragableMouseDrag;
    }

    public void OnSceneDragableMouseDrag(ISceneDragable currentSceneDragable)
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);
        currentSceneDragable.SceneDragableTransform.position = ray.GetPoint(distance);
    }
    
    void OnSceneDragableMouseUp(ISceneDragable currentSceneDragable)
    {
        //if (OnDrop == null) OnDrop += CardResetPosition;
        //OnDrop?.Invoke(transform);
        //OnCardEnd?.Invoke();
        currentSceneDragable.SceneDragableMesh.enabled = true;
        currentSceneDragable.OnDragableSelected -= OnSceneDragableMouseUp;
    }

    void CardResetPosition(ISceneDragable currentSceneDragable)
    {
        currentSceneDragable.SceneDragableTransform.position = initialPosition;
        //OnDrop -= CardResetPosition;
    }
}
