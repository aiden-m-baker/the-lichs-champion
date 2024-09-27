using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Vector3 _movement = Vector3.zero;

    void Update()
    {
        if (_movement.magnitude > 0)
        {
            transform.Translate(_movement.normalized * (moveSpeed * Time.deltaTime));
            _movement = Vector3.zero;
        }
    }

    void AddMovement(Vector3 movement)
    {
        _movement = movement;
    }
}