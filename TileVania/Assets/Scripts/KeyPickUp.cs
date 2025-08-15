using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickUp : MonoBehaviour
{
    [SerializeField] AudioClip keySFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(keySFX, Camera.main.transform.position);
        FindObjectOfType<Player>().GotKey();
        Destroy(gameObject);
    }
}
