using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LockedPatterns : MonoBehaviour {

    private DataController dataController;
    private PatternArray patternArray;

    [System.Serializable]
    public struct Patterns
    {
        public GameObject pattern;
        public enum UnlockmentCondition { gamesPlayed, totalScore, memoryScore, logicScore, observationScore, focusScore};
        public UnlockmentCondition Conditions;
        public int NumberToSurpass;
    }

    public Patterns[] lockedPatterns;

    private static LockedPatterns instance;

    // Use this for initialization
    void Start () {

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
        dataController = GameObject.FindObjectOfType<DataController>();
        patternArray = GameObject.FindObjectOfType<PatternArray>();

        CheckForUnlockedPatterns(); // patikrina, kokie specialūs raštai buvo atrakinti
    }

    public void CheckForUnlockedPatterns()
    {
        for (int i = 0; i < lockedPatterns.Length; i++)
        {
            if (PlayerPrefs.HasKey(lockedPatterns[i].pattern.name))
            {
                patternArray.UnlockPattern(lockedPatterns[i].pattern.name);
            }
            else
            {
                patternArray.LockPattern(lockedPatterns[i].pattern.name);
            }
        }
    }

    private List<GameObject> unlocked;

    public void CheckUnlockmentConditions()
    {
        unlocked = new List<GameObject>();
        for (int i = 0; i < lockedPatterns.Length; i++)
        {
            if (!PlayerPrefs.HasKey(lockedPatterns[i].pattern.name)) // elementa turetu atrakinti tada, jei jis dar nebuvo atrakintas
            {
                switch (lockedPatterns[i].Conditions)
                {
                    case Patterns.UnlockmentCondition.gamesPlayed:
                        if (CheckGamesPlayed(i))
                            PlayerPrefs.SetInt(lockedPatterns[i].pattern.name, 1);  // atrakinamas elementas
                        break;
                    case Patterns.UnlockmentCondition.totalScore:
                        if (CheckTotalScore(i))
                            PlayerPrefs.SetInt(lockedPatterns[i].pattern.name, 1);
                        break;
                    case Patterns.UnlockmentCondition.memoryScore:
                        if (CheckMiniGameScore("Memory", i))
                            PlayerPrefs.SetInt(lockedPatterns[i].pattern.name, 1);
                        break;
                    case Patterns.UnlockmentCondition.logicScore:
                        if (CheckMiniGameScore("Logic", i))
                            PlayerPrefs.SetInt(lockedPatterns[i].pattern.name, 1);
                        break;
                    case Patterns.UnlockmentCondition.observationScore:
                        if (CheckMiniGameScore("Observation", i))
                            PlayerPrefs.SetInt(lockedPatterns[i].pattern.name, 1);
                        break;
                    case Patterns.UnlockmentCondition.focusScore:
                        if (CheckMiniGameScore("Focus", i))
                            PlayerPrefs.SetInt(lockedPatterns[i].pattern.name, 1);
                        break;
                }
                if (PlayerPrefs.HasKey(lockedPatterns[i].pattern.name))
                    unlocked.Add(lockedPatterns[i].pattern);
            }
        }
        CheckForUnlockedPatterns();
    }

    public List<GameObject> GetUnlocked()
    {
        return unlocked;
    }

    private bool CheckGamesPlayed(int ind)
    {
        if (lockedPatterns[ind].NumberToSurpass <= dataController.GetTotalGamesPlayed())
            return true;
        return false;
    }

    private bool CheckTotalScore(int ind)
    {
        if (lockedPatterns[ind].NumberToSurpass <= dataController.GetCurrentGameScore())
            return true;
        return false;
    }

    private bool CheckMiniGameScore(string miniGameName, int ind)
    {
        if(miniGameName.Equals("Memory"))
        {
            if (lockedPatterns[ind].NumberToSurpass <= dataController.GetGameResults().memoryScore.GetScore())
                return true;
        }
        else if(miniGameName.Equals("Logic"))
        {
            if (lockedPatterns[ind].NumberToSurpass <= dataController.GetGameResults().logicScore.GetScore())
                return true;
        }
        else if (miniGameName.Equals("Observation"))
        {
            if (lockedPatterns[ind].NumberToSurpass <= dataController.GetGameResults().observationScore.GetScore())
                return true;
        }
        else if (miniGameName.Equals("Focus"))
        {
            if (lockedPatterns[ind].NumberToSurpass <= dataController.GetGameResults().focusScore.GetScore())
                return true;
        }
        return false;
    }
}
