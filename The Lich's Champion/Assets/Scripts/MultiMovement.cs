using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MultiMovement : MonoBehaviour
{
    public float speed = 5;
    private Vector2 movementInput;

    private Vector2 aimInput;
    private Vector3 rotationVector;

    // Update is called once per frame
    void Update()
    {
        transform.position += (new Vector3(movementInput.x, movementInput.y, 0) * Time.deltaTime * speed);

        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, aimInput.normalized);
        transform.rotation = rotation;
    }

    public void OnMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();

    public void OnAim(InputAction.CallbackContext ctx) => aimInput = ctx.ReadValue<Vector2>();
}
