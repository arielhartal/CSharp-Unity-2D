using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int coinPickupScore = 100;
    [SerializeField] int coin = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
        FindObjectOfType<GameSession>().AddToScore(coinPickupScore);
        FindObjectOfType<GameSession>().AddToCoins(coin);
        Destroy(gameObject);
    }
}
