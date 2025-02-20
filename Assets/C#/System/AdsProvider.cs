using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class AdsProvider : MonoBehaviour
{
    public MenuSystem menuSystem;

    private void OnEnable()
    {
        YandexGame.RewardVideoEvent += Rewarded;
    }

    private void OnDisable()
    {
        YandexGame.RewardVideoEvent -= Rewarded;
    }

    private void Rewarded(int id) // после просмотра рекламы будет даваться награда под номером id
    {
        if(id == 1)
        {
            menuSystem.AddCoins(1000);//кол-во добавляемых монет за просмотр рекламы
        }

        else if(id == 2)
        {
            LevelManager.instance.FindRevivePlayerStats();// возраждает игрока 
        }

        else if(id == 3)
        {
            SaveSystem.instance.LevelComplete();//вызываем следующий уровень за рекламу

            UIManager.instance.UpdateLevelText(SaveSystem.instance.roadGenerator.LevelNow);
        }

        else if (id == 4)
        {
            LevelManager.instance.CanPushZombieBonus = true;
        }

        UIManager.instance.NotifyPanel.SetActive(true);
    }

    public void OpenRewardAd(int id)
    {
        YandexGame.RewVideoShow(id); //вызывает показ рекламы после которой будет вызвано событие по номеру id
    }
}
