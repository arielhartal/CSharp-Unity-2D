using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int score = 0;
    int health = 200;
    int scoreForBoss = 0;
    int level = 0;


    // Start is called before the first frame update
    void Awake()
    {
        SetUpSingleton();
    }


    private void SetUpSingleton()
    {
        int numberGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }



    public int GetScore()
    {
        return score;
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetLevel()
    {
        return level;
    }

    public int GetScoreForBoss()
    {
        return scoreForBoss;
    }

    public void SetScoreForBoss()
    {
        scoreForBoss = 0;
    }

    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
        scoreForBoss += scoreValue;
      
    }

    public void DecreaseHealth(int damageValue)
    {
        health -= damageValue;
    }

    public void IncreaseHealth(int healthBonus)
    {
        health += healthBonus;
    }

    public void IncreaseLevel()
    {
        level += 1;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
   
