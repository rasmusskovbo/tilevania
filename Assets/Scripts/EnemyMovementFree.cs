using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementFree : EnemyMovement
{
    
    private void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        FlipFacing();
    }
    
}
