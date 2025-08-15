using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] float moveSpeed = 1f;
  

    Rigidbody2D myRigidBody;



    // Start is called before the first frame update
    void Start()
    {

        myRigidBody = GetComponent<Rigidbody2D>();

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
            
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.tag == "Projectile") { return; }
         transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);       

    }

 


}
