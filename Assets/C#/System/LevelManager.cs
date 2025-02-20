using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public GameObject Player;

    public float SpeedMultiple = 1f;

    public bool IsGameStart = false;

    public bool CanPushZombieBonus = false;

    public PlayerStats[] AllPlayerStats;

    public string GunName;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameStart == false) return;

        SpeedMultiple = Mathf.MoveTowards(SpeedMultiple, 10f, Time.deltaTime/120f);
    }

    public void StartGame()
    {
        Player.transform.eulerAngles = Vector3.zero;

        IsGameStart = true;
    }

    public void FindRevivePlayerStats()
    {
        for (int i = 0; i < AllPlayerStats.Length; i++)
        {
            if (AllPlayerStats[i].gameObject.activeSelf)
            {
                AllPlayerStats[i].Revive();

                Collider[] enemys = Physics.OverlapSphere(AllPlayerStats[i].transform.position, 10f);// создается сфера радиусом 10 метром и записывает в массив все компаненты имеющие компанент коллайдер

                for (int j = 0; j < enemys.Length; j++)
                {
                    if (enemys[j].GetComponent<DeathTrigger>() || enemys[j].GetComponent<DeathTriggerZone>()) 
                    {
                        Destroy(enemys[j].gameObject);
                    }
                }
            }
        }
    }
}
