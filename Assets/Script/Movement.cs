using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    private Rigidbody2D rb;
    private bool isGrounded = false;

    public LayerMask groundLayer; // Assign in Inspector
    public Transform groundCheck; // Empty GameObject under player

    private SpriteRenderer spriteRenderer;
    private Vector3 originalScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Horizontal movement
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Flip sprite left/right
        if (moveInput > 0.01f)
            spriteRenderer.flipX = false;
        else if (moveInput < -0.01f)
            spriteRenderer.flipX = true;

        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        // Jump input
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            // Change color randomly on jump
            spriteRenderer.color = new Color(Random.value, Random.value, Random.value);

            // Initial jump stretch
            transform.localScale = new Vector3(originalScale.x * 0.9f, originalScale.y * 1.2f, originalScale.z);
        }

        // Apply squash & stretch depending on state
        if (!isGrounded)
        {
            // In air = stretch
            transform.localScale = Vector3.Lerp(transform.localScale,
                new Vector3(originalScale.x * 0.9f, originalScale.y * 1.2f, originalScale.z),
                Time.deltaTime * 8f);
        }
        else if (Mathf.Abs(moveInput) > 0.01f)
        {
            // Running on ground = slight squash
            transform.localScale = Vector3.Lerp(transform.localScale,
                new Vector3(originalScale.x * 1.1f, originalScale.y * 0.95f, originalScale.z),
                Time.deltaTime * 8f);
        }
        else
        {
            // Idle on ground = normal scale
            transform.localScale = Vector3.Lerp(transform.localScale,
                originalScale,
                Time.deltaTime * 8f);
        }

        // Debug info
        Debug.Log("Is Grounded: " + isGrounded);
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, 0.2f);
        }
    }
}
