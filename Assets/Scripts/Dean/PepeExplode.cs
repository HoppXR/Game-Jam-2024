using UnityEngine;

public class PepeExplode : MonoBehaviour
{
    public GameObject explosionEffect;
    public AudioClip explosionSound;
    private void OnCollisionEnter(Collision collision)
    {
        Explode();
        Destroy(gameObject);
    }

    void Explode()
    {
        if (explosionEffect != null)
        {
            GameObject explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);

            if (explosionSound != null)
            {
                AudioSource.PlayClipAtPoint(explosionSound, transform.position);
            }

            Destroy(explosion, explosion.GetComponent<ParticleSystem>().main.duration);
        }
    }
}
