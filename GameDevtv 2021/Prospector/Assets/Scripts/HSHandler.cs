using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public struct Score
{
    public string name;
    public int level;
}

public class ScoreSort : IComparer<Score>
{
    public int Compare(Score x, Score y)
    {
        return x.level.CompareTo(y.level);
    }
}

public class HSHandler : MonoBehaviour
{
    [SerializeField] TMP_Text scoreDisplay = null;
    List<Score> scores;

    string filePath => $"{Application.persistentDataPath}/HighScores.json";

    private void Start()
    {
        scores = GetSavedScores();
        if (scoreDisplay)
        {
            DisplayScores();
        }
    }

    private List<Score> GetSavedScores()
    {
        if (!File.Exists(filePath))
        {
            
            File.Create(filePath).Dispose();
            return new List<Score>();
        }

        using( StreamReader stream = new StreamReader(filePath))
        {
            
            string file = stream.ReadToEnd();
            return JsonUtility.FromJson<List<Score>>(file);
        }
    }

    public void AddScore(string name, int level)
    {
        Score newScore = new Score();
        newScore.name = name;
        newScore.level = level;
        scores.Add(newScore);
        scores.Sort(new ScoreSort());
        while (10 <= scores.Count)
        {
            scores.Remove(scores[scores.Count]);
        }

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            string file = JsonUtility.ToJson(scores, true);
            writer.Write(file);
        }
        
    }

    public void DisplayScores()
    {
        scoreDisplay.text = "";
        int place = 1;
        foreach (Score score in scores)
        {
            scoreDisplay.text += $"{place}.\t{score.name}\t{score.level}\n";
            ++place;
        }
    }
}
