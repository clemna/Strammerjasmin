using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public LevelLoader lvlmanager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.attachedRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            //collision.attachedRigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
            /*collision.attachedRigidbody.velocity = Vector2.zero;
            collision.attachedRigidbody.angularDrag = 0;
            collision.attachedRigidbody.isKinematic = true;*/
            Debug.LogWarning("Collision funktioniert");
            if (this.gameObject.tag == "Exit")
            {
                Debug.LogWarning("Exit funktioniert");
                lvlmanager.LoadNextLevel();
            }
            if (this.gameObject.tag == "Enter")
            {
                Debug.LogWarning("Enter funktioniert");
                lvlmanager.LoadPreviousLevel();
            }
            if (this.gameObject.tag == "Enemy")
            {
                Debug.LogWarning("Enemy funktioniert");
                lvlmanager.LoadCurrentLevel();
            }
            if (this.gameObject.tag == "Restart")
            {
                lvlmanager.LoadFirstLevel();
            }
        }

    }
}
