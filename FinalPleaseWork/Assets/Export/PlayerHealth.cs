using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;  // Make sure to include this namespace

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    public UnityEvent onDeath;  // Create an event for when the player dies
    public Slider healthBar;    // Assign this via the Inspector

    void Start()
    {
        if (healthBar != null)
            healthBar.value = health;  // Initialize the health bar
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            health = 0; // Prevent health from going negative
            onDeath.Invoke(); // Trigger the death event
        }
        
        UpdateHealthBar();
    }

    private void Reset()
    {
        // Reset health to full or to a specific start value when needed
        health = 100f;
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
            healthBar.value = health;
        else
            Debug.LogError("Health bar slider is not assigned.");
    }
}
