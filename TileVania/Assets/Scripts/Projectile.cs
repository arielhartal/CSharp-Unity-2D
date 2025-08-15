using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float damage = 50f;
    [SerializeField] float speedEnemy = 2f;
    [SerializeField] AudioClip projectileSound;
    [SerializeField] float speedVerticalProjectile = -2f;


    Rigidbody2D myRigidBody;
    Collider2D myCollider2D;


    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myCollider2D = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.tag == "Projectile")
        {
            transform.Translate(Mathf.Sign(myRigidBody.velocity.x) * speed * Time.deltaTime, 0f, 0f);
        }

        if(transform.tag == "Projectile Enemy")
        {
            transform.Translate(Mathf.Sign(myRigidBody.velocity.x) * speedEnemy * Time.deltaTime, 0f, 0f);
        }

        if(transform.tag == "Vertical Projectile")
        {
            transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            transform.Translate(0f, speedVerticalProjectile * Time.deltaTime, 0f, Space.World);
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        var enemy = otherCollider.GetComponent<Enemy>();
        var player = otherCollider.GetComponent<Player>();

        if (enemy)
        {
            enemy.DealDamage(damage);
            Destroy(gameObject);
        }

        if (myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground", "Hazard")) && transform.tag != "Vertical Projectile")
        {
            AudioSource.PlayClipAtPoint(projectileSound, transform.position);
            Destroy(gameObject);
        }
        if(transform.tag == "Vertical Projectile")
        {
            Destroy(gameObject);
        }

        if (player)
        {
            player.Die();
            Destroy(gameObject);
        }
    }


}
