using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public enum StatusEffect
{
    Stunned
}
public abstract class Entity : MonoBehaviour
{
    #region all entity stats properties

    /// <summary>
    /// Property for the entity's health
    /// </summary>
    public abstract int Health { get; set; }

    /// <summary>
    /// Property for the entity's max health
    /// </summary>
    public abstract int MaxHealth { get; set; }

    /// <summary>
    /// Property for the entity's damage
    /// </summary>
    public abstract int Damage { get; set; }

    // currently unused
    /// <summary>
    /// Property for the entity's Cooldown Reduction and Attack Speed
    /// </summary>
    // public abstract float CDR { get; set; }

    // currently unused
    /// <summary>
    /// Property for the entity's armor
    /// </summary>
    // public abstract int Armor { get; set; }

    /// <summary>
    /// Property for the entity's movement speed
    /// </summary>
    public abstract float Speed { get; set; }

    /// <summary>
    /// Property for the entity's health regen
    /// </summary>
    public abstract float HealthRegen { get; set; }

    // currently unused
    /// <summary>
    /// Property for the entity's Vamp stat (heal based on damage)
    /// </summary>
    // public abstract float Vamp { get; set; }
    
    /// <summary>
    /// Property for the entity's energy
    /// </summary>
    public abstract int Energy { get; set; }

    /// <summary>
    /// Property for the entity's max energy
    /// </summary>
    public abstract int MaxEnergy { get; set; }

    /// <summary>
    /// Property for the entity's energy regeneration
    /// </summary>
    public abstract float EnergyRegen { get; set; }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Take damage function for the entity
    /// </summary>
    /// <param name="damage">how much will be deducted from player health</param>
    public abstract void TakeDamage(int damage, Vector3 sourceLoc);

    /// <summary>
    /// Apply a status effect to the entity
    /// </summary>
    /// <param name="effect">What kind of effect is being applied?</param>
    public abstract void ApplyStatusEffect(StatusEffect effect);
}
