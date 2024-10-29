using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Rarity of items. For cosmetic purposes.
/// </summary>
public enum Rarity
{
    Common,
    Rare,
    Epic,
    Legendary
}

/// <summary>
/// Basic Item class. Holds basic information for items obtained by the player. All weapons and utility inherit from this class.
/// </summary>
public abstract class Item : MonoBehaviour
{
    [Header("Object Params")]

    [SerializeField] [Tooltip("The sprite used by the item. Can be used for animations and instantiation.")] 
    protected Sprite sprite;

    [SerializeField] [Tooltip("The prefab used by the item. Can be used for instatiation.")]
    protected GameObject prefab;

    [SerializeField] [Tooltip("The gameObject holding the SpriteRenderer for the item's gameObject. Can be used for animations.")]
    protected GameObject spriteObject;

    [Tooltip("The name of the item. Can be used for UI.")]
    protected string itemName;

    [Tooltip("The description of the item. Can be used for UI.")]
    protected string description;

    [Tooltip("The rarity of the item. Can be used for loot tables, UI, and cosmetic purposes.")]
    protected Rarity rarity;

    #region Properties
    public Sprite Sprite { get { return sprite; } }
    public GameObject Prefab { get { return prefab; } }
    public GameObject SpriteObject { get { return spriteObject; } }
    #endregion
}
