using System.Collections;
using UnityEngine;

public class LandingSound : MonoBehaviour
{
    public AudioSource landSound;
    private bool isJumping = false;
    private bool isGroundedLastFrame = false;
    private bool canPlayLandSound = true;
    public float cooldownDuration = 1.0f; // Adjust this cooldown duration as needed

    private Transform playerTransform;
    private Collider playerCollider;
    private Rigidbody playerRigidbody;

    public float slopeAngleThreshold = 90f; // Adjust this threshold as needed

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
            // Play landing sound when the player is grounded and moving downwards after a jump
            bool isGrounded = IsGrounded();

            if (!isJumping && isGrounded && !isGroundedLastFrame && canPlayLandSound)
            {
                StartCoroutine(PlayLandSound());
            }

            isGroundedLastFrame = isGrounded;
        }
    }

    IEnumerator PlayLandSound()
    {
        landSound.enabled = true;
        canPlayLandSound = false;

        // Adjust the duration based on your landing sound length
        yield return new WaitForSeconds(landSound.clip.length + cooldownDuration);

        landSound.enabled = false;
        canPlayLandSound = true;
    }

    private bool IsGrounded()
    {
        float rayLength = playerCollider.bounds.extents.y + 0.1f;

        // Cast a ray directly below the player to check if the player is grounded
        RaycastHit hit;
        if (Physics.Raycast(playerTransform.position, Vector3.down, out hit, rayLength))
        {
            float slopeAngle = Vector3.Angle(Vector3.up, hit.normal);
            return slopeAngle < slopeAngleThreshold;
        }

        return false;
    }
}
