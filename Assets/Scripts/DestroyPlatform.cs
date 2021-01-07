using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlatform : MonoBehaviour
{
    public GameObject objectToDisable;
    public static bool disabled = false;
    public float RespawnDelay = 2;
    
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(null);
            Vector2 spawnPosition = transform.position;
            objectToDisable.SetActive(false);
            Invoke("RespawnPlatform", RespawnDelay);
        }
    }

    void RespawnPlatform()
    {
        objectToDisable.SetActive(true);
    }
}
