﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadMusicSettings : MonoBehaviour {

    int isSoundOn;
    Toggle soundToggle;

    // Use this for initialization
    void Awake()
    {
        soundToggle = GetComponent<Toggle>();

        if (PlayerPrefs.HasKey("Music"))
        {
            isSoundOn = PlayerPrefs.GetInt("Music");
        }
        else
        {
            PlayerPrefs.SetInt("Music", 1);
            isSoundOn = 1;
        }

        if (isSoundOn == 1)
            soundToggle.isOn = true;
        else
            soundToggle.isOn = false;
    }

    public void ToggleClicked()
    {
        if (isSoundOn == 0 && soundToggle.isOn)
        {
            isSoundOn = 1;
            PlayerPrefs.SetInt("Music", 1);
        }
        else if (isSoundOn == 1 && !soundToggle.isOn)
        {
            isSoundOn = 0;
            PlayerPrefs.SetInt("Music", 0);
        }
    }
}
