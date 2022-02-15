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
    private String statusText = "You won!";
    
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
        if (playerLives > 1)
        {
            ReduceLives();
        }
        else
        {
            playerLives = 0;
            statusText = "You died!";
            SceneManager.LoadScene(3);
        }
    }

    private void ReduceLives()
    {
        playerLives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResetGameSession()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(0);
//        FindObjectOfType<ScenePersist>().ResetScenePersist();
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
    
    public String getStatusText()
    {
        return statusText;
    }
}
