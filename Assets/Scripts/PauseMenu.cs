using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool bGameIsPaused = false;
    
    public GameObject pauseMenuUI;
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (bGameIsPaused)
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
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        bGameIsPaused = false;
    }
    
    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        bGameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(3);
    }

    public void EndScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(3);
    }
}
