using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Sliding : MonoBehaviour
{
    PhotonView view;

    private Transform orientation;
    private Rigidbody rb;
    private Movement playerMovement;

    [Header("Sliding Settings")]
    [SerializeField] private float maxSlideTime = 0.75f;
    [SerializeField] private float slideForce = 350f;
    [SerializeField] private float slideYScale = 0.5f;
    private float startYScale;
    private float slideTimer;

    [Header("Slide Keybind")]
    [SerializeField] private KeyCode slideKey = KeyCode.LeftControl;
    private float horizontalInput;
    private float verticalInput;
    private bool sliding;

    [Header("Animator")]
    [SerializeField] private Animator animator;

    private void Awake()
    {
        view = transform.root.GetComponent<PhotonView>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMovement = GetComponent<Movement>();
        startYScale = transform.localScale.y;
        orientation = Camera.main.transform;
    }

    private void Update()
    {
        if (!view.IsMine)
        {
            return;
        }

        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (playerMovement.state == Movement.MovementState.sprinting)
        {
            if (Input.GetKeyDown(slideKey) && (horizontalInput != 0 || verticalInput != 0))
            {
                animator.SetBool("isSliding", true);
                StartSlide();
            }

            if (Input.GetKeyUp(slideKey) && sliding)
            {
                StopSlide();
            }
        }
    }

    private void FixedUpdate()
    {
        if(sliding)
        {
            SlidingMovement();
        }
    }

    private void StartSlide()
    {
        sliding = true;

        transform.localScale = new Vector3(transform.localScale.x, slideYScale, transform.localScale.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        slideTimer = maxSlideTime;
    }

    private void SlidingMovement()
    {
        Vector3 inputDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        Vector3 newDirection = new Vector3(inputDirection.x, 0, inputDirection.z);

        // If not on a slope or moving up
        if (!playerMovement.OnSlope() || rb.velocity.y > -0.1f)
        {
            rb.AddForce(newDirection.normalized * slideForce, ForceMode.Force);

            slideTimer -= Time.deltaTime;
        }
        else // Sliding down slope
        {
            rb.AddForce(playerMovement.GetSlopeMoveDirection(inputDirection) * slideForce, ForceMode.Force);
        }

        if(slideTimer <= 0)
        {
            StopSlide();
        }
    }

    private void StopSlide()
    {
        animator.SetBool("isSliding", false);
        sliding = false;

        transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
    }
}
