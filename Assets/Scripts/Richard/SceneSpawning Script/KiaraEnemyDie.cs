using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiaraEnemyDie : MonoBehaviour
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
        Vector3 randomTeleportPosition = new Vector3(28f, 8.7f, -2.4f);
        transform.position = randomTeleportPosition;
    }
}