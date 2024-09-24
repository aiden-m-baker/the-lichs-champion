using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Utility
{
    [SerializeField] [Min(0)] protected int damage;
    [SerializeField] [Min(0)] protected int specialDamage;
}
