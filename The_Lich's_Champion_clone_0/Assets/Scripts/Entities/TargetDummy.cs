using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class TargetDummy : Entity
{
    // all entity stats
    private int health;
    private int maxHealth = 100;
    private int damage = 0;
    private float speed = 0;
    private float healthRegen;
    private int energy;
    private int maxEnergy;
    private float energyRegen;

    // player IFrames
    public float iFrameDuration = 0.5f;
    public float iFrameTimer = 0f;

    // properties
    #region player stat properties

    public override int Health
    {
        get { return health; }
        set { health = value; }
    }

    public override int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public override int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public override float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public override float HealthRegen
    {
        get { return healthRegen; }
        set { healthRegen = value; }
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        // knockback test code
        // TakeDamage(10, new Vector3(0, -1, 0));
    }

    // Update is called once per frame
    void Update()
    {
        // count IFrame cooldown
        if (iFrameTimer > 0)
        {
            iFrameTimer -= Time.deltaTime;
        }

    }
    public override void TakeDamage(int damage, Vector3 sourceLoc)
    {
        if (iFrameTimer <= 0)
        {
            health -= damage;
            iFrameTimer = iFrameDuration;

            if (sourceLoc != null)
            {
                // 0.1f == knockback constant
                transform.position += (transform.position - sourceLoc).normalized * 0.1f;
            }
        }
    }

    public override void CrowdControlEntity(float duration)
    {
        throw new NotImplementedException();
    }

    public override void ApplyStatusEffect(StatusEffect effect)
    {
        throw new NotImplementedException();
    }
}
