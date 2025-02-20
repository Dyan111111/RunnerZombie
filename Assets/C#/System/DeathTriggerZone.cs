using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTriggerZone : MonoBehaviour
{
    private SoundManager soundManager;//удалить если не сработает 

    private void Start()
    {
        soundManager = SoundManager.instance;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            PlayerStats.instance.Death();

            soundManager.PlayShot(soundManager.HitSound);//удалить если не сработает 
        }
    }
}
