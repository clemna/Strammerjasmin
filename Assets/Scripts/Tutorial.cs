using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        
            if (other.gameObject.tag == "Player")
            {
            this.gameObject.SetActive(true);
            }

        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.gameObject.SetActive(false);
        }
    }


}
