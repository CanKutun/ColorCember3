using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {


        int savedLevel = PlayerPrefs.GetInt("SavedLevel"); // Varsay�lan: Level 1
        string sceneName = "Level " + savedLevel; // Bo�luklu sahne ad�
        SceneManager.LoadScene(sceneName);

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
