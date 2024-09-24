using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerEntity : Entity
{
    // all entity stats
    private int health;
    private int maxHealth;
    private int damage;
    private float speed = 100;
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
    private float maxSpeed = 10;
    [SerializeField]
    private float frictionCoeff = 0.1f;

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
    }

    // Update is called once per frame
    void Update()
    {

        // friction
        Vector3 friction = velocity * -1;
        friction.Normalize();
        friction = friction * frictionCoeff;
        acceleration += friction / mass;


        SimpleMovement();

        velocity += acceleration * Time.deltaTime;

        // clamp the velocity to the max speed
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        position += velocity * Time.deltaTime;
        direction = velocity.normalized;

        transform.position = position;
        acceleration = Vector3.zero;

        // rotate the player to face the direction they are moving
        if (velocity != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = rotation;
        }
    }

    public void SimpleMovement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            acceleration += Vector3.right * speed * Time.deltaTime * 1000;
        }
        if (Input.GetKey(KeyCode.A))
        {
            acceleration += Vector3.left * speed * Time.deltaTime * 1000;
        }
        if (Input.GetKey(KeyCode.W))
        {
            acceleration += Vector3.up * speed * Time.deltaTime * 1000;
        }
        if (Input.GetKey(KeyCode.S))
        {
            acceleration += Vector3.down * speed * Time.deltaTime * 1000;
        }
    }
}
