using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class DaysManager : MonoBehaviour
{
    public QuestSystem questSystem;

    private const string LastLoginKey = "LastLoginTime";

    void Start()
    {
        YandexGame.LoadProgress();
        // Проверяем готовность SDK и запускаем логику
        if (!YandexGame.SDKEnabled)
        {
            YandexGame.GetDataEvent += OnDataLoaded;
        }
        else
        {
            CheckDailyReward();
        }
    }

    private void OnDataLoaded()
    {
        YandexGame.GetDataEvent -= OnDataLoaded;
        CheckDailyReward();
    }

    private void CheckDailyReward()
    {
        if (YandexGame.savesData != null)
        {
            string savedTime = YandexGame.savesData.LastLoginKey;
            DateTime lastLoginTime = DateTime.Parse(savedTime);

            // Проверка разницы во времени
            if ((DateTime.Now - lastLoginTime).TotalHours >= 24)
            {
                if (YandexGame.savesData.DailyRewardIndex + 1 >= 7 || YandexGame.savesData.DailyRewardComplete[6] == true)
                {
                    YandexGame.savesData.DailyRewardIndex = 0;

                    YandexGame.savesData.DailyRewardComplete = new bool[7];
                }

                else
                {
                    YandexGame.savesData.DailyRewardIndex++;
                }

                ShowDailyRewardMessage();
            }
        }
        else
        {
            Debug.Log("Первый вход в игру!");

            questSystem.GenerateQuest();
        }

        questSystem.UpdateQuestUI();
    }

    private void SaveCurrentTime()
    {
        string currentTime = DateTime.Now.ToString();
        YandexGame.savesData.LastLoginKey = currentTime;
        YandexGame.SaveProgress();
    }

    private void ShowDailyRewardMessage()
    {
        // Здесь добавьте логику показа сообщения или награды
        Debug.Log("Прошли сутки с последнего входа! Получите награду.");

        questSystem.GenerateQuest();

        SaveCurrentTime();
    }
}
