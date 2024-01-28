using UnityEngine;

public class PrefabDestroy : MonoBehaviour
{
    public AudioClip destructionSound; // Sound to play upon destruction

    private SpawnAfterDestroyManager spawnManager; // Reference to the SpawnAfterDestroyManager

    private void Start()
    {
        // Find the SpawnAfterDestroyManager in the scene
        spawnManager = FindObjectOfType<SpawnAfterDestroyManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the tag "Pepe"
        if (collision.gameObject.CompareTag("Pepe"))
        {
            // Play destruction sound if available
            if (destructionSound != null)
            {
                AudioSource.PlayClipAtPoint(destructionSound, transform.position);
            }

            // Destroy the current prefab instance
            Destroy(gameObject);

            // Notify the SpawnAfterDestroyManager that a prefab is destroyed
            if (spawnManager != null)
            {
                spawnManager.PrefabDestroyed();
            }
        }
    }
}
