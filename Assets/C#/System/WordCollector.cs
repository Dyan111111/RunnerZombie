using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using Random = UnityEngine.Random;
using DG.Tweening;

public class WordCollector : MonoBehaviour
{
    public static WordCollector instance;

    public List<string> words = new List<string>()
    {
        "Cat",
        "Alphabet",
        "Dog",
        "Frog"
    };

    public LetterDictionary letterDictionary;

    public GameObject currentLetterPrefab;

    public bool canCreateLetter = false;

    public TextMeshProUGUI LetterText;

    public RectTransform LetterPanelTransform;

    public bool IsWordEnd
    {
        get
        {
            if (currentLetterIndex < currentWord.Length)
            {
                return false;
            }

            else
            {
                return true;
            }
        }
    }

    private string currentWord;

    private int currentLetterIndex = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GenerateNewWord();
    }

    public void GenerateNewWord()
    {
        currentWord = words[Random.Range(0, words.Count)];

        currentLetterIndex = 0;

        GenerateNextLetter();

        CheckLetterColor();
    }

    private void GenerateNextLetter()
    {
        if (currentLetterIndex < currentWord.Length)
        {
            char letterToGenerate = Char.ToUpper(currentWord[currentLetterIndex]);

            for (int i = 0; i < letterDictionary.Letters.Length; i++)
            {
                if (letterDictionary.Letters[i] == letterToGenerate)
                {
                    currentLetterPrefab = letterDictionary.LettersPrefab[i];

                    break;
                }
            }
        }

        else
        {
            //Слово закончено
        }
    }

    public void CollectLetter()
    {
        if (currentLetterIndex < currentWord.Length)
        {
            currentLetterIndex++;

            GenerateNextLetter();

            CheckLetterColor();

            ShowWordPanel();
        }
    }

    public void ShowWordPanel()
    {
        Vector2 startPosition = LetterPanelTransform.anchoredPosition;

        // Анимация спуска панели
        LetterPanelTransform.DOAnchorPosY(LetterPanelTransform.anchoredPosition.y - 600f, 1f)
            .SetEase(Ease.InFlash) // Можно выбрать другой тип easing
            .OnComplete(() =>
            {
                // После завершения спуска, ждем 4 секунды и возвращаем панель обратно
                DOVirtual.DelayedCall(4f, () =>
                {
                    LetterPanelTransform.DOAnchorPosY(startPosition.y, 1f)
                        .SetEase(Ease.InBack); // Можно выбрать другой тип easing
                });
            });
    }

    private void CheckLetterColor()
    {
        string convertText = currentWord;

        convertText = convertText.Insert(currentLetterIndex, "</color>");

        convertText = convertText.Insert(0, "<color=#54eb66>");

        LetterText.text = convertText;
    }
}

[Serializable]

public class LetterDictionary
{
    public GameObject[] LettersPrefab;

    public char[] Letters = new char[]
    {
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
    'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
    };
}
