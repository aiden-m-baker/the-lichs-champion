using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class AIEntity : Entity
{
    // all entity stats
    private int health;
    private int maxHealth;
    private int damage;
    private float speed;
    private float healthRegen;
    private int energy;
    private int maxEnergy;
    private float energyRegen;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
