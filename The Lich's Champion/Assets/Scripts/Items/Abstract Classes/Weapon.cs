using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic weapon class. Holds information for damaging opponents.
/// </summary>
public abstract class Weapon : Utility
{
    [SerializeField] [Min(0)] protected int damage;
    [SerializeField] [Min(0)] protected int specialDamage;
    [SerializeField] protected EntityCollisionDetection entityCollisionDetector;

    protected abstract void DealDamage(Entity e);
}
