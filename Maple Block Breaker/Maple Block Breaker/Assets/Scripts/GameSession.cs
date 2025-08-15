using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    // config params
    [Range(0.1f, 10f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBlockDestroyed = 83;
    [SerializeField] Text scoreText;
    [SerializeField] Text livesText;
    [SerializeField] bool isAutoPlayEnabled;





    // state variables
    [SerializeField] int currentScore = 0;
    public static int currentLives = 3;
    public static int currentLevel = 1;

    private void Awake()
    {
        int gameStatusCount = 0;
        Debug.Log("Awake");
        gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1)
        {
            currentLevel += 1;
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {

            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        scoreText.text = currentScore.ToString();
        livesText.text = "Lives: " + currentLives.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;

    }

    public void AddToScore()
    {
        currentScore += pointsPerBlockDestroyed;
        scoreText.text = currentScore.ToString();
    }

    public void DecreaseLives()
    {
        currentLives -= 1;
        livesText.text = "Lives: " + currentLives.ToString();
    }

    public void LoweringLevel()
    {
        currentLevel -= 1;
    }
    

    public void ResetGame()
    {
        // gameStatusCount = 0;
        Debug.Log(currentLevel);
        currentLevel = 1;
        Debug.Log(currentLevel);
        currentLives = 3;
        Destroy(gameObject);
     
    }


    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }
}
