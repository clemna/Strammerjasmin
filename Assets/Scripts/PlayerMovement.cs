using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement player;

    public CharacterController2D controller;

    public Animator animator;

    //public AudioManager audiomanager;

    public float runSpeed = 40f;

    private float lastDirection = 0;

    //public float jumpTimeCounter;
    //public float jumpTime;

    float horizontalMove = 0f;
    bool jump = false;
    bool dash = false;
    public bool jumpheld = false;

    private void Awake()
    {
        player = this;
    }

    // Update is called once per frame
    void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (Mathf.Abs(horizontalMove) > 0 && AudioManager.instance != null)
        {
            AudioManager.instance.Play("Steps");
        }
        animator.SetFloat("Walk", Mathf.Abs(horizontalMove));
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            lastDirection = 1;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            lastDirection = -1;
        }


        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            jumpheld = true;
            //audiomanager.Play("Jump");
            //animator.SetBool("Jump", true);
        }
        else if (Input.GetButtonUp("Jump"))
        {
            jumpheld = false;
        }

        

        /*if (Input.GetButton("Jump") && jump == true)
        {
            if (jumpTimeCounter > 0)
            {
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                jump = false;
            }
        }*/
        

        if (Input.GetButtonUp("Jump"))
        {
            jump = false;
        }

        if (Input.GetButtonDown("Dash"))
        {
            if (AudioManager.instance != null)
            {
                AudioManager.instance.Play("Dash");
                //audiomanager.Play("Dash");
            }
            
            dash = true;
        }

    }


    public void OnLanding()
    {
        animator.SetBool("Jump", false);
        animator.SetBool("IsDoubleJumping", false);
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump, dash, lastDirection, jumpheld);
        jump = false;
        dash = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("movingPlatform"))
        {
            this.transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("movingPlatform"))
        {
            this.transform.parent = null;
        }
    }

}
