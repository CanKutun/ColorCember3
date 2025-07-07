using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundManager : MonoBehaviour
{
    [Header("Arkaplan Ayarları")]
    [SerializeField] private int backgroundCount = 5;
    [SerializeField] private string resourceFolder = "Backgrounds";
    [SerializeField] private int totalBackgroundCount = 68; // 🎯 Toplam kaç arka plan sprite'ı varsa buraya yaz

    [Header("Obje Referansları")]
    public Transform player;
    public SpriteRenderer background1;
    public SpriteRenderer background2;

    [Header("Teknik Ayarlar")]
    public float sectionHeight = 10f;

    private Sprite[] backgrounds;
    private int currentIndex = 1;
    private bool useFirst = true;

    private int startIndex;
    public int endIndex;

    public int StartIndex => startIndex;
    public int BackgroundCount => backgroundCount;

    void Start()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        if (currentScene == 1)
        {
            startIndex = 1;
        }
        else
        {
            // Önceki sahnede kullanılan endIndex'ten başla
            startIndex = PlayerPrefs.GetInt("PreviousEndIndex", 1);
        }

        endIndex = startIndex + backgroundCount - 1;

        Debug.Log($"[BG] Sahne: {SceneManager.GetActiveScene().name} | startIndex: {startIndex}, endIndex: {endIndex}");

        LoadSpritesFromResources(startIndex, endIndex);
        SetInitialTwoBackgrounds();

        // Sonraki sahne için endIndex kaydet (başlangıç olarak kullanılacak)
        //PlayerPrefs.SetInt("PreviousEndIndex", endIndex);
        //PlayerPrefs.Save();
    }

    void Update()
    {
        if (backgrounds == null || backgrounds.Length == 0 || player == null)
            return;

        float camY = Camera.main.transform.position.y;
        float screenTop = camY + Camera.main.orthographicSize;
        float offset = sectionHeight / 2f;
        float screenTopAdjusted = screenTop + offset;

        int index = (int)(screenTopAdjusted / sectionHeight);

        if (index < 0 || index >= backgrounds.Length)
            return;

        if (index != currentIndex)
        {
            currentIndex = index;
            Sprite nextSprite = backgrounds[index];
            float targetY = index * sectionHeight;

            if (useFirst)
            {
                background1.sprite = nextSprite;
                background1.transform.position = new Vector3(0f, targetY, 0f);
                FitSpriteToCamera(background1);
                SetAlpha(background1, 1f);
            }
            else
            {
                background2.sprite = nextSprite;
                background2.transform.position = new Vector3(0f, targetY, 0f);
                FitSpriteToCamera(background2);
                SetAlpha(background2, 1f);
            }

            useFirst = !useFirst;
        }
    }

    void LoadSpritesFromResources(int start, int end)
    {
        List<Sprite> loaded = new List<Sprite>();

        for (int i = start; i <= end; i++)
        {
            int wrappedIndex = ((i - 1) % totalBackgroundCount) + 1; // 🎯 Döngüsel sistem
            string fileName = $"S{wrappedIndex.ToString("D2")}";
            string path = $"{resourceFolder}/{fileName}";
            Sprite sprite = Resources.Load<Sprite>(path);

            if (sprite != null)
            {
                loaded.Add(sprite);
            }
            else
            {
                Debug.LogWarning($"Arka plan bulunamadı: {path}");
            }
        }

        backgrounds = loaded.ToArray();
    }

    void SetInitialTwoBackgrounds()
    {
        if (backgrounds.Length < 2 || player == null)
            return;

        float camY = Camera.main.transform.position.y;

        background1.sprite = backgrounds[0];
        background1.transform.position = new Vector3(0f, camY, 0f);
        FitSpriteToCamera(background1);
        SetAlpha(background1, 1f);

        background2.sprite = backgrounds[1];
        background2.transform.position = new Vector3(0f, sectionHeight, 0f);
        FitSpriteToCamera(background2);
        SetAlpha(background2, 1f);

        currentIndex = 1;
        useFirst = true;
    }

    void FitSpriteToCamera(SpriteRenderer sr)
    {
        if (sr == null || sr.sprite == null) return;

        float screenHeight = Camera.main.orthographicSize * 2f;
        float screenWidth = screenHeight * Camera.main.aspect;
        Vector2 spriteSize = sr.sprite.bounds.size;

        sr.transform.localScale = new Vector3(
            screenWidth / spriteSize.x,
            screenHeight / spriteSize.y,
            1f);
    }

    void SetAlpha(SpriteRenderer sr, float alpha)
    {
        if (sr == null) return;
        Color c = sr.color;
        c.a = alpha;
        sr.color = c;
    }
}