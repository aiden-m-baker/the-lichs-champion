using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MultiMovement : MonoBehaviour
{
    public float speed = 5;
    private Vector2 movementInput;

    // Update is called once per frame
    void Update()
    {
        transform.position += (new Vector3(movementInput.x, movementInput.y, 0) * Time.deltaTime * speed);
    }

    public void OnMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();
}
