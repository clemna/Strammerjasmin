using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement player;

    public CharacterController2D controller;

    public Animator animator;

    public AudioManager audiomanager;

    public float runSpeed = 40f;

    private float lastDirection = 0;

    float horizontalMove = 0f;
    bool jump = false;
    bool dash = false;

    private void Awake()
    {
        player = this;
    }

    // Update is called once per frame
    void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (Mathf.Abs(horizontalMove) > 0)
        {
            audiomanager.Play("Steps");
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
            //audiomanager.Play("Jump");
            //animator.SetBool("Jump", true);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            audiomanager.Play("Dash");
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
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("movingPlatform"))
        {
            this.transform.parent = null;
        }
    }

}
