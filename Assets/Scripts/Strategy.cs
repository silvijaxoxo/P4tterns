using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Strategy {

	public abstract void AlgorithmInterface();
    public void EndGame(string Game, Score score)
    {
        GameObject panel = GameObject.Find("Time'sUpPanel");
        if (panel != null)
        {
            GameObject.Find("ScoreText").GetComponent<Text>().text = "Score: " + score.GetScore();
            if(PlayerPrefs.HasKey(Game))
            {
                GameObject.Find("HighscoreText").GetComponent<Text>().text = "Highscore: " + PlayerPrefs.GetInt(Game);
            }
            GameObject.Find("CorrectAnswersText").GetComponent<Text>().text = score.GetCorrectAnswersCount().ToString() + " correct answers";
            GameObject.Find("MistakesText").GetComponent<Text>().text = score.GetMistakesCount().ToString() + " mistakes made";

            AlgorithmInterface();
        }
    }
}

public class ConcreteStrategyGame : Strategy
{
    public override void AlgorithmInterface()
    {
        GameObject panel = GameObject.Find("MiniGamePanel");
        if(panel != null)
            panel.SetActive(false);
    }
}

public class ConcreteStrategyMiniGame : Strategy
{
    public override void AlgorithmInterface()
    {
        GameObject panel = GameObject.Find("GamePanel");
        if (panel != null)
            panel.SetActive(false);
    }
}

public class Context
{
    private Strategy strategy;

    public Context(Strategy strategy)
    {
        this.strategy = strategy;
    }

    public void EndMiniGame(string Game, Score score)
    {
        strategy.EndGame(Game, score);
    }
}
