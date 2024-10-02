using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private PlayerInput _playerInput;
    private InputAction _moveAction;

    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private int controller;

    public void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions["Move"];
    }

    public void Update()
    {
        var input = _moveAction.ReadValue<Vector2>();

        transform.position += new Vector3(input.x, input.y, 0) * (movementSpeed * Time.deltaTime);
    }
}