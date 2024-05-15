using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public float damage = 20f;  // Amount of damage the rock deals on collision
    public float launchPower = 10f;  // Power with which the player will be launched

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);  // Deal damage to the player

                // Launch the player up and away
                Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
                if (playerRb != null)
                {
                    Vector3 launchDirection = (collision.transform.position - transform.position).normalized + Vector3.up;
                    playerRb.AddForce(launchDirection * launchPower, ForceMode.Impulse);
                }
            }

            // Destroy the rock after impacting the player
            Destroy(gameObject);
        }
    }
}
