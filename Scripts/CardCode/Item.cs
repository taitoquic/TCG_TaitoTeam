using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : CardAsset
{
    public override void PlayCard()
    {
        Debug.Log("hola soy item");
    }
}
