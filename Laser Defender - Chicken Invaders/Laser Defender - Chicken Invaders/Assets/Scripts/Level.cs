using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 3.95f;
    [SerializeField] AudioClip gameOver;
    [SerializeField] [Range(0, 1)] float gameOverVolume = 0.7f;

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());
        
    }

    IEnumerator WaitAndLoad()
    {
        
        AudioSource.PlayClipAtPoint(gameOver, Camera.main.transform.position, gameOverVolume);
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene("Game Over");

    }

    public void LoadStartMenu()
    {
       SceneManager.LoadScene(0);
    }


    public void QuitGame()
    {
        Application.Quit();
    }

}
