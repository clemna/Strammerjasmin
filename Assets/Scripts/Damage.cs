using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Damage : MonoBehaviour
{

    [SerializeField]
    private GameObject hitEffect = null;

    private Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        Health health = other.transform.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage();
        }

        float offset = rigid.velocity.magnitude;
        GameObject.Instantiate(hitEffect, transform.position, transform.rotation * Quaternion.Euler(Vector3.up * 180f));

    }
}
