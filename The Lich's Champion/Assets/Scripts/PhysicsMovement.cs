using UnityEngine;

public class PhysicsMovement : MonoBehaviour
{
    private Vector3 _velocity;

    private Vector3 _acceleration;

    public float mass = 5f;

    [SerializeField] private float maxSpeed = 1;

    void Update()
    {
        _velocity += _acceleration * Time.deltaTime;

        _velocity = Vector3.ClampMagnitude(_velocity, maxSpeed);

        transform.position += _velocity * Time.deltaTime;

        _acceleration = Vector3.zero;
    }

    public void ApplyForce(Vector3 force)
    {
        _acceleration += force / mass;
    }
}