using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager2 : MonoBehaviour
{
    public LeaderBoardfManager2 leaderboardManager;
    public HighScoreManager2 highScoreManager;
    public Text leaderboardText;
    public Text player1ScoreText;
    public Text player2ScoreText;
    public Text highScoreText;
    public Text targetScoreText;

    public Ball2 ball;

    public GameObject leaderboardPanel;
    public int matchWinningScore = 10;

    void Start()
    {
        leaderboardPanel.SetActive(false);
        DisplayTargetScore();
    }

    void Update()
    {
        player1ScoreText.text = "Player 1: " + highScoreManager.player1Score;
        player2ScoreText.text = "Player 2: " + highScoreManager.player2Score;
        highScoreText.text = "High Score: " + highScoreManager.highScore;
    }

    public void GoalScored(bool isPlayerOne)
    {
        highScoreManager.AddScore(isPlayerOne);
        ball.transform.position = Vector2.zero;
        ball.SendMessage("LaunchBall");

        if (highScoreManager.player1Score >= matchWinningScore || highScoreManager.player2Score >= matchWinningScore)
        {
            EndMatch();
        }
    }

    void EndMatch()
    {
        string winner = highScoreManager.player1Score > highScoreManager.player2Score ? "Player 1" : "Player 2";
        string runnerUp = highScoreManager.player1Score > highScoreManager.player2Score ? "Player 2" : "Player 1";

        leaderboardManager.AddScore("Player 1", highScoreManager.player1Score);
        leaderboardManager.AddScore("Player 2", highScoreManager.player2Score);

        EnableLeaderboard(winner, runnerUp);
        Time.timeScale = 0f;
    }

    void EnableLeaderboard(string winner, string runnerUp)
    {
        leaderboardPanel.SetActive(true);
        DisplayLeaderboard(winner, runnerUp);
    }

    void DisplayLeaderboard(string winner, string runnerUp)
    {
        leaderboardText.text = "Leaderboard:\n";
        leaderboardText.text += $"Winner: {winner}\n";
        leaderboardText.text += $"Runner-Up: {runnerUp}\n\n";

        foreach (var entry in leaderboardManager.leaderboard)
        {
            leaderboardText.text += $"{entry.playerName}: {entry.score}\n";
        }
    }

    void DisplayTargetScore()
    {
        targetScoreText.text = "Winning Score: " + matchWinningScore;
    }

    public void RestartGame()
    {
        leaderboardManager.ResetLeaderboard();
        highScoreManager.ResetScores();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;

        ball.transform.position = Vector2.zero;
        leaderboardPanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
