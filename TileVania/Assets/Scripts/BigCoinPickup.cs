using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int coinPickupScore = 1000;
    [SerializeField] int coin = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
        FindObjectOfType<GameSession>().AddToScore(coinPickupScore);
        FindObjectOfType<GameSession>().AddToCoins(coin);
        Destroy(gameObject);
    }
}
