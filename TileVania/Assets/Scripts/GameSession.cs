using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int playerHealth = 3;
    [SerializeField] int score = 0;
    [SerializeField] int coin = 0;
    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;
    [SerializeField] Text coinText;
    [SerializeField] GameObject [] hearts;

    private void Awake()
    {

        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
            livesText.text = playerLives.ToString();
            scoreText.text = score.ToString();
            coinText.text = coin.ToString();
    
    }

    void Update()
    {
    
        if (playerHealth < 1)
        {
            hearts[0].gameObject.SetActive(false);
        }
        else if(playerHealth < 2)
        {
            hearts[1].gameObject.SetActive(false);
        }
        else if (playerHealth < 3)
        {
            hearts[2].gameObject.SetActive(false);
        }


    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }

    public void AddToCoins(int CoinsToAdd)
    {
        coin += CoinsToAdd;
        coinText.text = coin.ToString();
    }

    public void ProcessPlayerDeath()
    {

        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            // ResetGameSession();
            SceneManager.LoadScene(8);
            Destroy(gameObject);
        }
    }

    public void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void ResetGameSessionForSuccessOrTry()
    {
        Destroy(gameObject);
    }

    public void TakeHealth()
    {
        playerHealth--;
    }

    public int GetPlayerHealth()
    {
        return playerHealth;
    }

    public int GetScore()
    {
        return score;
    }

    public void SetPlayerHealth(int health)
    {
        playerHealth = health;
    }


    public void RemoveHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].gameObject.SetActive(false);
        }
    }

    private void TakeLife()
    {
        
        playerLives--;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        SetPlayerHealth(3);
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].gameObject.SetActive(true);
        }
        livesText.text = playerLives.ToString();

    }

  



}
