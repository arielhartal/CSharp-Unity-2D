using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] float minSpawnRate = 2f;
    [SerializeField] float maxSpawnRate = 3f;
    [SerializeField] float minDistance;
    [SerializeField] float maxDistance;
    float randX;
    Vector2 whereToSpawn;
    float nextSpawn = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        if(Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            randX = Random.Range(transform.position.x - minDistance, transform.position.x + maxDistance);
            whereToSpawn = new Vector2(randX, transform.position.y);
            Instantiate(projectile, whereToSpawn, Quaternion.identity);
        }
    }


   
}
