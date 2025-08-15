using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingMovement : MonoBehaviour
{


    [SerializeField] float moveSpeed = 1f;
    [SerializeField] GameObject gun, projectile;
    [SerializeField] float minDelayTimeShoot = 0.7f;
    [SerializeField] float maxDelayTimeShoot = 1.5f;
    [SerializeField] float fireRate = 3.5f;
    [SerializeField] AudioClip projectileSound;

    Rigidbody2D myRigidBody;
    float lastShot = 0.0f;
    GameObject projectileParent;
    const string PROJECTILE_PARENT_NAME = "Enemy Projectiles";
    public Transform player;

    Quaternion projectileSide = Quaternion.Euler(0, 0, 0);



    // Start is called before the first frame update
    void Start()
    {

        myRigidBody = GetComponent<Rigidbody2D>();
       // player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        if (IsFacingRight())
        {
            myRigidBody.velocity = new Vector2(moveSpeed, 0f);
         
        }
        else
        {
            myRigidBody.velocity = new Vector2(-moveSpeed, 0f);
          
        }

        Shoot();
        CreateProjectileParent();
    
        
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Projectile") { return; }
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);

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
            if (myRigidBody.velocity.x > 0)
            {
                projectileSide = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                projectileSide = Quaternion.Euler(0, 0, 180);
            }
        
            GameObject newProjectile = Instantiate(projectile, gun.transform.position, projectileSide) as GameObject;
            newProjectile.transform.parent = projectileParent.transform;
            AudioSource.PlayClipAtPoint(projectileSound, transform.position);

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


    

