using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSoundsSettings : MonoBehaviour {

    int isSoundOn;
    Toggle soundToggle;

    // Use this for initialization
    void Awake()
    {
        soundToggle = GetComponent<Toggle>();

        if (PlayerPrefs.HasKey("Sound"))
        {
            isSoundOn = PlayerPrefs.GetInt("Sound");
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 1);
            isSoundOn = 1;
        }

        if (isSoundOn == 1)
            soundToggle.isOn = true;
        else
            soundToggle.isOn = false;
    }
	
	public void ToggleClicked()
    {
        if(isSoundOn == 0 && soundToggle.isOn)
        {
            isSoundOn = 1;
            PlayerPrefs.SetInt("Sound", 1);
        }
        else if(isSoundOn == 1 && !soundToggle.isOn)
        {
            isSoundOn = 0;
            PlayerPrefs.SetInt("Sound", 0);
        }
    }
}
