using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    private void Awake()
    {
        cooldown_ActionNormal = 0.25f;
        cooldown_ActionSpecial = 0.75f;
    }

    public override void ActionNormal()
    {

    }

    public override void ActionSpecial()
    {

    }
}
