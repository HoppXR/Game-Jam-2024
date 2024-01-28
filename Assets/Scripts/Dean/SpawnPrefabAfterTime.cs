using UnityEngine;

public class SpawnPrefabAfterTime : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public Transform spawnPositionObject; // Changed to Transform type for empty GameObject
    public AudioClip soundToPlay;

    private bool hasSpawned = false;
    private float timer = 0f;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Increment timer
        timer += Time.deltaTime;

        // Check if 65 seconds have passed and prefab hasn't spawned yet
        if (timer >= 65f && !hasSpawned)
        {
            SpawnPrefab();
            hasSpawned = true;
        }
    }

    void SpawnPrefab()
    {
        Vector3 spawnPosition = spawnPositionObject.position; // Get position of the spawnPositionObject

        // Instantiate the prefab at the specified position
        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

        // Play the sound
        if (soundToPlay != null && audioSource != null)
        {
            audioSource.PlayOneShot(soundToPlay);
        }
    }
}
