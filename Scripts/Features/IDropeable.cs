using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDropeable 
{
    
    IEnumerator MoveDropeable(ISceneDragable currentSceneDragable, Vector3 dropPosition);
}
