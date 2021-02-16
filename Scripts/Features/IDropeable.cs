using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDropeable 
{
    void PlayDropeable();
    Sprite DropeablePreviewSprite { get; }
}
