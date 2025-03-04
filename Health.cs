using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;  // Starting health of the player
    public float maxHealth = 100f;  // Maximum health of the player
    public float damageCooldown = 1f;  // Time interval between taking damage

    private bool canTakeDamage = true;

    // Method to apply damage to the player
    public void TakeDamage(float damage)
    {
        if (canTakeDamage)
        {
            health -= damage;
            health = Mathf.Clamp(health, 0, maxHealth); // Make sure health doesn't go below 0

            Debug.Log("Player Health: " + health);

            if (health <= 0)
            {
                Die();
            }
            else
            {
                // Start cooldown to prevent taking damage too quickly
                StartCoroutine(DamageCooldown());
            }
        }
    }

    private void Die()
    {
        Debug.Log("Player has died.");
        // Add death logic here (e.g., trigger animation, game over screen, etc.)
        Destroy(gameObject);  // Destroy the player object for now
    }

    private System.Collections.IEnumerator DamageCooldown()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageCooldown);
        canTakeDamage = true;
    }
}

