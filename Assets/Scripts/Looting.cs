using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Looting : MonoBehaviour
{

    [SerializeField]
    private string[] lootTags = new string[0];

    private void OnTriggerEnter2D(Collider2D other)
    {
        foreach(var tag in lootTags)
        {
            if (other.gameObject.tag == tag)
            {
                CollectLoot(other.gameObject);
            }
            
        }
    }

    private void CollectLoot(GameObject loot)
    {
        switch (loot.tag)
        {
            case "Soul":
                CollectSoul();
                FindObjectOfType<AudioManager>().Play("Collect Soul");
                break;
            default:
                break;
        }
        Destroy(loot);
    }

    private void CollectSoul()
    {
        if (UIManager.manager != null)
        {
            UIManager.manager.souls++;
        }
    }

}
