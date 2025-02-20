using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseGun : MonoBehaviour
{
    public string GunName;

    public GameObject[] GunsPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < GunsPrefabs.Length; i++)
        {
            GunsPrefabs[i].SetActive(false);
        }

        if (GunName == "Baseballbet")
        {
            GunsPrefabs[0].SetActive(true);
        }

        else if (GunName == "Knife")
        {
            GunsPrefabs[1].SetActive(true);
        }

        else if (GunName == "Pistol")
        {
            GunsPrefabs[2].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
