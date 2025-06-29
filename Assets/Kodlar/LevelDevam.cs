using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDevam : MonoBehaviour
{
    void Start()
    {
        // Ba�lang��ta kald��� leveli al
        currentLevel = PlayerPrefs.GetInt("SavedLevel", 1);
        Debug.Log("Bu sahne i�in kay�tl� level: " + currentLevel);
    }

    public int currentLevel = 1;

    public void CompleteLevel()
    {
        int nextLevel = currentLevel + 1;
        PlayerPrefs.SetInt("SavedLevel", nextLevel);
        PlayerPrefs.Save();
        Debug.Log("Level kaydedildi: " + nextLevel);
    }
}