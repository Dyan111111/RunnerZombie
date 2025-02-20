using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance; 

    public GameObject Weapon;

    public GameObject StatusPanel;

    public Image StatusImage;

    public bool CanPushZombie = false;

    public bool CanMagnetCoins = false;

    public bool CanGetDoubleCoins = false;

    public CameraFollow cameraFollow;

    public GameObject StatusBackgroudPrefab;

    public Transform StatusParent; //родитель для создаваемого статуса (бустера)

    private float TimerStatusEffect = 20f; // время работы бустера

    private bool isStatus = false;

    private Animator animator;

    private Rigidbody rb;

    private PlayerMove playerMove;

    private StatusType currentStatusType;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        rb = GetComponent<Rigidbody>();

        playerMove = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isStatus == true)
        {
            TimerStatusEffect -= Time.deltaTime;

            StatusImage.fillAmount = TimerStatusEffect / 20f;

            if (TimerStatusEffect <= 0f)
            {
                isStatus = false;

                if (currentStatusType == StatusType.Baseballbet)
                {
                    CanPushZombie = false;

                    Weapon.SetActive(false);
                }

                else if (currentStatusType == StatusType.Magnet)
                {
                    CanMagnetCoins = false;
                }

                else if (currentStatusType == StatusType.X2)
                {
                    CanGetDoubleCoins = false;
                }

                StatusPanel.SetActive(false);

                TimerStatusEffect = 20f;
            }
        }
    }

    public void SetStatus(float time, StatusType statusType)
    {
        isStatus = true;

        currentStatusType = statusType;

        if(currentStatusType == StatusType.Baseballbet)
        {
            CanPushZombie = true;

            Weapon.SetActive(true);
        }

        else if (currentStatusType == StatusType.Magnet)
        {
            CanMagnetCoins = true;
        }
        else if (currentStatusType == StatusType.X2)
        {
            CanGetDoubleCoins = true;
        }

        StatusPanel.SetActive(true);

        GameObject Status = Instantiate(StatusBackgroudPrefab, StatusParent);

        Destroy(Status, 20f); //уничтожаем статус (бустер) из интерфейса через 20 секунд

        TimerStatusEffect = time;
    }

    public void Death()
    {
        playerMove.enabled = false;

        rb.velocity = Vector3.zero;

        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

        UIManager.instance.DeathPanel.SetActive(true);

        cameraFollow.Offset += new Vector3(0f, -2f, 0f);

        animator.SetBool("isDead", true);

        SaveSystem.instance.SaveData();
    }

    public void Revive() //метод для воскрешения 
    {
        playerMove.enabled = true;

        rb.constraints = RigidbodyConstraints.FreezeRotation;

        UIManager.instance.DeathPanel.SetActive(false);

        cameraFollow.Offset -= new Vector3(0f, -2f, 0f);

        animator.SetBool("isDead", false);

        SaveSystem.instance.SaveData();
    }
}
