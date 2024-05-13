using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    public UnityEvent onDeath; // Create an event for when the player dies

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            health = 0; // Prevent health from going negative
            onDeath.Invoke(); // Trigger the death event
        }
    }

    private void Reset()
    {
        // Reset health to full or to a specific start value when needed
        health = 100f;
    }
}
