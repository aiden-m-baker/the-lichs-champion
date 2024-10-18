using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic utility class. Utility is something that can be held and used by the player, such as weapons, shields, magic items, etc.
/// </summary>
public abstract class Utility : Item
{
    [Header("Position Default Params")]
    [SerializeField] protected Vector3 defaultRotation = Vector3.zero;
    [SerializeField][Min(0)] protected float defaultScale = 1;
    [Header("Interaction Params")]
    [SerializeField][Min(0)] protected float cooldown_ActionNormal;
    [SerializeField][Min(0)] protected float cooldown_ActionSpecial;
    protected float cooldownTracker_ActionNormal;
    protected float cooldownTracker_ActionSpecial;

    protected Animator animator;

    // Properties disabled rn cause im lazy and they're unneeded so far
    #region Properites
    //public abstract float Cooldown_ActionNormal { get; }
    //public abstract float Cooldown_ActionSpecial { get; }
    //public abstract float CooldownTracker_ActionNormal { get; }
    //public abstract float CooldownTracker_ActionSpecial { get; }
    #endregion

    protected virtual void Awake()
    {
        // Get and set prefab object
        if (!prefab)
            prefab = gameObject;

        // Get and set sprite object
        if (!spriteObject)
            spriteObject = transform.Find("SpriteObject").gameObject;

        // Setup sprite
        spriteObject.GetComponent<SpriteRenderer>().sprite = sprite;
        spriteObject.transform.localScale = new Vector3(defaultScale, defaultScale, defaultScale);
        animator = spriteObject.GetComponent<Animator>();

        // Resets cooldowns
        ResetAction();
    }

    /// <summary>
    /// Function called when player inputs normal action
    /// </summary>
    public abstract void ActionNormal();

    /// <summary>
    /// Function called when player inputs specialized action. Can do nothing if specific item does not have a special action
    /// </summary>
    public abstract void ActionSpecial();

    /// <summary>
    /// Default LateUpdate tracks ability cooldowns. Use base.LateUpdate(); at the start of each overridden LateUpdate method
    /// </summary>
    protected virtual void LateUpdate()
    {
        // If cooldowns are less than 0, reset tracker to 0. Else, count down timer
        cooldownTracker_ActionNormal -= Time.deltaTime;
        cooldownTracker_ActionSpecial -= Time.deltaTime;

        if (cooldownTracker_ActionNormal < 0)
            cooldownTracker_ActionNormal = 0;

        if (cooldownTracker_ActionSpecial < 0)
            cooldownTracker_ActionSpecial = 0;
    }

    /// <summary>
    /// Reset logic when needed. Can be used to reset cooldowns and animations.
    /// </summary>
    protected abstract void ResetAction();
}
