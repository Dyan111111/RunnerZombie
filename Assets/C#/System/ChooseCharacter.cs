using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;
using UnityEngine.Events;

public class ChooseCharacter : MonoBehaviour
{
    public GameObject[] Characters;

    public Transform CameraPoint;

    public Transform MenuCameraPoint;

    public GameObject ShopPanel;

    public Button _BuyButton;

    public Text PriceText;

    public Text LevelText;

    public CameraFollow cameraFollow;

    public string GunName;

    public UnityEvent CanBePlay;

    public Animator[] CharactersAnimators;

    private int[] prices = new int[4] { 0, 1000, 2000, 3000 };

    private int[] levels = new int[4] { 0, 3, 6, 9 };

    private bool[] wasBought = new bool[4] { true, false, false, false };

    private int CurrentIndex = 0; //номер персонажа

    private int allMoney;

    private int CurrentLevel;

    // Start is called before the first frame update
    void Start()
    {
        YandexGame.LoadProgress();

        allMoney = YandexGame.savesData.AllCoins;

        wasBought = YandexGame.savesData.wasBought;

        CurrentLevel = SaveSystem.instance.roadGenerator.LevelNow;

        CurrentIndex = YandexGame.savesData.PlayerIndex;

        CheckPlayers();

        UpdatePriceText();

        CheckBuyButton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseCharacterGun(int index)
    {
        if(index == 0)
        {
            GunName = "Baseballbet";

            LevelManager.instance.GunName = GunName;
        }

        else if (index == 1)
        {
            GunName = "Pistol";

            LevelManager.instance.GunName = GunName;
        }

        else if (index == 2)
        {
            GunName = "Pistol";

            LevelManager.instance.GunName = GunName;
        }

        else if (index == 3)
        {
            GunName = "Knife";

            LevelManager.instance.GunName = GunName;
        }
    }

    public void ChooseLeftCharacter()
    {
        DiactivateAllCharacters();

        if(CurrentIndex - 1 >= 0)
        {
            CurrentIndex--;
        }

        else
        {
            CurrentIndex = Characters.Length - 1;
        }

        Characters[CurrentIndex].SetActive(true);

        cameraFollow.SetPlayer(Characters[CurrentIndex]);

        CameraPoint.SetParent(Characters[CurrentIndex].transform, false);

        MenuCameraPoint.SetParent(Characters[CurrentIndex].transform, false);

        LevelManager.instance.Player = Characters[CurrentIndex];

        CharactersAnimators[CurrentIndex].SetTrigger("isAttack");

        ChooseCharacterGun(CurrentIndex);

        UpdatePriceText();

        CheckBuyButton();
    }

    public void ChooseRightCharacter()
    {
        DiactivateAllCharacters();

        if (CurrentIndex + 1 < Characters.Length)
        {
            CurrentIndex++;
        }

        else
        {
            CurrentIndex = 0;
        }

        Characters[CurrentIndex].SetActive(true);

        cameraFollow.SetPlayer(Characters[CurrentIndex]);

        CameraPoint.SetParent(Characters[CurrentIndex].transform,false);

        MenuCameraPoint.SetParent(Characters[CurrentIndex].transform, false);

        LevelManager.instance.Player = Characters[CurrentIndex];

        CharactersAnimators[CurrentIndex].SetTrigger("isAttack");

        ChooseCharacterGun(CurrentIndex);

        UpdatePriceText();

        CheckBuyButton();
    }

    private void CheckPlayers()
    {
        DiactivateAllCharacters();

        Characters[CurrentIndex].SetActive(true);

        cameraFollow.SetPlayer(Characters[CurrentIndex]);

        CameraPoint.SetParent(Characters[CurrentIndex].transform, false);

        MenuCameraPoint.SetParent(Characters[CurrentIndex].transform, false);

        LevelManager.instance.Player = Characters[CurrentIndex];

        CharactersAnimators[CurrentIndex].SetTrigger("isAttack");

        ChooseCharacterGun(CurrentIndex);

    }

    public void BuyButton()
    {
        if (wasBought[CurrentIndex] == true)
        {
            CanBePlay?.Invoke();
        }

        else
        {
            if (allMoney >= prices[CurrentIndex] && CurrentLevel >= levels[CurrentIndex])
            {
                allMoney -= prices[CurrentIndex];

                YandexGame.savesData.AllCoins = allMoney;

                UIManager.instance.UpdateCoinsText(allMoney);

                wasBought[CurrentIndex] = true;

                YandexGame.savesData.wasBought[CurrentIndex] = true;

                UpdatePriceText();

                CheckBuyButton();

                YandexGame.SaveProgress();
            }

            else
            {
                ShopPanel.SetActive(true);
            }
        }
    }

    private void DiactivateAllCharacters()
    {
        for (int i = 0; i < Characters.Length; i++)
        {
            Characters[i].SetActive(false);
        }
    }

    public void UpdatePriceText()
    {
        if (wasBought[CurrentIndex] == true)
        {
            PriceText.enabled = false;

            LevelText.enabled = false;
        }

        else
        {
            PriceText.enabled = true;

            LevelText.enabled = true;

            if (YandexGame.lang == "ru")
            {

                if (allMoney >= prices[CurrentIndex])// хвататет денег 
                {
                    PriceText.text = $"Цена: <color=#74d945>{prices[CurrentIndex]}</color>";//зеленый
                }

                else
                {
                    PriceText.text = $"Цена: <color=#d94d45>{prices[CurrentIndex]}</color>";//красный
                }

                if (CurrentLevel >= levels[CurrentIndex])// хвататет уровня
                {
                    LevelText.text = $"Уровень: <color=#74d945>{levels[CurrentIndex]}</color>";
                }

                else
                {
                    LevelText.text = $"Уровень: <color=#d94d45>{levels[CurrentIndex]}</color>";
                }

            }

            else if (YandexGame.lang == "en")
            {

                if (allMoney >= prices[CurrentIndex])// хвататет денег 
                {
                    PriceText.text = $"Price: <color=#74d945>{prices[CurrentIndex]}</color>";//зеленый
                }

                else
                {
                    PriceText.text = $"Price: <color=#d94d45>{prices[CurrentIndex]}</color>";//красный
                }

                if (CurrentLevel >= levels[CurrentIndex])// хвататет уровня
                {
                    LevelText.text = $"Level: <color=#74d945>{levels[CurrentIndex]}</color>";
                }

                else
                {
                    LevelText.text = $"Level: <color=#d94d45>{levels[CurrentIndex]}</color>";
                }

            }

            else if (YandexGame.lang == "tr")
            {

                if (allMoney >= prices[CurrentIndex])// хвататет денег 
                {
                    PriceText.text = $"Fiyat: <color=#74d945>{prices[CurrentIndex]}</color>";//зеленый
                }

                else
                {
                    PriceText.text = $"Fiyat: <color=#d94d45>{prices[CurrentIndex]}</color>";//красный
                }

                if (CurrentLevel >= levels[CurrentIndex])// хвататет уровня
                {
                    LevelText.text = $"Seviye: <color=#74d945>{levels[CurrentIndex]}</color>";
                }

                else
                {
                    LevelText.text = $"Seviye: <color=#d94d45>{levels[CurrentIndex]}</color>";
                }

            }

        }

    }

    public void CheckBuyButton()
    {
        Text BuyButtonText = _BuyButton.GetComponentInChildren<Text>();

        if (wasBought[CurrentIndex] == true)
        {
            BuyButtonText.color = Color.black;

            if(YandexGame.lang == "ru")
            {
                BuyButtonText.text = "Играть";
            }

            else if (YandexGame.lang == "en")
            {
                BuyButtonText.text = "Play";
            }

            else if (YandexGame.lang == "tr")
            {
                BuyButtonText.text = "Oynamak";
            }

            YandexGame.savesData.PlayerIndex = CurrentIndex;

            YandexGame.SaveProgress();
        }

        else
        {
            if(allMoney >= prices[CurrentIndex] && CurrentLevel >= levels[CurrentIndex])
            {
                BuyButtonText.color = Color.green;

                if (YandexGame.lang == "ru")
                {
                    BuyButtonText.text = "Купить";
                }

                else if (YandexGame.lang == "en")
                {
                    BuyButtonText.text = "Buy";
                }

                else if (YandexGame.lang == "tr")
                {
                    BuyButtonText.text = "Satın almak";
                }
            }

            else
            {
                BuyButtonText.color = Color.red;

                if (YandexGame.lang == "ru")
                {
                    BuyButtonText.text = "Купить";
                }

                else if (YandexGame.lang == "en")
                {
                    BuyButtonText.text = "Buy";
                }

                else if (YandexGame.lang == "tr")
                {
                    BuyButtonText.text = "Satın almak";
                }
            }
        }
    }
}
