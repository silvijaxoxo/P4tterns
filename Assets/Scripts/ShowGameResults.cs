using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowGameResults : MonoBehaviour {

    public Text score;
    public Text highscore;

    public Text memory;
    public Text logic;
    public Text observation;
    public Text focus;

    private DataController dataController;
    private LockedPatterns lockedPatterns;

    public GameObject panel;
    private GameObject mask;

    void Start()
    {
        dataController = GameObject.FindObjectOfType<DataController>();
        dataController.SubmitNewPlayerScore();

        score.text = dataController.GetCurrentGameScore().ToString();
        highscore.text = "Best: " + dataController.GetHighscore();

        memory.text = "Memory: " + dataController.GetGameResults().memoryScore.GetScore();
        logic.text = "Logic: " + dataController.GetGameResults().logicScore.GetScore();
        observation.text = "Observation: " + dataController.GetGameResults().observationScore.GetScore();
        focus.text = "Focus: " + dataController.GetGameResults().focusScore.GetScore();

        lockedPatterns = GameObject.FindObjectOfType<LockedPatterns>();
        lockedPatterns.CheckUnlockmentConditions(); // atrakina rastus

        List<GameObject> unlocked = lockedPatterns.GetUnlocked();
        if (unlocked != null)
        {
            panel.gameObject.SetActive(true);
            mask = GameObject.Find("UnlockedPatterns");

            foreach (GameObject obj in unlocked)
            {
                Sprite spr = obj.GetComponent<SpriteRenderer>().sprite;
                
                GameObject temp = Instantiate(new GameObject());
                temp.transform.SetParent(mask.transform);
                temp.transform.localScale = new Vector3(1f, 1f, 1f);
                
                Image im = temp.gameObject.AddComponent<Image>();
                im.sprite = spr;
            }
        }
    }
}
