using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    private float damage;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }

    void Update()
    {
        if (target != null)
        {
            // Assume the projectile moves towards the target each frame
            float speed = 10f; // Adjust speed as needed
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == target)
        {
            // Assuming the target has a method to take damage
            target.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject); // Destroy projectile after hitting the target
        }
    }
}
