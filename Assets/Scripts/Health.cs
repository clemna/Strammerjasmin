using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField]
    private int maxHealth = 1;
    [SerializeField]
    Transform spawnPoint;

    public Animator animator;

    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }


    public void TakeDamage()
    {
        currentHealth--;
        //option for Loot can be added here
        if (currentHealth <= 0)
        {
            animator.SetInteger("Health", 0);
            Die();
        }
    }

    public void Die()
    {
        currentHealth = 0;
        if (gameObject.transform.CompareTag("Player"))
        {
            animator.SetInteger("Health", 1);
            gameObject.transform.position = spawnPoint.position;
        }
        else
        {
            Destroy(gameObject);
        }
        

    }

    public int GetCurrent()
    {
        return currentHealth;
    }
}
