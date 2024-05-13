using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject levelsPanel; // Reference to the Levels Canvas

    public void PlayGame()
    {
        SceneManager.LoadScene("Level1"); // Loads the first level directly
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // General method to load a scene by name
    }

    public void ToggleLevelsPanel(bool show)
    {
        levelsPanel.SetActive(show);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void HideLevelsPanel()
    {
        levelsPanel.SetActive(false);
    }
}
