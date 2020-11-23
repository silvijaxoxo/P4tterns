using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OtherMemoryGame : MonoBehaviour {

    AbstractFactory factory;
    AbstractProduct current;
    AbstractProduct previous;

    List<GameObject> patternsOf4;
    enum Question {Count, Pattern, Size, Direction, Speed};
    bool answer;

    Button yes;
    Button no;

    public Timer timer;
    public Score score;
    public Combo combo;
    public Text questionText;
    public GameObject panel;

    DataController dataController;
    bool updateOn = true;

    private void Awake()
    {
        yes = GameObject.Find("YesButton").GetComponent<Button>();
        no = GameObject.Find("NoButton").GetComponent<Button>();

        yes.onClick.AddListener(CheckIfYes);
        no.onClick.AddListener(CheckIfNo);

        yes.interactable = false;
        no.interactable = false;
    }

    void GenerateQuestion()
    {
        Question q = (Question)Random.Range(0, System.Enum.GetNames(typeof(Question)).Length);
        switch (q)
        {
            case Question.Count:
                questionText.text = "Is pattern quantity the same?";
                answer = current.CompareCount(previous);
                break;
            case Question.Pattern:
                questionText.text = "Is pattern the same?";
                answer = current.ComparePattern(previous);
                break;
            case Question.Size:
                questionText.text = "Is size the same?";
                answer = current.CompareSize(previous);
                break;
            case Question.Direction:
                questionText.text = "Is rotation direction the same?";
                answer = current.CompareDirection(previous);
                break;
            case Question.Speed:
                questionText.text = "Is rotation speed the same?";
                answer = current.CompareSpeed(previous);
                break;
        }
    }

    void InstantiateCurrentProduct()
    {
        int rand = RandomCount();
        if(rand == 1)
            current = factory.CreateProduct1(patternsOf4[Random.Range(0, patternsOf4.Count)]);
        else if(rand == 2)
            current = factory.CreateProduct2(patternsOf4[Random.Range(0, patternsOf4.Count)]);
        else if (rand == 3)
            current = factory.CreateProduct3(patternsOf4[Random.Range(0, patternsOf4.Count)]);
        else if (rand == 4)
            current = factory.CreateProduct4(patternsOf4[Random.Range(0, patternsOf4.Count)]);
    }

    private static int RandomCount()
    {
        int[] values = new int[] { 1, 2, 3, 4 };
        return values[Random.Range(0, values.Length)];
    }

    void DisplayNext()
    {
        previous = current;
        previous.Hide();
        InstantiateCurrentProduct();
        GenerateQuestion();
    }

    // Use this for initialization
    void Start () {
        dataController = GameObject.FindObjectOfType<DataController>();
        patternsOf4 = ListOfPatterns.CreateListOfPatterns(4);
        factory = new ConcreteDisplayFactory();
        questionText.text = "";

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

        InstantiateCurrentProduct();
        StartCoroutine(GameStart());
    }

    // Update is called once per frame
    void Update () {
        if (updateOn)
        {
            if (!timer.IsGameOver())
                current.Move();
            else
            {
                updateOn = false;
                panel.SetActive(true);
                dataController.SaveMemoryGameScore(score);
            }
        }
	}

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(2f);
        DisplayNext();
        timer.ResumeTimer();
        yes.interactable = true;
        no.interactable = true;
    }

    void CheckIfYes()
    {
        if (timer.IsTimerRunning())
        {
            if(answer == true)
            {
                combo.AddCombo();
                score.Correct(10 * combo.GetCombo());
            }
            else
            {
                combo.RemoveCombo();
                score.Mistake(10);
                timer.TakeAwayTime();
                timer.TakeAwayTime();
            }
            DisplayNext();
        }
    }

    void CheckIfNo()
    {
        if (timer.IsTimerRunning())
        {
            if (answer == false)
            {
                combo.AddCombo();
                score.Correct(10 * combo.GetCombo());
            }
            else
            {
                combo.RemoveCombo();
                score.Mistake(10);
                timer.TakeAwayTime();
                timer.TakeAwayTime();
            }
            DisplayNext();
        }
    }
}
