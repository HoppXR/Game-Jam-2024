using UnityEngine;

public class RPGWeapon : MonoBehaviour
{
    public GameObject thunderboltPrefab; // Reference to the Thunderbolt prefab
    public Transform firePoint; // Point from where the Thunderbolt is instantiated
    public GameObject rocketHead; // Reference to the Rocket Head object
    public float shootForce = 10f; // Force applied to shoot the Thunderbolt
    public float cooldown = 1f; // Cooldown between shots

    private float cooldownTimer = 0f; // Timer to track the cooldown

    void Update()
    {
        // Decrease cooldown timer
        cooldownTimer -= Time.deltaTime;

        // Check for input to shoot and cooldown
        if (Input.GetButtonDown("Fire1") && cooldownTimer <= 0f)
        {
            ShootThunderbolt();
            cooldownTimer = cooldown; // Reset cooldown timer
        }

        // Check if cooldownTimer is less than or equal to 0 and the rocket head is not active
        if (cooldownTimer <= 0f && rocketHead != null && !rocketHead.activeSelf)
        {
            rocketHead.SetActive(true); // Activate the rocket head
        }
    }

    void ShootThunderbolt()
    {
        // Deactivate or destroy the rocket head object
        if (rocketHead != null)
        {
            rocketHead.SetActive(false); // Deactivate the rocket head
        }

        // Calculate the direction to shoot (based on player's facing direction)
        Vector3 shootDirection = transform.forward;

        // Instantiate the Thunderbolt prefab at the firePoint position and rotation
        GameObject thunderbolt = Instantiate(thunderboltPrefab, firePoint.position, Quaternion.identity); // Set rotation to identity

        // Get the Rigidbody component of the Thunderbolt prefab
        Rigidbody rb = thunderbolt.GetComponent<Rigidbody>();

        // Check if Rigidbody component exists
        if (rb != null)
        {
            // Apply force to the Thunderbolt in the calculated direction
            rb.AddForce(shootDirection * shootForce, ForceMode.Impulse);
        }
    }
}
