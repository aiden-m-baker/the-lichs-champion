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

    /// <summary>
    /// Damage logic for targeted entity. Damage handling is up to the weapon.
    /// </summary>
    /// <param name="e">Entity targeted by the weapon</param>
    protected virtual void DealDamage(Entity e) 
    {
        if (!entityCollisionDetector)
            throw new System.NullReferenceException();

        entityCollisionDetector.gameObject.SetActive(false);

        e.TakeDamage(damage);
    }
}
