using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f;
    private Transform target;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (target != null)
        {
            // Use UnityEngine.Random to specify that we're using Unity's Random class
            Vector3 shootDirection = (target.position - transform.position).normalized + UnityEngine.Random.insideUnitSphere * 0.1f; // Add randomness to the shoot direction
            rb.AddForce(shootDirection * 20f, ForceMode.Impulse); // Modify the force multiplier as needed
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall")) // Make sure your walls have the tag "Wall"
        {
            Destroy(gameObject); // Destroy the projectile upon hitting a wall
        }
        else if (other.CompareTag("Player")) // Assuming player has the tag "Player"
        {
            // Apply damage to the player, then destroy the projectile
            other.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
