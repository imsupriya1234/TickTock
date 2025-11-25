using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject WinPanel, LoosePanel, StartPanel, TestGamePanel, learnPanel;

    private void Awake()
    {
        WinPanel.SetActive(false);
        LoosePanel.SetActive(false);
        TestGamePanel.SetActive(false);
        learnPanel.SetActive(false);
    }
    public void StartTestGame()
    {
        AudioController.Instance.PlayButtonClickSound();

        if (learnPanel.activeInHierarchy)
            learnPanel.SetActive(false);
        if (!TestGamePanel.activeInHierarchy)
            TestGamePanel.SetActive(true);
        if (WinPanel.activeInHierarchy)
            WinPanel.SetActive(false);
        if (LoosePanel.activeInHierarchy)
            LoosePanel.SetActive(false);
        if (StartPanel.activeInHierarchy)
            StartPanel.SetActive(false);

        TestGamePanel.GetComponent<Testgame>().ShowQuestion();
    }
    
    public void StartLeanPanel()
    {
        AudioController.Instance.PlayButtonClickSound();

        if (TestGamePanel.activeInHierarchy)
            TestGamePanel.SetActive(false);
        if (WinPanel.activeInHierarchy)
            WinPanel.SetActive(false);
        if (LoosePanel.activeInHierarchy)
            LoosePanel.SetActive(false);
        if (StartPanel.activeInHierarchy)
            StartPanel.SetActive(false);

        learnPanel.SetActive(true);
    }

    public void Home()
    {
        AudioController.Instance.PlayButtonClickSound();

        if (learnPanel.activeInHierarchy)
            learnPanel.SetActive(false);
        if (TestGamePanel.activeInHierarchy)
            TestGamePanel.SetActive(false);

        if (WinPanel.activeInHierarchy)
            WinPanel.SetActive(false);
        if (LoosePanel.activeInHierarchy)
            LoosePanel.SetActive(false);

        StartPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Game Quit");   // For debugging in editor
        Application.Quit();
    }
}
