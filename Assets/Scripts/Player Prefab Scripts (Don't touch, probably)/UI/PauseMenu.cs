using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        GameIsPaused = false;

        // Find and enable PlayerMovement
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.SetEnableMovement(true);
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        GameIsPaused = true;

        // Find and disable PlayerMovement
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.SetEnableMovement(false);
        }
    }

    public void LoadMenu()
    {
        Debug.Log("Menu opened");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
    }
}
