using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileFall : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool hasFallen = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 10f; // ⬅️ Increase this value to fall faster (default is 1)
    }

    public void TriggerFall()
    {
        if (hasFallen) return;

        hasFallen = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Death deathScript = collision.collider.GetComponent<Death>();
            if (deathScript != null)
            {
                Debug.Log("Player Died!");
                deathScript.Die();
            }
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity = Vector2.zero;
        }
    }
}
