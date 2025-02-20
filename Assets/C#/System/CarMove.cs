using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (rb != null)
        {
            rb.velocity = transform.forward * 10f; //скорость машин и направление
        }

        else
        {
            Destroy(this);
        }
    }
}
