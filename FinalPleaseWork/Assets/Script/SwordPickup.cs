using UnityEngine;

public class SwordPickup : MonoBehaviour
{
    public Transform playerHand;
    public Transform originalPosition;
    public Animator playerAnimator; // Reference to the player's animator
    public Collider swordCollider; // Collider used for damage calculation
    private bool isHeld = true; // Start with the sword being held

    public float attackDamage = 20f;
    public float attackRange = 1.5f;
    public LayerMask enemyLayer; // Layer to identify enemies

    void Start()
    {
        // Initialize as if the sword is already picked up
        transform.SetParent(playerHand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        GetComponent<Rigidbody>().isKinematic = true;
        swordCollider.enabled = true; // Enable the collider for damage calculation
        playerAnimator.SetBool("IsHoldingSword", true); // Assume sword holding animation state
    }

    void Update()
    {
        // Drop the sword with the Q key
        if (Input.GetKeyDown(KeyCode.Q) && isHeld)
        {
            Drop();
        }

        // Trigger attack with left mouse button when the sword is held
        if (Input.GetMouseButtonDown(0) && isHeld)
        {
            PerformAttack();
        }
    }

    void Drop()
    {
        isHeld = false;
        transform.SetParent(null);
        transform.position = originalPosition.position;
        GetComponent<Rigidbody>().isKinematic = false;
        swordCollider.enabled = false; // Disable the collider when dropped
        playerAnimator.SetBool("IsHoldingSword", false); // End the holding sword animation
    }

    void PerformAttack()
    {
        // Use a Raycast to detect enemies in range and in front of the sword
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange, enemyLayer))
        {
            if (hit.collider != null && hit.collider.CompareTag("Enemy"))
            {
                // Apply damage to the enemy
                hit.collider.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
            }
        }

        // Optional: trigger attack animation
        playerAnimator.SetTrigger("SwingSword");
    }
}
