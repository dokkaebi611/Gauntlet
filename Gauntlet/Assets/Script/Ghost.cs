using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Author: Olsen, Andrew
 * Last Updated: 05/14/2024
 * Contains data for Demon enemy
 */
public class Ghost : EnemyClass
{
    /// <summary>
    /// Initializes ghost stats
    /// </summary>
    void Awake()
    {
        enemyType = "ghost";
        health = 1;
        attack = 5;
        speed = 5;
    }
}
