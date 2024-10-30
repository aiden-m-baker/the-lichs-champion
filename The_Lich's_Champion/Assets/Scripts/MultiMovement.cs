using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;
using Unity.VisualScripting;

public class MultiMovement : NetworkBehaviour
{
    // current movement input
    [SerializeField]
    private PlayerInput playerInput;
    // camera
    [SerializeField]
    private Camera mainCam;
    
    private Rigidbody2D _rb;

    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float mass = 1;
    [SerializeField]
    private Vector2 movementInput;

    private Vector2 aimInput;
    private Vector2 aimInputMouse;
    private Vector2 previousAimInput;
    
    private float moveSpeed = 40f;

    // dash contains whether or not the button is pressed
    // dashing contains whether or not you are currently dashing
    // dashmax is the time spent dashing
    // dashtimer is a helper variable to count said time
    [SerializeField]
    private bool dashing;
    [SerializeField]
    private float dashPressed;

    private float dashSpeed = 35f;

    [SerializeField]
    private float dashTimer = 0f;
    [SerializeField]
    private float dashMax = 0.15f;
    // dashCdMax is the total dash cooldown
    // dashCdTimer is a helper variable to count said cooldown
    [SerializeField]
    private float dashCdMax = 5f;
    [SerializeField]
    private float dashCdTimer = 0f;

    // hold previous dash location
    Vector2 dashLocation;

    [SerializeField]
    private Vector3 position, velocity, direction, acceleration;
    private float maxSpeed = 4;
    private float frictionCoeff = 50f;

    [SerializeField]
    private bool frictionApplied = false;

    // properties
    public string CurrentControlScheme
    {
        get { return playerInput.currentControlScheme; }
    }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        _rb = GetComponent<Rigidbody2D>();
        if (!mainCam)
            mainCam = Camera.main;
    }

    //private void Start()
    //{
    //}

    //public override void OnNetworkSpawn()
    //{
    //}

    //private void Initialize()
    //{
    //}

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!Application.isFocused) return;
        //Debug.Log(moveSpeed);
        //Debug.Log(_rb.velocity);
        //Debug.Log(dashPressed);
        //Debug.Log(playerInput.currentControlScheme);
        // TODO: Add a deadzone to the movement input, so letting go of the joystick doesnt flick you
        // old non-physics input
        //transform.position += (Vector3)movementInput * Time.deltaTime * speed;

        // restore previous rotation if no input
        if (aimInput.magnitude == 0 && CurrentControlScheme != "MouseKeyboard")
        {
            aimInput = previousAimInput;
        }

        // if not dashing, allow movement
        if (!dashing)
            _rb.AddForce(movementInput * moveSpeed);

        // if dashing, count down the timer
        if (dashing)
        {
            if (aimInput.magnitude != 0)
            {
                //_rb.AddForce(dashLocation * dashSpeed);
            }
            else
            {
                //_rb.AddForce((dashLocation - (Vector2)transform.position).normalized * dashSpeed);
            }
        }
        // if not dashing, set dashing to false
        if (dashTimer <= 0)
        {
            dashing = false;
        }

        // when key is pressed, and you are not currently dashing, and the cd is done
        if (dashPressed == 1 && !dashing && dashCdTimer <= 0)
        {
            Debug.Log("DASH BUT ACTUALLY");
            dashing = true;
            // reset timers
            dashCdTimer = dashCdMax;
            dashTimer = dashMax;
            _rb.velocity = Vector3.zero;

            if (aimInput.magnitude != 0)
            {
                dashLocation = aimInput.normalized;
                _rb.AddForce(aimInput.normalized * dashSpeed, ForceMode2D.Impulse);
            }
            else
            {
                dashLocation = aimInputMouse.normalized;
                _rb.AddForce((aimInputMouse.normalized - (Vector2)transform.position).normalized * dashSpeed, ForceMode2D.Impulse);
            }
        }


        // count cooldowns
        UpdateTimers();

        // TODO: Fix Mouse Player Rotation
        //// look towards your aim stick orientation (or previous orientation)
        //if (CurrentControlScheme == "MouseKeyboard")
        //{
        //    aimInput = mainCam.ScreenToWorldPoint(aimInput) - transform.position;
        //}
        
        if (aimInput.magnitude != 0)
        {
            // look towards your aim stick orientation (or previous orientation)
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, aimInput.normalized);
            transform.rotation = rotation;
        }
        else
        {
            // look towards your aim stick orientation (or previous orientation)
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, aimInputMouse - (Vector2)transform.position);
            transform.rotation = rotation;
        }

        // save previous input
        if (aimInput.magnitude > 0)
            previousAimInput = aimInput;
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
    }

    public void OnAim(InputAction.CallbackContext ctx) => aimInput = ctx.ReadValue<Vector2>();
    public void OnAimMouse(InputAction.CallbackContext ctx) => aimInputMouse = mainCam.ScreenToWorldPoint(ctx.ReadValue<Vector2>());
    public void OnDash(InputAction.CallbackContext ctx) => dashPressed = ctx.ReadValue<float>();
    public void OnAbilityDash(float isPressed) => dashPressed = isPressed != 0.0f ? 1.0f : 0.0f;
    public void ApplyForce(Vector3 direction)
    {
        // if OnMove is already normalized, then pass in the original vector2. 
        // this gives the player more control on the movement. not available for kb+m
        acceleration += (direction * speed * Time.deltaTime) / mass;
    }

    private void UpdateTimers()
    {
        if (dashTimer >= 0)
            dashTimer -= Time.fixedDeltaTime;
        if (dashCdTimer >= 0)
            dashCdTimer -= Time.fixedDeltaTime;
    }
}


// old physics code
//if (frictionApplied)
//    frictionApplied = false;
//// apply friction when no keys are pressed
//if (movementInput.magnitude == 0 && !dashing) 
//{
//    // zero out acceleration
//    acceleration = Vector3.zero;
//    // apply friction
//    Vector3 friction = velocity * -1;
//    friction.Normalize();
//    friction *= frictionCoeff;
//    ApplyForce(friction);
//    frictionApplied = true;
//}


//// apply force on direction from controller
//if (movementInput.magnitude > 0)
//{
//    ApplyForce(movementInput);
//}

//// apply acceleration to velocity
//velocity += acceleration * Time.deltaTime;
//// clamp velocity to max speed ( unless you're dashing )
//if (dashing)
//    velocity = Vector3.ClampMagnitude(velocity, maxSpeed * 2);
//else if (!dashing) {
//    velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
//}
//// apply velocity to position
//position += velocity * Time.deltaTime;
//// TODO: Add a deadzone to the aim input, letting go of the joystick should not flick you

//// apply position to transform
//transform.position = position;

//// Rotate the player to face the direction of the right joystick
//// if there is currently no input, apply previously saved input
//// as current input

//// look towards your aim stick orientation (or previous orientation)
//if (CurrentControlScheme == "MouseKeyboard")
//{
//    aimInput = mainCam.ScreenToWorldPoint(aimInput) - transform.position;
//}