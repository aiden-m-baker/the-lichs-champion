using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Utility : Item
{
    [SerializeField][Min(0)] protected float cooldown_ActionNormal;
    [SerializeField][Min(0)] protected float cooldown_ActionSpecial;

    /// <summary>
    /// Function called when player inputs normal action
    /// </summary>
    public abstract void ActionNormal();

    /// <summary>
    /// Function called when player inputs specialized action. Can do nothing if specific item does not have a special action
    /// </summary>
    public abstract void ActionSpecial();
}
