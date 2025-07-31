using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagTrigger : MonoBehaviour
{
    public AudioClip flagSound;               // Assign in Inspector
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();  // Get AudioSource from flag
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Play the sound immediately
            if (flagSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(flagSound);
            }

            // Load next scene immediately
            int currentIndex = SceneManager.GetActiveScene().buildIndex;
            int totalScenes = SceneManager.sceneCountInBuildSettings;
            int nextSceneIndex = (currentIndex + 1) % totalScenes;

            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
