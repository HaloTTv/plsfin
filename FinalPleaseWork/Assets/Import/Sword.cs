using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float lightAttackDamage = 10f;
    public float heavyAttackDamage = 25f;
    public float attackRange = 2f;
    public LayerMask enemyLayer;

    public void PickUp(Transform hand)
    {
        transform.SetParent(hand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        GetComponent<Collider>().enabled = false; // Disable the collider when picked up
    }

    public void Drop()
    {
        transform.SetParent(null);
        GetComponent<Collider>().enabled = true; // Enable the collider when dropped
    }

    public void PerformAttack(string type)
    {
        float damage = type == "light" ? lightAttackDamage : heavyAttackDamage;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange, enemyLayer))
        {
            if (hit.collider != null && hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<EnemyHealth>().TakeDamage(damage);
            }
        }
    }
 

}
