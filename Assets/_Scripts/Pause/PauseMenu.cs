using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour { 

    public static bool GameisPaused = false;
    public GameObject pauseMenuUI;
   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameisPaused)
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
        Time.timeScale = 1.0f;
        GameisPaused = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameisPaused = true;
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Quit()
    {
        Debug.Log("Saiu do jogo");
        Application.Quit();
    }
}
