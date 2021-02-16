using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDragableFeature : MonoBehaviour
{
    public Camera cam;
    float distance;
    Ray ray;
    Vector3 initialPosition;

    public delegate void DragableActions(ISceneDragable currentSceneDragable);
    public DragableActions OnSceneDragableActions;
    public static event DragableActions OnDrop;

    public delegate void SceneDragabledActions();
    public static event SceneDragabledActions OnSceneDragableStopDrag;
    public static event SceneDragabledActions OnSceneDragableDragEnd;

    public ISceneDragable CurrentDragableDragged
    {
        set
        {
            OnSceneDragableActions?.Invoke(value);
        }
    }
    public ISceneDragable CurrentDragableBeginDragged
    {
        set
        {
            OnSceneDragableMouseDown(value);
        }
    }
    public ISceneDragable CurrentDragableEndDragged
    {
        set
        {
            OnSceneDragableActions -= OnSceneDragableMouseDrag;
            OnSceneDragableStopDrag?.Invoke();
            if (OnDrop == null) OnSceneDragableActions += SceneDragableResetPosition;
            OnSceneDragableActions += OnSceneDragableMouseUp;
            OnSceneDragableActions?.Invoke(value);
        }
    }

    void OnSceneDragableMouseDown(ISceneDragable currentSceneDragable)
    {
        currentSceneDragable.SceneDragableMesh.enabled = false;
        distance = Vector3.Distance(cam.transform.position, currentSceneDragable.SceneDragableTransform.position);
        initialPosition = currentSceneDragable.SceneDragableTransform.position;
        OnSceneDragableActions += OnSceneDragableMouseDrag;
    }
    
    void OnSceneDragableMouseDrag(ISceneDragable currentSceneDragable)
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);
        currentSceneDragable.SceneDragableTransform.position = ray.GetPoint(distance);
    }
    
    void OnSceneDragableMouseUp(ISceneDragable currentSceneDragable)
    {
        OnDrop?.Invoke(currentSceneDragable);
        OnSceneDragableDragEnd?.Invoke();
        OnSceneDragableActions -= OnSceneDragableMouseUp;
    }
    
    void SceneDragableResetPosition(ISceneDragable currentSceneDragable)
    {
        currentSceneDragable.SceneDragableTransform.position = initialPosition;
        currentSceneDragable.SceneDragableMesh.enabled = true;
        OnSceneDragableActions -= SceneDragableResetPosition;
    }
}
