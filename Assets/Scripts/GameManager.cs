using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static readonly int MAX_LEVEL = 3;
    public int LevelId;

    private TextMeshProUGUI m_timerText;
    private float m_currentTime;
    
    private GameObject m_crosshair;
    private GameObject m_pausePanel;
    private bool m_gamePaused;
    private bool m_level_finished;

    // Start is called before the first frame update
    void Start()
    {
        m_timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();
        m_crosshair = GameObject.Find("Crosshair");
        m_pausePanel = GameObject.Find("PausePanel");

        m_gamePaused = false;
        m_level_finished = false;
        m_crosshair.SetActive(true);
        m_pausePanel.SetActive(false);

        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_level_finished) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
            TogglePauseMenu();
            if (m_gamePaused)
            {
                m_pausePanel.GetComponent<PauseMenuController>().SetPauseMenu();
            }
        }
        
        m_currentTime += Time.deltaTime;
        m_timerText.text = "Time: " + (int)m_currentTime;
    }
   
    public void SetLevelFinished()
    {
        m_level_finished = true;
        PauseGame();
        TogglePauseMenu();
        m_pausePanel.GetComponent<PauseMenuController>().SetLevelFinished((int)m_currentTime, LevelId + 1);
        ScoreManager.Instance.SaveGame(LevelId, (int)m_currentTime);
    }

    public void TogglePause()
    {
        Time.timeScale = 1 - Time.timeScale;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void TogglePauseMenu()
    {
        m_gamePaused = !m_gamePaused;
        m_crosshair.SetActive(!m_gamePaused);
        m_pausePanel.SetActive(m_gamePaused);

        if (m_gamePaused)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
