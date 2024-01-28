using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject prefabToSpawn; // The prefab you want to spawn
    public Transform spawnPoint; // The empty GameObject serving as the spawn point
    public float spawnDelay = 50f; // Time delay before spawning

    private float timer = 0f;
    private bool hasSpawned = false;

    void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Check if the time delay has passed and if the prefabs haven't been spawned yet
        if (timer >= spawnDelay && !hasSpawned)
        {
            // Spawn the prefabs
            SpawnPrefabs();
            hasSpawned = true; // Mark as spawned
        }
    }

    void SpawnPrefabs()
    {
        // Spawn four prefabs at the specified spawn point
        for (int i = 0; i < 4; i++)
        {
            Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);
        }
    }
}
