using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combo : MonoBehaviour {

    int combo;
    Text comboText;

    // Use this for initialization
    void Start () {
        comboText = GetComponent<Text>();
        comboText.text = "Combo ○○○○○";
        combo = 0;
	}

    public void AddCombo()
    {
        if(combo < 5)
        {
            string bubbleEmpty = "";
            string bubbleFull = "";
            combo++;

            for (int i = 0; i < 5 - combo; i++)
            {
                bubbleEmpty += "○";
            }

            for (int i = 0; i < combo; i++)
            {
                bubbleFull += "●";
            }

            comboText.text = "Combo " + bubbleEmpty + bubbleFull;
        }
    }

    public void RemoveCombo()
    {
        comboText.text = "Combo ○○○○○";
        combo = 0;
    }

    public int GetCombo()
    {
        return combo;
    }
}
