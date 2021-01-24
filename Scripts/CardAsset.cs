using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardAsset : ScriptableObject
{
    public new string name;
    public int id;
    public Sprite art;
    public CardCollection cardCollection;
    public CardLevel cardLevel;
}
public enum CardCollection { Basic}
public enum CardLevel { Commun, Rare, Epic, Legendary }