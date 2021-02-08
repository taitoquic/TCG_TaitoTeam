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
                value.OnDragableActions += OnSceneDragableMouseDown;
                value.OnDragableActions?.Invoke();
            }
            else
            {
                currentSceneDragable.OnDragableActions -= OnSceneDragableMouseDrag;
                currentSceneDragable.OnDragableActions += OnSceneDragableMouseUp;
                currentSceneDragable.OnDragableActions?.Invoke();
            }
            currentSceneDragable = value;
        }
    }
    
    void OnSceneDragableMouseDown()
    {
        CurrentSceneDragable.SceneDragableMesh.enabled = false;
        distance = Vector3.Distance(cam.transform.position, CurrentSceneDragable.SceneDragableTransform.position);
        initialPosition = CurrentSceneDragable.SceneDragableTransform.position;
        CurrentSceneDragable.OnDragableActions -= OnSceneDragableMouseDown;
        CurrentSceneDragable.OnDragableActions += OnSceneDragableMouseDrag;
    }

    public void OnSceneDragableMouseDrag()
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);
        CurrentSceneDragable.SceneDragableTransform.position = ray.GetPoint(distance);
    }
    
    void OnSceneDragableMouseUp()
    {
        CurrentSceneDragable.SceneDragableMesh.enabled = true;
        //if (OnDrop == null) OnDrop += CardResetPosition;
        //OnDrop?.Invoke(transform);
        //OnCardEnd?.Invoke();
        CurrentSceneDragable.OnDragableActions -= OnSceneDragableMouseUp;
    }

    void CardResetPosition()
    {
        CurrentSceneDragable.SceneDragableTransform.position = initialPosition;
        //OnDrop -= CardResetPosition;
    }
}
