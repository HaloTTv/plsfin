using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    [Header("General Settings")]
    public float health = 100f;

    [Header("Shooting Settings")]
    public bool canShoot = true;
    public float shootingInterval = 3f;
    public GameObject projectilePrefab;
    public Transform[] projectileSpawnPoints; // Array of spawn points

    [Header("Minion Settings")]
    public GameObject minionPrefab;
    public int numberOfMinions = 3;
    public float spawnRadius = 5f;

    private NavMeshAgent agent;
    private Transform target;
    private float shootingTimer;
    private float lastAttackTime;
    public float attackInterval = 15f;

    [Header("Stomp Settings")]
    public GameObject rockPrefab;
    public int numberOfRocks = 10;
    public float stompSpawnRadius = 5f;  // Renamed to stompSpawnRadius
    public float launchForce = 10f;
    public float stompRadius = 10f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        if (target == null)
        {
            Debug.LogError("Player not found! Make sure your player is tagged correctly.");
        }
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
            if (Time.time - lastAttackTime > attackInterval)
            {
                PerformAction();
                lastAttackTime = Time.time;
            }
        }
    }

    void PerformAction()
    {
        float distanceToPlayer = Vector3.Distance(target.position, transform.position);
        if (distanceToPlayer < 10f)
        {
            SpawnMinions();
        }
        else if (distanceToPlayer < 20f)
        {
            StompAttack();
        }
        else
        {
            ShootProjectiles();
        }
    }

    void ShootProjectiles()
    {
        foreach (Transform spawnPoint in projectileSpawnPoints)
        {
            if (projectilePrefab && spawnPoint)
            {
                GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
                Rigidbody rb = projectile.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 shootingDirection = (target.position - spawnPoint.position).normalized;
                    rb.velocity = shootingDirection * 20f; // Adjust speed as needed
                }
            }
        }
    }

    void SpawnMinions()
    {
        for (int i = 0; i < numberOfMinions; i++)
        {
            Vector3 spawnPos = Random.insideUnitSphere * spawnRadius + transform.position;
            spawnPos.y = 0; // Keep the y position grounded
            Instantiate(minionPrefab, spawnPos, Quaternion.identity);
        }
    }

    private void StompAttack()
    {
        // Additional effects or animations can be triggered here
        SpawnRocks();
    }

    private void SpawnRocks()
    {
        for (int i = 0; i < numberOfRocks; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfRocks;
            Vector3 spawnPos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * stompSpawnRadius + transform.position;
            spawnPos.y = 0.5f;

            GameObject rock = Instantiate(rockPrefab, spawnPos, Quaternion.identity);
            Rigidbody rb = rock.GetComponent<Rigidbody>();
            if (rb != null && i % 3 == 0)
            {
                Vector3 launchDir = Vector3.up + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
                rb.AddForce(launchDir * launchForce, ForceMode.Impulse);
            }
        }
    }
}
