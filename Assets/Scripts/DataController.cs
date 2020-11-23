using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataController : MonoBehaviour {

    private PlayerProgress playerProgress;
    private Game game;
    private Context context;
    private int currentScore;

    private static DataController instance;

    // Use this for initialization
    void Awake()
    {
        if(!instance)
        {
            instance = this as DataController;
        }
        else
        {
            DestroyObject(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        LoadPlayerProgress();
    }

    public void ReloadProgress()
    {
        LoadPlayerProgress();
    }
    /*void Start () {

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
        LoadPlayerProgress();
    }*/

    public void PlayMainGame() // "play game" mygtukas paleis sita metodas
    {
        game = new Game();
        context = new Context (new ConcreteStrategyGame());
        Debug.Log("Game");
    }

    public void PlayMiniGame() // play logic, memory... mini-game paleis sita metoda
    {
        game = new Game();
        context = new Context(new ConcreteStrategyMiniGame());
        Debug.Log("Mini");
    }

    public void SaveMemoryGameScore(Score score)
    {
        game.memoryScore = score;

        if (score.GetScore() > playerProgress.memoryHighscore)
        {
            playerProgress.memoryHighscore = score.GetScore();
            PlayerPrefs.SetInt("Memory", playerProgress.memoryHighscore);
        }

        context.EndMiniGame("Memory", game.memoryScore);
    }

    public void SaveLogicGameScore(Score score)
    {
        game.logicScore = score;

        if (score.GetScore() > playerProgress.logicHighscore)
        {
            playerProgress.logicHighscore = score.GetScore();
            PlayerPrefs.SetInt("Logic", playerProgress.logicHighscore);
        }

        context.EndMiniGame("Logic", game.logicScore);
    }

    public void SaveObservationGameScore(Score score)
    {
        game.observationScore = score;

        if (score.GetScore() > playerProgress.observationHighscore)
        {
            playerProgress.observationHighscore = score.GetScore();
            PlayerPrefs.SetInt("Observation", playerProgress.observationHighscore);
        }

        context.EndMiniGame("Observation", game.observationScore);
    }

    public void SaveFocusingGameScore(Score score)
    {
        game.focusScore = score;

        if (score.GetScore() > playerProgress.focusHighscore)
        {
            playerProgress.focusHighscore = score.GetScore();
            PlayerPrefs.SetInt("Focus", playerProgress.focusHighscore);
        }

        context.EndMiniGame("Focus", game.focusScore);
    }

    public void SubmitNewPlayerScore()
    {
        int newScore = game.memoryScore.GetScore() + game.logicScore.GetScore() + game.observationScore.GetScore() + game.focusScore.GetScore();

        playerProgress.average = (playerProgress.totalGamesPlayed * playerProgress.average + newScore) / (playerProgress.totalGamesPlayed + 1);
        playerProgress.totalGamesPlayed += 1;
        if (newScore > playerProgress.highscore)
            playerProgress.highscore = newScore;
        
        currentScore = newScore;

        SavePlayerProgress();
    }

    private void LoadPlayerProgress()
    {
        playerProgress = new PlayerProgress();

        if (PlayerPrefs.HasKey("Total Games Played"))
        {
            playerProgress.totalGamesPlayed = PlayerPrefs.GetInt("Total Games Played");
        }
        if (PlayerPrefs.HasKey("Highscores"))
        {
            playerProgress.highscore = PlayerPrefs.GetInt("Highscores");
        }

        if (PlayerPrefs.HasKey("Average Score"))
        {
            playerProgress.average = PlayerPrefs.GetFloat("Average Score");
        }

        //uzkraunami ir kiti scores
        if (PlayerPrefs.HasKey("Memory"))
        {
            playerProgress.memoryHighscore = PlayerPrefs.GetInt("Memory");
        }
        if (PlayerPrefs.HasKey("Logic"))
        {
            playerProgress.logicHighscore = PlayerPrefs.GetInt("Logic");
        }
        if (PlayerPrefs.HasKey("Observation"))
        {
            playerProgress.observationHighscore = PlayerPrefs.GetInt("Observation");
        }
        if (PlayerPrefs.HasKey("Focus"))
        {
            playerProgress.focusHighscore = PlayerPrefs.GetInt("Focus");
        }
    }

    private void SavePlayerProgress()
    {
        PlayerPrefs.SetInt("Total Games Played", playerProgress.totalGamesPlayed);
        PlayerPrefs.SetInt("Highscores", playerProgress.highscore);
        PlayerPrefs.SetFloat("Average Score", playerProgress.average);
    }

    public int GetCurrentGameScore()
    {
        return currentScore;
    }

    public Game GetGameResults()
    {
        return game;
    }

    public int GetTotalGamesPlayed()
    {
        return playerProgress.totalGamesPlayed;
    }
   
    public int GetHighscore()
    {
        return playerProgress.highscore;
    }

    public double GetAverage()
    {
        return playerProgress.average;
    }

    public int GetMemoryHighscore()
    {
        return playerProgress.memoryHighscore;
    }

    public int GetLogicHighscore()
    {
        return playerProgress.logicHighscore;
    }

    public int GetObservationHighscore()
    {
        return playerProgress.observationHighscore;
    }

    public int GetFocusingHighscore()
    {
        return playerProgress.focusHighscore;
    }
}
