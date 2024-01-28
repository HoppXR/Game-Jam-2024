using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JadenEnemyDie : MonoBehaviour
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
        Vector3 randomTeleportPosition = new Vector3(116.06f, 6.6f, 128.72f);

        transform.position = randomTeleportPosition;
    }
}
