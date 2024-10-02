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
    [SerializeField] [Min(0)] protected float timeDetectDamage;
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
    protected virtual void DealDamage() 
    {
        if (!entityCollisionDetector)
            throw new System.NullReferenceException("Entity Collision Detector not found!");

        // Ensure we are not running the routine twice
        // and run the new routine
        StopCoroutine(DetectDamage());
        StartCoroutine(DetectDamage());

        //entityCollisionDetector.gameObject.SetActive(false);
    }

    protected virtual IEnumerator DetectDamage()
    {
        Entity e;
        float timeDetectDamageTracker = timeDetectDamage;

        while (timeDetectDamageTracker >= 0)
        {
            // Get entity hit
            e = entityCollisionDetector.EntityHit;
            // If entity found (that isnt parent), damage entity
            if (e && e.gameObject != transform.parent.gameObject)
                e.TakeDamage(damage, transform.position);
            timeDetectDamageTracker -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }
}
