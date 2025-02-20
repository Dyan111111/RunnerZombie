using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;

    public ScoreManager scoreManager;

    public PlayerMove playerMove;

    public RoadGenerator roadGenerator;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SaveData()
    {
        YandexGame.savesData.AllCoins += scoreManager.SessionCoins;

        if((int)playerMove.transform.position.z > YandexGame.savesData.RecordDistance)
        {
            YandexGame.savesData.RecordDistance = (int)playerMove.transform.position.z;
        }

        YandexGame.SaveProgress();
    }

    public void LevelComplete()
    {
        if(roadGenerator.LevelNow + 1 <= roadGenerator.levelMax)
        {
            roadGenerator.LevelNow += 1;

            YandexGame.NewLeaderboardScores("Leaderboard", roadGenerator.LevelNow);

            YandexGame.savesData.LevelsSave = roadGenerator.LevelNow;

            YandexGame.SaveProgress();
        }
    }
 
}
