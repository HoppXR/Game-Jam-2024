using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public Slider healthBar;

    public AudioSource hitSound;

    public float knockbackForce = 10f;

    public event Action OnPlayerDeath;

    void Start()
    {
        hitSound = GetComponent<AudioSource>();

        currentHealth = maxHealth;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    public void TakeDamage(int damage)
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

    public void ApplyKnockback(Vector3 direction)
    {
        // Apply knockback force to the player
        GetComponent<Rigidbody>().AddForce(direction * knockbackForce, ForceMode.Impulse);
    }

    void Die()
    {
        Debug.Log("You Died!");
        hitSound.Play();
    }
}
