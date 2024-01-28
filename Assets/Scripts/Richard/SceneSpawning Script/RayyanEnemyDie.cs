using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayyanEnemyDie : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision involves an object with the "Pepe" tag
        if (collision.gameObject.CompareTag("Pepe"))
        {
            TeleportEnemy();
        }
    }
    private void TeleportEnemy()
    {
        // Teleport the enemy GameObject to a random location
        Vector3 randomTeleportPosition = new Vector3(
            Random.Range(72.77f,72.77f),
            3.11f,
            Random.Range(180.92f,180.92f)
        );

        transform.position = randomTeleportPosition;
    }
}
