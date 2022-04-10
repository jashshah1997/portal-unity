using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private GameObject m_mainMenuPanel;
    private Button m_playButton;
    private Button m_highscoresButton;
    private Button m_exitButton;

    private GameObject m_highscoresPanel;
    private Button m_backButton;

    void Start()
    {
        m_mainMenuPanel = GameObject.Find("MainMenuPanel");
        m_highscoresPanel = GameObject.Find("HighscoresPanel");

        m_playButton = GameObject.Find("PlayButton").GetComponent<Button>();
        m_highscoresButton = GameObject.Find("HighscoresButton").GetComponent<Button>();
        m_exitButton = GameObject.Find("ExitButton").GetComponent<Button>();
        m_backButton = GameObject.Find("BackButton").GetComponent<Button>();

        m_playButton.onClick.AddListener(onPlay);
        m_highscoresButton.onClick.AddListener(onHighscores);
        m_exitButton.onClick.AddListener(onExit);
        m_backButton.onClick.AddListener(onBack);

        m_mainMenuPanel.SetActive(true);
        m_highscoresPanel.SetActive(false);
    }

    void onPlay()
    {
        SceneManager.LoadScene("Level1");
    }

    void onHighscores()
    {
        m_mainMenuPanel.SetActive(false);
        m_highscoresPanel.SetActive(true);
        m_highscoresPanel.GetComponent<HighscoresController>().RefreshScoreEntries(1);
    }

    void onExit()
    {
        Application.Quit();
    }

    void onBack()
    {
        m_mainMenuPanel.SetActive(true);
        m_highscoresPanel.SetActive(false);
    }
}
