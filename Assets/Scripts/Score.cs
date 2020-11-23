using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    int score;
    Text scoreText;

    int correctAnswersCount;
    int mistakesCount;
    
    public AudioSource audioCorrect;
    public AudioSource audioMistake;

    int isSoundOn;

    private void Awake()
    {
        if(PlayerPrefs.HasKey("Sound"))
        {
            isSoundOn = PlayerPrefs.GetInt("Sound");
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 1);
            isSoundOn = 1;
        }
    }

    // Use this for initialization
    void Start () {

        scoreText = GetComponent<Text>();
        scoreText.text = "Score: 0";
        score = 0;
        correctAnswersCount = 0;
        mistakesCount = 0;
    }

    public void Correct(int points)
    {
        if(isSoundOn != 0)
        {
            if (audioCorrect != null)
                audioCorrect.Play();
        }
        score += points;
        correctAnswersCount++;
        scoreText.text = "Score: " + score;
    }

    public void Mistake(int points)
    {
        if (isSoundOn != 0)
        {
            if (audioMistake != null)
                audioMistake.Play();
        }
        mistakesCount++;
        score -= points;

        if (score < 0)
            score = 0;
        scoreText.text = "Score: " + score;
    }

    public int GetScore()
    {
        return score;
    }

    public int GetCorrectAnswersCount()
    {
        return correctAnswersCount;
    }

    public int GetMistakesCount()
    {
        return mistakesCount;
    }
}
