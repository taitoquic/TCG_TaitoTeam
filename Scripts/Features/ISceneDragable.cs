using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DragableInScene(ISceneDragable currentSceneDragable);
public interface ISceneDragable
{
    public MeshCollider SceneDragableMesh { get; }
    public Transform SceneDragableTransform { get; }
    public DragableInScene OnDragableSelected { get; set; }

    public void SetSceneDragable();
    public void SetSceneDragableNull();
}
