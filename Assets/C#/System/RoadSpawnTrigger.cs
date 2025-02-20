using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawnTrigger : MonoBehaviour
{
    public RoadGenerator roadGenerator;

    private void OnTriggerEnter(Collider other)// вызывается один раз когда объект входит в триггер
    {
        if (other.CompareTag("Player"))
        {
            if(roadGenerator.ChooseCityIndex == 0)
            {
                roadGenerator.VillageGenerate();
            }

            else if(roadGenerator.ChooseCityIndex == 1)
            {
                roadGenerator.RoadGenerate();
            }

            else if (roadGenerator.ChooseCityIndex == 2)
            {
                roadGenerator.IndustriaGenerate();
            }

            else if (roadGenerator.ChooseCityIndex == 3)
            {
                roadGenerator.DayVillageGenerate();
            }
        }
    }
}
