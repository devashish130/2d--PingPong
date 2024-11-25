using UnityEngine;
using System.IO;

public class HighScoreManager2 : MonoBehaviour
{
    public int player1Score;
    public int player2Score;
    public int highScore;

    private string savePath;

    void Start()
    {
        savePath = Application.persistentDataPath + "/pvp_highscore.json";
        LoadHighScore();
    }

    public void AddScore(bool isPlayerOne)
    {
        if (isPlayerOne)
            player1Score++;
        else
            player2Score++;

        if (Mathf.Max(player1Score, player2Score) > highScore)
        {
            highScore = Mathf.Max(player1Score, player2Score);
            SaveHighScore();
        }
    }

    void SaveHighScore()
    {
        PvPHighScoreData data = new PvPHighScoreData { highScore = highScore };
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, json);
    }

    void LoadHighScore()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            PvPHighScoreData data = JsonUtility.FromJson<PvPHighScoreData>(json);
            highScore = data.highScore;
        }
    }

    public void ResetScores()
    {
        player1Score = 0;
        player2Score = 0;
        highScore = 0;
        SaveHighScore();
    }
}

[System.Serializable]
public class PvPHighScoreData
{
    public int highScore;
}
