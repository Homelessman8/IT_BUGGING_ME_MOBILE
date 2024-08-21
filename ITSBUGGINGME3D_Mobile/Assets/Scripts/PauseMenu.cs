using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameManager gameManager;


    void Update()
    {
        // Check if the 'Escape' key is pressed.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //isPaused = true;
            gameManager.Pause();
        }
    }
}