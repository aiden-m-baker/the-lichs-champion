using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;
using Unity.VisualScripting;
public enum ControlScheme
{
    MouseKeyboard,
    Controller
}
public class MultiMovement : NetworkBehaviour
{
    // current movement input
    [SerializeField]
    private PlayerInput playerInput;
    // camera
    [SerializeField]
    private Camera mainCam;
    // sprite renderer
    [SerializeField]
    private SpriteRenderer spriteRenderer;


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
    [SerializeField]
    private float abilityPressed;

    private float dashSpeed = 30f;
    private float abilitySpeed = 0.0f;

    [SerializeField]
    private float dashTimer = 0f;
    [SerializeField]
    private float dashMax = 0.25f;
    // dashCdMax is the total dash cooldown
    // dashCdTimer is a helper variable to count said cooldown
    [SerializeField]
    private float dashCdMax = 3f;
    [SerializeField]
    private float dashCdTimer = 0f;

    // ability dash timers
    [SerializeField]
    private float abilityDashTimer = 0f;
    [SerializeField]
    private float abilityDashMax = 0.75f;

    // hold previous dash location
    Vector2 dashLocation;

    public bool disableMovement;

    // Crowd Control
    public bool knockedBack;
    public bool stunned;

    // properties
    public ControlScheme CurrentControlScheme
    {
        get
        {
            if (playerInput.currentControlScheme == "MouseKeyboard")
                return ControlScheme.MouseKeyboard;
            else if (playerInput.currentControlScheme == "Gamepad")
                return ControlScheme.Controller;
            else
            {
                Debug.Log("No Valid Control Scheme Detected! Destroying Player");
                throw new Exception("No Valid Control Scheme");
            }
        }
    }

    public bool KnockedBack
    {
        get { return knockedBack; }
        set { knockedBack = value; }
    }

    public bool Dashing
    {
        get { return dashing; }
        set { dashing = value; }
    }
    
    public float AbilityDashTimer
    {
        get { return abilityDashTimer; }
        set { abilityDashTimer = value; }
    }

    public bool Stunned
    {
        get { return stunned; }
        set { stunned = value; }
    }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        _rb = GetComponent<Rigidbody2D>();
        if (!mainCam)
            mainCam = Camera.main;
    }

    private void Start()
    {
        //Debug.Log(CurrentControlScheme);
    }

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

        Debug.Log(CurrentControlScheme);
        // restore previous rotation if no input (controller only)
        if (aimInput.magnitude == 0 && CurrentControlScheme != ControlScheme.MouseKeyboard)
        {
            aimInput = previousAimInput;
        }

        // if dashing, or knocked back, disable movement
        // ADD MORE CONDITIONS IF APPLICABLE
        if (dashing || knockedBack || stunned)
        {
            disableMovement = true;
            // darken player color when movement is disabled
            spriteRenderer.color = Color.gray;
        }
        else
        {
            disableMovement = false;
            // reset player color back to normal
            spriteRenderer.color = Color.white;
        }

        // movement
        // if disableMovement is false, allow movement
        if (!disableMovement)
        {
            _rb.AddForce(movementInput * moveSpeed);
            //Debug.Log("movement input called");
        }

        // player dash
        PlayerDash();
        PlayerAbilityDash();

        // count cooldowns
        UpdateTimers();
        
        if (CurrentControlScheme == ControlScheme.Controller)
        {
            // look towards your aim stick orientation (or previous orientation)
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, aimInput.normalized);
            transform.rotation = rotation;
        }
        else
        {
            // look towards your aim stick orientation (or previous orientation)
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, aimInput - (Vector2)transform.position);
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

    public void OnAim(InputAction.CallbackContext ctx)
    {
        if (CurrentControlScheme == ControlScheme.Controller)
         aimInput = ctx.ReadValue<Vector2>();
        else if (CurrentControlScheme == ControlScheme.MouseKeyboard)
         aimInput = mainCam.ScreenToWorldPoint(ctx.ReadValue<Vector2>());
    }
    public void OnAimMouse(InputAction.CallbackContext ctx) => aimInputMouse = mainCam.ScreenToWorldPoint(ctx.ReadValue<Vector2>());
    public void OnDash(InputAction.CallbackContext ctx) => dashPressed = ctx.ReadValue<float>();
    public void OnAbilityDash(float isPressed, float abilitySpeedInput)
    {
        abilityPressed = isPressed != 0.0f ? 1.0f : 0.0f;
        abilitySpeed = abilitySpeedInput;
    }
    
    public void ApplyForce(Vector3 direction)
    {
        // if OnMove is already normalized, then pass in the original vector2. 
        // this gives the player more control on the movement. not available for kb+m
        _rb.AddForce((direction * speed * Time.deltaTime) / mass);
    }

    public void Knock(Vector3 movement)
    {
        _rb.AddForce(movement, ForceMode2D.Impulse);
    }

    private void UpdateTimers()
    {
        if (dashTimer >= 0)
            dashTimer -= Time.fixedDeltaTime;
        if (dashCdTimer >= 0)
            dashCdTimer -= Time.fixedDeltaTime;
        if (abilityDashTimer >= 0)
            abilityDashTimer -= Time.fixedDeltaTime;
    }
    private void PlayerDash()
    {
        // if not dashing, set dashing to false (player lockout)
        // dash timer is to count player control lockout
        if (dashTimer <= 0 && abilityDashTimer <= 0)
        {
            dashing = false;
        }

        // when key is pressed, and you are not currently dashing, and the cd is done
        if (dashPressed == 1 && !dashing && dashCdTimer <= 0)
        {
            //Debug.Log("DASH BUT ACTUALLY");
            dashing = true;
            // reset timers
            dashCdTimer = dashCdMax;
            dashTimer = dashMax;
            // zero the velocity
            _rb.velocity = Vector3.zero;

            if (CurrentControlScheme == ControlScheme.Controller)
            {
                dashLocation = aimInput.normalized;
                _rb.AddForce(aimInput.normalized * dashSpeed, ForceMode2D.Impulse);
            }
            else
            {
                dashLocation = aimInput.normalized;
                _rb.AddForce((aimInput - (Vector2)transform.position).normalized * dashSpeed, ForceMode2D.Impulse);
            }
        }
    }

    private void PlayerAbilityDash()
    {
        if (abilityDashTimer <= 0 && dashTimer <= 0)
        {
            dashing = false;
        }
        // when key is pressed, and you are not currently dashing, and the cd is done
        if (abilityPressed == 1 && !dashing)
        {
            //Debug.Log("Ability Dash");
            dashing = true;
            // reset timers
            abilityDashTimer = abilityDashMax;
            _rb.velocity = Vector3.zero;

            if (CurrentControlScheme == ControlScheme.Controller)
            {
                dashLocation = aimInput.normalized;
                _rb.AddForce(aimInput.normalized * abilitySpeed, ForceMode2D.Impulse);
            }
            else
            {
                dashLocation = aimInput.normalized;
                //Debug.Log(dashLocation);
                _rb.AddForce((aimInput - (Vector2)transform.position).normalized * abilitySpeed, ForceMode2D.Impulse);
            }
            abilityPressed = 0;
        }
    }
}