using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockmentCondition : MonoBehaviour {

    public LockedPatterns lockedPatterns;
    public Text condition;
    public int PatternIndex;

    private void Start()
    {
        if (PlayerPrefs.HasKey(lockedPatterns.lockedPatterns[PatternIndex].pattern.name))
            this.gameObject.SetActive(false);
    }

    public void ShowUnlockmentCondition()
    {
        switch(lockedPatterns.lockedPatterns[PatternIndex].Conditions)
        {
            case LockedPatterns.Patterns.UnlockmentCondition.gamesPlayed:
                condition.text = "Play game " + lockedPatterns.lockedPatterns[PatternIndex].NumberToSurpass + " times";
                break;
            case LockedPatterns.Patterns.UnlockmentCondition.totalScore:
                condition.text = "Score " + lockedPatterns.lockedPatterns[PatternIndex].NumberToSurpass + " in a game";
                break;
            case LockedPatterns.Patterns.UnlockmentCondition.memoryScore:
                condition.text = "Score " + lockedPatterns.lockedPatterns[PatternIndex].NumberToSurpass + " in a memory mini-game";
                break;
            case LockedPatterns.Patterns.UnlockmentCondition.logicScore:
                condition.text = "Score " + lockedPatterns.lockedPatterns[PatternIndex].NumberToSurpass + " in a logic mini-game";
                break;
            case LockedPatterns.Patterns.UnlockmentCondition.observationScore:
                condition.text = "Score " + lockedPatterns.lockedPatterns[PatternIndex].NumberToSurpass + " in an observation mini-game";
                break;
            case LockedPatterns.Patterns.UnlockmentCondition.focusScore:
                condition.text = "Score " + lockedPatterns.lockedPatterns[PatternIndex].NumberToSurpass + " in a focus mini-game";
                break;
        }
    }
}
