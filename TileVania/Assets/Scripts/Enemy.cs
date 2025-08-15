using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 200f;
    [SerializeField] GameObject deathVFX;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip hitSound;
    [SerializeField] GameObject[]  enemies;
    [SerializeField] int score = 100;
    [SerializeField] GameObject key;
    float distance = 0f;
    GameSession gameSession;

    public void DealDamage(float damage)
    {
        gameSession = FindObjectOfType<GameSession>();
        health -= damage;
        if (health <= 0)
        {
            TriggerDeathVFX();
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
            Destroy(gameObject);
            gameSession.AddToScore(score);
            if (transform.tag == "Boss")
            {
                SummonKey();
                SummonEnemies();
            }
        }
        else
        {
            AudioSource.PlayClipAtPoint(hitSound, transform.position);
        }
    }

    private void SummonKey()
    {
        var pos = new Vector2(transform.position.x, -25.51f);
        GameObject keyObject = Instantiate(key, pos, Quaternion.Euler(0, 0, 0)) as GameObject;
    }

        private void SummonEnemies()  
            {
                for (int enemyIndex = 0; enemyIndex < enemies.Length; enemyIndex++)
                {
                    var pos = new Vector2(transform.position.x + distance, -25.51f);
                    GameObject newEnemy = Instantiate(enemies[enemyIndex], pos, Quaternion.Euler(0, 0, 0)) as GameObject;
                    distance += 1f;

                }
            }
        
    

    private void TriggerDeathVFX()
    {
        if (!deathVFX) { return; }
        GameObject deathVFXObject = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(deathVFXObject, 1f);
    }

  
}
