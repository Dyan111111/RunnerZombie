using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Text DistanceText;

    public Text LevelText;

    public Text CoinsText;

    public GameObject DeathPanel;

    public GameObject PausePanel;

    public GameObject FinishPanel;

    public GameObject NotifyPanel;

    public AudioClip PauseTapSound;

    public AudioClip OpenFinishPanelSound;

    private bool isPause = false;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCoinsText(int newCoins)
    {
        CoinsText.text = $"{newCoins}";
    }

    public void PauseGame()
    {
        isPause = !isPause;

        if (isPause == true)
        {
            Time.timeScale = 0f;

            PausePanel.SetActive(true);

            SoundManager.instance.PlayShot(PauseTapSound);
        }

        else
        {
            Time.timeScale = 1f;

            PausePanel.SetActive(false);
        }
    }

    public void OpenFinishPanel()
    {
        FinishPanel.SetActive(true);

        SoundManager.instance.PlayShot(OpenFinishPanelSound);
    }

    public void UpdateDistanceText(float distance)
    {
        int ConvertedDistance = (int)distance;

        DistanceText.text = $"{ConvertedDistance}m";

        if (QuestSystem.instance.CurrentQuest.questType == QuestType.Run)
        {
            if (ConvertedDistance % 100 == 0 && ConvertedDistance != 0) 
            {
                QuestSystem.instance.AddNumberToQuest(100);
            }
        }
    }

    public void UpdateLevelText(int levelNow) //обновляет номер уровня в меню
    {
        if (YandexGame.lang == "ru")
        {
            LevelText.text = $"Уровень {levelNow}";
        }

        else if (YandexGame.lang == "en")
        {
            LevelText.text = $"Level {levelNow}";
        }

        else if (YandexGame.lang == "tr")
        {
            LevelText.text = $"Seviye {levelNow}";
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
