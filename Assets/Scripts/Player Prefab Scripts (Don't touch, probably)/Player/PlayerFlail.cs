using UnityEngine;

public class PlayerFlail : MonoBehaviour
{
    public float flailSpeed = 5f;   // Adjust the speed of flailing
    public float flailIntensity = 0.1f;  // Adjust the intensity of flailing

    private Vector3 originalRotation;
    private bool isFlailing = false;

    private void Start()
    {
        originalRotation = transform.localEulerAngles;
    }

    private void Update()
    {
        if (ShouldFlail())
        {
            if (!isFlailing)
            {
                // Start flailing animation
                isFlailing = true;
                StartCoroutine(FlailArms());
            }
        }
        else
        {
            // Stop flailing animation
            isFlailing = false;
        }
    }

    private bool ShouldFlail()
    {
        // Add conditions for when the arms should flail, e.g., when moving or sprinting
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        bool isMoving = Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f;

        // You can customize these conditions based on your game's movement logic
        return isMoving;
    }

    private System.Collections.IEnumerator FlailArms()
    {
        while (isFlailing)
        {
            // Apply flailing animation using Perlin noise
            float perlinX = Mathf.PerlinNoise(Time.time * flailSpeed, 0) * 2 - 1;
            float perlinY = Mathf.PerlinNoise(0, Time.time * flailSpeed) * 2 - 1;

            Vector3 flailRotation = new Vector3(perlinX, perlinY, 0) * flailIntensity;
            transform.localEulerAngles = originalRotation + flailRotation;

            yield return null;
        }
    }
}