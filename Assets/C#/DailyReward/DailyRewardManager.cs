using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class DailyRewardManager : MonoBehaviour
{
    public DailyReward[] DailyRewards;

    public Animator DailyRewardAnimator;

    private int RewardIndex = 3;
    // Start is called before the first frame update
    void Start()
    {
        YandexGame.LoadProgress();

        RewardIndex = YandexGame.savesData.DailyRewardIndex;

        for(int i = 0; i <= RewardIndex; i++)
        {
            DailyRewards[i].OpenReward();

            if (YandexGame.savesData.DailyRewardComplete[i] == true)
            {
                DailyRewards[i].CompletePanel.SetActive(true);
            }
        }

        if (DailyRewards[RewardIndex].CompletePanel.activeSelf == false)
        {
            DailyRewardAnimator.SetBool("isReward", true);
        }

        else
        {
            DailyRewardAnimator.SetBool("isReward", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetReward(int index)
    {
        if (YandexGame.savesData.DailyRewardComplete[index] == false)
        {
            if (DailyRewards[index].ReadyPanel.activeSelf == true)
            {
                YandexGame.savesData.AllCoins += DailyRewards[index].Reward;

                UIManager.instance.UpdateCoinsText(YandexGame.savesData.AllCoins);

                YandexGame.savesData.DailyRewardComplete[index] = true;

                DailyRewards[index].CompletePanel.SetActive(true);

                DailyRewardAnimator.SetBool("isReward", true);

                YandexGame.SaveProgress();
            }
        }
    }
}
