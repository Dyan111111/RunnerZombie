using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusTrigger : MonoBehaviour
{
    public string StatusName;

    public float Duration;

    public Sprite StatusIcon;

    public GameObject StatusPrefab;

    private SoundManager soundManager;//если не сработает удалить 

    private void Start()//если не сработает удалить 
    {
        soundManager = SoundManager.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StatusSystem statusSystem = StatusSystem.Instance;

            Status AddStatus = other.GetComponent<Status>();

            if (statusSystem.HasStatus(StatusName))
            {
                AddStatus.ExtandDuration(Duration);
            }

            else
            {
                statusSystem.SetStatus(StatusName, true);

                GameObject UIStatus = Instantiate(StatusPrefab, statusSystem.ParentStatus);

                UIStatus.transform.GetChild(0).GetComponent<Image>().sprite = StatusIcon;

                UIStatus.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = StatusIcon;

                Destroy(UIStatus, Duration + 1);

                Status newStatus = other.gameObject.AddComponent<Status>();

                newStatus.StatusImage = UIStatus.transform.GetChild(0).GetChild(0).GetComponent<Image>();

                newStatus.StatusName = StatusName;

                newStatus.Duration = Duration;

                newStatus.Init();
            }

            soundManager.PlayShot(soundManager.StatusSound);//если не сработает удалить 

            Destroy(this.gameObject);

        }
    }
}

public enum StatusType
{
    Baseballbet,

    Magnet,

    X2
}
