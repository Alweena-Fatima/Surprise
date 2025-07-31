using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private bool isDead = false;
    private float shrinkSpeed = 3f;

    [SerializeField] private float fallLimitY = -10f;

    [Header("Death Effects")]
    [SerializeField] private GameObject deathParticlePrefab; // Particle prefab
    [SerializeField] private AudioClip deathSound; // Optional sound
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Fall detection
        if (!isDead && transform.position.y < fallLimitY)
        {
            Die();
        }

        // Shrinking effect
        if (isDead && transform.localScale.x > 0f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime * shrinkSpeed);
        }
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;

        // 💥 Instantiate particle effect
        if (deathParticlePrefab != null)
        {
            Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        }

        // 🔊 Play death sound
        if (deathSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        // 🧊 Disable movement + collision
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GetComponent<Collider2D>().enabled = false;

        // 🔁 Restart after delay
        Invoke("RestartLevel", 2f);
    }

    private void RestartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }
}
