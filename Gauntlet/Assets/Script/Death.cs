using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Author: Olsen, Andrew; health drain functionality by Daniel
 * Last Updated: 05/14/2024
 * Contains data for Death enemy including health drain functionality
 */

public class Death : EnemyClass
{

    public int healthDecreaseRate = 1;
    public int healthDecreaseInterval = 1; // 1 second
    private float nextHealthDecreaseTime = 0f;

    /// <summary>
    /// Initializes death's stats.
    /// </summary>
    void Awake()
    {
        enemyType = "death";
        health = 20;
        attack = 10;
        speed = 25;
    }

    /// <summary>
    /// Activates health drain function.
    /// </summary>
    void FixedUpdate()
    {
        DecreaseHealthOverTime();
    }

    /// <summary>
    /// Drains player health on regular intervals.
    /// </summary>
    private void DecreaseHealthOverTime()
    {
        if (Time.time >= nextHealthDecreaseTime)
        {
            GameManager.Instance.playerHealth -= healthDecreaseRate;
            nextHealthDecreaseTime = Time.time + healthDecreaseInterval;
        }
    }
}
