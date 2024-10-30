using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Netcode;

public class ItemObject : NetworkBehaviour
{
    [SerializeField] private Item item;

    public Item Item { get { return item; } }

    public ItemObject(Item i)
    {
        item = i;
    }

    private void Start()
    {
        ResetItemObject();  
    }

    public void ResetItemObject()
    {
        if (!item)
        {
            print("No item on this object! Instance ID: " + gameObject.GetInstanceID());
            Destroy(gameObject);
        }

        // Create gameobject to hold sprite object
        GameObject s = new GameObject();
        s.AddComponent<SpriteRenderer>().sprite = item.Sprite;

        // Reset positions and size to display accurately
        s.transform.SetPositionAndRotation(transform.position + Vector3.forward, transform.rotation);
        s.transform.SetParent(transform);
        s.transform.localScale = item.SpriteObject.transform.localScale;
        s.transform.position -= item.Sprite.bounds.center * s.transform.localScale.x;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
