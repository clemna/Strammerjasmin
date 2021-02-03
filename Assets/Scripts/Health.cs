using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField]
    private int maxHealth = 1;
    [SerializeField]
    Transform spawnPoint;
    [SerializeField]
    Transform PlayerToSpawn;

    public LevelLoader change;

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
            //animator.SetInteger("Health", 0);
            Die();
        }
    }

    public void Die()
    {
        currentHealth = 0;
        if (gameObject.transform.CompareTag("Player"))
        {

            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            //animator.SetInteger("Health", 1);
            StartCoroutine(DeathAnimation());
            /*Destroy(gameObject);
            change.LoadCurrentLevel();*/

            //gameObject.transform.position = spawnPoint.position;
            
        
            
            //Instantiate(PlayerToSpawn, spawnPoint.position, spawnPoint.rotation);
            }
        else
        {
            //FindObjectOfType<AudioManager>().Play("Hit Fireball");
            AudioManager.instance.Play("Hit Fireball");
            Destroy(gameObject);
        }
        

    }

    public int GetCurrent()
    {
        return currentHealth;
    }

    IEnumerator DeathAnimation()
    {
        animator.SetInteger("Health", 0);
        yield return new WaitForSeconds(1f);
        animator.SetInteger("Health", 1);
        Destroy(gameObject);
        change.LoadCurrentLevel();
    }
}
