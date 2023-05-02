using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    


    public static bool GameOver;
    public static bool levelWin;

    public GameObject gameOverPannal;
    public GameObject levelWinPannal;

    public static int CurrentLevelIndex;
    public static int noOfPassingRings;
    public static int score = 0;
    public static int score1 = 0;

    public TextMeshProUGUI CurrentLevelText;
    public TextMeshProUGUI nextLevelText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI completedScore;

    public Slider ProgressBar;

    public void Awake()
    {
        CurrentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 1);
    }

    private void Start()
    {
        Time.timeScale = 1f;
        noOfPassingRings = 0;
        GameOver = false;
        levelWin = false;
    } 
    private void Update() 
    {
        CurrentLevelText.text = CurrentLevelIndex.ToString();
        nextLevelText.text = (CurrentLevelIndex + 1).ToString();
        scoreText.text = score.ToString();
        completedScore.text = score1.ToString()+ "% COMPLETED"; 

        int progress = noOfPassingRings * 100 / FindObjectOfType<HelixManager1>().noOfRings;
        ProgressBar.value = progress;
        
        if (GameOver == true)
        {
            Time.timeScale = 0f;
            gameOverPannal.SetActive(true);
            
            if (Input.GetMouseButtonDown(0))
            {
                score = 0;
                score1 = 0;
                SceneManager.LoadScene(0);
            }
        }

        if (levelWin == true)
        {
            levelWinPannal.SetActive(true);
            if (Input.GetMouseButtonDown(0))
            {
                score = 0;
                score1 = 0;
                PlayerPrefs.SetInt("CurrentLevelIndex", CurrentLevelIndex + 1);
                SceneManager.LoadScene(0);
            }
        }
    }


    
    

}
