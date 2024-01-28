using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsmondEnemyDie : MonoBehaviour
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
            Random.Range(84f, 126f),
            -3.1f,
            Random.Range(15f, 198f)
        );

        transform.position = randomTeleportPosition;
    }
}