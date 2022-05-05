using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;                            // The target this unit is attempting to reach

    public float nextWaypointDistance = 3f;             // How close this unit needs to be to a waypoint before moving to the next waypoint
    private float speed;                                // Default speed of the unit

    private int attackDamage;                           // Damage enemy will deal when attacking Player
    private float attackDistance;                       // Distance the enemy must be within to perform an attack
    private float attackMultiplier;                     // Multiplier to apply to movement when 'attacking'
    private float attackCD;                             // Minimum time (in seconds) between enemy attacks
    private float health;                               // Health of the enemy unit

    private float attackStage = 0;                      // Variable to help track what "stage" of the attack animation the enemy is currently in
    private float nextAttack = 0f;

    Path path;                                          // Current path this unit is following
    int currentWaypoint = 0;                            // Waypoint (on the path) that this unit is currently moving towards
    bool reachedEnd = false;                            // Has this unit reached the end of its path?

    Seeker seeker;                                      // Seeker generates the path to the target
    Rigidbody2D rigidBody;                              // RigidBody of the unit

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rigidBody = GetComponent<Rigidbody2D>();

        // Get stats from child
        EnemyStats stats = this.GetComponentInChildren<EnemyStats>();
        speed = stats.speed;
        attackDamage = stats.attackDamage;
        attackDistance = stats.attackDistance;
        attackMultiplier = stats.attackMultiplier;
        attackCD = stats.attackCD;
        health = stats.health;

        target = GameObject.FindGameObjectsWithTag("Player")[0].transform;

        // Invoke the "UpdatePath" method every 0.5 seconds
        InvokeRepeating("UpdatePath", 0f, 0.5f);
        // Generate path to target
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // If this enemy has reached 0 health, destroy it
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }

        // Check that there is a path
        if(path == null)
        {
            return;
        }

        // Check if the end of the path has been reached
        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEnd = true;
            return;
        } 
        else
        {
            reachedEnd = false;
        }

        // If the current attackStage is 0, this unit is not attacking
        // While not attacking, enemies will continue to move along their path until they are close enough to their target to begin an attack
        if(attackStage == 0)
        {
            // If the distance between the enemy and its target is less than the enemy's minimum attack distance,
            // and the enemy's attack is not on cooldown,
            // then begin the first stage of the attack animation
            // Otherwise, continue moving along the path to the target
            if (Vector2.Distance(rigidBody.position, target.position) < attackDistance && Time.time > nextAttack)
            {
                attackStage = 1;
            }
            else
            {
                Move();
            }
        }
        else if(attackStage == 1)
        {
            Attack1();
        }
        else if(attackStage == 2)
        {
            Attack2();
        }

        // Distance from this unit to the next waypoint
        float distance = Vector2.Distance(rigidBody.position, path.vectorPath[currentWaypoint]);
        // If this unit is sufficiently close to its current waypoint, begin moving towards the next waypoint
        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    // Handle movement along path to target
    void Move()
    {
        // Get direction from this unit's current position to the position of the next waypoint in the path
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rigidBody.position).normalized;
        // Force to apply to this unit to move towards the next waypoint
        Vector2 force = direction * speed * Time.deltaTime;

        // Apply force to this unit to move it
        rigidBody.AddForce(force);
    }

    // Reduce the velocity of this unit
    // When the magnitude of the unit's velocity is sufficiently close to 0, set it to 0 and move to stage 2 of the attack animation
    void Attack1()
    {
        rigidBody.velocity *= 0.9999f;
        if(rigidBody.velocity.magnitude < 1)
        {
            rigidBody.velocity.Set(0, 0);
            attackStage = 2;
        }
    }

    // Quickly accelerate towards the target to "attack" it
    void Attack2()
    {
        // Get direction from this unit's current position to the position of the next waypoint in the path
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rigidBody.position).normalized;
        // Force to apply to this unit to move towards the next waypoint
        Vector2 force = direction * speed * Time.deltaTime * attackMultiplier;

        // Set time for nextAttack
        // Another attack cannot be performed before this time
        nextAttack = Time.time + attackCD;
        attackStage = 0;

        rigidBody.AddForce(force);
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
        {
            // start: (rigidBody.position) this unit's position
            // end: (target.position) the target's position
            // callback: (SetPath) function to be called when Seeker has finished calculating the path
            seeker.StartPath(rigidBody.position, target.position, SetPath);
        }
    }

    // Set current path of this unit
    void SetPath(Path p)
    {
        if(!p.error)
        {
            path = p;
            // Set currentWaypoint to first waypoint of the new path
            currentWaypoint = 0;
        }
    }

    // Get attackDamage value
    public int getDamage()
    {
        return attackDamage;
    }

    // Handle enemy being hit by the player's bullet
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>();
            health -= player.getDamage();
            if (health <= 0)
            {
                Score.currentScore += 100; //rewards 100 points for defeating enemy
            }
            else
            {
                Score.currentScore += 25; //rewards 25 points for hitting but not defeating enemy
            }
            
            //Debug.Log("Enemy health: " + health);
        }
    }
}
