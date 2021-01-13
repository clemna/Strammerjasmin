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
        }

    }
}
