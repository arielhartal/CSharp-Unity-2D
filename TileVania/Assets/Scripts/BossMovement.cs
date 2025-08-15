using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] GameObject gun, projectile;
    [SerializeField] float minDelayTimeShoot = 0.7f;
    [SerializeField] float maxDelayTimeShoot = 1.5f;
    [SerializeField] float fireRate = 3.5f;
    [SerializeField] AudioClip projectileSound;
    [SerializeField] Transform[] waypoints;

    Rigidbody2D myRigidBody;
    float lastShot = 0.0f;
    GameObject projectileParent;
    const string PROJECTILE_PARENT_NAME = "Enemy Projectiles";
    int waypointIndex = 0;

    Quaternion projectileSide = Quaternion.Euler(0, 0, 0);


    // Start is called before the first frame update
    void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        Movement();

        projectileSide = Quaternion.Euler(0, 0, 180);
            Shoot();
            CreateProjectileParent();
        

    
     
    }

  
    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }


    private void Shoot()
    {
        if (Time.time > fireRate + lastShot)
        {
            StartCoroutine(shootingEnumerator(projectileSide));
            lastShot = Time.time;
        }

    }

    IEnumerator shootingEnumerator(Quaternion projectileSide)
    {
        float delayTimeShoot = Random.Range(minDelayTimeShoot, maxDelayTimeShoot);
        yield return new WaitForSeconds(delayTimeShoot * Time.timeScale);
        GameObject newProjectile = Instantiate(projectile, gun.transform.position, projectileSide) as GameObject;
        newProjectile.transform.parent = projectileParent.transform;
        
        AudioSource.PlayClipAtPoint(projectileSound, transform.position);

    }


    private void Movement()
    {
        transform.position = Vector2.MoveTowards(transform.position,
            waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);

        if(transform.position == waypoints [waypointIndex].transform.position)
        {
            waypointIndex = Random.Range(0, waypoints.Length);
        }

    }
    private void CreateProjectileParent()
    {
        projectileParent = GameObject.Find(PROJECTILE_PARENT_NAME);
        if (!projectileParent)
        {
            projectileParent = new GameObject(PROJECTILE_PARENT_NAME);
        }
    }
}