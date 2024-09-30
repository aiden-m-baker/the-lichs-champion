using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityCollisionDetection : MonoBehaviour
{
    private Entity entityHit = null;

    public Entity EntityHit
    {
        get { return entityHit; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        entityHit = collision.GetComponent<Entity>();

        if (!entityHit) entityHit = null;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Entity>() == entityHit) entityHit = null;
    }
}
