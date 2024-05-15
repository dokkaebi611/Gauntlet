using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Author: Olsen, Andrew
 * Last Updated: 05/14/2024
 * Contains data for Grunt enemy
 */
public class Grunt : EnemyClass
{
    /// <summary>
    /// Initializes grunt stats
    /// </summary>
    void Awake()
    {
        enemyType = "grunt";
        health = 3;
        attack = 10;
        speed = 10;
    }
}
