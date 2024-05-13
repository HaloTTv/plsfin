using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Health Settings")]
    public float health = 100f;
    public GameObject deathMenuPanel;  // Make sure this is assigned in the Unity Inspector

    [Header("Basic Movement Settings")]
    public float walkSpeed = 5.0f;
    public float sprintSpeed = 10.0f;
    private float currentSpeed;

    [Header("Jump Settings")]
    public float jumpForce = 8.0f;
    public LayerMask groundLayer;
    public float checkGroundRadius = 0.1f;
    public Transform groundCheck;

    private Rigidbody rb;
    private Animator animator;
    private bool isGrounded;
    private bool jumpRequested = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        deathMenuPanel.SetActive(false);  // Ensure the death panel is hidden initially
        currentSpeed = walkSpeed;
    }

    void Update()
    { 
        if (Input.GetMouseButtonDown(0) && animator.GetBool("IsHoldingSword"))
        {
            animator.SetTrigger("swingSword");
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, checkGroundRadius, groundLayer);
        HandleMovement();
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpRequested = true;
        }
        if (health <= 0)
        {
            Die();
        }
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
        ShowDeathMenu();
    }

    private void ShowDeathMenu()
{
    deathMenuPanel.SetActive(true);
    Time.timeScale = 0; // Pause the game

    // Enable the canvas if it's disabled
    Canvas canvas = deathMenuPanel.GetComponent<Canvas>();
    if (canvas != null)
    {
        canvas.enabled = true;
    }
    else
    {
        Debug.LogError("No Canvas component found on the death menu panel!");
    }

    // Ensure the EventSystem is enabled
    UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
}

    public void RestartGame()
    {
        Time.timeScale = 1;  // Resume the game time
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
