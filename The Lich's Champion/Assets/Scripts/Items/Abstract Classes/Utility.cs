using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic utility class. Utility is something that can be held and used by the player, such as weapons, shields, magic items, etc.
/// </summary>
public abstract class Utility : Item
{
    [SerializeField] protected GameObject prefab;
    [SerializeField] protected GameObject spriteObject;
    [SerializeField][Min(0)] protected float cooldown_ActionNormal;
    [SerializeField][Min(0)] protected float cooldown_ActionSpecial;
    protected float cooldownTracker_ActionNormal;
    protected float cooldownTracker_ActionSpecial;

    #region Properites
    //public abstract float Cooldown_ActionNormal { get; }
    //public abstract float Cooldown_ActionSpecial { get; }
    //public abstract float CooldownTracker_ActionNormal { get; }
    //public abstract float CooldownTracker_ActionSpecial { get; }
    #endregion



    /// <summary>
    /// Function called when player inputs normal action
    /// </summary>
    public abstract void ActionNormal();

    /// <summary>
    /// Function called when player inputs specialized action. Can do nothing if specific item does not have a special action
    /// </summary>
    public abstract void ActionSpecial();

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

    protected abstract void ResetAction();
}
