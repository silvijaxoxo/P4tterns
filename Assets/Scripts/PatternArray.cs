using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PatternArray : MonoBehaviour{

    [System.Serializable]
    public struct patternData
    {
        public GameObject pattern;
        public bool unlockedByDefault;
        public bool unlocked;
    }

    public patternData[] patterns;

    public List<GameObject> GetUnlockedPatternList()
    {
        List<GameObject> unlockedPatterns = new List<GameObject>();
        for(int i = 0; i<patterns.Length; i++)
        {
            if(patterns[i].unlockedByDefault == true)
            {
                unlockedPatterns.Add(patterns[i].pattern);
            }
            else
            {
                if(patterns[i].unlocked == true)
                {
                    unlockedPatterns.Add(patterns[i].pattern);
                }
            }
        }
        return unlockedPatterns;
    }

    public void UnlockPattern(string patternName)
    {
        for (int i = 0; i < patterns.Length; i++)
        {
            if((patterns[i].unlockedByDefault == false) && (patterns[i].pattern.name.Equals(patternName)))
            {
                patterns[i].unlocked = true;
                break;
            }
        }
    }

    public void LockPattern(string patternName)
    {
        for (int i = 0; i < patterns.Length; i++)
        {
            if ((patterns[i].unlockedByDefault == false) && (patterns[i].pattern.name.Equals(patternName)))
            {
                patterns[i].unlocked = false;
                break;
            }
        }
    }

    private static PatternArray instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
}
