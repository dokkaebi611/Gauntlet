using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform[] players;
    public float speed = 5.0f;
    protected Rigidbody enemyRigidbody;
    public int health = 10;

    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Transform closestPlayer = FindClosestPlayer();
        if (closestPlayer != null)
        {
            MoveTowardsPlayer(closestPlayer);
        }
    }

    protected void MoveTowardsPlayer(Transform targetPlayer)
    {
        Vector3 direction = (targetPlayer.position - transform.position).normalized;
        enemyRigidbody.MovePosition(transform.position + direction * speed * Time.deltaTime);
    }

    Transform FindClosestPlayer()
    {
        Transform closestPlayer = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform player in players)
        {
            if (player == null) continue; // Null »Æ¿Œ
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPlayer = player;
            }
        }

        if (closestPlayer != null)
        {
            Debug.Log("Closest player: " + closestPlayer.name);
        }

        return closestPlayer;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
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
    }
}
