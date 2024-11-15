using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{

    protected override void Awake()
    {
        base.Awake();

        // Set sword stats
        name = "Sword";
        rarity = Rarity.Common;
    }

    private void Update()
    {
        //print(cooldownTracker_ActionNormal);
        //if (Input.GetKey(KeyCode.Mouse0))
        //ActionNormal();

        print(cooldownTracker_ActionSpecial);
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

        animator.Play("actionNormal_Sword");

        Invoke("DealDamage", windUpTime);
    }

    /// <summary>
    /// Grab, stab, and push back enemy player
    /// </summary>
    public override void ActionSpecial()
    {

        if (cooldownTracker_ActionSpecial > 0) return;

        ResetSpecialCD();
        
        // Set and start countdown
        cooldownTracker_ActionNormal = cooldown_ActionNormal;

        entityCollisionDetector.gameObject.SetActive(true);

        MultiMovement movement = transform.parent.GetComponent<MultiMovement>();

        animator.Play("actionSpecial_Sword");

        movement.OnAbilityDash(1.0f, 15f);
        //print("pommel striked");

        Invoke("CrowdControlSpecial", windUpTime / 2);
    }

    protected override void ResetAction()
    {
        StopAllCoroutines();
        cooldownTracker_ActionNormal = 0;
        //cooldownTracker_ActionSpecial = 0;

        spriteObject.transform.rotation = Quaternion.Euler(defaultRotation);
    }

    private void ResetSpecialCD()
    {
        cooldownTracker_ActionSpecial = 5;
    }
}
