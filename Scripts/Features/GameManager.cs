using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region SINGLETON
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion

    public SceneDragableFeature sceneDragableFeature;
    public DropeableFeature dropeableFeature;

}
