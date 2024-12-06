using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Halberd : Weapon
{
    protected override void Awake()
    {
        base.Awake();

        // Set halberd stats
        name = "Halberd";
        rarity = Rarity.Common;
    }

    /// <summary>
    /// Swing sword in front of player,
    /// </summary>
    public override void ActionNormal()
    {
        if (cooldownTracker_ActionNormal > 0) return;

        //ResetAction();

        // Set and start countdown
        cooldownTracker_ActionNormal = cooldown_ActionNormal;

        entityCollisionDetector.gameObject.SetActive(true);

        animator.Play("actionNormal_Halberd");

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

        if (!projectile)
            return;

        Projectile proj = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Projectile>();

        proj.Owner = GetComponentInParent<Entity>();
    }

    protected override void ResetAction()
    {
        StopAllCoroutines();
        cooldownTracker_ActionNormal = 0;
        cooldownTracker_ActionSpecial = 0;

        spriteObject.transform.rotation = Quaternion.Euler(defaultRotation);
    }
}
