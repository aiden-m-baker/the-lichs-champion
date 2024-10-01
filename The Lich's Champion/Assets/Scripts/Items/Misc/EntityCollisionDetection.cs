using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityCollisionDetection : MonoBehaviour
{
    private GameObject entityHit = null;

    public Entity EntityHit
    {
        get 
        {
            if(!entityHit)
                return null;

            // Attempt to get the entity script
            // Return null otherwise
            if(entityHit.TryGetComponent(out Entity e))
                return e;

            return null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        entityHit = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == entityHit) entityHit = null;
    }
}
