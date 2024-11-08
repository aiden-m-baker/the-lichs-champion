using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// Basic projectile class. Holds basic information 
/// </summary>
[DisallowMultipleComponent]
[RequireComponent(typeof(EntityCollisionDetection))]
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
    [Min(0)]
    [Tooltip("The default scale of the spriteObject.")]
    protected float defaultScale = 1;

    [SerializeField]
    [Tooltip("The collider object. Used to get information from the weapon's hitbox.")]
    protected EntityCollisionDetection entityCollisionDetector;

    [Header("Projectile Params")]

    [SerializeField]
    [Tooltip("The entity the projectile originates from. Can be used to alter owner Entity data")]
    protected Entity owner;

    [SerializeField]
    [Tooltip("The projectile's direction.")]
    protected Vector2 direction = Vector2.up;

    [SerializeField]
    [Min(0)]
    [Tooltip("The speed at which the projectile travels.")]
    protected float speed;

    #region Properties
    public Sprite Sprite { get { return sprite; } }
    public GameObject Prefab { get { return prefab; } }
    public GameObject SpriteObject { get { return spriteObject; } }
    public Entity Owner{ get { return owner; } }
    public Vector2 Direction { get { return direction; } set { direction = value.normalized; } }
    public float Speed { get { return speed; } }
    #endregion

    protected virtual void Awake()
    {
        // Get and set prefab object if none specified
        if (!prefab)
            prefab = gameObject;

        // Get and set sprite object if none specified
        if (!spriteObject)
            spriteObject = transform.Find("SpriteObject").gameObject;

        // Setup spriteObject for animations and visual feedback
        spriteObject.GetComponent<SpriteRenderer>().sprite = sprite;
        spriteObject.transform.localScale = new Vector3(defaultScale, defaultScale, defaultScale);

        entityCollisionDetector = gameObject.GetComponent<EntityCollisionDetection>();

        // Get and set animator object
        //animator = spriteObject.GetComponent<Animator>();
        
    }
}
