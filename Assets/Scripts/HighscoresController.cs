using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighscoresController : MonoBehaviour
{
    private GameObject m_scoresContainer;
    private GameObject m_headerTitle;
    private GameObject m_entryTemplate;

    private Button m_nextScores;
    private Button m_previousScores;

    private List<GameObject> m_currentEntries = new List<GameObject>();

    public Color ODD_COLOR;
    public Color EVEN_COLOR;

    private int m_levelID;

    private void Awake()
    {
        m_scoresContainer = GameObject.Find("HighscoresTable");
        m_entryTemplate = GameObject.Find("TemplateEntry");
        m_headerTitle = GameObject.Find("HeaderTitle");

        m_nextScores = GameObject.Find("NextButton").GetComponent<Button>();
        m_previousScores = GameObject.Find("PreviousButton").GetComponent<Button>();

        m_nextScores.onClick.AddListener(onNextButton);
        m_previousScores.onClick.AddListener(onPreviousButton);

        RefreshScoreEntries(1);
    }

    private void onNextButton()
    {
        if (m_levelID == 3) return;
        RefreshScoreEntries(m_levelID + 1);
    }

    private void onPreviousButton()
    {
        if (m_levelID == 1) return;
        RefreshScoreEntries(m_levelID - 1);
    }

    public void RefreshScoreEntries(int levelID)
    {
        // Clear current entries
        foreach (var entry in m_currentEntries)
        {
            Destroy(entry);
        }
        m_currentEntries.Clear();

        var scores = ScoreManager.Instance.getScoresForLevel(levelID);
        scores.Sort();

        for (int i = 0; i < scores.Count; i++)
        {
            if (i > 9) break;

            var newEntry = Instantiate(m_entryTemplate, m_scoresContainer.transform);
            var rectTransform = newEntry.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(0, -(i + 2.5f) * rectTransform.rect.height);

            var rank = i + 1;
            newEntry.transform.Find("Rank").GetComponent<TextMeshProUGUI>().text = rank + ".";
            newEntry.transform.Find("Time").GetComponent<TextMeshProUGUI>().text = scores[i] + "";
            newEntry.transform.Find("Background").GetComponent<Image>().color = i % 2 == 0 ? EVEN_COLOR : ODD_COLOR;
            m_currentEntries.Add(newEntry);
        }

        // Set the title
        m_headerTitle.GetComponent<TextMeshProUGUI>().text = "Level " + levelID + " Highscores";
        m_levelID = levelID;
    }
}
