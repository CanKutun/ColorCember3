using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LeaderboardEntry : MonoBehaviour
{
    public TextMeshProUGUI siraText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI skorText;
    public Image bayrakImage; // Prefab�da ba�lay�n

    public void SetEntry(int sira, string isim, int skor, string countryCode)
    {
        siraText.text = sira.ToString();
        nameText.text = isim;
        skorText.text = skor.ToString();

        SetFlag(countryCode);
    }

    public void SetFlag(string countryCode)
    {
        if (!string.IsNullOrEmpty(countryCode))
        {
            Sprite bayrakSprite = Resources.Load<Sprite>($"Flags/PNG/{countryCode.ToLower()}");

            if (bayrakSprite != null)
            {
                bayrakImage.sprite = bayrakSprite;
                bayrakImage.gameObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Bayrak bulunamad�: " + countryCode);
                bayrakImage.gameObject.SetActive(false);
            }
        }
        else
        {
            bayrakImage.gameObject.SetActive(false);
        }
    }
}