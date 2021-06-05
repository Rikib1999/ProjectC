using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool GameIsOver = false;
    public GameObject pauseMenuUI;
    public GameObject gameOverMenuUI;
    private int currentScene;
    public TextMeshProUGUI highScorePause;
    public TextMeshProUGUI highScoreGameOver;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsOver == false)
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
    }

    public void Resume()
    {
        Cursor.visible = false;
        highScorePause.gameObject.SetActive(false);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        if (GameObject.FindGameObjectWithTag("Player 1") != null)
        {
            GameObject.FindGameObjectWithTag("Player 1").GetComponent<Player_health>().CanFirePause();
        }
        if (GameObject.FindGameObjectWithTag("Player 2") != null)
        {
            GameObject.FindGameObjectWithTag("Player 2").GetComponent<Player_health>().CanFirePause();
        }
    }

    void Pause()
    {
        highScorePause.text = "HIGH SCORE: " + PlayerPrefs.GetFloat("HighScore", 0).ToString("0");
        highScorePause.gameObject.SetActive(true);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        if (GameObject.FindGameObjectWithTag("Player 1") != null)
        {
            GameObject.FindGameObjectWithTag("Player 1").GetComponent<Player_health>().CantFirePause();
        }
        if (GameObject.FindGameObjectWithTag("Player 2") != null)
        {
            GameObject.FindGameObjectWithTag("Player 2").GetComponent<Player_health>().CantFirePause();
        }
    }

    public void LoadMenu()
    {
        GameIsOver = false;
        GameIsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void GameOverMenu()
    {
        highScoreGameOver.text = "HIGH SCORE: " + PlayerPrefs.GetFloat("HighScore", 0).ToString("0");
        highScoreGameOver.gameObject.SetActive(true);
        gameOverMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsOver = true;
    }

    public void Retry()
    {
        Cursor.visible = false;
        GameIsOver = false;
        GameIsPaused = false;
        Time.timeScale = 1f;
        currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
