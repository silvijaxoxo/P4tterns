using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FocusGame : MonoBehaviour {

    public static int orderInLayer;
    List<GameObject> patternsOf4;
    List<string> patternsOnTheLeft;
    List<string> patternsOnTheRight;
    List<Pattern> queueOfPatterns;
    Pattern current;
    Pattern previous;

    public Timer timer;
    public Score score;
    public Combo combo;
    public GameObject panel;

    Vector3 startVector;
    Vector3 middleVector;
    Vector3 leftVector;
    Vector3 rightVector;

    Vector3 leftV, leftV2;
    Vector3 rightV, rightV2;

    float proporcW, proporcH;
    bool updateOn;

    DataController dataController;

    // Use this for initialization
    void Start()
    {
        dataController = GameObject.FindObjectOfType<DataController>();

        orderInLayer = 0;
        proporcH = 8;
        proporcW = proporcH / Screen.height * Screen.width;
        Camera.main.orthographicSize = (float)(proporcW * Screen.height / Screen.width * 0.5);
        float up = (float)(proporcH - 0.5 + proporcH / 3);
        float right = (float)(proporcW);
        startVector = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0.5f)) + new Vector3((float)(proporcW / 2), up, 0f);
        middleVector = startVector - new Vector3(0f, (float)((proporcH - 0.5) / 10 * 11), 0f);
        leftVector = middleVector - new Vector3((right + 1) / 2, 0f, 0f);
        rightVector = middleVector + new Vector3((right + 1) / 2, 0f, 0f);

        leftV = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0.5f)) + new Vector3(proporcW / 5, proporcH * 7 / 9, 0f);
        rightV = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0.5f)) + new Vector3(4 * proporcW / 5, proporcH * 7 / 9, 0f);

        leftV2 = new Vector3(leftV.x, leftV.y - 1f, 0f);
        rightV2 = new Vector3(rightV.x, rightV.y - 1f, 0f);

        patternsOf4 = ListOfPatterns.CreateListOfPatterns(4);

        patternsOnTheLeft = new List<string>();
        patternsOnTheRight = new List<string>();

        queueOfPatterns = new List<Pattern>();
        Vector3 starttemp = startVector - new Vector3(0f, (float)((proporcH - 0.5) / 10 * 10), -1f);
        
        foreach(GameObject ptrn in patternsOf4)
        {
            ptrn.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        previous = null;
        queueOfPatterns.Add(new Pattern(patternsOf4[Random.Range(0, patternsOf4.Count - 2)], starttemp, middleVector));
        for (int i = 1; i < 8; i++)
        {
            starttemp = startVector - new Vector3(0f, (float)((proporcH - 0.5) / 10 * (10 - i)), (float)(-1-i));
            Vector3 endtemp = queueOfPatterns[i - 1].GetStartVector();
            queueOfPatterns.Add(new Pattern(patternsOf4[Random.Range(0, patternsOf4.Count - 2)], starttemp, endtemp));
        }

        updateOn = false;
        StartCoroutine(GetReady());
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

        StartCoroutine(GameStart());
    }

    // Update is called once per frame
    void Update () {
        if (updateOn)
        {
            if (timer.IsTimerRunning())
            {
                if (Input.GetMouseButtonDown(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began))
                {
                    if ((Input.mousePosition.x < Screen.width / 2))
                    {
                        // paspausta kaireje
                        CheckIfLeft();
                    }
                    else if ((Input.mousePosition.x >= Screen.width / 2))
                    {
                        // paspausta desineje
                        CheckIfRight();
                    }
                }
            }
            else if (timer.IsGameOver())
            {
                updateOn = false;
                panel.SetActive(true);
                dataController.SaveFocusingGameScore(score);
            }
        }
    }

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(0.5f);
        ShowNextPattern();
        timer.ResumeTimer();
    }

    IEnumerator MovePattern()
    {
        updateOn = false;
        
        if (current != null)
        {
            while (current.ReachedTargetPosition() == false)
            {
                current.MoveLeft();
                yield return null;
            }
            current.Delete();
        }

        queueOfPatterns.Add(new Pattern(patternsOf4[Random.Range(0, patternsOf4.Count)],
            startVector - new Vector3(0f, (float)((proporcH - 0.5) / 10 * 3), -8f), queueOfPatterns[queueOfPatterns.Count - 1].GetStartVector()));
        
        /*for (int i = 0; i < queueOfPatterns.Count; i++)
        {
            //queueOfPatterns[i].GetPatternGameObject().transform.localScale = new Vector3(1f, 1f, 1f);
            while (queueOfPatterns[i].ReachedTargetPosition() == false)
            {
                queueOfPatterns[i].MoveLeft();
                yield return null;
                //if (i == 0)
                //    queueOfPatterns[i].GetPatternGameObject().transform.localScale += new Vector3(0.05f, 0.05f, 0f);
            }
            if(i > 0)
            {
                queueOfPatterns[i].SetNextVector(queueOfPatterns[i - 1].GetStartVector());
            }
        }*/
        while (queueOfPatterns[0].ReachedTargetPosition() == false)
        {
            for (int i = 0; i < queueOfPatterns.Count; i++)
            {
                queueOfPatterns[i].MoveLeft();
            }
            yield return null;
        }
        for (int i = 0; i < queueOfPatterns.Count; i++)
        {
            if (i > 0)
            {
                queueOfPatterns[i].SetNextVector(queueOfPatterns[i - 1].GetStartVector());
            }
        }
        
        current = queueOfPatterns[0];
        queueOfPatterns.RemoveAt(0);
        
        queueOfPatterns[0].SetNextVector(middleVector);
        updateOn = true;
    }

    private void ShowNextPattern()
    {
        StartCoroutine(MovePattern());
    }

    void CheckIfLeft()
    {
        if((patternsOnTheLeft.Count == 0 && patternsOnTheRight.Count == 0) || (patternsOnTheLeft.Count == 0 && !patternsOnTheRight.Contains(current.GetName())))    // jei abu tusti
        {
            patternsOnTheLeft.Add(current.GetName());
            GameObject temp = current.GetPatternGameObject();
            temp.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
            foreach (Transform childObj in temp.transform)
                GameObject.DestroyImmediate(childObj.gameObject);
            
            Instantiate(temp, leftV, temp.transform.rotation);
            combo.AddCombo();
            score.Correct(combo.GetCombo());
            current.SetNextVector(leftVector);
            ShowNextPattern();
        }
        else if(patternsOnTheLeft.Contains(current.GetName()))
        {
            combo.AddCombo();
            score.Correct(combo.GetCombo());
            current.SetNextVector(leftVector);
            ShowNextPattern();
        }
        else if(patternsOnTheRight.Count != 0 && patternsOnTheLeft.Count != 2 && !patternsOnTheRight.Contains(current.GetName()))    // jeigu desinej nera ir kairys jo neturi
        {
            patternsOnTheLeft.Add(current.GetName());
            GameObject temp = current.GetPatternGameObject();
            temp.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
            foreach (Transform childObj in temp.transform)
                GameObject.DestroyImmediate(childObj.gameObject);

            Instantiate(temp, leftV2, temp.transform.rotation);
            combo.AddCombo();
            score.Correct(combo.GetCombo());
            current.SetNextVector(leftVector);
            ShowNextPattern();
        }
        else
        {
            combo.RemoveCombo();
            score.Mistake(5);
            timer.TakeAwayTime();
        }
    }

    void CheckIfRight()
    {
        if ((patternsOnTheLeft.Count == 0 && patternsOnTheRight.Count == 0) || (patternsOnTheRight.Count == 0 && !patternsOnTheLeft.Contains(current.GetName())))
        {
            patternsOnTheRight.Add(current.GetName());
            GameObject temp = current.GetPatternGameObject();
            temp.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
            foreach (Transform childObj in temp.transform)
                GameObject.DestroyImmediate(childObj.gameObject);

            Instantiate(temp, rightV, temp.transform.rotation);
            combo.AddCombo();
            score.Correct(combo.GetCombo());
            current.SetNextVector(rightVector);
            ShowNextPattern();
        }
        else if (patternsOnTheRight.Contains(current.GetName()))
        {
            combo.AddCombo();
            score.Correct(combo.GetCombo());
            current.SetNextVector(rightVector);
            ShowNextPattern();
        }
        else if (patternsOnTheLeft.Count != 0 && patternsOnTheRight.Count != 2 && !patternsOnTheLeft.Contains(current.GetName()))    // jeigu kairej nera ir desinys jo neturi
        {
            patternsOnTheRight.Add(current.GetName());
            GameObject temp = current.GetPatternGameObject();
            temp.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
            foreach (Transform childObj in temp.transform)
                GameObject.DestroyImmediate(childObj.gameObject);

            Instantiate(temp, rightV2, temp.transform.rotation);
            combo.AddCombo();
            score.Correct(combo.GetCombo());
            current.SetNextVector(rightVector);
            ShowNextPattern();
        }
        else
        {
            combo.RemoveCombo();
            score.Mistake(5);
            timer.TakeAwayTime();
        }
    }

    bool PatternInAList(List<string> patternsOnTheX, Pattern ptrn)
    {
        if (patternsOnTheX.Count != 0)
        {
            foreach (string x in patternsOnTheX)
            {
                if (x.Equals(ptrn.GetName()))
                {
                    return true;
                }
            }
        }
        return false;
    }
}
