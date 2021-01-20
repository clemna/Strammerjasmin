using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : MonoBehaviour
{
    public float speed;
    public bool moveRight;


    private void Update()
    {
        if (moveRight)
        {
            transform.Translate(2 * Time.deltaTime * speed, 0, 0);
            
        }
        else
        {
            transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
            
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        FindObjectOfType<AudioManager>().Play("Hit Fireball");
        Destroy(gameObject);

    }
}
