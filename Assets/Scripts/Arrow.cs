using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D body;
    private PlayerMovement playerScript;

    [SerializeField] private float arrowSpeed;
    private float spawnSpeedAndDirection;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        playerScript = FindObjectOfType<PlayerMovement>();
        spawnSpeedAndDirection = playerScript.transform.localScale.x * arrowSpeed;
    }
    
    void Update()
    {
        body.velocity = new Vector2(spawnSpeedAndDirection, 0);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.gameObject.tag)
        {
            case "Enemy":
                Destroy(col.gameObject);
                Destroy(gameObject);
                break;
            case "Platform":
                Destroy(gameObject);
                break;
        }
    }










}
