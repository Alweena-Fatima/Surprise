using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerupperTile : MonoBehaviour
{
    private TileFall parentTile;

    private void Start()
    {
        parentTile = GetComponentInParent<TileFall>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && parentTile != null)
        {
            parentTile.TriggerFall();
        }
    }
}

