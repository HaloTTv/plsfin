using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Basic Movement Settings")]
    public float walkSpeed = 5.0f;
    public float sprintSpeed = 10.0f;
    private float currentSpeed;

    [Header("Jump Settings")]
    public float jumpForce = 8.0f;
    public LayerMask groundLayer;
    public float checkGroundRadius = 0.1f;
    public Transform groundCheck;

    [Header("Combat Settings")]
    public GameObject sword;
    public Animator animator;
    public float attackDamage = 25f;
    public float attackRange = 2f;
    public LayerMask enemyLayer;

    [Header("Health Settings")]
    [SerializeField] private float health = 100f;

    [Header("Animation Clips")]
    public AnimationClip idleAnimation;
    public AnimationClip lightAttackAnimation;
    public AnimationClip heavyAttackAnimation;

    private Rigidbody rb;
    private bool isGrounded;
    private bool jumpRequested = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = walkSpeed;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, checkGroundRadius, groundLayer);
        HandleMovement();
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpRequested = true;
        }
        HandleAttack();
    }

    void FixedUpdate()
    {
        if (jumpRequested)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpRequested = false;
        }
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movementDirection = (transform.right * horizontal + transform.forward * vertical).normalized;
        currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;
        transform.Translate(movementDirection * currentSpeed * Time.deltaTime, Space.World);
    }

    private void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button for light attack
        {
            animator.SetTrigger("LightAttack");
            PerformAttack();
        }
        if (Input.GetMouseButtonDown(1)) // Right mouse button for heavy attack
        {
            animator.SetTrigger("HeavyAttack");
            PerformAttack();
        }
    }

    void PerformAttack()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange, enemyLayer))
        {
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(attackDamage);
                }
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player Died!");
    }

        private RuntimeAnimatorController CreateAnimatorController()
    {
        // This method dynamically creates an AnimatorController based on assigned clips
        var controller = new AnimatorOverrideController();
        controller["Idle"] = idleAnimation;
        controller["LightAttack"] = lightAttackAnimation;
        controller["HeavyAttack"] = heavyAttackAnimation;
        return controller;
    }
}

