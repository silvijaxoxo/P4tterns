using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitOnClick : MonoBehaviour {
    
    //AudioSettings audioSettings;

    public void OnApplicationQuit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit ();
        #endif
    }

    IEnumerator DelayExit(AudioClip audioClip)
    {
        yield return new WaitForSeconds(audioClip.length);
        OnApplicationQuit();
    }

    public void Exit()
    {
        //audioSettings = GameObject.FindObjectOfType<AudioSettings>();
        //audioSettings.PlayClick();
        //StartCoroutine(DelayExit(audioSettings.GetClick().clip));

    }
}
