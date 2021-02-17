using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDragableFeature : MonoBehaviour
{
    float xOffset = 45;
    float yOffset = 60;
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

    Vector2 ValidMousePosition
    {
        get
        {
            float validXPosition = Mathf.Clamp(Input.mousePosition.x, xOffset, Screen.width - xOffset);
            float validYPosition = Mathf.Clamp(Input.mousePosition.y, yOffset, Screen.height - yOffset);
            return new Vector2(validXPosition, validYPosition);
        }
    }
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
        ray = cam.ScreenPointToRay(ValidMousePosition);
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
