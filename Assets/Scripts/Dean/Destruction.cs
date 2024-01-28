using UnityEngine;

public class Destruction : MonoBehaviour
{
    public AudioClip destructionSound; // Sound to play upon destruction

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the tag "Pepe"
        if (collision.gameObject.CompareTag("Pepe"))
        {
            // Play destruction sound if available
            if (destructionSound != null)
            {
                AudioSource.PlayClipAtPoint(destructionSound, transform.position);
            }
        }
    }
}
