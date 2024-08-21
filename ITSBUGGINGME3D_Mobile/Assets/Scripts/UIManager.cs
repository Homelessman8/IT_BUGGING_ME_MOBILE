using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //Variables

    [Header("Menu")]
    [SerializeField] private GameObject MenuCanvas;
    [SerializeField] private GameObject LblGameOver;
    [SerializeField] private TMP_Text txtMenuHighScore;


    [Header("Gameplay")]
    [SerializeField] private TMP_Text txtHealth;
    [SerializeField] private TMP_Text txtScore;
    [SerializeField] private TMP_Text txtHighScore;


    
    private ScoreManager scoreManager;

    private void Start()
    {
        

    }

    public void UpdateHealth()
    {

    }

    public void UpdateScore() 
    {
    

    }

    public void UPdateHighScore()
    {

    }

    public void GameStarted()
    {

    }

    public void GameOver()
    {

    }


}
