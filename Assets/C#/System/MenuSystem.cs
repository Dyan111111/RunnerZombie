using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class MenuSystem : MonoBehaviour
{
    public int AllCoins = 0;

    public int RecordDistance = 0;

    public Text AllCoinsText;

    public Text RecordDistanceText;

    private void Start()
    {
        YandexGame.LoadProgress();

        AllCoins = YandexGame.savesData.AllCoins;

        RecordDistance = YandexGame.savesData.RecordDistance;

        AllCoinsText.text = AllCoins.ToString();

        RecordDistanceText.text = RecordDistance.ToString();
    }

    public void AddCoins(int value) // value это формальный параметр который будет задан при вызове данного метода 
    {
        AllCoins += value;

        AllCoinsText.text = AllCoins.ToString(); // обновление интерфейса монет

        YandexGame.savesData.AllCoins = AllCoins;

        YandexGame.SaveProgress();// сохроняет прогресс

    }
}
