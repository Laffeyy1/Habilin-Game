using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public DemoLoadScene scene;
    public void RetryLevel()
    {
        // Reload the current scene to retry the level
        Scene currentScene = SceneManager.GetActiveScene();
        scene.LoadScene(currentScene.name);
    }

    // Method for the "Back to Lobby" button
    public void BackToLobby()
    {
        scene.LoadScene("Lobby");
        GameObject Keep = GameObject.FindGameObjectWithTag("Keep");
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Destroy(Keep);
        Destroy(player);
    }
    
    // Method for the "Main Menu" button
    public void ReturnToMainMenu()
    {
        // Load the main menu scene
        scene.LoadScene("Main Menu");
    }
}
