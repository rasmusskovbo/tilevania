using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lives;
    [SerializeField] private TextMeshProUGUI coins;
    private GameSession _session;
    
    private void Start()
    {
        _session = FindObjectOfType<GameSession>();
    }
    
    void Update()
    {
        setLives(_session.getLives());
        setCoins(_session.getCoins());
    }

    void setLives(String value)
    {
        lives.text = value;
    }

    void setCoins(String value)
    {
        coins.text = value;
    }
}
