using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScoresPanel : MonoBehaviour {

    public Text totalGamesPlayed;
    public Text highscore;
    public Text average;

    public Text memory;
    public Text logic;
    public Text observation;
    public Text focus;

    private DataController dataController;

    // Use this for initialization
    void Start () {
        dataController = GameObject.FindObjectOfType<DataController>();

        totalGamesPlayed.text = "Played: " + dataController.GetTotalGamesPlayed();
        highscore.text = "Best: " + dataController.GetHighscore();
        average.text = "Avg: " + dataController.GetAverage();

        memory.text = "Memory: " + dataController.GetMemoryHighscore();
        logic.text = "Logic: " + dataController.GetLogicHighscore();
        observation.text = "Observation: " + dataController.GetObservationHighscore();
        focus.text = "Focus: " + dataController.GetFocusingHighscore();
    }
}
