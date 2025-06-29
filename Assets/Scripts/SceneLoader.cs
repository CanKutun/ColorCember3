using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadLeaderboardScene()
    {
        // Sahne y�klendikten sonra �al��mas� i�in olay� ba�la
        SceneManager.sceneLoaded += OnTabloSceneLoaded;

        // Sahneyi y�kle
        SceneManager.LoadScene("Tablo");
    }

    private void OnTabloSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Tablo" && LeaderboardManager.Instance != null)
        {
            Debug.Log("Tablo sahnesi y�klendi, RefreshLeaderboard �a�r�l�yor");
            LeaderboardManager.Instance.RefreshLeaderboard();
        }

        // Ba�lant�y� kald�r, yoksa ileride ayn� �ey tekrar tekrar �a�r�l�r
        SceneManager.sceneLoaded -= OnTabloSceneLoaded;
    }
}