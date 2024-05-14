using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevel2 : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        health *= 2;  // Double the health of the base enemy
    }

    // Update is called once per frame
    void Update()
    {
        base.MoveTowardsPlayer();
    }
}
