using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float delayTime = 4.2f;
    [SerializeField] AudioClip finishLevel;
    [SerializeField] float LevelExitSlowMoFactor = 0.2f;
    Player player;

    AudioSource audioSource;
    public float checkIfFinish = 0;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        player = FindObjectOfType<Player>();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (checkIfFinish < 1)
        {
            if (currentSceneIndex != 6)
            {
                StartCoroutine(FinishLevel());
                checkIfFinish++;
            }
            else
            {
                if(player.PlayerGotKey())
                {
                    StartCoroutine(FinishLevel());
                    checkIfFinish++;
                }
            }
        }


    }

    public float GetCheckIfFinished()
    {
        return checkIfFinish;
    }

    IEnumerator FinishLevel()
    {
        
        audioSource.clip = finishLevel;
        audioSource.Play();
        Time.timeScale = LevelExitSlowMoFactor;
        Camera.main.GetComponent<AudioSource>().Stop();
        yield return new WaitForSeconds(delayTime * Time.timeScale);
        Time.timeScale = 1f;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
