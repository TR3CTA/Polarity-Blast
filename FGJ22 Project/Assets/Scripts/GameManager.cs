using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject gameOverScreen;

    private EnemyScriptNeg negEnemyDead;
    private EnemyScriptPos posEnemyDead;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI finalTimeText;

    private int score = 0;
    public int scoreFromKill = 10;

    public bool isTimerOn;
    private float currentTime = 0;

    public bool gameActive;

    // Start is called before the first frame update
    void Start()
    {
        //UpdateScore(score);
        gameActive = true;
        TimerStart();
    }

    // Update is called once per frame
    void Update()
    {
        Timer();

        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().currentHealth <= 0)
        {
            gameActive = false;
        }

        if (!gameActive)
        {
            Time.timeScale = 0f;
        }
    }

    void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "SCORE: " + score;
    }

    void Timer()
    {
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        float milliSeconds = Mathf.FloorToInt(1000 * (currentTime - minutes * 60 - seconds));

        if (isTimerOn)
        {
            currentTime += 1 * Time.deltaTime;
            if (!gameActive)
            {
                TimerEnded();
                Cursor.lockState = CursorLockMode.Confined;
                gameOverScreen.SetActive(true);
                finalTimeText.text = "YOUR FINAL TIME: " + string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliSeconds);
            }
        }

        
        timerText.text = "Time: " + string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliSeconds);
    }

    public void TimerStart()
    {
        isTimerOn = true;
    }

    public void TimerEnded()
    {
        isTimerOn = false;
    }

}
