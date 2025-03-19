using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MarblePlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 15f;
    public float jumpForce = 8f;
    public float maxVelocity = 10f;

    [Header("Responsiveness Settings")]
    public float stoppingDrag = 3f;
    public float movingDrag = 0.2f;
    public float turnSpeedMultiplier = 2f;

    private Rigidbody rb;
    private Vector3 moveDirection; // Renamed from moveInput

    [Header("Camera Reference")]
    public Transform cameraTransform; // Assign the Main Camera here

    [Header("Ground Check")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.3f;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = maxVelocity;
    }

    void Update()
    {
        // Get movement input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0;
        camRight.y = 0;

        // Relative Camera Direction
        Vector3 forwardRelative = moveZ * camForward;
        Vector3 rightRelative = moveX * camRight;

        moveDirection = (forwardRelative + rightRelative).normalized;

        // Apply velocity instead of angular velocity
        rb.linearVelocity = new Vector3(moveDirection.x * moveSpeed, rb.linearVelocity.y, moveDirection.z * moveSpeed);

        // Jumping
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        // Adjust drag dynamically
        rb.linearDamping = moveDirection.magnitude > 0 ? movingDrag : stoppingDrag;
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (moveDirection.magnitude > 0)
        {
            Vector3 force = moveDirection * moveSpeed * Time.fixedDeltaTime * 100f;
            Vector3 velocityCorrection = (moveDirection * maxVelocity - rb.linearVelocity) * turnSpeedMultiplier;
            rb.AddForce(force + velocityCorrection, ForceMode.Acceleration);
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
