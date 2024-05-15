using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Author: Olsen, Andrew
 * Last Updated: 05/14/2024
 * Dictates enemy bullet behavior when colliding with an object
 */
public class EnemyBullet : MonoBehaviour
{
    private PlayerController playerController;

    public static GameManager Instance { get; private set; }

    /// <summary>
    /// Dictates how bullet interacts with objects it collides with.
    /// If hits player, player loses health and bullet is destroyed.
    /// If hits wall, bullet is destroyed.
    /// </summary>
    /// <param name="collision">The object being collided with</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.playerHealth -= 10;
            Debug.Log("Player hit by enemy! Remaining health: " + GameManager.Instance.playerHealth);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
