using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI element;
    public static int highScore = 0;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (Score.currentScore > highScore)
        {
            highScore = Score.currentScore;
        }
        element.text = highScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
