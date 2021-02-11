using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private SpriteRenderer tut;
    void Start()
    {
        tut = this.gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
            if (other.gameObject.tag == "Player")
            {
            tut.enabled = true;
            
            }

        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            tut.enabled = false;
            //this.gameObject.SetActive(false);
        }
    }


}
