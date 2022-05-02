using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSpawner : MonoBehaviour
{
    float xcoordinate;
    float ycoordinate;
    Vector2 location;
    public GameObject playerModel;
    Vector2 player;
    public List<GameObject> powers;
    public bool[] instantiated;
    public GameObject active;
    float sleep;
    bool occupied;


    void Start()
    {
        powers = new List<GameObject>(Resources.LoadAll<GameObject>("Powerups"));
        instantiated = new bool[powers.Count];
        for(int i = 0; i < instantiated.Length; i++)
        {
            instantiated[i] = false;
        }
        sleep = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        for (int i = 0; i < powers.Count; i++)
        {
            if (sleep >= 10 && instantiated[i] != false)
            {
                sleep = 0;
                Destroy(active);
                instantiated[i] = false;
            } else if (instantiated[i] != false && !InView(powers[i].transform.position))
            {
                sleep += Time.fixedDeltaTime;
            }
        }

        occupied = false;
        for (int i = 0; i < powers.Count; i++)
        {
            if (instantiated[i] == true)
            {
                occupied = true;
            }
        }

        if (!occupied)
        {
            float chance = Random.Range(0, 10000) % 100;
            if (chance < .5)
            {
                int choose = Random.Range(0, powers.Count);
                Spawn(powers[choose]);
                instantiated[choose] = true;
            }
        }
    }

    void Spawn(GameObject obj)
    {
        player = playerModel.transform.position;
        SpawnCoords();
        active = Instantiate(obj, location, Quaternion.identity);
        obj.transform.position = location;
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