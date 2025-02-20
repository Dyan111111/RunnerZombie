using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
    public string StatusName;

    public float Duration = 30f;

    public float Timer;

    public Image StatusImage;

    public void Init()
    {
        Timer = Duration;

        if (StatusName == "Baseballbat")
        {
            PlayerStats.instance.CanPushZombie = true;

            PlayerStats.instance.Weapon.SetActive(true);
        }

        else if (StatusName == "Magnet")
        {
            PlayerStats.instance.CanMagnetCoins = true;

        }

        else if (StatusName == "X2")
        {
            PlayerStats.instance.CanGetDoubleCoins = true;

        }
    }

    private void Update()
    {
        Timer -= Time.deltaTime; // Time.deltaTime - время в милисекундах на отрисовку одного кадра

        if(StatusImage != null)
        {
            StatusImage.fillAmount = Timer / Duration; // fillAmount свойство компонента Image говорящее о заполненяемости картинки. Timer / Duration для того чтобы возвразщать значение находящееся в диапазоне от 0 до 1  
        }

        if (Timer <= 0f)
        {
            StatusSystem.Instance.SetStatus(StatusName, false);

            if (StatusName == "Baseballbat")
            {
                PlayerStats.instance.CanPushZombie = false;

                PlayerStats.instance.Weapon.SetActive(false);
            }

            else if (StatusName == "Magnet")
            {
                PlayerStats.instance.CanMagnetCoins = false;

            }

            else if (StatusName == "X2")
            {
                PlayerStats.instance.CanGetDoubleCoins = false;

            }

            Destroy(this);
        }
    }

    public void ExtandDuration(float AddTime)
    {
        Timer = AddTime;
    }
}
