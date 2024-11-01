using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rapier : Weapon
{
    [SerializeField]
    MultiMovement movement;

    protected override void Awake()
    {
        base.Awake();

        // Set sword stats
        name = "Rapier";
        rarity = Rarity.Common;

        // set movement
        movement = transform.parent.GetComponent<MultiMovement>();
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

        //MultiMovement movement = transform.parent.GetComponent<MultiMovement>();


        animator.Play("actionNormal_Rapier");

        //movement.OnNormDash(1.0f, 20.0f);

        movement.OnAbilityDash(1.0f, 25.0f);

        Invoke("DealDamage", windUpTime);
    }

    /// <summary>
    /// Grab, stab, and push back enemy player
    /// </summary>
    public override void ActionSpecial()
    {
        if (cooldownTracker_ActionSpecial > 0) return;

        // Set and start countdown
        cooldownTracker_ActionSpecial = cooldown_ActionSpecial;
    }

    protected override void ResetAction()
    {
        StopAllCoroutines();
        cooldownTracker_ActionNormal = 0;
        cooldownTracker_ActionSpecial = 0;

        spriteObject.transform.rotation = Quaternion.Euler(defaultRotation);
    }
}
