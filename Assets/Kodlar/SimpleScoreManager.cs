using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class SimpleScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text highScoreText;

    private int currentScore = 0;

    void Start()
    {
        // Ba�lang��ta y�ksek skoru y�kle ve g�ster
        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateUI(savedHighScore);
    }

    public void AddScore(int points)
    {
        currentScore += points;
        scoreText.text = "Skor: " + currentScore;

        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);

        if (currentScore > savedHighScore)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            highScoreText.text = "En Y�ksek: " + currentScore;

            // PlayFab'a skor g�nder
            SendHighScoreToPlayFab(currentScore);
        }
    }

    private void UpdateUI(int highScore)
    {
        scoreText.text = "Skor: " + currentScore;
        highScoreText.text = "En Y�ksek: " + highScore;
    }

    void SendHighScoreToPlayFab(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new System.Collections.Generic.List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "HighScore",
                    Value = score
                }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request,
            result => Debug.Log("Skor PlayFab'a g�nderildi: " + score),
            error => Debug.LogError("PlayFab Skor G�nderme Hatas�: " + error.GenerateErrorReport()));
    }
}