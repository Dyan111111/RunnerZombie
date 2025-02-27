using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordCollectorTrigger : MonoBehaviour
{
    private void OnEnable()
    {
        WordCollector.instance.canCreateLetter = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        WordCollector.instance.CollectLetter();

        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        WordCollector.instance.canCreateLetter = true;
    }
}
