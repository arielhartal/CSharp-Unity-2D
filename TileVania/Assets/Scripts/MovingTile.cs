using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTile : MonoBehaviour
{
    [Tooltip("Game units per second")]
    [SerializeField] float scrollRate = 0.2f;
    bool playerOnTile = false;
    Collider2D myBodyCollider;

    // Start is called before the first frame update
    void Start()
    {
        myBodyCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            playerOnTile = true;
        }
        if(playerOnTile)
        { 
            float xMove = scrollRate * Time.deltaTime;
            transform.Translate(new Vector2(xMove, 0f));
        }
    }


}

