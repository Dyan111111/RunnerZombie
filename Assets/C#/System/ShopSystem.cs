using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class ShopSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyButton(int index)
    {
        if(index == 3)
        {
            if(ScoreManager.instance.Coins > 2)//проверяет есть ли две монеты за переход на след уровень
            {
                ScoreManager.instance.Coins -= 2; //вычетаем монеты

                YandexGame.savesData.AllCoins = ScoreManager.instance.Coins;

                UIManager.instance.UpdateCoinsText(ScoreManager.instance.Coins);

                SaveSystem.instance.LevelComplete();//вызываем следующий уровень 

                UIManager.instance.UpdateLevelText(SaveSystem.instance.roadGenerator.LevelNow);

                YandexGame.SaveProgress();
            }

            UIManager.instance.NotifyPanel.SetActive(true);
        }

        else if (index == 4)
        {
            if (ScoreManager.instance.Coins > 2)//проверяет есть ли две монеты за переход на след уровень
            {
                ScoreManager.instance.Coins -= 2; //вычетаем монеты

                YandexGame.savesData.AllCoins = ScoreManager.instance.Coins;

                UIManager.instance.UpdateCoinsText(ScoreManager.instance.Coins);

                LevelManager.instance.CanPushZombieBonus = true;

                YandexGame.SaveProgress();
            }

            UIManager.instance.NotifyPanel.SetActive(true);//вызывает галочку после просмотра рекламы или покупки 
        }
    }
}
