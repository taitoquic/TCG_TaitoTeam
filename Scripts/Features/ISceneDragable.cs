using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DragableActions(ISceneDragable currentSceneDragable);
public interface ISceneDragable
{
    public MeshCollider SceneDragableMesh { get; }
    public Transform SceneDragableTransform { get; }
    public DragableActions OnDragableActions { get; set; }
    public bool IsDraggingSceneDragable { set; }
}
