using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{   
    public string newScene;
    // Start is called before the first frame update
    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {   
        if(AudioManager.instance.IsPlaying("musica menu"))
        {
            AudioManager.instance.StopSound("musica menu");
        }

        SceneManager.LoadScene(newScene);
    }

    private void Start() 
    {   
        if (SceneManager.GetActiveScene().name == "MainMenu")
        { 
            AudioManager.instance.PlaySound("musica menu");
        }
    }
}
