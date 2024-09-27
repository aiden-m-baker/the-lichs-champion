using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    public void AddMovement(Vector3 direction)
    {
        transform.Translate(direction.normalized * (moveSpeed * Time.deltaTime));
    }
}