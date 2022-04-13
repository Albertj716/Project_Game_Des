using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Score : MonoBehaviour
{
    int score;
    int nextActionTime = 10;
    bool first = true;
    public TextMeshProUGUI element;
    
    void Start()
    {
        score = 0;
        element.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (first == true && ((Math.Floor(Time.timeSinceLevelLoad)) % nextActionTime != 0))
        {
            first = false;
        }
        else if (first == false && ((Math.Floor(Time.timeSinceLevelLoad)) % nextActionTime == 0))
        {
            score += 50;
            element.text = score.ToString();
            first = true;
        }    
    }
}
