using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicGame : TemplateMethod {

    public Timer timer;
    public Score score;
    public Combo combo;
    public GameObject panel;

    GameObject correctPtrn;
    DataController dataController;

    bool updateOn = true;
    bool firstTry = true;

    public Text countDownText;
    public GameObject countDownBackground;

    public override void ChangePattern()
    {
        patternGrid.Delete(x, y);
    }

    private void ShowPossibleAnswers()
    {
        Vector3 start = patternGrid.GetStartVector();
        start = new Vector3((float)(start.x - 0.5), (float)(start.y - 1.5), start.z);

        GameObject otherPtrn;

        if (Random.Range(0,100) % 2 == 0)
        {
            correctPtrn = Instantiate(ptrn, start, rot.GetRotationXY(x, y, 0));
            otherPtrn = Instantiate(ptrn, start + new Vector3(2f, 0f, 0f), rot.GetRotationXY(x, y, 90));
        }
        else
        {
            otherPtrn = Instantiate(ptrn, start, rot.GetRotationXY(x, y, 90));
            correctPtrn = Instantiate(ptrn, start + new Vector3(2f, 0f, 0f), rot.GetRotationXY(x, y, 0));
        }
        correctPtrn.tag = "Correct";
        otherPtrn.tag = "Incorrect";
    }

    // Use this for initialization
    void Start () {
        dataController = GameObject.FindObjectOfType<DataController>();
        PatternArray patternArray = FindObjectOfType<PatternArray>();
        patternList = patternArray.GetUnlockedPatternList();

        patternGrid = new Grid(2, 2, 3.5);

        updateOn = false;
        StartCoroutine(GetReady());
    }

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
        ShowPossibleAnswers();
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
                        if (hit.collider.CompareTag("Correct"))
                        {
                            StartCoroutine("SpawnNext");
                            CorrectPattern(hit, correctPtrn);
                            combo.AddCombo();
                            if(firstTry)
                                score.Correct(10 * combo.GetCombo());
                            else
                                score.Correct(5);
                            firstTry = true;
                        }
                        else if (hit.collider.CompareTag("Incorrect"))
                        {
                            combo.RemoveCombo();
                            timer.TakeAwayTime();
                            timer.TakeAwayTime();
                            score.Mistake(5);
                            firstTry = false;
                        }
                    }
                }
            }
            else if(timer.IsGameOver()) // jeigu zaidimas pasibaige
            {
                updateOn = false;
                panel.SetActive(true);
                dataController.SaveLogicGameScore(score);
            }
        }
    }

    IEnumerator SpawnNext()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(GameObject.FindWithTag("Incorrect"));
        CreatePattern();
        ShowPossibleAnswers();
    }
}
