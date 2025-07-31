using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level1"); // Make sure Level1 is added in Build Settings
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit(); // Won’t work in Editor
    }
}

