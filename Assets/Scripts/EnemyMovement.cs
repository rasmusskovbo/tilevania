using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] public float moveSpeed;
    public Rigidbody2D body;
    public GameObject player;
    public PlayerMovement playerScript;
    
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = FindObjectOfType<PlayerMovement>();

    }
    
    void Update()
    {
        body.velocity = new Vector2(moveSpeed, 0f);
    }

    public void FlipFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(body.velocity.x)), 1f);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.Equals(player))
        {
            if (!FindObjectOfType<PlayerMovement>().isAlive) return;
            FindObjectOfType<PlayerMovement>().Die();
        }
    }
}
