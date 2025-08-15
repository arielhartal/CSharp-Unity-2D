using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPickup : MonoBehaviour
{
    [SerializeField] AudioClip powerUpSFX;
    [SerializeField] int powerUpValue = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(powerUpSFX, Camera.main.transform.position);
        FindObjectOfType<Player>().PowerUp(powerUpValue);
        Destroy(gameObject);
    }
}
