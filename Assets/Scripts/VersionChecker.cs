using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class VersionChecker : MonoBehaviour
{
    [Header("Version")]
    public string versionUrl = "https://raw.githubusercontent.com/CanKutun/ColorCember2/main/version.txt";
    public GameObject updatePanel;

    void Start()
    {
        StartCoroutine(CheckVersion());
    }

    IEnumerator CheckVersion()
    {
        UnityWebRequest www = UnityWebRequest.Get(versionUrl);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(">>> [VersionChecker] version.txt al�namad�: " + www.error);
        }
        else
        {
            string latestVersionRaw = www.downloadHandler.text.Replace("\n", "").Replace("\r", "").Trim();
            string currentVersionRaw = Application.version.Trim();

            Debug.Log(">>> [VersionChecker] Uygulama S�r�m�: " + currentVersionRaw);
            Debug.Log(">>> [VersionChecker] En Son S�r�m: " + latestVersionRaw);

            try
            {
                Version latestVersion = new Version(latestVersionRaw);
                Version currentVersion = new Version(currentVersionRaw);

                if (currentVersion < latestVersion)
                {
                    Debug.Log(">>> [VersionChecker] G�ncelleme gerekli.");
                    if (updatePanel != null)
                        updatePanel.SetActive(true);
                    else
                        Debug.LogWarning(">>> [VersionChecker] updatePanel atanmam��.");
                }
                else
                {
                    Debug.Log(">>> [VersionChecker] G�ncel s�r�m kullan�l�yor.");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(">>> [VersionChecker] S�r�m kar��la�t�rmas�nda hata: " + ex.Message);
            }
        }
    }
}