using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    private Transform target;
    private float speed;
    private float damage;

    public void Initialize(Transform target, float damage, float speed)
    {
        this.target = target;
        this.damage = damage;
        this.speed = speed;
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            GetComponent<Rigidbody>().velocity = direction * speed;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == target)
        {
            // Assuming the player has a method to take damage
            target.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject); // Destroy projectile after hitting the target
        }
    }
}
