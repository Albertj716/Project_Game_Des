using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    static int maxObstacles = 3;
    float xcoordinate;
    float ycoordinate;
    Vector2 location;
    public GameObject[] obstacles = new GameObject[maxObstacles];
    public GameObject playerModel;
    Vector2 player;
    private float[] sleepTimer = new float[maxObstacles];



    void Start()
    {
        playerModel = GameObject.FindWithTag("Player");
        for (int i = 0; i < maxObstacles; i++)
        {
            obstacles[i] = GameObject.Instantiate(Resources.Load("TestObstacle")) as GameObject;
            sleepTimer[i] = 0;
        }
        for (int i = 0; i < maxObstacles; i++)
        {
            Spawn(obstacles[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        for (int i = 0; i < maxObstacles; i++)
        {
            if (sleepTimer[i] >= 5)
            {
                Debug.Log("OTime");
                sleepTimer[i] = 0;
                Spawn(obstacles[i]);
            }
            else if (!InView(obstacles[i].transform.position))
            {
                sleepTimer[i] += Time.fixedDeltaTime;
            }

        }
    }

    void Spawn(GameObject obj)
    {
        player = playerModel.transform.position;
        SpawnCoords();
        obj.transform.position = location;
        //Debug.Log("Obstacle: " + xcoordinate + " " + ycoordinate);
    }

    bool InView(Vector2 vect)
    {
        player = playerModel.transform.position;
        if (vect.x - player.x > 10 || vect.x - player.x < -10)
        {
            return false;
        }
        else if (vect.y - player.y > 5 || vect.y - player.y < -5)
        {
            return false;
        }

        return true;
    }

    void SpawnCoords()
    {
        xcoordinate = Random.Range((player.x - 13), (player.x + 13));
        ycoordinate = Random.Range((player.y - 13), (player.y + 13));
        if (InView(new Vector2(xcoordinate, ycoordinate)))
        {
            SpawnCoords();
        }
        location = new Vector2(xcoordinate, ycoordinate);

    }
}
