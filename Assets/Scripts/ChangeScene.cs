using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public LevelLoader lvlmanager;
    private void OnTriggerEnter2D(Collider2D collision)
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
    }
}
