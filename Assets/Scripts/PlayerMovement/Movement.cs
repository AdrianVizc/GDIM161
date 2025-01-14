using Photon.Pun.Demo.Cockpit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 9f;
    [SerializeField] private float sprintSpeed = 14f;
    [HideInInspector] public float playerSpeed;
    private bool isInputMoving;

    [Header("Stamina Settings")]
    [SerializeField] private float staminaAmount = 5f;
    [SerializeField] private float staminaRecoverMultiplier = 1f;
    private float currentStamina;
    private bool staminaRecover;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 6.5f;
    private float jumpCooldown = 0.25f;
    private float airMultiplier = 0.4f;
    private bool readyToJump;

    [Header("Crouch Settings")]
    [SerializeField] private float crouchSpeed = 4.5f;
    [SerializeField] private float crouchYScale = 0.5f;
    private float startYScale;

    [Header("Stamina Bar")]
    [SerializeField] private Image staminaBar;

    [Header("Ground Check")]
    [SerializeField] private float playerHeight = 2;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundDrag = 5;
    private bool grounded;

    [Header("Slope Check")]
    [SerializeField] private float maxSlopeAngle = 20f;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    [Header("Keybinds")]
    [SerializeField] public KeyCode jumpkey = KeyCode.Space;
    [SerializeField] public KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] public KeyCode crouchKey = KeyCode.LeftControl;

    private Camera mainCamera;

    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDir;
    private Rigidbody rb;
    private AbilityControlHandler ac;

    public MovementState state;

    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        air
    }

    private void Start()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        ac = GetComponent<AbilityControlHandler>();
        rb.freezeRotation = true;
        readyToJump = true;
        startYScale = transform.localScale.y;
        isInputMoving = false;
        currentStamina = staminaAmount;
        staminaRecover = false;
    }

    private void FixedUpdate()
    {
        MovePlayer();

        if (state == MovementState.sprinting)
        {
            StopStaminaRecover();
            StaminaHandler();
        }
        if(staminaRecover)
        {
            StaminaRecovering();
        }
    }
    private void Update()
    {
        // Check if WASD, Jump, or movement abilities are being pressed
        isInputMoving = Input.GetKey(jumpkey) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)||
                        Input.GetKey(ac.movementAbility);

        // Rotate player with camera
        transform.rotation = Quaternion.Euler(0, mainCamera.transform.localEulerAngles.y, 0);

        // Ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);

        // Update stamina bar
        staminaBar.fillAmount = currentStamina / staminaAmount;

        // Start sprint restore on sprint key release
        if (Input.GetKeyUp(sprintKey))
        {
            StartStaminaRecover();
        }

        MyInput();
        SpeedControl();
        StateHandler();

        if (grounded)
        {
            if (!isInputMoving)
            {
                rb.drag = 25f;
            }
            else
            {
                rb.drag = groundDrag;
            }
        }
        else
        {
            rb.drag = 0;
        }
    }

    // Handles movement state
    private void StateHandler()
    {
        if(Input.GetKey(crouchKey)) // Crouching
        {
            state = MovementState.crouching;
            playerSpeed = crouchSpeed;
        }
        else if(grounded && Input.GetKey(sprintKey) && StaminaHandler()) // Sprinting
        {
            state = MovementState.sprinting;
            playerSpeed = sprintSpeed;
        }
        else if (grounded) // Walking
        {
            state = MovementState.walking;
            playerSpeed = walkSpeed;
        }
        else // Air
        {
            state = MovementState.air;
        }
    }

    private void MovePlayer()
    {
        moveDir = mainCamera.transform.forward * verticalInput + mainCamera.transform.right * horizontalInput;
        moveDir = new Vector3(moveDir.x, 0, moveDir.z);

        // On slope
        if(OnSlope() && !exitingSlope)
        {
            rb.AddForce(GetSlopeMoveDirection(moveDir) * playerSpeed * 20f, ForceMode.Force);
            if(isInputMoving)
            {
                rb.AddForce(Vector3.down * 80f, ForceMode.Force); // Eliminates bounce effect going up
            }
        }

        // On ground
        if (grounded)
        {
            rb.AddForce(moveDir.normalized * playerSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDir.normalized * playerSpeed * 10f * airMultiplier, ForceMode.Force);
        }

        // Gravity off when on slope
        rb.useGravity = !OnSlope();
    }

    private bool StaminaHandler()
    {
        if(currentStamina >= 0)
        {
            currentStamina -= Time.deltaTime;
            return true;
        }
        else
        {
            currentStamina = 0;
        }
        return false;
    }

    private void StartStaminaRecover()
    {
        staminaRecover = true;
    }

    private void StaminaRecovering()
    {
        if(currentStamina <= staminaAmount)
        {
            currentStamina += Time.deltaTime * staminaRecoverMultiplier;
        }
        else
        {
            currentStamina = staminaAmount;
            StopStaminaRecover();
        }
    }

    private void StopStaminaRecover()
    {
        staminaRecover = false;
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKey(jumpkey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        // Crouch
        if(Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 25f, ForceMode.Impulse);
        }

        // End Crouch
        if(Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    private void SpeedControl()
    {
        // Limit speed on slope
        if(OnSlope())
        {
            if(rb.velocity.magnitude > playerSpeed)
            {
                rb.velocity = rb.velocity.normalized * playerSpeed;
            }
        }
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // Limit velocity
            if (flatVel.magnitude > playerSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * playerSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

    public void Jump()
    {
        exitingSlope = true;

        // Reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        exitingSlope = false;
        readyToJump = true;
    }

    public bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);

            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }

    public bool getGrounded()
    {
        return grounded;
    }
}
