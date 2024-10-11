using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For cosmetic purposes
/// </summary>
public enum Rarity
{
    Common,
    Rare,
    Epic,
    Legendary
}

/// <summary>
/// Basic Item class. Holds basic information for all types of items used ingame.
/// </summary>
public abstract class Item : MonoBehaviour
{
    
     [Header("Object Params")] [SerializeField] protected Sprite sprite;
     protected string name;
     protected string description;
     protected Rarity rarity;
}
