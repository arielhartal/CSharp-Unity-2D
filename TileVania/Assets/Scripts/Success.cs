using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Success : MonoBehaviour
{
    int score;
    Player player;
    [SerializeField] Text scoreText;
    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        score = player.GetPlayerScore();
        scoreText.text = score.ToString();
        gameSession = FindObjectOfType<GameSession>();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentSceneIndex == 7 || currentSceneIndex == 8)
        {
            gameSession.ResetGameSessionForSuccessOrTry();
        }
    }

    public void LoadMainMenu()
    {
        FindObjectOfType<GameSession>().ResetGameSession();
        SceneManager.LoadSceneAsync(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
