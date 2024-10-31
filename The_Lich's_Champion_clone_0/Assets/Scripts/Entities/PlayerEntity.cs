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
    bool isPickingUp = false;


    //inputs
    public PlayerInput playerInput;
    private InputAction pickUp;

    // camera

    [SerializeField]
    private Camera mainCam;
    Vector2 screenWorldPos;
    float boundsOffset = 0.2f;

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

    private void Awake()
    {
        if (!mainCam)
            mainCam = Camera.main;
        playerInput = GetComponent<PlayerInput>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(!mainCam)
            mainCam = Camera.main;

        position = transform.position;
        // temp health value
        health = maxHealth;
        //weapon.ActionNormal();
        screenWorldPos = new Vector2((float)Screen.width / Screen.height * mainCam.orthographicSize, mainCam.orthographicSize);
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Move this to rigidbody or something
        #region Bounds
        if (transform.position.x > screenWorldPos.x - boundsOffset)
            transform.position = new Vector3(screenWorldPos.x - boundsOffset, transform.position.y, 0);
        else if (transform.position.x < -screenWorldPos.x + boundsOffset)
            transform.position = new Vector3(-screenWorldPos.x + boundsOffset, transform.position.y, 0);
        if (transform.position.y > screenWorldPos.y - boundsOffset)
            transform.position = new Vector3(transform.position.x, screenWorldPos.y - boundsOffset, 0);
        else if (transform.position.y < -screenWorldPos.y + boundsOffset)
            transform.position = new Vector3(transform.position.x, -screenWorldPos.y + boundsOffset, 0);
        #endregion

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

    public void OnPickup(InputAction.CallbackContext context)
    {
        isPickingUp = context.started || context.performed;
    }

    public void SwingWeaponNormal()
    {
        if (weapon != null)
            weapon.ActionNormal();
    }

    public void SwingWeaponSpecial()
    {
        if (weapon != null)
            weapon.ActionSpecial();
    }

    //determines pick up weapons
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!(collision.tag == "ItemObject" && isPickingUp))
            return;
        
        ItemObject weapon = collision.GetComponent<ItemObject>();

        if (!(weapon.Item is Weapon))
            return;

        if (this.weapon)
        {
            if (this.weapon.GetType() == weapon.Item.GetType())
                return;
            Destroy(this.weapon.gameObject);
        }

        this.weapon = Instantiate(weapon.Item.Prefab, transform).GetComponent<Utility>();
    }
}
