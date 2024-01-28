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
        Vector3 randomTeleportPosition = new Vector3(34.68f, 5.96f, 153.41f);
        transform.position = randomTeleportPosition;
    }
}
