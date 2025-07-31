using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHeadKillZone : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Death death = other.GetComponent<Death>();
            if (death != null)
            {
                death.Die();
            }
            else
            {
                Debug.Log("Death script not found on Player!");
            }
        }
    }

}
