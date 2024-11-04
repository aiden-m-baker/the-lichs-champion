using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// 
/// </summary>
[DisallowMultipleComponent]
public abstract class Projectile : MonoBehaviour
{
    [Header("Object Params")]

    [SerializeField]
    [Tooltip("The sprite used by the projectile. Can be used for animations and instantiation.")]
    protected Sprite sprite;

    [SerializeField]
    [Tooltip("The prefab used by the projectile. Can be used for instatiation.")]
    protected GameObject prefab;

    [SerializeField]
    [Tooltip("The gameObject holding the SpriteRenderer for the projectile's gameObject. Can be used for animations.")]
    protected GameObject spriteObject;

    [SerializeField]
    [Tooltip("The projectile's direction.")]
    protected Vector2 direction = Vector2.up;

    [SerializeField] [Min(0)]
    [Tooltip("The speed at which the projectile travels.")]
    protected float speed;

    #region Properties
    public Sprite Sprite { get { return sprite; } }
    public GameObject Prefab { get { return prefab; } }
    public GameObject SpriteObject { get { return spriteObject; } }
    public Vector2 Direction { get { return direction; } set { direction = value.normalized; } }
    public float Speed { get { return speed; } }
    #endregion
}
