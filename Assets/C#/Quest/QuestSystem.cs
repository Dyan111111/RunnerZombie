using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class QuestSystem : MonoBehaviour
{
    public static QuestSystem instance;

    public List<Quest> QuestList = new List<Quest>();

    public Quest CurrentQuest;

    [Header("UI Game")]

    public Text QuestNameText;

    public Text QuestCountText;

    public Text QuestRewardText;

    public GameObject CompleteImage;

    public GameObject RewardPanel;

    [Header("UI Menu")]

    public Text QuestNameTextMenu;

    public Text QuestCountTextMenu;

    public Text QuestRewardTextMenu;

    public GameObject CompleteImageMenu;

    public GameObject RewardPanelMenu;

    public bool QuestWasComplete = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        YandexGame.LoadProgress();

        if (YandexGame.savesData != null)
        {
            CurrentQuest = YandexGame.savesData.CurrentQuest;
        }

        QuestWasComplete = YandexGame.savesData.QuestWasComplete;

        if (QuestWasComplete == true)
        {
            RewardPanel.SetActive(false);

            CompleteImage.SetActive(true);

            RewardPanelMenu.SetActive(false);

            CompleteImageMenu.SetActive(true);
        }

        UpdateQuestUI();
    }

    public void GenerateQuest()
    {
        CurrentQuest = QuestList[Random.Range(0, QuestList.Count)];

        YandexGame.savesData.CurrentQuest = CurrentQuest;

        QuestWasComplete = false;

        YandexGame.savesData.QuestWasComplete = QuestWasComplete;

        YandexGame.SaveProgress();
    }

    public void AddNumberToQuest(int number)
    {
        if (!CheckMaxNumberQuest())
        {
            CurrentQuest.QuestNow += number;

            YandexGame.savesData.CurrentQuest = CurrentQuest;

            YandexGame.SaveProgress();

            UpdateQuestUI();
        }
    }

    public bool CheckMaxNumberQuest()
    {
        if (CurrentQuest.QuestNow >= CurrentQuest.QuestMax)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    public void UpdateQuestUI()
    {
        if (CurrentQuest != null)
        {
            if(YandexGame.lang == "ru")
            {
                QuestNameText.text = CurrentQuest.QuestNameRu;

                QuestNameTextMenu.text = CurrentQuest.QuestNameRu;
            }

            else if(YandexGame.lang == "tr")
            {
                QuestNameText.text = CurrentQuest.QuestNameTr;

                QuestNameTextMenu.text = CurrentQuest.QuestNameTr;
            }

            else
            {
                QuestNameText.text = CurrentQuest.QuestNameEn;

                QuestNameTextMenu.text = CurrentQuest.QuestNameEn;
            }

            QuestCountText.text = $"{CurrentQuest.QuestNow} / {CurrentQuest.QuestMax}";

            QuestCountTextMenu.text = $"{CurrentQuest.QuestNow} / {CurrentQuest.QuestMax}";

            QuestRewardText.text = CurrentQuest.Reward.ToString();

            QuestRewardTextMenu.text = CurrentQuest.Reward.ToString();
        }
    }

    public void CompleteQuest()
    {
        if (QuestWasComplete == false)
        {
            if (CheckMaxNumberQuest())
            {
                YandexGame.savesData.AllCoins += CurrentQuest.Reward;

                UIManager.instance.UpdateCoinsText(YandexGame.savesData.AllCoins);

                QuestWasComplete = true;

                YandexGame.savesData.QuestWasComplete = QuestWasComplete;

                RewardPanel.SetActive(false);

                CompleteImage.SetActive(true);

                RewardPanelMenu.SetActive(false);

                CompleteImageMenu.SetActive(true);

                YandexGame.SaveProgress();
            }
        }
    }
}
