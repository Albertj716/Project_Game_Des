using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Score : MonoBehaviour
{
    public static int currentScore;
    int nextActionTime = 10; //time period in seconds that survival points are awarded
    bool first = true; //used to reward survival points only once per nextActionTime period instead of every frame of that second
    public TextMeshProUGUI element;
    
    void Start()
    {
        currentScore = 0;
        element.text = currentScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (first == true && ((Math.Floor(Time.timeSinceLevelLoad)) % nextActionTime != 0)) //checks if it has not been nextActionTime since level load to disable first flag
        {
            first = false;
        }
        else if (first == false && ((Math.Floor(Time.timeSinceLevelLoad)) % nextActionTime == 0)) //checks if it has been nextActionTime since level load to reward points and enable first flag
        {
            currentScore += 10;
            first = true;
        }
        element.text = currentScore.ToString();    
    }
}
