using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Score : MonoBehaviour
{
    int score = 0;
    int nextActionTime = 10;
    bool first = true;
    public TextMeshProUGUI element;
    
    void Start()
    {
        element.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Math.Floor(Time.time));
        Debug.Log(first);
        if (first == true && ((Math.Floor(Time.time)) % nextActionTime != 0))
        {
            first = false;
        }
        else if (first == false && ((Math.Floor(Time.time)) % nextActionTime == 0))
        {
            score += 50;
            element.text = score.ToString();
            first = true;
        }    
    }
}
