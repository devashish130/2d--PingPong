using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LeaderBoardfManager2 : MonoBehaviour
{
    public List<PvPScoreEntry> leaderboard;
    private string savePath;

    void Start()
    {
        savePath = Application.persistentDataPath + "/pvp_leaderboard.json";
        EnsureLeaderboardFileExists();
        LoadLeaderboard();
    }

    public void AddScore(string playerName, int score)
    {
        PvPScoreEntry newEntry = new PvPScoreEntry { playerName = playerName, score = score };
        leaderboard.Add(newEntry);

        leaderboard.Sort((a, b) => b.score.CompareTo(a.score));

        if (leaderboard.Count > 10)
            leaderboard.RemoveRange(10, leaderboard.Count - 10);

        SaveLeaderboard();
    }

    void SaveLeaderboard()
    {
        PvPLeaderboardData data = new PvPLeaderboardData { entries = leaderboard };
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
        Debug.Log("PvP Leaderboard saved: " + savePath);
    }

    void LoadLeaderboard()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            PvPLeaderboardData data = JsonUtility.FromJson<PvPLeaderboardData>(json);
            leaderboard = data.entries;
        }
        else
        {
            leaderboard = new List<PvPScoreEntry>();
        }
    }

    void EnsureLeaderboardFileExists()
    {
        if (!File.Exists(savePath))
        {
            leaderboard = new List<PvPScoreEntry>();
            SaveLeaderboard();
        }
    }

    public void ResetLeaderboard()
    {
        leaderboard.Clear();
        SaveLeaderboard();
    }
}

[System.Serializable]
public class PvPScoreEntry
{
    public string playerName;
    public int score;
}

[System.Serializable]
public class PvPLeaderboardData
{
    public List<PvPScoreEntry> entries;
}
