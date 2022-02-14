using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] private int playerLives;
    [SerializeField] private int coins;
    
    private void Awake()
    {
        int numGamesSession = FindObjectsOfType<GameSession>().Length;

        if (numGamesSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 0)
        {
            ReduceLives();
        }
        else
        {
            ResetGameSession();
        }
    }

    private void ReduceLives()
    {
        playerLives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ResetGameSession()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(0);
    }

    public void AddCoin()
    {
        this.coins++;
        Debug.Log(coins);
    }

    public String getLives()
    {
        return playerLives.ToString();
    }
    
    public String getCoins()
    {
        return coins.ToString();
    }
}
