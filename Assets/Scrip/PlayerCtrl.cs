using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    [SerializeField] private Animator animator; // Reference to the Animator component
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveForce = 10f; // Movement force multiplier
    [SerializeField] private float inputThreshold = 0.1f; // Minimum input to consider as movement

    private float xInput; // Horizontal input value
    private float yInput; // Vertical input value
    private Vector2 lastFacingDirection = Vector2.down; // Start facing down

    void Start()
    {
        // Initialize facing down (IdleDown)
        animator.SetFloat("X", 0f);
        animator.SetFloat("Y", -1f);
    }

    void Update()
    {
        PlayerMove();
    }

    private void FixedUpdate()
    {
        OnAnimatorMove();
        ApplyMovement();
    }

    private void OnAnimatorMove()
    {
        // Check if player is moving based on input magnitude
        bool isMoving = Mathf.Sqrt(xInput * xInput + yInput * yInput) > inputThreshold;

        if (isMoving)
        {
            // Update facing direction only when moving
            lastFacingDirection = new Vector2(xInput, yInput).normalized;
            animator.SetFloat("Speed", 1);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }

        // Set animator parameters based on facing direction
        animator.SetFloat("X", lastFacingDirection.x);
        animator.SetFloat("Y", lastFacingDirection.y);
    }

    private void PlayerMove()
    {
        xInput = Input.GetAxis("Horizontal"); // Get horizontal input (A/D or Left/Right arrows)
        yInput = Input.GetAxis("Vertical"); // Get vertical input (W/S or Up/Down arrows)
    }

    private void ApplyMovement()
    {
        Vector2 movement = Vector2.zero;

        if (Mathf.Abs(xInput) > Mathf.Abs(yInput))
        {
            movement.x = xInput; // Move horizontally only
        }
        else if (Mathf.Abs(yInput) > 0.1f)
        {
            movement.y = yInput; // Move vertically only
        }

        rb.linearVelocity = movement * moveForce;
    }
}