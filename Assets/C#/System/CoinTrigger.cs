using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTrigger : MonoBehaviour
{
    private SoundManager soundManager;

    private void Start()
    {
        soundManager = SoundManager.instance;
    }

    private void Update()
    {
        if (PlayerStats.instance.CanMagnetCoins == false)
            return;

        if (Vector3.Distance(transform.position, PlayerStats.instance.transform.position) <= 5f)
        {
            transform.position = Vector3.Lerp(transform.position, PlayerStats.instance.transform.position, Time.deltaTime * 10f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerStats.instance.CanGetDoubleCoins == true)
            {
                ScoreManager.instance.SessionCoins += 2;

                if (QuestSystem.instance.CurrentQuest.questType == QuestType.Coin)
                {
                    QuestSystem.instance.AddNumberToQuest(2);
                }
            }

            else
            {
                ScoreManager.instance.SessionCoins++;

                if (QuestSystem.instance.CurrentQuest.questType == QuestType.Coin)
                {
                    QuestSystem.instance.AddNumberToQuest(1);
                }
            }

            soundManager.PlayShot(soundManager.CoinSound);

            ScoreManager.instance.UpdateUI();

            Destroy(this.gameObject);
        }
    }


}
