using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float speed = 5.0f;
    private Rigidbody enemyRigidbody;
    public int health = 10;

    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MoveTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        // 플레이어 쪽으로 이동
        Vector3 direction = (player.position - transform.position).normalized;
        enemyRigidbody.MovePosition(transform.position + direction * speed * Time.deltaTime);
    }

    // 피격 처리
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