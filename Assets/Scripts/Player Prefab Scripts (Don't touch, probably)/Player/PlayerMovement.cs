using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float swingSpeed;

    public float groundDrag;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    public RaycastHit slopeHit;
    private bool exitingSlope;

    [Header("Camera Effects")]
    public PlayerCam cam;
    public float grappleFov = 95f;

    [Header("Camera FOV")]
    public float defaultFov = 60f;
    public float sprintFov = 70f;
    public float fovTransitionSpeed = 5f;

    [Header("Power-up Multipliers")]
    public float jumpBoostMultiplier = 2f;  // Jump boost multiplier during the power-up
    public float movementMultiplier = 2f;   // Movement speed multiplier during the power-up

    [Header("PowerUps")]
    private bool speedBoostActive;
    private bool jumpBoostActive;


    private float originalMoveSpeed;
    private float originalJumpForce;

    public Transform orientation;

    public float horizontalInput;
    public float verticalInput;

    Vector3 moveDirection;

    private bool isWalking;
    private bool isSprinting;
    private float timeSinceLastMove;
    private bool powerUpActive;

    public bool enableMovement = true;

    public Rigidbody rb;

    public MovementState state;
    public enum MovementState
    {
        freeze,
        grappling,
        swinging,
        walking,
        sprinting,
        crouching,
        air
    }

    public bool freeze;

    public bool activeGrapple;
    public bool swinging;

    private void Start()
    {
        // Set the initial state
        isWalking = false;
        isSprinting = false;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        startYScale = transform.localScale.y;

        originalMoveSpeed = moveSpeed;
        originalJumpForce = jumpForce;
    }

    public bool IsGrounded()
    {
        // Ground check using a raycast
        return Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
    }

    private void Update()
    {
        // Check for player movement (excluding crouching) only if movement is enabled
        if (enableMovement)
        {
            if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
            {
                timeSinceLastMove = 0f;  // Player moved, reset the timer

                // Check if walking or sprinting
                isWalking = !Input.GetKey(sprintKey);
                isSprinting = Input.GetKey(sprintKey);
            }
            else
            {
                timeSinceLastMove += Time.deltaTime;

                isWalking = false;
                isSprinting = false;
            }

            // ground check
            grounded = IsGrounded();

            MyInput();
            SpeedControl();
            StateHandler();

            // handle drag
            if (grounded && !activeGrapple)
                rb.drag = groundDrag;
            else
                rb.drag = 0;
        }

        // If movement is disabled (game paused), set the cursor to be locked and invisible.
        // This prevents the player from moving the camera with the mouse.
        Cursor.lockState = enableMovement ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !enableMovement;

        // handle drag
        if (grounded && !activeGrapple)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }


    private void FixedUpdate()
    {
        if (enableMovement)
        {
            MovePlayer();
        }
    }


    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if (Input.GetKeyDown(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        // start crouch
        if (Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        // stop crouch
        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    private void StateHandler()
    {
        // Mode - Freeze
        if (freeze)
        {
            state = MovementState.freeze;
        }

        // Mode - Grappling
        else if (activeGrapple)
        {
            state = MovementState.grappling;
            moveSpeed = sprintSpeed;
        }

        // Mode - Swinging
        else if (swinging)
        {
            state = MovementState.swinging;
            moveSpeed = swingSpeed;
        }

        // Mode - Crouching
        else if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }

        // Mode - Sprinting
        else if (grounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;

            // Smoothly transition to sprint FOV
            cam.SetFov(Mathf.Lerp(cam.GetFov(), sprintFov, Time.deltaTime * fovTransitionSpeed));
        }

        // Mode - Walking
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;

            // Smoothly transition to default FOV
            cam.SetFov(Mathf.Lerp(cam.GetFov(), defaultFov, Time.deltaTime * fovTransitionSpeed));
        }


        // Mode - Air
        else
        {
            state = MovementState.air;
        }
    }

    private void MovePlayer()
    {
        if (enableMovement)
        {
            if (activeGrapple) return;
            if (swinging) return;

            // calculate movement direction
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

            float cameraYaw = orientation.eulerAngles.y;

            // Rotate the player model to match the camera's rotation (only yaw)
            transform.rotation = Quaternion.Euler(0f, cameraYaw, 0f);

            // Move the player in the direction of the camera's forward
            Vector3 moveDirectionRelative = Quaternion.Euler(0f, cameraYaw, 0f) * moveDirection;

            // on slope
            if (OnSlope() && !exitingSlope)
            {
                rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

                if (rb.velocity.y > 0)
                    rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }

            // on ground
            else if (grounded)
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

            // in air
            else if (!grounded)
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

            // turn gravity off while on slope
            rb.useGravity = !OnSlope();
        }
    }

    private void SpeedControl()
    {
        if (activeGrapple) return;

        // limiting speed on slope
        if (OnSlope() && !exitingSlope)
        {
            if (rb.velocity.magnitude > moveSpeed)
                rb.velocity = rb.velocity.normalized * moveSpeed;
        }

        // limiting speed on ground or in air
        else
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // limit velocity if needed
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }
    public void SetEnableMovement(bool enable)
    {
        enableMovement = enable;
    }
    private void Jump()
    {
        exitingSlope = true;

        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void ResetJump()
    {
        readyToJump = true;

        exitingSlope = false;
    }

    private bool enableMovementOnNextTouch;
    public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight)
    {
        activeGrapple = true;

        velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
        Invoke(nameof(SetVelocity), 0.1f);

        Invoke(nameof(ResetRestrictions), 3f);
    }

    private Vector3 velocityToSet;
    private void SetVelocity()
    {
        enableMovementOnNextTouch = true;
        rb.velocity = velocityToSet;

        cam.DoFov(grappleFov);
    }

    public void ResetRestrictions()
    {
        activeGrapple = false;
        cam.DoFov(85f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (enableMovementOnNextTouch)
        {
            enableMovementOnNextTouch = false;
            ResetRestrictions();
        }
    }

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }

    public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
    {
        float gravity = Physics.gravity.y;
        float displacementY = endPoint.y - startPoint.y;
        Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

        Debug.Log($"gravity: {gravity}, displacementY: {displacementY}, displacementXZ: {displacementXZ}");


        // Ensure trajectoryHeight is non-negative
        trajectoryHeight = Mathf.Max(trajectoryHeight, 0f);

        // Check if the denominator is not zero
        if (gravity != 0f)
        {
            float rootTerm = Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / -gravity);
            if (!float.IsNaN(rootTerm) && !float.IsInfinity(rootTerm))
            {
                Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
                Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity) + rootTerm);

                // Check if any component contains NaN or Infinity
                if (!ContainsInvalidValues(velocityY) && !ContainsInvalidValues(velocityXZ))
                {
                    return velocityXZ + velocityY;
                }
            }
        }

        Debug.LogError("Invalid jump velocity calculation. Returning Vector3.zero.");
        return Vector3.zero;
    }

    // Helper method to check if a vector contains NaN or Infinity values
    private bool ContainsInvalidValues(Vector3 vector)
    {
        return float.IsNaN(vector.x) || float.IsNaN(vector.y) || float.IsNaN(vector.z) ||
               float.IsInfinity(vector.x) || float.IsInfinity(vector.y) || float.IsInfinity(vector.z);
    }


    #region Text & Debugging

    public static float Round(float value, int digits)
    {
        float mult = Mathf.Pow(10.0f, (float)digits);
        return Mathf.Round(value * mult) / mult;
    }

    #endregion

    public void ApplyPowerUp(float jumpMultiplier, float duration)
    {
        if (!powerUpActive)
        {
            StartCoroutine(PowerUpRoutine(jumpMultiplier, duration));
        }
    }

    private IEnumerator PowerUpRoutine(float jumpMultiplier, float duration)
    {
        powerUpActive = true;

        // Save original values
        float originalJumpForce = jumpForce;

        // Apply power-up effects
        jumpForce *= jumpMultiplier;

        // Wait for the duration of the power-up
        yield return new WaitForSeconds(duration);

        // Reset to original values
        jumpForce = originalJumpForce;

        // Reset the power-up flag
        powerUpActive = false;
    }
}