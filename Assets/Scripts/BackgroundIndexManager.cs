using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundIndexManager : MonoBehaviour
{
    public static BackgroundIndexManager Instance;

    public int lastEndIndex = 0;

    private void Awake()
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
}