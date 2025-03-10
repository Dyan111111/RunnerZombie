using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;
using System;
using Random = UnityEngine.Random;

public class RoadGenerator : MonoBehaviour
{
    [Header("Ночные")]

    public NightCityRoad NightCityPrefab;

    public GameObject[] SimpleCityPrefab;

    public NightVillageRoad NightVillagePrefab;

    public NightIndustrialRoad NightIndustrialPrefab;

    [Header("Дневные")]

    public GameObject[] DayVillagePrefab;

    public GameObject[] DayCityPrefab;

    public GameObject[] DayIndustrialPrefab;

    public GameObject FinishPrefab; 

    public Image TimerImage;

    public int LevelNow = 1;

    public int levelMax = 20;

    public int CountCityPrefab = 0;

    public int ChooseCityIndex = 0;

    public ChooseCharacter chooseCharacter;

    private float baseTimeToDaySpawn = 10f; //базовое время на при котором будет наступать день

    private float timeToDaySpawn = 10f; //таймер до дневной локации 

    private float timerGenerator = 0f;

    private bool isDay = false;

    private int PrefabTypeIndex = 0;

    private Vector3 SpawnPosition;

    private List<GameObject> Roads = new List<GameObject>();

    private void Start()
    {
        YandexGame.LoadProgress();

        LevelNow = YandexGame.savesData.LevelsSave;

        for (int i = 0; i < 6; i++) // запускаем цикл, который за секунду создает три дороги идущие друг за другом 

            SimpleRoadGenerate(); //вызываем метод для того чтобы при запуске игры дорога уже была

        timeToDaySpawn = baseTimeToDaySpawn + (LevelNow * 10f); //формуля для прибавления времени зависящая от уровня

        UIManager.instance.UpdateLevelText(LevelNow);

        //for (int i = 0; i < 2; i++) // запускаем цикл, который за секунду создает три дороги идущие друг за другом 

        //RoadGenerate(); //вызываем метод для того чтобы при запуске игры дорога уже была 
    }

    private void Update()
    {
        if (LevelManager.instance.IsGameStart == true)
        {
            if (isDay == false)
            {
                timerGenerator += Time.deltaTime;

                TimerImage.fillAmount = timerGenerator / timeToDaySpawn;

                if (timerGenerator >= timeToDaySpawn)
                {
                    isDay = true;

                    CountCityPrefab = 0;

                    ChooseCityIndex = 3;

                    Camera.main.backgroundColor = new Color(0.4f, 0.7f, 0.9f); //изменение цвета неба для дневной локации

                    RenderSettings.fogColor = new Color(0.4f, 0.7f, 0.9f);

                    CheckCityPrefabs();
                }
            }
        }   
    }

    public void RoadGenerate()//генерация города 
    {
        if (NightCityPrefab.NeedNightCityStay == false)
        {
            int rndStay = Random.Range(0, 4);

            if (rndStay == 0)
            {
                NightCityPrefab.NeedNightCityStay = true;
            }

            GameObject City = Instantiate(NightCityPrefab.CityPrefabMoveCar[Random.Range(0, NightCityPrefab.CityPrefabMoveCar.Length)], SpawnPosition, Quaternion.identity); //CityPrefab - это наш префаб, SpawnPosition - где мы спавнем префаб, Quaternion.identity - спавнем префаб в идентичном повороте нашей точки

            City.transform.GetChild(0).GetComponent<RoadSpawnTrigger>().roadGenerator = this;

            SpawnPosition = SpawnPosition + new Vector3(0f, 0f, 18f); // прибавляем смещение по z координате на 20 метров к точке спавна

            CountCityPrefab++;

            PrefabTypeIndex = 1;

            Roads.Add(City);

            DestroyRoad();

            CheckCityPrefabs();
        }

        else
        {
            GameObject City = Instantiate(NightCityPrefab.CityPrefabStayCar[Random.Range(0, NightCityPrefab.CityPrefabStayCar.Length)], SpawnPosition, Quaternion.identity); //CityPrefab - это наш префаб, SpawnPosition - где мы спавнем префаб, Quaternion.identity - спавнем префаб в идентичном повороте нашей точки

            City.transform.GetChild(0).GetComponent<RoadSpawnTrigger>().roadGenerator = this;

            SpawnPosition = SpawnPosition + new Vector3(0f, 0f, 18f); // прибавляем смещение по z координате на 20 метров к точке спавна

            CountCityPrefab++;

            PrefabTypeIndex = 1;

            Roads.Add(City);

            DestroyRoad();

            CheckCityPrefabs();
        }
     
    }

