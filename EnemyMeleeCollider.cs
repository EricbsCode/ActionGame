using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeCollider : MonoBehaviour
{
    public float attack;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            collision.gameObject.GetComponent<PlayerController>().Damage(attack);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            collision.gameObject.GetComponent<PlayerController>().Damage(attack);
    }
}
