using UnityEngine;

public class SpawnAfterDestroyManager : MonoBehaviour
{
    public GameObject prefabToSpawnAfterDestroy; // Prefab to spawn after all instances are destroyed
    public Transform spawnLocation; // Location to spawn the prefab

    private int totalPrefabs; // Total number of prefabs to track
    private int destroyedPrefabs; // Number of prefabs destroyed

    void Start()
    {
        // Find the total number of prefabs in the scene
        GameObject[] prefabs = GameObject.FindGameObjectsWithTag("Enemy");
        totalPrefabs = prefabs.Length;
    }

    // Call this method when a prefab is destroyed
    public void PrefabDestroyed()
    {
        destroyedPrefabs++;

        // Check if all prefabs are destroyed
        if (destroyedPrefabs == totalPrefabs && prefabToSpawnAfterDestroy != null && spawnLocation != null)
        {
            // Spawn the prefab at the designated location
            Instantiate(prefabToSpawnAfterDestroy, spawnLocation.position, Quaternion.identity);
        }
    }
}
