using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabScoreManager : MonoBehaviour
{
    public static PlayFabScoreManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Oyuncu giri� yapt�ktan sonra local ve PlayFab skorlar�n� bir kere senkronize eder.
    /// </summary>
    public void SyncScoreOnLogin()
    {
        
    }

    /// <summary>
    /// Yeni skor PlayFab'a g�nderilir. Oyun i�inde s�rekli �a�r�lmamal�.
    /// </summary>
    public void SendHighScore(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "HighScore",
                    Value = score
                }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request,
    result => {
        Debug.Log("Yeni y�ksek skor PlayFab'a g�nderildi: " + score);
        // Liderlik tablosunu yenile (�rnek olarak)
        LeaderboardManager.Instance?.RefreshLeaderboard();
    },
    error => Debug.LogError("Skor g�nderme hatas�: " + error.GenerateErrorReport()));
    }
}