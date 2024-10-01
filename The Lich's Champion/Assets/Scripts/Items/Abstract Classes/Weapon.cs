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

    protected override void Awake()
    {
        base.Awake();

        // Setup hitbox
        if (!entityCollisionDetector)
            entityCollisionDetector = transform.Find("HitboxObject").GetComponent<EntityCollisionDetection>();

        //entityCollisionDetector.gameObject.SetActive(false);
    }

    /// <summary>
    /// Damage logic for targeted entity. Damage handling is up to the weapon.
    /// </summary>
    /// <param name="e">Entity targeted by the weapon</param>
    protected virtual void DealDamage(Entity e) 
    {
        if (!entityCollisionDetector)
            throw new System.NullReferenceException("Entity Collision Detector not found!");

        if (!e)
            return;

        e.TakeDamage(damage, transform.position);

        //entityCollisionDetector.gameObject.SetActive(false);
    }
}
