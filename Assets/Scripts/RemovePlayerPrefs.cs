using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovePlayerPrefs : MonoBehaviour {

    private LockedPatterns lockedPatterns;
    public LoadSceneOnClick loadSceneOnClick;
    private DataController dataController;

    public void RemovePreferences()
    {
        PlayerPrefs.DeleteAll();

        //lockedPatterns = GameObject.FindObjectOfType<LockedPatterns>();
        //lockedPatterns.CheckForUnlockedPatterns();

        //dataController = GameObject.FindObjectOfType<DataController>();
        //dataController.ReloadProgress();
        loadSceneOnClick.LoadByIndex(0);
    }
}
