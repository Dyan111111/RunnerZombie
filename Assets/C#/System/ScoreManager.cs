using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;//instance позволяет брать из класса любые публичные поля и методы 

    public int Coins = 0;

    public int SessionCoins = 0;

    public Text coinText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        YandexGame.LoadProgress();

        Coins = YandexGame.savesData.AllCoins;
    }

    public void UpdateUI()//метод который обновляет интерфейс 
    {
        coinText.text = SessionCoins.ToString();
    }
   
}
