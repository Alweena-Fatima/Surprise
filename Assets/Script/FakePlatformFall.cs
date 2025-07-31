using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakePlatformFall : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool hasFallen = false;

    [SerializeField] private float fallDelay = 0.2f;     // Delay before falling
    [SerializeField] private float gravity = 1.0f;        // Custom gravity after falling

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic; // Initially not affected by physics
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasFallen && collision.gameObject.CompareTag("Player"))
        {
            hasFallen = true;
            StartCoroutine(FallAfterDelay());
        }
    }

    private IEnumerator FallAfterDelay()
    {
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;    // Let it fall
        rb.gravityScale = gravity;                // Set custom gravity
    }
}
