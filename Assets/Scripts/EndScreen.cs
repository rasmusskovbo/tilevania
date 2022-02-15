using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lives;
    [SerializeField] private TextMeshProUGUI coins;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI statusText;
    private GameSession session;
    
    void Start()
    {
        Destroy(FindObjectOfType<UI>().gameObject);
        Destroy(FindObjectOfType<ScenePersist>().gameObject);
        
        session = FindObjectOfType<GameSession>();
        statusText.text = session.getStatusText();
        lives.text = session.getLives();
        coins.text = session.getCoins();
        setScore();
    }

    void setScore()
    {
        float livesNo = int.Parse(lives.text);
        float coinsNo = int.Parse(coins.text);

        if (livesNo > 0)
        {
            score.text = (livesNo * coinsNo).ToString();
        }
        else
        {
            score.text = Math.Ceiling(coinsNo / 2).ToString();
        }
    }

    
}
