using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Author: Olsen, Andrew
 * Last Updated: 05/14/2024
 * Contains data for Thief enemy
 */
public class Thief : EnemyClass
{
    /// <summary>
    /// Initializes thief stats
    /// </summary>
    void Awake()
    {
        enemyType = "thief";
        health = 3;
        attack = 10;
        speed = 20;
    }
}
