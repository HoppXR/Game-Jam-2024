using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayyanEnemySpawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public float spawnDelay = 7f;

    void Start()
    {
        StartCoroutine(SpawnPrefabsWithDelay());
    }

    IEnumerator SpawnPrefabsWithDelay()
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            yield return new WaitForSeconds(spawnDelay);

            // Spawn the ith prefab at a random position
            GameObject prefabToSpawn = prefabs[i];
            Vector3 randomSpawnPosition = new Vector3(Random.Range(4.74f, 324.34f), 0.7f, Random.Range(104.3f, 335.2f));

            Instantiate(prefabToSpawn, randomSpawnPosition, Quaternion.identity);
        }
    }
}
