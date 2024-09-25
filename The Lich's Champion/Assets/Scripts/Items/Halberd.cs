using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Halberd : Weapon
{
    [SerializeField] private Vector3 defaultRotation = Vector3.zero;
    [SerializeField] [Min(0)] private float defaultScale = 1;

    private void Awake()
    {
        // Set halberd stats
        name = "Halberd";
        rarity = Rarity.Common;

        // Get and set necessary objects
        if (!spriteObject)
            spriteObject = transform.Find("SpriteObject").gameObject;
        if (!entityCollisionDetector)
            entityCollisionDetector = transform.Find("HitboxObject").GetComponent<EntityCollisionDetection>();

        // Setup sprite
        spriteObject.GetComponent<SpriteRenderer>().sprite = sprite;
        spriteObject.transform.localScale = new Vector3(defaultScale, defaultScale, defaultScale);

        // Setup hitbox
        entityCollisionDetector.gameObject.SetActive(false);

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

        entityCollisionDetector.gameObject.SetActive(true);

        StartCoroutine(SwingSwordSprite());

        DealDamage(entityInRange);
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
        entityCollisionDetector.gameObject.SetActive(false);
        //throw new System.NotImplementedException();
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

        while (rot.z < 95)
        {
            rot.z += 500 * Time.deltaTime;
            spriteObject.transform.rotation = Quaternion.Euler(rot);
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }


}
