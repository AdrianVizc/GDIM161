using Photon.Pun.Demo.Cockpit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Movement : MonoBehaviour
{
    PhotonView view;
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 9f;
    public float normAccel = 1f;
    [HideInInspector] public float accelSpeed;
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
    public bool canDoubleJump;
    public bool spaceAgain;

    //[Header("Slide Settings")]
    // [SerializeField] private float crouchSpeed = 4.5f;
    // [SerializeField] private float crouchYScale = 0.5f;
    private float startYScale;

    //[Header("Stamina Bar")]
    //[SerializeField] private Image staminaBar;

    [Header("Ground Check")]
    [SerializeField] private float playerHeight = 2;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundDrag = 5;
    [HideInInspector] public bool grounded;

    [Header("Slope Check")]
    [SerializeField] private float maxSlopeAngle = 20f;
    private RaycastHit slopeHit;
    private bool exitingSlope;

    [Header("Keybinds")]
    [SerializeField] public KeyCode jumpkey = KeyCode.Space;
    [SerializeField] public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Animator")]
    [SerializeField] private Animator animator;

    private Camera mainCamera;

    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDir;
    private Rigidbody rb;

    [SerializeField]
    public GameObject playerShield;

    [SerializeField]
    private AudioSource explosionAudio;

    public MovementState state;

    public enum MovementState
    {
        sprinting,
        crouching,
        air
    }
    private void Awake()
    {
        view = GetComponentInParent<PhotonView>();
    }
    private void Start()
    {
        if (!view.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
        }
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        startYScale = transform.localScale.y;
        isInputMoving = false;
        currentStamina = staminaAmount;
        staminaRecover = false;
        accelSpeed = normAccel;
        canDoubleJump = false;
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
        if (!view.IsMine)
        {
            return;
        }

        // Check if WASD, Jump, or movement abilities are being pressed
        isInputMoving = (Input.GetKey(jumpkey) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D));
        animator.SetBool("isMoving", isInputMoving);

        // Rotate player with camera
        // transform.rotation = Quaternion.Euler(0, mainCamera.transform.localEulerAngles.y, 0);

        // Ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);

        // Update stamina bar
        //staminaBar.fillAmount = currentStamina / staminaAmount;

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
        if (grounded) // Walking
        {
            state = MovementState.sprinting;
            playerSpeed = moveSpeed;
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
            rb.AddForce(moveDir.normalized * playerSpeed * accelSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDir.normalized * playerSpeed * accelSpeed * 10f * airMultiplier, ForceMode.Force);
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
        // if(Input.GetKeyDown(crouchKey))
        // {
        //     transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
        //     rb.AddForce(Vector3.down * 25f, ForceMode.Impulse);
        // }
        // 
        // // End Crouch
        // if(Input.GetKeyUp(crouchKey))
        // {
        //     transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        // }
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

        canDoubleJump = true;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bomb"))
        {
            explosionAudio.Play();
        }
    }
}
