using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusSystem : MonoBehaviour
{
    public static StatusSystem Instance;

    public Transform ParentStatus;

    private Dictionary<string, bool> activeStatuses = new Dictionary<string, bool>();

    private void Awake()
    {
        Instance = this;
    }

    public bool HasStatus(string StatusName) //возвращает true если на нас есть статус бейсбольной биты, если нет вернет false
    {
        return activeStatuses.ContainsKey(StatusName) && activeStatuses[StatusName];
    }

    public void SetStatus(string StatusName, bool isActive)
    {
        activeStatuses[StatusName] = isActive;
    }
}
