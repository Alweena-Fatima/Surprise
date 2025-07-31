using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeOscillator : MonoBehaviour
{
    public float swingAngle = 45f;  // Max angle (left & right)
    public float speed = 2f;        // Swinging speed

    private Quaternion startRotation;

    void Start()
    {
        startRotation = transform.localRotation;
    }

    void Update()
    {
        float angle = Mathf.Sin(Time.time * speed) * swingAngle;
        transform.localRotation = startRotation * Quaternion.Euler(0f, 0f, angle);
    }
}
