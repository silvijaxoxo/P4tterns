using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePanelSounds : MonoBehaviour {

    AudioSource audioSource;
    int isSoundOn;

    // Use this for initialization
    void Awake () {
        audioSource = GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("Music"))
            isSoundOn = PlayerPrefs.GetInt("Music");
        else
        {
            PlayerPrefs.SetInt("Music", 1);
            isSoundOn = 1;
        }
        if (isSoundOn == 0)
            audioSource.enabled = false;
        else
            audioSource.enabled = true;
    }
}
