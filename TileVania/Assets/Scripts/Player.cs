using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f, jumpSpeed = 28f, climbSpeed = 5f, fireRate = 0.5f,
        delayTimeDeath = 3f, delayTimeShoot = 0.7f, destroyProjectileTime;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f);
    [SerializeField] GameObject gun, projectile;
    [SerializeField] AudioClip deathSound, projectileSound, jumpSound;


    GameObject projectileParent;
    const string PROJECTILE_PARENT_NAME = "Projectiles";
    float facingSide;
    float lastShot = 0.0f;
    float gravityScaleAtStart;
    float checkIfDead = 0;
    bool isAlive = true;
    int score;
    bool hasKey = false;

    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    GameSession gameSession;
    

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gameSession = FindObjectOfType<GameSession>();
        gravityScaleAtStart = myRigidBody.gravityScale;
        score = gameSession.GetScore();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }
        Run();
        Jump();
        ClimbLadder();
        FlipSprite();
        Fire(facingSide);
        Die();
        CreateProjectileParent();
    }

    private void Run() 
    {

      float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
      Vector2 playerVelocity = new Vector2(moveSpeed * controlThrow, myRigidBody.velocity.y);
      myRigidBody.velocity = playerVelocity;
      bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
      myAnimator.SetBool("Running", playerHasHorizontalSpeed);
     
    }

    private void Jump()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {           
           Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
           myRigidBody.velocity += jumpVelocityToAdd;
           AudioSource.PlayClipAtPoint(jumpSound, transform.position);
        }
    }

    private void ClimbLadder()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myAnimator.SetBool("Climbing", false);
            myRigidBody.gravityScale = gravityScaleAtStart;
            return;
        }

        

        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeed);
        myRigidBody.velocity = climbVelocity;
        myRigidBody.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", playerHasVerticalSpeed);
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
           transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x) * 2.5f , 2.5f);
           facingSide = Mathf.Sign(myRigidBody.velocity.x);
        }

    }

    public void Die()
    {
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy","ProjectileEnemy")))
        {
            gameSession.TakeHealth();
            if(gameSession.GetPlayerHealth() == 0 && checkIfDead < 1)
            { 
                checkIfDead++;
                isAlive = false;
                myAnimator.SetTrigger("Dying");
                AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position);
                GetComponent<Rigidbody2D>().velocity = deathKick;
                StartCoroutine(processDeath());
            }

            
        }

        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazard")))
        {
            gameSession.RemoveHearts();
            gameSession.SetPlayerHealth(0);   
            if (checkIfDead < 1)
            {
                
                checkIfDead++;
                isAlive = false;
                myAnimator.SetTrigger("Dying");
                AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position);
                GetComponent<Rigidbody2D>().velocity = deathKick;
                StartCoroutine(processDeath());
            }
        }

    }

    public int GetPlayerScore()
    {
        return score;
    }

    IEnumerator processDeath()
    {
        yield return new WaitForSeconds(delayTimeDeath);
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }

    IEnumerator shootingEnumerator()
    {
        var projectileSide = Quaternion.Euler(0, 0, 0);
        myAnimator.SetBool("Shooting", true);
        yield return new WaitForSeconds(delayTimeShoot * Time.timeScale);
    //    facingSide = Mathf.Sign(myRigidBody.velocity.x);
        if (facingSide < 0)
        {
           projectileSide = Quaternion.Euler(0, 0, 180);
        }
        else
        {
            projectileSide = Quaternion.Euler(0, 0, 0);
        }
        GameObject newProjectile = Instantiate(projectile, gun.transform.position, projectileSide) as GameObject;
        newProjectile.transform.parent = projectileParent.transform;
        AudioSource.PlayClipAtPoint(projectileSound, transform.position);
        myAnimator.SetBool("Shooting", false);
        yield return new WaitForSeconds(destroyProjectileTime);
        Destroy(newProjectile);
        
    }

        private void Fire(float facingSide)
    {
        
      

        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
        
            if (Time.time > fireRate + lastShot)
            {
                StartCoroutine(shootingEnumerator());
                lastShot = Time.time;
            }

        }      
    }

    public void PowerUp(int powerUpValue)
    {
        fireRate = fireRate / powerUpValue;
    }

    public void GotKey()
    {
        hasKey = true;
    }

    public bool PlayerGotKey()
    {
        return hasKey;
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
