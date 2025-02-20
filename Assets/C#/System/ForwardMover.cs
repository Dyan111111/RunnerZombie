using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardMover : MonoBehaviour
{
    public float speed = 6f;

    public Rigidbody RB;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RB.velocity = new Vector3(0f, RB.velocity.y, speed);// RB.velocity.y - скорость по y

    }
}
