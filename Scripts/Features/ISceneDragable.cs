using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneDragable
{
    MeshCollider SceneDragableMesh { get; }
    Transform SceneDragableTransform { get; }
    ISceneDragable CurrentDragable{ set; }
    void SetSceneDragableState(bool isBeginDragged);
}
