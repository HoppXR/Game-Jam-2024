using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioSource footstepSound, sprintSound, jumpSound;
    private bool isMoving = false;
    private bool isSprinting = false;
    private bool isGrounded = false;
    private bool isJumping = false;

    private Transform playerTransform;
    private Collider playerCollider;
    private Rigidbody playerRigidbody;

    private void Start()
    {
        playerTransform = transform;
        playerCollider = GetComponent<Collider>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the game is not paused
        if (!PauseMenu.GameIsPaused)
        {
            // Perform a ground check with slope consideration
            isGrounded = CheckIfGrounded();

            // Check if any movement key is pressed
            isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

            // Check if LeftShift is pressed
            isSprinting = Input.GetKey(KeyCode.LeftShift) && isMoving && isGrounded;

            // Play footstep sound
            footstepSound.enabled = isMoving && !isSprinting && isGrounded;

            // Play sprint sound
            sprintSound.enabled = isSprinting;

            // Play jump sound only when grounded and not already jumping
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isJumping)
            {
                StartCoroutine(PlayJumpSound());
            }
        }
        else
        {
            // Game is paused, disable all movement sounds
            footstepSound.enabled = false;
            sprintSound.enabled = false;
            jumpSound.enabled = false;
        }
    }


    IEnumerator PlayJumpSound()
    {
        jumpSound.enabled = true;
        isJumping = true;

        // Adjust the duration based on your jump sound length
        yield return new WaitForSeconds(jumpSound.clip.length);

        jumpSound.enabled = false;
        isJumping = false;
    }

    private bool CheckIfGrounded()
    {
        float rayLength = playerCollider.bounds.extents.y + 0.1f;


        // Cast rays around the player to check if any point is grounded
        return Physics.Raycast(playerTransform.position, Vector3.down, rayLength) ||
               Physics.Raycast(playerTransform.position + playerTransform.forward * playerCollider.bounds.extents.z, Vector3.down, rayLength) ||
               Physics.Raycast(playerTransform.position - playerTransform.forward * playerCollider.bounds.extents.z, Vector3.down, rayLength) ||
               Physics.Raycast(playerTransform.position + playerTransform.right * playerCollider.bounds.extents.x, Vector3.down, rayLength) ||
               Physics.Raycast(playerTransform.position - playerTransform.right * playerCollider.bounds.extents.x, Vector3.down, rayLength);
    }
}