using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private Rigidbody _rb;

    [SerializeField] private float moveSpeed = 1f;

    public void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions["Move"];
        
        _rb = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        var input = _moveAction.ReadValue<Vector2>();
        
        var movement = new Vector2(input.x, input.y);
        
        _rb.AddForce(movement * (moveSpeed * Time.deltaTime));
    }
}