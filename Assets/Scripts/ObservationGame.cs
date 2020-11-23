using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObservationGame : TemplateMethod {

    public Timer timer;
    public Score score;
    public Combo combo;
    public GameObject panel;

    GameObject correctPtrn;

    DataController dataController;
    bool updateOn;

    public override void ChangePattern()
    {
        ptrn.transform.rotation = rot.GetRotationXY(x, y, 90);
        ptrn.tag = "Incorrect";
        patternGrid.Add(ptrn, x, y);

        correctPtrn = ptrn;
        correctPtrn.transform.rotation = rot.GetRotationXY(x, y, 0);
    }

    // Use this for initialization
    void Start () {
        dataController = GameObject.FindObjectOfType<DataController>();
        PatternArray patternArray = FindObjectOfType<PatternArray>();
        patternList = patternArray.GetUnlockedPatternList();

        patternGrid = new Grid(4, 4, 1.9);

        updateOn = false;
        StartCoroutine(GetReady());
        //CreatePattern();
        //timer.ResumeTimer();
	}

    public Text countDownText;
    public GameObject countDownBackground;

    IEnumerator GetReady()
    {
        countDownText.text = "3";
        yield return new WaitForSeconds(1.5f);

        countDownText.text = "2";
        yield return new WaitForSeconds(1f);

        countDownText.text = "1";
        yield return new WaitForSeconds(1f);

        countDownBackground.SetActive(false);
        countDownText.gameObject.SetActive(false);

        updateOn = true;

        CreatePattern();
        timer.ResumeTimer();
    }

    // Update is called once per frame
    void Update () {
        if (updateOn)
        {
            if (timer.IsTimerRunning())
            {
                if (Input.GetMouseButtonDown(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began))
                {
                    Vector3 touchPosWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
                    RaycastHit2D hit = Physics2D.Raycast(touchPosWorld2D, Vector2.zero, 0f);

                    if (hit)
                    {
                        if (hit.collider.CompareTag("Incorrect"))
                        {
                            StartCoroutine("SpawnNext");
                            CorrectPattern(hit, correctPtrn);
                            combo.AddCombo();
                            score.Correct(10 * combo.GetCombo());
                        }
                        else
                        {
                            combo.RemoveCombo();
                            score.Mistake(5);
                            timer.TakeAwayTime();
                        }
                    }
                }
            }
            else if (timer.IsGameOver())
            {
                updateOn = false;
                panel.SetActive(true);
                dataController.SaveObservationGameScore(score);
            }
        }
    }

    IEnumerator SpawnNext()
    {
        yield return new WaitForSeconds(0.5f);
        CreatePattern();
    }
}