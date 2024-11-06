using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EntityCollisionDetection : MonoBehaviour
{
    private List<GameObject> entityHit = new List<GameObject>();

    public List<Entity> EntityHit
    {
        get 
        {
            List<Entity> eList = new List<Entity>();

            // Attempt to get the entity script
            // Return null otherwise
            foreach (GameObject g in entityHit)
            {
                if (g.TryGetComponent(out Entity e))
                {
                    eList.Add(e);
                }
            }

            return eList;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!entityHit.Contains(collision.gameObject))
            entityHit.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        entityHit.Remove(collision.gameObject);
    }
}
