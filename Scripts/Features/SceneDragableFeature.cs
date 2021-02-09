using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDragableFeature : MonoBehaviour
{
    public Camera cam;
    float distance;
    Ray ray;
    Vector3 initialPosition;
    //ISceneDragable currentSceneDragable;

    public DragableActions OnDragableSceneActions;

    //public delegate void onCardMove(Transform cardTransform);
    //public static event onCardMove OnDrop;

    //public delegate void CardActions();
    //public static event CardActions OnCardEnd;

    public bool DraggingCurrentSceneDragable
    {
        set
        {
            if (value)
            {
                OnDragableSceneActions += OnSceneDragableMouseDown;
            }
            else
            {
                OnDragableSceneActions -= OnSceneDragableMouseDrag;
                OnDragableSceneActions += OnSceneDragableMouseUp;
            }
        }
    }

    void OnSceneDragableMouseDown(ISceneDragable currentSceneDragable)
    {
        currentSceneDragable.SceneDragableMesh.enabled = false;
        distance = Vector3.Distance(cam.transform.position, currentSceneDragable.SceneDragableTransform.position);
        initialPosition = currentSceneDragable.SceneDragableTransform.position;
        OnDragableSceneActions -= OnSceneDragableMouseDown;
        OnDragableSceneActions += OnSceneDragableMouseDrag;
    }

    public void OnSceneDragableMouseDrag(ISceneDragable currentSceneDragable)
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);
        currentSceneDragable.SceneDragableTransform.position = ray.GetPoint(distance);
    }
    
    void OnSceneDragableMouseUp(ISceneDragable currentSceneDragable)
    {
        currentSceneDragable.SceneDragableMesh.enabled = true;
        //if (OnDrop == null) OnDrop += CardResetPosition;
        //OnDrop?.Invoke(transform);
        //OnCardEnd?.Invoke();
        OnDragableSceneActions -= OnSceneDragableMouseUp;
    }

    void CardResetPosition(ISceneDragable currentSceneDragable)
    {
        currentSceneDragable.SceneDragableTransform.position = initialPosition;
        //OnDrop -= CardResetPosition;
    }
}
