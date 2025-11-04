using UnityEngine;

public class GameOverManager : MonoBehaviour
{

    public GameObject gameOverUI;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true); // Show the game over UI
        Time.timeScale = 0f; // Pause the game
    }
}
