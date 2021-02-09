using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDragableFeature : MonoBehaviour
{
    public Camera cam;
    float distance;
    Ray ray;
    Vector3 initialPosition;

    public DragableActions OnSceneDragableActions;

    public delegate void sceneDragableMove(ISceneDragable currentSceneDragable);
    public static event sceneDragableMove OnDrop;

    public delegate void sceneDragabledActions();
    public static event sceneDragabledActions OnSceneDragableEnd;

    public bool DraggingCurrentSceneDragable
    {
        set
        {
            if (value)
            {
                OnSceneDragableActions += OnSceneDragableMouseDown;
            }
            else
            {
                OnSceneDragableActions -= OnSceneDragableMouseDrag;
                OnSceneDragableActions += OnSceneDragableMouseUp;
            }
        }
    }

    void OnSceneDragableMouseDown(ISceneDragable currentSceneDragable)
    {
        currentSceneDragable.SceneDragableMesh.enabled = false;
        distance = Vector3.Distance(cam.transform.position, currentSceneDragable.SceneDragableTransform.position);
        initialPosition = currentSceneDragable.SceneDragableTransform.position;
        OnSceneDragableActions -= OnSceneDragableMouseDown;
        OnSceneDragableActions += OnSceneDragableMouseDrag;
    }

    public void OnSceneDragableMouseDrag(ISceneDragable currentSceneDragable)
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);
        currentSceneDragable.SceneDragableTransform.position = ray.GetPoint(distance);
    }
    
    void OnSceneDragableMouseUp(ISceneDragable currentSceneDragable)
    {
        currentSceneDragable.SceneDragableMesh.enabled = true;
        if (OnDrop == null) OnDrop += CardResetPosition;
        OnDrop?.Invoke(currentSceneDragable);
        OnSceneDragableEnd?.Invoke();
        OnSceneDragableActions -= OnSceneDragableMouseUp;
    }

    void CardResetPosition(ISceneDragable currentSceneDragable)
    {
        currentSceneDragable.SceneDragableTransform.position = initialPosition;
        OnDrop -= CardResetPosition;
    }
}
