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
    [SerializeField] protected string name;
    [SerializeField] protected string description;
    [SerializeField] protected Rarity rarity;
    [SerializeField] protected Sprite sprite;
}
