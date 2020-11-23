using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOfPatterns {

    public static List<GameObject> CreateListOfPatterns(int x)
    {
        PatternArray patternArray = MonoBehaviour.FindObjectOfType<PatternArray>();
        List<GameObject> patternList = patternArray.GetUnlockedPatternList();

        patternList = ShuffleList(patternList); // išmaišomi visi raštai

        List<GameObject> patternsOfX = new List<GameObject>();

        for (int i = 0; i < x; i++)
        {
            patternsOfX.Add(patternList[i]);
        }
        return patternsOfX;
    }

    private static List<GameObject> ShuffleList(List<GameObject> inputList)
    {
        List<GameObject> randomList = new List<GameObject>();

        int randomIndex = 0;
        while (inputList.Count > 0)
        {
            randomIndex = Random.Range(0, inputList.Count); //Choose a random object in the list
            randomList.Add(inputList[randomIndex]); //add it to the new, random list
            inputList.RemoveAt(randomIndex); //remove to avoid duplicates
        }

        return randomList; //return the new random list
    }
}
