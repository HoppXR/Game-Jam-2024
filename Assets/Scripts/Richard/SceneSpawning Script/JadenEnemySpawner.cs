using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JadenEnemySpawner : MonoBehaviour
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
            Vector3 randomSpawnPosition = new Vector3(116.06f, 6.6f, 128.72f);

            Instantiate(prefabToSpawn, randomSpawnPosition, Quaternion.identity);
        }
    }
}
