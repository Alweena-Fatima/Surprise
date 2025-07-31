using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SpikeHeadMovement : MonoBehaviour
{
    public float horizontalSpeed = 5f;   // Controls left-right movement
    public float dropSpeed = 100f;       // Controls vertical drop speed
    public float dropDistance = 3f;      // How much it drops down
    public float waitTime = 0f;        // Optional pause before dropping

    private float minX = 14f;
    private float maxX = 27f;
    private Vector3 startPos;
    private bool isDropping = false;
    private bool isRising = false;
    private float dropStartY;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (!isDropping && !isRising)
        {
            // Move back and forth on X using PingPong
            float x = Mathf.PingPong(Time.time * horizontalSpeed, maxX - minX) + minX;
            transform.position = new Vector3(x, startPos.y, startPos.z);

            // Drop when reaching either end
            if (Mathf.Abs(x - minX) < 0.05f || Mathf.Abs(x - maxX) < 0.05f)
            {
                StartCoroutine(DropAndRise());
            }
        }

        if (isDropping)
        {
            transform.position += Vector3.down * dropSpeed * Time.deltaTime;
            if (transform.position.y <= dropStartY - dropDistance)
            {
                isDropping = false;
                isRising = true;
            }
        }

        if (isRising)
        {
            transform.position += Vector3.up * dropSpeed * Time.deltaTime;
            if (transform.position.y >= startPos.y)
            {
                transform.position = new Vector3(transform.position.x, startPos.y, startPos.z);
                isRising = false;
            }
        }
    }

    IEnumerator DropAndRise()
    {
        yield return new WaitForSeconds(waitTime);
        dropStartY = transform.position.y;
        isDropping = true;
    }
}

