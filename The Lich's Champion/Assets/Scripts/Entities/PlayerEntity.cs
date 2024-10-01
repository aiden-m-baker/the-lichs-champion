using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerEntity : Entity
{
    // all entity stats
    private int health;
    private int maxHealth = 100;
    private int damage;
    private float speed = 5;
    private float healthRegen;
    private int energy;
    private int maxEnergy;
    private float energyRegen;

    // additional fields

    // movement

    [SerializeField]
    private Vector3 position;
    [SerializeField]
    private Vector3 velocity;
    [SerializeField]
    private Vector3 direction;
    [SerializeField]
    private Vector3 acceleration;
    [SerializeField]
    private float mass = 1;
    [SerializeField]
    private float maxSpeed = 4;
    [SerializeField]
    private float frictionCoeff = 10f;

    public bool frictionApplied = false;

    // weapons

    [SerializeField]
    Utility weapon;

    // dash

    public float dashCd = 5f;
    public float dashCdTimer = 0f;
    public float dashDuration = 0.5f;
    public float dashDurationTimer = 0f;
    public bool isDashing = false;

    // player exclusive stats

    // levels?
    // experience?
    // gold?

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

    public override int Energy 
    {
        get { return energy; }
        set { energy = value; }
    }

    public override int MaxEnergy 
    {
        get { return maxEnergy; }
        set { maxEnergy = value; }
    }

    public override float EnergyRegen 
    {
        get { return energyRegen; }
        set { energyRegen = value; }
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        // temp health value
        health = maxHealth;
        //weapon.ActionNormal();
    }

    // Update is called once per frame
    void Update()
    {
        //if (frictionApplied)
        //{
        //    frictionApplied = false;
        //}
        // apply friction when no keys are pressed
        // and not while dashing
        // TODO: remake this for controller
        //if ((!Input.GetKey(KeyCode.A) && 
        //    !Input.GetKey(KeyCode.D) && 
        //    !Input.GetKey(KeyCode.W) && 
        //    !Input.GetKey(KeyCode.S)) &&
        //    !isDashing)
        //{
        //    //acceleration = Vector3.zero;
        //    // friction
        //    Vector3 friction = velocity * -1;
        //    friction.Normalize();
        //    friction = friction * frictionCoeff;
        //    acceleration += friction / mass;
        //    frictionApplied = true;
        //}


        //// count dash cooldown
        //if (dashCdTimer > 0)
        //{
        //    dashCdTimer -= Time.deltaTime;
        //}

        //// if you are dashing, lock the player's movement
        //// if the dash duration is over, stop dashing
        //if (isDashing)
        //{
        //    dashDurationTimer -= Time.deltaTime;
        //    if (dashDurationTimer <= 0)
        //        isDashing = false;
        //}

        // movement
        // SimpleMovement();

        // weapon input
        // SimpleWeaponUse();

        //TakeDamage();

        //velocity += acceleration * Time.deltaTime;

        // clamp the velocity to the max speed
        //if (isDashing)
        //    velocity = Vector3.ClampMagnitude(velocity, maxSpeed * 2);
        //else
        //    velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        //position += velocity * Time.deltaTime;
        //direction = velocity.normalized;

        //transform.position = position;
        //acceleration = Vector3.zero;

        // rotate the player to face the direction they are moving
        //if (velocity != Vector3.zero)
        //{
        //    Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
        //    transform.rotation = rotation;
        //}
    }

    public void SimpleMovement()
    {
        if (Input.GetKey(KeyCode.D) && !isDashing)
        {
            acceleration += Vector3.right * speed * Time.deltaTime * 10000;
        }
        if (Input.GetKey(KeyCode.A) && !isDashing)
        {
            acceleration += Vector3.left * speed * Time.deltaTime * 10000;
        }
        if (Input.GetKey(KeyCode.W) && !isDashing)
        {
            acceleration += Vector3.up * speed * Time.deltaTime * 10000;
        }
        if (Input.GetKey(KeyCode.S) && !isDashing)
        {
            acceleration += Vector3.down * speed * Time.deltaTime * 10000;
        }
        if (Input.GetKey(KeyCode.Space) && dashCdTimer <= 0)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            Debug.Log(mousePos);
            velocity = Vector3.zero;
            acceleration = Vector3.zero;
            acceleration += (mousePos - position).normalized * speed * Time.deltaTime * 1000000;

            // reset timers
            dashCdTimer = dashCd;
            dashDurationTimer = dashDuration;

            isDashing = true;
        }
    }
    public void SimpleWeaponUse()
    {
        // weapon usage
        if (Input.GetKey(KeyCode.Mouse0) && weapon != null) 
            weapon.ActionNormal();
    }
    public override void TakeDamage(int damage)
    {
        health -= damage;
    }
    public override void ApplyStatusEffect(StatusEffect effect)
    {
        throw new NotImplementedException();
    }
}
