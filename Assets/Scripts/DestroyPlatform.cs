using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlatform : MonoBehaviour
{
    public GameObject objectToDisable;
    public static bool disabled = false;
    public float RespawnDelay = 2;

    private ContactPoint2D[] hitObject;
    private Vector2 hit;

    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            hitObject = collision.contacts;
            hit = hitObject[0].normal;
            Debug.LogWarning(hitObject);
            //collision.collider.transform.SetParent(transform);
            Debug.LogWarning(collision.contacts[0]);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //collision.collider.transform.SetParent(null);

            if (Vector2.Dot(hit, Vector2.up) > 0)
            { // top
              // Back
                Debug.LogWarning("bot hit");
            }
            else if (Vector2.Dot(hit, Vector2.down) < 0)
            {
                // Front
                Debug.LogWarning("front hit");
            }
            else if (Vector2.Dot(hit, Vector2.right) == 0)
            {
                // Sides
                Vector2 spawnPosition = transform.position;
                objectToDisable.SetActive(false);
                Invoke("RespawnPlatform", RespawnDelay);
            }

        }
    }

    void RespawnPlatform()
    {
        objectToDisable.SetActive(true);
    }
}
