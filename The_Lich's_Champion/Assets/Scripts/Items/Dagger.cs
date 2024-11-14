using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dagger : Weapon
{

    protected override void Awake()
    {
        base.Awake();

        // Set dagger stats
        name = "Dagger";
        rarity = Rarity.Common;
    }

    private void Update()
    {
        //print(cooldownTracker_ActionNormal);
        //if (Input.GetKey(KeyCode.Mouse0))
            //ActionNormal();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
    }

    /// <summary>
    /// Swing sword in front of player,
    /// </summary>
    public override void ActionNormal()
    {
        if (cooldownTracker_ActionNormal > 0) return;

        ResetAction();

        // Set and start countdown
        cooldownTracker_ActionNormal = cooldown_ActionNormal;

        entityCollisionDetector.gameObject.SetActive(true);

        animator.Play("actionNormal_Dagger");

        Invoke("DealDamage", windUpTime);
    }

    /// <summary>
    /// Grab, stab, and push back enemy player
    /// </summary>
    public override void ActionSpecial()
    {
        if (cooldownTracker_ActionSpecial > 0) return;

        ResetAction();

        // Set and start countdown
        cooldownTracker_ActionSpecial = cooldown_ActionSpecial;

        DealDamageSpecial();
    }

    protected override IEnumerator DetectDamageSpecial()
    {
        float timeDetectDamageTracker = timeDetectDamage;
        MultiMovement movement = transform.parent.GetComponent<MultiMovement>();

        movement.OnAbilityDash(1.0f, 20.0f);

        //movement.OnDash(1);
        //yield return new WaitForEndOfFrame();

        //movement.OnDash(0);

        while (timeDetectDamageTracker >= 0)
        {
            foreach (Entity e in entityCollisionDetector.EntityHit)
            {
                // If entity found (that isnt parent), damage entity
                //if (e.gameObject != transform.parent.gameObject)

                if (e.gameObject != transform.parent.gameObject)
                {
                    e.TakeDamage(damage, transform.position);
                }

                cooldownTracker_ActionSpecial = 3.5f;
            }

            timeDetectDamageTracker -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        movement.OnAbilityDash(0.0f, 0.0f);

        yield return null;
    }

    protected override void ResetAction()
    {
        StopAllCoroutines();
        cooldownTracker_ActionNormal = 0;
        cooldownTracker_ActionSpecial = 0;

        spriteObject.transform.rotation = Quaternion.Euler(defaultRotation);
    }

    /// <summary>
    /// Temporary animation for demonstration purposes
    /// </summary>
    /// <returns></returns>
    //private IEnumerator SwingSwordSprite()
    //{
    //    Vector3 rot = Vector3.zero;
    //    rot.z = -30;
    //    spriteObject.transform.rotation = Quaternion.Euler(rot);

    //    yield return new WaitForSeconds(0.05f);

    //    while (rot.z < 95)
    //    {
    //        rot.z += 1200 * Time.deltaTime;
    //        spriteObject.transform.localRotation = Quaternion.Euler(rot);
    //        yield return new WaitForEndOfFrame();
    //    }

    //    yield return null;
    //}
}