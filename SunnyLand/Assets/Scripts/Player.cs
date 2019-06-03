using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

public class Player : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float gravity = -10f;
    public float jumpHeight = 7f;
    public float centreRadius = .1f;


    private CharacterController2D controller;
    private SpriteRenderer rend;
    private Animator anim;
    private bool isClimbing = false;


    private Vector3 velocity;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, centreRadius);
    }
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController2D>();
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Gets inputs for movement left/right
        float inputH = Input.GetAxis("Horizontal");
        //Gets up and down input
        float inputV = Input.GetAxis("Vertical");
        //If controller is not grounded
        if (!controller.isGrounded)
        {
            //Apply delta to gravity
            velocity.y += gravity * Time.deltaTime;
        }
        //get jump input
        bool isJumping = Input.GetButtonDown("Jump");

        //if player presses jump
        if (isJumping)
        {
            //make the player jump
            Jump();
            
        }

        anim.SetBool("IsGrounded", controller.isGrounded);
        anim.SetFloat("JumpY", velocity.y);

        Run(inputH);
        Climb(inputV);
        //applies velocity to controller (to get it to move)
        controller.move(velocity * Time.deltaTime);
    }

    void Run(float inputH)
    {
        //sets character controller left and right with inputs
        controller.move(transform.right * inputH * moveSpeed * Time.deltaTime);
        //set bool to true if input is pressed
        bool isRunning = inputH != 0;
        //animate the player to running if input is pressed
        anim.SetBool("IsRunning", isRunning);



        //check if input is pressed
        if (isRunning)
        {
            //Flip character depending on left/right input
            rend.flipX = inputH < 0;
        }
        //rend.flipX = inputH > 0
    }

    void Climb(float inputV)
    {
        //is overlapping ladder
        bool isOverLadder = false;
        // is in climbing state
        //check if point overlaps a climbable object
        Collider2D[] hits = Physics2D.OverlapPointAll(transform.position);
        //if  is over climbable and input V has been made
        foreach (var hit in hits)
        {
            //if point overlaps a climbable object
            if(hit.tag == "Ladder")
            {
                isOverLadder = true;
                break;
            }
        }

        if(isOverLadder && inputV != 0)
        {
            isClimbing = true;
        }
        //is climbing
        //if is climbing
        //perform logic for climbing    
    }

    void Jump()
    {
        //set velocity Y to height
        velocity.y = jumpHeight;
    }
}
