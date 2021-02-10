using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip collectSoul;
    public AudioClip Steps;
    public AudioClip Landing;
    public AudioClip Jump;
    public AudioClip Dash;

    public AudioSource audioS;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Soul"))
        {
            audioS.PlayOneShot(collectSoul);
        }
    }

}
