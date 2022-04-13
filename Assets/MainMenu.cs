using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Start()
    {
        //SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame ()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void Play()
    {
        PauseMenu.GameIsPaused = false;
        SceneManager.LoadScene("Gameplay");
    }
}
