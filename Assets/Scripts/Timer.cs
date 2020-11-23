using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    
    float timeLeft;
    bool timerRunning;
    Text countdownText;
    //public GameObject panel;
    bool gameOver;
    // Use this for initialization
    void Start () {

        countdownText = GetComponent<Text>();
        countdownText.text = "Time 60s";
        timeLeft = 60;
        gameOver = false;
        //StartCoroutine("LoseTime");
	}
	
	// Update is called once per frame
	void Update () {
        countdownText.text = "Time " + timeLeft + "s";
        if (timeLeft <= 0)
        {
            StopCoroutine("LoseTime");
            timerRunning = false;
            countdownText.text = "Time's up!";
            gameOver = true;
            //panel.SetActive(true);
        }
    }

    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
            //timerRunning = true;
        }
    }

    public void PauseTimer()
    {
        StopCoroutine("LoseTime");
        timerRunning = false;
    }

    public void ResumeTimer()
    {
        StartCoroutine("LoseTime");
        timerRunning = true;
    }

    public void TakeAwayTime()
    {
        timeLeft--;
        countdownText.text = "Time " + timeLeft + "s";
    }

    public bool IsTimerRunning()
    {
        return timerRunning;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }
}
