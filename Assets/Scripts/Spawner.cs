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
    private float lastSomethingSpawned;

    [SerializeField]
    private Transform spawnArea = null;

    void Update()
    {

        
            if (Time.time > lastSomethingSpawned + spawnCooldown && spawnCounter > 0)
            {
                lastSomethingSpawned = Time.time;

                Vector2 spawnPosition;
              
                spawnPosition = transform.position;
                

                GameObject newBullet = GameObject.Instantiate(somethingToSpawn, spawnPosition, transform.rotation);
                spawnCounter--;
            }
        
    }
}
