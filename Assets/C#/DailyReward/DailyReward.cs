using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyReward : MonoBehaviour
{
    public GameObject ReadyPanel;

    public GameObject BlockPanel;

    public GameObject CompletePanel;

    public int Reward = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenReward()
    {
        ReadyPanel.SetActive(true);

        BlockPanel.SetActive(false);
    }

    public void CloseReward()
    {
        ReadyPanel.SetActive(false);

        BlockPanel.SetActive(true);
    }
}
