using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Utility : Item
{
    [SerializeField][Min(0)] protected float cooldown_ActionNormal;
    [SerializeField][Min(0)] protected float cooldown_ActionSpecial;
    protected float cooldownTracker_ActionNormal;
    protected float cooldownTracker_ActionSpecial;

    /// <summary>
    /// Function called when player inputs normal action
    /// </summary>
    protected abstract void ActionNormal();

    /// <summary>
    /// Function called when player inputs specialized action. Can do nothing if specific item does not have a special action
    /// </summary>
    protected abstract void ActionSpecial();

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
