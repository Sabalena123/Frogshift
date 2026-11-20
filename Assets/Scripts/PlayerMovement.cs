using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // SPEED/FORCE VARIABLES
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float jumpForce = 12f;

    // TIME VARIABLES 
    [SerializeField] float jumpBufferTimer;
    [SerializeField] float groundCheckRadius = 0.1f;
    [SerializeField] float jumpBufferTime = 0.12f;
    [SerializeField] float coyoteTime = 0.1f;
    float coyoteTimer;

    // OBJECT VARIABLES
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] SpriteRenderer sprite;

    // ANIMATION VARIABLES
    [SerializeField] Animator animator;
    [SerializeField] bool flipX;

    Rigidbody2D rb;
    InputAction moveAction, jumpAction;

    bool isGrounded;
    Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        if (jumpAction.WasPressedThisFrame())
            jumpBufferTimer = jumpBufferTime;
        else 
            jumpBufferTimer -= Time.deltaTime;

        if (moveInput.x < 0)
            sprite.flipX = true;
        else if (moveInput.x > 0)
            sprite.flipX = false;



        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundMask);

        if (isGrounded)
            coyoteTimer = coyoteTime;
        else
            coyoteTimer -= Time.deltaTime;

        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);

        if (coyoteTimer > 0 && jumpBufferTimer > 0)
        {
            rb.linearVelocity = new Vector2(moveInput.x, jumpForce);
            jumpBufferTimer = 0;
            coyoteTimer = 0;
        }
    }

    private void OnDrawGizmos()
    {
        if (groundCheck == null) return;
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
