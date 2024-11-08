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
    private float dashMax = 0.15f;
    // dashCdMax is the total dash cooldown
    // dashCdTimer is a helper variable to count said cooldown
    [SerializeField]
    private float dashCdMax = 3f;
    [SerializeField]
    private float dashCdTimer = 0f;

    // hold previous dash location
    Vector2 dashLocation;
    
    [SerializeField]
    private float maxSpeed = 4;
    private float frictionCoeff = 50f;
    
    [SerializeField]
    private bool frictionApplied = false;

    public bool disableMovement;

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
                Destroy(this);
                throw new Exception("No Valid Control Scheme");
            }
        }
    }

    public bool DisableMovement
    {
        get { return disableMovement; }
        set { disableMovement = value; }
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
        Debug.Log(CurrentControlScheme);
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

        // movement
        // if not dashing, or disableMovement is false, allow movement
        if (!dashing || !disableMovement)
        {
            _rb.AddForce(movementInput * moveSpeed);
            //Debug.Log("movement input called");
        }

        // player dash
        PlayerDash();
        PlayerAbilityDash();

        // count cooldowns
        UpdateTimers();
        
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
    }
    private void PlayerDash()
    {
        // if not dashing, set dashing to false (player lockout)
        // dash timer is to count player control lockout
        if (dashTimer <= 0)
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
                dashLocation = aimInputMouse.normalized;
                _rb.AddForce((aimInputMouse - (Vector2)transform.position).normalized * dashSpeed, ForceMode2D.Impulse);
            }
        }
    }

    private void PlayerAbilityDash()
    {
        // if not dashing, set dashing to false
        if (dashTimer <= 0)
        {
            dashing = false;
        }

        // when key is pressed, and you are not currently dashing, and the cd is done
        if (abilityPressed == 1 && !dashing)
        {
            //Debug.Log("Ability Dash");
            dashing = true;
            // reset timers
            //dashTimer = dashMax;
            _rb.velocity = Vector3.zero;

            if (CurrentControlScheme == ControlScheme.Controller)
            {
                dashLocation = aimInput.normalized;
                _rb.AddForce(aimInput.normalized * abilitySpeed, ForceMode2D.Impulse);
            }
            else
            {
                dashLocation = aimInputMouse.normalized;
                //Debug.Log(dashLocation);
                _rb.AddForce((aimInputMouse - (Vector2)transform.position).normalized * abilitySpeed, ForceMode2D.Impulse);
            }
            abilityPressed = 0;
        }
    }
}