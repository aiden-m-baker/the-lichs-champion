using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private Vector2 movementInput;

    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private int controller;

    public void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions["Move"];
    }

    public void Update()
    {
        transform.position += new Vector3(movementInput.x, movementInput.y, 0) * (movementSpeed * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();
}