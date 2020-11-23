using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindDataController : MonoBehaviour {

    private DataController dataController;

    // Use this for initialization
    void Start () {
        dataController = GameObject.FindObjectOfType<DataController>();
    }

    public void PlayMainGame()
    {
        dataController.PlayMainGame();
    }

    public void PlayMiniGame()
    {
        dataController.PlayMiniGame();
    }
}
