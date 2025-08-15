using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float health = 100;
    [SerializeField] int scoreValue = 150;

    [Header("Shooting")]
    float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject projectileType;
    [SerializeField] float projectileSpeed = 10f;

    [Header("Sound Effects")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] AudioClip enemyShooting;
    [SerializeField] [Range(0, 1)] float enemyShootingVolume = 0.4f;
    [SerializeField] AudioClip enemyKilled;
    [SerializeField] [Range(0, 1)] float enemyKilledVolume = 0.2f;

    [Header("Bonus")]
    [SerializeField] GameObject chickenPrefab;
    [SerializeField] float minChickenWingSpeedY = 7f;
    [SerializeField] float maxChickenWingSpeedY = 14f;
    [SerializeField] float minChickenWingSpeedX = -5f;
    [SerializeField] float maxChickenWingSpeedX = 5f;
    [SerializeField] float timeTillWingDissapears = 3f;
    float chickenWingSpeedY;
    float chickenWingSpeedX;
    float damageBonus = 0.01f;
    int healthBonus = 50;

    Player player;

    Coroutine firingCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        chickenWingSpeedX = Random.Range(minChickenWingSpeedX, maxChickenWingSpeedX);
        chickenWingSpeedY = Random.Range(-minChickenWingSpeedY, -maxChickenWingSpeedY);
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
        GameObject[] chickenWings = GameObject.FindGameObjectsWithTag("Bonus");
        foreach (GameObject chickenWing in chickenWings)
        {
            Destroy(chickenWing, timeTillWingDissapears);
        }
    }


    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject projectile =
                            Instantiate(
                                projectileType,
                                transform.position,
                                Quaternion.identity) as GameObject;
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(enemyShooting, Camera.main.transform.position, enemyShootingVolume);
    }

  


    private void OnTriggerEnter2D(Collider2D other)
    {
      
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0 )
        {
            Die();
        }
    }

    private void Die()
    {
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        if (gameObject.tag == "Boss")
        {
            
            enemySpawner.BossKilled();
            ProccessDie();
            player = FindObjectOfType<Player>();
            player.PowerUp(damageBonus, healthBonus);
            
        }

        else
        {
            ProccessDie();
            var numChickenWings = Random.Range(0, 3);
            for (var i = 0; i < numChickenWings; i++)
            {
                chickenWingSpeedX = Random.Range(minChickenWingSpeedX, maxChickenWingSpeedX);
                chickenWingSpeedY = Random.Range(-minChickenWingSpeedY, -maxChickenWingSpeedY);
                GameObject chickenWing = Instantiate(chickenPrefab, transform.position, Quaternion.identity);
                chickenWing.GetComponent<Rigidbody2D>().velocity = new Vector2
                    (chickenWingSpeedX, chickenWingSpeedY);
            }
        }


    }

    private void ProccessDie()
    {
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(enemyKilled, Camera.main.transform.position, enemyKilledVolume);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        FindObjectOfType<GameSession>().AddToScore(scoreValue);
    }

    
}
