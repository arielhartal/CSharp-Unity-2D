using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    GameSession gameSession;
    Text healthText;


    // Start is called before the first frame update
    void Start()
    {
        healthText = GetComponent<Text>();
        gameSession = FindObjectOfType<GameSession>();


    }

    // Update is called once per frame
    void Update()
    {
        if (gameSession.GetHealth() >= 0)
        {
            healthText.text = gameSession.GetHealth().ToString();
        }
        else 
        {
            healthText.text = 0.ToString();
        }
    }
}
