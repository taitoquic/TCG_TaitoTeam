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
    //public static event DragableActions OnDrop;

    //public delegate void sceneDragableMove(ISceneDragable currentSceneDragable);

    public delegate void SceneDragabledActions();
    public static event SceneDragabledActions OnSceneDragableDragEnd;

    //public static DragableActions IsSceneDragableDropped
    //{
    //    get
    //    {
    //        return OnDrop;
    //    }
    //    set
    //    {
    //        OnDrop -= CardResetPosition;
    //        OnDrop += value;
    //    }
    //}

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
                OnSceneDragableActions += OnSceneDragableDrop;
            }
        }
    }

    //public bool IsDroppingCurrentSceneDragable
    //{
    //    set
    //    {
    //        if (!value) OnSceneDragableActions += SceneDragableResetPosition;
    //        OnSceneDragableActions += OnSceneDragableMouseUp;
    //    }
    //}

    void OnSceneDragableMouseDown(ISceneDragable currentSceneDragable)
    {
        currentSceneDragable.SceneDragableMesh.enabled = false;
        distance = Vector3.Distance(cam.transform.position, currentSceneDragable.SceneDragableTransform.position);
        initialPosition = currentSceneDragable.SceneDragableTransform.position;
        OnSceneDragableActions -= OnSceneDragableMouseDown;
        //OnDrop += CardResetPosition;
        OnSceneDragableActions += OnSceneDragableMouseDrag;
    }

    public void OnSceneDragableMouseDrag(ISceneDragable currentSceneDragable)
    {
        ray = cam.ScreenPointToRay(Input.mousePosition);
        currentSceneDragable.SceneDragableTransform.position = ray.GetPoint(distance);
    }

    public void OnSceneDragableDrop(ISceneDragable currentSceneDragable)
    {
        if (!currentSceneDragable.IsDroppingSceneDragable) OnSceneDragableActions += SceneDragableResetPosition;
        OnSceneDragableActions += OnSceneDragableMouseUp;
        OnSceneDragableActions -= OnSceneDragableDrop;
    }
    
    void OnSceneDragableMouseUp(ISceneDragable currentSceneDragable)
    {
        currentSceneDragable.SceneDragableMesh.enabled = true;
        //if (OnDrop == null) OnDrop += CardResetPosition;
        //OnDrop?.Invoke(currentSceneDragable);
        OnSceneDragableDragEnd?.Invoke();
        OnSceneDragableActions -= OnSceneDragableMouseUp;
    }
    
    void SceneDragableResetPosition(ISceneDragable currentSceneDragable)
    {
        currentSceneDragable.SceneDragableTransform.position = initialPosition;
        OnSceneDragableActions -= SceneDragableResetPosition;
        //OnDrop -= CardResetPosition;
    }
}
