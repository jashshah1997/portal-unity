using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenuController : MonoBehaviour
{
    private Button m_resumeButton;
    private Button m_nextLevelButton;
    private Button m_backToMainMenuButton;

    private TextMeshProUGUI m_titleText;
    private TextMeshProUGUI m_scoreText;

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
    }

 
    public void SetPauseMenu()
    {
        m_titleText.text = "Pause Menu";
        m_nextLevelButton.gameObject.SetActive(false);
        m_resumeButton.gameObject.SetActive(true);
    }

    public void SetLevelFinished(int score)
    {
        m_titleText.text = "Level Finished";
        m_scoreText.text = "Elapsed time: " + score;
        m_nextLevelButton.gameObject.SetActive(true);
        m_resumeButton.gameObject.SetActive(false);
    }

    private void onResumeButtonClicked()
    {
        Debug.Log("Resume button");
    }

    private void onNextLevelButtonClicked()
    {
        Debug.Log("Next level");
    }

    private void onBackToMainMenuClicked()
    {
        Debug.Log("Main menu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
