using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false; // Static variable to track if the game is paused
    public GameObject pauseMenuUI; // Reference to the pause menu UI GameObject

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Check if the Escape key is pressed
        {
            if (GameIsPaused)
            {
                Resume(); // Resume the game if it is currently paused
            }
            else
            {
                Pause(); // Pause the game if it is currently running
            }
        }

    }

    

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Set the time scale to 1 to resume the game
        GameIsPaused = false; // Set the static variable to false to indicate that the game is running
    }
    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Set the time scale to 0 to pause the game
        GameIsPaused = true; // Set the static variable to true to indicate that the game is paused
    }

    public void Restart()
    {
        Time.timeScale = 1f; // Reset time scale before reloading
        SceneManager.LoadScene("GameplayScene"); // Replace with exact scene name
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f; // Reset time scale before loading menu
        SceneManager.LoadScene("Menu Scene"); // Use exact name from build settings
    }



}
