using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI element;
    
    // Update is called once per frame
    void Update()
    {
        element.text = player.health.ToString();
    }
}
