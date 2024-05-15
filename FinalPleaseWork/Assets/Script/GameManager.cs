using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    public GameObject deathMenuPanel; // Panel to show when the player dies
    private bool isGamePaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isGamePaused) // Prevent opening the pause menu if the game is already paused by death
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true; // Make the cursor visible
    }

    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
        Cursor.visible = false; // Hide the cursor
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Ensure the game time is resumed
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ResumeGame();  // Ensure game state is set back to unpaused
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // Make sure game time is resumed
        SceneManager.LoadScene("MainMenu");
        ResumeGame();  // Ensure game state is set back to unpaused
    }

 public void PlayerDied()
    {
        deathMenuPanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game
        isGamePaused = true; // Ensure that the pause state is consistent
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true; // Make the cursor visible
    }



}
