using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryGame : MonoBehaviour {
    
    List<GameObject> patternsOf4;
    Pattern current;
    Pattern previous;
    Pattern next;

    public Timer timer;
    public Score score;
    public Combo combo;

    Vector3 startVector;
    Vector3 middleVector;
    Vector3 endVector;

    Button yes;
    Button no;

    DataController dataController;

    private void ShowNextPattern()
    {
        StartCoroutine(MovePattern());
    }

    private void Awake()
    {
        yes = GameObject.Find("YesButton").GetComponent<Button>();
        no = GameObject.Find("NoButton").GetComponent<Button>();

        yes.onClick.AddListener(CheckIfYes);
        no.onClick.AddListener(CheckIfNo);

        yes.interactable = false;
        no.interactable = false;
    }

    // Use this for initialization
    void Start () {
        dataController = GameObject.FindObjectOfType<DataController>();

        double proporcW = 4.5 / ((double)Screen.height / Screen.width);
        double proporcH = proporcW * ((double)Screen.height / Screen.width);
        Camera.main.orthographicSize = (float)(proporcW * Screen.height / Screen.width * 0.5);
        float up = (float)((proporcH - 0.5)/2);
        float right = (float)(proporcW);
        startVector = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0.5f)) + new Vector3(right + 2f, up + 0.5f, 0f);
        middleVector = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0.5f)) + new Vector3(right + 0.5f, up + 0.5f, 0f) - new Vector3((right + 1) / 2, 0f, 0f);
        endVector = middleVector - new Vector3((right + 1) / 2, 0f, 0f);

        patternsOf4 = ListOfPatterns.CreateListOfPatterns(4);
        previous = null;
        current = new Pattern(patternsOf4[Random.Range(0, patternsOf4.Count)], middleVector, endVector);
        next = new Pattern(patternsOf4[Random.Range(0, patternsOf4.Count)], startVector, middleVector);
        
        StartCoroutine(GameStart());
    }

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(2f);
        ShowNextPattern();
        timer.ResumeTimer();
        yes.interactable = true;
        no.interactable = true;
    }

    IEnumerator MovePattern()
    {
        if (previous != null)
            previous.Delete();
        while (current.ReachedTargetPosition() == false || next.ReachedTargetPosition() == false)
        {
            current.MoveLeft();
            next.MoveLeft();
            yield return null;
        }

        previous = current;
        current = next;
        current.SetNextVector(endVector);
        next = new Pattern(patternsOf4[Random.Range(0, patternsOf4.Count)], startVector, middleVector);
    }

    void CheckIfYes()
    {
        if (timer.IsTimerRunning())
        {
            if (previous.ArePatternsEqual(current))
            {
                combo.AddCombo();
                score.Correct(10 * combo.GetCombo());
            }
            else
            {
                combo.RemoveCombo();
                score.Mistake(5);
            }
            ShowNextPattern();
        }
    }

    void CheckIfNo()
    {
        if (timer.IsTimerRunning())
        {
            if (!previous.ArePatternsEqual(current))
            {
                combo.AddCombo();
                score.Correct(10 * combo.GetCombo());
            }
            else
            {
                combo.RemoveCombo();
                score.Mistake(5);
            }
            ShowNextPattern();
        }
    }
}
