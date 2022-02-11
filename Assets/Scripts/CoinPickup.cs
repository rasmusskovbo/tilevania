using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    private GameSession _gameSession;
    [SerializeField] private AudioClip pickupSFX;

    private void Start()
    {
        _gameSession = FindObjectOfType<GameSession>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           Destroy(gameObject);
           AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position);
           _gameSession.AddCoin();
        }
    }
}
