using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    private Button m_resumeButton;
    private Button m_nextLevelButton;
    private Button m_backToMainMenuButton;

    private TextMeshProUGUI m_titleText;
    private TextMeshProUGUI m_scoreText;

    private GameManager m_gameManager;

    private int m_nextLevelId = 1;

    // Start is called before the first frame update
    void Awake()
    {
        m_resumeButton = GameObject.Find("ResumeButton").GetComponent<Button>();
        m_resumeButton.onClick.AddListener(onResumeButtonClicked);

        m_nextLevelButton = GameObject.Find("NextLevelButton").GetComponent<Button>();
        m_nextLevelButton.onClick.AddListener(onNextLevelButtonClicked);

        m_backToMainMenuButton = GameObject.Find("BackToMainMenuButton").GetComponent<Button>();
        m_backToMainMenuButton.onClick.AddListener(onBackToMainMenuClicked);

        m_titleText = GameObject.Find("TitleText").GetComponent<TextMeshProUGUI>();
        m_scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

 
    public void SetPauseMenu()
    {
        m_titleText.text = "Pause Menu";
        m_nextLevelButton.gameObject.SetActive(false);
        m_resumeButton.gameObject.SetActive(true);
    }

    public void SetLevelFinished(int score, int nextLevelId)
    {
        m_titleText.text = "Level " + (nextLevelId - 1) +  " Finished";
        m_scoreText.text = "Elapsed time: " + score;
        m_nextLevelButton.gameObject.SetActive(true);
        m_resumeButton.gameObject.SetActive(false);
        m_nextLevelId = nextLevelId;
    }

    private void onResumeButtonClicked()
    {
        m_gameManager.TogglePause();
        m_gameManager.TogglePauseMenu();
    }

    private void onNextLevelButtonClicked()
    {
        SceneManager.LoadScene("Level" + m_nextLevelId);
    }

    private void onBackToMainMenuClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
