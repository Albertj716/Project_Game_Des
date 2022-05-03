using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Score : MonoBehaviour
{
    public static int currentScore;
    int nextActionTime = 10;
    bool first = true;
    public TextMeshProUGUI element;
    
    void Start()
    {
        currentScore = 0;
        element.text = currentScore.ToString();
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
            currentScore += 10;
            first = true;
        }
        element.text = currentScore.ToString();    
    }
}
