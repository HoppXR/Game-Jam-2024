using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public Slider healthBar;
    public AudioSource hitSound;
    public AudioClip deathSound;
    public Transform respawnPoint;

    public float knockbackForce = 10f;
    public float respawnDelay = 2f;
    public float disableMovementTime = 1f;

    public Image deathScreenImage; // Reference to the UI Image component

    private bool isDead = false;

    void Start()
    {
        hitSound = GetComponent<AudioSource>();

        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        // Hide the death screen image at the start
        if (deathScreenImage != null)
        {
            deathScreenImage.gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            currentHealth -= damage;
            hitSound.Play();

            if (healthBar != null)
            {
                healthBar.value = currentHealth;
            }

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    public void ApplyKnockback(Vector3 direction)
    {
        if (!isDead)
        {
            // Apply knockback force to the player
            GetComponent<Rigidbody>().AddForce(direction * knockbackForce, ForceMode.Impulse);
        }
    }

    void Die()
    {
        hitSound.PlayOneShot(deathSound);
        isDead = true;
        Debug.Log("You Died!");

        // Show the death screen image
        if (deathScreenImage != null)
        {
            deathScreenImage.gameObject.SetActive(true);
        }

        StartCoroutine(DeathSequence());
    }

    IEnumerator DeathSequence()
    {
        // Simulate faceplant animation
        float faceplantDuration = 0.5f; // Adjust as needed
        Quaternion startRotation = transform.rotation;
        Quaternion faceplantRotation = Quaternion.Euler(90f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        float timer = 0f;
        while (timer < faceplantDuration)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / faceplantDuration);
            transform.rotation = Quaternion.Slerp(startRotation, faceplantRotation, t);
            yield return null;
        }

        // Disable movement temporarily
        GetComponent<PlayerMovement>().enabled = false;

        yield return new WaitForSeconds(disableMovementTime);

        // Respawn the player
        Respawn();
    }

    void Respawn()
    {
        // Reset health
        currentHealth = maxHealth;
        healthBar.value = currentHealth;

        // Move player to respawn point
        transform.position = respawnPoint.position;

        // Re-enable movement
        GetComponent<PlayerMovement>().enabled = true;

        isDead = false;

        // Hide the death screen image
        if (deathScreenImage != null)
        {
            deathScreenImage.gameObject.SetActive(false);
        }
    }
}
