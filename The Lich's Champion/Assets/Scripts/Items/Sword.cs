using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    [SerializeField] private Vector3 defaultRotation;

    private void Awake()
    {
        // Set sword stats
        name = "Sword";
        rarity = Rarity.Common;

        // Get and set necessary objects
        if (!spriteObject)
            spriteObject = transform.Find("SpriteObject").gameObject;
        if (!hitbox)
            hitbox = transform.Find("HitboxObject").GetComponent<Collider2D>();

        // Setup sprite
        spriteObject.GetComponent<SpriteRenderer>().sprite = sprite;

        // Setup hitbox
        hitbox.gameObject.SetActive(false);

        // Resets cooldowns
        ResetAction();
    }

    private void Update()
    {
        //print(cooldownTracker_ActionNormal);
        if (Input.GetKey(KeyCode.Mouse0))
            ActionNormal();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
    }

    /// <summary>
    /// Swing sword in front of player,
    /// </summary>
    protected override void ActionNormal()
    {
        if (cooldownTracker_ActionNormal > 0) return;

        ResetAction();

        // Set and start countdown
        cooldownTracker_ActionNormal = cooldown_ActionNormal;

        hitbox.gameObject.SetActive(true);

        StartCoroutine(SwingSwordSprite());
    }

    /// <summary>
    /// Grab, stab, and push back enemy player
    /// </summary>
    protected override void ActionSpecial()
    {
        if (cooldownTracker_ActionSpecial > 0) return;

        // Set and start countdown
        cooldownTracker_ActionSpecial = cooldown_ActionSpecial;
    }

    protected override void DealDamage(Entity e)
    {
        throw new System.NotImplementedException();
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
    private IEnumerator SwingSwordSprite()
    {
        Vector3 rot = Vector3.zero;
        rot.z = -45;
        spriteObject.transform.rotation = Quaternion.Euler(rot);

        yield return new WaitForSeconds(0.05f);

        while (rot.z < 160)
        {
            rot.z += 1200 * Time.deltaTime;
            spriteObject.transform.rotation = Quaternion.Euler(rot);
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }


}
