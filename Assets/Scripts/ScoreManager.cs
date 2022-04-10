using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class SavedScore
{
    // key - level ID
    // value - level scores
    public Dictionary<int, List<int>> scoreDictionary = new Dictionary<int, List<int>>();
}

public class ScoreManager
{
    public static readonly string SAVE_FILE = Application.persistentDataPath + "/portal_scores.save";

    private static ScoreManager _instance;
    public static ScoreManager Instance { get { 
            if (_instance == null)
            {
                _instance = new ScoreManager();
            }
            return _instance; 
        } }

    public void SaveGame(int levelID, int finalScore)
    {
        Debug.Log("Saving to: " + SAVE_FILE);
        SavedScore savedScore = new SavedScore();

        // Check if there exists a saved, load current score information
        if (File.Exists(SAVE_FILE))
        {
            Debug.Log("Loading existing file");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(SAVE_FILE, FileMode.Open);
            savedScore = (SavedScore)bf.Deserialize(file);
            file.Close();
        }

        // Check if the score key exists
        if (!savedScore.scoreDictionary.ContainsKey(levelID))
        {
            Debug.Log("Adding new score list for level " + levelID);
            savedScore.scoreDictionary.Add(levelID, new List<int>());
        }

        // Add a new score entry
        savedScore.scoreDictionary[levelID].Add(finalScore);
        Debug.Log("Score entries for " + levelID + 
            " \n" + savedScore.scoreDictionary[levelID].Count);

        // Save the game
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(SAVE_FILE);
            bf.Serialize(file, savedScore);
            file.Close();
        }
    }

    public List<int> getScoresForLevel(int levelId)
    {
        List<int> scores = new List<int>();
        if (File.Exists(SAVE_FILE))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(SAVE_FILE, FileMode.Open);
            SavedScore savedScore = (SavedScore)bf.Deserialize(file);
            if (savedScore.scoreDictionary.ContainsKey(levelId)) {
                scores = savedScore.scoreDictionary[levelId];
            }
            file.Close();
        }

        return scores;
    }
}
