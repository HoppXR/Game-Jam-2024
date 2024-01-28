using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RichardEnemiesDie : MonoBehaviour
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
            Random.Range(4.74f, 324.34f),
            0.7f,
            Random.Range(104.3f, 335.2f)
        );

        transform.position = randomTeleportPosition;
    }
}
