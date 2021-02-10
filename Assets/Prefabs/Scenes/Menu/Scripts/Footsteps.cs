using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    CharacterController2D cc;
    Rigidbody2D rb;
    AudioSource aud;
    void Start()
    {
        cc = GetComponent<CharacterController2D>();
        rb = GetComponent<Rigidbody2D>();
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cc.m_Grounded == true && rb.velocity.magnitude > 2f && aud.isPlaying == false)
        {
            aud.Play();
        }
    }
}
