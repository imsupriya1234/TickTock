using UnityEngine;
using UnityEngine.UI;

public class Testgame : MonoBehaviour
{
    public Transform Hr, Min;
    public Button leftAns, rightAns;
    public Transform[] life = new Transform[3];

    public Question[] questions = new Question[5];

    [SerializeField] private int currentQuestionIndex = 0;
    [SerializeField] private int correctCount = 0;
    [SerializeField] private int wrongCount = 0;

    [SerializeField] private int lifeCount = 0;

    public GameManager gameManager;

    public float timeRemaining = 180f; // Timer starts at 180 seconds
    public bool isRunning = false;
    public Text timerText; // Assign in Inspector

    void Start()
    {

    }
    void Update()
    {
        if (isRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay(timeRemaining);
            }
            else
            {
                isRunning = false;
                timeRemaining = 0;
                UpdateTimerDisplay(timeRemaining);
                OnTimerEnd();
            }
        }
    }

    public void ShowQuestion()
    {
        StartTimer();

        Question q = questions[currentQuestionIndex];
        Debug.Log("Q: " + q.questionText);
        Debug.Log("A: " + q.optionA);
        Debug.Log("B: " + q.optionB);
        //Update Answer texts
        leftAns.gameObject.transform.GetChild(0).GetComponent<Text>().text = q.optionA;
        rightAns.gameObject.transform.GetChild(0).GetComponent<Text>().text = q.optionB;

        //Update the Clock Time
        Hr.rotation = Quaternion.Euler(0, 0, q.rotationHr);
        Min.rotation = Quaternion.Euler(0, 0, q.rotationMin);

        //Assign Correct Answer
        leftAns.onClick.RemoveAllListeners();
        rightAns.onClick.RemoveAllListeners();
        leftAns.onClick.AddListener(() => SelectOption(q.optionA));
        rightAns.onClick.AddListener(() => SelectOption(q.optionB));
    }

    public void SelectOption(string selectedOption)
    {
        Question q = questions[currentQuestionIndex];

        if (selectedOption == q.correctAnswer)
        {
            AudioController.Instance.PlayCorrectSound();
            Debug.Log("Correct!");
            correctCount++;
        }
        else
        {
            AudioController.Instance.PlayWrongSound();
            Debug.Log("Wrong!");
            wrongCount++;
            lifeCount++;
            if (lifeCount < 3)
                life[lifeCount - 1].gameObject.SetActive(false);
            else
            {
                ResetTestGameData();

                AudioController.Instance.PlayLoosePopUpSound();
                gameManager.LoosePanel.SetActive(true);
                gameManager.LoosePanel.GetComponent<Loosepanel>().looseText.text = "Life End";
                SetUpLoosepanel();

                return;
            }
                
        }

        NextQuestion();
    }

    void NextQuestion()
    {
        currentQuestionIndex++;

        if (currentQuestionIndex >= questions.Length)
        {
            Debug.Log("Quiz Finished! ");
            leftAns.onClick.RemoveAllListeners();
            rightAns.onClick.RemoveAllListeners();

            if (correctCount > wrongCount)
            {
                AudioController.Instance.PlayWinPopUpSound();
                gameManager.WinPanel.SetActive(true);
                SetUpWinPanel();
            }
            else
            {
                AudioController.Instance.PlayLoosePopUpSound();
                gameManager.LoosePanel.SetActive(true);
                gameManager.LoosePanel.GetComponent<Loosepanel>().looseText.text = "Wrong Answers";
                SetUpLoosepanel();
            }
            ResetTestGameData();

            return;
        }

        ShowQuestion();
    }

    // Reset all values !!!
    void ResetTestGameData()
    {
        StopTimer();
        currentQuestionIndex = 0;
        correctCount = 0;
        wrongCount = 0;

        if (lifeCount != 0)
        {
            for (int i = 0; i < 3; i++)
            {
                life[i].gameObject.SetActive(true);
            }
        }
        lifeCount = 0;
        ResetTimer(180f);
    }

    void SetUpLoosepanel()
    {
        gameManager.LoosePanel.GetComponent<Loosepanel>().HomeButton.onClick.RemoveAllListeners();
        gameManager.LoosePanel.GetComponent<Loosepanel>().RestartButton.onClick.RemoveAllListeners();
        gameManager.LoosePanel.GetComponent<Loosepanel>().HomeButton.onClick.AddListener(() => Home());
        gameManager.LoosePanel.GetComponent<Loosepanel>().RestartButton.onClick.AddListener(() => RestartTestgame());
    }
    void SetUpWinPanel()
    {
        gameManager.WinPanel.GetComponent<Winpanel>().HomeButton.onClick.RemoveAllListeners();
        gameManager.WinPanel.GetComponent<Winpanel>().RestartButton.onClick.RemoveAllListeners();
        gameManager.WinPanel.GetComponent<Winpanel>().HomeButton.onClick.AddListener(() => Home());
        gameManager.WinPanel.GetComponent<Winpanel>().RestartButton.onClick.AddListener(() => RestartTestgame());
    }
    void RestartTestgame()
    {
        gameManager.StartTestGame();
    }

    void Home()
    {
        gameManager.Home();
    }

    // =================== TIMER ===================


    void UpdateTimerDisplay(float timeToDisplay)
    {
        timeToDisplay = Mathf.Max(0, timeToDisplay); // Avoid negative display
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void OnTimerEnd()
    {
        Debug.Log("Timer Finished!");
        // Trigger any event here (e.g., end game, load scene, etc.)
        gameManager.LoosePanel.SetActive(true);
        gameManager.LoosePanel.GetComponent<Loosepanel>().looseText.text = "Time over";
        ResetTestGameData();
        SetUpLoosepanel();
    }

    public void StartTimer()
    {
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResetTimer(float newTime)
    {
        timeRemaining = newTime;
        UpdateTimerDisplay(newTime);
    }
}

[System.Serializable]
public class Question
{
    public string questionText;
    public string optionA;
    public string optionB;
    public float rotationHr;
    public float rotationMin;
    public string correctAnswer;
}
