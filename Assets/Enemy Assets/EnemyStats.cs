using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float speed = 300;

    public int attackDamage = 50;                       // Damage enemy will deal when attacking Player
    public float attackDistance = 9f;                   // Distance the enemy must be within to perform an attack
    public float attackMultiplier = 10;                 // Multiplier to apply to movement when 'attacking'
    public float attackCD = 1f;                         // Minimum time (in seconds) between enemy attacks
    public float health = 100;                          // Health of the enemy unit

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float getDamage()
    {
        return attackDamage;
    }
}
