using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropeableBoardPlace : MonoBehaviour
{
    public Transform DropBFColliders;

    public void ActiveBFDropeableColliders()
    {
        DropBFColliders.gameObject.SetActive(true);
        //SceneDragableFeature.OnSceneDragableDragEnd += DesActiveBFDropeableColliders;
    }
    void DesActiveBFDropeableColliders()
    {
        DropBFColliders.gameObject.SetActive(false);
        SceneDragableFeature.OnSceneDragableDragEnd -= DesActiveBFDropeableColliders;
    }
}
