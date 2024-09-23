using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Item, IUtility
{
    [SerializeField] [Min(0)] protected int damage;
    [SerializeField] [Min(0)] protected float cooldown_ActionNormal;
    [SerializeField] [Min(0)] protected float cooldown_ActionSpecial;

    public abstract void ActionNormal();
    public abstract void ActionSpecial();
}
