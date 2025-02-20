using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMove : MonoBehaviour
{
    private Rigidbody rigidbody;

    private Animator animator;

    private int speed;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        animator = GetComponent<Animator>();

        speed = Random.Range(0, 10);
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", rigidbody.velocity.magnitude);

        rigidbody.velocity = transform.forward * speed;
    }
}
