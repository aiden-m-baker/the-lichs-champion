using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MultiMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float mass = 1;
    private Vector2 movementInput;

    private Vector2 aimInput;
    private Vector2 previousAimInput;

    // dash contains whether or not the button is pressed
    // dashing contains whether or not you are currently dashing
    private bool dash, dashing;
    // dashCdMax is the total dash cooldown
    // dashCdTimer is a helper variable to count said cooldown
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
        // old non-physics input
        // transform.position += (new Vector3(movementInput.x, movementInput.y, 0) * Time.deltaTime * speed);



        // apply acceleration to velocity
        velocity += acceleration * Time.deltaTime;
        // clamp velocity to max speed ( unless you're dashing )
        if (dashing)
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed * 2);
        else if (!dashing) {
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        }
        // apply velocity to position
        position += velocity * Time.deltaTime;
        // TODO: Add a deadzone to the aim input, letting go of the joystick should not flick you


        // Rotate the player to face the direction of the right joystick
        // if there is currently no input, apply previously saved input
        // as current input
        if (aimInput.magnitude == 0)
        {
            aimInput = previousAimInput;
        }

        // look towards your aim stick orientation (or previous orientation)
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, aimInput.normalized);
        transform.rotation = rotation;

        // save previous input
        previousAimInput = aimInput;
    }

    public void OnMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();

    public void OnAim(InputAction.CallbackContext ctx) => aimInput = ctx.ReadValue<Vector2>();
    public void OnDash(InputAction.CallbackContext ctx) => dash = ctx.ReadValueAsButton();
    public void ApplyForce(Vector3 direction)
    {
        // if OnMove is already normalized, then pass in the original vector2. 
        // this gives the player more control on the movement. not available for kb+m
        acceleration += (direction * speed * Time.deltaTime) / mass;
    }
}
