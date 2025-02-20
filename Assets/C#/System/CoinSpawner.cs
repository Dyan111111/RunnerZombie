using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class CoinSpawner : MonoBehaviour
{
    [Header("Точки Спавна")]

    public Transform[] SpawenPoints;

    [Header("Точки Противников")]

    public Transform[] EnemysPoints;

    [Header("Точки Машин")]

    public Transform[] CarsPoints;

    [Header("Префабы")]

    public GameObject CoinPrefab;

    public GameObject EnemyPrefab;

    [Header("Настраиваемые Характеристики")]

    public PointsSpawnerInfo pointsSpawnerInfo;

    [Header("Дочерний объект")]

    public Transform SpawnPoint;

    [HideInInspector]

    public ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        #region генерация монет 

        int rndLine = Random.Range(0, 3);

        bool doubleCoins = Random.Range(0, 6) == 0;// отвечает за то что сможет ли заспавниться две линии монет 

        int rndDoubleCoins = 0;// хранит номер линии если выпал шанс второй линии 

        if (doubleCoins == true)
        {
            while(rndDoubleCoins == rndLine)
            {
                rndDoubleCoins = Random.Range(0, 3);
            }

            for (int i = 0; i < SpawenPoints[rndLine].childCount; i++)
            {
                GameObject coin = Instantiate(CoinPrefab, SpawenPoints[rndLine].GetChild(i).transform.position, SpawenPoints[rndLine].GetChild(i).transform.rotation, SpawnPoint);
            }

            for (int i = 0; i < SpawenPoints[rndDoubleCoins].childCount; i++)
            {
                GameObject coin = Instantiate(CoinPrefab, SpawenPoints[rndDoubleCoins].GetChild(i).transform.position, SpawenPoints[rndDoubleCoins].GetChild(i).transform.rotation, SpawnPoint);
            }

        }

        else
        {
            for (int i = 0; i < SpawenPoints[rndLine].childCount; i++)
            {
                GameObject coin = Instantiate(CoinPrefab, SpawenPoints[rndLine].GetChild(i).transform.position, SpawenPoints[rndLine].GetChild(i).transform.rotation, SpawnPoint);
            }
        }

        #endregion 

        int rndObstaclePos = Random.Range(0, 3);
        if (rndObstaclePos != rndLine && doubleCoins == false)
        {
            int rndObstacle = Random.Range(0, 101);

            for (int i = 0; i < pointsSpawnerInfo.ObstaclePrefabs.Length; i++)
            {
                if (pointsSpawnerInfo.ObstaclePrefabs[i].Chance >= rndObstacle)
                {
                    Transform spawnPos = null;

                    for (int j = 0; j < 4; j++)
                    {
                        spawnPos = SpawenPoints[rndObstaclePos].GetChild(Random.Range(0, SpawenPoints[rndObstaclePos].childCount)).transform;

                        if (spawnPos.position.y <= 1f)
                        {
                            break;
                        }
                    }

                    if (spawnPos.position.y <= 1f)
                    {
                        GameObject obstacle = Instantiate(pointsSpawnerInfo.ObstaclePrefabs[i].ObstaclePrefab, spawnPos.position, spawnPos.rotation, SpawnPoint);

                        obstacle.transform.position += new Vector3(0f, -0.8f, 0f);

                        break;
                    }
                }
            }
        }

        int rndChance = Random.Range(0, 4);//шанс на появление какоего либу бустера 

        if (rndChance == 0 && pointsSpawnerInfo.StatusPrefabs.Length > 0 && doubleCoins == false)
        {
            for (int i = 0; i < SpawenPoints.Length; i++)
            {
                if (i != rndLine && i != rndObstaclePos)
                {
                    int rndStatusChance = Random.Range(0, 10);

                    if(rndStatusChance >= 0 && rndStatusChance < 4)// шанс на бейсболную биту 
                    {
                        GameObject baseballbet = Instantiate(pointsSpawnerInfo.StatusPrefabs[0], SpawenPoints[i].GetChild(Random.Range(0, 4)).transform.position, SpawenPoints[i].GetChild(Random.Range(0, 4)).transform.rotation, SpawnPoint);

                        baseballbet.GetComponent<ChooseGun>().GunName = LevelManager.instance.GunName;
                    }

                    else if (rndStatusChance >= 4 && rndStatusChance < 6)
                    {
                        GameObject magnet = Instantiate(pointsSpawnerInfo.StatusPrefabs[1], SpawenPoints[i].GetChild(Random.Range(0, 4)).transform.position, SpawenPoints[i].GetChild(Random.Range(0, 4)).transform.rotation, SpawnPoint);
                    }

                    else
                    {
                        GameObject x2 = Instantiate(pointsSpawnerInfo.StatusPrefabs[2], SpawenPoints[i].GetChild(Random.Range(0, 4)).transform.position, SpawenPoints[i].GetChild(Random.Range(0, 4)).transform.rotation, SpawnPoint);
                    }

                    break;
                }
            }
        }

        int rndCarChance = Random.Range(0, 10);//генерация машин 

        if (rndCarChance >= 0 && rndCarChance <= 7) //дает 70% генерации машины
        {
            Transform carPoint = CarsPoints[Random.Range(0, CarsPoints.Length)];

            int rndCar = Random.Range(0, 101);

            if (pointsSpawnerInfo.CarPrefabs.Length > 0)
            {
                for (int i = 0; i < pointsSpawnerInfo.CarPrefabs.Length; i++)
                {
                    if (pointsSpawnerInfo.CarPrefabs[i].Chance >= rndCar)
                    {
                        GameObject car = Instantiate(pointsSpawnerInfo.CarPrefabs[i].CarPrefab, carPoint.position, carPoint.rotation, SpawnPoint);

                        car.transform.position -= new Vector3(0f, 1f, 0f);

                        car.AddComponent<CarMove>();

                        break;
                    }
                }
            }
        }

        int rndEnemyChance = Random.Range(0, 10);// генерация зомби 

        if (rndEnemyChance == 0 && rndEnemyChance <= 7)//дает 70% генерации зомби
        {
            int rndEnemyPos = Random.Range(0, EnemysPoints.Length);

            if (EnemyPrefab != null)
            {
                GameObject enemy = Instantiate(EnemyPrefab, EnemysPoints[rndEnemyPos].position, EnemysPoints[rndEnemyPos].rotation, SpawnPoint);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}


[Serializable]
public class PointsSpawnerInfo
{
    public GameObject[] StatusPrefabs;

    public ObstacleInfo[] ObstaclePrefabs;

    public CarInfo[] CarPrefabs;
}

[Serializable]
public class CarInfo
{
    public GameObject CarPrefab;

    public int Chance;
}

[Serializable]
public class ObstacleInfo
{
    public GameObject ObstaclePrefab;

    public int Chance;
}
