using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private AudioClip pickupSFX;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           Destroy(gameObject);
           AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position, 0.25f);
           FindObjectOfType<GameSession>().AddCoin();
        }
    }
}
