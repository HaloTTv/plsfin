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

    private Rigidbody rb;
    private bool isGrounded;
    private bool jumpRequested = false;

    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = walkSpeed;
         animator = GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, checkGroundRadius, groundLayer);
        HandleMovement();
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpRequested = true;
        }
        if (Input.GetMouseButtonDown(0)) // Assuming left mouse click triggers the swing
        {
            animator.SetTrigger("swingSword");
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
}
