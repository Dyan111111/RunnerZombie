using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathTrigger : MonoBehaviour
{
    private SoundManager soundManager;//удалить если не сработает 

    private void Start()
    {
        soundManager = SoundManager.instance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (this.CompareTag("Zombie"))
            {
                if (PlayerStats.instance.CanPushZombie == true || LevelManager.instance.CanPushZombieBonus == true)
                {
                    if (GetComponent<ZombieMove>())
                    {
                        GetComponent<ZombieMove>().enabled = false;
                    }

                    if(LevelManager.instance.GunName == "Pistol")
                    {
                        SoundManager.instance.PlayShot(SoundManager.instance.PistolSound);
                    }

                    else if (LevelManager.instance.GunName == "Baseballbet")
                    {
                        SoundManager.instance.PlayShot(SoundManager.instance.BaseballBetSound);
                    }

                    else if (LevelManager.instance.GunName == "Knife")
                    {
                        SoundManager.instance.PlayShot(SoundManager.instance.KnifeSound);
                    }

                    LevelManager.instance.Player.GetComponent<Animator>().SetTrigger("isAttack");//добавляет аниматор атаки 

                    Vector3 knockbackDirection = GetKnockbackDirection();
                    ApplyKnockback(knockbackDirection);

                    ScoreManager.instance.SessionCoins += 5; //начисляется за убийство зомби 

                    ScoreManager.instance.UpdateUI();

                    if (QuestSystem.instance.CurrentQuest.questType == QuestType.Zombie)
                    {
                            QuestSystem.instance.AddNumberToQuest(1);
                    }

                    Destroy(this.gameObject, 2f);

                    return;
                }
                else
                {
                    if (GetComponent<Rigidbody>())
                    {
                        GetComponent<Rigidbody>().velocity = Vector3.zero;
                    }

                    if (GetComponent<ZombieMove>())
                    {
                        GetComponent<ZombieMove>().enabled = false;
                    }

                    GetComponent<Animator>().SetBool("isAttack", true);
                }
            }

            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            soundManager.PlayShot(soundManager.HitSound);//удалить если не сработает 

            PlayerStats.instance.Death();
        }
    }


    private Vector3 GetKnockbackDirection()
    {
        // Выбор направления случайно (влево или вправо)
        float randomDirection = Random.Range(-1f, 1f);
        // Определяем вектор отталкивания на основе вектора вправо от игрока
        return new Vector3(randomDirection, 1f,0.4f).normalized;
    }

    private void ApplyKnockback(Vector3 knockbackDirection)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.freezeRotation = false;
        float knockbackForce = 2000f;  // Можно настроить значение
        rb.AddForce(knockbackDirection * knockbackForce);

        Vector3 randomRotation = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        rb.AddTorque(randomRotation * 1000f);
    }
}
