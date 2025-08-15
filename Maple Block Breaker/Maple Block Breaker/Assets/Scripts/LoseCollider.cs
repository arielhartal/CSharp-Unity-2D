using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
 
        if (GameSession.currentLives == 1)
        {
            SceneManager.LoadScene("Game Over");
        }
        else
        {
         
            SceneManager.LoadScene("Try Again");
        }
        

    }
}
