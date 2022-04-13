using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    public int health;
    EnemyAI enemy;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Cancel") && Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }else if (Input.GetButtonDown("Cancel") && Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
    }

    void FixedUpdate()
    {
        if (health <= 0)
        {
            SceneManager.LoadScene("MainMenu");
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
    
}
