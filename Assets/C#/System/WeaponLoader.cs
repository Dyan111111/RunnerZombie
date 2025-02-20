using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLoader : MonoBehaviour
{
    public GameObject Weapon;

    private float weaponTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(LevelManager.instance.CanPushZombieBonus == true)
        {
            Weapon.SetActive(true);

            return;
        }

        else if (LevelManager.instance.IsGameStart == true)
        {
            weaponTimer += Time.deltaTime;

            if(weaponTimer >= 4f)
            {
                Weapon.SetActive(false);

                Destroy(this);
            }
        }
    }
}
