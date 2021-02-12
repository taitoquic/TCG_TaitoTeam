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
    public static event DragableActions OnDrop;

    public delegate void SceneDragabledActions();
    public static event SceneDragabledActions OnSceneDragableStopDrag;
    public static event SceneDragabledActions OnSceneDragableDragEnd;

    public bool IsDraggingCurrentSceneDragable
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
                if (OnDrop == null) OnSceneDragableActions += SceneDragableResetPosition;
                OnSceneDragableActions += OnSceneDragableMouseUp;
                OnSceneDragableStopDrag?.Invoke();
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
        OnDrop?.Invoke(currentSceneDragable);
        OnSceneDragableDragEnd?.Invoke();
        OnSceneDragableActions -= OnSceneDragableMouseUp;
    }
    
    void SceneDragableResetPosition(ISceneDragable currentSceneDragable)
    {
        currentSceneDragable.SceneDragableTransform.position = initialPosition;
        OnSceneDragableActions -= SceneDragableResetPosition;
    }
}
