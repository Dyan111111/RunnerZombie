using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;

            other.transform.eulerAngles = new Vector3(0f, 180f, 0f);

            Camera.main.GetComponent<CameraFollow>().SetTargetFinish();

            UIManager.instance.OpenFinishPanel();

            SaveSystem.instance.LevelComplete();

            LevelManager.instance.IsGameStart = false;
        }
    }
}
