using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {
    
    //AudioSettings audioSettings;

    public void LoadByIndex(int sceneIndex)
    {
        StartCoroutine(LoadWithFade(sceneIndex));
    }

    IEnumerator LoadWithFade(int sceneIndex)
    {
        float fadeTime = GameObject.Find("Fade").GetComponent<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);

        SceneManager.LoadScene(sceneIndex);
    }
    /*
    IEnumerator DelaySceneLoad(int index, AudioClip audioClip)
    {
        yield return new WaitForSeconds(audioClip.length);
        LoadByIndex(index);
    }

    public void LoadScene(int sceneIndex)
    {
        //audioSettings = GameObject.FindObjectOfType<AudioSettings>();
        //StartCoroutine(DelaySceneLoad(sceneIndex, audioSettings.GetClick().clip));
        //audioSettings.PlayClick();
    }*/
}