    public void IndustriaGenerate() //я добавила 
    {
        if (NightIndustrialPrefab.NeedNightIndustrialStay == false)
        {
            int rndStay = Random.Range(0, 4);

            if (rndStay == 0)
            {
                NightIndustrialPrefab.NeedNightIndustrialStay = true;
            }

            GameObject City = Instantiate(NightIndustrialPrefab.IndustrialPrefabMoveCar[Random.Range(0, NightIndustrialPrefab.IndustrialPrefabMoveCar.Length)], SpawnPosition, Quaternion.identity); //CityPrefab - это наш префаб, SpawnPosition - где мы спавнем префаб, Quaternion.identity - спавнем префаб в идентичном повороте нашей точки

            City.transform.GetChild(0).GetComponent<RoadSpawnTrigger>().roadGenerator = this;

            SpawnPosition = SpawnPosition + new Vector3(0f, 0f, 18f); // прибавляем смещение по z координате на 20 метров к точке спавна

            CountCityPrefab++;

            PrefabTypeIndex = 2;

            Roads.Add(City);

            DestroyRoad();

            CheckCityPrefabs();
        }

        else
        {
            GameObject City = Instantiate(NightIndustrialPrefab.IndustrialPrefabStayCar[Random.Range(0, NightIndustrialPrefab.IndustrialPrefabStayCar.Length)], SpawnPosition, Quaternion.identity); //CityPrefab - это наш префаб, SpawnPosition - где мы спавнем префаб, Quaternion.identity - спавнем префаб в идентичном повороте нашей точки

            City.transform.GetChild(0).GetComponent<RoadSpawnTrigger>().roadGenerator = this;

            SpawnPosition = SpawnPosition + new Vector3(0f, 0f, 18f); // прибавляем смещение по z координате на 20 метров к точке спавна

            CountCityPrefab++;

            PrefabTypeIndex = 2;

            Roads.Add(City);

            DestroyRoad();

            CheckCityPrefabs();
        }
       
    }

    public void VillageGenerate()
    {
        if (NightVillagePrefab.NeedNightVillageStay == false)
        {
            int rndStay = Random.Range(0, 4);

            if (rndStay == 0)
            {
                NightVillagePrefab.NeedNightVillageStay = true;
            }

            GameObject City = Instantiate(NightVillagePrefab.VillagePrefabMoveCar[Random.Range(0, NightVillagePrefab.VillagePrefabMoveCar.Length)], SpawnPosition, Quaternion.identity); //CityPrefab - это наш префаб, SpawnPosition - где мы спавнем префаб, Quaternion.identity - спавнем префаб в идентичном повороте нашей точки

            City.transform.GetChild(0).GetComponent<RoadSpawnTrigger>().roadGenerator = this;

            SpawnPosition = SpawnPosition + new Vector3(0f, 0f, 18f); // прибавляем смещение по z координате на 20 метров к точке спавна

            CountCityPrefab++;

            PrefabTypeIndex = 0;

            Roads.Add(City);

            DestroyRoad();

            CheckCityPrefabs();
        }

        else
        {
            GameObject City = Instantiate(NightVillagePrefab.VillagePrefabStayCar[Random.Range(0, NightVillagePrefab.VillagePrefabStayCar.Length)], SpawnPosition, Quaternion.identity); //CityPrefab - это наш префаб, SpawnPosition - где мы спавнем префаб, Quaternion.identity - спавнем префаб в идентичном повороте нашей точки

            City.transform.GetChild(0).GetComponent<RoadSpawnTrigger>().roadGenerator = this;

            SpawnPosition = SpawnPosition + new Vector3(0f, 0f, 18f); // прибавляем смещение по z координате на 20 метров к точке спавна

            CountCityPrefab++;

            PrefabTypeIndex = 0;

            Roads.Add(City);

            DestroyRoad();

            CheckCityPrefabs();
        }
    }

    public void SimpleRoadGenerate()// метод для спавна дорог без преград 
    {
        GameObject City = Instantiate(SimpleCityPrefab[Random.Range(0, SimpleCityPrefab.Length)], SpawnPosition, Quaternion.identity); //CityPrefab - это наш префаб, SpawnPosition - где мы спавнем префаб, Quaternion.identity - спавнем префаб в идентичном повороте нашей точки

        City.transform.GetChild(0).GetComponent<RoadSpawnTrigger>().roadGenerator = this;

        SpawnPosition = SpawnPosition + new Vector3(0f, 0f, 18f); // прибавляем смещение по z координате на 20 метров к точке спавна

        Roads.Add(City);
    }

