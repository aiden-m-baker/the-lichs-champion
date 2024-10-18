using System;
using UnityEngine;
using UnityEngine.InputSystem;

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


    //inputs
    private InputAction pickUpAction;

    // camera

    [SerializeField]
    private Camera mainCam;

    // dash
    public float dashCd = 5f;
    public float dashCdTimer = 0f;
    public float dashDuration = 0.5f;
    public float dashDurationTimer = 0f;
    public bool isDashing = false;

    // player IFrames
    public float iFrameDuration = 0.5f;
    public float iFrameTimer = 0f;

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

    public bool IsDead
    {
        get
        {
            return health <= 0;
        }
    }

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

        // count IFrame cooldown
        if (iFrameTimer > 0)
        {
            iFrameTimer -= Time.deltaTime;
        }

        // count dash cooldown
        if (dashCdTimer > 0)
        {
            dashCdTimer -= Time.deltaTime;
        }

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

        //// rotate the player to face the direction they are moving
        //Vector3 worldPos = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        //worldPos -= transform.position;
        //Quaternion rotation = Quaternion.LookRotation(Vector3.forward, worldPos.normalized);
        //transform.rotation = rotation;
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
    public override void TakeDamage(int damage, Vector3 sourceLoc)
    {
        // TODO: add knockback to the player thats not instant
        // TODO: if hit multiple times in a row player will be unstoppable (maybe)
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
    public override void ApplyStatusEffect(StatusEffect effect)
    {
        throw new NotImplementedException();
    }

    public void Dash(InputAction.CallbackContext context)
    {
        // canceled == released
        // performed == pressed
        // started == pressed
        if (context.performed && dashCdTimer <= 0)
        {
            Debug.Log("Dash!!");
            dashCdTimer = dashCd;
        }
    }
    public void SwingWeaponNormal()
    {
        if (weapon != null)
            weapon.ActionNormal();
    }


    //determines pick up weapons
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "ItemObject" && Input.GetKeyDown(KeyCode.Space))
        {
            print("BUS");
            ItemObject weapon = collision.GetComponent<ItemObject>();
            if(weapon.Item is Weapon)
            {
                this.weapon = Instantiate(weapon.Item.Prefab, transform).GetComponent<Utility>();
            }
            
        }
    }
}
