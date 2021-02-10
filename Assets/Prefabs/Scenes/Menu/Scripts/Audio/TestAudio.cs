using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAudio : MonoBehaviour
{
    public AudioController audioController;


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.T))
        {
           /// audioController.PlayAudio(AudioType.ST_01);
        }
    }
}
