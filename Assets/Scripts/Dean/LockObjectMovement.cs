using UnityEngine;

public class LockPlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private bool canMove = false;
    private float delay = 50f;
    private float timer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Ensure the Rigidbody component is attached to the player
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing!");
        }
        else
        {
            // Disable Rigidbody's movement by default
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Check if the delay time has passed
        if (timer >= delay)
        {
            canMove = true; // Allow movement
            // Re-enable Rigidbody's movement
            rb.constraints = RigidbodyConstraints.None;
        }
    }
}
