using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Item
{

    Animator anim;
    public LayerMask enemyLayer;
    public float rayLength;

    public override void Use()
    {
        base.Use();
        FindObjectOfType<Animator>().SetTrigger("strike");


    }

    public override void Remove()
    {
        base.Remove();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyController>().Damage(amount);

        }
    }
}

