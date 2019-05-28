using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

public class Player : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float gravity = -10f;

    private CharacterController2D controller;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");
        controller.move(transform.right * inputH * moveSpeed *Time.deltaTime);
       
    }

    void Move(float inputH, float inputV)
    {
        controller.move(transform.right * inputH * moveSpeed * Time.deltaTime);
        bool isRunning = inputH != 0;
        anim.SetBool("IsRunning", isRunning);

        //rend.flipX = inputH > 0
    }
}
