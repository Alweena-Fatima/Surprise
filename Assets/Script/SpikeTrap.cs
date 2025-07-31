using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SpikeTrap : MonoBehaviour
{
    public LayerMask boxLayer;        // Assign your Box layer in Inspector
    public float checkHeight = 0.2f;  // Height of the box to check below the player

    private void Reset()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (IsBoxBetweenSpikeAndPlayer(other.transform))
            {
                Debug.Log("Box detected between spike and player — safe!");
                return;
            }

            // Kill the player
            Death death = other.GetComponent<Death>();
            if (death != null)
            {
                death.Die();
            }
            else
            {
                Debug.LogWarning("Player has no Death script!");
            }
        }
    }

    private bool IsBoxBetweenSpikeAndPlayer(Transform player)
    {
        float distance = Mathf.Abs(transform.position.y - player.position.y);

        Vector2 checkCenter = new Vector2(player.position.x, (transform.position.y + player.position.y) / 2f);
        Vector2 checkSize = new Vector2(0.8f, distance - 0.1f); // Slightly smaller to avoid edge collision

        // Check for box collider between spike and player
        Collider2D hit = Physics2D.OverlapBox(checkCenter, checkSize, 0f, boxLayer);
        return hit != null;
    }

    private void OnDrawGizmosSelected()
    {
        // Optional: Shows the approximate area being checked (between spike and player)
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + Vector3.up * 0.3f, new Vector2(0.8f, checkHeight));
    }
}
