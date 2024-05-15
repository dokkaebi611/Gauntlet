using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
 * 
 * Author: Olsen, Andrew
 * Last Updated: 05/14/2024
 * Contains data for overall enemy and behavior
 */

public class EnemyClass : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Transform[] players;
    private Rigidbody enemyRigidbody;
    public int health;
    public int attack;
    public float speed;
    public string enemyType;
    public GameObject rangedAttack;
    public Transform rangedSpawn;
    public float rangedSpeed;
    private bool isTouchingPlayer = false;
    public UnityEvent OnDestroyed;

    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
        StartCoroutine(EnemyAttack());
    }

    void Update()
    {
        MoveTowardsPlayer();
    }

    /// <summary>
    /// Moves the enemy toward the closest player
    /// </summary>
    void MoveTowardsPlayer()
    {
        Transform closestPlayer = GetClosestPlayer();
        Vector3 direction = (closestPlayer.position - transform.position).normalized;
        enemyRigidbody.MovePosition(transform.position + direction * speed * Time.deltaTime);
    }

    /// <summary>
    /// Finds the closest player from an array of players
    /// </summary>
    Transform GetClosestPlayer()
    {
        Transform closest = null;
        float minDistance = float.MaxValue;
        foreach (Transform player in players)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = player;
            }
        }
        return closest;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
            GameManager.Instance.score += health;
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        OnDestroyed?.Invoke();
    }

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
        else if (collision.gameObject.CompareTag("Player"))
        {
            isTouchingPlayer = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouchingPlayer = false;
        }
    }

    private IEnumerator EnemyAttack()
    {
        while (true)
        {
            if (isTouchingPlayer)
            {
                MeleeAttack();
                yield return new WaitForSeconds(1f);
            }
            else
            {
                if (enemyType == "demon" || enemyType == "sorceror" || enemyType == "lobber")
                {
                    RangedAttack();
                }
                yield return new WaitForSeconds(2f);
            }
        }
    }

    private void MeleeAttack()
    {
        GameManager.Instance.playerHealth -= attack;
        if (enemyType == "ghost")
            Destroy(gameObject);
    }

    public void RangedAttack()
    {
        Transform closestPlayer = GetClosestPlayer();
        GameObject enemyBullet = Instantiate(rangedAttack, rangedSpawn.position, Quaternion.identity);
        Rigidbody rb = enemyBullet.GetComponent<Rigidbody>();
        Vector3 directionToPlayer = (closestPlayer.position - rangedSpawn.position).normalized;
        rb.velocity = directionToPlayer * rangedSpeed;
    }
}