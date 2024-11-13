using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalberdProjectile : Projectile
{
    // Update is called once per frame
    protected override void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        timeToDestroyTracker -= Time.deltaTime;
        if (timeToDestroyTracker <= 0)
        {
            if(owner)
                owner.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 0);

            Destroy(gameObject);
        }
    }
}
