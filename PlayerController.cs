using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D playerRigidbody;
    Vector2 input;
    public float speed;

    Animator anim;

    SpriteRenderer rend;

    int lookDir = 0;
    bool moving = false;

    public float maxHealth;
    public float health;
    public Image healthUI;
    

    public int maxMoney;
    public int money;
    public Text moneyText;

    public float attack;
    public int level = 1;
    public float experience;
    public float expToNext;
    public AnimationCurve expCurve =new AnimationCurve();
    public Text expText;

    public float iframeTime = 0.6f;
    float iframe;

    public GameObject meleeCollider;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        expToNext = CalculateExp(level);
        for(int i = 1; i <= 30; i++)
        {
            expCurve.AddKey(i, CalculateExp(i));
        }

        health = maxHealth;
        money = maxMoney;
        iframe = iframeTime;

    }



    void Update()
    {
        if(iframe > 0)
        {
            iframe -= Time.deltaTime;
        }
        input = new Vector2(Input.GetAxis("Horizontal"), (Input.GetAxis("Vertical")));

        playerRigidbody.AddForce(input * speed * Time.deltaTime);

        moving = (input.x != 0 || input.y != 0);

        if(input.y < 0)
        {
            lookDir = 0;
            meleeCollider.transform.localPosition = new Vector3 (0, 0, 0);
            meleeCollider.transform.localScale = new Vector3 (1, 1, 1);
        }
        else if(input.x > 0)
        {
            lookDir = 1;
            rend.flipX = false;
            meleeCollider.transform.localPosition = new Vector3 (0.3f, 0.2f, 0);
            meleeCollider.transform.localScale = new Vector3 (1, 1, 1);
        }
        else if(input.x < 0)
        {
            lookDir = 1;
            rend.flipX = true;
            meleeCollider.transform.localPosition = new Vector3 (-0.3f, 0.2f, 0);
            meleeCollider.transform.localScale = new Vector3 (1, 1, 1);
        }
        else if(input.y > 0)
        {
            lookDir = 2;
            meleeCollider.transform.localPosition = new Vector3 (0, 0.4f, 0);
            meleeCollider.transform.localScale = new Vector3 (1, 1, 1);
        }

        anim.SetInteger("dir", lookDir);
        anim.SetBool("moving", moving);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwingAttack();
        }
        
        if(Input.GetKeyDown(KeyCode.J))
        {
            AddExp(20);
        }

        healthUI.fillAmount = health / maxHealth;
        moneyText.text = "Coins: " + money.ToString();
        expText.text = "Level: " + level.ToString() + "Exp: " + experience.ToString() + "/" + expToNext.ToString();
    }


    public void SwingAttack()
    {
        anim.SetBool("attacking", true);
        Invoke("ResetAttack", 0.1f);
    }

    void ResetAttack()
    {
        anim.SetBool("attacking", false);
    }

    public void Heal(float amt)
    {
        health += amt;
        if(health > maxHealth) health = maxHealth;
    }

    public void Damage(float amt)
    {
        if (iframe <= 0)
        {
            health -= amt;
            iframe = iframeTime;
            if (health <= 0) 
                Die();             
        }

    }

    void Die()
    {
        gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    public void AddMoney(int amt)
    {
        money += amt;
    }


    public float CalculateExp(int level)
    {
        float expNeeded;
        expNeeded = level * 100f;
        return expNeeded;
    }

    public void AddExp(float amt)
    {
        experience += amt;
        if(experience  >= expToNext)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        level++;
        experience -= expToNext;
        attack = attack + 5f;
        speed = speed + 50f;
        Heal(maxHealth);
        expToNext = CalculateExp(level);

    }
}
