using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * 
 * Author: Olsen, Andrew
 * Last Updated: 05/14/2024
 * Contains data for Lobber enemy and behavior; not a subclass of enemy due to lack of melee, but base code is shared.
 */
public class Lobber : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Transform player;
    private Rigidbody enemyRigidbody;
    public int health = 3;
    public int attack = 15;
    public float speed = 10;
    public string enemyType;
    public GameObject rangedAttack;
    public Transform rangedSpawn;
    public float rangedSpeed;
    private bool isTouchingPlayer = false;
    public UnityEvent OnDestroyed;

    /// <summary>
    /// Grabs enemy rigidbody and initializes attack functionality
    /// </summary>
    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
        StartCoroutine(EnemyAttack());
    }

    /// <summary>
    /// Initializes function to move enemy toward player
    /// </summary>
    void Update()
    {
        MoveTowardsPlayer();
    }

    /// <summary>
    /// Moves the enemy toward the player
    /// </summary>
    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        enemyRigidbody.MovePosition(transform.position + direction * speed * Time.deltaTime);
    }

    /// <summary>
    /// Dictates how the enemy takes damage from the player when hit.
    /// </summary>
    /// <param name="damage">The amount of damage received from the player.</param>
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
            GameManager.Instance.score += health;
        }
    }

    /// <summary>
    /// Destroys enemy when it loses all health
    /// </summary>
    public void Die()
    {
        Destroy(gameObject);
        OnDestroyed?.Invoke();
    }

    /// <summary>
    /// Dictates what happens when enemy is struck by bullet
    /// </summary>
    /// <param name="collision">The object being collided with</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage);
            }
            Destroy(collision.gameObject);
        }
    }

    /// <summary>
    /// Allows for repeatable ranged enemy attack
    /// </summary>
    /// <returns>How long to wait between attacks</returns>
    private IEnumerator EnemyAttack()
    {
        RangedAttack();
        yield return new WaitForSeconds(2f);
    }

    /// <summary>
    /// Spawns enemy bullet and sends it toward player.
    /// </summary>
    public void RangedAttack()
    {
        GameObject enemyBullet = Instantiate(rangedAttack, rangedSpawn.position, rangedSpawn.rotation);
        Rigidbody rb = enemyBullet.GetComponent<Rigidbody>();
        Vector3 directionToPlayer = (player.position - rangedSpawn.position).normalized;
        rb.velocity = directionToPlayer * rangedSpeed;
    }
}
