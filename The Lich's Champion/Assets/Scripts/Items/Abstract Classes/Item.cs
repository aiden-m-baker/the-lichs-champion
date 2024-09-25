using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rarity
{
    Common,
    Rare,
    Epic,
    Legendary
}

public abstract class Item : MonoBehaviour
{
     [SerializeField] protected Sprite sprite;
     protected string name;
     protected string description;
     protected Rarity rarity;
}
