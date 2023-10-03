using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float maxHp;
     float hp;
    public float exp;
    public int money;
    PlayerController player; 

    public float iframeTime = 0.3f;
    float iframe;

    public enum enemystates {chase, attack};
    public enemystates currentState;
    

    Animator anim;
    Rigidbody2D enemyRigidbody;
    GameController cont;

    public float timeBwtAttacks = 1f;
    float cools;

    public float speed;
    int dir;
    SpriteRenderer rend;
    public float attackRange;
    float distance;

    public GameObject meleeCollider;


    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        cont = FindObjectOfType<GameController>();
        anim = FindObjectOfType<Animator>();
        enemyRigidbody = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();

        hp = maxHp;
        iframe = iframeTime;
        cools = timeBwtAttacks;


    }

    public void Damage(float amt)
    {
        if(iframe <= 0)
        {
            hp -= amt;
            iframe = iframeTime;
            if (hp <= 0)
            {
                Die();
            }   
        }

    }

    void Die()
    {
        gameObject.SetActive(false);
        player.AddExp(exp);
        player.AddMoney(money);

    }

    void Update()
    {
        if(iframe > 0)
        {
            iframe -= Time.deltaTime;
        }
        if(cools > 0)
        {
            cools -= Time.deltaTime;
        }

        switch(currentState)
        {
            case(enemystates.chase):
                Chase();
                break;
            case(enemystates.attack):
                Attack();
                break;
        }
        anim.SetInteger("dir", dir);
    }

    void Chase()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        if(player.transform.position.y < transform.position.y)
        {
            dir = 0;
            meleeCollider.transform.localPosition = new Vector3 (0, 0, 0);
            meleeCollider.transform.localScale = new Vector3 (1, 1, 1);
        }
        else if(player.transform.position.x > transform.position.x)
        {
            dir = 1;
            rend.flipX = false;
            meleeCollider.transform.localPosition = new Vector3 (0.3f, 0.2f, 0);
            meleeCollider.transform.localScale = new Vector3 (1, 1, 1);
        }
        else if(player.transform.position.x < transform.position.x)
        {
            dir = 1;
            rend.flipX = true;
            meleeCollider.transform.localPosition = new Vector3 (-0.3f, 0.2f, 0);
            meleeCollider.transform.localScale = new Vector3 (1, 1, 1);
        }
        else if(player.transform.position.y > transform.position.y)
        {
            dir = 2;
            meleeCollider.transform.localPosition = new Vector3 (0, 0.4f, 0);
            meleeCollider.transform.localScale = new Vector3 (1, 1, 1);
        }
        

        if(distance > attackRange)
        {
            Vector3 direction = player.transform.position - transform.position;
            enemyRigidbody.AddForce(direction * speed * Time.deltaTime);
        }
        else
        {
            if(cools <= 0) currentState = enemystates.attack;
        }
    }

    void Attack()
    {
        anim.SetTrigger("attacking");
        cools = timeBwtAttacks;
        currentState = enemystates.chase;
    }

    
}
