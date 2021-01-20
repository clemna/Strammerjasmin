using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject somethingToSpawn = null;
    [SerializeField]
    private int spawnCounter = 10;
    [SerializeField]
    private float spawnCooldown = 2;
    public bool useSequence = false;
    [SerializeField]
    private bool[] sequence = new bool[1];

    private float lastSomethingSpawned;


    void Update()
    {
        if (Time.time > lastSomethingSpawned + spawnCooldown)
        {
            lastSomethingSpawned = Time.time;

            Vector2 spawnPosition;

            spawnPosition = transform.position;

            if (useSequence == true)
            {
                spawnSequence(spawnCounter);
            }
            else
            {
                GameObject newBullet = GameObject.Instantiate(somethingToSpawn, spawnPosition, transform.rotation);
                FindObjectOfType<AudioManager>().Play("Spawn Fireball");
            }
            
            
            spawnCounter++;
        }

    }

    private void spawnSequence(int spawncounter)
    {
       
            if (sequence[spawncounter % sequence.Length] == true)
            {
                Vector2 spawnPosition;
                spawnPosition = transform.position;
                GameObject newBullet = GameObject.Instantiate(somethingToSpawn, spawnPosition, transform.rotation);
                FindObjectOfType<AudioManager>().Play("Spawn Fireball");
        }
        
    }

}
