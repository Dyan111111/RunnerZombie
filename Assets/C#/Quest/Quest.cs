using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Quest
{
    [Header("Название задания")]

    [TextArea] public string QuestNameRu;

    [TextArea] public string QuestNameEn;

    [TextArea] public string QuestNameTr;

    [Header("Настройки")]

    public QuestType questType;

    public int Reward;

    public int QuestMax;

    public int QuestNow;
}

public enum QuestType
{
    Run,
    Zombie,
    Coin,
    Jump,
    Buster
}
