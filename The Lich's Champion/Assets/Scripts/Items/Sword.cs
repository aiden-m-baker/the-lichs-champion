using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    [SerializeField] private Vector3 defaultRotation;

    private void Awake()
    {
        name = "Sword";
        rarity = Rarity.Common;
        if (!spriteObject)
            spriteObject = transform.Find("SpriteObject").gameObject;
        if (!hitbox)
            hitbox = transform.Find("HitboxObject").GetComponent<Collider2D>();

        spriteObject.GetComponent<SpriteRenderer>().sprite = sprite;

        ResetAction();
    }

    /// <summary>
    /// Swing sword in front of player,
    /// </summary>
    public override void ActionNormal()
    {
        if (cooldownTracker_ActionNormal > 0) return;

        // Set and start countdown
        cooldownTracker_ActionNormal = cooldown_ActionNormal;

        print("Attacking Normal!");

        ResetAction();

        StartCoroutine(SwingSwordSprite());
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

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
            ActionNormal();
    }

    protected override void LateUpdate()
    {
        base.LateUpdate();
    }

    protected override void ResetAction()
    {
        base.ResetAction();

        spriteObject.transform.rotation = Quaternion.Euler(defaultRotation);
    }

    private IEnumerator SwingSwordSprite()
    {
        Vector3 rot = Vector3.zero;
        rot.z = 10;
        spriteObject.transform.rotation = Quaternion.Euler(rot);

        yield return null;
    }
}
