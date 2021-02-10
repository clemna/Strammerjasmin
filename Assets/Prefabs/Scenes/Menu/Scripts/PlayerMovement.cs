using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement player;

    public CharacterController2D controller;

    public Animator animator;

    public Health hp;

    //public AudioManager audiomanager;

    public float runSpeed = 40f;

    private float lastDirection = 0;

    //public float jumpTimeCounter;
    //public float jumpTime;

    float horizontalMove = 0f;
    bool jump = false;
    bool dash = false;
    private bool canmove = true;

    private void Awake()
    {
        player = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp.GetCurrent() >= 1 && canmove == true)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

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

                //audiomanager.Play("Jump");
                //animator.SetBool("Jump", true);
            }



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
            /*if (Mathf.Abs(horizontalMove) > 0 && AudioManager.instance != null && jump == false && dash == false)
            {
                AudioManager.instance.Play("Steps");
            }*/
        }
        else
        {
            horizontalMove = 0;
        }
    }


    public void OnLanding()
    {
        animator.SetBool("Jump", false);
        animator.SetBool("IsDoubleJumping", false);
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump, dash, lastDirection);
        jump = false;
        dash = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("movingPlatform"))
        {
            this.transform.parent = collision.transform;
        }
        if (collision.gameObject.tag.Equals("Exit"))
        {
            canmove = false;
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
