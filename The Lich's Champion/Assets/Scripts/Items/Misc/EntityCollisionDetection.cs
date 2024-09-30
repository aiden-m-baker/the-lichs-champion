using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityCollisionDetection : MonoBehaviour
{
    private GameObject entityHit = null;

    public GameObject EntityHit
    {
        get { return entityHit; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Entity") return;

        entityHit = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        entityHit = null;
    }
}
