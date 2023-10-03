using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    public float speed;
    Rigidbody2D bulletRigidbody;
    RangedWeapon colt;


    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody2D>();
        colt = FindObjectOfType<RangedWeapon>();
    }

    private void OnEnable()
    {
        bulletRigidbody.AddForce(transform.up * speed);
        Invoke("Disable", 4f);
    }


    private void Disable()
    {

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyController>().Damage(colt.amount);
            Invoke("Disable", 0.01f);
        }
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}