using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalberdProjectile : Projectile
{
    protected override void Awake()
    {
        base.Awake();

        // Will implement once aim is a public property
        //Direction = owner.GetComponent<MultiMovement>().;
    }

    // Update is called once per frame
    protected override void Update()
    {
        // Rotate sprite
        spriteObject.transform.Rotate(Vector3.forward * 900 * Time.deltaTime, Space.Self);

        base.Update();
    }

    private void OnDestroy()
    {
        // Move player towards halberd position
        if (owner)
            owner.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}
