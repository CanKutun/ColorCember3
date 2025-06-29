using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabLogin : MonoBehaviour
{
    public static PlayFabLogin Instance { get; private set; }
    public string PlayFabId { get; private set; }

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

    void Start()
    {

        PlayFabSettings.staticSettings.TitleId = "1A8324";

        LoginWithCustomID();
    }

    void LoginWithCustomID()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Giri� ba�ar�l�");
        PlayFabId = result.PlayFabId;

        // Giri� ba�ar�l� olunca sadece 1 kere skor senkronizasyonunu yap
        PlayFabScoreManager.Instance.SyncScoreOnLogin();
    }

    void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError("Giri� hatas�: " + error.GenerateErrorReport());
    }
}
