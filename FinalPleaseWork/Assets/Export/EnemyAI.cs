using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Required for NavMesh

public class EnemyAI : MonoBehaviour
{
    [Header("General Settings")]
    public float health = 100f;
    public float damage = 10f;
    public float moveSpeed = 3f; // Adjust based on enemy type

    [Header("Shooting Settings")]
    public bool canShoot = true;
    public float shootingInterval = 3f;
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;

    private NavMeshAgent agent;
    private Transform target; // Player target
    private float shootingTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed; // Set NavMeshAgent speed
        target = GameObject.FindGameObjectWithTag("Player").transform; // Find the player
        if (target == null)
        {
            Debug.LogError("Player not found! Make sure your player is tagged correctly.");
        }
    }

    void Update()
    {
        if (target != null)
        {
            MoveToTarget();
            if (canShoot)
            {
                HandleShooting();
            }
        }
    }

    private void MoveToTarget()
    {
        agent.SetDestination(target.position); // Set destination to move towards the player
    }

    private void HandleShooting()
    {
        shootingTimer += Time.deltaTime;
        if (shootingTimer >= shootingInterval)
        {
            shootingTimer = 0;
            ShootProjectile();
        }
    }

    private void ShootProjectile()
{
    if (projectilePrefab && projectileSpawnPoint)
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            // Assuming your Projectile script has methods to set the target and damage
            projectileScript.SetTarget(target);
            projectileScript.SetDamage(damage);
        }
        else
        {
            Debug.LogError("Projectile script not found on instantiated projectile object.");
        }
    }
}

}
