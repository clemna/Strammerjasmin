using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement player;

    public CharacterController2D controller;

    public float runSpeed = 40f;

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


        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            dash = true;
        }

    }


    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump, dash);
        jump = false;
        dash = false;
    }
}
