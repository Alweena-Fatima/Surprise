using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTile : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float fallGravity = 1f; // You can set this per tile in Inspector

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Initially no gravity
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            rb.gravityScale = fallGravity; // Apply custom gravity
        }
    }
}