    public void DayVillageGenerate()
    {
        if (CountCityPrefab < 10)
        {
            GameObject City = null;

            if (PrefabTypeIndex == 0)
            {
               City = Instantiate(DayVillagePrefab[Random.Range(0, DayVillagePrefab.Length)], SpawnPosition, Quaternion.identity); //CityPrefab - это наш префаб, SpawnPosition - где мы спавнем префаб, Quaternion.identity - спавнем префаб в идентичном повороте нашей точки
            }

            else if (PrefabTypeIndex == 1)
            {
                City = Instantiate(DayCityPrefab[Random.Range(0, DayCityPrefab.Length)], SpawnPosition, Quaternion.identity); //CityPrefab - это наш префаб, SpawnPosition - где мы спавнем префаб, Quaternion.identity - спавнем префаб в идентичном повороте нашей точки
            }

            else if (PrefabTypeIndex == 2)
            {
                City = Instantiate(DayIndustrialPrefab[Random.Range(0, DayIndustrialPrefab.Length)], SpawnPosition, Quaternion.identity); //CityPrefab - это наш префаб, SpawnPosition - где мы спавнем префаб, Quaternion.identity - спавнем префаб в идентичном повороте нашей точки
            }

            City.transform.GetChild(0).GetComponent<RoadSpawnTrigger>().roadGenerator = this;

            SpawnPosition = SpawnPosition + new Vector3(0f, 0f, 18f); // прибавляем смещение по z координате на 20 метров к точке спавна

            CountCityPrefab++;

            Roads.Add(City);

            DestroyRoad();
        }

        else
        {
            CheckCityFinish();
        }
    }

    private void CheckCityPrefabs()
    {
        if (CountCityPrefab >= 20) //количество генерируемях обектов города 
        {
            if(ChooseCityIndex < 2)
            {
                ChooseCityIndex++;
            }

            else
            {
                ChooseCityIndex = 0;
            }

            CountCityPrefab = 0;
        }
    }

    private void CheckCityFinish()
    {
        if (CountCityPrefab >= 4) //количество генерируемях обектов дневного города 
        {
            GameObject City = Instantiate(FinishPrefab, SpawnPosition, Quaternion.identity); //CityPrefab - это наш префаб, SpawnPosition - где мы спавнем префаб, Quaternion.identity - спавнем префаб в идентичном повороте нашей точки

            SpawnPosition = SpawnPosition + new Vector3(0f, 0f, 18f); // прибавляем смещение по z координате на 20 метров к точке спавна
        }
    }

    private void DestroyRoad()
    {
        if (Roads.Count > 7)
        {
            Destroy(Roads[0].gameObject);

            Roads.Remove(Roads[0]);
        }
    }

    public void ResetLevel()
    {
        LevelNow = 1;

        timeToDaySpawn = baseTimeToDaySpawn + (LevelNow * 10f); //формуля для прибавления времени зависящая от уровня

        UIManager.instance.UpdateLevelText(LevelNow);

        YandexGame.savesData.LevelsSave = 1;

        YandexGame.SaveProgress();
    }

    public void UpdateLevel()//обновляет текст для языка 
    {
        UIManager.instance.UpdateLevelText(LevelNow);
    }
}

[Serializable]

public class NightCityRoad
{
    public bool NeedNightCityStay = false;

    public int CountNightCityStay = 0;

    [Header("Едущие машины")]

    public GameObject[] CityPrefabMoveCar;

    [Header("Стоящие машины")]

    public GameObject[] CityPrefabStayCar;
}

[Serializable]

public class NightVillageRoad
{
    public bool NeedNightVillageStay = false;

    public int CountNightVillageStay = 0;

    [Header("Едущие машины")]

    public GameObject[] VillagePrefabMoveCar;

    [Header("Стоящие машины")]

    public GameObject[] VillagePrefabStayCar;
}

[Serializable]

public class NightIndustrialRoad
{
    public bool NeedNightIndustrialStay = false;

    public int CountNightIndustrialeStay = 0;

    [Header("Едущие машины")]

    public GameObject[] IndustrialPrefabMoveCar;

    [Header("Стоящие машины")]

    public GameObject[] IndustrialPrefabStayCar;
}

