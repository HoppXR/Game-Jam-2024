using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsmondEnemySpawner : MonoBehaviour
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
            Vector3 randomSpawnPosition = new Vector3(Random.Range(84f, 126f), -3.1f, Random.Range(15f, 198f));

            Instantiate(prefabToSpawn, randomSpawnPosition, Quaternion.identity);
        }
    }
}
