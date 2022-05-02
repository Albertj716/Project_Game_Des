using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    public GameObject GameOver;
    public GameObject Character;
    public int health;
    public int playerDamage;
    EnemyAI enemy;
    PlayerMovement player;
    bool powSpd;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        playerDamage = 50;
        player = Character.GetComponent<PlayerMovement>();
    }

    void FixedUpdate()
    {
        if (health <= 0)
        {
            Destroy(Character);
			GameOver.SetActive(true);
			Time.timeScale = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (Time.timeScale != 0)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                enemy = collision.gameObject.GetComponent<EnemyAI>();
                int damage = enemy.getDamage();
                health -= damage;
                Debug.Log(health);
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.timeScale != 0)
        {
            if (collision.gameObject.tag == "powDmg")
            {
                Destroy(collision.gameObject);
                StartCoroutine(dmgBuff());
            }
            if (collision.gameObject.tag == "powSpd")
            {
                Destroy(collision.gameObject);
                StartCoroutine(SpeedBuff());
            }
            if (collision.gameObject.tag == "powHeal")
            {
                Destroy(collision.gameObject);
                Heal();
            }
        }
    }

    public int getDamage()
    {
        return playerDamage;
    }

    IEnumerator dmgBuff()
    {
        int i = playerDamage;
        playerDamage *= 2;
        yield return new WaitForSeconds(7);
        playerDamage = i;
    }

    IEnumerator SpeedBuff()
    {
        float temp = player.moveSpeed;
        player.moveSpeed = 6.5f;
        yield return new WaitForSeconds(7);
        player.moveSpeed = temp;
    }

    public void Heal()
    {
        if (health + 30 >= 150)
        {
            health = 150;
        }else
        {
            health += 30;
        }
        
    }

    
}
