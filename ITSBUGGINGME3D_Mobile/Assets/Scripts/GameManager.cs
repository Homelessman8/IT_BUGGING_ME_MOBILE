using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Variables
    [Header("Game Entities")]
    [SerializeField] private GameObject enemyPrefab0;
    [SerializeField] private GameObject enemyPrefab1;
    [SerializeField] private GameObject enemyPrefab2;
    [SerializeField] private Transform[] spawnPositions;


    [Header("Game Variables")]
    [SerializeField] private float enemySpawnRate;


    /// <summary>
    /// Singleton : any script could access it anywhere and anytime.
    /// </summary>
    private static GameManager instance;

    [SerializeField]
    //private LevelManager[] levelManagers;

    private State currentState;
    //private LevelManager currentLevel;
    //private int currentLevelIndex;

    //public GameObject player;
    public GameObject bug;
    public GameObject gamePauseMenu;
    public GameObject gameOverMenu;
    //public GameObject endgameUI;  // Reference to the endgame UI GameObject.
    //public GameObject healthBar;
    //public GameObject killCount;
    public TextMeshProUGUI timerText;

    public static GameManager GetInstance()
    {
        return instance;
    }

    void SetSingleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }

        instance = this;
    }

    //------------------------------

    private void Awake()
    {
        SetSingleton();
    }



    //public void Change state method
    public void ChangeState(State newState)
    {
        currentState = newState;
        //currentLevel = level;

        switch (currentState)
        {
            case State.StartGame:
                StartGame();
                break;
            case State.InitiateLevel:
                InitiateLevel();
                break;
            case State.RunLevel:
                RunLevel();
                break;
            //case State.CompleteLevel:
            //    CompleteLevel();
            //    break;
            case State.Pause:
                Pause();
                break;
            case State.UnPause:
                UnPause();
                break;
            case State.GameOver:
                GameOver();
                break;
            case State.GameOverExit:
                GameOverExit();
                break;



        }
    }

    //--------------

    //Methods needed


    private void StartGame()
    {
        Debug.Log("Start Game");
        ChangeState(State.InitiateLevel);
    }

    //Spawn Enemy Randomly around the environment
    private void CreateEnemy()
    {
        //Cockroach
        bug = Instantiate(enemyPrefab0);
        bug.transform.position = spawnPositions[UnityEngine.Random.Range(0, spawnPositions.Length)].position;
        

        //Spider
        bug = Instantiate(enemyPrefab1);
        bug.transform.position = spawnPositions[UnityEngine.Random.Range(0, spawnPositions.Length)].position;


        //another bug
        bug = Instantiate(enemyPrefab2);
        bug.transform.position = spawnPositions[UnityEngine.Random.Range(0, spawnPositions.Length)].position;
    }

    private void Update()
    {
        //// Testing of random enemy creation
        if (Input.GetKeyDown(KeyCode.X))
        {
            CreateEnemy();
        }
    }

    private void InitiateLevel()
    {
        Debug.Log("Start Level");
        ChangeState(State.RunLevel);
    }

    private void RunLevel()
    {
        Debug.Log("In Level");
    }

    public void Pause()
    {
        Debug.Log("Paused");
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;

        //Add setActive.true pause menu panel
        gamePauseMenu.SetActive(true);

    }


    public void UnPause()
    {
        Debug.Log("Back to Game");
        //Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        gamePauseMenu.SetActive(false);
        //Add setActive.false pause menu panel
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        timerText.gameObject.SetActive(true);
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        //SetActive.true 
        Debug.Log("You Lose, Game Over");
        ChangeState(State.GameOverExit);
        Time.timeScale = 0;
    }

    private void GameOverExit()
    {
        Cursor.lockState = CursorLockMode.None;
        gameOverMenu.SetActive(true);
        timerText.gameObject.SetActive(false);
        gamePauseMenu = null;
    }

    public void ExitMenu(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1;
        gameOverMenu.SetActive(false);
        gamePauseMenu.SetActive(false);
    }

    public enum State
    {
        StartGame,
        InitiateLevel,
        RunLevel,
        CompleteLevel,
        Pause,
        UnPause,
        RestartLevel,
        GameOver,
        GameOverExit,
        ExitMenu
    }
}
