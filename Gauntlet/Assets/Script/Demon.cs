using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Author: Olsen, Andrew
 * Last Updated: 05/14/2024
 * Contains data for Demon enemy
 */

public class Demon : EnemyClass
{
    /// <summary>
    /// Initializes demon stats
    /// </summary>
    void Awake()
    {
        enemyType = "demon";
        health = 5;
        attack = 15;
        speed = 15;
    }
}
