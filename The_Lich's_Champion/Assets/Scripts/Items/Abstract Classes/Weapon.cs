using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic weapon class. Holds extra information for damaging opponents.
/// </summary>
public abstract class Weapon : Utility
{
    [SerializeField] [Min(0)] [Tooltip("Value for how much damage Normal Actions do.")]
    protected int damage;

    [SerializeField] [Min(0)] [Tooltip("Value for how much damage Special Actions do.")]
    protected int specialDamage;

    [SerializeField] [Min(0)] [Tooltip("Amount of time (in seconds) the collider and collision detection remain active and checking for damage.")]
    protected float timeDetectDamage;

    [SerializeField] [Min(0)] [Tooltip("Amount of time (in seconds) delayed between the player's input and the first frame damage detection is turned on.")]
    protected float windUpTime;

    [SerializeField] [Tooltip("The projectile object. Used to instantiate projectile objects.")]
    protected GameObject projectile;

    [SerializeField] [Tooltip("The collider object. Used to get information from the weapon's hitbox.")]
    protected EntityCollisionDetection entityCollisionDetector;

    protected override void Awake()
    {
        // Call base class Awake
        base.Awake();

        // Get and set hitbox object
        if (!entityCollisionDetector)
            entityCollisionDetector = transform.Find("HitboxObject").GetComponent<EntityCollisionDetection>();
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

    /// <summary>
    /// Started to detect damage over certain amount of seconds
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator DetectDamage()
    {
        float timeDetectDamageTracker = timeDetectDamage;

        while (timeDetectDamageTracker >= 0)
        {
            foreach (Entity e in entityCollisionDetector.EntityHit)
            {
                // If entity found (that isnt parent), damage entity
                if (e.gameObject != transform.parent.gameObject)
                    e.TakeDamage(damage, transform.position);
            }
            
            timeDetectDamageTracker -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }


    //for abilities
    protected virtual void DealDamageSpecial()
    {
        if (!entityCollisionDetector)
            throw new System.NullReferenceException("Entity Collision Detector not found!");

        // Ensure we are not running the routine twice
        // and run the new routine
        StopCoroutine(DetectDamageSpecial());
        StartCoroutine(DetectDamageSpecial());

        //entityCollisionDetector.gameObject.SetActive(false);
    }

    // for abilities that have crowd control
    protected virtual void CrowdControlSpecial()
    {
        if (!entityCollisionDetector)
            throw new System.NullReferenceException("Entity Collision Detector not found!");

        // Ensure we are not running the routine twice
        // and run the new routine
        StopCoroutine(DetectCCSpecial());
        StartCoroutine(DetectCCSpecial());

        //entityCollisionDetector.gameObject.SetActive(false);
    }

    protected virtual IEnumerator DetectDamageSpecial()
    {
        float timeDetectDamageTracker = timeDetectDamage;

        while (timeDetectDamageTracker >= 0)
        {
            foreach (Entity e in entityCollisionDetector.EntityHit)
            {
                // If entity found (that isnt parent), damage entity
                if (e.gameObject != transform.parent.gameObject)
                    e.TakeDamage(damage, transform.position);
            }

            timeDetectDamageTracker -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }
    protected virtual IEnumerator DetectCCSpecial()
    {
        float timeDetectDamageTracker = timeDetectDamage;

        while (timeDetectDamageTracker >= 0)
        {
            foreach (Entity e in entityCollisionDetector.EntityHit)
            {
                // If entity found (that isnt parent), damage entity
                if (e.gameObject != transform.parent.gameObject)
                    e.CrowdControlEntity(1.5f);
            }

            timeDetectDamageTracker -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }
}
