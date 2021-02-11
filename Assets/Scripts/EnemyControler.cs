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
            /*if (AudioManager.instance != null && aud.isPlaying == false)
            {
                AudioManager.instance.Play("Flying Fireball");
            }*/
        }
        else
        {
            transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
            /*if (AudioManager.instance != null && aud.isPlaying == false)
            {
                
                AudioManager.instance.Play("Flying Fireball");
            }*/

        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play("Hit Fireball");
        }
        if (other.gameObject.tag == "Soul")
        {
            return;
        }
        Destroy(gameObject);

    }
}
