using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour
{
    GameSession gameSession;
    Text levelText;


    // Start is called before the first frame update
    void Start()
    {
        levelText = GetComponent<Text>();
        gameSession = FindObjectOfType<GameSession>();


    }

    // Update is called once per frame
    void Update()
    {
        levelText.text = "Lvl:" + gameSession.GetLevel().ToString();
    }
}
