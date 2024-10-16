using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MultiMovement : MonoBehaviour
{
    public float speed = 5;
    private Vector2 movementInput;

    private Vector2 aimInput;
    private Vector2 previousInput;

    // dash
    private bool dash;
    private float dashCdMax = 5f;
    private float dashCdTimer = 0f;

    [SerializeField]
    private Vector3 position, velocity, direction, acceleration;
    private float maxSpeed = 4;
    private float frictionCoeff = 10f;

    private bool frictionApplied = false;

    // Update is called once per frame
    void Update()
    {
        // TODO: Add a deadzone to the movement input, so letting go of the joystick doesnt flick you
        transform.position += (new Vector3(movementInput.x, movementInput.y, 0) * Time.deltaTime * speed);

        // Rotate the player to face the direction of the right joystick
        if (aimInput.magnitude == 0)
        {
            aimInput = previousInput;
        }

        // apply acceleration to velocity
        // clamp velocity to max speed ( unless you're dashing )
        // apply velocity to position

        // Rotation
        // TODO: Add a deadzone to the aim input, letting go of the joystick should not flick you
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, aimInput.normalized);
        transform.rotation = rotation;

        // save previous input
        previousInput = aimInput;
    }

    public void OnMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();

    public void OnAim(InputAction.CallbackContext ctx) => aimInput = ctx.ReadValue<Vector2>();
    public void OnDash(InputAction.CallbackContext ctx) => dash = ctx.ReadValueAsButton();
}
