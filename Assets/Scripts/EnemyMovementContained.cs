using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementContained : EnemyMovement
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        FlipFacing();
    }
    
}
