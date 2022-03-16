using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;                            // The target this unit is attempting to reach

    public float speed = 5f;                            // Default speed of the unit
    public float nextWaypointDistance = 3f;             // How close this unit needs to be to a waypoint before moving to the next waypoint

    Path path;                                          // Current path this unit is following
    int currentWaypoint = 0;                            // Waypoint (on the path) that this unit is currently moving towards
    bool reachedEnd = false;                            // Has this unit reached the end of its path?

    Seeker seeker;                                      // Seeker generates the path to the target
    Rigidbody2D rigidBody;                              // RigidBody of the unit

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rigidBody = GetComponent<Rigidbody2D>();

        // Invoke the "UpdatePath" method every 0.5 seconds
        InvokeRepeating("UpdatePath", 0f, 0.5f);
        // Generate path to target
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Check that there is a path
        if(path == null)
        {
            return;
        }

        // Check if the end of the path has been reached
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEnd = true;
            return;
        } 
        else
        {
            reachedEnd = false;
        }

        // Get direction from this unit's current position to the position of the next waypoint in the path
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rigidBody.position).normalized;
        // Force to apply to this unit to move towards the next waypoint
        Vector2 force = direction * speed * Time.deltaTime;
        // Apply force to this unit to move it
        rigidBody.AddForce(force);

        // Distance from this unit to the next waypoint
        float distance = Vector2.Distance(rigidBody.position, path.vectorPath[currentWaypoint]);
        // If this unit is sufficiently close to its current waypoint, begin moving towards the next waypoint
        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
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
            currentWaypoint = 0;                        // Set currentWaypoint to first waypoint of the new path
        }
    }
}
