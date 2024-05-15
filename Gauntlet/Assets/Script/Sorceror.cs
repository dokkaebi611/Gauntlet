using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Author: Olsen, Andrew
 * Last Updated: 05/14/2024
 * Contains data for Sorceror enemy
 */
public class Sorceror : EnemyClass
{
    /// <summary>
    /// Initializes sorceror stats
    /// </summary>
    void Awake()
    {
        enemyType = "sorceror";
        health = 6;
        attack = 15;
        speed = 15;
    }
}
