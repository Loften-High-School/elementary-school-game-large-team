using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracking : MonoBehaviour
{
    public Transform player;           // Reference to the player's Transform
    public float moveSpeed = 5f;       // Movement speed of the enemy
    public float detectionRadius = 10f; // The radius within which the enemy will start following the player
    public float damageAmount = 10f;    // Amount of damage to deal to the player upon contact

    private void Update()
    {
        // Check the distance between the enemy and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // If the player is within the detection radius, start moving towards the player
        if (distanceToPlayer <= detectionRadius)
        {
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        // Calculate the direction towards the player
        Vector3 direction = (player.position - transform.position).normalized;

        // Move the enemy towards the player
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Optional: Make the enemy rotate to face the player
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    // Detect collision with player and apply damage
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the enemy collides with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the PlayerHealth script and apply damage
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);  // Deal damage to the player
            }
        }
    }
}
