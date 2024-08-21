using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        
        Time.timeScale = 1;
    }

    public void OptionsMenu()
    {
        //Add options and button map for player info
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}