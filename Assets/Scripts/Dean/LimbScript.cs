using UnityEngine;

public class LimbScript : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private AudioSource audioSource; // Reference to the AudioSource component

    public AudioClip deathSound; // Assign the death sound in the Unity Editor

    private void Start()
    {
        // Find and store a reference to the PlayerHealth script
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();

        // Subscribe to the OnPlayerDeath event
        playerHealth.OnPlayerDeath += HandlePlayerDeath;

        // Get the AudioSource component attached to this object
        audioSource = GetComponent<AudioSource>();
    }

    private void HandlePlayerDeath()
    {
        FlingBodyAndPlaySound();
    }

    public void FlingBodyAndPlaySound()
    {
        // Play the death sound
        if (audioSource != null && deathSound != null)
        {
            audioSource.clip = deathSound;
            audioSource.Play();
        }

        // Fling the body like a ragdoll
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Calculate a random direction for the force
            Vector3 flingDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(0.5f, 1f), Random.Range(-1f, 1f)).normalized;

            // Apply a random force to simulate a ragdoll effect
            rb.AddForce(flingDirection * Random.Range(10f, 20f), ForceMode.Impulse);

            // Add torque for a spinning effect
            rb.AddTorque(Random.insideUnitSphere * Random.Range(5f, 15f), ForceMode.Impulse);
        }
    }
}
