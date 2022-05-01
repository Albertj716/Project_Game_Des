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

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        playerDamage = 50;
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
        if (collision.gameObject.tag == "Enemy" && Time.timeScale != 0)
        {
            enemy = collision.gameObject.GetComponent<EnemyAI>();
            int damage = enemy.getDamage();
            health -= damage;
            Debug.Log(health);
        }
    }

    public int getDamage()
    {
        return playerDamage;
    }
}
