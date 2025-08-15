using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalScroll : MonoBehaviour
{
    [Tooltip("Game units per second")]
    [SerializeField] float scrollRate = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = scrollRate * Time.deltaTime;
        transform.Translate(new Vector2(0f, xMove));
    }
}
